using ClassLibrary.Dto;

namespace StorageAppWASM.Services.Abstraction
{
    public interface IBalancesService
    {
        Task<IEnumerable<BalanceDto>> GetAllAsync();
        Task<IEnumerable<BalanceDto>> GetAllGroupedAsync();
        Task AddAsync(BalanceInsertDto balanceInsertDto);
        Task<BalanceDto> GetByIdAsync(int id);
        Task RemoveAsync(int id);

        Task<BalanceDto> UpdateAsync(BalanceDto balanceDto);

        Task<BalanceDto> GetByResourceAndUnitAsync(int resourceId, int unitId);
    }
}
