using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal.DB;

namespace Portal.Controllers
{
    public class ApplyAppController : Controller
    {
        private readonly DataContext _dataContext;

        public ApplyAppController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        
        // GET
        public IActionResult Index()
        {
            HttpContext.Request.Cookies.TryGetValue("Login", out String loginUser);
            var accounts = from ac in this._dataContext.Account select ac;
            if (loginUser != null)
            {
                var selectedAccount = accounts.Where(ac => ac.Username == loginUser).FirstOrDefault();
                var selectedApply = this._dataContext.ApplyForApp.AsQueryable()
                    .Include(afa => afa.Applyer)
                    .Include(afa => afa.App)
                    .ToList();
                return View(selectedApply);
            }
            return View();
        }
    }
}