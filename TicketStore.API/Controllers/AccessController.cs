using System.Net;
using System.Net.Http;
using System.Web.Http;
using TicketStore.API.ViewModel;
using TicketStore.Domain.Users;

namespace TicketStore.API.Controllers
{
    public class AccessController : BaseController
    {
        private readonly AuthenticationService _autenticacaoService;

        public AccessController(AuthenticationService autenticacaoService)
        {
            _autenticacaoService = autenticacaoService;
        }
        
        // POST api/access
        [HttpPost]
        public HttpResponseMessage Authenticate(CredentialsViewModel credentials)
        {
            if (credentials == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Incorrect Credentials.");

            try
            {
                _autenticacaoService.Authenticate(credentials.Email, credentials.Password);
            }
            catch (UnauthorizedException ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message);
            }

            UserSession = _autenticacaoService.User;
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("UserId", UserSession.UserId.ToString());

            return response;
        }
    }
}