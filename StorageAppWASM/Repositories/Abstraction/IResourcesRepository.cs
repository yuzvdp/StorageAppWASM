using ClassLibrary.Models;

namespace StorageAppWASM.Repositories.Abstraction
{
    public interface IResourcesRepository
    {
        Task<IEnumerable<Resource>> GetAll();
        Task<Resource> Add(Resource resource);
        Task<Resource> GetById(int Id);
        Task Update(Resource resource);
    }
}
