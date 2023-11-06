using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIcheck.Data
{
    public class Tuitionfee
    {
        [Key]
        public int IdTutionFee { get; set; }
        public int IdStudent { get; set; }  
        public int IdSemester { get; set; }
        public int MoneyTuition { get; set; }
        public int MoneyPaid { get; set; }
        [ForeignKey(nameof(IdStudent))]
        public Students Students { get; set; }
        [ForeignKey(nameof(IdSemester))]
        public Semesters Semesters { get; set; }


    }
}
