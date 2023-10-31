using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;

using APISale.Models;
using API.Controllers;
using API.Dto;

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
      var sellers = await _dbContext.Sellers
                  .Include(sell => sell.Sales)
                  .Select(sell => new {
                    sell.Id,
                    sell.Name,
                    sell.Cpf,
                    sell.Sales_Quantity,
                    Sales= sell.Sales!.Select(s => new
                    {
                      s.Id,
                      s.Sale_Date,
                      s.Value,

                      Seller = new {
                        s.Seller.Id,
                        s.Seller.Name,
                        s.Seller.Cpf,
                      },
                    })
                  })
                  .ToListAsync();

      return Ok(sellers);
    }

    // GET: api/Seller/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Seller>> GetSeller(string id)
    {
      var sell = await _dbContext.Sellers.FindAsync(id);
      if (sell == null)
      {
        return NotFound();
      }
      
      var seller = new
      {
        sell.Id,
        sell.Name,
        sell.Cpf,
        sell.Sales_Quantity,
        Sales= sell.Sales!.Select(s => new
        {
          s.Id,
          s.Sale_Date,
          s.Value,

          Seller = new {
            s.Seller.Id,
            s.Seller.Name,
            s.Seller.Cpf,
          },
        })
      };

      return Ok(seller);
    }

    // POST: api/Seller
    [HttpPost]
    public async Task<ActionResult> PostSeller(SellerDTO sellerDTO)
    {
      if (sellerDTO.Cpf == null || sellerDTO.Name == null ) return NotFound();

      var newSeller = new Seller()
      {
        Id=GetNewUuid(),
        Cpf=sellerDTO.Cpf,
        Name=sellerDTO.Name,
        Sales_Quantity=0
      };

      _dbContext.Sellers.Add(newSeller);
      await _dbContext.SaveChangesAsync();

      return CreatedAtAction("PostEvent", new { id = newSeller.Id }, newSeller);
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

    [HttpDelete("name/{name}")]
    public async Task<IActionResult> DeleteSellerBasedOnName(string name)
    {
      if (_dbContext is null) return NotFound();
      if (_dbContext.Sellers is null) return NotFound();

      var sellers = await _dbContext.Sellers.Where(e => e.Name == name).ToListAsync();
      if (sellers == null)
      {
        return NotFound();
      }

      _dbContext.Sellers.RemoveRange(sellers);
      await _dbContext.SaveChangesAsync();

      return NoContent();
    }

    private bool SellerExists(string id)
    {
      return _dbContext.Sellers!.Any(e => e.Id == id);
    }
  }
}