using TicketStore.Domain.Common;
using TicketStore.Domain.Users;
using TicketStore.Infra.Data.EF.Contexts;

namespace TicketStore.Infra.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork)
            : base((unitOfWork as UnitOfWork).TicketStoreContext) { }

        public User Get(string email)
        {
            return base.Single(i => i.Email == email);
        }

        public User Get(int id)
        {
            return base.Single(i => i.UserId == id);
        }
    }
}
