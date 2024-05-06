using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using viveiro_back.Data;
using viveiro_back.Models;

namespace viveiro_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly viveiro_backContext _context;

        public usersController(viveiro_backContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<users>>> Getusers()
        {
          if (_context.users == null)
          {
              return NotFound();
          }
            return await _context.users.ToListAsync();
        }

        // GET: api/users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<users>> Getusers(int id)
        {
          if (_context.users == null)
          {
              return NotFound();
          }
            var users = await _context.users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // PUT: api/users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putusers(int id, users users)
        {
            if (id != users.id)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!usersExists(id))
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

        // POST: api/users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<users>> Postusers(users users)
        {
          if (_context.users == null)
          {
              return Problem("Entity set 'viveiro_backContext.users'  is null.");
          }
            _context.users.Add(users);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getusers", new { id = users.id }, users);
        }

        // DELETE: api/users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteusers(int id)
        {
            if (_context.users == null)
            {
                return NotFound();
            }
            var users = await _context.users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.users.Remove(users);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool usersExists(int id)
        {
            return (_context.users?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
