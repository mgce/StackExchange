using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Core.Entities;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace StackExchange.Infrastructure
{
    public class Context : DbContext 
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        public Microsoft.EntityFrameworkCore.DbSet<Company> Companies { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Wallet> Wallets { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Stack> Stacks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Stack>()
                .HasOne(s=>s.Company)
                .WithMany(c=>c.Stacks)
                .HasForeignKey(s=>s.CompanyId);
            modelBuilder.Entity<Stack>()
                .HasOne(s => s.User)
                .WithMany(c => c.Stacks)
                .HasForeignKey(s => s.UserId);
            modelBuilder.Entity<User>()
                .HasOne(u => u.Wallet)
                .WithOne(w=>w.User)
                .HasForeignKey<Wallet>(u=>u.UserId);
            base.OnModelCreating(modelBuilder);
        }

    }
}
