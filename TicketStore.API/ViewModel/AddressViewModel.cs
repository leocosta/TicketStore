using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TicketStore.API.ViewModel
{
    [DataContract]
    public class AddressViewModel
    {
        [DataMember]
        public string Line1 { get; set; }
        [DataMember]
        public string Line2 { get; set; }
        [DataMember]
        public string State { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public string ZipCode { get; set; }
    }
}