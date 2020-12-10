using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data.Repositories
{
    public class BudgetTypeRepository : IBudgetTypeRepository
    {
        private readonly DataContext _context;

        public BudgetTypeRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(BudgetType budgetType)
        {
            _context.BudgetTypes.Add(budgetType);
        }

        public void Delete(BudgetType budgetType)
        {
            budgetType.Deleted = true;
        }

        public async Task<BudgetType> GetBudgetTypeByIdAsync(int id)
        {
            return await _context.BudgetTypes
                .Where(x => x.Deleted == false)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<BudgetType>> GetBudgetTypesAsync()
        {
            return await _context.BudgetTypes
                .Where(x => x.Deleted == false)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(BudgetType budgetType)
        {
            _context.Entry(budgetType).State = EntityState.Modified;
        }
    }
}
