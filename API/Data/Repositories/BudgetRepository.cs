﻿using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly DataContext _context;

        public BudgetRepository(DataContext context)
        {
            _context = context;
        }

        public void AddBudget(Budget budget)
        {
            _context.Budgets.Add(budget);
        }

        public void DeleteBudget(Budget budget)
        {
            budget.Deleted = true;
        }

        public async Task<Budget> GetBudgetByIdAsync(int id, int userId)
        {
            return await GetBudgetsBase(userId)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<IEnumerable<Budget>> GetBudgetsAsync(int userId)
        {
            return await GetBudgetsBase(userId)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateBudget(Budget budget)
        {
            _context.Entry(budget).State = EntityState.Modified;
        }

        private IQueryable<Budget> GetBudgetsBase(int userId)
        {
            return _context.Budgets
                            .Include(b => b.UserBudgets)
                            .Where(b => b.UserBudgets.Any(u => u.UserId == userId))
                            .Where(b => b.Deleted == false);
        }
    }
}
