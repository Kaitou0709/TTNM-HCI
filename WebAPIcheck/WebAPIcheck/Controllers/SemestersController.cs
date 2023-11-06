using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIcheck.Data;

namespace WebAPIcheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemestersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public SemestersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Semesters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Semesters>>> GetSemesters()
        {
          if (_context.Semesters == null)
          {
              return NotFound();
          }
            return await _context.Semesters.ToListAsync();
        }

        // GET: api/Semesters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Semesters>> GetSemesters(int id)
        {
          if (_context.Semesters == null)
          {
              return NotFound();
          }
            var semesters = await _context.Semesters.FindAsync(id);

            if (semesters == null)
            {
                return NotFound();
            }

            return semesters;
        }

        // PUT: api/Semesters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSemesters(int id, Semesters semesters)
        {
            if (id != semesters.IdSemester)
            {
                return BadRequest();
            }

            _context.Entry(semesters).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SemestersExists(id))
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

        // POST: api/Semesters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Semesters>> PostSemesters(Semesters semesters)
        {
          if (_context.Semesters == null)
          {
              return Problem("Entity set 'MyDbContext.Semesters'  is null.");
          }
            _context.Semesters.Add(semesters);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSemesters", new { id = semesters.IdSemester }, semesters);
        }

        // DELETE: api/Semesters/5
        

        private bool SemestersExists(int id)
        {
            return (_context.Semesters?.Any(e => e.IdSemester == id)).GetValueOrDefault();
        }
    }
}
