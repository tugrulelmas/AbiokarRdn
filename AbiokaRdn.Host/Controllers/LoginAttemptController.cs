using AbiokaRdn.ApplicationService.Abstractions;
using AbiokaRdn.ApplicationService.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AbiokaRdn.Host.Controllers
{
    [Route("api/LoginAttempt")]
    public class LoginAttemptController : BaseReadController<LoginAttemptDTO>
    {
        public LoginAttemptController(IReadService<LoginAttemptDTO> service)
            : base(service) {
        }
    }
}
