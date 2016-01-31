using AutoMapper;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TicketStore.API.ViewModel;
using TicketStore.Domain.Events;
using TicketStore.Domain.Orders;
using TicketStore.Domain.Users;

namespace TicketStore.API.Controllers
{
    public class OrdersController : ApiController
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;

        public OrdersController(IOrderRepository orderRepository, IUserRepository userRepository, IEventRepository eventRepository)
        {
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _eventRepository = eventRepository;
        }

        // GET api/users
        public HttpResponseMessage Get()
        {
            var orders = _orderRepository.GetAll();
            var result = Mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(orders);
            return Request.CreateResponse(result);
        }

        // GET api/users/5
        public HttpResponseMessage Get(int id) { 
            var order = _orderRepository.Get(id);
            if (order == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Order not found.");

            var result = Mapper.Map<Order, OrderViewModel>(order);
            return Request.CreateResponse(result);
        }

        // POST api/users
        public HttpResponseMessage Post(OrderViewModel orderViewModel)
        {
            //var user = _userRepository.Get(id);
            //if (user == null)
            //    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not found.");

            //var result = Mapper.Map<User, UserViewModel>(user);
            //return Request.CreateResponse(result);
            return Request.CreateResponse();
        }
    }
}
