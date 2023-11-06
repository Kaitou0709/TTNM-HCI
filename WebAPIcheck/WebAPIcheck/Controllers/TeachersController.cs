using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIcheck.Data;
using WebAPIcheck.Models;

namespace WebAPIcheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public TeachersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult> GetTeachers()
        {
          if (_context.Teachers == null)
          {
              return NotFound();
          }
            var Teacher = await (from t in _context.Teachers
                                 join f in _context.Faculties on t.IdFaculty equals f.IdFaculty
                                 select new
                                 {
                                     idTeacher = t.IdTeacher,
                                     NameTeacher = t.NameTeacher,
                                     Salary = t.Salary,
                                     Position = t.Position,
                                     IdFaculty = f.IdFaculty,
                                     NameFaculty = f.Name,
                                 }).ToListAsync();
            return Ok(Teacher);
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetTeachers(int id)
        {
          if (_context.Teachers == null)
          {
              return NotFound();
          }
            var Teacher = await (from t in _context.Teachers
                                 join f in _context.Faculties on t.IdFaculty equals f.IdFaculty where t.IdTeacher == id
                                 select new
                                 {
                                     idTeacher = t.IdTeacher,
                                     NameTeacher = t.NameTeacher,
                                     Salary = t.Salary,
                                     Position = t.Position,
                                     IdFaculty = f.IdFaculty,
                                     NameFaculty = f.Name,
                                 }).FirstAsync();
            if (Teacher == null)
            {
                return NotFound();
            }
            return Ok(Teacher);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeachers(int id, TeachersModel teachers)
        {
            if (id != teachers.IdTeacher)
            {
                return BadRequest();
            }
            var faculties = await _context.Faculties.FindAsync(teachers.IdFaculty);
            if (faculties == null)
            {
                return Ok(value: "Khong dung ma khoa");
            }
            var teacher = new Teachers
            {
                IdTeacher = teachers.IdTeacher,
                NameTeacher = teachers.NameTeacher,
                Salary = teachers.Salary,
                Position = teachers.Position,
                IdFaculty = teachers.IdFaculty,
            };
            _context.Entry(teacher).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeachersExists(id))
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
        public async Task<ActionResult<Teachers>> PostTeachers(TeachersModel teachers)
        {
          if (_context.Teachers == null)
          {
              return Problem("Entity set 'MyDbContext.Teachers'  is null.");
          }
            var faculties = await _context.Faculties.FindAsync(teachers.IdFaculty);
            if (faculties == null)
            {
                return Ok(value: "Khong dung ma khoa");
            }
            var teacher = new Teachers
            {
                NameTeacher = teachers.NameTeacher,
                Salary = teachers.Salary,
                Position = teachers.Position,
                IdFaculty = teachers.IdFaculty,
            };
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            return Ok(value: "Them thanh cong");
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeachers(int id)
        {
            var grade = await (from g in _context.Grades
                                 where g.IdTeacher == id
                                 select new
                                 {
                                     id = g.IdGrade,
                                     name = g.Name,
                                 }).ToListAsync();
            if (grade.Count != 0)
            {
                return Ok(grade);
            }
            if (_context.Teachers == null)
            {
                return NotFound();
            }
            var teachers = await _context.Teachers.FindAsync(id);
            if (teachers == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teachers);
            await _context.SaveChangesAsync();

            return Ok(value: "Xoa thanh cong");
        }

        private bool TeachersExists(int id)
        {
            return (_context.Teachers?.Any(e => e.IdTeacher == id)).GetValueOrDefault();
        }
    }
}
