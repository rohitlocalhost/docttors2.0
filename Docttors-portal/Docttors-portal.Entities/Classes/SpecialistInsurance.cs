using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docttors_portal.Entities.Classes
{
    [Table("SpecialistInsurance")]
    public class SpecialistInsurance
    {
        [Key]
        public int Id { get; set; }
        public string InsuranceName { get; set; }
        public bool IsActive { get; set; }
    }
}
