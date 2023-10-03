using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;

using APISale.Models;
using API.Controllers;

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
      if (_dbContext is null) return NotFound();
      if (_dbContext.Events is null) return NotFound();

      var Event = await _dbContext.Events.ToListAsync();
      return Ok(Event);
    }

    // GET: api/Event/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Event>> GetEvent(string id)
    {
      if (_dbContext is null) return NotFound();
      if (_dbContext.Events is null) return NotFound();

      var Event = await _dbContext.Events.FindAsync(id);

      if (Event == null)
      {
        return NotFound();
      }

      return Ok(Event);
    }

    // POST: api/Event
    [HttpPost]
    public async Task<ActionResult<Event>> PostEvent(Event Event)
    {
      if (_dbContext is null) return NotFound();
      if (_dbContext.Events is null) return NotFound();

      _dbContext.Events.Add(Event);
      await _dbContext.SaveChangesAsync();

      return CreatedAtAction("GetEvent", new { id = Event.Id }, Event);
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