using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Common.Models
{
    public class PatientSearchDoctorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Display(Name = "State")]
        public int StateId { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        public string City { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Specialty")]
        public int? PhysicianSpecialtyId { get; set; }
        public List<NameIdModel> StateList { get; set; }
        public List<NameIdModel> SpecialistTypesList { get; set; }
    }
}
