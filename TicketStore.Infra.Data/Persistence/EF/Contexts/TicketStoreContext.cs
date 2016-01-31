using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using TicketStore.Domain.Events;
using TicketStore.Domain.Orders;
using TicketStore.Domain.CreditCards;
using TicketStore.Infra.Data.Persistence.EF.Configurations;

namespace TicketStore.Infra.Data.Persistence.EF.Contexts
{
    public class TicketStoreContext : DbContext
    {
        public TicketStoreContext()
            : base("TicketStoreContext")
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TicketStoreContext>());
        }

        public IDbSet<User> Users { get; set; }
        public IDbSet<Event> Events { get; set; }
        public IDbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            setPropertyConfigurations(modelBuilder);
            setConvetions(modelBuilder);
            setEntityTypeConfigurations(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        private void setConvetions(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        private void setPropertyConfigurations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(100));
        }

        private void setEntityTypeConfigurations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new EventConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
        }
    }
}