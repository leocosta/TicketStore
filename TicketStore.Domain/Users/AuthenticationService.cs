using System;
using System.Collections;
using System.Linq;
using TicketStore.Domain.Common;
using TicketStore.Infra.CrossCutting.Encryption.Extension;
using TicketStore.Infra.CrossCutting.Serialization;

namespace TicketStore.Domain.Users
{
    public class AuthenticationService
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;

        private readonly int _sessionTimeout = 60;
        private readonly string[] _publicResources = { "access.post", "users.post", "events.get" };

        #endregion

        #region Properties

        public virtual User User { get; set; }

        #endregion

        #region Constructors

        public AuthenticationService(IUnitOfWork unitOfWork, IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        #endregion

        #region Authenticate

        public virtual void Authenticate(string email, string password)
        {
            User = _userRepository.Get(email);
            
            if (User == null)
                throw new UnauthorizedException("Invalid User.");

            User.ValidateAccess(password);
            _unitOfWork.Commit();
        }

        #endregion

        #region ValidateAccess

        public virtual void ValidateAccess(string securityToken, string resource, string action, string method)
        {
            var isPublic = _publicResources.Contains(resource + (string.IsNullOrEmpty(action) ? "" : "." + action) + "." + method.ToLower()) || method == "OPTIONS";
            if (isPublic)
                return;

            if (securityToken == null)
                throw new ArgumentException("SecurityToken cannot be empty.");

            Hashtable sessionData;
            try
            {
                sessionData = DecryptSecurityToken(securityToken);
            }
            catch (FormatException)
            {
                throw new ArgumentException("Invalid SecurityToken.");
            }

            if ((DateTime)sessionData["Expires"] < DateTime.Now.ToUniversalTime())
                throw new UnauthorizedException("Session expired.");

            User = _userRepository.Get((int)sessionData["UserId"]);

            if (User == null)
                throw new UnauthorizedException("User not found.");
        }

        #endregion

        #region CreateToken

        public virtual string CreateToken()
        {
            if (User == null)
                return null;

            var sessionData = new Hashtable();

            sessionData["UserId"] = User.UserId;
            sessionData["Expires"] = DateTime.Now.AddMinutes(_sessionTimeout);

            string securityToken = EncryptSecurityToken(sessionData);

            return securityToken;
        }

        #endregion

        #region Criptography

        private static string EncryptSecurityToken(Hashtable securityData)
        {
            return securityData.Serialize().Encrypt();
        }

        private static Hashtable DecryptSecurityToken(string securityToken)
        {
            return securityToken.Decrypt().Deserialize<Hashtable>();
        }

        #endregion
    }
}