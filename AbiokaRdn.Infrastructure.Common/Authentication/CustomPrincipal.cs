using System;
using System.Linq;
using System.Security.Principal;

namespace AbiokaRdn.Infrastructure.Common.Authentication
{
    public class CustomPrincipal : ICustomPrincipal
    {
        public CustomPrincipal(string userName) {
            Identity = new CustomIdentity(userName);
        }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Language { get; set; }

        public string Token { get; set; }

        public DateTime TokenExpirationDate { get; set; }

        public bool IsInRole(string role) => Roles.Where(r => r == role).Any();

        public string[] Roles { get; set; }

        public Guid Id { get; set; }

        public IIdentity Identity { get; }
    }
}
