using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIcheck.Data
{
    public class Schedules
    {
        [Key]
        public int IdSchedule { get; set; }
        public string Day { get; set; }
        [Required]
        public int IdTimeStart { get; set; }
        [Required] 
        public int IdTimeEnd { get; set; }
        [ForeignKey("IdTimeStart")]
        public LessonTime LessonTimeStart { get; set; }
        [ForeignKey("IdTimeEnd")]
        public LessonTime LessonTimeEnd { get; set; }
    }
}
