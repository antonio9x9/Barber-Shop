using Barber_Shop.ApplicationDbContext;
using Barber_Shop.DTO;
using Barber_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barber_Shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicesController : ControllerBase
    {
        public readonly AppDbContex _dbContext;

        public ServicesController(AppDbContex appDbContex)
        {
            _dbContext = appDbContex;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Service> Services = await _dbContext.Services.ToListAsync();

            return Ok(Services);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            Service service = await _dbContext.Services.FindAsync(id);

            if (service is null)
            {
                return NotFound();
            }

            return Ok(service);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ServiceDTO clientDTO)
        {
            if (ModelState.IsValid)
            {
                Service service = new()
                {
                    Description = clientDTO.Description,
                    MinDuration = clientDTO.MinDuration,
                    Name = clientDTO.Name,
                    Price = clientDTO.Price,
                };

                _dbContext.Services.Add(service);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction("id", service);
            }

            return BadRequest("Error during service creation");
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ServiceDTO serviceDTO)
        {
            if (id != serviceDTO.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                Service service = _dbContext.Services.Find(id);

                if (service is null)
                {
                    return BadRequest();
                }

                _dbContext.Update(service);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id:int}")]

        public async Task<IActionResult> Delete(int id)
        {
            Service service = _dbContext.Services.Find(id);

            if (service is null)
            {
                return BadRequest();
            }

            _dbContext.Remove(service);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
