using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docttors_portal.Entities.Classes
{
    [Table("SpecialistTypes")]
    public class SpecialistTypes
    {
        [Key]
        public int Id { get; set; }
        public string SpecialistType { get; set; }
        public bool Isactive { get; set; }
        public int HospitalId { get; set; }

    }
}
