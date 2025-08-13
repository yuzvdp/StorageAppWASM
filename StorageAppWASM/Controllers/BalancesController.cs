using ClassLibrary.Dto;
using ClassLibrary.Filters;
using Microsoft.AspNetCore.Mvc;
using StorageAppWASM.Services.Abstraction;

namespace StorageAppWASM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalancesController : ControllerBase
    {
        private readonly IBalancesService _balanceService;

        public BalancesController(IBalancesService balanceService)
        {
            _balanceService = balanceService;
        }

        [HttpPost("filter")]
        public async Task<ActionResult<BalanceDto>> FilterProducts([FromBody] BalanceFilter filter)
        {
            var items = await _balanceService.GetAllAsync();

            if (filter.ResourceTitles.Count != 0 && filter.UnitTitles.Count != 0)
            {
                return Ok(items.Where(p => filter.ResourceTitles.Contains(p.ResourceTitle) && filter.UnitTitles.Contains(p.UnitTitle)));
            }
            if (filter.ResourceTitles.Count != 0 && filter.UnitTitles.Count == 0)
            {
                return Ok(items.Where(p => filter.ResourceTitles.Contains(p.ResourceTitle)));
            }
            if (filter.UnitTitles.Count != 0 && filter.ResourceTitles.Count == 0)
            {
                return Ok(items.Where(p => filter.UnitTitles.Contains(p.UnitTitle)));
            }
            return Ok(items);
        }

        // GET: api/Balances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BalanceDto>>> GetAll()
        {
            return Ok(await _balanceService.GetAllAsync());
        }

        // GET: api/Balances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BalanceDto>> GetBalanceDto(int id)
        {
            if (id == 0) return NotFound();

            var item = await _balanceService.GetByIdAsync(id);

            if (item == null) return NotFound();

            return Ok(item);
        }

        // POST: api/Balances  
        [HttpPost]
        public async Task<ActionResult<BalanceDto>> PostBalanceDto(BalanceInsertDto balanceInsertDto)
        {
            try
            {
                await _balanceService.AddAsync(balanceInsertDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Created();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveItemById(int id)
        {
            if (id == 0) return NotFound();

            await _balanceService.RemoveAsync(id);

            return NoContent();
        }

    }
}
