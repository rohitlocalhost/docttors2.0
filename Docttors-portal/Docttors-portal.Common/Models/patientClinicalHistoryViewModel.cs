using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Common.Models
{
    public class PatientClinicalViewModel
    {
        public PatientClinicalViewModel()
        {
            this.allChronicConditions = new List<ChronicConditionsMasterViewModel>();
            this.patientChronicConditionsModel = new List<PatientChronicConditionsViewModel>();
            this.patientSocialHisotryModel = new PatientSocialHisotryViewModel();
            this.patientClinicalHistoryModel = new PatientClinicalHistoryViewModel();
            this.patientSurgicalHistoryModel = new PatientSurgicalHistoryViewModel();
            this.patientReviewOfSystemModel = new PatientReviewOfSystemViewModel();
            this.YesNoSelectionList = new List<NameIdModel>();
            this.PatientConditionInfo = new PatientConditionInfo();
        }
        public List<ChronicConditionsMasterViewModel> allChronicConditions { get; set; }
        public List<PatientChronicConditionsViewModel> patientChronicConditionsModel { get; set; }
        public PatientSocialHisotryViewModel patientSocialHisotryModel { get; set; }
        public PatientClinicalHistoryViewModel patientClinicalHistoryModel { get; set; }
        public PatientSurgicalHistoryViewModel patientSurgicalHistoryModel { get; set; }
        public PatientReviewOfSystemViewModel patientReviewOfSystemModel { get; set; }
        public List<NameIdModel> YesNoSelectionList { get; set; }
        public List<NameIdModel> YesNoDontKnowList { get; set; }
        public List<NameIdModel> GeneralConditionList { get; set; }
        public List<NameIdModel> EndocrineorDiabetesList { get; set; }
        public List<NameIdModel> StomachList { get; set; }
        public List<NameIdModel> UrinaryList { get; set; }
        public List<NameIdModel> NeurologicalList { get; set; }
        public List<NameIdModel> CardiovascularList { get; set; }
        public List<NameIdModel> RespiratoryList { get; set; }
        public List<NameIdModel> EyesList { get; set; }
        public List<NameIdModel> EarList { get; set; }
        public List<NameIdModel> ObOrGynList { get; set; }
        public List<NameIdModel> MusclesOrJointsList { get; set; }
        public List<NameIdModel> SkinList { get; set; }
        public List<NameIdModel> CancerOrHematologyList { get; set; }
        public List<NameIdModel> DentalList { get; set; }
        public List<NameIdModel> PsychologicalList { get; set; }
        public int PatientId { get; set; }
        public PatientConditionInfo PatientConditionInfo { get; set; }

    }
    public class ChronicConditionsMasterViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }
    public class PatientChronicConditionsViewModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int ChronicConditionId { get; set; }
        public string ChronicConditionText { get; set; }
        //public bool IsAlzheimer { get; set; }
        //public string AlzheimerText { get; set; }
        //public bool IsAllergy { get; set; }
        //public string AllergyText { get; set; }
        //public bool IsArthritis { get; set; }
        //public string ArthritisText { get; set; }
        //public bool IsAsthma { get; set; }
        //public string AsthmaText { get; set; }
        //public bool IsBipolar { get; set; }
        //public string BipolarText { get; set; }
        //public bool IsCancer { get; set; }
        //public string CancerText { get; set; }
        //public bool IsCrohns { get; set; }
        //public string CrohnsText { get; set; }
        //public bool IsCholesterol { get; set; }
        //public string CholesterolText { get; set; }
        //public bool IsEmphysema { get; set; }
        //public string EmphysemaText { get; set; }
        //public bool IsDepression { get; set; }
        //public string DepressionText { get; set; }
        //public bool IsDiabetes { get; set; }
        //public string DiabetesText { get; set; }
        //public bool IsSTD { get; set; }
        //public string STDText { get; set; }
        //public bool IsEpilepsy { get; set; }
        //public string EpilepsyText { get; set; }
        //public bool IsKidney { get; set; }
        //public string KidneyText { get; set; }
        //public bool IsHeartDisease { get; set; }
        //public string HeartDiseaseText { get; set; }
        //public bool IsHIV { get; set; }
        //public string HIVText { get; set; }
        //public bool IsThyroid { get; set; }
        //public string ThyroidText { get; set; }
        //public bool IsObesity { get; set; }
        //public string ObesityText { get; set; }
        //public bool IsParkinson { get; set; }
        //public string ParkinsonText { get; set; }
        //public bool IsSickle { get; set; }
        //public string SickleText { get; set; }
        //public bool IsStroke { get; set; }
        //public string StrokeText { get; set; }
        //public bool IsOther { get; set; }
        //public string OtherText { get; set; }
    }
    public class PatientSocialHisotryViewModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int IsSmoke { get; set; }
        public int IsDrugs { get; set; }
        public int IsAlcohol { get; set; }
        public string Other { get; set; }
    }
    public class PatientClinicalHistoryViewModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public int IsCancer { get; set; }
        public int IsDiabetes { get; set; }
        public int IsBloodPressure { get; set; }
        public int IsHeartDisease { get; set; }
        public int IsAlcoholAbuse { get; set; }
        public int IsDrugAbuse { get; set; }
    }
    public class PatientSurgicalHistoryViewModel
    {
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
    public class PatientReviewOfSystemViewModel
    {
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


    public class PatientConditionInfo
    {
        public PatientConditionInfo()
        {
            this.patientConditionViewModel = new PatientConditionViewModel();
            this.patientConditionData = new List<PatientConditionViewModel>();
        }
        public PatientConditionViewModel patientConditionViewModel { get; set; }
        public List<PatientConditionViewModel> patientConditionData { get; set; }
    }
    public class PatientConditionViewModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        [Required(ErrorMessage = "Condition is Required")]
        public string Condition { get; set; }
    }

}
