﻿using AbiokaRdn.Infrastructure.Common.Authentication;
using AbiokaRdn.Infrastructure.Common.Exceptions;
using AbiokaRdn.Infrastructure.Common.Helper;
using AbiokaRdn.Infrastructure.Common.IoC;
using System.Linq;
using System.Reflection;

namespace AbiokaRdn.ApplicationService.Interceptors
{
    internal class RoleValidationInterceptor : IServiceInterceptor
    {
        private readonly ICurrentContext currentContext;

        public RoleValidationInterceptor(ICurrentContext currentContext) {
            this.currentContext = currentContext;
        }

        public int Order => 0;

        public void BeforeProceed(IInvocationContext context) {
            var attributes = context.MethodInvocationTarget.GetCustomAttributes(typeof(AllowedRole), true);

            if (attributes.IsNullOrEmpty()) {
                attributes = context.Method.GetCustomAttributes(typeof(AllowedRole), true);
                if (attributes.IsNullOrEmpty())
                    return;
            }

            if(currentContext.Current.Principal == null || currentContext.Current.Principal.Roles == null)
                throw new DenialException("AccessDenied");


            var allowedRoles = (AllowedRole)attributes.First();
            if (currentContext.Current.Principal.Roles.Any(r => allowedRoles.Roles.Contains(r)))
                return;

            throw new DenialException("AccessDenied");
        }
    }
}
