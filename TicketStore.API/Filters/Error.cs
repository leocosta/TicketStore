using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TicketStore.API.Filters
{
    [DataContract()]
    public class Error
    {
        [DataMember()]
        public string Source { get; set; }
        [DataMember()]
        public List<string> Message { get; set; }

        public Error() { }

        public Error(string source, List<string> message)
        {
            Source = source;
            Message = message;
        }
    }
}