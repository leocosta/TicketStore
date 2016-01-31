using TicketStore.Domain.Common;
using TicketStore.Domain.Events;
using TicketStore.Infra.Data.EF.Contexts;

namespace TicketStore.Infra.Data.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(IUnitOfWork unitOfWork)
            : base((unitOfWork as UnitOfWork).TicketStoreContext) { }
    
        public Event Get(int id)
        {
            return base.Single(i => i.EventId == id);
        }
    }
}
