using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("DoctorContact")]
    public class DoctorContact : BaseClass
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DoctorName { get; set; }
        public string practiceName { get; set; }
        public string Address { get; set; }
        public int Phone { get; set; }
        public string FaxNumber { get; set; }
        public string OfficeEmail { get; set; }
        public string Office1_DoctorName { get; set; }
        public string Office1_practiceName { get; set; }
        public string Office1_Address { get; set; }
        public int Office1_Phone { get; set; }
        public string Office1_FaxNumber { get; set; }
        public string Office1_OfficeEmail { get; set; }
        public string Office2_DoctorName { get; set; }
        public string Office2_practiceName { get; set; }
        public string Office2_Address { get; set; }
        public int Office2_Phone { get; set; }
        public string Office2_FaxNumber { get; set; }
        public string Office2_OfficeEmail { get; set; }
        public string Office3_DoctorName { get; set; }
        public string Office3_practiceName { get; set; }
        public string Office3_Address { get; set; }
        public int Office3_Phone { get; set; }
        public string Office3_FaxNumber { get; set; }
        public string Office3_OfficeEmail { get; set; }

    }
}
