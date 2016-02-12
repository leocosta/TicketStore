using Akka.Actor;
using Akka.DI.Core;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using TicketStore.API.AutoMapper;
using TicketStore.API.IoC;
using TicketStore.Service.Actors;

namespace TicketStore.API
{
    public class WebApiApplication : HttpApplication
    {
        public static ActorSystem ActorSystem;
        public static IActorRef OrderProcessorActor;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AutoMapperConfig.RegisterMappings();
            RegisterActorSystem();
        }

        protected void Application_End()
        {
            ActorSystem.Terminate().Wait(5000);
        }

        private static void RegisterActorSystem()
        {
            //Warning:
            //When the hosting inside of IIS the applicationpool the app lives in could be stopped and started at the whim of IIS.
            //This in turn means your ActorSystem could be stopped at any given time.
            //http://getakka.net/docs/deployment-scenarios/ASP%20NET
            ActorSystem = ActorSystem.Create("ticketStore");
            var resolver = new UnityDependencyResolver(UnityConfig.GetConfiguredContainer(), ActorSystem);
            OrderProcessorActor = ActorSystem.ActorOf(ActorSystem.DI().Props<OrderProcessorActor>(), "OrderProcessor");
        }
    }
}
