using System;

namespace Portal.Models
{
    public class ApplyForApp
    {
        public int Id { get; set; }
        public Account Applyer { get; set; }
        public int Applyerid { get; set; }
        public Application App { get; set; }
        public int Appid { get; set; }
        public String Processed { get; set; }
        public String Result { get; set; }
    }
}