using Database.Entity;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Windows.Input;
using static DataTransferService.Enums.ParamStrings;

namespace TestProjectPrism.DialogModule.ViewModels
{
    public class AddEditEmployeeViewModel : BindableBase, IDialogAware
    {
        #region Fields
        private string? _title;
        private Employee? _employee;
        List<Department>? _departments;
        List<Position>? _positions;
        private string? _okButtonContent;
        #endregion

        #region Properties
        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public Employee? Employee
        {
            get => _employee;
            set => SetProperty(ref _employee, value);
        }

        public List<Department>? Departments
        {
            get => _departments;
            set => SetProperty(ref _departments, value);
        }

        public List<Position>? Positions
        {
            get => _positions;
            set => SetProperty(ref _positions, value);
        }

        public string? OkButtinContent
        {
            get => _okButtonContent;
            set => SetProperty(ref _okButtonContent, value);
        }
        #endregion

        #region Commands
        private DelegateCommand<string>? _closeCommand;
        public ICommand CloseCommand => _closeCommand ??= new DelegateCommand<string>(CloseDialog);

        public event Action<IDialogResult>? RequestClose;
        #endregion

        #region Methods
        public bool CanCloseDialog() => true;

        public void OnDialogClosed() { }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>(TitleString);
            Employee = parameters.GetValue<Employee>(EntityString);
            Departments = parameters.GetValue<List<Department>>(DepartmentsString);
            Positions = parameters.GetValue<List<Position>>(PositionsString);
            OkButtinContent = parameters.GetValue<string>(OkButtonContentString);
        }

        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;
            if (parameter?.ToLower() == "true") result = ButtonResult.OK;
            else if (parameter?.ToLower() == "false") result = ButtonResult.Cancel;

            RaiseRequestClose(new DialogResult(result, new DialogParameters() { { EntityString, Employee } }));
        }

        public virtual void RaiseRequestClose(DialogResult dialogResult) => RequestClose?.Invoke(dialogResult);
        #endregion
    }
}


