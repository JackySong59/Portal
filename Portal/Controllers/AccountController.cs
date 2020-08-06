using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Portal.DB;
using Portal.Models;

namespace Portal.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountContext _accountContext;

        public AccountController(AccountContext accountContext)
        {
            this._accountContext = accountContext;
        }
        
        // GET
        public IActionResult Index(String username, String password)
        {
            var accounts = from ac in _accountContext.Account select ac;
            
            if (!String.IsNullOrEmpty(username) && !String.IsNullOrEmpty(password))
            {
                Account selectedAccount = accounts.Where(ac => ac.Username == username).FirstOrDefault();
                if (selectedAccount == null)
                {
                    System.Console.WriteLine("No User");
                }
                else
                {
                    if (selectedAccount.Password == password)
                    {
                        System.Console.WriteLine("Login Success");
                        return Redirect("http://baidu.com"); // TODO: Change URL here
                    }
                    else
                    {
                        System.Console.WriteLine("Wrong Username or Password");
                    }
                }
                System.Console.WriteLine(username);
                System.Console.WriteLine(password);
            }
            return View();
        }
    }
}