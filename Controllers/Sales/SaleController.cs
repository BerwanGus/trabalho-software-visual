using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;

using APISale.Models;
using API.Controllers;

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

      var Sale = await _dbContext.Sales.ToListAsync();
      return Ok(Sale);
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
    public async Task<ActionResult<Sale>> PostSale(Sale sale)
    {
      if (_dbContext is null) return NotFound();
      if (_dbContext.Sales is null) return NotFound();

      _dbContext.Sales.Add(sale);
      await _dbContext.SaveChangesAsync();

      return CreatedAtAction("GetSale", new { id = sale.Id }, sale);
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