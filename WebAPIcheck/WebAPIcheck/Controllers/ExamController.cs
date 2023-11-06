using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIcheck.Data;
using WebAPIcheck.Migrations;
using WebAPIcheck.Models;

namespace WebAPIcheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly MyDbContext _context;
        public ExamController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet("NotCompleter")]
        public async Task<IActionResult> GetExamNotCompleter()
        {
            if (_context.ExamSubjects == null)
            {
                return NotFound();
            }
            var exam = _context.ExamSubjects.Where(e => e.Status != "co lich thi").Join(
                _context.ReSubjects,
                e => e.IdReSubject,
                re => re.IdReSubject,
                (e, re) => new
                {
                    IdExam = e.IdExam,
                    IdStartTime = e.IdStartTime,
                    IdEndTime = e.IdEndTime,
                    IdReSubject = e.IdReSubject,
                    IdStudent = re.IdStudent,
                    IdSubject = re.IdSubject,
                }).Join(
                _context.SubjectClasses,
                re => re.IdSubject,
                su => su.IdClass,
                (re, su) => new
                {
                    IdExam = re.IdExam,
                    IdStartTime = re.IdStartTime,
                    IdEndTime = re.IdEndTime,
                    IdStudent = re.IdStudent,
                    SemesterDateStart = su.SemesterDateStart,
                    SemesterDateEnd = su.SemesterDateEnd,
                    IdSemester = su.IdSemester,
                    IdClass = su.IdClass,
                    NameClass = su.NameClass,
                    IdSubject = su.IdSubject,
                }).Join(
                _context.Subjects,
                su => su.IdSubject,
                sub => sub.IdSubject,
                (su, sub) => new
                {
                    IdExam = su.IdExam,
                    IdStartTime = su.IdStartTime,
                    IdEndTime = su.IdEndTime,
                    IdStudent = su.IdStudent,
                    IdClass = su.IdClass,
                    IdSubject = su.IdSubject,
                    IdSemester = su.IdSemester,
                    NameSubject = _context.Subjects.Where(s => s.IdSubject == su.IdSubject).Select(s => s.Name).FirstOrDefault(),
                    NameClass = su.NameClass,
                    Semester = _context.Semesters.Where(se => se.IdSemester == su.IdSemester).Select(se => se.Semester).FirstOrDefault(),
                }).Join(
                _context.Students,
                e => e.IdStudent,
                st => st.IdStudent,
                (e, st) => new
                {
                    IdExam = e.IdExam,
                    StartTime = e.IdStartTime,
                    EndTime = e.IdEndTime,
                    IdStudent = e.IdStudent,
                    NameStudent = st.Name,
                    IdClass = e.IdClass,
                    IdSubject = e.IdSubject,
                    IdSemester = e.IdSemester,
                    NameSubject = e.NameSubject,
                    NameClass = e.NameClass,
                    Semester = e.Semester,
                }).Select(result => new
                {

                    result.IdExam,
                    result.StartTime,
                    result.EndTime,
                    result.IdStudent,
                    result.NameStudent,
                    result.IdClass,
                    result.IdSubject,
                    result.IdSemester,
                    result.NameSubject,
                    result.NameClass,
                    result.Semester,
                }).ToList();
            return Ok(exam);
        }
        [HttpGet("Completer")]
        public async Task<IActionResult> GetExamCompleter()
        {
            if (_context.ExamSubjects == null)
            {
                return NotFound();
            }
            var exam = _context.ExamSubjects.Where(e => e.Status == "co lich thi").Join(
                _context.ReSubjects,
                e => e.IdReSubject,
                re => re.IdReSubject,
                (e, re) => new
                {
                    IdExam = e.IdExam,
                    IdStartTime = e.IdStartTime,
                    IdEndTime = e.IdEndTime,
                    IdReSubject = e.IdReSubject,
                    IdStudent = re.IdStudent,
                    IdSubject = re.IdSubject,
                }).Join(
                _context.SubjectClasses,
                re => re.IdSubject,
                su => su.IdClass,
                (re, su) => new
                {
                    IdExam = re.IdExam,
                    IdStartTime = re.IdStartTime,
                    IdEndTime = re.IdEndTime,
                    IdStudent = re.IdStudent,
                    SemesterDateStart = su.SemesterDateStart,
                    SemesterDateEnd = su.SemesterDateEnd,
                    IdSemester = su.IdSemester,
                    IdClass = su.IdClass,
                    NameClass = su.NameClass,
                    IdSubject = su.IdSubject,
                }).Join(
                _context.Subjects,
                su => su.IdSubject,
                sub => sub.IdSubject,
                (su, sub) => new
                {
                    IdExam = su.IdExam,
                    IdStartTime = su.IdStartTime,
                    IdEndTime = su.IdEndTime,
                    IdStudent = su.IdStudent,
                    IdClass = su.IdClass,
                    IdSubject = su.IdSubject,
                    IdSemester = su.IdSemester,
                    NameSubject = _context.Subjects.Where(s => s.IdSubject == su.IdSubject).Select(s => s.Name).FirstOrDefault(),
                    NameClass = su.NameClass,
                    Semester = _context.Semesters.Where(se => se.IdSemester == su.IdSemester).Select(se => se.Semester).FirstOrDefault(),
                }).Join(
                _context.Students,
                e => e.IdStudent,
                st => st.IdStudent,
                (e, st) => new
                {
                    IdExam = e.IdExam,
                    IdStartTime = e.IdStartTime,
                    IdEndTime = e.IdEndTime,
                    IdStudent = e.IdStudent,
                    NameStudent = st.Name,
                    IdClass = e.IdClass,
                    IdSubject = e.IdSubject,
                    IdSemester = e.IdSemester,
                    NameSubject = e.NameSubject,
                    NameClass = e.NameClass,
                    Semester = e.Semester,
                }).Join(_context.LessonTime,
                                    e => e.IdStartTime,
                                    lt => lt.IdTime,
                                    (e, lt) => new
                                    {
                                        IdExam = e.IdExam,
                                        StartTime = lt.NameTime,
                                        IdEndTime = e.IdEndTime,
                                        IdStudent = e.IdStudent,
                                        NameStudent = e.NameStudent,
                                        IdClass = e.IdClass,
                                        IdSubject = e.IdSubject,
                                        IdSemester = e.IdSemester,
                                        NameSubject = e.NameSubject,
                                        NameClass = e.NameClass,
                                        Semester = e.Semester,
                                    })
                                .Join(_context.LessonTime,
                                    e => e.IdEndTime,
                                    lt1 => lt1.IdTime,
                                    (e, lt1) => new
                                    {
                                        IdExam = e.IdExam,
                                        StartTime = e.StartTime,
                                        EndTime = lt1.NameTime,
                                        IdStudent = e.IdStudent,
                                        NameStudent = e.NameStudent,
                                        IdClass = e.IdClass,
                                        IdSubject = e.IdSubject,
                                        IdSemester = e.IdSemester,
                                        NameSubject = e.NameSubject,
                                        NameClass = e.NameClass,
                                        Semester = e.Semester,
                                    }).Select(result => new
                                    {
                                        result.IdExam,
                                        result.StartTime,
                                        result.EndTime,
                                        result.IdStudent,
                                        result.NameStudent,
                                        result.IdClass,
                                        result.IdSubject,
                                        result.IdSemester,
                                        result.NameSubject,
                                        result.NameClass,
                                        result.Semester,
                                    }).ToList();
            return Ok(exam);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExam(int id, ExamModel exam)
        {
            if (exam.IdExam != id)
            {
                return BadRequest();
            }
            var examSubject = await _context.ExamSubjects.FindAsync(id);
            if (examSubject == null)
            {
                return BadRequest();
            }
            string[] start_time = _context.LessonTime.Where(lt => lt.IdTime == exam.IdStartTime).Select(lt => lt.StartTime).FirstOrDefault().Split(":");
            string[] end_time = _context.LessonTime.Where(lt => lt.IdTime == exam.IdEndTime).Select(lt => lt.EndTime).FirstOrDefault().Split(":");
            if (int.Parse(start_time[0]) > int.Parse(end_time[0]))
            {
                return Ok(value: "Khong dung tiet ket thuc va bat dau");
            }
            examSubject.IdStartTime = exam.IdStartTime;
            examSubject.IdEndTime = exam.IdEndTime;
            examSubject.Status = "co lich thi";
            await _context.SaveChangesAsync();
            return Ok(value: "Them thanh cong");
        }
        [HttpPut("Postponement/{id}")]
             public async Task<IActionResult> PostponementExam(int id)
        {
            var examSubject = await _context.ExamSubjects.FindAsync(id);
            if (examSubject == null)
            {
                return BadRequest();
            }
            examSubject.IdStartTime = null;
            examSubject.IdEndTime = null;
            examSubject.Status = "hoan thi";
            await _context.SaveChangesAsync();
            return Ok(value: "Hoan thanh cong");
        }
        [HttpGet("{idStudent}/{idSemester}")]
        public async Task<IActionResult> GetExamCompleter1(int idStudent,int idSemester)
        {
            if (_context.ExamSubjects == null)
            {
                return NotFound();
            }
            var exam = _context.ExamSubjects.Where(e => e.Status == "co lich thi").Join(
                _context.ReSubjects,
                e => e.IdReSubject,
                re => re.IdReSubject,
                (e, re) => new
                {
                    IdExam = e.IdExam,
                    IdStartTime = e.IdStartTime,
                    IdEndTime = e.IdEndTime,
                    IdReSubject = e.IdReSubject,
                    IdStudent = re.IdStudent,
                    IdSubject = re.IdSubject,
                }).Where(joined => joined.IdStudent == idStudent).Join(
                _context.SubjectClasses,
                re => re.IdSubject,
                su => su.IdClass,
                (re, su) => new
                {
                    IdExam = re.IdExam,
                    IdStartTime = re.IdStartTime,
                    IdEndTime = re.IdEndTime,
                    IdStudent = re.IdStudent,
                    SemesterDateStart = su.SemesterDateStart,
                    SemesterDateEnd = su.SemesterDateEnd,
                    IdSemester = su.IdSemester,
                    IdClass = su.IdClass,
                    NameClass = su.NameClass,
                    IdSubject = su.IdSubject,
                }).Where(joined => joined.IdSemester == idSemester).Join(
                _context.Subjects,
                su => su.IdSubject,
                sub => sub.IdSubject,
                (su, sub) => new
                {
                    IdExam = su.IdExam,
                    IdStartTime = su.IdStartTime,
                    IdEndTime = su.IdEndTime,
                    IdStudent = su.IdStudent,
                    IdClass = su.IdClass,
                    IdSubject = su.IdSubject,
                    IdSemester = su.IdSemester,
                    NameSubject = _context.Subjects.Where(s => s.IdSubject == su.IdSubject).Select(s => s.Name).FirstOrDefault(),
                    NameClass = su.NameClass,
                    Semester = _context.Semesters.Where(se => se.IdSemester == su.IdSemester).Select(se => se.Semester).FirstOrDefault(),
                }).Join(
                _context.Students,
                e => e.IdStudent,
                st => st.IdStudent,
                (e, st) => new
                {
                    IdExam = e.IdExam,
                    IdStartTime = e.IdStartTime,
                    IdEndTime = e.IdEndTime,
                    IdStudent = e.IdStudent,
                    NameStudent = st.Name,
                    IdClass = e.IdClass,
                    IdSubject = e.IdSubject,
                    IdSemester = e.IdSemester,
                    NameSubject = e.NameSubject,
                    NameClass = e.NameClass,
                    Semester = e.Semester,
                }).Join(_context.LessonTime,
                                    e => e.IdStartTime,
                                    lt => lt.IdTime,
                                    (e, lt) => new
                                    {
                                        IdExam = e.IdExam,
                                        StartTime = lt.NameTime,
                                        IdEndTime = e.IdEndTime,
                                        IdStudent = e.IdStudent,
                                        NameStudent = e.NameStudent,
                                        IdClass = e.IdClass,
                                        IdSubject = e.IdSubject,
                                        IdSemester = e.IdSemester,
                                        NameSubject = e.NameSubject,
                                        NameClass = e.NameClass,
                                        Semester = e.Semester,
                                    })
                                .Join(_context.LessonTime,
                                    e => e.IdEndTime,
                                    lt1 => lt1.IdTime,
                                    (e, lt1) => new
                                    {
                                        IdExam = e.IdExam,
                                        StartTime = e.StartTime,
                                        EndTime = lt1.NameTime,
                                        IdStudent = e.IdStudent,
                                        NameStudent = e.NameStudent,
                                        IdClass = e.IdClass,
                                        IdSubject = e.IdSubject,
                                        IdSemester = e.IdSemester,
                                        NameSubject = e.NameSubject,
                                        NameClass = e.NameClass,
                                        Semester = e.Semester,
                                    }).Select(result => new
                                    {
                                        result.IdExam,
                                        result.StartTime,
                                        result.EndTime,
                                        result.IdStudent,
                                        result.NameStudent,
                                        result.IdClass,
                                        result.IdSubject,
                                        result.IdSemester,
                                        result.NameSubject,
                                        result.NameClass,
                                        result.Semester,
                                    }).ToList();
            return Ok(exam);
        }
    }
}