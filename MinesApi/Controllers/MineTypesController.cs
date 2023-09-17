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
    public class MineTypesController : ControllerBase
    {
        private readonly MinesDbContext _context;

        public MineTypesController(MinesDbContext context)
        {
            _context = context;
        }

        // GET: api/MineTypes
        [HttpGet("GetMineTypes")]
        public async Task<ActionResult<IEnumerable<MineType>>> GetMineTypes()
        {
          if (_context.MineTypes == null)
          {
              return NotFound();
          }
            return await _context.MineTypes.ToListAsync();
        }

        // GET: api/MineTypes/5
        [HttpGet("GetMineType")]
        public async Task<ActionResult<MineType>> GetMineType(int id)
        {
          if (_context.MineTypes == null)
          {
              return NotFound();
          }
            var mineType = await _context.MineTypes.FindAsync(id);

            if (mineType == null)
            {
                return NotFound();
            }

            return mineType;
        }

        // PUT: api/MineTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutMineType")]
        public async Task<IActionResult> PutMineType(int id, MineType mineType)
        {
            if (id != mineType.Id)
            {
                return BadRequest();
            }

            _context.Entry(mineType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MineTypeExists(id))
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

        // POST: api/MineTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostMineType")]
        public async Task<ActionResult<MineType>> PostMineType(MineType mineType)
        {
          if (_context.MineTypes == null)
          {
              return Problem("Entity set 'MinesDbContext.MineTypes'  is null.");
          }
            _context.MineTypes.Add(mineType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMineType", new { id = mineType.Id }, mineType);
        }

        // DELETE: api/MineTypes/5
        [HttpDelete("DeleteMineType")]
        public async Task<IActionResult> DeleteMineType(int id)
        {
            if (_context.MineTypes == null)
            {
                return NotFound();
            }
            var mineType = await _context.MineTypes.FindAsync(id);
            if (mineType == null)
            {
                return NotFound();
            }

            _context.MineTypes.Remove(mineType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MineTypeExists(int id)
        {
            return (_context.MineTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
