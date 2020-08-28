using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Portal.DB;
using Portal.Models;

namespace Portal.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;

        public AccountController(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }
        
        // GET
        public IActionResult Index([Bind("Username,Password,Appkey")] Login login)
        {
            ViewData["appkey"] = login.Appkey;
            Account selectedAccount = null;
            Application selectedApp = null;
            bool loginSuccess = false;
            ViewData["login"] = false;
            HttpContext.Request.Cookies.TryGetValue("Login", out String loginUser);
            var accounts = from ac in this._dataContext.Account select ac;
            var applications = from app in this._dataContext.Application select app;

            if (loginUser != null)
            {
                selectedAccount = accounts.Where(ac => ac.Username == loginUser).FirstOrDefault();
                ViewData["status"] = "Login Success";
                loginSuccess = true;
                ViewData["login"] = true;
            }
            else if (!String.IsNullOrEmpty(login.Username) && !String.IsNullOrEmpty(login.Password))
            {
                selectedAccount = accounts.Where(ac => ac.Username == login.Username).FirstOrDefault();
                if (selectedAccount == null)
                {
                    ViewData["status"] = "No User";
                }
                else
                {
                    if (selectedAccount.Password == login.Password)
                    {
                        ViewData["status"] = "Login Success";
                        HttpContext.Response.Cookies.Append("Login", selectedAccount.Username, new CookieOptions
                        {
                            Expires = DateTime.Now.AddMinutes(30)
                        });
                        loginSuccess = true;
                        ViewData["login"] = true;
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
                if (login.Appkey == null)
                {
                    System.Console.WriteLine("No Appkey, Go to Home Page");
                    return Redirect("/");
                }
                else
                {
                    selectedApp = applications.Where(app => app.Appkey == login.Appkey).FirstOrDefault();
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
                        this._dataContext.Add(new Ticket
                        {
                            Number = sb.ToString(),
                            Appkey = login.Appkey,
                            Username = selectedAccount.Username
                        });
                        this._dataContext.SaveChanges();
                        var a = this._dataContext.Account
                            .Where(acc => acc.Username == selectedAccount.Username)
                            .Include(aa => aa.AccountApplications)
                            .ThenInclude(aa => aa.Application)
                            .FirstOrDefault();
                        List<String> appKeys = new List<String>();
                        foreach (var aa in a.AccountApplications)
                        {
                            appKeys.Add(aa.Application.Appkey);
                        }

                        if (appKeys.Contains(selectedApp.Appkey))
                        {
                            return Redirect(selectedApp.Url + "/Home/Login?ticket=" + sb.ToString());
                        }
                        else
                        {
                            ViewData["status"] =
                                "You have no access to this application, please contact the administrator";
                        }
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

        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("Login");
            return Redirect("/");
        }
    }
}