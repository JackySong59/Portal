using Microsoft.EntityFrameworkCore;
using Portal.Models;

namespace Portal.DB
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }
        public DbSet<Application> Application { get; set; }
    }
}