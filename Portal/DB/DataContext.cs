using Microsoft.EntityFrameworkCore;
using Portal.Models;

namespace Portal.DB
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options)
        {
        }
        
        public DbSet<Account> Account { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
    }
}