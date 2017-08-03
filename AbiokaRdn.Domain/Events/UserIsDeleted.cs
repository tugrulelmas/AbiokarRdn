using AbiokaRdn.Infrastructure.Common.Domain;
using System;

namespace AbiokaRdn.Domain.Events
{
    public class UserIsDeleted : IEvent
    {
        public UserIsDeleted(Guid userId, string email) {
            UserId = userId;
            Email = email;
        }

        public Guid UserId { get; }

        public string Email { get; }
    }
}
