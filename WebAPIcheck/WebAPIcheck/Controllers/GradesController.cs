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
    public class GradesController : ControllerBase
    {
        private readonly MyDbContext _context;

        public GradesController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Grades
        [HttpGet]
        public async Task<ActionResult> GetGrades()
        {
          if (_context.Grades == null)
          {
              return NotFound();
          }
            var Grade = await (from t in _context.Teachers
                                 join g in _context.Grades on t.IdTeacher equals g.IdTeacher
                                 select new
                                 {
                                     IdGrade = g.IdGrade,
                                     Name = g.Name,
                                     idTeacher = t.IdTeacher,
                                     NameTeacher = t.NameTeacher,
                                     idFaculty = g.idFaculty,
                                     nameFaculty = _context.Faculties.Where(s => s.IdFaculty == g.idFaculty).Select(s => s.Name).FirstOrDefault(),
                                     list = _context.Students.Where(s => s.IdGrade == g.IdGrade).Select(s => new {
                                         s.IdStudent,
                                         s.Name,
                                         s.Gender,
                                         s.Address,
                                         s.Country,
                                         s.BirthDay,
                                         s.Email,
                                         s.Phone,
                                     }).ToList()
                                 }).ToListAsync();
            return Ok(Grade);
        }

        // GET: api/Grades/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetGrade(int id)
        {
          if (_context.Grades == null)
          {
              return NotFound();
          }
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            var Grade = await (from t in _context.Teachers
                               join g in _context.Grades on t.IdTeacher equals g.IdTeacher
                               where g.IdGrade == id
                               select new
                               {
                                   IdGrade = g.IdGrade,
                                   Name = g.Name,
                                   idTeacher = t.IdTeacher,
                                   NameTeacher = t.NameTeacher,
                                   idFaculty = g.idFaculty,
                                   nameFaculty = _context.Faculties.Where(s => s.IdFaculty == g.idFaculty).Select(s => s.Name).FirstOrDefault(),
                                   list = _context.Students.Where(s => s.IdGrade == g.IdGrade).Select(s => new {
                                       s.IdStudent,
                                       s.Name,
                                       s.Gender,
                                       s.Address,
                                       s.Country,
                                       s.BirthDay,
                                       s.Email,
                                       s.Phone,
                                   }).ToList(),
                               }).FirstAsync();
            return Ok(Grade);
        }

        // PUT: api/Grades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGrade(int id, GradeModel grade)
        {

            if (id != grade.IdGrade)
            {
                return BadRequest();
            }
            var teacher = await _context.Teachers.FindAsync(grade.IdTeacher);
            if (teacher == null)
            {
                return Ok(value: "Khong dung ma khoa");
            }
            var faculties = await _context.Faculties.FindAsync(grade.idFaculty);
            if (faculties == null)
            {
                return Ok(value: "Khong dung ma khoa");
            }
            var grade_data = new Data.Grade
            {   
                IdGrade = grade.IdGrade,
                Name = grade.Name,
                IdTeacher = grade.IdTeacher,
                idFaculty = grade.idFaculty,
            };
            _context.Entry(grade_data).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GradeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(value: "Sua thanh cong");
        }

        [HttpPost]
        public async Task<ActionResult<Data.Grade>> PostGrade(GradeModel grade)
        {
          if (_context.Grades == null)
          {
              return Problem("Entity set 'MyDbContext.Grades'  is null.");
          }
            var teacher = await _context.Teachers.FindAsync(grade.IdTeacher);
            if (teacher == null)
            {
                return Ok(value: "Khong dung ma khoa");
            }
            var faculties = await _context.Faculties.FindAsync(grade.idFaculty);
            if (faculties == null)
            {
                return Ok(value: "Khong dung ma khoa");
            }
            var grade_data = new Data.Grade
            {
                Name = grade.Name,
                IdTeacher = grade.IdTeacher,
                idFaculty = grade.idFaculty,
            };
            _context.Grades.Add(grade_data);
            await _context.SaveChangesAsync();
            return Ok(value:"Them thanh cong");
        }

        // DELETE: api/Grades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
        {

            if (_context.Grades == null)
            {
                return NotFound();
            }
            var student = await (from s in _context.Students
                               where s.IdGrade == id
                               select new
                               {
                                   id = s.IdStudent,
                                   name = s.Name,
                               }).ToListAsync();
            if (student.Count != 0)
            {
                return Ok(student);
            }
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();

            return Ok(value:"Xoa thanh cong");
        }

        private bool GradeExists(int id)
        {
            return (_context.Grades?.Any(e => e.IdGrade == id)).GetValueOrDefault();
        }
        [HttpGet("page/{p}")]
        public async Task<ActionResult> GetGrades(int p)
        {
            if (_context.Grades == null)
            {
                return NotFound();
            }
            var Grade = await (from t in _context.Teachers
                               join g in _context.Grades on t.IdTeacher equals g.IdTeacher
                               select new
                               {
                                   IdGrade = g.IdGrade,
                                   Name = g.Name,
                                   idTeacher = t.IdTeacher,
                                   NameTeacher = t.NameTeacher,
                                   idFaculty = g.idFaculty,
                                   nameFaculty = _context.Faculties.Where(s => s.IdFaculty == g.idFaculty).Select(s => s.Name).FirstOrDefault(),
                                   list = _context.Students.Where(s => s.IdGrade == g.IdGrade).Select(s => new {
                                       s.IdStudent,
                                       s.Name,
                                       s.Gender,
                                       s.Address,
                                       s.Country,
                                       s.BirthDay,
                                       s.Email,
                                       s.Phone,
                                   }).ToList()
                               }).OrderByDescending(x => x.IdGrade).Skip((p - 1) * 4).Take(4).ToListAsync();
            return Ok(Grade);
        }
    }
}
