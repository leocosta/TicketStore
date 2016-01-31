using GatewayApiClient;
using GatewayApiClient.DataContracts;
using GatewayApiClient.DataContracts.EnumTypes;
using System;
using System.Net;
using TicketStore.Domain.Orders;

namespace TicketStore.Infra.Data.Integration
{
    public class MundiPaggGateway : IPaymentGateway
    {
        public void CreateTransaction(PaymentInfo paymentInfo)
        {
            // Creates the credit card transaction.
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

            try
            {

                // Creates the client that will send the transaction.
                var serviceClient = new GatewayServiceClient();

                // Authorizes the credit card transaction and returns the gateway response.
                var httpResponse = serviceClient.Sale.Create(transaction);

                // API response code
                Console.WriteLine("Status: {0}", httpResponse.HttpStatusCode);

                var createSaleResponse = httpResponse.Response;
                if (httpResponse.HttpStatusCode == HttpStatusCode.Created)
                {
                    foreach (var creditCardTransaction in createSaleResponse.CreditCardTransactionResultCollection)
                    {
                        Console.WriteLine(creditCardTransaction.AcquirerMessage);
                    }
                }
                else
                {
                    if (createSaleResponse.ErrorReport != null)
                    {
                        foreach (ErrorItem errorItem in createSaleResponse.ErrorReport.ErrorItemCollection)
                        {
                            Console.WriteLine("Error {0}: {1}", errorItem.ErrorCode, errorItem.Description);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
