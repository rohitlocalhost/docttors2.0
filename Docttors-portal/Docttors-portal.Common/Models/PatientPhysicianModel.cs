using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Docttors_portal.Common.Models
{
    public class PatientPhysicianModel
    {        
        public int PatientPhysicianId { get; set; }
        public int UserId { get; set; }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string PhysicianFName { get; set; }
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string PhysicianLName { get; set; }
        [DisplayName("Specialty")]
        public int? PhysicianSpecialtyId { get; set; }
        [DisplayName("Practice Name")]
        public string PracticeName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        [DisplayName("State")]
        public int StateId { get; set; }
        [DisplayName("Zip Code")]
        public string ZipCode { get; set; }
        public string Phone { get; set; }
        [DisplayName("None")]
        public bool IsNone { get; set; }

        public List<NameIdModel> StateList { get; set; }
        public List<NameIdModel> SpecialistTypesList { get; set; }
    }
}
