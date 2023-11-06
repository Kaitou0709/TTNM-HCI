using System.ComponentModel.DataAnnotations;

namespace WebAPIcheck.Data
{
    public class Semesters
    {
        [Key]   
        public int IdSemester { get; set; }
        [Required]
        public string Semester { get; set; }
        [Required]
        public DateTime StartDay { get; set; }
    }
}
