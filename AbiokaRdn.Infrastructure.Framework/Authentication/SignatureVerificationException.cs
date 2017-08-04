using AbiokaRdn.Infrastructure.Common.Exceptions;
using System.Net;

namespace AbiokaRdn.Infrastructure.Framework.Authentication
{

    public class SignatureVerificationException : DenialException
    {
        public SignatureVerificationException(string message)
            : base(HttpStatusCode.Unauthorized, message) {
        }
    }
}
