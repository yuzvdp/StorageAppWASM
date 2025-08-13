using ClassLibrary.Models;

namespace StorageAppWASM.Repositories.Abstraction
{
    public interface IResourcesRepository
    {
        Task<IEnumerable<Resource>> GetAllAsync();

        Task<IEnumerable<Resource>> GetAllIsActiveAsync(bool b);
        Task<Resource> AddAsync(Resource resource);
        Task<Resource> GetByIdAsync(int Id);


        Task UpdateAsync(Resource resource);
    }
}
