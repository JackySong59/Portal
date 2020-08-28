using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Portal.Models
{
    public class Account
    {
        public int Id { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
        public String Telephone { get; set; }
        public String Address { get; set; }
        public String Email { get; set; }
        
        public List<AccountApplication> AccountApplications { get; set; }
        public List<ApplyForApp> ApplyForApps { get; set; }
    }
}