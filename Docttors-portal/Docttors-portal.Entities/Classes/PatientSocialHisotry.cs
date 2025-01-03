using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("PatientSocialHisotry")]
    public class PatientSocialHisotry : BaseClass
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int IsSmoke { get; set; }
        public int IsDrugs { get; set; }
        public int IsAlcohol { get; set; }
        public string Other { get; set; }
    }
}
