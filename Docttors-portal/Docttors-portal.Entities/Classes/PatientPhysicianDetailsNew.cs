using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docttors_portal.Entities.Classes
{
    [Table("PatientPhysicianDetailsNew")]
    public class PatientPhysicianDetailsNew : BaseClass
    {
        [Key]
        public int PatientPhysicianId { get; set; }
        public int UserId { get; set; }
        public string PhysicianFirstName { get; set; }
        public string PhysicianLastName { get; set; }
        public int? PhysicianSpecialtyId { get; set; }
        public string PracticeName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string ZipCode { get; set; }
        public string Phone { get; set; }

    }
}
