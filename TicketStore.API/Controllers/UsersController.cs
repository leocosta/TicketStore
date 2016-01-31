using AutoMapper;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TicketStore.API.ViewModel;
using TicketStore.Domain.Users;

namespace TicketStore.API.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET api/users
        public HttpResponseMessage Get()
        {
            var users = _userRepository.GetAll();
            var result = Mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(users);
            return Request.CreateResponse(result);
        }

        // GET api/users/5
        public HttpResponseMessage Get(int id) { 
            var user = _userRepository.Get(id);
            if (user == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "User not found.");

            var result = Mapper.Map<User, UserViewModel>(user);
            return Request.CreateResponse(result);
        }

        // POST api/users
        public void Post(UserViewModel userViewModel)
        {
        }
    }
}
