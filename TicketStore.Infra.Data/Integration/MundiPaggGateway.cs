using GatewayApiClient;
using GatewayApiClient.DataContracts;
using GatewayApiClient.DataContracts.EnumTypes;
using GatewayApiClient.Utility;
using System;
using System.Linq;
using System.Net;
using System.Text;
using TicketStore.Domain.Orders;
using TicketStore.Infra.CrossCutting.Logging;
using TicketStore.Infra.CrossCutting.Serialization;

namespace TicketStore.Infra.Data.Integration
{
    public class MundiPaggGateway : IPaymentService
    {
        private readonly GatewayServiceClient _serviceClient = new GatewayServiceClient();

        public PaymentResult CreateTransaction(PaymentInfo paymentInfo)
        {
            HttpResponse<CreateSaleResponse> httpResponse = null;
            try
            {
                var transaction = createCreditCardTransation(paymentInfo);
                Logger.Info($"GatewayServiceClient Request: {transaction.Serialize()}");
                httpResponse = _serviceClient.Sale.Create(transaction);
                Logger.Info($"GatewayServiceClient Response: {httpResponse.Serialize()}");
            }
            catch (Exception ex)
            {
                Logger.Error($"GatewayServiceClient Error: {ex}");
            }

            var createSaleResponse = httpResponse.Response;
            if (httpResponse.HttpStatusCode != HttpStatusCode.Created)
                throw new InvalidOperationException("Unable to process payment.", treatErrorReport(createSaleResponse.ErrorReport));

            var creditCardTransaction = createSaleResponse.CreditCardTransactionResultCollection.FirstOrDefault();
            return new PaymentResult(creditCardTransaction.TransactionReference, creditCardTransaction.CreditCard.InstantBuyKey);
        }

        private CreditCardTransaction createCreditCardTransation(PaymentInfo paymentInfo)
        {
            var creditCard = paymentInfo.InstantBuyKey != null
                ? new CreditCard { InstantBuyKey = paymentInfo.InstantBuyKey.Value }
                : new CreditCard
                {
                    CreditCardNumber = paymentInfo.CreditCardNumber,
                    CreditCardBrand = (CreditCardBrandEnum)paymentInfo.CreditCardBrand,
                    ExpMonth = paymentInfo.ExpMonth,
                    ExpYear = paymentInfo.ExpYear,
                    SecurityCode = paymentInfo.SecurityCode,
                    HolderName = paymentInfo.HolderName
                };

            return new CreditCardTransaction()
            {
                AmountInCents = paymentInfo.Amount,
                CreditCard = creditCard
            };
        }
        private Exception treatErrorReport(ErrorReport errorReport)
        {
            if (errorReport == null)
                return null;

            var errorMessages = new StringBuilder();
            foreach (var errorItem in errorReport.ErrorItemCollection)
                errorMessages.AppendFormat("Error {0}: {1}", errorItem.ErrorCode, errorItem.Description);

            return new Exception(errorMessages.ToString());
        }
    }
}
