using ClassLibrary.Dto;
using ClassLibrary.Models;
using StorageAppWASM.Repositories.Abstraction;
using StorageAppWASM.Services.Abstraction;

namespace StorageAppWASM.Services.Implementation
{
    public class ResourcesService : IResourcesService
    {
        private readonly IResourcesRepository _resourceRepository;

        public ResourcesService(IResourcesRepository resourceRepository)
        {
            _resourceRepository = resourceRepository;
        }

        public async Task AddAsync(ResourceInsertDto resourceInsertDto)
        {
            if (!string.IsNullOrEmpty(resourceInsertDto.Title))
            {
                Resource newResource = new()
                {
                    Title = resourceInsertDto.Title,
                };
                await _resourceRepository.AddAsync(newResource);
            }
        }

        public async Task<ResourceDto> GetByIdAsync(int id)
        {
            var item = await _resourceRepository.GetByIdAsync(id);
            //if (item == null) return new ResourceDto();

            ResourceDto newResource = new()
            {
                Title = item.Title,
                Id = item.Id,
                IsActive = item.IsActive,
            };
            return newResource;
        }

        public async Task<IEnumerable<ResourceDto>> GetAllAsync()
        {
            var resourceDtos = await _resourceRepository.GetAllAsync();
            return from resourceDto in resourceDtos
                   select new ResourceDto
                   {
                       Id = resourceDto.Id,
                       Title = resourceDto.Title,
                       IsActive = resourceDto.IsActive
                   };
        }

        public async Task UpdateAsync(ResourceDto resourceDto)
        {
            var item = await _resourceRepository.GetByIdAsync(resourceDto.Id);
            if (item == null) return;

            item.Title = resourceDto.Title;
            item.IsActive = resourceDto.IsActive;

            await _resourceRepository.UpdateAsync(item);
        }

        public async Task<IEnumerable<ResourceDto>> GetAllIsActiveAsync(bool b)
        {
            var resourceDtos = await _resourceRepository.GetAllIsActiveAsync(b);
            return from resourceDto in resourceDtos
                   select new ResourceDto
                   {
                       Id = resourceDto.Id,
                       Title = resourceDto.Title,
                       IsActive = resourceDto.IsActive
                   };
        }
    }
}
