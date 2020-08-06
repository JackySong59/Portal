using System;

namespace Portal.Models
{
    public class Account
    {
        public Account()
        {
            this.Id = 1;
            this.Username = "test";
            this.Password = "test";
            this.Name = "test";
        }
        
        public int Id { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String Name { get; set; }
    }
}