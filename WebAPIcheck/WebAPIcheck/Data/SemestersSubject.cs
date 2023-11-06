using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIcheck.Data
{
    public class SemestersSubject
    {
        [Key]
        public int IdSemesterSubject { get; set; }
        [Required]
        public int IdSubjectClass { get; set; }
        [Required]
        public int IdSchedule { get; set; }
        [ForeignKey("IdSubjectClass")]
        public SubjectsClass SubjectsClass { get; set; }
        [ForeignKey("IdSchedule")]
        public Schedules Schedules { get; set; }
        

    }
}
