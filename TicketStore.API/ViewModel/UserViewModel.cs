using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TicketStore.API.ViewModel
{
    [DataContract]
    public class UserViewModel
    {
        [DataMember]
        public int? UserId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string SSN { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public AddressViewModel Address { get; set; }
        [DataMember]
        public DateTime Birthdate { get; set; }
        [DataMember()]
        public string Email { get; set; }
        [DataMember]
        public string Password { get; set; }
        public List<CreditCardViewModel> CreditCards { get; set; }
    }
}