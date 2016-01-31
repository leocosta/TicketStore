using System.Runtime.Serialization;

namespace TicketStore.API.ViewModel
{
    [DataContract]
    public class EventViewModel
    {
        [DataMember]
        public int? EventId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public AddressViewModel Location { get; set; }
        [DataMember]
        public decimal Price { get; set; }
        [DataMember]
        public decimal StartDate { get; set; }
        [DataMember]
        public decimal EndDate { get; set; }
        [DataMember]
        public string CardImageUrl { get; set; }
    }
}