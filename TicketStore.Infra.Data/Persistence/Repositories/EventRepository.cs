using System.Data.Entity;
using TicketStore.Domain.Common;
using TicketStore.Domain.Events;
using TicketStore.Infra.Data.Persistence.EF.Contexts;

namespace TicketStore.Infra.Data.Persistence.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(DbContext context)
            : base(context) { }
    
        public Event Get(int id)
        {
            return base.Single(i => i.EventId == id);
        }
    }
}
