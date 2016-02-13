using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TicketStore.API.ViewModel
{
    public class CredentialsViewModel : IValidatableObject
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
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
