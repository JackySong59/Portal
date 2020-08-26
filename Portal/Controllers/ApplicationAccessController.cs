using Microsoft.AspNetCore.Mvc;
using Portal.DB;
using System.Linq;

namespace Portal.Controllers
{
    public class ApplicationAccessController : Controller
    {
        private readonly ApplicationContext _applicationContext;

        public ApplicationAccessController(ApplicationContext applicationContext)
        {
            this._applicationContext = applicationContext;
        }
        
        // GET
        public IActionResult Index()
        {
            var applications = from app in this._applicationContext.Application select app;
            return View(applications);
        }
    }
}