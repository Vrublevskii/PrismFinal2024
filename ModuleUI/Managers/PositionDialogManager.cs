using Database.Contexts;
using Database.Entity;
using Microsoft.EntityFrameworkCore;
using Prism.Services.Dialogs;
using System.Windows;
using static DataTransferService.Enums.ParamStrings;
using static DataTransferService.Enums.ViewModelsNamesStrings;
using static ModuleUI.Managers.Utils<Database.Entity.Position>;

namespace ModuleUI.Managers
{
    public class PositionDialogManager : IDialogManager
    {
        EmployeeContext _employeeContext;
        IDialogService _dialogService;

        public PositionDialogManager(EmployeeContext employeeContext, IDialogService dialogService)
        {
            _employeeContext = employeeContext;
            _dialogService = dialogService;
        }

        public void ShowAddDialog(DbEntity positionToAdd, List<Employee>? employees, List<Department>? departments, List<Position>? positions)
        {
            DialogParameters parameters = new DialogParameters
            {
                {TitleString, TitleAddDepartmentValueString },
                {EntityString, positionToAdd },
                {DepartmentsString, positions },
                {OkButtonContentString, OkAddDepartmentValueString }
            };
            AddDialogIfErrorRepeat(parameters, positionToAdd, positions);
        }

        private void AddDialogIfErrorRepeat(DialogParameters parameters, DbEntity positionToAdd, List<Position>? positions)
        {
            _dialogService.ShowDialog(AddEditPositionString, parameters, async r =>
            {
                if (r.Result != ButtonResult.OK) return;
                Position positionToAddAsPosition = (Position)positionToAdd;
                string namePosition = positionToAddAsPosition.NamePosition;
                bool positionAlreadyPresent = false;
                foreach (var pos in positions)
                {
                    if (pos.NamePosition.Equals(namePosition))
                    {
                        MessageBox.Show("Такая должность уже есть");
                        AddDialogIfErrorRepeat(parameters, positionToAddAsPosition, positions);
                        positionAlreadyPresent = true;
                        _employeeContext.Positions.FromSqlRaw($"SELECT * FROM Departments");
                        break;
                    }
                }
                await CreateEntityInContextIfFalse(positionAlreadyPresent, _employeeContext, "Такая должность уже есть", parameters, positionToAddAsPosition, positions, AddDialogIfErrorRepeat);
            });
        }
        /*
         
         
         
        
        public void ShowAddDialog(DbEntity positionToAdd, List<Employee> employees, List<Department> departments, List<Position> positions)
        {
            DialogParameters parameters = new DialogParameters
            {
                {TitleString, TitleAddDepartmentValueString },
                {EntityString, positionToAdd },
                {DepartmentsString, departments },
                {OkButtonContentString, OkAddDepartmentValueString }
            };
            _dialogService.ShowDialog(AddEditPositionString, parameters, async r =>
            {
                if (r.Result != ButtonResult.OK) return;
                try
                {
                    await positionToAdd.GetManager().CreateInContext(_employeeContext, positionToAdd);
                    await _employeeContext.SaveChangesAsync();
                }
                catch (InvalidOperationException)
                {
                    MessageBox.Show("Такой отдел уже есть");
                    ShowAddDialog(new Position { NamePosition = ((Position)positionToAdd).NamePosition }, employees, departments, positions);
                }
                catch (DbUpdateException)
                {
                    MessageBox.Show("Такой отдел уже есть");
                    positionToAdd.GetManager().GetEntitiesFromDbForContext(_employeeContext);
                    ShowAddDialog(new Position { NamePosition = ((Position)positionToAdd).NamePosition }, employees, departments, positions);
                }
            });
        }*/
        /*
        public void ShowAddDialog(DbEntity positionToAdd, List<Employee>? employees, List<Department>? departments, List<Position>? positions)
        {
            Position positionsToAdd = new Position();
            DialogParameters parameters = new DialogParameters
            {
                {TitleString, TitleAddDepartmentValueString },
                {EntityString, positionsToAdd },
                {DepartmentsString, positions },
                {OkButtonContentString, OkAddDepartmentValueString }
            };
            AddDialogIfErrorRepeat(parameters, positionsToAdd, positions);
        }

        private void AddDialogIfErrorRepeat(DialogParameters parameters, Position positionsToAdd, List<Position>? positions)
        {
            _dialogService.ShowDialog(AddEditPositionString, parameters, async r =>
            {
                if (r.Result != ButtonResult.OK) return;
                string namePosition = positionsToAdd.NamePosition;
                bool positionAlreadyPresent = false;
                foreach (var pos in positions)
                {
                    if (pos.NamePosition.Equals(namePosition))
                    {
                        MessageBox.Show("Такой отдел уже есть");
                        AddDialogIfErrorRepeat(parameters, positionsToAdd, positions);
                        positionAlreadyPresent = true;
                        _employeeContext.Positions.FromSqlRaw($"SELECT * FROM Departments");
                        break;
                    }
                }
                await CreateEntityInContextIfFalse(positionAlreadyPresent, _employeeContext, parameters, positionsToAdd, positions, AddDialogIfErrorRepeat);
            });
        }
        */

        public void ShowEditDialog(DbEntity entityEmployeeToChange, List<Department> departments, List<Position> positions)
        {
            throw new NotImplementedException();
        }

        public DbEntity GetEmptyEntity() => new Position();

        /*
        

        public void ShowAddDialog(IDialogService dialogService,
            BindableBase invokerViewModel,
            DbEntity emptyEntity,
            Action<IDialogResult> callBack)
        {
            ControlsViewModel viewModel = (ControlsViewModel)invokerViewModel;
            DialogParameters parameters = new DialogParameters
            {
                {ParamStrings.Title, ParamStrings.TitleAddPositionValue },
                {ParamStrings.Entity, emptyEntity },
                {ParamStrings.OkButtonContent, ParamStrings.OkAddPositionValue }
            };
            dialogService.ShowDialog(ViewModelsNamesStrings.AddEditPosition, parameters, callBack);
        }
        */




    }
}
