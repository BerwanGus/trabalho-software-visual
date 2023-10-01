using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using APIStock.Models;
using API.Data;

namespace APIStock.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductController : ControllerBase
  {
    private readonly DBContext _dbContext;

    public ProductController(DBContext dbContext)
    {
      _dbContext = dbContext;
    }

    // GET: api/Product
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
    {
      if (_dbContext is null) return NotFound();
      if (_dbContext.Products is null) return NotFound();
      var product = await _dbContext.Products.ToListAsync();
      return Ok(product);
    }

    // GET: api/Product/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
      var product = await _dbContext.Products.FindAsync(id);

      if (product == null)
      {
        return NotFound();
      }

      return Ok(product);
    }

    // POST: api/Product
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
      _dbContext.Products.Add(product);
      await _dbContext.SaveChangesAsync();

      return CreatedAtAction("GetProduct", new { id = product.Id }, product);
    }

    // PUT: api/Product/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, Product product)
    {
      if (id != product.Id)
      {
        return BadRequest();
      }

      _dbContext.Entry(product).State = EntityState.Modified;

      try
      {
        await _dbContext.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ProductExists(id))
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

    // DELETE: api/Product/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
      var product = await _dbContext.Products.FindAsync(id);
      if (product == null)
      {
        return NotFound();
      }

      _dbContext.Products.Remove(product);
      await _dbContext.SaveChangesAsync();

      return NoContent();
    }

    private bool ProductExists(int id)
    {
      return _dbContext.Products.Any(e => e.Id == id);
    }
  }
}