using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("PatientEmergencyNew")]
    public class PatientEmergencyNew : BaseClass
    {
        [Key]
        public int PatientEmergencyId { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Relationship { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string Zipcode { get; set; }
        public int CountryId { get; set; }
        public string EmailId { get; set; }
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

    }
}
