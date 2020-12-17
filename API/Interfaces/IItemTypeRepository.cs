using API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Interfaces
{
    public interface IItemTypeRepository
    {
        Task<bool> SaveAllAsync();
        Task<IEnumerable<ItemType>> GetBudgetTypesAsync(bool includeDeleted = false);
        Task<ItemType> GetBudgetTypeByIdAsync(int id, bool includeDeleted = false);
        void Add(ItemType itemType);
        void Update(ItemType itemType);
        void Delete(ItemType itemType);
    }
}
