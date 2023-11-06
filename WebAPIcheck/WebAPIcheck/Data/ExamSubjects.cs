using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIcheck.Data
{
    public class ExamSubjects
    {
        [Key]
        public int IdExam { get; set; }
        public int? IdStartTime { get; set; }
        public int? IdEndTime { get; set; }
        public int IdReSubject { get; set; }
        public string Status { get; set; }
        [ForeignKey("IdReSubject")]
        public ReSubjects ReSubject { get; set; }
    }
}
