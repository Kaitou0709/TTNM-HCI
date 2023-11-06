using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIcheck.Data
{
    public class ReSubjects
    {
        [Key]
        public int IdReSubject { get; set; }
        public int IdSubject { get; set; }
        public int IdStudent { get; set; }
        [ForeignKey("IdSubject")]
        public SubjectsClass SubjectClass { get; set; }
        [ForeignKey("IdStudent")]
        public Students Students { get; set; }
    }
}
