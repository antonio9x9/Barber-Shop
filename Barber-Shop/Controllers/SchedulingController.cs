using Barber_Shop.ApplicationDbContext;
using Barber_Shop.DTO;
using Barber_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barber_Shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SchedulingController : ControllerBase
    {
        public readonly AppDbContex _dbContext;

        public SchedulingController(AppDbContex appDbContex)
        {
            _dbContext = appDbContex;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Scheduling> schedulings = await _dbContext
                                                 .Scheduling
                                                 .Include(o => o.Client)
                                                 .Include(o => o.Services)
                                                 .ToListAsync();

            return Ok(schedulings);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            Scheduling schedulings = await _dbContext.Scheduling.FindAsync(id);

            if (schedulings is null)
            {
                return NotFound();
            }

            return Ok(schedulings);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SchedulingDTO schedulingDTO)
        {
            if (ModelState.IsValid)
            {
                //TODO: Add includ from clients and services
                Scheduling scheduling = new()
                {
                    Services = schedulingDTO.Services,
                    Client = schedulingDTO.Client,
                    ClientId = schedulingDTO.Client.Id,
                    Date = schedulingDTO.Date,
                    Observations = schedulingDTO.Observations,
                    Status = schedulingDTO.Status
                };

                _dbContext.Scheduling.Add(scheduling);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction("id", scheduling);
            }

            return BadRequest("Error during scheduling creation");
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, SchedulingDTO schedulingDTO)
        {
            if (id != schedulingDTO.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                //TODO: Add includ from clients and services
                Scheduling scheduling = _dbContext.Scheduling.Find(id);

                if (scheduling is null)
                {
                    return BadRequest();
                }
                
                scheduling.Status = schedulingDTO.Status;
                scheduling.Date = schedulingDTO.Date;
                scheduling.Services = schedulingDTO.Services;
                scheduling.Client = schedulingDTO.Client;
                scheduling.ClientId = schedulingDTO.Client.Id;

                _dbContext.Update(scheduling);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            //TODO: Add includ from clients and services
            Scheduling scheduling = _dbContext.Scheduling.Find(id);

            if (scheduling is null)
            {
                return BadRequest();
            }

            _dbContext.Remove(scheduling);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

    }
}
