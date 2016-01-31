using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Http.ModelBinding;

namespace TicketStore.API.Filters
{
    [DataContract()]
    public class ApiHttpError
    {
        [DataMember()]
        public string Message { get; set; }
        [DataMember()]
        public List<Error> Errors { get; set; }

        public ApiHttpError() { }

        public ApiHttpError(ModelStateDictionary modelStateDictionary)
        {
            Message = "Invalid request.";
            Errors = new List<Error>();

            foreach (var item in modelStateDictionary.Where(m => m.Value.Errors.Count > 0))
            {
                var itemErrors = new List<string>();
                foreach (var childItem in item.Value.Errors)
                {
                    if (!string.IsNullOrEmpty(childItem.ErrorMessage))
                        itemErrors.Add(childItem.ErrorMessage);
                    else if (childItem.Exception != null)
                    {
                        itemErrors.Add(childItem.Exception.Message);
                        if (childItem.Exception.InnerException != null)
                            itemErrors.Add(childItem.Exception.InnerException.Message);
                    }
                    else
                        itemErrors.Add("Invalid parameter.");
                }
                Errors.Add(new Error(item.Key, itemErrors));
            }
        }
    }
}