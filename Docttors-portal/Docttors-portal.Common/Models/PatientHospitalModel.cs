using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Docttors_portal.Common.Models
{
    public class PatientHospitalModel
    {
        public int PatientHospitalId { get; set; }
        public int UserId { get; set; }
        [Display(Name = "Hospital Name")]
        [Required(ErrorMessage = "Hospital Name is required")]
        public string HospitalName { get; set; }
        public string Phone { get; set; }
        [Display(Name = "Address")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public bool IsNone { get; set; }
        public string City { get; set; }
        [Display(Name = "State")]
        public int StateId { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        [Display(Name = "Reason For Visit")]
        public string ReasonForVisit { get; set; }
        public List<NameIdModel> StateList { get; set; }
        public List<PatientHospitalModel> HospitalList { get; set; }

    }
}
