namespace WebAPIcheck.Models
{
    public class TeachersModel
    {
        public int IdTeacher { get; set; }
        public String NameTeacher { get; set; }
        public int Salary { get; set; }
        public string Position { get; set; }
        public int? IdFaculty { get; set; }
    }
}
