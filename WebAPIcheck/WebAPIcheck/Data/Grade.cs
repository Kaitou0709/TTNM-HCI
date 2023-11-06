using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIcheck.Data
{
    public class Grade
    {
        [Key]
        public int IdGrade { get; set; }
        public string Name { get; set; }
        public int IdTeacher { get; set; }
        public int idFaculty { get; set; }
        [ForeignKey("IdTeacher")]
        public Teachers Teacher { get; set; }

    }
}
