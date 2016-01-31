namespace TicketStore.Domain.Orders
{
    public class CreditCardTransation
    {
        private CreditCardTransation() {}

        public CreditCardTransation(string transactionReference)
        {
            TransactionReference = transactionReference;
        }

        public int CreditCardTransationId { get; private set; }
        public virtual Order Order { get; set; }
        public string TransactionReference { get; private set; }
    }
}
