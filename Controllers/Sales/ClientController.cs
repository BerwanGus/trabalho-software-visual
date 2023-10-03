using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;

using APISale.Models;
using API.Controllers;

namespace APISale.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ClientController : Back2youControllerBase
  {
    private readonly DBContext _dbContext;

    public ClientController(DBContext dbContext)
    {
      _dbContext = dbContext;
    }

    // GET: api/Client

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Client>>> GetClient()
    {
      if (_dbContext is null) return NotFound();
      if (_dbContext.Clients is null) return NotFound();

      var Client = await _dbContext.Clients.ToListAsync();
      return Ok(Client);
    }

    // GET: api/Client/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetClient(string id)
    {
      if (_dbContext is null) return NotFound();
      if (_dbContext.Clients is null) return NotFound();

      var Client = await _dbContext.Clients.FindAsync(id);

      if (Client == null)
      {
        return NotFound();
      }

      return Ok(Client);
    }

    // POST: api/Client
    [HttpPost]
    public async Task<ActionResult> PostClient(string name, string cpf)
    {
      if (_dbContext is null) return NotFound();
      if (_dbContext.Clients is null) return NotFound();

      var client = new Client()
      {
        Id=GetNewUuid(),
        Name=name,
        Cpf=cpf,
        Purchases_Quantity=0,
        Register_Date=DateTime.Now,
        Purchases=new List<Sale>(),
      };

      _dbContext.Clients.Add(client);
      await _dbContext.SaveChangesAsync();

      return CreatedAtAction("GetClient", new { id = client.Id }, client);
    }

    // PUT: api/Client/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutClient(string id, string name, string cpf)
    {
      var new_client = _dbContext.Clients.Find(id);

      if(new_client == null) return NotFound();

      new_client.Name = name;
      new_client.Cpf = cpf;
      
      try
      {
        await _dbContext.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        throw;
      }

      return CreatedAtAction("Update Client", new { id = new_client.Id }, new_client);
    }

    // DELETE: api/Client/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(string id)
    {
      if (_dbContext is null) return NotFound();
      if (_dbContext.Clients is null) return NotFound();

      var Client = await _dbContext.Clients.FindAsync(id);
      if (Client == null)
      {
        return NotFound();
      }

      _dbContext.Clients.Remove(Client);
      await _dbContext.SaveChangesAsync();

      return NoContent();
    }

    private bool ClientExists(string id)
    {
      return _dbContext.Clients!.Any(e => e.Id == id);
    }
  }
}