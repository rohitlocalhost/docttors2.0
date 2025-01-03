using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("PatientChronicConditions")]
    public class PatientChronicConditions : BaseClass
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ChronicConditionId { get; set; }
        public string ChronicConditionText { get; set; }
        //public int AlzheimerId { get; set; }
        //public string AlzheimerText { get; set; }
        //public int AllergyId { get; set; }
        //public string AllergyText { get; set; }
        //public int ArthritisId { get; set; }
        //public string ArthritisText { get; set; }
        //public int AsthmaId { get; set; }
        //public string AsthmaText { get; set; }
        //public int BipolarId { get; set; }
        //public string BipolarText { get; set; }
        //public int CancerId { get; set; }
        //public string CancerText { get; set; }
        //public int CrohnsId { get; set; }
        //public string CrohnsText { get; set; }
        //public int CholesterolId { get; set; }
        //public string CholesterolText { get; set; }
        //public int EmphysemaId { get; set; }
        //public string EmphysemaText { get; set; }
        //public int DepressionId { get; set; }
        //public string DepressionText { get; set; }
        //public int DiabetesId { get; set; }
        //public string DiabetesText { get; set; }
        //public int STDId { get; set; }
        //public string STDText { get; set; }
        //public int EpilepsyId { get; set; }
        //public string EpilepsyText { get; set; }
        //public int KidneyId { get; set; }
        //public string KidneyText { get; set; }
        //public int HeartDiseaseId { get; set; }
        //public string HeartDiseaseText { get; set; }
        //public int HIVId { get; set; }
        //public string HIVText { get; set; }
        //public int ThyroidId { get; set; }
        //public string ThyroidText { get; set; }
        //public int ObesityId { get; set; }
        //public string ObesityText { get; set; }
        //public int ParkinsonId { get; set; }
        //public string ParkinsonText { get; set; }
        //public int SickleId { get; set; }
        //public string SickleText { get; set; }
        //public int StrokeId { get; set; }
        //public string StrokeText { get; set; }
        //public int OtherId { get; set; }
        //public string OtherText { get; set; }

    }
}
