using ClassLibrary.Dto;
using ClassLibrary.Models;
using StorageAppWASM.Repositories.Abstraction;
using StorageAppWASM.Services.Abstraction;

namespace StorageAppWASM.Services.Implementation
{
    public class BalancesService : IBalancesService
    {
        private readonly IBalancesRepository _balancesRepository;

        public BalancesService(IBalancesRepository balancesRepository)
        {
            _balancesRepository = balancesRepository;
        }
        public async Task AddAsync(BalanceInsertDto balanceInsertDto)
        {
            Balance newItem = new()
            {
                ResourceId = balanceInsertDto.ResourceId,
                UnitId = balanceInsertDto.UnitId,
                Count = balanceInsertDto.Count,
            };

            await _balancesRepository.AddAsync(newItem);
        }

        public async Task<IEnumerable<BalanceDto>> GetAllAsync()
        {
            var balances = await _balancesRepository.GetAllAsync();
            return from balance in balances
                   select new BalanceDto
                   {
                       Id = balance.Id,
                       UnitId = balance.UnitId,
                       UnitTitle = balance.Unit.Title,
                       ResourceId = balance.ResourceId,
                       ResourceTitle = balance.Resource.Title,
                       Count = balance.Count,
                   };
        }

        public async Task<IEnumerable<BalanceDto>> GetAllGroupedAsync()
        {
            var balances = await _balancesRepository.GetAllGroupedAsync();
            return from balance in balances
                   select new BalanceDto
                   {
                       Id = balance.Id,
                       UnitId = balance.UnitId,
                       UnitTitle = balance.Unit.Title,
                       ResourceId = balance.ResourceId,
                       ResourceTitle = balance.Resource.Title,
                       Count = balance.Count,
                   };
        }

        public async Task<BalanceDto> GetByIdAsync(int id)
        {
            var item = await _balancesRepository.GetByIdAsync(id);

            BalanceDto balanceDto = new()
            {
                Id = item.Id,
                UnitId = item.UnitId,
                UnitTitle = item.Unit.Title,
                ResourceId = item.ResourceId,
                ResourceTitle = item.Resource.Title,
                Count = item.Count,
            };
            return balanceDto;
        }

        public async Task<BalanceDto> GetByResourceAndUnitAsync(int resourceId, int unitId)
        {
            var item = await _balancesRepository.GetByResourceAndUnitAsync(resourceId, unitId);

            BalanceDto balanceDto = new()
            {
                Id = item.Id,
                UnitId = item.UnitId,
                UnitTitle = item.Unit.Title,
                ResourceId = item.ResourceId,
                ResourceTitle = item.Resource.Title,
                Count = item.Count,
            };
            return balanceDto;
        }

        public async Task RemoveAsync(int id)
        {
            await _balancesRepository.RemoveAsync(id);
        }

        public async Task<BalanceDto> UpdateAsync(BalanceDto balanceDto)
        {
            await _balancesRepository.UpdateAsync(new Balance
            {
                ResourceId = balanceDto.Id,
                UnitId = balanceDto.UnitId,
                Count = balanceDto.Count,
            });

            return balanceDto;
        }
    }
}
