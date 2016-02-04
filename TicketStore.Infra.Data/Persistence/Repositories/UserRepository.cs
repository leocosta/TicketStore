using System.Data.Entity;
using TicketStore.Domain.Users;

namespace TicketStore.Infra.Data.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context) { }

        public User Get(string email)
        {
            return base.FirstOrDefault(i => i.Email == email);
        }

        public User Get(int id)
        {
            return base.FirstOrDefault(i => i.UserId == id);
        }
    }
}
