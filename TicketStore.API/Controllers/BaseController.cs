using System.Web.Http;
using TicketStore.Domain.Users;

namespace TicketStore.API.Controllers
{
    public abstract class BaseController : ApiController
    {
        internal User UserSession
        {
            get
            {
                if (Request.Properties.ContainsKey("User"))
                    return Request.Properties["User"] as User;
                return null;
            }
            set
            {
                Request.Properties["User"] = value;
            }
        }
    }
}