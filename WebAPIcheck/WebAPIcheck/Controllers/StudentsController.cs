using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
    public class StudentsController : ControllerBase
    {
        private readonly MyDbContext _context;

        public StudentsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult> GetStudents()
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var Student = await (from s in _context.Students
                                 join g in _context.Grades on s.IdGrade equals g.IdGrade
                                 select new
                                 {
                                     IdStudent = s.IdStudent,
                                     Name = s.Name,
                                     Gender = s.Gender,
                                     Address = s.Address,
                                     Country = s.Country,
                                     BirthDay = s.BirthDay,
                                     Email = s.Email,
                                     Phone = s.Phone,
                                     IdGrade = s.IdGrade,
                                     NameGrade = g.Name,
                                 }).ToListAsync();
            return Ok(Student);
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetStudents(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var Student = await (from s in _context.Students
                                 join g in _context.Grades on s.IdGrade equals g.IdGrade
                                 where s.IdStudent == id
                                 select new
                                 {
                                     IdStudent = s.IdStudent,
                                     Name = s.Name,
                                     Gender = s.Gender,
                                     Address = s.Address,
                                     Country = s.Country,
                                     BirthDay = s.BirthDay,
                                     Email = s.Email,
                                     Phone = s.Phone,
                                     IdGrade = s.IdGrade,
                                     NameGrade = g.Name,
                                 }).FirstAsync();
            if (Student == null)
            {
                return NotFound();
            }

            return Ok(Student);
        }
        [HttpGet("SeachingName/{Name}")]
        public async Task<ActionResult> GetStudents(string name)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var Student = await (from s in _context.Students
                                 join g in _context.Grades on s.IdGrade equals g.IdGrade
                                 where s.Name.Contains(name)
                                 select new
                                 {
                                     IdStudent = s.IdStudent,
                                     Name = s.Name,
                                     Gender = s.Gender,
                                     Address = s.Address,
                                     Country = s.Country,
                                     BirthDay = s.BirthDay,
                                     Email = s.Email,
                                     Phone = s.Phone,
                                     IdGrade = s.IdGrade,
                                     NameGrade = g.Name,
                                 }).FirstAsync();
            if (Student == null)
            {
                return NotFound();
            }

            return Ok(Student);
        }
        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudents(int id, StudentsModel students)
        {
            if (id != students.IdStudent)
            {
                return BadRequest();
            }
            var grade = await _context.Grades.FindAsync(students.IdGrade);
            if (grade == null)
            {
                return Ok(value: "Khong dung ma khoa");
            }
            var student = new Students
            {
                IdStudent = students.IdStudent,
                Name = students.Name,
                Gender = students.Gender,
                Address = students.Address,
                Country = students.Country,
                BirthDay = students.BirthDay,
                Email = students.Email,
                Phone = students.Phone,
                IdGrade = students.IdGrade,
            };
            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentsExists(id))
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

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Students>> PostStudents(StudentsModel students)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'MyDbContext.Students'  is null.");
            }
            var student_check = await _context.Students.FindAsync(students.IdStudent);
            if (student_check != null)
            {
                return Ok(value: "Trung ma sinh vien");
            }
            var student = new Students
            {
                IdStudent = students.IdStudent,
                Name = students.Name,
                Gender = students.Gender,
                Address = students.Address,
                Country = students.Country,
                BirthDay = students.BirthDay,
                Email = students.Email,
                Phone = students.Phone,
                IdGrade = students.IdGrade,
            };
            _context.Students.Add(student);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentsExists(students.IdStudent))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(value: "Them thanh cong");
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudents(int id)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var students = await _context.Students.FindAsync(id);
            if (students == null)
            {
                return NotFound();
            }

            _context.Students.Remove(students);
            await _context.SaveChangesAsync();

            return Ok(value: "Xoa thanh cong");
        }

        private bool StudentsExists(int id)
        {
            return (_context.Students?.Any(e => e.IdStudent == id)).GetValueOrDefault();
        }

        [HttpGet("page/{p}")]
        public async Task<ActionResult> GetStudentsPage(int p)
        {
            if (_context.Students == null)
            {
                return NotFound();
            }
            var Student = await (from s in _context.Students
                                 join g in _context.Grades on s.IdGrade equals g.IdGrade
                                 select new
                                 {
                                     IdStudent = s.IdStudent,
                                     Name = s.Name,
                                     Gender = s.Gender,
                                     Address = s.Address,
                                     Country = s.Country,
                                     BirthDay = s.BirthDay,
                                     Email = s.Email,
                                     Phone = s.Phone,
                                     IdGrade = s.IdGrade,
                                     NameGrade = g.Name,
                                 }).OrderByDescending(x => x.IdGrade).Skip((p - 1) * 4).Take(4).ToListAsync();
            return Ok(Student);
        }
    }
}
