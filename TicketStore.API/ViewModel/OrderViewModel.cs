using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TicketStore.API.ViewModel
{
    [DataContract]
    public class OrderViewModel: IValidatableObject
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
        [DataMember]
        public PaymentInfoViewModel PaymentInfo { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Customer == null)
                yield return new ValidationResult("Customer is required.");

            if (Event == null)
                yield return new ValidationResult("Event is required.");

            if (PaymentInfo == null)
                yield return new ValidationResult("PaymentInfo is required.");

            if (Quantity < 1)
                yield return new ValidationResult("Quantity should be greater than zero.");
        }
    }
}