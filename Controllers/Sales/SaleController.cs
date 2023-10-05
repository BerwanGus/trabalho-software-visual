using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using APISale.Models;
using API.Controllers;
using API.Dto;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace APISale.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SaleController : Back2youControllerBase
  {
    private readonly DBContext _dbContext;

    public SaleController(DBContext dbContext)
    {
      _dbContext = dbContext;
    }

    // GET: api/Sale

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Sale>>> GetSale()
    {
      if (_dbContext is null) return NotFound();
      if (_dbContext.Sales is null) return NotFound();

      var sales = await _dbContext.Sales
                        .Include(s => s.Client)
                        .Include(s => s.Event)
                        .Include(s => s.Seller)
                        .Select(s => new {
                          s.Id,
                          s.Sale_Date,
                          s.Value,

                          Seller = new {
                            s.Seller.Id,
                            s.Seller.Name,
                            s.Seller.Cpf,
                          },
                          Client = new {
                            s.Client.Id,
                            s.Client.Name,
                            s.Client.Cpf,
                          },
                          Event = new {
                            s.Event.Id,
                            s.Event.Name,
                            s.Event.Event_Date,
                            s.Event.Sales_Quantity
                          }
                        })
                        .ToListAsync();

      return Ok(sales);
    }

    // GET: api/Sale/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Sale>> GetSale(string id)
    {
      if (_dbContext is null) return NotFound();
      if (_dbContext.Sales is null) return NotFound();

      var Sale = await _dbContext.Sales.FindAsync(id);

      if (Sale == null)
      {
        return NotFound();
      }

      return Ok(Sale);
    }

    // POST: api/Sale
    [HttpPost]
    public async Task<ActionResult<Sale>> PostSale(SaleDTO sale)
    {
      if(sale.Seller_Id == null || sale.Sale_Date == null) return NotFound();

      var newSale = new Sale()
      {
        Id=GetNewUuid(),
        Value=sale.Value,
        Client_Id=sale.Client_Id,
        Event_Id=sale.Event_Id,
        Seller_Id=sale.Seller_Id
      };

      _dbContext.Sales.Add(newSale);
      await _dbContext.SaveChangesAsync();

      return CreatedAtAction("PostSale", new { id = newSale.Id }, newSale);
    }

    // PUT: api/Sale/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSale(string id, Sale sale)
    {
      if (id != sale.Id)
      {
        return BadRequest();
      }

      _dbContext.Entry(sale).State = EntityState.Modified;

      try
      {
        await _dbContext.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!SaleExists(id))
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

    // DELETE: api/Sale/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSale(string id)
    {
      if (_dbContext is null) return NotFound();
      if (_dbContext.Sales is null) return NotFound();

      var sale = await _dbContext.Sales.FindAsync(id);
      if (sale == null)
      {
        return NotFound();
      }

      _dbContext.Sales.Remove(sale);
      await _dbContext.SaveChangesAsync();

      return NoContent();
    }

    private bool SaleExists(string id)
    {
      return _dbContext.Sales!.Any(e => e.Id == id);
    }
  }
}