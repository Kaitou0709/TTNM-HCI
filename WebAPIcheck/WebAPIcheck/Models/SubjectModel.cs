using MessagePack;
using System.ComponentModel.DataAnnotations;

namespace WebAPIcheck.Models
{
    public class SubjectModel
    {
       public int IdSubject { get; set; }
       public string Name { get; set; }
        public int Fee { get; set; }
       public int IdFaclty { get; set; }
    }
}
