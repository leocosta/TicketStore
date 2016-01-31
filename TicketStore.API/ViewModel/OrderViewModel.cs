using System.Runtime.Serialization;

namespace TicketStore.API.ViewModel
{
    [DataContract]
    public class OrderViewModel
    {
        [DataMember]
        public int? OrderId { get; set; }
        [DataMember]
        public UserViewModel Customer { get; set; }
        [DataMember]
        public EventViewModel Event { get; set; }
        [DataMember]
        public string Status { get; set; }
        [DataMember]
        public int Quantity { get; set; }
    }
}