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

        public DbSet<Armor> Armors { get; set; }

        public DbSet<Weapon> Weapons { get; set; }

        public DbSet<Material> Materials { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Statue> Statues { get; set; }

        public DbSet<UserStatue> UsersStatues { get; set; }

        public DbSet<TaskGather> TasksGather { get; set; }

        public DbSet<CraftableArmor> CraftableArmors { get; set; }

        public DbSet<CraftableWeapon> CraftableWeapons { get; set; }

        public DbSet<BattleReport> BattleReports { get; set; }

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
