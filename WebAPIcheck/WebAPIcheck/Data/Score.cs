using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIcheck.Data
{
    public class Score
    {
        [Key] 
        public int IdScore { get; set; }
        public int IdReSubject { get; set; }
        public string Status { get; set; }
        public int? ScoreSubject { get; set; }
        [ForeignKey("IdReSubject")]
        public ReSubjects ReSubjects { get; set; }

    }
}
