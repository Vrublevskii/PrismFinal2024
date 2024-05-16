using Database.Contexts;
using Database.Entity;
using Microsoft.EntityFrameworkCore;
using Prism.Services.Dialogs;
using System.Windows;

namespace ModuleUI.Managers
{
    internal static class Utils<T> where T : DbEntity
    {
        internal static async Task CreateEntityInContextIfFalse(bool entityAlreadyPresent, EmployeeContext employeeContext, string errorMessage,
            DialogParameters parameters, T entityToAdd, List<T> entities, Action<DialogParameters, T, List<T>> addDialogIfError)
        {
            if (entityAlreadyPresent) return;
            await entityToAdd.GetManager().CreateInContext(employeeContext, entityToAdd);
            try
            {
                await employeeContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                MessageBox.Show(errorMessage);
                addDialogIfError(parameters, entityToAdd, entities);
            }
        }
    }
}
