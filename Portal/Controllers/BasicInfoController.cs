using System;
using Microsoft.AspNetCore.Mvc;
using Portal.DB;
using Portal.Models;
using System.Linq;

namespace Portal.Controllers
{
    public class BasicInfoController : Controller
    {
        private readonly AccountContext _accountContext;

        public BasicInfoController(AccountContext accountContext)
        {
            this._accountContext = accountContext;
        }
        
        // GET
        public IActionResult Index(String name, String telephone, String address, String email)
        {
            Account selectedAccount = null;
            HttpContext.Request.Cookies.TryGetValue("Login", out String loginUser);

            var accounts = from ac in this._accountContext.Account select ac;

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
        
        public IActionResult HandleBasicInfoSubmit(String name, String telephone, String address, String email)
        {
            Account selectedAccount = null;
            HttpContext.Request.Cookies.TryGetValue("Login", out String loginUser);

            var accounts = from ac in this._accountContext.Account select ac;
            if (!String.IsNullOrEmpty(name) && 
                !String.IsNullOrEmpty(telephone) && 
                !String.IsNullOrEmpty(address) && 
                !String.IsNullOrEmpty(email) &&
                loginUser != null)
            {
                selectedAccount = accounts.Where(ac => ac.Username == loginUser).FirstOrDefault();
                selectedAccount.Name = name;
                selectedAccount.Telephone = telephone;
                selectedAccount.Address = address;
                selectedAccount.Email = email;
                this._accountContext.SaveChanges();
            }

            return Redirect("/BasicInfo");
        }
    }
}