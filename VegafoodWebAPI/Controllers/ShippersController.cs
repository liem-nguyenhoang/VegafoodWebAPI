using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VegafoodWebAPI.Models;

namespace VegafoodWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippersController : ControllerBase
    {
        private readonly VegafoodContext _context;

        public ShippersController(VegafoodContext context)
        {
            _context = context;
        }

        // GET: api/Shippers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shippers>>> GetShippers()
        {
            return await _context.Shippers.ToListAsync();
        }

        // GET: api/Shippers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shippers>> GetShippers(int id)
        {
            var shippers = await _context.Shippers.FindAsync(id);

            if (shippers == null)
            {
                return NotFound();
            }

            return shippers;
        }

        // PUT: api/Shippers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShippers(int id, Shippers shippers)
        {
            if (id != shippers.ShipperId)
            {
                return BadRequest();
            }

            _context.Entry(shippers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShippersExists(id))
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

        // POST: api/Shippers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Shippers>> PostShippers(Shippers shippers)
        {
            _context.Shippers.Add(shippers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShippers", new { id = shippers.ShipperId }, shippers);
        }

        // DELETE: api/Shippers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Shippers>> DeleteShippers(int id)
        {
            var shippers = await _context.Shippers.FindAsync(id);
            if (shippers == null)
            {
                return NotFound();
            }

            _context.Shippers.Remove(shippers);
            await _context.SaveChangesAsync();

            return shippers;
        }

        private bool ShippersExists(int id)
        {
            return _context.Shippers.Any(e => e.ShipperId == id);
        }
    }
}
