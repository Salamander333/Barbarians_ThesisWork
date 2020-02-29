using System;
using System.Collections.Generic;
using System.Text;
using Barbarians.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Barbarians.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Armor> Armors { get; }

        public DbSet<Weapon> Weapons { get; }

        public DbSet<Material> Materials { get; }

        public DbSet<ApplicationUser> ApplicationUsers { get; }

        public DbSet<Statue> Statues { get; }

        public DbSet<UserStatue> UsersStatues { get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserStatue>(entity =>
            {
                entity.HasKey(x => new
                {
                    x.StatueId,
                    x.UserId,
                });

                entity.HasOne(x => x.User).WithMany(x => x.UserStatues).HasForeignKey(x => x.UserId);

                entity.HasOne(x => x.Statue).WithMany(x => x.UserStatues).HasForeignKey(x => x.StatueId);
            });
        }
    }
}
