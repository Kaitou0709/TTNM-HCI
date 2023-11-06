using System.ComponentModel.DataAnnotations.Schema;
using WebAPIcheck.Data;

namespace WebAPIcheck.Models
{
    public class SemesterSubjectModel
    {
        public int IdClass { get; set; } 
        public string NameClass { get; set; }
        public int NumberClass { get; set; }
        public int IdSubject { get; set; }
        public int IdSemester { get; set; }
        public DateTime SemesterDateStart { get; set; }
        public DateTime SemesterDateEnd { get; set; }
        public SchedulesModel[] schedules { get; set; }
        public string nameTeacher { get; set; }
     
    }
}
