using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("PatientReviewOfSystem")]
    public class PatientReviewOfSystem : BaseClass
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int GeneralCondition { get; set; }
        public int Diabetes { get; set; }
        public int Stomach { get; set; }
        public int Urinary { get; set; }
        public int Neurological { get; set; }
        public int Cardiovascular { get; set; }
        public int Respiratory { get; set; }
        public int Eyes { get; set; }
        public int Ear { get; set; }
        public int ObOrGyn { get; set; }
        public int MusclesOrJoints { get; set; }
        public int Skin { get; set; }
        public int Hematology { get; set; }
        public int Dental { get; set; }
        public int Psychological { get; set; }
    }
}
