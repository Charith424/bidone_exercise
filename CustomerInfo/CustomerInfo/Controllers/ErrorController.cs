using Microsoft.AspNetCore.Mvc;

namespace CustomerInfo.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult Index(int? statusCode)
        {
            if (statusCode.HasValue)
            {
                if (statusCode.Value == 404)
                {
                    return View("NotFound");
                }
            }
            return View("Error");
        }
    }
}
