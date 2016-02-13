using Akka.Actor;
using AutoMapper;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TicketStore.API.Filters;
using TicketStore.API.ViewModel;
using TicketStore.Domain.Orders;
using TicketStore.Service.Messages;

namespace TicketStore.API.Controllers
{
    public class OrdersController : ApiController
    {
        // POST api/orders
        [ModelValidate]
        public async Task<HttpResponseMessage> Post(OrderViewModel orderViewModel)
        {
            if (orderViewModel == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid request.");

            var paymentInfo = Mapper.Map<PaymentInfoViewModel, PaymentInfo>(orderViewModel.PaymentInfo);
            var placeOrder = new PlaceOrder(orderViewModel.Customer.UserId.Value, orderViewModel.Event.EventId.Value, orderViewModel.Quantity, paymentInfo);
            var orderPlaced = await WebApiApplication.OrderProcessorActor.Ask(placeOrder);
            if (orderPlaced is InvalidOperationException)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ((InvalidOperationException)orderPlaced).Message);

            var result = Mapper.Map<Order, OrderViewModel>(orderPlaced as Order);
            return Request.CreateResponse(HttpStatusCode.Created, result);
        }
    }
}
