using System;
using TicketStore.Domain.CreditCards;

namespace TicketStore.Domain.Orders
{
    public class PaymentResult
    {
        public PaymentResult(string transactionReference, Guid instantBuyKey)
        {
            TransactionReference = transactionReference;
            InstantBuyKey = instantBuyKey;
        }

        public string TransactionReference { get; set; }
        public Guid InstantBuyKey { get; set; }
    }
}