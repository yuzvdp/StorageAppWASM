using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using StorageAppWASM.Data;
using StorageAppWASM.Repositories.Abstraction;

namespace StorageAppWASM.Repositories.Implementation
{
    public class BalancesRepository : IBalancesRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        public BalancesRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<Balance> AddAsync(Balance balance)
        {
            var db = await _dbFactory.CreateDbContextAsync();

            Balance newItem = new()
            {
                UnitId = balance.UnitId,
                ResourceId = balance.ResourceId,
                Count = balance.Count,
            };
            await db.Balances.AddAsync(newItem);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return await Task.FromResult(newItem);
        }

        public async Task<IEnumerable<Balance>> GetAllAsync()
        {
            var db = await _dbFactory.CreateDbContextAsync();
            return await db.Balances
                .Include(u => u.Unit)
                .Include(r => r.Resource)
                .ToListAsync();
        }

        public async Task<IEnumerable<Balance>> GetAllGroupedAsync()
        {
            var db = await _dbFactory.CreateDbContextAsync();
            return (IEnumerable<Balance>)await db.Balances
                .Include(u => u.Unit)
                .Include(r => r.Resource)
                .GroupBy(g => g.ResourceId)
                .ToListAsync();
        }

        public async Task<Balance> GetByIdAsync(int Id)
        {
            var db = await _dbFactory.CreateDbContextAsync();
            return await db.Balances
                .Include(u => u.Unit)
                .Include(u => u.Resource)
                .FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<Balance> GetByResourceAndUnitAsync(int resourceId, int unitId)
        {
            var db = await _dbFactory.CreateDbContextAsync();
            return await db.Balances
                .Include(u => u.Unit)
                .Include(u => u.Resource)
                .FirstOrDefaultAsync(p => p.ResourceId == resourceId && p.UnitId == unitId);
        }

        public async Task RemoveAsync(int Id)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var item = await db.Balances.FirstOrDefaultAsync(u => u.Id == Id);

            if (item != null)
            {
                db.Balances.Remove(item);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Balance> UpdateAsync(Balance balance)
        {
            using var db = await _dbFactory.CreateDbContextAsync();
            var existBalance = db.Balances.FirstOrDefaultAsync(p => p.Id == balance.Id).Result;

            if (existBalance != null)
            {
                existBalance.ResourceId = balance.ResourceId;
                existBalance.UnitId = balance.UnitId;
                existBalance.Count = balance.Count;
                db.Balances.Update(existBalance);
                await db.SaveChangesAsync();
            }

            return existBalance;
        }
    }
}
