using System;
using Microsoft.AspNetCore.Mvc;
using Portal.DB;
using Portal.Models;
using System.Linq;

namespace Portal.Controllers
{
    public class BasicInfoController : Controller
    {
        private readonly DataContext _dataContext;

        public BasicInfoController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        
        // GET
        public IActionResult Index(String name, String telephone, String address, String email)
        {
            Account selectedAccount = null;
            HttpContext.Request.Cookies.TryGetValue("Login", out String loginUser);
            if (loginUser == null)
            {
                return Redirect("/Account");
            }

            var accounts = from ac in this._dataContext.Account select ac;

            if (loginUser != null)
            {
                selectedAccount = accounts.Where(ac => ac.Username == loginUser).FirstOrDefault();
                ViewData["name"] = selectedAccount.Name;
                ViewData["telephone"] = selectedAccount.Telephone;
                ViewData["address"] = selectedAccount.Address;
                ViewData["email"] = selectedAccount.Email;
            }
            
            return View();
        }
        
        public IActionResult HandleBasicInfoSubmit([Bind("Name,Telephone,Address,Email")] Account account)
        {
            Account selectedAccount = null;
            HttpContext.Request.Cookies.TryGetValue("Login", out String loginUser);

            var accounts = from ac in this._dataContext.Account select ac;
            if (account != null && loginUser != null)
            {
                selectedAccount = accounts.Where(ac => ac.Username == loginUser).FirstOrDefault();
                selectedAccount.Name = account.Name;
                selectedAccount.Telephone = account.Telephone;
                selectedAccount.Address = account.Address;
                selectedAccount.Email = account.Email;
                this._dataContext.SaveChanges();
            }

            return Redirect("/BasicInfo");
        }
    }
}