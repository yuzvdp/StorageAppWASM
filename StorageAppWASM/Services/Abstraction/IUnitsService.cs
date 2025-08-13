using ClassLibrary.Dto;

namespace StorageAppWASM.Services.Abstraction
{
    public interface IUnitsService
    {
        Task<IEnumerable<UnitDto>> GetAllAsync();
        Task<IEnumerable<UnitDto>> GetAllIsActiveAsync(bool b);
        Task AddAsync(UnitInsertDto unitInsertDto);
        Task<UnitDto> GetByIdAsync(int id);
        Task UpdateAsync(UnitDto unitDto);
    }
}
