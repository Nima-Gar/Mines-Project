using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities;
using MinesApi.Models;

namespace MinesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvincesController : ControllerBase
    {
        private readonly MinesDbContext _context;

        public ProvincesController(MinesDbContext context)
        {
            _context = context;
        }

        // GET: api/Provinces/GetProvinces
        [HttpGet("GetProvinces")]
        public async Task<ActionResult<IEnumerable<Province>>> GetProvinces()
        {
          if (_context.Provinces == null)
          {
              return NotFound();
          }
            return await _context.Provinces.ToListAsync();
        }

        // GET: api/Provinces/GetProvince
        [HttpGet("GetProvince")]
        public async Task<ActionResult<Province>> GetProvince(int id)
        {
          if (_context.Provinces == null)
          {
              return NotFound();
          }
            var province = await _context.Provinces.FindAsync(id);

            if (province == null)
            {
                return NotFound();
            }

            return province;
        }

        // PUT: api/Provinces/PutProvince
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutProvince")]
        public async Task<IActionResult> PutProvince(int id, Province province)
        {
            if (id != province.Id)
            {
                return BadRequest();
            }

            _context.Entry(province).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProvinceExists(id))
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

        // POST: api/Provinces/PostProvince
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostProvince")]
        public async Task<ActionResult<Province>> PostProvince(Province province)
        {
          if (_context.Provinces == null)
          {
              return Problem("Entity set 'MinesDbContext.Provinces'  is null.");
          }
            _context.Provinces.Add(province);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProvince", new { id = province.Id }, province);
        }

        // DELETE: api/Provinces/DeleteProvince
        [HttpDelete("DeleteProvince")]
        public async Task<IActionResult> DeleteProvince(int id)
        {
            if (_context.Provinces == null)
            {
                return NotFound();
            }
            var province = await _context.Provinces.FindAsync(id);
            if (province == null)
            {
                return NotFound();
            }

            _context.Provinces.Remove(province);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProvinceExists(int id)
        {
            return (_context.Provinces?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
