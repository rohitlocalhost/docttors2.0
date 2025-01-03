using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("OnlineVisitStep1")]
    public class OnlineVisitStep1 : BaseClass
    {
        [Key]
        public int VisitId { get; set; }
        public bool IsTncChecked { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int ServiceType { get; set; }
    }
}
