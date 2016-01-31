using Microsoft.Practices.Unity;
using System.Web.Http;
using TicketStore.Domain.Common;
using TicketStore.Domain.Events;
using TicketStore.Domain.Orders;
using TicketStore.Domain.Users;
using TicketStore.Infra.Data.EF.Contexts;
using TicketStore.Infra.Data.Repositories;
using TicketStore.Infra.Data.Integration;

namespace TicketStore.API.IoC
{
    public static class UnityConfig
    {
        public static void ConfigureUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IUnitOfWork, UnitOfWork>();
            container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IEventRepository, EventRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IOrderRepository, OrderRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IPaymentGateway, MundiPaggGateway>(new HierarchicalLifetimeManager());
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}