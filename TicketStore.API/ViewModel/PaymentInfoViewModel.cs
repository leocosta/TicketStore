using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TicketStore.API.ViewModel
{
    [DataContract]
    public class PaymentInfoViewModel : IValidatableObject
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!CreditCardId.HasValue && string.IsNullOrEmpty(CreditCardNumber))
                yield return new ValidationResult("CreditCardNumber is required.");

            if (!CreditCardId.HasValue && string.IsNullOrEmpty(CreditCardBrand))
                yield return new ValidationResult("CreditCardBrand is required.");

            if (!CreditCardId.HasValue && ExpMonth < 1 || ExpMonth > 12)
                yield return new ValidationResult("The ExpMonth should be between 1 and 12.");

            if (!CreditCardId.HasValue && ExpYear < DateTime.Today.Year)
                yield return new ValidationResult(string.Format("The ExpYear should be greater than or equal to {0}.", DateTime.Today.Year));

            if (!CreditCardId.HasValue && string.IsNullOrEmpty(SecurityCode))
                yield return new ValidationResult("SecurityCode is required.");

            if (!CreditCardId.HasValue && string.IsNullOrEmpty(HolderName))
                yield return new ValidationResult("SecurityCode is required.");
        }
    }
}