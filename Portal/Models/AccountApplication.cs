namespace Portal.Models
{
    public class AccountApplication
    {
        public int Accid { get; set; }
        public int Appid { get; set; }
        
        public Account Account { get; set; }
        public Application Application { get; set; }
    }
}