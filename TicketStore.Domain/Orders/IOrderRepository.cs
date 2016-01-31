using TicketStore.Domain.Common;

namespace TicketStore.Domain.Orders
{
    public interface IOrderRepository : IRepository<Order>
    {
        Order Get(int id);
    }
}
