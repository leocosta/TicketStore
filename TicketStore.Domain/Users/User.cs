using System;
using System.Collections.Generic;
using TicketStore.Infra.CrossCutting.Encryption.Extension;

namespace TicketStore.Domain.Users
{
    public class User
    {
        public User()
        {
            CreateDate = DateTime.Now;
            CreditCards = new List<CreditCard>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string SSN { get; set; }
        public Gender Gender { get; set; }
        public virtual Address Address { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
        public string Password { get; private set; }
        public virtual ICollection<CreditCard> CreditCards { get; set; }
        public int AuthAttempt { get; private set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ModifyDate { get; set; }

        internal void AddCreditCard(CreditCard creditCard)
        {
            this.CreditCards.Add(creditCard);
        }
        internal virtual void ValidateAccess(string password)
        {
            ValidatePassword(password);
        }
        private void ValidatePassword(string password)
        {
            const int attemptsLimit = 6;

            if (Password.Equals(password.Encrypt()))
                AuthAttempt = 0;
            else
            {
                string message = string.Empty;
                AuthAttempt++;

                if (AuthAttempt > attemptsLimit)
                {
                    AuthAttempt = 0;
                    message = "Password attempts exceeded. Access suspended.";
                }
                else
                    message = "User not authorized.";

                throw new UnauthorizedException("User not authorized.");
            }
        }

        public void Validate(IUserRepository _userRepository)
        {
            if (_userRepository.Any(a => a.UserId != this.UserId && a.Email.Equals(this.Email)))
                throw new InvalidOperationException("Email account is already registered.");
        }

        public void SetPassword(string password)
        {
            this.Password = password.Encrypt();
        }
    }
}
