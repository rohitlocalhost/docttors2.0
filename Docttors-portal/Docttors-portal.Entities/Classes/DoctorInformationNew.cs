using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("DoctorInformationNew")]
    public class DoctorInformationNew : BaseClass
    {
        [Key]
        public int DoctorInfoId { get; set; }
        public int UserId { get; set; }
        public string AboutDoctor { get; set; }
        public string Affilation { get; set; }
        public string School { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public DateTime GradutionDate { get; set; }
        public string Languages { get; set; }
        public string InsuranceAdded { get; set; }
        public string ServicesAdded { get; set; }
        public string DoctorAdded { get; set; }
        public string Message { get; set; }
    }
}
