using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketStore.Infra.CrossCutting
{
    public class AUnityConfig
    {
        public static UnityContainer RegisterAll()
        {
            var container = new UnityContainer();
            //container.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());
            //container.RegisterType<ILocalService, LocalService>(new HierarchicalLifetimeManager());
            //container.RegisterType<ITypeService, TypeService>(new HierarchicalLifetimeManager());
            //container.RegisterType<IItemService, ItemService>(new HierarchicalLifetimeManager());
            //container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
            //container.RegisterType<ILocalRepository, LocalRepository>(new HierarchicalLifetimeManager());
            //container.RegisterType<ITypeRepository, TypeRepository>(new HierarchicalLifetimeManager());
            //container.RegisterType<IItemRepository, ItemRepository>(new HierarchicalLifetimeManager());
            return container;
        }

    }
}
