using GatewayApiClient;
using GatewayApiClient.DataContracts;
using GatewayApiClient.DataContracts.EnumTypes;
using System;
using System.Linq;
using System.Net;
using System.Text;
using TicketStore.Domain.Orders;

namespace TicketStore.Infra.Data.Integration
{
    public class MundiPaggGateway : IPaymentService
    {
        public PaymentResult CreateTransaction(PaymentInfo paymentInfo)
        {
            var transaction = new CreditCardTransaction()
            {
                AmountInCents = paymentInfo.Amount,
                CreditCard = new CreditCard()
                {
                    CreditCardNumber = paymentInfo.CreditCardNumber,
                    CreditCardBrand = (CreditCardBrandEnum)paymentInfo.CreditCardBrand,
                    ExpMonth = paymentInfo.ExpMonth,
                    ExpYear = paymentInfo.ExpYear,
                    SecurityCode = paymentInfo.SecurityCode,
                    HolderName = paymentInfo.HolderName
                }
            };

            var serviceClient = new GatewayServiceClient();
            var httpResponse = serviceClient.Sale.Create(transaction);

            var createSaleResponse = httpResponse.Response;
            if (httpResponse.HttpStatusCode != HttpStatusCode.Created)
                throw new InvalidOperationException("Payment failed.", treatInnerException(createSaleResponse));

            var creditCardTransaction = createSaleResponse.CreditCardTransactionResultCollection.FirstOrDefault();
            return new PaymentResult(creditCardTransaction.TransactionReference, creditCardTransaction.CreditCard.InstantBuyKey);
        }

        private Exception treatInnerException(CreateSaleResponse createSaleResponse)
        {
            if (createSaleResponse.ErrorReport == null)
                return null;

            var errorMessages = new StringBuilder();
            foreach (ErrorItem errorItem in createSaleResponse.ErrorReport.ErrorItemCollection)
                errorMessages.AppendFormat("Error {0}: {1}", errorItem.ErrorCode, errorItem.Description);

            return new Exception(errorMessages.ToString());
        }
    }
}
