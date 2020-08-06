using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Portal.Controllers
{
    public class ApiController : Controller
    {
        // GET
        public String Index()
        {
            return JsonConvert.SerializeObject(new {test = "test"});
        }

        public String Login(String username, String password)
        {
            return null;
        }
    }
}