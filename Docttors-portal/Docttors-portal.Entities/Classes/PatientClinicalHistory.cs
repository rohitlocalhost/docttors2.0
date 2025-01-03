using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("PatientClinicalHistory")]
    public class PatientClinicalHistory : BaseClass
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int IsCancer { get; set; }
        public int IsDiabetes { get; set; }
        public int IsBloodPressure { get; set; }
        public int IsHeartDisease { get; set; }
        public int IsAlcoholAbuse { get; set; }
        public int IsDrugAbuse { get; set; }
    }
}
