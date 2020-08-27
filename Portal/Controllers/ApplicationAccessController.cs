using Microsoft.AspNetCore.Mvc;
using Portal.DB;
using System.Linq;

namespace Portal.Controllers
{
    public class ApplicationAccessController : Controller
    {
        private readonly DataContext _dataContext;

        public ApplicationAccessController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        
        // GET
        public IActionResult Index()
        {
            var applications = from app in this._dataContext.Application select app;
            return View(applications);
        }
    }
}