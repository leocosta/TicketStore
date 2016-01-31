using TicketStore.Domain.Common;

namespace TicketStore.Infra.Data.EF.Contexts
{
    public class UnitOfWork : IUnitOfWork
    {
        public TicketStoreContext TicketStoreContext { get; private set; }

        public UnitOfWork(TicketStoreContext ticketStoreContext)
        {
            TicketStoreContext = ticketStoreContext;
        }

        public void Commit()
        {
            TicketStoreContext.Commit();
        }

        public void Dispose()
        {
            TicketStoreContext.Commit();
        }
    }
}
