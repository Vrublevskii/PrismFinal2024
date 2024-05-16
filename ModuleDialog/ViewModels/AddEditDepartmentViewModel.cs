using Database.Entity;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Windows.Input;
using static DataTransferService.Enums.ParamStrings;

namespace TestProjectPrism.ViewModels
{
    public class AddEditDepartmentViewModel : BindableBase, IDialogAware
    {
        private string? _title;
        private Department? _department;
        private string? _okButtinContent;

        private DelegateCommand<string>? _closeCommand;

        public event Action<IDialogResult>? RequestClose;

        public string? Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public Department? Department
        {
            get => _department;
            set => SetProperty(ref _department, value);
        }


        public string? OkButtinContent
        {
            get => _okButtinContent;
            set => SetProperty(ref _okButtinContent, value);
        }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {

        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            Title = parameters.GetValue<string>(TitleString);
            Department = parameters.GetValue<Department>(EntityString);
            OkButtinContent = parameters.GetValue<string>(OkButtonContentString);
        }

        public ICommand CloseCommand => _closeCommand ??= new DelegateCommand<string>(CloseDialog);

        protected virtual void CloseDialog(string parameter)
        {
            ButtonResult result = ButtonResult.None;

            if (parameter?.ToLower() == "true") result = ButtonResult.OK;
            else if (parameter?.ToLower() == "false") result = ButtonResult.Cancel;

            RaiseRequestClose(new DialogResult(result, new DialogParameters()
        {
            { EntityString, _department }
        }));
        }

        public virtual void RaiseRequestClose(DialogResult dialogResult) => RequestClose?.Invoke(dialogResult);
    }
}
