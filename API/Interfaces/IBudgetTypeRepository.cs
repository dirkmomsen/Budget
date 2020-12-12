using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IBudgetTypeRepository
    {
        Task<bool> SaveAllAsync();
        Task<IEnumerable<BudgetType>> GetBudgetTypesAsync(bool includeDeleted = false);
        Task<BudgetType> GetBudgetTypeByIdAsync(int id, bool includeDeleted = false);
        void Add(BudgetType budgetType);
        void Update(BudgetType budgetType);
        void Delete(BudgetType budgetType);
    }
}
