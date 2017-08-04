using AbiokaRdn.ApplicationService.Abstractions;
using AbiokaRdn.ApplicationService.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AbiokaRdn.Host.Controllers
{
    [Route("api/Menu")]
    public class MenuController : BaseCrudController<MenuDTO>
    {
        private readonly IMenuService menuService;

        public MenuController(IMenuService menuService)
            : base(menuService) {
            this.menuService = menuService;
        }

        
        [HttpGet("filter")]
        public virtual IActionResult Get(string text) {
            var result = menuService.Filter(text);

            return Ok(result);
        }
    }
}
