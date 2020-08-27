using System;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Portal.DB;
using System.Linq;
using Portal.Models;

namespace Portal.Controllers
{
    public class ApiController : Controller
    {
        private readonly DataContext _dataContext;

        public ApiController(
            DataContext dataContext
        )
        {
            this._dataContext = dataContext;
        }
        
        // GET
        public String Index(String appkey, String ticket)
        {
            var tickets = from t in this._dataContext.Ticket select t;
            if (!String.IsNullOrEmpty(appkey) && !String.IsNullOrEmpty(ticket))
            {
                Ticket selectedTicket = tickets.Where(t => t.Number == ticket).FirstOrDefault();
                if (selectedTicket.Appkey == appkey)
                {
                    try
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            username = selectedTicket.Username
                        });
                    }
                    catch (NullReferenceException e)
                    {
                        System.Console.WriteLine(e.Message);
                    }
                }
            }

            return "No Ticket";
        }
        
        public String LoginVerification(String appkey, String ticket)
        {
            var tickets = from t in this._dataContext.Ticket select t;
            if (!String.IsNullOrEmpty(appkey) && !String.IsNullOrEmpty(ticket))
            {
                Ticket selectedTicket = tickets.Where(t => t.Number == ticket).FirstOrDefault();
                if (selectedTicket.Appkey == appkey)
                {
                    try
                    {
                        return JsonConvert.SerializeObject(new
                        {
                            username = selectedTicket.Username
                        });
                    }
                    catch (NullReferenceException e)
                    {
                        System.Console.WriteLine(e.Message);
                    }
                }
            }

            return "No Ticket";
        }
    }
}