using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Repositories
{
    public class ItemTypeRepository : IItemTypeRepository
    {
        private readonly DataContext _context;

        public ItemTypeRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(ItemType itemType)
        {
            _context.ItemTypes.Add(itemType);
        }

        public void Delete(ItemType itemType)
        {
            itemType.Deleted = true;
        }

        public async Task<ItemType> GetItemTypeByIdAsync(int id, bool includeDeleted = false)
        {
            return await GetItemTypesBase(includeDeleted)
                .FirstOrDefaultAsync(bt => bt.Id == id);
        }

        public async Task<IEnumerable<ItemType>> GetItemTypesAsync(bool includeDeleted = false)
        {
            return await GetItemTypesBase(includeDeleted)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(ItemType itemType)
        {
            _context.Entry(itemType).State = EntityState.Modified;
        }

        private IQueryable<ItemType> GetItemTypesBase(bool includeDeleted)
        {
            var budgetTypes = _context.ItemTypes.AsQueryable();

            if (includeDeleted is false)
                budgetTypes = budgetTypes.Where(bt => bt.Deleted == false);

            return budgetTypes;
        }
    }
}
