using Database.Entity;
using Prism.Events;
using System.Collections.Generic;

namespace DataTransferService.EventAggregator
{
    public class EmployeesListEvent : PubSubEvent<List<Employee>> { }
}
