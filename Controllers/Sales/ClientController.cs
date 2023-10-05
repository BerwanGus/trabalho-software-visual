using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;

using APISale.Models;
using API.Controllers;
using API.Dto;
using Microsoft.VisualBasic;
using Microsoft.OpenApi.Any;

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
      var client = await _dbContext.Clients
                  .Include(c => c.Purchases)
                  .Select(c => FixClientJSON(c))
                  .ToListAsync();

      return Ok(client);
    }

    // GET: api/Client/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Client>> GetClient(string id)
    {
      var c = await _dbContext.Clients.FindAsync(id);
      if (c == null)
      {
        return NotFound();
      }

      var client = FixClientJSON(c);

      return Ok(client);
    }

    // POST: api/Client
    [HttpPost]
    public async Task<ActionResult> PostClient(ClientDTO clientDto)
    {
      if (clientDto.Cpf == null || clientDto.Name == null ) return NotFound();

      var client = new Client()
      {
        Id=GetNewUuid(),
        Name=clientDto.Name,
        Cpf=clientDto.Cpf,
        Register_Date=DateTime.Now,
        Purchases_Quantity=0
      };

      _dbContext.Clients.Add(client);
      await _dbContext.SaveChangesAsync();

      return CreatedAtAction("GetClient", new { id = client.Id }, client);
    }

    // --------------------> CHATGPT 
    // PUT: api/Client/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutClient(string id, ClientDTO clientDto)
    {
      var client = await _dbContext.Clients.FindAsync(id);

      if (client == null)
      {
          return NotFound();
      }

      if (clientDto.Name != null)
      {
          client.Name = clientDto.Name;
      }

      if (clientDto.Cpf != null)
      {
          client.Cpf = clientDto.Cpf;
      }

      try
      {
          _dbContext.Update(client);
          await _dbContext.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
          if (!ClientExists(id))
          {
              return NotFound();
          }
          else
          {
              throw;
          }
      }

      return NoContent();
    }

    // DELETE: api/Client/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(string id)
    {
      var client = await _dbContext.Clients.FindAsync(id);
      if (client == null)
      {
        return NotFound();
      }

      var client_purchases = (from sales in _dbContext.Sales
                              where sales.Client_Id == id
                              select sales).ToList();

      foreach (var c in client_purchases)
      {
        c.Client_Id = null;
      } 

      _dbContext.Clients.Remove(client);
      await _dbContext.SaveChangesAsync();

      return NoContent();
    }

    private bool ClientExists(string id)
    {
      return _dbContext.Clients!.Any(e => e.Id == id);
    }
  
    private static object FixClientJSON(Client client)
    {
      var fixedClient = new
      {
        client.Id,
        client.Name,
        client.Cpf,
        client.Register_Date,
        client.Purchases_Quantity,
        Purchases = client.Purchases?.Select(s => new
        {
          s.Id,
          s.Sale_Date,
          s.Value,
          
          s.Seller_Id,
          s.Event_Id,
        })
      };

      return fixedClient;
    }
  }
}