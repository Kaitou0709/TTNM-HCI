using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIcheck.Data;
using WebAPIcheck.Migrations;
using WebAPIcheck.Models;

namespace WebAPIcheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutionFeeController : ControllerBase
    {
        private readonly MyDbContext _context;
        public TutionFeeController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetFee()
        {
            if (_context.Tuitionfees == null)
            {
                return NotFound();
            }
            var fee = _context.Tuitionfees.Join(
                _context.Students,
                fee => fee.IdStudent,
                st => st.IdStudent,
                (fee, st) => new
                {
                    IdTutionFee = fee.IdTutionFee,
                    IdStudent = fee.IdStudent,
                    NameStudent = st.Name,
                    IdSemester = fee.IdSemester,
                    MoneyTuition = fee.MoneyTuition,
                    MoneyPaid = fee.MoneyPaid,
                }).Join(
                _context.Semesters,
                fee => fee.IdSemester,
                se => se.IdSemester,
                (fee, se) => new
                {
                    IdTutionFee = fee.IdTutionFee,
                    IdStudent = fee.IdStudent,
                    NameStudent = fee.NameStudent,
                    IdSemester = fee.IdSemester,
                    Semester = se.Semester,
                    MoneyTuition = fee.MoneyTuition,
                    MoneyPaid = fee.MoneyPaid,
                }).Select(result => new
                {
                    result.IdTutionFee,
                    result.IdStudent,
                    result.NameStudent,
                    result.IdSemester,
                    result.Semester,
                    result.MoneyTuition,
                    result.MoneyPaid,
                }).ToList();
            return Ok(fee);
        }
        [HttpGet("/All/Student/{id}")]
        public async Task<IActionResult> GetFeeByStudent(int id)
        {
            if (_context.Tuitionfees == null)
            {
                return NotFound();
            }
            var fee = _context.Tuitionfees.Where(fee => fee.IdStudent == id).Join(
                _context.Students,
                fee => fee.IdStudent,
                st => st.IdStudent,
                (fee, st) => new
                {
                    IdTutionFee = fee.IdTutionFee,
                    IdStudent = fee.IdStudent,
                    NameStudent = st.Name,
                    IdSemester = fee.IdSemester,
                    MoneyTuition = fee.MoneyTuition,
                    MoneyPaid = fee.MoneyPaid,
                }).Join(
                _context.Semesters,
                fee => fee.IdSemester,
                se => se.IdSemester,
                (fee, se) => new
                {
                    IdTutionFee = fee.IdTutionFee,
                    IdStudent = fee.IdStudent,
                    NameStudent = fee.NameStudent,
                    IdSemester = fee.IdSemester,
                    Semester = se.Semester,
                    MoneyTuition = fee.MoneyTuition,
                    MoneyPaid = fee.MoneyPaid,
                }).Select(result => new
                {
                    result.IdTutionFee,
                    result.IdStudent,
                    result.NameStudent,
                    result.IdSemester,
                    result.Semester,
                    result.MoneyTuition,
                    result.MoneyPaid,
                }).ToList();
            return Ok(fee);
        }
        [HttpGet("/All/Semester/{id}")]
        public async Task<IActionResult> GetFeeBySemester(int id)
        {
            if (_context.Tuitionfees == null)
            {
                return NotFound();
            }
            var fee = _context.Tuitionfees.Where(fee => fee.IdSemester == id).Join(
                _context.Students,
                fee => fee.IdStudent,
                st => st.IdStudent,
                (fee, st) => new
                {
                    IdTutionFee = fee.IdTutionFee,
                    IdStudent = fee.IdStudent,
                    NameStudent = st.Name,
                    IdSemester = fee.IdSemester,
                    MoneyTuition = fee.MoneyTuition,
                    MoneyPaid = fee.MoneyPaid,
                }).Join(
                _context.Semesters,
                fee => fee.IdSemester,
                se => se.IdSemester,
                (fee, se) => new
                {
                    IdTutionFee = fee.IdTutionFee,
                    IdStudent = fee.IdStudent,
                    NameStudent = fee.NameStudent,
                    IdSemester = fee.IdSemester,
                    Semester = se.Semester,
                    MoneyTuition = fee.MoneyTuition,
                    MoneyPaid = fee.MoneyPaid,
                }).Select(result => new
                {
                    result.IdTutionFee,
                    result.IdStudent,
                    result.NameStudent,
                    result.IdSemester,
                    result.Semester,
                    result.MoneyTuition,
                    result.MoneyPaid,
                }).ToList();
            return Ok(fee);
        }
        [HttpGet("/All/{idStudent}/{idSemester}")]
        public async Task<IActionResult> GetFeeOne(int idStudent,int idSemester)
        {
            if (_context.Tuitionfees == null)
            {
                return NotFound();
            }
            var fee = _context.Tuitionfees.Where(fee => fee.IdSemester == idSemester && fee.IdSemester == idStudent).Join(
                _context.Students,
                fee => fee.IdStudent,
                st => st.IdStudent,
                (fee, st) => new
                {
                    IdTutionFee = fee.IdTutionFee,
                    IdStudent = fee.IdStudent,
                    NameStudent = st.Name,
                    IdSemester = fee.IdSemester,
                    MoneyTuition = fee.MoneyTuition,
                    MoneyPaid = fee.MoneyPaid,
                }).Join(
                _context.Semesters,
                fee => fee.IdSemester,
                se => se.IdSemester,
                (fee, se) => new
                {
                    IdTutionFee = fee.IdTutionFee,
                    IdStudent = fee.IdStudent,
                    NameStudent = fee.NameStudent,
                    IdSemester = fee.IdSemester,
                    Semester = se.Semester,
                    MoneyTuition = fee.MoneyTuition,
                    MoneyPaid = fee.MoneyPaid,
                }).Select(result => new
                {
                    result.IdTutionFee,
                    result.IdStudent,
                    result.NameStudent,
                    result.IdSemester,
                    result.Semester,
                    result.MoneyTuition,
                    result.MoneyPaid,
                }).ToList();
            return Ok(fee);
        }
        [HttpGet("/NotComplete")]
        public async Task<IActionResult> GetFeeNotcomplete()
        {
            if (_context.Tuitionfees == null)
            {
                return NotFound();
            }
            var fee = _context.Tuitionfees.Where(fee => fee.MoneyTuition - fee.MoneyPaid > 0).Join(
                _context.Students,
                fee => fee.IdStudent,
                st => st.IdStudent,
                (fee, st) => new
                {
                    IdTutionFee = fee.IdTutionFee,
                    IdStudent = fee.IdStudent,
                    NameStudent = st.Name,
                    IdSemester = fee.IdSemester,
                    MoneyTuition = fee.MoneyTuition,
                    MoneyPaid = fee.MoneyPaid,
                }).Join(
                _context.Semesters,
                fee => fee.IdSemester,
                se => se.IdSemester,
                (fee, se) => new
                {
                    IdTutionFee = fee.IdTutionFee,
                    IdStudent = fee.IdStudent,
                    NameStudent = fee.NameStudent,
                    IdSemester = fee.IdSemester,
                    Semester = se.Semester,
                    MoneyTuition = fee.MoneyTuition,
                    MoneyPaid = fee.MoneyPaid,
                }).Select(result => new
                {
                    result.IdTutionFee,
                    result.IdStudent,
                    result.NameStudent,
                    result.IdSemester,
                    result.Semester,
                    result.MoneyTuition,
                    result.MoneyPaid,
                }).ToList();
            return Ok(fee);
        }
        [HttpGet("/NotComplete/Semester/{id}")]
        public async Task<IActionResult> GetFeeNotCompleteBySemester(int id)
        {
            if (_context.Tuitionfees == null)
            {
                return NotFound();
            }
            var fee = _context.Tuitionfees.Where(fee => fee.IdSemester == id && fee.MoneyTuition - fee.MoneyPaid > 0).Join(
                _context.Students,
                fee => fee.IdStudent,
                st => st.IdStudent,
                (fee, st) => new
                {
                    IdTutionFee = fee.IdTutionFee,
                    IdStudent = fee.IdStudent,
                    NameStudent = st.Name,
                    IdSemester = fee.IdSemester,
                    MoneyTuition = fee.MoneyTuition,
                    MoneyPaid = fee.MoneyPaid,
                }).Join(
                _context.Semesters,
                fee => fee.IdSemester,
                se => se.IdSemester,
                (fee, se) => new
                {
                    IdTutionFee = fee.IdTutionFee,
                    IdStudent = fee.IdStudent,
                    NameStudent = fee.NameStudent,
                    IdSemester = fee.IdSemester,
                    Semester = se.Semester,
                    MoneyTuition = fee.MoneyTuition,
                    MoneyPaid = fee.MoneyPaid,
                }).Select(result => new
                {
                    result.IdTutionFee,
                    result.IdStudent,
                    result.NameStudent,
                    result.IdSemester,
                    result.Semester,
                    result.MoneyTuition,
                    result.MoneyPaid,
                }).ToList();
            return Ok(fee);
        }
        [HttpGet("/NotComplete/Student/{id}")]
        public async Task<IActionResult> GetFeeNotCompleteByStudent(int id)
        {
            if (_context.Tuitionfees == null)
            {
                return NotFound();
            }
            var fee = _context.Tuitionfees.Where(fee => fee.IdSemester == id && fee.MoneyTuition - fee.MoneyPaid > 0).Join(
                _context.Students,
                fee => fee.IdStudent,
                st => st.IdStudent,
                (fee, st) => new
                {
                    IdTutionFee = fee.IdTutionFee,
                    IdStudent = fee.IdStudent,
                    NameStudent = st.Name,
                    IdSemester = fee.IdSemester,
                    MoneyTuition = fee.MoneyTuition,
                    MoneyPaid = fee.MoneyPaid,
                }).Join(
                _context.Semesters,
                fee => fee.IdSemester,
                se => se.IdSemester,
                (fee, se) => new
                {
                    IdTutionFee = fee.IdTutionFee,
                    IdStudent = fee.IdStudent,
                    NameStudent = fee.NameStudent,
                    IdSemester = fee.IdSemester,
                    Semester = se.Semester,
                    MoneyTuition = fee.MoneyTuition,
                    MoneyPaid = fee.MoneyPaid,
                }).Select(result => new
                {
                    result.IdTutionFee,
                    result.IdStudent,
                    result.NameStudent,
                    result.IdSemester,
                    result.Semester,
                    result.MoneyTuition,
                    result.MoneyPaid,
                }).ToList();
            return Ok(fee);
        }
        [HttpGet("/Complete")]
        public async Task<IActionResult> GetFeecomplete()
        {
            if (_context.Tuitionfees == null)
            {
                return NotFound();
            }
            var fee = _context.Tuitionfees.Where(fee => fee.MoneyTuition - fee.MoneyPaid <= 0).Join(
                _context.Students,
                fee => fee.IdStudent,
                st => st.IdStudent,
                (fee, st) => new
                {
                    IdTutionFee = fee.IdTutionFee,
                    IdStudent = fee.IdStudent,
                    NameStudent = st.Name,
                    IdSemester = fee.IdSemester,
                    MoneyTuition = fee.MoneyTuition,
                    MoneyPaid = fee.MoneyPaid,
                }).Join(
                _context.Semesters,
                fee => fee.IdSemester,
                se => se.IdSemester,
                (fee, se) => new
                {
                    IdTutionFee = fee.IdTutionFee,
                    IdStudent = fee.IdStudent,
                    NameStudent = fee.NameStudent,
                    IdSemester = fee.IdSemester,
                    Semester = se.Semester,
                    MoneyTuition = fee.MoneyTuition,
                    MoneyPaid = fee.MoneyPaid,
                }).Select(result => new
                {
                    result.IdTutionFee,
                    result.IdStudent,
                    result.NameStudent,
                    result.IdSemester,
                    result.Semester,
                    result.MoneyTuition,
                    result.MoneyPaid,
                }).ToList();
            return Ok(fee);
        }
        [HttpGet("/Complete/Semester/{id}")]
        public async Task<IActionResult> GetFeeCompleteBySemester(int id)
        {
            if (_context.Tuitionfees == null)
            {
                return NotFound();
            }
            var fee = _context.Tuitionfees.Where(fee => fee.IdSemester == id && fee.MoneyTuition - fee.MoneyPaid > 0).Join(
                _context.Students,
                fee => fee.IdStudent,
                st => st.IdStudent,
                (fee, st) => new
                {
                    IdTutionFee = fee.IdTutionFee,
                    IdStudent = fee.IdStudent,
                    NameStudent = st.Name,
                    IdSemester = fee.IdSemester,
                    MoneyTuition = fee.MoneyTuition,
                    MoneyPaid = fee.MoneyPaid,
                }).Join(
                _context.Semesters,
                fee => fee.IdSemester,
                se => se.IdSemester,
                (fee, se) => new
                {
                    IdTutionFee = fee.IdTutionFee,
                    IdStudent = fee.IdStudent,
                    NameStudent = fee.NameStudent,
                    IdSemester = fee.IdSemester,
                    Semester = se.Semester,
                    MoneyTuition = fee.MoneyTuition,
                    MoneyPaid = fee.MoneyPaid,
                }).Select(result => new
                {
                    result.IdTutionFee,
                    result.IdStudent,
                    result.NameStudent,
                    result.IdSemester,
                    result.Semester,
                    result.MoneyTuition,
                    result.MoneyPaid,
                }).ToList();
            return Ok(fee);
        }
        [HttpGet("/Complete/Student/{id}")]
        public async Task<IActionResult> GetFeeCompleteByStudent(int id)
        {
            if (_context.Tuitionfees == null)
            {
                return NotFound();
            }
            var fee = _context.Tuitionfees.Where(fee => fee.IdSemester == id && fee.MoneyTuition - fee.MoneyPaid > 0).Join(
                _context.Students,
                fee => fee.IdStudent,
                st => st.IdStudent,
                (fee, st) => new
                {
                    IdTutionFee = fee.IdTutionFee,
                    IdStudent = fee.IdStudent,
                    NameStudent = st.Name,
                    IdSemester = fee.IdSemester,
                    MoneyTuition = fee.MoneyTuition,
                    MoneyPaid = fee.MoneyPaid,
                }).Join(
                _context.Semesters,
                fee => fee.IdSemester,
                se => se.IdSemester,
                (fee, se) => new
                {
                    IdTutionFee = fee.IdTutionFee,
                    IdStudent = fee.IdStudent,
                    NameStudent = fee.NameStudent,
                    IdSemester = fee.IdSemester,
                    Semester = se.Semester,
                    MoneyTuition = fee.MoneyTuition,
                    MoneyPaid = fee.MoneyPaid,
                }).Select(result => new
                {
                    result.IdTutionFee,
                    result.IdStudent,
                    result.NameStudent,
                    result.IdSemester,
                    result.Semester,
                    result.MoneyTuition,
                    result.MoneyPaid,
                }).ToList();
            return Ok(fee);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFee(int id, int Fee)
        {
            var Paid = await _context.Tuitionfees.FindAsync(id);
            if (Paid == null)
            {
                return BadRequest();
            }
            if (Fee > (Paid.MoneyTuition - Paid.MoneyPaid))
            {
                return Ok(value: "So tien lon hon so voi so tien phai nop");
            }
            Paid.MoneyPaid = Paid.MoneyPaid + Fee;
            await _context.SaveChangesAsync();
            return (Ok(value: "Nop tien thanh cong"));
        }
    }
}
