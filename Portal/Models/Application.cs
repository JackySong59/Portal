using System;
using System.Collections.Generic;

namespace Portal.Models
{
    public class Application
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Appkey { get; set; }
        public String Url { get; set; }
        
        public List<AccountApplication> AccountApplications { get; set; }
        public List<ApplyForApp> ApplyForApps { get; set; }
    }
}