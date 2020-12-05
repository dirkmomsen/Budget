using API.Entities;
using API.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            if (users == null) return;

            var roles = new List<AppRole>
            {
                new AppRole{ Name = "Admin" },
                new AppRole{ Name = "User" }
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "User");
            }

            var admin = new AppUser
            {
                UserName = "admin",
                Email = "admin@test.mail.com"
            };

            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] { "Admin" });
        }

        public static async Task SeedBudgetTypes(DataContext context)
        {
            await context.BudgetTypes.AddAsync(new()
            {
                Name = "Fortnightly",
                Description = "Budget that will allow you to create fortnightly budgets."
            });

            await context.BudgetTypes.AddAsync(new()
            {
                Name = "Monthly",
                Description = "Budget that will allow you to create monthly budgets."
            });

            await context.BudgetTypes.AddAsync(new()
            {
                Name = "Yearly",
                Description = "Budget that will allow you to create yearly budgets."
            });

            await context.SaveChangesAsync();
        }

        public static async Task SeedItemTypes(DataContext context)
        {
            await context.ItemTypes.AddAsync(new()
            {
                Name = "YearlyIncome",
                DisplaySymbol = '+',
            });

            await context.ItemTypes.AddAsync(new()
            {
                Name = "Income",
                DisplaySymbol = '+',
            });

            await context.ItemTypes.AddAsync(new()
            {
                Name = "PlannedExpense",
                DisplaySymbol = '~',
            });

            await context.ItemTypes.AddAsync(new()
            {
                Name = "Expense",
                DisplaySymbol = '-',
            });

            await context.SaveChangesAsync();
        }

        public static async Task SeedBudgets(DataContext context, UserManager<AppUser> userManager)
        {
            var budgetTypes = await context.BudgetTypes.ToListAsync();
            var itemTypes = await context.ItemTypes.ToListAsync();
            var users = await userManager.Users.ToListAsync();

            var budgetCount = users.Count;

            Random rnd = new Random();

            var budgets = new List<Budget>();
            for (int i = 0; i < budgetCount; i++)
            {
                var randomBudgetType = rnd.Next(0, budgetTypes.Count - 1);

                var items = new List<BudgetItem>();
                for (int o = 0; o < rnd.Next(1, 10); o++)
                {
                    items.Add(new()
                    {
                        Type = itemTypes[rnd.Next(0, itemTypes.Count - 1)],
                        Description = $"Random Item {o}",
                        Value = Convert.ToDecimal(rnd.NextDouble() * 10000)
                    });
                }

                budgets.Add(new()
                {
                    Name = $"{budgetTypes[randomBudgetType].Name} {i}",
                    Type = budgetTypes[randomBudgetType],
                    Items = items,
                    Users = new List<AppUser>() { users[i] }
                });
            }

            await context.Budgets.AddRangeAsync(budgets);
            await context.SaveChangesAsync();
        }
    }
}
