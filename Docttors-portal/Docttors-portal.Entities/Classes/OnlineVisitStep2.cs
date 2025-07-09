using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("OnlineVisitStep2")]
    public class OnlineVisitStep2 : BaseClass
    {
        [Key]
        public int Step2VisitId { get; set; }
        public int PatientId { get; set; }
        public int step1Id { get; set; }
        public string A1 { get; set; }
        public string A2 { get; set; }
        public int A3 { get; set; }
        public string A4 { get; set; }
        public string A5 { get; set; }
        public int A6 { get; set; }
        public int A7 { get; set; }
        public int A8 { get; set; }
        public DateTime A9 { get; set; }
        public DateTime A10 { get; set; }
        public DateTime A11 { get; set; }
        public int DoctorId { get; set; }

    }
}
