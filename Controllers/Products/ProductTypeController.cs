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
  public class ProductTypeController : Back2youControllerBase
  {
    private readonly DBContext _dbContext;

    public ProductTypeController(DBContext dbContext)
    {
      _dbContext = dbContext;
    }

    // GET: api/ProductType
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductType>>> GetProductType()
    {
      var productType = await _dbContext.ProductTypes
                  //.Include(c => c.Products)
                  .Select(c => new
                  {
                    c.Id,
                    c.TypeName,
                    c.Style
                    /*Products = c.Products!.Select(s => new
                    {
                      s.Id,
                      
                    })*/
                  })
                  .ToListAsync();

      return Ok(productType);
    }

    // GET: api/ProductType/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductType>> GetProductType(string id)
    {
      var c = await _dbContext.ProductTypes.FindAsync(id);
      if (c == null)
      {
        return NotFound();
      }

      var productType = new
      {
        c.Id,
        c.TypeName,
        c.Style

        /*Purchases = c.Purchases!.Select(s => new
        {
          s.Id,
          s.Sale_Date,
          s.Value,

          Seller = new
          {
            s.Seller.Id,
            s.Seller.TypeName,
            s.Seller.Cpf,
          },
          Brand = new
          {
            s.Brand.Id,
            s.Brand.TypeName,
            s.Brand.Cpf,
          },
          Event = new
          {
            s.Event.Id,
            s.Event.TypeName,
            s.Event.Event_Date,
            s.Event.Sales_Quantity
          }
        })*/
      };

      return Ok(productType);
    }

    // POST: api/Client
    [HttpPost]
    public async Task<ActionResult> PostClient(ProductTypeDTO productTypeDto)
    {
      if (productTypeDto.TypeName == null) return NotFound();
      if (productTypeDto.Style == null) return NotFound();

      var productType = new ProductType()
      {
        Id = GetNewUuid(),
        TypeName = productTypeDto.TypeName,
        Style = productTypeDto.Style,
      };

      _dbContext.ProductTypes.Add(productType);
      await _dbContext.SaveChangesAsync();

      return CreatedAtAction("GetProductType", new { id = productType.Id }, productType);
    }

    // --------------------> CHATGPT 
    // PUT: api/ProductType/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProductType(string id, ProductTypeDTO productTypeDto)
    {
      var productType = await _dbContext.ProductTypes.FindAsync(id);

      if (productType == null)
      {
        return NotFound();
      }

      if (productTypeDto.TypeName != null)
      {
        productType.TypeName = productTypeDto.TypeName;
        productType.Style = productTypeDto.Style;
      }

      try
      {
        _dbContext.Update(productType);
        await _dbContext.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ProductTypeExists(id))
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

    // DELETE: api/ProductType/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProductType(string id)
    {
      var productType = await _dbContext.ProductTypes.FindAsync(id);
      if (productType == null)
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

      _dbContext.ProductTypes.Remove(productType);
      await _dbContext.SaveChangesAsync();

      return NoContent();
    }

    private bool ProductTypeExists(string id)
    {
      return _dbContext.ProductTypes!.Any(e => e.Id == id);
    }
  }
}