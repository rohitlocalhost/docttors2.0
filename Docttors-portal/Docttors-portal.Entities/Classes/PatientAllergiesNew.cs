using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docttors_portal.Entities.Classes
{
    [Table("PatientAllergiesNew")]
    public class PatientAllergiesNew : BaseClass
    {
        [Key]
        public int PatientAllergyId { get; set; }
        public string AllergyName { get; set; }
        public string Reaction { get; set; }
        public int UserId { get; set; }
    }
}
