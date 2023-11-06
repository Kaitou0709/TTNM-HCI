using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebAPIcheck.Data;
using WebAPIcheck.Migrations;
using WebAPIcheck.Models;

namespace WebAPIcheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReSubjectController : ControllerBase
    {
        private readonly MyDbContext _context;
        public ReSubjectController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet("SeachingSubject/{idStudent}")]
        public async Task<IActionResult> GetSubject(int idStudent)
        {
            if (_context.ReSubjects == null)
            {
                return NotFound();
            }
            var listSubject = _context.ReSubjects.Where(re => re.IdStudent == idStudent).Join(
                _context.SubjectClasses,
                re => re.IdSubject,
                su => su.IdClass,
                (re, su) => new
                {
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
                    IdStudent = su.IdStudent,
                    SemesterDateStart = su.SemesterDateStart,
                    SemesterDateEnd = su.SemesterDateEnd,
                    IdClass = su.IdClass,
                    IdSubject = su.IdSubject,
                    IdSemester = su.IdSemester,
                    NameSubject = _context.Subjects.Where(s => s.IdSubject == su.IdSubject).Select(s => s.Name).FirstOrDefault(),
                    NameClass = su.NameClass,
                    Semester = _context.Semesters.Where(se => se.IdSemester == su.IdSemester).Select(se => se.Semester).FirstOrDefault(),
                    list = _context.SemestersSubject
                        .Where(ss => ss.IdSubjectClass == su.IdClass)
                        .Join(_context.Schedules,
                                ss => ss.IdSchedule,
                                sc => sc.IdSchedule,
                                (ss, sc) => new
                                {
                                    Day = sc.Day,
                                    IdTimeStart = sc.IdTimeStart,
                                    IdTimeEnd = sc.IdTimeEnd
                                })
                                .Join(_context.LessonTime,
                                    sc => sc.IdTimeStart,
                                    lt => lt.IdTime,
                                    (sc, lt) => new
                                    {
                                        Day = sc.Day,
                                        StartTime = lt.NameTime,
                                        IdTimeEnd = sc.IdTimeEnd
                                    })
                                .Join(_context.LessonTime,
                                    sc => sc.IdTimeEnd,
                                    lt1 => lt1.IdTime,
                                    (sc, lt1) => new
                                    {
                                        Day = sc.Day,
                                        StartTime = sc.StartTime,
                                        EndTime = lt1.NameTime
                                    })
                                .Select(result => new
                                {
                                    result.Day,
                                    result.StartTime,
                                    result.EndTime
                                }).ToList()
                }).Select(result => new
                {
                    result.IdClass,
                    result.NameClass,
                    result.SemesterDateStart,
                    result.SemesterDateEnd,
                    result.IdSemester,
                    result.Semester,
                    result.IdSubject,
                    result.NameSubject,
                    result.list,
                }).ToList();
            return Ok(listSubject);
        }
        [HttpGet("SeachingStudent/{idSubject}")]
        public async Task<IActionResult> GetStudent(int idSubject)
        {
            if (_context.ReSubjects == null)
            {
                return NotFound();
            }
            var listSubject = _context.ReSubjects.Where(re => re.IdSubject == idSubject).Join(
                _context.Students,
                re => re.IdStudent,
                st => st.IdStudent,
                (re, st) => new
                {
                    IdStudent = re.IdStudent,
                    Name = st.Name,
                    Gender = st.Gender,
                    birthday = st.BirthDay,
                    NameGrade = _context.Grades.Where(g => g.IdGrade == st.IdGrade).Select(g => g.Name).FirstOrDefault(),
                })
                                .Select(result => new
                                {
                                    result.IdStudent,
                                    result.Name,
                                    result.Gender,
                                    result.birthday,
                                    result.NameGrade,
                                }).ToList();
            return Ok(listSubject);
        }
        [HttpPost]
        public async Task<ActionResult> PostReSubject(ReSubjectsModel ReSubjects)
        {
            var Class = await _context.SubjectClasses.FindAsync(ReSubjects.IdSubject);
            var Student = await _context.Students.FindAsync(ReSubjects.IdStudent);
            if (Class == null || Student == null)
                return Ok(value: ("Khong dung ma"));
            /*var check = _context.ReSubjects.Where(re => re.IdStudent == ReSubjects.IdStudent).Join(
                _context.SubjectClasses,
                re => re.IdSubject,
                su => su.IdClass,
                (re, su) => new
                {
                    IdSubject = su.IdSubject,
                    IdSemesters = su.IdSemester,
                }
                ).Select(result => new
                {
                    result.IdSubject,
                    result.IdSemesters,
                }).ToList();
            if (check.Any(c => c.IdSubject == Class.IdSubject && c.IdSemesters == Class.IdSemester))
            {
                return Ok(value: ("Hoc sinh dang hoc mon nay o hoc ky nay roi"));
            }*/
            var listTime = _context.ReSubjects.Where(re => re.IdStudent == ReSubjects.IdStudent).Join(
               _context.SubjectClasses,
               re => re.IdSubject,
               su => su.IdClass,
               (re, su) => new
               {
                   idReSubject = re.IdReSubject,
                   IdStudent = re.IdStudent,
                   SemesterDateStart = su.SemesterDateStart,
                   SemesterDateEnd = su.SemesterDateEnd,
                   IdSemester = su.IdSemester,
                   IdClass = su.IdClass,
                   NameClass = su.NameClass,
                   IdSubject = su.IdSubject,
               }).Where(joined => joined.IdSemester == Class.IdSemester && joined.IdSubject != Class.IdSubject)
                .Select(result => new
                {
                    result.IdClass,
                }).Join(
                _context.SemestersSubject,
                su => su.IdClass,
                ss => ss.IdSubjectClass,
                (su, ss) => new
                {
                    IdSchedule = ss.IdSchedule,
                }).Join(_context.Schedules,
                                ss => ss.IdSchedule,
                                sc => sc.IdSchedule,
                                (ss, sc) => new
                                {
                                    Day = sc.Day,
                                    IdTimeStart = sc.IdTimeStart,
                                    IdTimeEnd = sc.IdTimeEnd
                                })
                                .Join(_context.LessonTime,
                                    sc => sc.IdTimeStart,
                                    lt => lt.IdTime,
                                    (sc, lt) => new
                                    {
                                        Day = sc.Day,
                                        StartTime = lt.NameTime,
                                        IdTimeEnd = sc.IdTimeEnd
                                    })
                                .Join(_context.LessonTime,
                                    sc => sc.IdTimeEnd,
                                    lt1 => lt1.IdTime,
                                    (sc, lt1) => new
                                    {
                                        Day = sc.Day,
                                        StartTime = sc.StartTime,
                                        EndTime = lt1.NameTime
                                    })
                                .Select(result => new
                                {
                                    result.Day,
                                    result.StartTime,
                                    result.EndTime
                                }).ToList();
            var listSubject = _context.SubjectClasses.Where(joined => joined.IdSemester == Class.IdSemester && joined.IdSubject == Class.IdSubject)
                .Select(result => new
                {
                    result.IdClass,
                }).Join(
                _context.SemestersSubject,
                su => su.IdClass,
                ss => ss.IdSubjectClass,
                (su, ss) => new
                {
                    IdClass = su.IdClass,
                    IdSchedule = ss.IdSchedule,
                }).Join(_context.Schedules,
                                ss => ss.IdSchedule,
                                sc => sc.IdSchedule,
                                (ss, sc) => new
                                {
                                    IdClass = ss.IdClass,
                                    Day = sc.Day,
                                    IdTimeStart = sc.IdTimeStart,
                                    IdTimeEnd = sc.IdTimeEnd
                                })
                                .Join(_context.LessonTime,
                                    sc => sc.IdTimeStart,
                                    lt => lt.IdTime,
                                    (sc, lt) => new
                                    {
                                        IdClass = sc.IdClass,
                                        Day = sc.Day,
                                        StartTime = lt.NameTime,
                                        IdTimeEnd = sc.IdTimeEnd
                                    })
                                .Join(_context.LessonTime,
                                    sc => sc.IdTimeEnd,
                                    lt1 => lt1.IdTime,
                                    (sc, lt1) => new
                                    {
                                        IdClass = sc.IdClass,
                                        Day = sc.Day,
                                        StartTime = sc.StartTime,
                                        EndTime = lt1.NameTime
                                    })
                                .Select(result => new
                                {
                                    result.IdClass,
                                    result.Day,
                                    result.StartTime,
                                    result.EndTime
                                }).ToList();
            foreach (var subject in listSubject)
            {
                foreach (var time in listTime)
                {
                    if (subject.Day.ToLower() == time.Day.ToLower() && int.Parse(subject.StartTime.Split(" ")[1]) <= int.Parse(time.EndTime.Split(" ")[1]))
                    {
                        if (subject.IdClass == ReSubjects.IdSubject)
                        {
                            return Ok(value: "Trung mon hoc");
                        }
                    }
                }
            }
            var ReSubject = new ReSubjects
            {
                IdSubject = ReSubjects.IdSubject,
                IdStudent = ReSubjects.IdStudent,
            };
            _context.ReSubjects.Add(ReSubject);
            await _context.SaveChangesAsync();
            var IdReSubject = ReSubject.IdReSubject;
            var TuiTionfee = _context.Tuitionfees.Where(t => t.IdSemester == Class.IdSemester && t.IdStudent == ReSubject.IdStudent).FirstOrDefault();
            int fee = _context.Subjects.Where(f => f.IdSubject == Class.IdSubject).Select(t => t.Fee).FirstOrDefault();
            if (TuiTionfee == null)
            {
                var TuiTionFee = new Data.Tuitionfee
                {
                    IdStudent = ReSubject.IdStudent,
                    IdSemester = Class.IdSemester,
                    MoneyTuition = _context.Subjects.Where(f => f.IdSubject == Class.IdSubject).Select(t => t.Fee).FirstOrDefault(),
                    MoneyPaid = 0,
                };
                _context.Tuitionfees.Add(TuiTionFee);
                await _context.SaveChangesAsync();
            }
            else
            {
                TuiTionfee.MoneyTuition = TuiTionfee.MoneyTuition + fee;
                await _context.SaveChangesAsync();
            }
            var ExamSubject = new ExamSubjects
            {
                IdReSubject = IdReSubject,
                IdStartTime = null,
                IdEndTime = null,
                Status = "chua thi"
            };
            var Score = new Score
            {
                IdReSubject = IdReSubject,
                Status = "chua thi",
                ScoreSubject = null,
            };
            _context.ExamSubjects.Add(ExamSubject);
            _context.Scores.Add(Score);
            await _context.SaveChangesAsync();
            return Ok(value: "Dang ky mon thanh cong");
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReSubject(int id, ReSubjectsModel ReSubjects)
        {
            var reSubjects = await _context.ReSubjects.FindAsync(id);
            if (reSubjects == null)
            {
                return BadRequest();
            }
            if (reSubjects.IdStudent != ReSubjects.IdStudent)
            {
                return Ok(value: ("Khong duoc thay doi Ma sinh vien"));
            }
            var Class = await _context.SubjectClasses.FindAsync(reSubjects.IdSubject);
            var Class_new = await _context.SubjectClasses.FindAsync(ReSubjects.IdSubject);
            if (Class_new == null)
            {
                return Ok(value: "Khong ton tai lop hoc nay");
            }    
            if (Class.IdSubject != Class_new.IdSubject)
            {
                return Ok(value: ("Khong duoc thay doi mon"));
            }
            if (Class.IdSemester != Class_new.IdSemester)
            {
                return Ok(value: ("Khong dung  hoc ky"));
            }
            var listTime = _context.ReSubjects.Where(re => re.IdStudent == ReSubjects.IdStudent).Join(
               _context.SubjectClasses,
               re => re.IdSubject,
               su => su.IdClass,
               (re, su) => new
               {
                   idReSubject = re.IdReSubject,
                   IdStudent = re.IdStudent,
                   SemesterDateStart = su.SemesterDateStart,
                   SemesterDateEnd = su.SemesterDateEnd,
                   IdSemester = su.IdSemester,
                   IdClass = su.IdClass,
                   NameClass = su.NameClass,
                   IdSubject = su.IdSubject,
               }).Where(joined => joined.IdSemester == Class.IdSemester && joined.IdSubject != Class.IdSubject)
                .Select(result => new
                {
                    result.IdClass,
                }).Join(
                _context.SemestersSubject,
                su => su.IdClass,
                ss => ss.IdSubjectClass,
                (su, ss) => new
                {
                    IdSchedule = ss.IdSchedule,
                }).Join(_context.Schedules,
                                ss => ss.IdSchedule,
                                sc => sc.IdSchedule,
                                (ss, sc) => new
                                {
                                    Day = sc.Day,
                                    IdTimeStart = sc.IdTimeStart,
                                    IdTimeEnd = sc.IdTimeEnd
                                })
                                .Join(_context.LessonTime,
                                    sc => sc.IdTimeStart,
                                    lt => lt.IdTime,
                                    (sc, lt) => new
                                    {
                                        Day = sc.Day,
                                        StartTime = lt.NameTime,
                                        IdTimeEnd = sc.IdTimeEnd
                                    })
                                .Join(_context.LessonTime,
                                    sc => sc.IdTimeEnd,
                                    lt1 => lt1.IdTime,
                                    (sc, lt1) => new
                                    {
                                        Day = sc.Day,
                                        StartTime = sc.StartTime,
                                        EndTime = lt1.NameTime
                                    })
                                .Select(result => new
                                {
                                    result.Day,
                                    result.StartTime,
                                    result.EndTime
                                }).ToList();
            var listSubject = _context.SubjectClasses.Where(joined => joined.IdSemester == Class.IdSemester && joined.IdSubject == Class.IdSubject)
                .Select(result => new
                {
                    result.IdClass,
                }).Join(
                _context.SemestersSubject,
                su => su.IdClass,
                ss => ss.IdSubjectClass,
                (su, ss) => new
                {
                    IdClass = su.IdClass,
                    IdSchedule = ss.IdSchedule,
                }).Join(_context.Schedules,
                                ss => ss.IdSchedule,
                                sc => sc.IdSchedule,
                                (ss, sc) => new
                                {
                                    IdClass = ss.IdClass,
                                    Day = sc.Day,
                                    IdTimeStart = sc.IdTimeStart,
                                    IdTimeEnd = sc.IdTimeEnd
                                })
                                .Join(_context.LessonTime,
                                    sc => sc.IdTimeStart,
                                    lt => lt.IdTime,
                                    (sc, lt) => new
                                    {
                                        IdClass = sc.IdClass,
                                        Day = sc.Day,
                                        StartTime = lt.NameTime,
                                        IdTimeEnd = sc.IdTimeEnd
                                    })
                                .Join(_context.LessonTime,
                                    sc => sc.IdTimeEnd,
                                    lt1 => lt1.IdTime,
                                    (sc, lt1) => new
                                    {
                                        IdClass = sc.IdClass,
                                        Day = sc.Day,
                                        StartTime = sc.StartTime,
                                        EndTime = lt1.NameTime
                                    })
                                .Select(result => new
                                {
                                    result.IdClass,
                                    result.Day,
                                    result.StartTime,
                                    result.EndTime
                                }).ToList();
            var listConflicting = new List<Data.Conflicting>();
            foreach (var subject in listSubject)
            {
                foreach (var time in listTime)
                {
                    if (subject.Day.ToLower() == time.Day.ToLower() && int.Parse(subject.StartTime.Split(" ")[1]) <= int.Parse(time.EndTime.Split(" ")[1]))
                    {
                        if (subject.IdClass == ReSubjects.IdSubject)
                        {
                            return Ok(value: "Trung mon hoc");
                        }
                    }
                }
            }
            reSubjects.IdSubject = ReSubjects.IdSubject;
            await _context.SaveChangesAsync();
            return Ok(value: "Sua thanh cong");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaculties(int id)
        {
            var reSubjects = await _context.ReSubjects.FindAsync(id);
            if(reSubjects == null)
            {
                return NotFound();
            }    
            var Class = await _context.SubjectClasses.FindAsync(reSubjects.IdSubject);
            var TuiTionfee = _context.Tuitionfees.Where(t => t.IdSemester == Class.IdSemester && t.IdStudent == reSubjects.IdStudent).FirstOrDefault();
            TuiTionfee.MoneyTuition = TuiTionfee.MoneyTuition - _context.Subjects.Where(f => f.IdSubject == Class.IdSubject).Select(t => t.Fee).FirstOrDefault();
            _context.ReSubjects.Remove(reSubjects);
            await _context.SaveChangesAsync();
            return Ok(value: "Xoa thanh cong");
        }
        [HttpGet("SeachingSubject/{idSemester_input}/{idStudent}")]
        public async Task<IActionResult> GetStudentLearn(int idSemester_input, int idStudent)
        {
            var listSubject = _context.ReSubjects.Where(re => re.IdStudent == idStudent).Join(
               _context.SubjectClasses,
               re => re.IdSubject,
               su => su.IdClass,
               (re, su) => new
               {
                   IdStudent = re.IdStudent,
                   SemesterDateStart = su.SemesterDateStart,
                   SemesterDateEnd = su.SemesterDateEnd,
                   IdSemester = su.IdSemester,
                   IdClass = su.IdClass,
                   NameClass = su.NameClass,
                   IdSubject = su.IdSubject,
               }).Where(joined => joined.IdSemester == idSemester_input)
                .Select(result => new
                {
                    result.IdSubject,
                }).ToList();
            if (listSubject.Count == 0)
                return NotFound();
            return Ok(listSubject);
        }
        [HttpGet("SeachingSubject/{idSemester_input}/{idSubject_input}/{idStudent}")]
        public async Task<IActionResult> GetStudentLearnSubject(int idSubject_input, int idSemester_input, int idStudent)
        {
            var listSubject = _context.ReSubjects.Where(re => re.IdStudent == idStudent).Join(
               _context.SubjectClasses,
               re => re.IdSubject,
               su => su.IdClass,
               (re, su) => new
               {
                   idReSubject = re.IdReSubject,
                   IdStudent = re.IdStudent,
                   SemesterDateStart = su.SemesterDateStart,
                   SemesterDateEnd = su.SemesterDateEnd,
                   IdSemester = su.IdSemester,
                   IdClass = su.IdClass,
                   NameClass = su.NameClass,
                   IdSubject = su.IdSubject,
               }).Where(joined => joined.IdSemester == idSemester_input && joined.IdSubject == idSubject_input)
                .Select(result => new
                {
                    result.idReSubject,
                    result.IdClass,
                }).FirstOrDefault();
            if(listSubject == null)
            {
                return NotFound();
            }
            return Ok(listSubject);

        }
        [HttpGet("SeachingTimeClass/{idSemester_input}/{idSubject_input}/{idStudent}")]
        public async Task<IActionResult> GetStudentLearnTime(int idSubject_input, int idSemester_input, int idStudent)
        {
            var listTime = _context.ReSubjects.Where(re => re.IdStudent == idStudent).Join(
               _context.SubjectClasses,
               re => re.IdSubject,
               su => su.IdClass,
               (re, su) => new
               {
                   idReSubject = re.IdReSubject,
                   IdStudent = re.IdStudent,
                   SemesterDateStart = su.SemesterDateStart,
                   SemesterDateEnd = su.SemesterDateEnd,
                   IdSemester = su.IdSemester,
                   IdClass = su.IdClass,
                   NameClass = su.NameClass,
                   IdSubject = su.IdSubject,
               }).Where(joined => joined.IdSemester == idSemester_input && joined.IdSubject != idSubject_input)
                .Select(result => new
                {
                    result.IdClass,
                }).Join(
                _context.SemestersSubject,
                su => su.IdClass,
                ss => ss.IdSubjectClass,
                (su,ss) => new
                {
                    IdSchedule = ss.IdSchedule,
                }).Join(_context.Schedules,
                                ss => ss.IdSchedule,
                                sc => sc.IdSchedule,
                                (ss, sc) => new
                                {
                                    Day = sc.Day,
                                    IdTimeStart = sc.IdTimeStart,
                                    IdTimeEnd = sc.IdTimeEnd
                                })
                                .Join(_context.LessonTime,
                                    sc => sc.IdTimeStart,
                                    lt => lt.IdTime,
                                    (sc, lt) => new
                                    {
                                        Day = sc.Day,
                                        StartTime = lt.NameTime,
                                        IdTimeEnd = sc.IdTimeEnd
                                    })
                                .Join(_context.LessonTime,
                                    sc => sc.IdTimeEnd,
                                    lt1 => lt1.IdTime,
                                    (sc, lt1) => new
                                    {
                                        Day = sc.Day,
                                        StartTime = sc.StartTime,
                                        EndTime = lt1.NameTime
                                    })
                                .Select(result => new
                                {
                                    result.Day,
                                    result.StartTime,
                                    result.EndTime
                                }).ToList();
            var listSubject = _context.SubjectClasses.Where(joined => joined.IdSemester == idSemester_input  && joined.IdSubject == idSubject_input)
                .Select(result => new
                {
                    result.IdClass,
                }).Join(
                _context.SemestersSubject,
                su => su.IdClass,
                ss => ss.IdSubjectClass,
                (su, ss) => new
                {
                    IdClass = su.IdClass,
                    IdSchedule = ss.IdSchedule,
                }).Join(_context.Schedules,
                                ss => ss.IdSchedule,
                                sc => sc.IdSchedule,
                                (ss, sc) => new
                                {
                                    IdClass = ss.IdClass,
                                    Day = sc.Day,
                                    IdTimeStart = sc.IdTimeStart,
                                    IdTimeEnd = sc.IdTimeEnd
                                })
                                .Join(_context.LessonTime,
                                    sc => sc.IdTimeStart,
                                    lt => lt.IdTime,
                                    (sc, lt) => new
                                    {
                                        IdClass = sc.IdClass,
                                        Day = sc.Day,
                                        StartTime = lt.NameTime,
                                        IdTimeEnd = sc.IdTimeEnd
                                    })
                                .Join(_context.LessonTime,
                                    sc => sc.IdTimeEnd,
                                    lt1 => lt1.IdTime,
                                    (sc, lt1) => new
                                    {
                                        IdClass = sc.IdClass,
                                        Day = sc.Day,
                                        StartTime = sc.StartTime,
                                        EndTime = lt1.NameTime
                                    })
                                .Select(result => new
                                {
                                    result.IdClass,
                                    result.Day,
                                    result.StartTime,
                                    result.EndTime
                                }).ToList();
            var listConflicting = new List<Data.Conflicting>();
            foreach (var subject in listSubject)
            {
                foreach (var time in listTime)
                {
                    if(subject.Day.ToLower() == time.Day.ToLower() && int.Parse(subject.StartTime.Split(" ")[1]) <= int.Parse(time.EndTime.Split(" ")[1]))
                    {
                        listConflicting.Add(new Data.Conflicting { Id = subject.IdClass });
                    }
                }
            }    
            if (listConflicting == null)
            {
                return NotFound();
            }
            return Ok(listConflicting);

        }
    }
}
