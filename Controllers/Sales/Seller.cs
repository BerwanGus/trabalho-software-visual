using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;

using APISale.Models;
using API.Controllers;

namespace APISale.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SellerController : Back2youControllerBase
  {
    private readonly DBContext _dbContext;

    public SellerController(DBContext dbContext)
    {
      _dbContext = dbContext;
    }

    // GET: api/Seller

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Seller>>> GetSeller()
    {
      if (_dbContext is null) return NotFound();
      if (_dbContext.Sellers is null) return NotFound();

      var Seller = await _dbContext.Sellers.ToListAsync();
      return Ok(Seller);
    }

    // GET: api/Seller/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Seller>> GetSeller(string id)
    {
      if (_dbContext is null) return NotFound();
      if (_dbContext.Sellers is null) return NotFound();

      var Seller = await _dbContext.Sellers.FindAsync(id);

      if (Seller == null)
      {
        return NotFound();
      }

      return Ok(Seller);
    }

    // POST: api/Seller
    [HttpPost]
    public async Task<ActionResult<Seller>> PostSeller(Seller Seller)
    {
      if (_dbContext is null) return NotFound();
      if (_dbContext.Sellers is null) return NotFound();

      _dbContext.Sellers.Add(Seller);
      await _dbContext.SaveChangesAsync();

      return CreatedAtAction("GetSeller", new { id = Seller.Id }, Seller);
    }

    // PUT: api/Seller/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSeller(string id, Seller Seller)
    {
      if (id != Seller.Id)
      {
        return BadRequest();
      }

      _dbContext.Entry(Seller).State = EntityState.Modified;

      try
      {
        await _dbContext.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!SellerExists(id))
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

    // DELETE: api/Seller/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSeller(string id)
    {
      if (_dbContext is null) return NotFound();
      if (_dbContext.Sellers is null) return NotFound();

      var Seller = await _dbContext.Sellers.FindAsync(id);
      if (Seller == null)
      {
        return NotFound();
      }

      _dbContext.Sellers.Remove(Seller);
      await _dbContext.SaveChangesAsync();

      return NoContent();
    }

    private bool SellerExists(string id)
    {
      return _dbContext.Sellers!.Any(e => e.Id == id);
    }
  }
}