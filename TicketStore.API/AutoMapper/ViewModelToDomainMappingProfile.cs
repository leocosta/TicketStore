using AutoMapper;
using TicketStore.API.ViewModel;
using TicketStore.Domain.Events;
using TicketStore.Domain.Orders;
using TicketStore.Domain.Users;

namespace TicketStore.API.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile() 
            : base("ViewModelToDomainMapping") { }

        protected override void Configure()
        {
            Mapper.CreateMap<UserViewModel, User>();
            Mapper.CreateMap<AddressViewModel, Address>();
            Mapper.CreateMap<EventViewModel, Event>();
            Mapper.CreateMap<OrderViewModel, Order>();
            Mapper.CreateMap<PaymentInfoViewModel, PaymentInfo>();
        }
    }
}