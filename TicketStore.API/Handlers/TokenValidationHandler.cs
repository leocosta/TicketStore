using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TicketStore.Domain.Users;

namespace TicketStore.API.Handlers
{
    public class TokenValidationHandler : DelegatingHandler
    {
        private readonly AuthenticationService _authenticationService;

        public TokenValidationHandler(AuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var resource = ((string)request.GetRouteData().Values["controller"]).ToLower();
            var action = request.GetRouteData().Values.ContainsKey("action") ? ((string)request.GetRouteData().Values["action"]).ToLower() : null;
            var method = request.Method.ToString().ToUpper();

            var securityToken = GetToken(request);//
            try
            {
                _authenticationService.ValidateAccess(securityToken, resource, action, method);
            }
            catch (UnauthorizedException ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.Unauthorized, ex.Message);
            }

            SaveContext(request, _authenticationService);

            var response = await base.SendAsync(request, cancellationToken);

            UpdateContext(request, _authenticationService);

            securityToken = _authenticationService.CreateToken();

            if (securityToken != null)
                AddTokenToHeader(response, securityToken);

            return response;
        }

        private static string GetToken(HttpRequestMessage request)
        {
            var querystring = request.RequestUri.ParseQueryString();

            if (querystring["securityToken"] != null)
                return querystring["securityToken"];

            if (request.Headers.Contains("SecurityToken"))
                return request.Headers.GetValues("SecurityToken").FirstOrDefault();

            return null;
        }

        private static void AddTokenToHeader(HttpResponseMessage response, string securityToken)
        {
            response.Headers.Add("SecurityToken", securityToken);
        }

        private static void SaveContext(HttpRequestMessage request, AuthenticationService authenticationService)
        {
            request.Properties.Add("User", authenticationService.User);
        }

        private static void UpdateContext(HttpRequestMessage request, AuthenticationService authenticationService)
        {
            if (request.Properties.ContainsKey("User"))
                authenticationService.User = request.Properties["User"] as User;
        }
    }

}