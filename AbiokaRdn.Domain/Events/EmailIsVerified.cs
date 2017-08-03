using AbiokaRdn.Infrastructure.Common.Domain;
using System;

namespace AbiokaRdn.Domain.Events
{
    public class EmailIsVerified : IEvent
    {
        public EmailIsVerified(IIdEntity<Guid> user) {
            User = user;
        }

        public IIdEntity<Guid> User { get; }
    }
}
