using ClassLibrary.Models;
using Microsoft.EntityFrameworkCore;
using StorageAppWASM.Data;
using StorageAppWASM.Repositories.Abstraction;

namespace StorageAppWASM.Repositories.Implementation
{
    public class ResourcesRepository : IResourcesRepository
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbFactory;
        public ResourcesRepository(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            _dbFactory = dbFactory;
        }
        public async Task<IEnumerable<Resource>> GetAllAsync()
        {
            var db = await _dbFactory.CreateDbContextAsync();
            return await db.Resources.ToListAsync();
        }

        public async Task<Resource> GetByIdAsync(int Id)
        {
            var db = await _dbFactory.CreateDbContextAsync();
            return await db.Resources.FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<Resource> AddAsync(Resource resource)
        {
            var db = await _dbFactory.CreateDbContextAsync();

            Resource newResource = new()
            {
                Title = resource.Title,
            };
            await db.Resources.AddAsync(newResource);
            try
            {
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return await Task.FromResult(newResource);
        }

        public async Task UpdateAsync(Resource resource)
        {
            var db = await _dbFactory.CreateDbContextAsync();

            var item = await db.Resources.FirstOrDefaultAsync(p => p.Id == resource.Id);

            if (item == null) return;

            item.Title = resource.Title;
            item.IsActive = resource.IsActive;

            await db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Resource>> GetAllIsActiveAsync(bool b)
        {
            var db = await _dbFactory.CreateDbContextAsync();
            return await db.Resources.Where(p => p.IsActive == b).ToListAsync();
        }
    }
}
