using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinesApi.Models;
using Entities;

namespace MinesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountiesController : ControllerBase
    {
        private readonly MinesDbContext _context;

        public CountiesController(MinesDbContext context)
        {
            _context = context;
        }

        // GET: api/Counties/GetCounties
        [HttpGet("GetCounties")]
        public async Task<ActionResult<IEnumerable<County>>> GetCounties()
        {
          if (_context.Counties == null)
          {
              return NotFound();
          }
            return await _context.Counties.ToListAsync();
        }

        // GET: api/Counties/GetCounty
        [HttpGet("GetCounty")]
        public async Task<ActionResult<County>> GetCounty(int countyId)
        {
          if (_context.Counties == null)
          {
              return NotFound();
          }
            var county = await _context.Counties.FindAsync(countyId);

            if (county == null)
            {
                return NotFound();
            }

            return county;
        }

        // PUT: api/Counties/PutCounty
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutCounty")]
        public async Task<IActionResult> PutCounty(int id, County county)
        {
            if (id != county.Id)
            {
                return BadRequest();
            }

            _context.Entry(county).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountyExists(id))
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

        // POST: api/Counties
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostCounty")]
        public async Task<ActionResult<County>> PostCounty(County county)
        {
          if (_context.Counties == null)
          {
              return Problem("Entity set 'MinesDbContext.Counties'  is null.");
          }
            _context.Counties.Add(county);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCounty", new { id = county.Id }, county);
        }

        // DELETE: api/Counties/5
        [HttpDelete("DeleteCounty")]
        public async Task<IActionResult> DeleteCounty(int id)
        {
            if (_context.Counties == null)
            {
                return NotFound();
            }
            var county = await _context.Counties.FindAsync(id);
            if (county == null)
            {
                return NotFound();
            }

            _context.Counties.Remove(county);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountyExists(int id)
        {
            return (_context.Counties?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
