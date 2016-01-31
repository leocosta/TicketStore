using TicketStore.Domain.Common;
using TicketStore.Domain.Orders;
using TicketStore.Infra.Data.EF.Contexts;

namespace TicketStore.Infra.Data.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(IUnitOfWork unitOfWork)
            : base((unitOfWork as UnitOfWork).TicketStoreContext) { }
    
        public Order Get(int id)
        {
            return base.Single(i => i.OrderId == id);
        }
    }
}
