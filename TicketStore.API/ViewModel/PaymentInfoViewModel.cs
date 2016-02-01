using System;
using System.Runtime.Serialization;

namespace TicketStore.API.ViewModel
{
    [DataContract]
    public class PaymentInfoViewModel
    {
        [DataMember]
        public int? CreditCardId { get; set; }
        [DataMember]
        public string CreditCardNumber { get; set; }
        [DataMember]
        public string CreditCardBrand { get; set; }
        [DataMember]
        public int ExpMonth { get; set; }
        [DataMember]
        public int ExpYear { get; set; }
        [DataMember]
        public string SecurityCode { get; set; }
        [DataMember]
        public string HolderName { get; set; }
        [DataMember]
        public bool SaveCreditCard { get; set; }
    }
}