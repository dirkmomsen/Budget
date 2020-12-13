using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class BudgetItemRepository : IBudgetItemRepository
    {
        private readonly DataContext _context;

        public BudgetItemRepository(DataContext context)
        {
            _context = context;
        }

        public void AddBudgetItem(BudgetItem budgetItem)
        {
            _context.BudgetItems.Add(budgetItem);
        }

        public void DeleteBudgetItem(BudgetItem budgetItem)
        {
            budgetItem.Deleted = true;
        }

        public async Task<BudgetItem> GetBudgetItemByIdAsync(int id, int budgetId, bool includeDeleted = false)
        {
            return await GetBudgetItemBase(id, budgetId, includeDeleted)
                .FirstOrDefaultAsync(bi => bi.Id == id);
        }

        public Task<IEnumerable<BudgetItem>> GetBudgetItemsAsync(int budgetId, bool includeDeleted = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BudgetItem>> GetBudgetItemsByTypeAsync(int budgetId, int itemType, bool includeDeleted = false)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void UpdateBudgetItem(BudgetItem budgetItem)
        {
            _context.Entry(budgetItem).State = EntityState.Modified;
        }

        private IQueryable<BudgetItem> GetBudgetItemBase(int id, int budgetId, bool includeDeleted)
        {
            var budgetTypeBase = _context.BudgetItems
                            .Where(bi => bi.BudgetId == budgetId);

            if (includeDeleted is false)
                budgetTypeBase = budgetTypeBase.Where(bi => bi.Deleted == false);

            return budgetTypeBase;
        }
    }
}
