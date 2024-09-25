using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Docttors_portal.Common.Models
{
    public class PatientPharmacyModel
    {
        public int PatientPharmacyId { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage ="Pharmacy Name is Required")]
        public string Name { get; set; }
        public string Phone { get; set; }
        [Display(Name ="Address")]
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public bool IsNone { get; set; }
        [Display(Name = "Primary?")]
        public bool IsPrimaryPharmacy { get; set; }
        public string City { get; set; }
        [Display(Name = "State")]
        public int StateId { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        public List<NameIdModel> StateList { get; set; }
        public List<PatientPharmacyModel> AllPharmacyData { get; set; }

    }
}
