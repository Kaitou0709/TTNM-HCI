using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIcheck.Data;
using WebAPIcheck.Migrations;
using WebAPIcheck.Models;

namespace WebAPIcheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public SubjectsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Subjects
        [HttpGet]
        public async Task<ActionResult> GetSubjects()
        {
          if (_context.Subjects == null)
          {
              return NotFound();
          }
            var Subject = await (from s in _context.Subjects
                                 join f in _context.Faculties on s.IdFaclty equals f.IdFaculty
                                 select new
                                 {
                                     IdSubject = s.IdSubject,
                                     Name  = s.Name,
                                     Fee = s.Fee,
                                     IdFaculty  = f.IdFaculty,
                                     NameFacullty = f.Name,
                                 }).ToListAsync();
            return Ok(Subject);
        }

        // GET: api/Subjects/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetSubjects(int id)
        {
          if (_context.Subjects == null)
          {
              return NotFound();
          }
            var Subject = await (from s in _context.Subjects
                                 join f in _context.Faculties on s.IdFaclty equals f.IdFaculty
                                 where s.IdSubject == id
                                 select new
                                 {
                                     IdSubject = s.IdSubject,
                                     Name = s.Name,
                                     Fee = s.Fee,
                                     IdFaculty = f.IdFaculty,
                                     NameFacullty = f.Name,
                                 }).ToListAsync();
            if (Subject.Count == 0)
            {
                return NotFound();
            }
            return Ok(Subject);
        }

        // PUT: api/Subjects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubjects(int id, SubjectModel subjects)
        {
            if (id != subjects.IdSubject)
            {
                return BadRequest();
            }
            var  Faclty = await _context.Subjects.FindAsync(subjects.IdFaclty);
            if (Faclty == null)
            {
                return Ok(value: "Khong dung ma khoa");
            }
            var subject = new Data.Subjects
            {
                IdSubject = subjects.IdSubject,
                Name = subjects.Name,
                Fee = subjects.Fee,
                IdFaclty = subjects.IdFaclty,
            };
            _context.Entry(subject).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubjectsExists(id))
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

        // POST: api/Subjects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Data.Subjects>> PostSubjects(SubjectModel subjects)
        {
          if (_context.Subjects == null)
          {
              return Problem("Entity set 'MyDbContext.Subjects'  is null.");
          }
            var subject = new Data.Subjects
            {
                Name = subjects.Name,
                Fee = subjects.Fee,
                IdFaclty = subjects.IdFaclty,
            };
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubjects", new { id = subjects.IdSubject }, subjects);
        }

        // DELETE: api/Subjects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubjects(int id)
        {
            if (_context.Subjects == null)
            {
                return NotFound();
            }
            var subjects = await _context.Subjects.FindAsync(id);
            if (subjects == null)
            {
                return NotFound();
            }

            _context.Subjects.Remove(subjects);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SubjectsExists(int id)
        {
            return (_context.Subjects?.Any(e => e.IdSubject == id)).GetValueOrDefault();
        }
    }
}
