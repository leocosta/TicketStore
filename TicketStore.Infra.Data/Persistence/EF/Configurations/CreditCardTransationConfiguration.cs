using System.Data.Entity.ModelConfiguration;
using TicketStore.Domain.Orders;

namespace TicketStore.Infra.Data.Persistence.EF.Configurations
{
    public class CreditCardTransationConfiguration : EntityTypeConfiguration<CreditCardTransation>
    {
        public CreditCardTransationConfiguration()
        {
            base.HasKey(u => u.CreditCardTransationId);
        }
    }
}