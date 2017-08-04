using AbiokaRdn.Infrastructure.Common.Authentication;
using AbiokaRdn.Infrastructure.Common.Dynamic;
using AbiokaRdn.Infrastructure.Common.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace AbiokaRdn.Infrastructure.Framework.Authentication
{
    public class AuthenticationHandler : IDynamicHandler
    {
        private readonly IAbiokaToken abiokaToken;
        private readonly ICurrentContext currentContext;

        public AuthenticationHandler(IAbiokaToken abiokaToken, ICurrentContext currentContext) {
            this.abiokaToken = abiokaToken;
            this.currentContext = currentContext;
        }

        public short Order => 0;

        public void AfterSend(IResponseContext responseContext) {
        }

        public void BeforeSend(IRequestContext requestContext) {
            var context = requestContext.Request as ActionExecutingContext;
            SetContextActionType(context.HttpContext.Request);

            currentContext.IP = context.HttpContext.Connection.RemoteIpAddress.ToString();

            var isAllowAnonymous = context.Filters.Any(f => f.GetType() == typeof(AllowAnonymousFilter));

            var auth = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            var token = string.Empty;
            if (!string.IsNullOrEmpty(auth)) {
                var parameters = auth.Split(' ');
                if (parameters.Length != 2 || string.IsNullOrWhiteSpace(parameters[1])) {
                    if (isAllowAnonymous) {
                        currentContext.Principal = null;
                        return;
                    }
                    throw AuthenticationException.MissingCredential;
                }

                if (parameters[0] != "Bearer") {
                    if (isAllowAnonymous) {
                        currentContext.Principal = null;
                        return;
                    }

                    throw AuthenticationException.InvalidCredential;
                }
                token = parameters[1];
            } else if (!isAllowAnonymous) {
                currentContext.Principal = null;
                throw AuthenticationException.MissingCredential;
            }

            try {
                var payload = abiokaToken.Decode(token);

                var user = new CustomPrincipal(payload.id.ToString()) {
                    Token = token,
                    UserName = payload.id.ToString(),
                    Email = payload.email,
                    Id = payload.id,
                    TokenExpirationDate = Util.UnixTimeStampToDateTime(payload.exp),
                    Roles = payload.roles,
                    Language = payload.language
                };
                currentContext.Principal = user;
            } catch {
                if (!isAllowAnonymous)
                    throw;
            }
        }

        public void OnException(IExceptionContext exceptionContext) {
        }

        private void SetContextActionType(HttpRequest request) {
            if (request.Method == "DELETE") {
                currentContext.ActionType = ActionType.Delete;
            }
            else if (request.Method == "PUT") {
                currentContext.ActionType = ActionType.Update;
            }
            else if (request.Method == "POST") {
                currentContext.ActionType = ActionType.Add;
            }
            else {
                currentContext.ActionType = ActionType.List;
            }
        }
    }
}
