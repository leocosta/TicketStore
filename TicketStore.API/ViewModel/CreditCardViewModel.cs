using System.Runtime.Serialization;

namespace TicketStore.API.ViewModel
{
    [DataContract]
    public class CreditCardViewModel
    {
        [DataMember]
        public int? CreditCardId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public OrderViewModel Owner { get; set; }
    }
}