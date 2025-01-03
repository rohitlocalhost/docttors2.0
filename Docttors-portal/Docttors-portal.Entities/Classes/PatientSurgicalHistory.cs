using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("PatientSurgicalHistory")]
    public class PatientSurgicalHistory :  BaseClass
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int IsTonsils { get; set; }
        public int IsAppendix { get; set; }
        public int IsGallbladder { get; set; }
        public int IsStent { get; set; }
        public int IsHeartByPass { get; set; }
        public int IsHysterectomy { get; set; }
        public int IsCataracts { get; set; }
    }
}
