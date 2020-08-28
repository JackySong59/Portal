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
                var allApplications = from app in this._dataContext.Application select app;
                var allApps = allApplications.ToList();
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
                return View(allApps);
            }

            return View();
        }

        public IActionResult ApplyForUse(int? id)
        {
            HttpContext.Request.Cookies.TryGetValue("Login", out String loginUser);

            var accounts = from ac in this._dataContext.Account select ac;
            var applications = from app in this._dataContext.Application select app;
            var selectedAccount = accounts.Where(ac => ac.Username == loginUser).FirstOrDefault();
            var selectedApplication = applications.Where(app => app.Id == id).FirstOrDefault();
            this._dataContext.Add(new ApplyForApp
            {
                App = selectedApplication,
                Appid = selectedApplication.Id,
                Applyer = selectedAccount,
                Applyerid = selectedAccount.Id,
                Processed = "No",
                Result = ""
            });
            this._dataContext.SaveChanges();

            return Redirect("/ApplicationAccess");
        }
    }
}