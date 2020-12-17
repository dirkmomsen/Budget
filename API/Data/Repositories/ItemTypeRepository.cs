using API.Entities;
using API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Repositories
{
    public class ItemTypeRepository : IItemTypeRepository
    {
        public void Add(ItemType itemType)
        {
            throw new NotImplementedException();
        }

        public void Delete(ItemType itemType)
        {
            throw new NotImplementedException();
        }

        public Task<ItemType> GetBudgetTypeByIdAsync(int id, bool includeDeleted = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemType>> GetBudgetTypesAsync(bool includeDeleted = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAllAsync()
        {
            throw new NotImplementedException();
        }

        public void Update(ItemType itemType)
        {
            throw new NotImplementedException();
        }
    }
}
