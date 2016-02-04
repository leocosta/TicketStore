using AutoMapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TicketStore.API.Filters;
using TicketStore.API.ViewModel;
using TicketStore.Domain.Common;
using TicketStore.Domain.Users;

namespace TicketStore.API.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        public UsersController(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
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
        [ModelValidate]
        public HttpResponseMessage Post(UserViewModel userViewModel)
        {
            if (userViewModel == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid User.");

            var user = Mapper.Map<UserViewModel, User>(userViewModel);
            //TODO: User validation

            try
            {
                user.Validate(_userRepository);
                user.SetPassword(userViewModel.Password);
                _userRepository.Add(user);
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                return Request.CreateResponse((HttpStatusCode)422, string.Format("Failed to save the user: {0}", ex.Message));
            }
       
            var result = Mapper.Map<User, UserViewModel>(user);
            return Request.CreateResponse(result);
        }

        // OPTIONS api/users
        public HttpResponseMessage Options()
        {
            var response = new HttpResponseMessage();
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, OPTIONS");

            return response;
        }
    }
}
