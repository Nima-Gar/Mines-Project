using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities;
using MinesApi.Models;

namespace MinesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneNumbersController : ControllerBase
    {
        private readonly MinesDbContext _context;

        public PhoneNumbersController(MinesDbContext context)
        {
            _context = context;
        }

        // GET: api/PhoneNumbers/GetPhoneNumbers
        [HttpGet(nameof(GetPhoneNumbers))]
        public async Task<ActionResult<IEnumerable<PhoneNumber>>> GetPhoneNumbers()
        {
          if (_context.PhoneNumbers == null)
          {
              return NotFound();
          }
          return await _context.PhoneNumbers.ToListAsync();
        }

        // GET: api/PhoneNumbers/GetPhoneNumber?id=5
        [HttpGet(nameof(GetPhoneNumber))]
        public async Task<ActionResult<PhoneNumber>> GetPhoneNumber(int id)
        {
          if (_context.PhoneNumbers == null)
          {
              return NotFound();
          }
            var phoneNumber = await _context.PhoneNumbers.FindAsync(id);

            if (phoneNumber == null)
            {
                return NotFound();
            }

            return phoneNumber;
        }

        // PUT: api/PhoneNumbers/PutPhoneNumber?id=5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut(nameof(PutPhoneNumber))]
        public async Task<IActionResult> PutPhoneNumber(int id, PhoneNumber phoneNumber)
        {
            if (id != phoneNumber.Id)
            {
                return BadRequest();
            }

            _context.Entry(phoneNumber).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneNumberExists(id))
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

        // POST: api/PhoneNumbers/PostPhoneNumber
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost(nameof(PostPhoneNumber))]
        public async Task<ActionResult<PhoneNumber>> PostPhoneNumber(PhoneNumber phoneNumber)
        {
          if (_context.PhoneNumbers == null)
          {
              return Problem("Corresponding table does not exist in database");
          }
            _context.PhoneNumbers.Add(phoneNumber);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhoneNumber", new { id = phoneNumber.Id }, phoneNumber);
        }

        // DELETE: api/PhoneNumbers/DeletePhoneNumber?id=5
        [HttpDelete(nameof(DeletePhoneNumber))]
        public async Task<IActionResult> DeletePhoneNumber(int id)
        {
            if (_context.PhoneNumbers == null)
            {
                return NotFound();
            }
            var phoneNumber = await _context.PhoneNumbers.FindAsync(id);
            if (phoneNumber == null)
            {
                return NotFound();
            }

            _context.PhoneNumbers.Remove(phoneNumber);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhoneNumberExists(int id)
        {
            return (_context.PhoneNumbers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
