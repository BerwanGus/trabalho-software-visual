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
  public class EventController : Back2youControllerBase
  {
    private readonly DBContext _dbContext;

    public EventController(DBContext dbContext)
    {
      _dbContext = dbContext;
    }

    // GET: api/Event
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Event>>> GetEvent()
    {
      var events = await _dbContext.Events
                  .Include(ev => ev.Sales)
                  .Select(ev => new {
                    ev.Id,
                    ev.Name,
                    ev.Event_Date,
                    ev.Sales_Quantity,
                    Sales= ev.Sales!.Select(s => new
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

      return Ok(events);
    }

    // GET: api/Event/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetEventId(string id)
    {
      var ev = await _dbContext.Events.FindAsync(id);
      if (ev == null)
      {
        return NotFound();
      }
      
      var e = new
      {
        ev.Id,
        ev.Name,
        ev.Event_Date,
        ev.Sales_Quantity,
        Sales= ev.Sales!.Select(s => new
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

      return Ok(e);
    }

    // POST: api/Event
    [HttpPost]
    public async Task<ActionResult<Event>> PostEvent(EventDTO eventDTO)
    {
      if (eventDTO.Event_Date == null || eventDTO.Name == null ) return NotFound();

      var newEvent = new Event()
      {
        Id=GetNewUuid(),
        Name=eventDTO.Name,
        Sales_Quantity=eventDTO.Sales_Quantity
      };

      _dbContext.Events.Add(newEvent);
      await _dbContext.SaveChangesAsync();

      return CreatedAtAction("PostEvent", new { id = newEvent.Id }, newEvent);
    }

    // PUT: api/Event/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutEvent(string id, Event Event)
    {
      if (id != Event.Id)
      {
        return BadRequest();
      }

      _dbContext.Entry(Event).State = EntityState.Modified;

      try
      {
        await _dbContext.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!EventExists(id))
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

    // DELETE: api/Event/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEvent(string id)
    {
      if (_dbContext is null) return NotFound();
      if (_dbContext.Events is null) return NotFound();

      var Event = await _dbContext.Events.FindAsync(id);
      if (Event == null)
      {
        return NotFound();
      }

      _dbContext.Events.Remove(Event);
      await _dbContext.SaveChangesAsync();

      return NoContent();
    }

    private bool EventExists(string id)
    {
      return _dbContext.Events!.Any(e => e.Id == id);
    }
  }
}