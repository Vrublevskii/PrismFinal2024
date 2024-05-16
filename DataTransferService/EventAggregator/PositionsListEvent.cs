using Database.Entity;
using Prism.Events;

namespace DataTransferService.EventAggregator
{
    public class PositionsListEvent : PubSubEvent<List<Position>> { }
}
