using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tavis.UriTemplates;
using WebAPIcheck.Data;
using WebAPIcheck.Migrations;
using WebAPIcheck.Models;

namespace WebAPIcheck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterSubjectController : ControllerBase
    {
        private readonly MyDbContext _context;
        public SemesterSubjectController(MyDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetSemesterSubject()
        {
            if (_context.SubjectClasses == null)
            {
                return NotFound();
            }
            var listClassSubject = from c in _context.SubjectClasses
                                     select new
                                     {
                                         IdClass = c.IdClass,
                                         NameClass = c.NameClass,
                                         NumberClass = c.NumberClass,
                                         Semester = _context.Semesters.Where(se => se.IdSemester == c.IdSemester).Select(se => se.Semester).FirstOrDefault(),
                                         Subject = _context.Subjects.Where(su => su.IdSubject == c.IdSubject).Select(su => su.Name).FirstOrDefault(),
                                         SemesterDateStart = c.SemesterDateStart,
                                         SemesterDateEnd = c.SemesterDateEnd,
                                         NameTeacher = c.NameTeacher,
                                         ListSchedules = _context.SemestersSubject.Where(ss => ss.IdSubjectClass == c.IdClass)
                                         .Join(
                                             _context.Schedules,
                                             ss => ss.IdSchedule,
                                             sc => sc.IdSchedule,
                                             (ss,sc) => new
                                             {
                                                 IdSchedule = sc.IdSchedule,
                                                 Day = sc.Day,
                                                 IdTimeStart = sc.IdTimeStart,
                                                 IdTimeEnd = sc.IdTimeEnd,
                                             }
                                             )
                                            .Join(
                                             _context.LessonTime,
                                             sc => sc.IdTimeStart,
                                             l => l.IdTime,
                                             (sc,l) => new
                                             {
                                                 IdSchedule = sc.IdSchedule,
                                                 Day = sc.Day,
                                                 IdTimeStart = sc.IdTimeStart,
                                                 IdTimeEnd = sc.IdTimeEnd,
                                                 Name_Start = l.NameTime,
                                                 StartTime = l.StartTime,
                                             } 
                                             )
                                            .Join
                                            (
                                             _context.LessonTime,
                                             sc => sc.IdTimeEnd,
                                             l => l.IdTime,
                                             (sc,l) => new
                                             {
                                                 IdSchedule = sc.IdSchedule,
                                                 Day = sc.Day,
                                                 IdTimeStart = sc.IdTimeStart,
                                                 Name_Start = sc.Name_Start,
                                                 StartTime = sc.StartTime,
                                                 IdTimeEnd = sc.IdTimeEnd,
                                                 Name_End = l.NameTime,
                                                 EndTime = l.EndTime,
                                             }
                                            ).Select(result => new
                                            {
                                                result.IdSchedule,
                                                result.Day,
                                                result.IdTimeStart,
                                                result.Name_Start,
                                                result.StartTime,
                                                result.IdTimeEnd,
                                                result.Name_End,
                                                result.EndTime,
                                            })
                                         .ToList(),
                                     };
            return Ok(listClassSubject);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSemesterSubject(int id)
        {
            if (_context.SubjectClasses == null)
            {
                return NotFound();
            }
            var listClassSubject = await (from c in _context.SubjectClasses where c.IdClass == id
                                   select new
                                   {
                                       IdClass = c.IdClass,
                                       NameClass = c.NameClass,
                                       NumberClass = c.NumberClass,
                                       Semester = _context.Semesters.Where(se => se.IdSemester == c.IdSemester).Select(se => se.Semester).FirstOrDefault(),
                                       Subject = _context.Subjects.Where(su => su.IdSubject == c.IdSubject).Select(su => su.Name).FirstOrDefault(),
                                       SemesterDateStart = c.SemesterDateStart,
                                       SemesterDateEnd = c.SemesterDateEnd,
                                       NameTeacher = c.NameTeacher,
                                       ListSchedules = _context.SemestersSubject.Where(ss => ss.IdSubjectClass == c.IdClass)
                                       .Join(
                                           _context.Schedules,
                                           ss => ss.IdSchedule,
                                           sc => sc.IdSchedule,
                                           (ss, sc) => new
                                           {
                                               IdSchedule = sc.IdSchedule,
                                               Day = sc.Day,
                                               IdTimeStart = sc.IdTimeStart,
                                               IdTimeEnd = sc.IdTimeEnd,
                                           }
                                           )
                                          .Join(
                                           _context.LessonTime,
                                           sc => sc.IdTimeStart,
                                           l => l.IdTime,
                                           (sc, l) => new
                                           {
                                               IdSchedule = sc.IdSchedule,
                                               Day = sc.Day,
                                               IdTimeStart = sc.IdTimeStart,
                                               IdTimeEnd = sc.IdTimeEnd,
                                               Name_Start = l.NameTime,
                                               StartTime = l.StartTime,
                                           }
                                           )
                                          .Join
                                          (
                                           _context.LessonTime,
                                           sc => sc.IdTimeEnd,
                                           l => l.IdTime,
                                           (sc, l) => new
                                           {
                                               IdSchedule = sc.IdSchedule,
                                               Day = sc.Day,
                                               IdTimeStart = sc.IdTimeStart,
                                               Name_Start = sc.Name_Start,
                                               StartTime = sc.StartTime,
                                               IdTimeEnd = sc.IdTimeEnd,
                                               Name_End = l.NameTime,
                                               EndTime = l.EndTime,
                                           }
                                          ).Select(result => new
                                          {
                                              result.IdSchedule,
                                              result.Day,
                                              result.IdTimeStart,
                                              result.Name_Start,
                                              result.StartTime,
                                              result.IdTimeEnd,
                                              result.Name_End,
                                              result.EndTime,
                                          }).ToList(),
                                   }).FirstAsync();
            return Ok(listClassSubject);

        }
        [HttpGet("SeachingSemester/{id}")]
        public async Task<IActionResult> GetSubject(int id)
        {
            if (_context.SubjectClasses == null)
            {
                return NotFound();
            }
            var listSubject = await _context.SubjectClasses.Where(s => s.IdSemester == id).Join(
                _context.Subjects,
                s => s.IdSubject,
                su => su.IdSubject,
                (s, su) => new
                {
                    IdSubject = su.IdSubject,
                    subject = su.Name,
                }
                ).Select(result => new
                {
                    result.IdSubject,
                    result.subject,
                }).Distinct().ToListAsync();
            return Ok(listSubject);
        }
        [HttpGet("SeachingSubject/{id}")]
        public async Task<IActionResult> GetSeachingSubject(int id)
        {
            if (_context.SubjectClasses == null)
            {
                return NotFound();
            }
            var listClassSubject = await (from c in _context.SubjectClasses where c.IdSubject == id
                                          select new
                                          {
                                              IdClass = c.IdClass,
                                              NameClass = c.NameClass,
                                              NumberClass = c.NumberClass,
                                              Semester = _context.Semesters.Where(se => se.IdSemester == c.IdSemester).Select(se => se.Semester).FirstOrDefault(),
                                              Subject = _context.Subjects.Where(su => su.IdSubject == c.IdSubject).Select(su => su.Name).FirstOrDefault(),
                                              SemesterDateStart = c.SemesterDateStart,
                                              SemesterDateEnd = c.SemesterDateEnd,
                                              NameTeacher = c.NameTeacher,
                                              ListSchedules = _context.SemestersSubject.Where(ss => ss.IdSubjectClass == c.IdClass)
                                              .Join(
                                                  _context.Schedules,
                                                  ss => ss.IdSchedule,
                                                  sc => sc.IdSchedule,
                                                  (ss, sc) => new
                                                  {
                                                      IdSchedule = sc.IdSchedule,
                                                      Day = sc.Day,
                                                      IdTimeStart = sc.IdTimeStart,
                                                      IdTimeEnd = sc.IdTimeEnd,
                                                  }
                                                  )
                                                 .Join(
                                                  _context.LessonTime,
                                                  sc => sc.IdTimeStart,
                                                  l => l.IdTime,
                                                  (sc, l) => new
                                                  {
                                                      IdSchedule = sc.IdSchedule,
                                                      Day = sc.Day,
                                                      IdTimeStart = sc.IdTimeStart,
                                                      IdTimeEnd = sc.IdTimeEnd,
                                                      Name_Start = l.NameTime,
                                                      StartTime = l.StartTime,
                                                  }
                                                  )
                                                 .Join
                                                 (
                                                  _context.LessonTime,
                                                  sc => sc.IdTimeEnd,
                                                  l => l.IdTime,
                                                  (sc, l) => new
                                                  {
                                                      IdSchedule = sc.IdSchedule,
                                                      Day = sc.Day,
                                                      IdTimeStart = sc.IdTimeStart,
                                                      Name_Start = sc.Name_Start,
                                                      StartTime = sc.StartTime,
                                                      IdTimeEnd = sc.IdTimeEnd,
                                                      Name_End = l.NameTime,
                                                      EndTime = l.EndTime,
                                                  }
                                                 ).Select(result => new
                                                 {
                                                     result.IdSchedule,
                                                     result.Day,
                                                     result.IdTimeStart,
                                                     result.Name_Start,
                                                     result.StartTime,
                                                     result.IdTimeEnd,
                                                     result.Name_End,
                                                     result.EndTime,
                                                 })
                                              .ToList()
                                          }).ToListAsync();
            return Ok(listClassSubject);
        }
        [HttpGet("SeachingSubject/{idSubject}/{idSemester}")]
        public async Task<IActionResult> GetSeachingSubject(int idSubject,int idSemester)
        {
            if (_context.SubjectClasses == null)
            {
                return NotFound();
            }
            var listClassSubject = await (from c in _context.SubjectClasses
                                          where c.IdSubject == idSubject && c.IdSemester == idSemester
                                          select new
                                          {
                                              IdClass = c.IdClass,
                                              NameClass = c.NameClass,
                                              NumberClass = c.NumberClass,
                                              Semester = _context.Semesters.Where(se => se.IdSemester == c.IdSemester).Select(se => se.Semester).FirstOrDefault(),
                                              Subject = _context.Subjects.Where(su => su.IdSubject == c.IdSubject).Select(su => su.Name).FirstOrDefault(),
                                              SemesterDateStart = c.SemesterDateStart,
                                              SemesterDateEnd = c.SemesterDateEnd,
                                              NameTeacher = c.NameTeacher,
                                              ListSchedules = _context.SemestersSubject.Where(ss => ss.IdSubjectClass == c.IdClass)
                                              .Join(
                                                  _context.Schedules,
                                                  ss => ss.IdSchedule,
                                                  sc => sc.IdSchedule,
                                                  (ss, sc) => new
                                                  {
                                                      IdSchedule = sc.IdSchedule,
                                                      Day = sc.Day,
                                                      IdTimeStart = sc.IdTimeStart,
                                                      IdTimeEnd = sc.IdTimeEnd,
                                                  }
                                                  )
                                                 .Join(
                                                  _context.LessonTime,
                                                  sc => sc.IdTimeStart,
                                                  l => l.IdTime,
                                                  (sc, l) => new
                                                  {
                                                      IdSchedule = sc.IdSchedule,
                                                      Day = sc.Day,
                                                      IdTimeStart = sc.IdTimeStart,
                                                      IdTimeEnd = sc.IdTimeEnd,
                                                      Name_Start = l.NameTime,
                                                      StartTime = l.StartTime,
                                                  }
                                                  )
                                                 .Join
                                                 (
                                                  _context.LessonTime,
                                                  sc => sc.IdTimeEnd,
                                                  l => l.IdTime,
                                                  (sc, l) => new
                                                  {
                                                      IdSchedule = sc.IdSchedule,
                                                      Day = sc.Day,
                                                      IdTimeStart = sc.IdTimeStart,
                                                      Name_Start = sc.Name_Start,
                                                      StartTime = sc.StartTime,
                                                      IdTimeEnd = sc.IdTimeEnd,
                                                      Name_End = l.NameTime,
                                                      EndTime = l.EndTime,
                                                  }
                                                 ).Select(result => new
                                                 {
                                                     result.IdSchedule,
                                                     result.Day,
                                                     result.IdTimeStart,
                                                     result.Name_Start,
                                                     result.StartTime,
                                                     result.IdTimeEnd,
                                                     result.Name_End,
                                                     result.EndTime,
                                                 })
                                              .ToList()
                                          }).ToListAsync();
            return Ok(listClassSubject);
        }
        [HttpPost]
        public async Task<ActionResult> PostSemesterSubject(SemesterSubjectModel semesterSubject)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'MyDbContext.Students'  is null.");
            }
            var Subject = await _context.Subjects.FindAsync(semesterSubject.IdSubject);
            var Semester = await _context.Semesters.FindAsync(semesterSubject.IdSemester);
            if (Subject == null || Semester == null)
            {
                return Ok(value: "Khong dung ma khoa");
            }
            int datecheck = (semesterSubject.SemesterDateStart - Semester.StartDay).Days;
            if (datecheck < 0)
            {
                return Ok(value: "Khong dung hoc ky");
            }
            datecheck = (semesterSubject.SemesterDateEnd - semesterSubject.SemesterDateStart).Days;
            if (datecheck < 0)
            {
                return Ok(value: "Khong dung ngay ket thuc va bat dau");
            }
            if (semesterSubject.schedules.Count() == 0)
            {
                return (Ok(value: "Khong co ngay hoc"));
            }
            List<int> idSchedule = new List<int>();
            foreach (SchedulesModel i in semesterSubject.schedules)
            {
                var LessonTime_Start = await _context.LessonTime.FindAsync(i.IdTimeStart);
                var LessonTime_End = await _context.LessonTime.FindAsync(i.IdTimeEnd);
                if (LessonTime_End == null || LessonTime_Start == null)
                {
                    return (Ok(value: "Khong co tiet nay trong du lieu"));
                }    
                string[] start_time = _context.LessonTime.Where(lt => lt.IdTime == i.IdTimeStart).Select(lt => lt.StartTime).FirstOrDefault().Split(":");
                string[] end_time = _context.LessonTime.Where(lt => lt.IdTime == i.IdTimeEnd).Select(lt => lt.EndTime).FirstOrDefault().Split(":");
                if (int.Parse(start_time[0]) > int.Parse(end_time[0]))
                {
                    return Ok(value: "Khong dung tiet ket thuc va bat dau");
                }
                else if (int.Parse(start_time[0]) == int.Parse(end_time[0]) && int.Parse(start_time[1]) > int.Parse(end_time[1]))
                {
                    return Ok(value: "Khong dung tiet ket thuc va bat dau");
                }
                var checkSchedule = _context.Schedules.Where(sc => i.IdTimeStart == sc.IdTimeStart && i.IdTimeEnd == sc.IdTimeEnd && sc.Day == i.Day).Select(sc => sc.IdSchedule).FirstOrDefault();
                if (checkSchedule == 0)
                {
                    var Schedule = new Schedules
                    {
                        Day = i.Day,
                        IdTimeStart = i.IdTimeStart,
                        IdTimeEnd = i.IdTimeEnd,
                    };
                    _context.Schedules.Add(Schedule);
                    await _context.SaveChangesAsync();
                    idSchedule.Add(Schedule.IdSchedule);
                }
                else
                {
                    int Saveid = checkSchedule;
                    idSchedule.Add(Saveid);
                }
            }
            var SubjectClass = new SubjectsClass
            {
                NameClass = semesterSubject.NameClass,
                NumberClass = semesterSubject.NumberClass,
                IdSubject = semesterSubject.IdSubject,
                IdSemester = semesterSubject.IdSemester,
                SemesterDateStart = semesterSubject.SemesterDateStart,
                SemesterDateEnd = semesterSubject.SemesterDateEnd,
                NameTeacher = semesterSubject.nameTeacher,
            };
            _context.SubjectClasses.Add( SubjectClass );
            await _context.SaveChangesAsync();
            var idClass = SubjectClass.IdClass;
            foreach (int i in idSchedule)
            {
                var SemesterSubject = new SemestersSubject
                {
                    IdSubjectClass = idClass,
                    IdSchedule = i,
                };
                _context.SemestersSubject.Add(SemesterSubject);
                await _context.SaveChangesAsync();
            }
            return (Ok(value: "Them thanh cong"));
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateSemesterSubject(int id,SemesterSubjectModel semesterSubject)
        {
            if (id != semesterSubject.IdClass)
            {
                return BadRequest();
            }
            var SubjectClass = await _context.SubjectClasses.FindAsync(id);
            if(SubjectClass == null)
            {
                return NotFound();
            }    
            var Subject = await _context.Subjects.FindAsync(semesterSubject.IdSubject);
            var Semester = await _context.Semesters.FindAsync(semesterSubject.IdSemester);
            if (Subject == null || Semester == null)
            {
                return Ok(value: "Khong dung ma khoa");
            }
            int datecheck = (semesterSubject.SemesterDateEnd.Date - semesterSubject.SemesterDateStart.Date).Days;
            if (datecheck < 0)
            {
                return Ok(value: "Khong dung ngay ket thuc va bat dau");
            }
            if (semesterSubject.schedules.Count() == 0)
            {
                return (Ok(value: "Khong co ngay hoc"));
            }
            List<int> idSchedule = new List<int>();
            foreach (SchedulesModel i in semesterSubject.schedules)
            {
                var LessonTime_Start = await _context.LessonTime.FindAsync(i.IdTimeStart);
                var LessonTime_End = await _context.LessonTime.FindAsync(i.IdTimeEnd);
                if (LessonTime_End == null || LessonTime_Start == null)
                {
                    return (Ok(value: "Khong co tiet nay trong du lieu"));
                }
                string[] start_time = _context.LessonTime.Where(lt => lt.IdTime == i.IdTimeStart).Select(lt => lt.StartTime).FirstOrDefault().Split(":");
                string[] end_time = _context.LessonTime.Where(lt => lt.IdTime == i.IdTimeEnd).Select(lt => lt.EndTime).FirstOrDefault().Split(":");
                if (int.Parse(start_time[0]) > int.Parse(end_time[0]))
                {
                    return Ok(value: "Khong dung tiet ket thuc va bat dau");
                }
                else if (int.Parse(start_time[0]) == int.Parse(end_time[0]) && int.Parse(start_time[1]) > int.Parse(end_time[1]))
                {
                    return Ok(value: "Khong dung tiet ket thuc va bat dau");
                }
                var checkSchedule = _context.Schedules.Where(sc => i.IdTimeStart == sc.IdTimeStart && i.IdTimeEnd == sc.IdTimeEnd && sc.Day == i.Day).Select(sc => sc.IdSchedule).FirstOrDefault();
                if (checkSchedule == 0)
                {
                    var Schedule = new Schedules
                    {
                        Day = i.Day,
                        IdTimeStart = i.IdTimeStart,
                        IdTimeEnd = i.IdTimeEnd,
                    };
                    _context.Schedules.Add(Schedule);
                    await _context.SaveChangesAsync();
                    idSchedule.Add(Schedule.IdSchedule);
                }
                else
                {
                    int Saveid = checkSchedule;
                    idSchedule.Add(Saveid);
                }
            }
            SubjectClass.NameClass = semesterSubject.NameClass;
            SubjectClass.NumberClass = semesterSubject.NumberClass;
            SubjectClass.IdSubject = semesterSubject.IdSubject;
            SubjectClass.IdSemester = semesterSubject.IdSemester;
            SubjectClass.SemesterDateStart = semesterSubject.SemesterDateStart;
            SubjectClass.SemesterDateEnd = semesterSubject.SemesterDateEnd;
            await _context.SaveChangesAsync();
            var idClass = SubjectClass.IdClass;
            var idSemesterSubject = _context.SemestersSubject.Where(ss => ss.IdSubjectClass == semesterSubject.IdClass).Select(ss => ss.IdSemesterSubject).ToList();
            if (idSchedule.Count < idSemesterSubject.Count)
            {
                for (int i = idSemesterSubject.Count - 1; i >= idSchedule.Count; i--) 
                {
                    var deleteSemesterSubject = await _context.SemestersSubject.FindAsync(idSemesterSubject[i]);
                    _context.SemestersSubject.Remove(deleteSemesterSubject);
                    await _context.SaveChangesAsync();
                }
            }
            idSemesterSubject = _context.SemestersSubject.Where(ss => ss.IdSubjectClass == semesterSubject.IdClass).Select(ss => ss.IdSemesterSubject).ToList();
            for (int i = 0; i <  idSchedule.Count; i++)
            {
                if (i <=  idSemesterSubject.Count - 1)
                {
                    var SemesterSuject = await _context.SemestersSubject.FindAsync(idSemesterSubject[i]);
                    SemesterSuject.IdSchedule = idSchedule[i];
                    await _context.SaveChangesAsync();
                }
                else
                {
                    var SemesterSubject = new SemestersSubject
                    {
                        IdSubjectClass = idClass,
                        IdSchedule = idSchedule[i],
                    };
                    _context.SemestersSubject.Add(SemesterSubject);
                    await _context.SaveChangesAsync();
                }
            }
            return (Ok(value: "Sua thanh cong"));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFacultiesSemesterSubject(int id)
        {
            var ClassSubject = await _context.SubjectClasses.FindAsync(id);
            if (ClassSubject == null)
            {
                return NotFound();
            }
            var SemesterSubject = _context.SemestersSubject.Where(ss => ss.IdSubjectClass == id).ToList();
            for  (int i = 0; i < SemesterSubject.Count; i++)
            {
                _context.SemestersSubject.Remove(SemesterSubject[i]);
                await _context.SaveChangesAsync();
            }
            _context.SubjectClasses.Remove(ClassSubject);
            await _context.SaveChangesAsync();
            return Ok(value: "Xoa thanh cong");
        }
        [HttpGet("SeachingSemester/{idSemester}/{idStudent}")]
        public async Task<IActionResult> GetSubjectStudent(int idSemester, int idStudent)
        {
            if (_context.SubjectClasses == null)
            {
                return NotFound();
            }
            var listSubject = await _context.Students.Where(s => s.IdStudent == idStudent).Join(
                _context.Grades,
                s => s.IdGrade,
                su => su.IdGrade,
                (s, su) => new
                {
                    idFaculty = su.idFaculty,
                }
                ).Join(
                _context.Subjects,
                s => s.idFaculty,
                su => su.IdFaclty,
                (s,su) => new
                {
                    idSubject = su.IdSubject,
                    subject = su.Name,
                }
                ).Join(
                _context.SubjectClasses,
                s => s.idSubject,
                su => su.IdSubject,
                (s,su) => new {
                    IdSubject = s.idSubject,
                    subject = s.subject,
                    Idsemester = su.IdSemester,
                }).Where(result => result.Idsemester == idSemester)
                .Select(result => new
                {
                    result.IdSubject,
                    result.subject,
                }).Distinct().ToListAsync();
            return Ok(listSubject);
        }
    }
}
