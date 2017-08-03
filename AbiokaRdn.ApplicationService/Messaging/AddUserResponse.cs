using AbiokaRdn.ApplicationService.DTOs;
using System;
using System.Collections.Generic;

namespace AbiokaRdn.ApplicationService.Messaging
{
    public class AddUserResponse : ServiceResponseBase
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public IEnumerable<RoleDTO> Roles { get; set; }
    }
}
