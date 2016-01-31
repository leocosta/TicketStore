using TicketStore.Domain.Common;

namespace TicketStore.Domain.Events
{
    public interface IEventRepository : IRepository<Event>
    {
        Event Get(int id);
    }
}
