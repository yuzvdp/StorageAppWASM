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
        public async Task<IEnumerable<Resource>> GetAll()
        {
            var db = _dbFactory.CreateDbContext();
            return await db.Resources.ToListAsync();
        }

        public async Task<Resource> GetById(int Id)
        {
            var db = _dbFactory.CreateDbContext();
            return await db.Resources.FirstOrDefaultAsync(p => p.Id == Id);
        }

        public async Task<Resource> Add(Resource resource)
        {
            var db = _dbFactory.CreateDbContext();

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

        public async Task Update(Resource resource)
        {
            var db = _dbFactory.CreateDbContext();

            var item = await db.Resources.FirstOrDefaultAsync(p => p.Id == resource.Id);

            if (item == null) return;

            item.Title = resource.Title;
            item.IsActive = resource.IsActive;

            await db.SaveChangesAsync();
        }
    }
}
