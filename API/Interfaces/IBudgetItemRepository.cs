using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IBudgetItemRepository
    {
        Task<bool> SaveAllAsync();
        Task<IEnumerable<BudgetItem>> GetBudgetItemsAsync(int budgetId, bool includeDeleted = false);
        Task<IEnumerable<BudgetItem>> GetBudgetItemsByTypeAsync(int budgetId, int itemTypeId, bool includeDeleted = false);
        Task<BudgetItem> GetBudgetItemByIdAsync(int id, int budgetId, bool includeDeleted = false);
        void AddBudgetItem(BudgetItem budgetItem);
        void UpdateBudgetItem(BudgetItem budgetItem);
        void DeleteBudgetItem(BudgetItem budgetItem);
    }
}
