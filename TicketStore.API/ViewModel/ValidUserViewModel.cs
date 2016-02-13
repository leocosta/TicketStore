using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TicketStore.API.ViewModel
{
    [DataContract]
    public class ValidUserViewModel : UserViewModel, IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Name))
                yield return new ValidationResult("Name is required.");

            if (string.IsNullOrWhiteSpace(SSN))
                yield return new ValidationResult("SSN is required.");

            if (string.IsNullOrWhiteSpace(Gender))
                yield return new ValidationResult("Gender is required.");

            if (Address == null)
                yield return new ValidationResult("Address is required.");

            if (!Birthdate.HasValue)
                yield return new ValidationResult("Birthdate is required.");

            if (string.IsNullOrWhiteSpace(Email))
                yield return new ValidationResult("Email is required.");
            
            var emailValidator = new EmailAddressAttribute();
            if (!emailValidator.IsValid(Email))
                yield return new ValidationResult("Invalid email.");

            if (string.IsNullOrWhiteSpace(Password))
                yield return new ValidationResult("Password is required.");
        }
    }
}