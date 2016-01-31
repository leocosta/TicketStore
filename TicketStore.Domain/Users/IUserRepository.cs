using TicketStore.Domain.Common;

namespace TicketStore.Domain.CreditCards
{
    public interface IUserRepository : IRepository<User>
    {
        User Get(int id);
        User Get(string email);
    }
}
