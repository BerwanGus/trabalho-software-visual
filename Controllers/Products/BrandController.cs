using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using APIStock.Models;
using APISale.Models;
using API.Controllers;
using API.Dto;
using Microsoft.VisualBasic;
using Microsoft.OpenApi.Any;

namespace APIStock.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class BrandController : Back2youControllerBase
  {
    private readonly DBContext _dbContext;

    public BrandController(DBContext dbContext)
    {
      _dbContext = dbContext;
    }

    // GET: api/Brand
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Brand>>> GetBrand()
    {
      var brand = await _dbContext.Brands
                  //.Include(c => c.Products)
                  .Select(c => new
                  {
                    c.Id,
                    c.Name,
                    /*Products = c.Products!.Select(s => new
                    {
                      s.Id,
                      
                    })*/
                  })
                  .ToListAsync();

      return Ok(brand);
    }

    // GET: api/Client/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Brand>> GetBrand(string id)
    {
      var c = await _dbContext.Brands.FindAsync(id);
      if (c == null)
      {
        return NotFound();
      }

      var brand = new
      {
        c.Id,
        c.Name,

        /*Purchases = c.Purchases!.Select(s => new
        {
          s.Id,
          s.Sale_Date,
          s.Value,

          Seller = new
          {
            s.Seller.Id,
            s.Seller.Name,
            s.Seller.Cpf,
          },
          Brand = new
          {
            s.Brand.Id,
            s.Brand.Name,
            s.Brand.Cpf,
          },
          Event = new
          {
            s.Event.Id,
            s.Event.Name,
            s.Event.Event_Date,
            s.Event.Sales_Quantity
          }
        })*/
      };

      return Ok(brand);
    }

    // POST: api/Client
    [HttpPost]
    public async Task<ActionResult> PostClient(BrandDTO brandDto)
    {
      if (brandDto.Name == null) return NotFound();

      var brand = new Brand()
      {
        Id = GetNewUuid(),
        Name = brandDto.Name,
      };

      _dbContext.Brands.Add(brand);
      await _dbContext.SaveChangesAsync();

      return CreatedAtAction("GetBrand", new { id = brand.Id }, brand);
    }

    // --------------------> CHATGPT 
    // PUT: api/Brand/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBrand(string id, BrandDTO brandDto)
    {
      var brand = await _dbContext.Brands.FindAsync(id);

      if (brand == null)
      {
        return NotFound();
      }

      if (brandDto.Name != null)
      {
        brand.Name = brandDto.Name;
      }

      try
      {
        _dbContext.Update(brand);
        await _dbContext.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!BrandExists(id))
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

    // DELETE: api/Brand/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBrand(string id)
    {
      var brand = await _dbContext.Brands.FindAsync(id);
      if (brand == null)
      {
        return NotFound();
      }

      /*var brand_purchases = (from sales in _dbContext.Sales
                             where sales.Brand_Id == id
                             select sales).ToList();

      foreach (var c in brand_purchases)
      {
        c.Brand_Id = null;
      }*/

      _dbContext.Brands.Remove(brand);
      await _dbContext.SaveChangesAsync();

      return NoContent();
    }

    private bool BrandExists(string id)
    {
      return _dbContext.Brands!.Any(e => e.Id == id);
    }
  }
}