using System.Data.Entity;
using TicketStore.Domain.Users;
using TicketStore.Infra.Data.EF.Configurations;

namespace TicketStore.Infra.Data.EF.Contexts
{
    public class TicketStoreContext : DbContext
    {
        public TicketStoreContext()
            : base("TicketStoreContext")
        {
        }

        public IDbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public void Commit()
        {
            SaveChanges();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}