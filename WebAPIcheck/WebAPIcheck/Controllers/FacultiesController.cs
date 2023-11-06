using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using WebAPIcheck.Data;

namespace WebAPIcheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultiesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public FacultiesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Faculties
        [HttpGet]
        public async Task<ActionResult> GetFaculties()
        {
            if (_context.Faculties == null)
            {
                return NotFound();
            }
            var listTeacher = await (from f in _context.Faculties
                                 select new
                                 {
                                     IdFaculty = f.IdFaculty,
                                     Name = f.Name,
                                     List = _context.Teachers.Where(t=> t.IdFaculty == f.IdFaculty).Select(t => new 
                                     {t.IdTeacher,
                                     t.NameTeacher,
                                     t.Position,
                                     }).ToList()
                                 }).ToListAsync();
            return Ok(listTeacher);
        }

        // GET: api/Faculties/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetFaculties(int id)
        {
            if (_context.Faculties == null)
            {
                return NotFound();
            }
            var faculties = await _context.Faculties.FindAsync(id);

            if (faculties == null)
            {
                return NotFound();
            }

            
            var listTeacher = await (from f in _context.Faculties
                                     where f.IdFaculty == id
                                     select new
                                     {
                                         idFaculty = f.IdFaculty,
                                         name = f.Name,
                                         list = _context.Teachers.Where(t => t.IdFaculty == f.IdFaculty).Select(t => new
                                         {
                                             t.IdTeacher,
                                             t.NameTeacher,
                                             t.Position,
                                         }).ToList()
                                     }).FirstAsync();
            return Ok(listTeacher);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> AddFaculties(int id, Faculties faculties)
        {
            if (id != faculties.IdFaculty)
            {
                return BadRequest();
            }

            _context.Entry(faculties).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FacultiesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(value:"Sua thanh cong");
        }
        [HttpPost]
        public async Task<ActionResult<Faculties>> PostFaculties(Faculties faculties)
        {
          if (_context.Faculties == null)
          {
              return Problem("Entity set 'MyDbContext.Faculties'  is null.");
          }
            _context.Faculties.Add(faculties);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFaculties", new { id = faculties.IdFaculty }, faculties);
        }

        // DELETE: api/Faculties/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaculties(int id)
        {
            var teacher = await (from t in _context.Teachers
                           where t.IdFaculty == id
                           select new
                           {
                               id = t.IdTeacher,
                               name = t.NameTeacher,
                           }).ToListAsync();
            if (teacher.Count != 0)
            {
                return Ok(teacher);
            }
            if (_context.Faculties == null)
            {
                return NotFound();
            }
            var faculties = await _context.Faculties.FindAsync(id);
            if (faculties == null)
            {
                return NotFound();
            }

            _context.Faculties.Remove(faculties);
            await _context.SaveChangesAsync();

            return Ok(value:"Xoa thanh cong");
        }

        private bool FacultiesExists(int id)
        {
            return (_context.Faculties?.Any(e => e.IdFaculty == id)).GetValueOrDefault();
        }
    }
}
