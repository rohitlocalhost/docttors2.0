using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Docttors_portal.Common.Models
{
    public class PatientEmergencyModel
    {
        public int PatientEmergencyId { get; set; }
        public int UserId { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Display(Name = "Relationship Name")]
        [Required(ErrorMessage = "Relationship Name is required")]
        public string Relationship { get; set; }
        [Display(Name = "Address Name")]
        [Required(ErrorMessage = "Address Name is required")]
        public string Address { get; set; }
        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Display(Name = "State")]
        [Required(ErrorMessage = "State is required")]
        public int StateId { get; set; }
        [Display(Name = "Zipcode")]
        [Required(ErrorMessage = "Zipcode is required")]
        public string Zipcode { get; set; }
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Country is required")]
        public int CountryId { get; set; }
        [Display(Name = "EmailId")]
        [Required(ErrorMessage = "EmailId is required")]
        public string EmailId { get; set; }
        [Display(Name = "ContactNo")]
        [Required(ErrorMessage = "ContactNo is required")]
        public string ContactNo { get; set; }
        public string SecondaryFirstName { get; set; }
        public string SecondaryLastName { get; set; }
        public string SecondaryRelationship { get; set; }
        public string SecondaryAddress { get; set; }
        public string SecondaryCity { get; set; }
        public int? SecondaryStateId { get; set; }
        public string SecondaryZipcode { get; set; }
        public int? SecondaryCountryId { get; set; }
        public string SecondaryEmailId { get; set; }
        public string SecondaryContactNo { get; set; }
        public List<NameIdModel> StateList { get; set; }
        public List<NameIdModel> CountryList { get; set; }
    }
}
