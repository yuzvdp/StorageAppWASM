using ClassLibrary.Models;

namespace StorageAppWASM.Repositories.Abstraction
{
    public interface IBalancesRepository
    {
        Task<IEnumerable<Balance>> GetAllAsync();
        Task<IEnumerable<Balance>> GetAllGroupedAsync();
        Task<Balance> AddAsync(Balance balance);
        Task<Balance> GetByIdAsync(int Id);

        Task RemoveAsync(int Id);

        Task<Balance> UpdateAsync(Balance balance);

        Task<Balance> GetByResourceAndUnitAsync(int resourceId, int unitId);
    }
}
