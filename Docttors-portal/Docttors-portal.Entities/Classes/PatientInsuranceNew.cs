using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docttors_portal.Entities.Classes
{
    [Table("PatientInsurancesNew")]
    public class PatientInsuranceNew : BaseClass
    {
        [Key]
        public int PatientInsuranceId { get; set; }
        public string NameOfInsured { get; set; }
        public string RelationshipToPatient { get; set; }
        public string InsuranceIdNumber { get; set; }
        public string InsuranceGroupId { get; set; }
        public string InsuranceCompanyName { get; set; }
        public int UserId { get; set; }
        public string InsuranceCompanyCity { get; set; }
        public string InsuranceCompanyAddress { get; set; }
        public string InsuranceCompanyWebsite { get; set; }
        public int InsuranceCompanyStateId { get; set; }
        public string InsuranceCompanyZipcode { get; set; }
        public int InsuranceCompanyCountryId { get; set; }
        public string InsuranceCompanyPhone { get; set; }
        public string InsuranceCompanyFax { get; set; }
        public DateTime? EligibilityStartDate { get; set; }
        public DateTime? EligibilityEndDate { get; set; }

       }
}
