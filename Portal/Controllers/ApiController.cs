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
        private readonly TicketContext _ticketContext;

        public ApiController(
            TicketContext ticketContext
        )
        {
            this._ticketContext = ticketContext;
        }
        
        // GET
        public String Index(String appkey, String ticket)
        {
            var tickets = from t in this._ticketContext.Ticket select t;
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
            var tickets = from t in this._ticketContext.Ticket select t;
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