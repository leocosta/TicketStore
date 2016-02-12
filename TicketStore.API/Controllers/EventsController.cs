using AutoMapper;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TicketStore.API.ViewModel;
using TicketStore.Domain.Events;

namespace TicketStore.API.Controllers
{
    public class EventsController : ApiController
    {
        private readonly IEventRepository _eventRepository;

        public EventsController(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        // GET api/events
        public HttpResponseMessage Get()
        {
            var events = _eventRepository.GetAll();
            var result = Mapper.Map<IEnumerable<Event>, IEnumerable<EventViewModel>>(events);
            return Request.CreateResponse(result);
        }

        // GET api/events/5
        public HttpResponseMessage Get(int id) { 
            var @event = _eventRepository.Get(id);
            if (@event == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Event not found.");

            var result = Mapper.Map<Event, EventViewModel>(@event);
            return Request.CreateResponse(result);
        }
    }
}
