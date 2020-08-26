using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
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
        private readonly TicketContext _ticketContext;

        public AccountController(
            AccountContext accountContext, 
            ApplicationContext applicationContext,
            TicketContext ticketContext
            )
        {
            this._accountContext = accountContext;
            this._applicationContext = applicationContext;
            this._ticketContext = ticketContext;
        }
        
        // GET
        public IActionResult Index(String username, String password, String appkey)
        {
            ViewData["appkey"] = appkey;
            Account selectedAccount = null;
            Application selectedApp = null;
            bool loginSuccess = false;
            HttpContext.Request.Cookies.TryGetValue("Login", out String loginUser);
            var accounts = from ac in this._accountContext.Account select ac;
            var applications = from app in this._applicationContext.Application select app;

            if (loginUser != null)
            {
                selectedAccount = accounts.Where(ac => ac.Username == loginUser).FirstOrDefault();
                ViewData["status"] = "Login Success";
                loginSuccess = true;
            }
            else if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                selectedAccount = accounts.Where(ac => ac.Username == username).FirstOrDefault();
                if (selectedAccount == null)
                {
                    ViewData["status"] = "No User";
                }
                else
                {
                    if (selectedAccount.Password == password)
                    {
                        ViewData["status"] = "Login Success";
                        HttpContext.Response.Cookies.Append("Login", selectedAccount.Username, new CookieOptions
                        {
                            Expires = DateTime.Now.AddMinutes(30)
                        });
                        loginSuccess = true;
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
                    System.Console.WriteLine("No Appkey, Go to Home Page");
                    return Redirect("/");
                }
                else
                {
                    selectedApp = applications.Where(app => app.Appkey == appkey).FirstOrDefault();
                    try
                    {
                        String curDate = DateTime.Now.Date.ToString();
                        byte[] dateArray = System.Text.Encoding.ASCII.GetBytes(curDate);
                        MD5 md5 = MD5.Create();
                        byte[] hash = md5.ComputeHash(dateArray);
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < hash.Length; i++)
                        {
                            sb.Append(hash[i].ToString("X"));
                        }
                        this._ticketContext.Add(new Ticket
                        {
                            Number = sb.ToString(),
                            Appkey = appkey,
                            Username = selectedAccount.Username
                        });
                        this._ticketContext.SaveChanges();
                        return Redirect(selectedApp.Url + "/Home/Login?ticket=" + sb.ToString());
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