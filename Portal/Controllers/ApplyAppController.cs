using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portal.DB;
using Portal.Models;

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
            if (loginUser == null)
            {
                return Redirect("/Account");
            }
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

        public IActionResult Accept(int? id)
        {
            if (id != null)
            {
                var selectedApply = this._dataContext.ApplyForApp.AsQueryable()
                    .Where(afa => afa.Id == id)
                    .Include(afa => afa.Applyer)
                    .Include(afa => afa.App)
                    .FirstOrDefault();
                this._dataContext.AccountApplication.Add(new AccountApplication
                {
                    Accid = selectedApply.Applyerid,
                    Account = selectedApply.Applyer,
                    Appid = selectedApply.Appid,
                    Application = selectedApply.App
                });
                selectedApply.Processed = "Yes";
                selectedApply.Result = "Accepted";
                this._dataContext.SaveChanges();
                return Redirect("/ApplyApp");
            }
            return Redirect("/ApplyApp");
        }

        public IActionResult Decline(int? id)
        {
            if (id != null)
            {
                var selectedApply = this._dataContext.ApplyForApp.AsQueryable()
                    .Where(afa => afa.Id == id)
                    .Include(afa => afa.Applyer)
                    .Include(afa => afa.App)
                    .FirstOrDefault();
                selectedApply.Processed = "Yes";
                selectedApply.Result = "Declined";
                this._dataContext.SaveChanges();
                return Redirect("/ApplyApp");
            }

            return Redirect("/ApplyApp");
        }
    }
}