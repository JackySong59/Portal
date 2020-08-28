using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Portal.DB;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Portal.Models;

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
            HttpContext.Request.Cookies.TryGetValue("Login", out String loginUser);
            if (loginUser != null)
            {
                var accounts = from ac in this._dataContext.Account select ac;
                var selectedAccount = accounts.Where(ac => ac.Username == loginUser).FirstOrDefault();
                var applications = this._dataContext.Account
                    .Where(acc => acc.Username == loginUser)
                    .Include(aa => aa.AccountApplications)
                    .ThenInclude(aa => aa.Application)
                    .FirstOrDefault();
                List<Application> apps = new List<Application>();
                foreach (var aa in applications.AccountApplications)
                {
                    apps.Add(aa.Application);
                }
                return View(apps);
            }

            return null;
        }
    }
}