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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountApplication>().HasKey(k => new
            {
                k.Accid,
                k.Appid
            });

            modelBuilder.Entity<AccountApplication>()
                .HasOne(aa => aa.Application)
                .WithMany(aa => aa.AccountApplications)
                .HasForeignKey(aa => aa.Appid);

            modelBuilder.Entity<AccountApplication>()
                .HasOne(aa => aa.Account)
                .WithMany(aa => aa.AccountApplications)
                .HasForeignKey(aa => aa.Accid);

            modelBuilder.Entity<ApplyForApp>()
                .HasOne(afa => afa.App)
                .WithMany(app => app.ApplyForApps)
                .HasForeignKey(afa => afa.Appid);

            modelBuilder.Entity<ApplyForApp>()
                .HasOne(afa => afa.Applyer)
                .WithMany(a => a.ApplyForApps)
                .HasForeignKey(afa => afa.Applyerid);
        }

        public DbSet<Account> Account { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<AccountApplication> AccountApplication { get; set; }
        public DbSet<ApplyForApp> ApplyForApp { get; set; }
    }
}