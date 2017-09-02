using Microsoft.EntityFrameworkCore;
using StackExchange.Core.Entities;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace StackExchange.Infrastructure.EF
{
    public class Context : DbContext 
    {
        public Context(DbContextOptions<Context> options) : base((DbContextOptions) options)
        {
        }

        
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Stack> Stacks { get; set; }
        public DbSet<StackPrice> StackPrices { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stack>()
                .HasOne(s=>s.Company)
                .WithMany(c=>c.Stacks)
                .HasForeignKey(s=>s.CompanyId);
            modelBuilder.Entity<Stack>()
                .HasOne(w=>w.Wallet)
                .WithMany(c => c.Stacks)
                .HasForeignKey(w=>w.WalletId);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Wallet)
                .WithOne(w=>w.User)
                .HasForeignKey<Wallet>(u=>u.UserId);
            modelBuilder.Entity<StackPrice>()
                .HasOne(s => s.Company)
                .WithMany(c => c.ActualStackPrice)
                .HasForeignKey(s => s.CompanyId);
            base.OnModelCreating(modelBuilder);
        }

    }
}
