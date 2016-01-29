using System.Data.Entity.ModelConfiguration;
using TicketStore.Domain.Users;

namespace TicketStore.Infra.Data.EF.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            base.HasKey(u => u.UserId);
        }
    }
}