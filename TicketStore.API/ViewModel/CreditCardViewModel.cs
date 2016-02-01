using System;
using System.Runtime.Serialization;

namespace TicketStore.API.ViewModel
{
    [DataContract]
    public class CreditCardViewModel
    {
        [DataMember]
        public int? CreditCardId { get; set; }
        [DataMember]
        public Guid? InstantBuyKey { get; set; }
        [DataMember]
        public string Brand { get; set; }
        [DataMember]
        public string LastFourDigits { get; set; }
        [DataMember]
        public int ExpMonth { get; set; }
        [DataMember]
        public int ExpYear { get; set; }
    }
}