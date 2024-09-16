using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Docttors_portal.Common.Models
{
    public class PatientAllergiesModel
    {
        public int PatientAllergyId { get; set; }
        public int UserId { get; set; }
        [DisplayName("Allergy Name")]
        [Required(ErrorMessage = "Allergy Name is required")]
        public string AllergyName { get; set; }
        public string Reaction { get; set; }
        public bool IsNone { get; set; }
    }
}
