using ClassLibrary.Dto;
using Microsoft.AspNetCore.Mvc;
using StorageAppWASM.Services.Abstraction;

namespace StorageAppWASM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsIncomeController : ControllerBase
    {
        private readonly IDocumentsIncomeService _documentsIncomeService;

        public DocumentsIncomeController(IDocumentsIncomeService documentsIncomeService)
        {
            _documentsIncomeService = documentsIncomeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentIncomeDto>>> GetItemsAsync()
        {
            return Ok(await _documentsIncomeService.GetAllAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItemAsync(int id, [FromBody] DocumentIncomeDto updatedItem)
        {
            if (id != updatedItem.Id) return BadRequest("ID in URL does not match ID in request body.");

            var existingItem = await _documentsIncomeService.GetByIdAsync(id);

            if (existingItem == null) return NotFound($"Resource with ID {id} not found.");

            existingItem.Number = updatedItem.Number;
            existingItem.Date = updatedItem.Date;

            await Task.FromResult(_documentsIncomeService.UpdateAsync(existingItem));

            return NoContent();
        }

        [HttpGet("getbyid/{id}")]
        public async Task<ActionResult<DocumentIncomeDto>> GetItemById(int id)
        {
            if (id == 0) return NotFound();

            var item = await _documentsIncomeService.GetByIdAsync(id);

            if (item == null) return NotFound();

            return Ok(item);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveItemById(int id)
        {
            if (id == 0) return NotFound();

            await _documentsIncomeService.RemoveAsync(id);

            return NoContent();
        }

        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AddItemAsync([FromBody] DocumentIncomeInsertDto documentIncomeInsertDto)
        {
            if (documentIncomeInsertDto.Number == 0) return BadRequest();
            try
            {
                await _documentsIncomeService.AddAsync(documentIncomeInsertDto);
                return Created();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }
    }
}
