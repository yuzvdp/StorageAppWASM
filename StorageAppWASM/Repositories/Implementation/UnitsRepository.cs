using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using StorageAppWASM.Data;
using StorageAppWASM.Repositories.Abstraction;

namespace StorageAppWASM.Repositories.Implementation
{
    public class UnitsRepository : IUnitsRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        public UnitsRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public async Task<Unit> AddAsync(Unit unit)
        {
            var db = await _dbFactory.CreateDbContextAsync();

            Unit newItem = new()
            {
                Title = unit.Title,
            };
            await db.Units.AddAsync(newItem);
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

        public async Task<IEnumerable<Unit>> GetAllAsync()
        {
            var db = await _dbFactory.CreateDbContextAsync();
            return await db.Units.ToListAsync();
        }

        public async Task<IEnumerable<Unit>> GetAllIsActiveAsync(bool b)
        {
            var db = await _dbFactory.CreateDbContextAsync();
            return await db.Units.Where(p => p.IsActive == b).ToListAsync();
        }

        public async Task<Unit> GetByIdAsync(int Id)
        {
            var db = await _dbFactory.CreateDbContextAsync();
            return await db.Units.FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task UpdateAsync(Unit unit)
        {
            var db = await _dbFactory.CreateDbContextAsync();

            var item = await db.Units.FirstOrDefaultAsync(p => p.Id == unit.Id);

            if (item == null) return;

            item.Title = unit.Title;
            item.IsActive = unit.IsActive;

            await db.SaveChangesAsync();
        }
    }
}
