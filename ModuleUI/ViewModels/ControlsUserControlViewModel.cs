using Database.Contexts;
using Database.Entity;
using Database.Managers;
using DataTransferService.EventAggregator;
using Microsoft.EntityFrameworkCore;
using ModuleUI.Managers;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System.Windows;
using System.Windows.Input;

namespace ModuleUI.ViewModels
{
    public class ControlsUserControlViewModel : BindableBase
    {
        #region Fields
        private EmployeeContext _employeeContext;
        private IDialogService _dialogService;
        private IEventAggregator _eventAggregator;
        private DbEntity? _selectedDbEntity;
        private int _selectedTabNumber;
        private string? _selectedComboBox;

        private List<Employee>? _employees;
        private List<Department>? _departments;
        private List<Position>? _positions;
        #endregion

        #region Properties
        public List<Employee>? Employees
        {
            get { return _employees; }
            set { SetProperty(ref _employees, value); }
        }

        public List<Department>? Departments
        {
            get { return _departments; }
            set { SetProperty(ref _departments, value); }
        }

        public List<Position>? Positions
        {
            get { return _positions; }
            set { SetProperty(ref _positions, value); }
        }
        #endregion

        public ControlsUserControlViewModel(EmployeeContext employeeContext, IDialogService dialogService, IEventAggregator eventAggregator)
        {
            _employeeContext = employeeContext;
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;

            DialogManagerCaller.RegisterDialogManagerForEntity(new EmployeeDialogManager(employeeContext, dialogService), typeof(Employee));
            DialogManagerCaller.RegisterDialogManagerForEntity(new DepartmentDialogManager(employeeContext, dialogService), typeof(Department));
            DialogManagerCaller.RegisterDialogManagerForEntity(new PositionDialogManager(employeeContext, dialogService), typeof(Position));

            DialogManagerCaller.RegisterDialogManagerForTabNumber(new EmployeeDialogManager(employeeContext, dialogService), 0);
            DialogManagerCaller.RegisterDialogManagerForTabNumber(new DepartmentDialogManager(employeeContext, dialogService), 1);
            DialogManagerCaller.RegisterDialogManagerForTabNumber(new PositionDialogManager(employeeContext, dialogService), 2);

            _eventAggregator.GetEvent<SendDbEntityEvent>().Subscribe(dbEntity => _selectedDbEntity = dbEntity);
            _eventAggregator.GetEvent<SendTabNumberEvent>().Subscribe(selectedTabNumber => _selectedTabNumber = (int)selectedTabNumber);
        }

        #region Commands
        private DelegateCommand<string>? _sendSelectedComboBox;
        public ICommand SendSelectedComboBox => _sendSelectedComboBox ??= new DelegateCommand<string>(selectedComboBox => RememberComboBox(selectedComboBox));

        private DelegateCommand? _addCommand;
        public ICommand AddCommand => _addCommand ??= new DelegateCommand(Add);

        private DelegateCommand? _changeCommand;
        public ICommand ChangeCommand => _changeCommand ??= new DelegateCommand(Change);

        private DelegateCommand? _deleteCommand;
        public ICommand DeleteCommand => _deleteCommand ??= new DelegateCommand(Delete);

        private DelegateCommand? _reloadCommand;
        public ICommand ReloadCommand => _reloadCommand ??= new DelegateCommand(async () => await GetDataFromContext());
        #endregion

        #region Methods
        private void RememberComboBox(string selectedComboBox)
        {
            _selectedComboBox = selectedComboBox;
        }

        private async void Add()
        {
            IDialogManager dialogManager = _selectedTabNumber.GetDialogManager();
            dialogManager.ShowAddDialog(dialogManager.GetEmptyEntity(), Employees, Departments, Positions);
            await GetDataFromContext();
        }

        private async void Change()
        {
            if (_selectedDbEntity != null) MessageBox.Show($"Change window");

            /*
            IDialogManager dialogManager = _selectedTab.GetDialogManager(this);
            dialogManager.ShowAddDialog(dialogManager.GetEmptyEntity());
            await LoadData();*/
        }

        private async void Delete()
        {
            MessageBox.Show("Delete window");

            /*
            IDialogManager dialogManager = _selectedTab.GetDialogManager(this);
            dialogManager.ShowAddDialog(dialogManager.GetEmptyEntity());
            await LoadData();*/
        }

        private async Task GetDataFromContext()
        {
            _employees = await _employeeContext.Employees.ToListAsync();
            _departments = await _employeeContext.Departments.ToListAsync();
            _positions = await _employeeContext.Positions.ToListAsync();

            _eventAggregator.GetEvent<EmployeesListEvent>().Publish(Employees);
            _eventAggregator.GetEvent<DepartmentsListEvent>().Publish(Departments);
            _eventAggregator.GetEvent<PositionsListEvent>().Publish(Positions);
        }
        #endregion
    }
}
