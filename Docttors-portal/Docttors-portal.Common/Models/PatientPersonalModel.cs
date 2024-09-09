using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Common.Models
{
    public class PatientPersonalModel
    {
        public int PatientPersonalId { get; set; }
        public int UserId { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Display(Name = "Middile Name")]
        public string MiddleName { get; set; }
        [Display(Name = "Work Phone")]
        public string WorkPhone { get; set; }
        [Display(Name = "Contact No")]
        public string ContactNo { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Display(Name = "State")]

        [Required(ErrorMessage = "State is required")]
        public int StateId { get; set; }
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }
        public int CountryId { get; set; }
        [Display(Name = "Gender")]
        [Required(ErrorMessage = "Gender is required")]
        public int GenderId { get; set; }
        [Display(Name = "Date Of Birth")]
        [Required(ErrorMessage = "DOB is required")]
        //[DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string DOB { get; set; }
        [Required(ErrorMessage = "SSN is required")]
        public string SSN { get; set; }
        [Required(ErrorMessage = "Weight is required")]
        public int Weight { get; set; }
        [Required(ErrorMessage = "MRN is required")]
        public string MRN { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? EducationId { get; set; }
        [Display(Name = "Height")]
        public int? HeightId { get; set; }
        public int? EthnicityId { get; set; }
        public string EmailId { get; set; }
        public bool? Ispatient { get; set; }
        public bool? IsGuardian { get; set; }
        public string GuardinshipReason { get; set; }

        public List<NameIdModel> MaritalMasterList { get; set; }
        public List<NameIdModel> GenderList { get; set; }
        public List<NameIdModel> StateList { get; set; }
        public List<NameIdModel> CountryList { get; set; }
        public List<NameIdModel> EducationMasterList { get; set; }
        public List<NameIdModel> EthnicityList { get; set; }
        public List<NameIdModel> HeightList { get; set; }
    }
}
