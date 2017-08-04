using AbiokaRdn.Infrastructure.Framework.RestHelper.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AbiokaRdn.Host.Controllers
{
    [CustomActionFilter]
    [CustomExceptionFilter]
    public class BaseApiController : Controller
    {
    }
}