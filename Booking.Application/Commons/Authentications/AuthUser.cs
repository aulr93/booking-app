using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Booking.Application.Commons.Authentications
{
    public class AuthUser
    {
        private readonly HttpContext _httpContext;

        public AuthUser()
        {
            _httpContext = null;
        }
        public AuthUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
            InitializeWithUserContext();
        }

        /// <summary>
        /// Initialize User Authenticated with User Claim Context from HttpContext Request
        /// </summary>
        private void InitializeWithUserContext()
        {
            if (_httpContext == null)
                return;

            // get user id
            var claimId = _httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (claimId != null)
                Id = Guid.Parse(claimId.Value);

            // get user fullname
            var claimName = _httpContext.User.FindFirst(ClaimTypes.Name);
            if (claimName != null)
                Name = claimName.Value;

            // get user username
            var claimUsername = _httpContext.User.FindFirst(ClaimTypes.GivenName);
            if (claimUsername != null)
                Username = claimUsername.Value;

            // get role
            var claimRole = _httpContext.User.FindFirst(ClaimTypes.Role);
            if (claimRole != null)
                Role = claimRole.Value;
        }

        public List<Claim> GetClaims()
        {
            List<Claim> claims = new List<Claim>();

            if (Id.HasValue)
                claims.Add(new Claim(type: ClaimTypes.NameIdentifier, value: Id.Value.ToString()));

            if (!string.IsNullOrWhiteSpace(Name))
                claims.Add(new Claim(type: ClaimTypes.Name, value: Name));

            if (!string.IsNullOrWhiteSpace(Username))
                claims.Add(new Claim(type: ClaimTypes.GivenName, value: Username));

            if (!string.IsNullOrWhiteSpace(Role))
                claims.Add(new Claim(type: ClaimTypes.Role, value: Role));

            return claims;
        }

        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
