using Database.Entity;
using Prism.Events;
using System.Collections.Generic;

namespace DataTransferService.EventAggregator
{
    public class DepartmentsListEvent : PubSubEvent<List<Department>> { }
}
