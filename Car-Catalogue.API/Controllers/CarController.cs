using Car_Catalogue.API.Data;
using Car_Catalogue.API.Model;
using Car_Catalogue.API.Model.Filters;
using Car_Catalogue.API.Model.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car_Catalogue.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private Repository<Car> _carRepository { get; set; }
        private ApplicationDbContext _context { get; set; }
        public CarController(ApplicationDbContext dbContext)
        {
            _carRepository = new Repository<Car>(dbContext);
            _context = dbContext;
        }
        [Route("paginated")]
        [HttpGet]
        public async Task<ActionResult<PaginatedResponse<Car>>> GetPaginated([FromQuery] CarFilterRequest request)
        {
            var result = await request.GetFiltered(_context);
            var paginated = PaginationHandler.GetPaginatedResponse<PaginatedResponse<Car>, Car>(result, request);
            return Ok(paginated);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetById([FromRoute] string id)
        {
            var result = await _carRepository.GetById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetAll()
        {
            var result = await _carRepository.GetAll();
            if (result == null) return NoContent();
            return Ok(result);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<ActionResult> AddCar([FromBody] Car carInput)
        {
            await _carRepository.Post(carInput);
            return CreatedAtAction(nameof(GetById), carInput.Id);
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPut]
        public async Task<ActionResult> UpdateCar([FromBody] Car carInput)
        {
            await _carRepository.Put(carInput);
            return Ok();
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete]
        public async Task<ActionResult> DeleteCar([FromBody] Car carInput)
        {
            await _carRepository.Delete(carInput);
            return Ok();
        }
    }
}
