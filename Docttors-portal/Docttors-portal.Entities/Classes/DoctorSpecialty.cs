using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("DoctorSpecialty")]
    public class DoctorSpecialty
    {
        [Key]
        public int SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        public bool SpecialtyId_Old { get; set; }
    }
}
