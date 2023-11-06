using System.ComponentModel.DataAnnotations;

namespace WebAPIcheck.Data
{
    public class Faculties
    {
        [Key]
        public int IdFaculty { get; set; }
        public string Name { get; set; }

    }
}
