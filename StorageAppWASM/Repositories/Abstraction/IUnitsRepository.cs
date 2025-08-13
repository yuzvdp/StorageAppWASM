using ClassLibrary.Models;

namespace StorageAppWASM.Repositories.Abstraction
{
    public interface IUnitsRepository
    {
        Task<IEnumerable<Unit>> GetAllAsync();

        Task<IEnumerable<Unit>> GetAllIsActiveAsync(bool b);
        Task<Unit> AddAsync(Unit unit);
        Task<Unit> GetByIdAsync(int Id);
        Task UpdateAsync(Unit unit);
    }
}
