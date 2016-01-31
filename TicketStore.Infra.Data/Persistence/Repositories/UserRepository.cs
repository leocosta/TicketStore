using System.Data.Entity;
using TicketStore.Domain.Common;
using TicketStore.Domain.CreditCards;
using TicketStore.Infra.Data.Persistence.EF.Contexts;

namespace TicketStore.Infra.Data.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context) { }

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
