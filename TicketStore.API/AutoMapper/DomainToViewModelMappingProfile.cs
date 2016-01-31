using AutoMapper;
using TicketStore.API.ViewModel;
using TicketStore.Domain.Events;
using TicketStore.Domain.Orders;
using TicketStore.Domain.Users;

namespace TicketStore.API.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile() 
            : base("DomainToViewModelMapping") {}

        protected override void Configure()
        {
            Mapper.CreateMap<User, UserViewModel>()
                .ForMember(i => i.Password, opt => opt.Ignore());
            Mapper.CreateMap<Address, AddressViewModel>();
            Mapper.CreateMap<Event, EventViewModel>();
            Mapper.CreateMap<Order, OrderViewModel>();
        }
    }
}