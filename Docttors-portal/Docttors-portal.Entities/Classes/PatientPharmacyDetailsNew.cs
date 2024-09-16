using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docttors_portal.Entities.Classes
{
    [Table("PatientPharmacyDetailsNew")]
    public class PatientPharmacyDetailsNew : BaseClass
    {
        [Key]
        public int PatientPharmacyId { get; set; }
        public int UserId { get; set; }
        public string PharmacyName { get; set; }
        public string PhramacyPhone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string ZipCode { get; set; }
        public bool isPrimaryPharmacy { get; set; }
    }
}
