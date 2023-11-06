using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIcheck.Data
{
    public class Teachers
    {
        [Key]
        public int IdTeacher { get; set; }
        public String NameTeacher { get; set; }
        public int Salary { get; set; }
        public string Position { get; set; }
        public int? IdFaculty { get; set; }
        [ForeignKey("IdFaculty")]
        public Faculties Faculties { get; set; }
    }
}
