using Microsoft.EntityFrameworkCore;
using Portal.Models;

namespace Portal.DB
{
    public class AccountContext: DbContext
    {
        public AccountContext(DbContextOptions<AccountContext> options)
            : base(options)
        {
        }
        public DbSet<Account> Account { get; set; }
    }
}