using System.Data.Entity.ModelConfiguration;
using TicketStore.Domain.Events;

namespace TicketStore.Infra.Data.EF.Configurations
{
    public class EventConfiguration : EntityTypeConfiguration<Event>
    {
        public EventConfiguration()
        {
            base.HasKey(u => u.EventId);

            base.Property(p => p.Name)
                .IsRequired();

            base.Property(p => p.CardImageUrl)
                .IsRequired()
                .HasMaxLength(500);

            base.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}