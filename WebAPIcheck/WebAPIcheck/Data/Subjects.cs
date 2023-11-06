using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIcheck.Data
{
    public class Subjects
    {
        [Key]
        public int IdSubject { get; set; }
        public string Name { get; set; }
        public int Fee { get; set; }    
        public int IdFaclty { get; set; }
        [ForeignKey("IdFaclty")]
        public Faculties Faculties { get; set; }
    }
}
