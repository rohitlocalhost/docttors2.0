using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docttors_portal.Entities.Classes
{
    [Table("PatientHospitalDetailsNew")]
    public class PatientHospitalDetailsNew : BaseClass
    {
        [Key]
        public int PatientHospitalId { get; set; }
        public int UserId { get; set; }
        public string HospitalName { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string ZipCode { get; set; }
        public string ReasonForVisit { get; set; }
    }
}
