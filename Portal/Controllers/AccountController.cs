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
        public IActionResult Index()
        {
            this._accountContext.Account.Add(new Account
            {
                Username = "test",
                Password = "test",
                Name = "name"
            });
            this._accountContext.SaveChanges();
            return View();
        }
    }
}