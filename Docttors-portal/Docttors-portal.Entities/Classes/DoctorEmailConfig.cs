using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("DoctorEmailConfig")]
    public class DoctorEmailConfig : BaseClass
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string DoctorEmail { get; set; }
        public string NurseEmail { get; set; }
        public string LaboratoryEmail { get; set; }
        public string BillingEmail { get; set; }
        public string DiagonsticEmail { get; set; }
    }
}
