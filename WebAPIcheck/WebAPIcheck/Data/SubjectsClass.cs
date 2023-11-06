using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIcheck.Data
{
    public class SubjectsClass
    {
        [Key]
        public int IdClass { get; set; }
        public string NameClass { get; set; }
        public int NumberClass { get; set; }
        public int IdSubject { get; set; }
        public int IdSemester { get; set; }
        public DateTime SemesterDateStart { get; set; }
        [Required]
        public DateTime SemesterDateEnd { get; set; }
        [ForeignKey("IdSubject")]
        public Subjects Subjects { get; set; }
        [ForeignKey("IdSemester")]
        public Semesters Semesters { get; set; }
        public string NameTeacher { get;set; }
    }
}
