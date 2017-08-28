using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using StackExchange.Core.Entities;

namespace StackExchange.Infrastructure
{
    public class Context : DbContext
    {
        public Context() : base()
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Stack> Stacks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasMany(c => c.Stacks).WithOptional();
            modelBuilder.Entity<User>()
                .HasMany(u => u.Stacks)
                .WithOptional();
            modelBuilder.Entity<Wallet>()
                .HasKey(w => w.UserId);
            modelBuilder.Entity<User>()
                .HasOptional(u => u.Wallet)
                .WithRequired(w => w.User);
            base.OnModelCreating(modelBuilder);
        }

    }
}
