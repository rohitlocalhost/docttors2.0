using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("PatientFavoriteDoctor")]
    public class PatientFavoriteDoctor : BaseClass
    {
        [Key]
        public int FavoriteId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public bool IsprimaryDoctor { get; set; }
    }
}
