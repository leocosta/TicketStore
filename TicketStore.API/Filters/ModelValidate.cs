using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TicketStore.API.Filters
{
    public class ModelValidate : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid) return;
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, new ApiHttpError(actionContext.ModelState));
        }
    }
}