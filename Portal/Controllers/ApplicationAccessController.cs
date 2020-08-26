using Microsoft.AspNetCore.Mvc;

namespace Portal.Controllers
{
    public class ApplicationAccessController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}