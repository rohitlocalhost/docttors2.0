using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Docttors_portal.Common.Models
{
    public class PatientInsuranceModel
    {
        public int PatientInsuranceId { get; set; }
        public bool IsNone { get; set; }
        [DisplayName("Name of Insured")]
        [Required(ErrorMessage = "Name of Insured is required")]
        public string NameOfInsured { get; set; }
        [DisplayName("Relationship to Insured")]
        [Required(ErrorMessage = "Relationship to Insured is required")]
        public string RelationshipToPatient { get; set; }  
        [DisplayName("Insurance ID Number")]
        [Required(ErrorMessage = "Insurance ID Number is required")]
        public string InsuranceIdNumber { get; set; }
        [DisplayName("Insurance Group ID")]
        [Required(ErrorMessage = "Insurance Group ID is required")]
        public string InsuranceGroupId { get; set; }
        [DisplayName("Insurance Company Name")]
        [Required(ErrorMessage = "Insurance Company Name is required")]
        public string InsuranceCompanyName { get; set; }  
        public int UserId { get; set; }
        [DisplayName("Company Address")]
        public string InsuranceCompanyAddress { get; set; }        
        [DisplayName("Company Website")]
        public string InsuranceCompanyWebsite { get; set; }
        [DisplayName("City")]
        public string InsuranceCompanyCity { get; set; }
        [DisplayName("State")]
        public int InsuranceCompanyStateId { get; set; }
        [DisplayName("Zip Code")]
        public string InsuranceCompanyZipcode { get; set; }
        [DisplayName("Country")]
        public int InsuranceCompanyCountryId { get; set; }
        [DisplayName("Phone Number")]
        public string InsuranceCompanyPhone { get; set; }
        [DisplayName("Fax Number")]
        public string InsuranceCompanyFax { get; set; }
        [DisplayName("Eligibility Start Date")]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string EligibilityStartDate { get; set; }
        [DisplayName("Eligibility End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string EligibilityEndDate { get; set; }

        public List<NameIdModel> StateList { get; set; }
        public List<NameIdModel> CountryList { get; set; }
    }
}
