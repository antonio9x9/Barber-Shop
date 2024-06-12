using Barber_Shop.ApplicationDbContext;
using Barber_Shop.DTO;
using Barber_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Barber_Shop.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        public readonly AppDbContex _dbContext;

        public ClientController(AppDbContex appDbContex)
        {
            _dbContext = appDbContex;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Client> clients = await _dbContext.Clients.ToListAsync();

            return Ok(clients);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            Client client = await _dbContext.Clients.FindAsync(id);

            if (client is null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ClientDTO clientDTO)
        {
            if (ModelState.IsValid)
            {
                Client client = new()
                {
                    BirthDate = clientDTO.BirthDate,
                    Email = clientDTO.Email,
                    Name = clientDTO.Name,
                    PhoneNumber = clientDTO.PhoneNumber,
                };

                _dbContext.Clients.Add(client);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction("id", client);
            }

            return BadRequest("Error during client creation");
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ClientDTO clientDTO)
        {
            if (id != clientDTO.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                Client client = _dbContext.Clients.Find(id);

                if (client is null)
                {
                    return BadRequest();
                }

                client.PhoneNumber = clientDTO.PhoneNumber;
                client.Name = clientDTO.Name;
                client.Email = clientDTO.Email;
                client.BirthDate = clientDTO.BirthDate;

                _dbContext.Update(client);
                await _dbContext.SaveChangesAsync();

                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            Client client = _dbContext.Clients.Find(id);

            if (client is null)
            {
                return BadRequest();
            }

            _dbContext.Remove(client);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

    }
}
