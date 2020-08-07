using Microsoft.EntityFrameworkCore;
using Portal.Models;

namespace Portal.DB
{
    public class TicketContext: DbContext
    {
        public TicketContext(DbContextOptions<TicketContext> options)
            : base(options)
        {
        }
        
        public DbSet<Ticket> Ticket { get; set; }
    }
}