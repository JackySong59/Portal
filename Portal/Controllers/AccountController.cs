using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portal.DB;
using Portal.Models;

namespace Portal.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountContext _accountContext;
        private readonly ApplicationContext _applicationContext;

        public AccountController(
            AccountContext accountContext, 
            ApplicationContext applicationContext
            )
        {
            this._accountContext = accountContext;
            this._applicationContext = applicationContext;
        }
        
        // GET
        public IActionResult Index(String username, String password, String appkey)
        {
            ViewData["appkey"] = appkey;
            bool loginSuccess = false;
            var accounts = from ac in this._accountContext.Account select ac;
            var applications = from app in this._applicationContext.Application select app;
            
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                Account selectedAccount = accounts.Where(ac => ac.Username == username).FirstOrDefault();
                if (selectedAccount == null)
                {
                    ViewData["status"] = "No User";
                }
                else
                {
                    if (selectedAccount.Password == password)
                    {
                        ViewData["status"] = "Login Success";
                        loginSuccess = true; // TODO: Change URL here
                    }
                    else
                    {
                        ViewData["status"] = "Wrong Username or Password";
                    }
                }
            }
            else
            {
                ViewData["status"] = null;
            }

            if (loginSuccess == true)
            {
                if (appkey == null)
                {
                    System.Console.WriteLine("No Appkey");
                }
                else
                {
                    Application selectedApp = applications.Where(app => app.Appkey == appkey).FirstOrDefault();
                    try
                    {
                        return Redirect(selectedApp.Url);
                    }
                    catch (NullReferenceException e)
                    {
                        System.Console.WriteLine(e.Message);
                    }
                }

                return View();
            }
            else
            {
                return View();
            }
        }
    }
}