using API.Entities;
using API.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<BudgetType> BudgetTypes { get; set; }
        public DbSet<BudgetItem> BudgetItems { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }

        public DbSet<Period> Periods { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
            
            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.Entity<AppUser>()
                .HasMany(au => au.Budgets)
                .WithMany(b => b.Users)
                .UsingEntity<AppUserBudget>(
                    j => j
                        .HasOne(auB => auB.Budget)
                        .WithMany(b => b.UserBudgets)
                        .HasForeignKey(auB => auB.BudgetId),
                    j => j
                        .HasOne(auB => auB.User)
                        .WithMany(au => au.UserBudgets)
                        .HasForeignKey(auB => auB.UserId),
                    j =>
                    {
                        j.HasKey(t => new { t.BudgetId, t.UserId });
                    });

            builder.Entity<Budget>()
                .HasMany(b => b.Items)
                .WithOne(i => i.Budget)
                .HasForeignKey(i => i.BudgetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Budget>()
                .HasOne(b => b.Type)
                .WithMany(bt => bt.Budgets)
                .HasForeignKey(b => b.TypeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Budget>()
                .HasOne(b => b.Period)
                .WithMany(per => per.Budgets)
                .HasForeignKey(b => b.PeriodId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<BudgetItem>()
                .HasOne(bi => bi.Type)
                .WithMany(bit => bit.Items)
                .HasForeignKey(bi => bi.TypeId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Entity<BudgetItem>()
                .HasOne(bi => bi.Period)
                .WithMany(per => per.Items)
                .HasForeignKey(bi => bi.PeriodId)
                .OnDelete(DeleteBehavior.SetNull);
            
        }
    }
}