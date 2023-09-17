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
    public class PhoneNumTypesController : ControllerBase
    {
        private readonly MinesDbContext _context;

        public PhoneNumTypesController(MinesDbContext context)
        {
            _context = context;
        }

        // GET: api/PhoneNumTypes
        [HttpGet("GetPhoneNumTypes")]
        public async Task<ActionResult<IEnumerable<PhoneNumType>>> GetPhoneNumTypes()
        {
          if (_context.PhoneNumTypes == null)
          {
              return NotFound();
          }
            return await _context.PhoneNumTypes.ToListAsync();
        }

        // GET: api/PhoneNumTypes/5
        [HttpGet("GetPhoneNumType")]
        public async Task<ActionResult<PhoneNumType>> GetPhoneNumType(int id)
        {
          if (_context.PhoneNumTypes == null)
          {
              return NotFound();
          }
            var phoneNumType = await _context.PhoneNumTypes.FindAsync(id);

            if (phoneNumType == null)
            {
                return NotFound();
            }

            return phoneNumType;
        }

        // PUT: api/PhoneNumTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("PutPhoneNumType")]
        public async Task<IActionResult> PutPhoneNumType(int id, PhoneNumType phoneNumType)
        {
            if (id != phoneNumType.Id)
            {
                return BadRequest();
            }

            _context.Entry(phoneNumType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneNumTypeExists(id))
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

        // POST: api/PhoneNumTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("PostPhoneNumType")]
        public async Task<ActionResult<PhoneNumType>> PostPhoneNumType(PhoneNumType phoneNumType)
        {
          if (_context.PhoneNumTypes == null)
          {
              return Problem("Entity set 'MinesDbContext.PhoneNumTypes'  is null.");
          }
            _context.PhoneNumTypes.Add(phoneNumType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhoneNumType", new { id = phoneNumType.Id }, phoneNumType);
        }

        // DELETE: api/PhoneNumTypes/5
        [HttpDelete("DeletePhoneNumType")]
        public async Task<IActionResult> DeletePhoneNumType(int id)
        {
            if (_context.PhoneNumTypes == null)
            {
                return NotFound();
            }
            var phoneNumType = await _context.PhoneNumTypes.FindAsync(id);
            if (phoneNumType == null)
            {
                return NotFound();
            }

            _context.PhoneNumTypes.Remove(phoneNumType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhoneNumTypeExists(int id)
        {
            return (_context.PhoneNumTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
