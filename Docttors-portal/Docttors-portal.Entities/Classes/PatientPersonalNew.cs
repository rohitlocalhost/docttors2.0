using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docttors_portal.Entities.Classes
{
    [Table("PatientPersonalNew")]
    public class PatientPersonalNew : BaseClass
    {
        [Key]
        public int PatientPersonalId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string WorkPhone { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public int? ZipCode { get; set; }
        public int GenderId { get; set; }
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public string SSN { get; set; }
        public int Weight { get; set; }
        public string MRN { get; set; }
        public int? MaritalStatusId { get; set; }
        public int? EducationId { get; set; }
        public int? HeightId { get; set; }
        public int? EthnicityId { get; set; }
        public string EmailId { get; set; }
        public bool Ispatient { get; set; }
        public bool IsGuardian { get; set; }
        public string GuardinshipReason { get; set; }
    }
}
