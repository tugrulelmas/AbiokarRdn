using AbiokaRdn.ApplicationService.Abstractions;
using AbiokaRdn.ApplicationService.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AbiokaRdn.Host.Controllers
{
    [Route("api/Role")]
    public class RoleController : BaseCrudController<RoleDTO>
    {
        public RoleController(IRoleService service)
            : base(service) {
        }
    }
}
