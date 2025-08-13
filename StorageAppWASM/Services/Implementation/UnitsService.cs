using ClassLibrary.Dto;
using ClassLibrary.Models;
using StorageAppWASM.Repositories.Abstraction;
using StorageAppWASM.Services.Abstraction;

namespace StorageAppWASM.Services.Implementation
{
    public class UnitsService : IUnitsService
    {
        private readonly IUnitsRepository _unitsRepository;

        public UnitsService(IUnitsRepository unitsRepository)
        {
            _unitsRepository = unitsRepository;
        }

        public async Task AddAsync(UnitInsertDto unitInsertDto)
        {
            if (!string.IsNullOrEmpty(unitInsertDto.Title))
            {
                Unit newItem = new()
                {
                    Title = unitInsertDto.Title,
                };
                await _unitsRepository.AddAsync(newItem);
            }
        }

        public async Task<IEnumerable<UnitDto>> GetAllAsync()
        {
            var unitDtos = await _unitsRepository.GetAllAsync();
            return from unitDto in unitDtos
                   select new UnitDto
                   {
                       Id = unitDto.Id,
                       Title = unitDto.Title,
                       IsActive = unitDto.IsActive
                   };
        }

        public async Task<IEnumerable<UnitDto>> GetAllIsActiveAsync(bool b)
        {
            var unitDtos = await _unitsRepository.GetAllIsActiveAsync(b);
            return from unitDto in unitDtos
                   select new UnitDto
                   {
                       Id = unitDto.Id,
                       Title = unitDto.Title,
                       IsActive = unitDto.IsActive
                   };
        }

        public async Task<UnitDto> GetByIdAsync(int id)
        {
            var item = await _unitsRepository.GetByIdAsync(id);

            UnitDto newItem = new()
            {
                Title = item.Title,
                Id = item.Id,
                IsActive = item.IsActive,
            };
            return newItem;
        }

        public async Task UpdateAsync(UnitDto unitDto)
        {
            var item = await _unitsRepository.GetByIdAsync(unitDto.Id);
            if (item == null) return;

            item.Title = unitDto.Title;
            item.IsActive = unitDto.IsActive;

            await _unitsRepository.UpdateAsync(item);
        }
    }
}
