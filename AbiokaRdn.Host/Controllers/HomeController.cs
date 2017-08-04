using Microsoft.AspNetCore.Mvc;

namespace AbiokaRdn.Host.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public IActionResult Index() => View();
    }
}