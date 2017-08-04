using AbiokaRdn.ApplicationService.Authentication;
using AbiokaRdn.ApplicationService.Messaging;
using AbiokaRdn.Infrastructure.Common.Authentication;
using AbiokaRdn.Infrastructure.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AbiokaRdn.Host.Controllers
{
    [AllowAnonymous]
    [Route("api/Auth")]
    public class AuthController : BaseApiController
    {
        private readonly IEnumerable<IAuthService> authServices;

        public AuthController(IEnumerable<IAuthService> authServices) {
            this.authServices = authServices;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]AuthRequest request) {
            var authService = authServices.Where(a => a.Provider == request.provider).FirstOrDefault();
            if (authService == null)
                throw new DenialException($"Invalid provider: {request.provider}");

            var token = await authService.LoginAsync(request);

            return Ok(token);
        }

        [HttpPost("refreshToken")]
        public async Task<IActionResult> Login(string refreshToken, AuthProvider provider) {
            var authService = authServices.Where(a => a.Provider == provider).FirstOrDefault();
            if (authService == null)
                throw new DenialException($"Invalid provider: {provider}");

            var token = await authService.RefreshTokenAsync(refreshToken);

            return Ok(token);
        }
    }
}
