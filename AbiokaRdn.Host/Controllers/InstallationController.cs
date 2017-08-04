using AbiokaRdn.ApplicationService.Abstractions;
using AbiokaRdn.ApplicationService.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AbiokaRdn.Host.Controllers
{
    [AllowAnonymous]
    [Route("api/Installation")]
    public class InstallationController : BaseApiController
    {
        private readonly IInstallationService installationService;

        public InstallationController(IInstallationService installationService) {
            this.installationService = installationService;
        }
        
        [HttpGet("Required")]
        public IActionResult Required() {
            var result = installationService.IsInstallationRequired();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create([FromBody]CreateApplicationDataRequest createApplicationDataRequest) {
            installationService.CreateApplicationData(createApplicationDataRequest);

            return Ok();
        }
    }
}
