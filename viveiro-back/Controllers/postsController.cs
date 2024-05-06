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
    public class postsController : ControllerBase
    {
        private readonly viveiro_backContext _context;

        public postsController(viveiro_backContext context)
        {
            _context = context;
        }

        // GET: api/posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<post>>> Getpost()
        {
          if (_context.post == null)
          {
              return NotFound();
          }
            return await _context.post.ToListAsync();
        }

        // GET: api/posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<post>> Getpost(int id)
        {
          if (_context.post == null)
          {
              return NotFound();
          }
            var post = await _context.post.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // PUT: api/posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putpost(int id, post post)
        {
            if (id != post.id)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!postExists(id))
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

        // POST: api/posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<post>> Postpost(post post)
        {
          if (_context.post == null)
          {
              return Problem("Entity set 'viveiro_backContext.post'  is null.");
          }
            _context.post.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getpost", new { id = post.id }, post);
        }

        // DELETE: api/posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletepost(int id)
        {
            if (_context.post == null)
            {
                return NotFound();
            }
            var post = await _context.post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.post.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool postExists(int id)
        {
            return (_context.post?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
