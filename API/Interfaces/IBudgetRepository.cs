using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IBudgetRepository
    {
        Task<bool> SaveAllAsync();
        Task<IEnumerable<Budget>> GetBudgetsAsync(int userId);
        Task<Budget> GetBudgetByIdAsync(int id, int userId);
        void AddBudget(Budget budget);
        void UpdateBudget(Budget budget);
        void DeleteBudget(Budget budget);
    }
}
