using api.Dtos.Stock;
using api.Helpers;
using api.Interface;
using api.Mapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _stockRepository;
        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stocks = await _stockRepository.GetAllAsync(query);
            var stocksDtos = stocks.Select(s => s.ToStockRequestDto()).ToList();

            return Ok(stocksDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stock = await _stockRepository.GetByIdAsync(id);
            if (stock == null)
                return NotFound();
            return Ok(stock.ToStockRequestDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StockCreateRequestDto stockDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = stockDTO.ToStockFromCreateDTO();
            await _stockRepository.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockRequestDto());
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] StockUpdateRequestDto stockDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var stockModel = await _stockRepository.UpdateAsync(id, stockDto);
            if (stockModel == null)
                return NotFound();
            return Ok(stockModel.ToStockRequestDto());
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userModel = await _stockRepository.DeleteAsync(id);
            if (userModel == null)
                return NotFound();
            return NoContent();
        }
    }
}