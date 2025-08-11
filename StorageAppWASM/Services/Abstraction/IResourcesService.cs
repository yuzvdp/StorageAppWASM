using ClassLibrary.Dto;

namespace StorageAppWASM.Services.Abstraction
{
    public interface IResourcesService
    {
        Task<IEnumerable<ResourceDto>> GetResourcesAsync();
        Task AddAsync(ResourceInsertDto resourceInsertDto);
        Task<ResourceDto> GetById(int id);
        Task Update(ResourceDto resourceDto);
    }
}
