using ClassLibrary.Dto;

namespace StorageAppWASM.Services.Abstraction
{
    public interface IResourcesService
    {
        Task<IEnumerable<ResourceDto>> GetAllAsync();
        Task<IEnumerable<ResourceDto>> GetAllIsActiveAsync(bool b);

        Task AddAsync(ResourceInsertDto resourceInsertDto);
        Task<ResourceDto> GetByIdAsync(int id);
        Task UpdateAsync(ResourceDto resourceDto);
    }
}
