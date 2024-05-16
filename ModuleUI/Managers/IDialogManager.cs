using Database.Entity;
using Prism.Services.Dialogs;
using System;

namespace ModuleUI.Managers
{
    public interface IDialogManager
    {
        public void ShowAddDialog(DbEntity departmentToAdd, List<Employee> employees, List<Department> departments, List<Position> positions);
        public void ShowEditDialog(DbEntity entityToChange, List<Department> departments, List<Position> positions);
        public DbEntity GetEmptyEntity();
    }
}