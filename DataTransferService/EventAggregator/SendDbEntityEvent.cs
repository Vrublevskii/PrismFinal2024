using Database.Entity;
using Prism.Events;

namespace DataTransferService.EventAggregator
{
    public class SendDbEntityEvent : PubSubEvent<DbEntity> { }
}
