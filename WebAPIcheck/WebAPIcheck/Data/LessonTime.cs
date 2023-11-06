using System.ComponentModel.DataAnnotations;

namespace WebAPIcheck.Data
{
    public class LessonTime
    {
        [Key]
        public int IdTime { get; set; }
        [Required]
        public string NameTime { get; set; }
        [Required]
        public string StartTime { get; set; }
        [Required]
        public string EndTime { get; set; }
    }
}
