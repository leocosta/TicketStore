using System.Data.Entity;
using TicketStore.Domain.Orders;

namespace TicketStore.Infra.Data.Persistence.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(DbContext context)
            : base(context) { }
    
        public Order Get(int id)
        {
            return base.FirstOrDefault(i => i.OrderId == id);
        }
    }
}
