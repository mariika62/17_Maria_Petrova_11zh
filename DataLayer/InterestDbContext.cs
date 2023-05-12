using BusinessLayer;

using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace DataLayer
{
    public class InterestDbContext : DbContext
    {
        public InterestDbContext()
        {

        }
        public InterestDbContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DESKTOP-IVFJCBT;Database=InterestsDb;Trusted_Connection=True;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Interest> Interests { get; set; }

        public DbSet<Category> Categories { get; set; }

    }
}