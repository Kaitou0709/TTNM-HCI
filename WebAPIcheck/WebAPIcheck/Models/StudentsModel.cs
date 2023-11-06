using System.ComponentModel.DataAnnotations;

namespace WebAPIcheck.Models
{
    public class StudentsModel
    {
        public int IdStudent { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(5)]
        public string Gender { get; set; }

        [Required]
        [MaxLength(100)]
        public string Address { get; set; }
        [Required]
        [MaxLength(40)]
        public string Country { get; set; }
        [Required]
        public DateTime BirthDay { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }
        public int IdGrade { get; set; }
    }
}
