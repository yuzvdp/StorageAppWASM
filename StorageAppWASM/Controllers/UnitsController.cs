using ClassLibrary.Dto;
using Microsoft.AspNetCore.Mvc;
using StorageAppWASM.Services.Abstraction;

namespace StorageAppWASM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitsController : ControllerBase
    {
        private readonly IUnitsService _unitsService;

        public UnitsController(IUnitsService unitsService)
        {
            _unitsService = unitsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitDto>>> GetAllAsync()
        {
            var items = await _unitsService.GetAllAsync();
            if (items == null) return Empty;
            return Ok(items);
        }

        [HttpGet("{b}")]
        public async Task<ActionResult<IEnumerable<UnitDto>>> GetAllIsActiveAsync(bool b)
        {
            var items = await _unitsService.GetAllIsActiveAsync(b);
            if (items == null) return Empty;
            return Ok(items);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnitAsync(int id, [FromBody] UnitDto updatedUnit)
        {
            if (id != updatedUnit.Id) return BadRequest("ID in URL does not match ID in request body.");

            var existingUnit = await _unitsService.GetByIdAsync(id);

            if (existingUnit == null) return NotFound($"Resource with ID {id} not found.");

            existingUnit.Title = updatedUnit.Title;
            existingUnit.IsActive = updatedUnit.IsActive;

            await Task.FromResult(_unitsService.UpdateAsync(existingUnit));

            return NoContent();
        }

        [HttpGet("getbyid/{id}")]
        public async Task<ActionResult<UnitDto>> GetUnitById(int id)
        {
            if (id == 0) return NotFound();

            var item = await _unitsService.GetByIdAsync(id);

            if (item == null) return NotFound();

            return Ok(item);
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddUnitAsync([FromBody] UnitInsertDto unitInsertDto)
        {
            if (string.IsNullOrEmpty(unitInsertDto.Title)) return BadRequest();
            try
            {
                await _unitsService.AddAsync(unitInsertDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Created();
        }
    }
}
