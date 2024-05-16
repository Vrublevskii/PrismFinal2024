using Database.Contexts;
using Database.Entity;
using DataTransferService.EventAggregator;
using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using System.Windows.Input;

namespace ModuleUI.ViewModels
{
    public class ResultListUserControlViewModel : BindableBase
    {
        #region Fields
        private List<Employee>? _employees;
        private List<Department>? _departments;
        private List<Position>? _positions;

        //private EmployeeContext _employeeContext;
        private IEventAggregator _eventAggregator;
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

        public ResultListUserControlViewModel(/*EmployeeContext employeeContext,*/ IEventAggregator eventAggregator)
        {
            //_employeeContext = employeeContext;
            _eventAggregator = eventAggregator;

            _eventAggregator.GetEvent<EmployeesListEvent>().Subscribe(e => Employees = e );
            _eventAggregator.GetEvent<DepartmentsListEvent>().Subscribe(d => Departments = d );
            _eventAggregator.GetEvent<PositionsListEvent>().Subscribe(p => Positions = p );
        }
        #region Commands
        /*
        private DelegateCommand<List<Employee>>? _getDataCommand;
        public ICommand GetDataCommand => _getDataCommand ??= new DelegateCommand<List<Employee>>(async (List<Employee> employees) => await GetData());
        */
        private DelegateCommand<DbEntity>? _sendSelectedItem;
        public ICommand SendSelectedItem => _sendSelectedItem ??= new DelegateCommand<DbEntity>(dbEntity => _eventAggregator.GetEvent<SendDbEntityEvent>().Publish(dbEntity));
        
        private DelegateCommand<Object>? _sendActiveTabNumber;
        public ICommand SendActiveTabNumber => _sendActiveTabNumber ??= new DelegateCommand<Object>(tabNumber => _eventAggregator.GetEvent<SendTabNumberEvent>().Publish(tabNumber));
        #endregion

        #region Methods
        /*
        private async Task GetData()
        {
            _employees = await _employeeContext.Employees.ToListAsync();
            _departments = await _employeeContext.Departments.ToListAsync();
            _positions = await _employeeContext.Positions.ToListAsync();
        }*/
        #endregion
    }
}
