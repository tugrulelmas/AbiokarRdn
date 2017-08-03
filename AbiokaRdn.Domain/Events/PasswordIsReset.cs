using AbiokaRdn.Infrastructure.Common.Domain;
using System;

namespace AbiokaRdn.Domain.Events
{
    public class PasswordIsReset : IEvent
    {
        public PasswordIsReset(IIdEntity<Guid> user) {
            User = user;
        }

        public IIdEntity<Guid> User { get; }
    }
}
