using ClassLibrary.Dto;
using Microsoft.AspNetCore.Mvc;
using StorageAppWASM.Services.Abstraction;


namespace StorageAppWASM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourcesController : ControllerBase
    {

        private readonly IResourcesService _resourceService;

        public ResourcesController(IResourcesService resourceService)
        {
            _resourceService = resourceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetResourcesAsync()
        {
            return Ok(await _resourceService.GetAllAsync());
        }

        [HttpGet("{b}")]
        public async Task<ActionResult<IEnumerable<ResourceDto>>> GetResourcesIsActiveAsync(bool b)
        {
            return Ok(await _resourceService.GetAllIsActiveAsync(b));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResourceAsync(int id, [FromBody] ResourceDto updatedResource)
        {
            if (id != updatedResource.Id) return BadRequest("ID in URL does not match ID in request body.");

            var existingResource = await _resourceService.GetByIdAsync(id);

            if (existingResource == null) return NotFound($"Resource with ID {id} not found.");

            existingResource.Title = updatedResource.Title;
            existingResource.IsActive = updatedResource.IsActive;

            await Task.FromResult(_resourceService.UpdateAsync(existingResource));

            return NoContent();
        }

        [HttpGet("getbyid/{id}")]
        public async Task<ActionResult<ResourceDto>> GetResourceById(int id)
        {
            if (id == 0) return NotFound();

            var item = await _resourceService.GetByIdAsync(id);

            if (item == null) return NotFound();

            return Ok(item);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddResourceAsync([FromBody] ResourceInsertDto resourceInsertDto)
        {
            if (string.IsNullOrEmpty(resourceInsertDto.Title)) return BadRequest();
            try
            {
                await _resourceService.AddAsync(resourceInsertDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Created();
        }

    }
}
