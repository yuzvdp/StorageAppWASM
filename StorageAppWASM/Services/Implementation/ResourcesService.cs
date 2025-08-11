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
                await _resourceRepository.Add(newResource);
            }
        }

        public async Task<ResourceDto> GetById(int id)
        {
            var item = await _resourceRepository.GetById(id);
            //if (item == null) return new ResourceDto();

            ResourceDto newResource = new()
            {
                Title = item.Title,
                Id = item.Id,
                IsActive = item.IsActive,
            };
            return newResource;

        }

        public async Task<IEnumerable<ResourceDto>> GetResourcesAsync()
        {
            var resourceDtos = await _resourceRepository.GetAll();
            return from resourceDto in resourceDtos
                   select new ResourceDto
                   {
                       Id = resourceDto.Id,
                       Title = resourceDto.Title,
                       IsActive = resourceDto.IsActive
                   };
        }

        public async Task Update(ResourceDto resourceDto)
        {
            var item = await _resourceRepository.GetById(resourceDto.Id);
            if (item == null) return;

            item.Title = resourceDto.Title;
            item.IsActive = resourceDto.IsActive;

            await _resourceRepository.Update(item);
        }
    }
}
