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
using API.Models;

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
                        .Include(s => s.ProductSales)
                        .ThenInclude(s => s.Product)
                        .ThenInclude(s => s.Brand)
                        .Include(s => s.ProductSales)
                        .ThenInclude(s => s.Product)
                        .ThenInclude(s => s.ProductType)
                        .Select(s => FixSaleJSON(s))
                        .ToListAsync();

      return Ok(sales);
    }

    // GET: api/Sale/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Sale>> GetSale(string id)
    {
      var idSale = await _dbContext.Sales
                  .Include(s => s.Client)
                  .Include(s => s.Event)
                  .Include(s => s.Seller)
                  .Include(s => s.ProductSales)
                  .FirstAsync(s => s.Id == id);

      if (idSale == null)
      {
        return NotFound();
      }

      var sale = FixSaleJSON(idSale);

      return Ok(sale);
    }

    // POST: api/Sale
    [HttpPost]
    public async Task<ActionResult<Sale>> PostSale(SaleDTO sale)
    {
      if(sale.Seller_Id == null || sale.Sale_Date == null || sale.ProductSaleDTOs == null) return NotFound();

      var newSale = new Sale()
      {
        Id=GetNewUuid(),
        Value=sale.Value,
        Client_Id=sale.Client_Id,
        Event_Id=sale.Event_Id,
        Seller_Id=sale.Seller_Id,
        Sale_Date=sale.Sale_Date
      };
      

      var newProductSales = new List<ProductSale>();

      foreach (var productSale in sale.ProductSaleDTOs)
      {
        newProductSales.Add(new ProductSale()
        {
          Id=GetNewUuid(),
          ProductId=productSale.ProductID,
          SaleId=newSale.Id,
          ProductQuantity=productSale.ProductSalesQuantity
        });
      }

      _dbContext.ProductsSales.AddRange(newProductSales);
      _dbContext.Sales.Add(newSale);
      await _dbContext.SaveChangesAsync();

      return CreatedAtAction("PostSale", new { id = newSale.Id }, newSale);
    }

    // PUT: api/Sale/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSale(string id, SaleDTO sale)
    {
      if(sale.Seller_Id == null || sale.Sale_Date == null || sale.ProductSaleDTOs == null) return NotFound();

      var oldSale = await _dbContext.Sales.FindAsync(id);

      if (oldSale == null) return NotFound();

      oldSale.Value = sale.Value;
      oldSale.Sale_Date = sale.Sale_Date;

      if (sale.Client_Id != null) oldSale.Client_Id = sale.Client_Id;
      if (sale.Seller_Id != null) oldSale.Seller_Id = sale.Seller_Id;
      if (sale.Event_Id != null) oldSale.Event_Id = sale.Event_Id;

      _dbContext.ProductsSales.RemoveRange(_dbContext.ProductsSales.Where(ps => ps.SaleId == id));

      
      var updatedProductSales = new List<ProductSale>();

      foreach (var productSale in sale.ProductSaleDTOs)
      {
        updatedProductSales.Add(new ProductSale()
        {
          Id=GetNewUuid(),
          ProductId=productSale.ProductID,
          SaleId=id,
          ProductQuantity=productSale.ProductSalesQuantity
        });
      }

      _dbContext.ProductsSales.AddRange(updatedProductSales);
      await _dbContext.SaveChangesAsync();

      return CreatedAtAction("UpdateSale", new { id = oldSale.Id }, oldSale);
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

    private static object FixSaleJSON(Sale sale)
    {
      var fixedSale = new 
      {
        sale.Id,
        sale.Sale_Date,
        sale.Value,
        profit=sale.Value - sale.ProductSales?.Sum(ps => ps.Product?.Cost * ps.ProductQuantity),

        Client = sale.Client != null ? new {
          sale.Client.Id,
          sale.Client.Name,
          sale.Client.Cpf,
          sale.Client.Purchases_Quantity
        }: null,

        Seller = sale.Seller != null ? new {
          sale.Seller.Id,
          sale.Seller.Name,
          sale.Seller.Cpf,
          sale.Seller.Sales_Quantity
        }: null,

        Event = sale.Event != null ? new {
          sale.Event.Id,
          sale.Event.Name,
          sale.Event.Event_Date,
          sale.Event.Sales_Quantity
        } : null,

        Products = sale.ProductSales?.Select(ps => new
        {
          ps.Product?.Id,
          ps.Product?.Size,
          ps.Product?.Cost,
          ps.Product?.Price,
          brandName=ps.Product.Brand.Name,
          productType=ps.Product.ProductType.TypeName + " " + ps.Product.ProductType.Style,
          ps.ProductQuantity,
          productProfit=ps.Product?.Price - ps.Product?.Cost
        })
      };

      return fixedSale;
    }
  }
}