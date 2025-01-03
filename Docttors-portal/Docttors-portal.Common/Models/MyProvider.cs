using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Common.Models
{
    public class MyProvider
    {
        public MyProvider()
        {
            this.PersonalTab = new PersonalTab();
            this.ComplaintTab = new ComplaintTab();
            this.videoTab=new VideoTab();
        }
        public PersonalTab PersonalTab { get; set; }

        public ComplaintTab ComplaintTab { get; set; }

        public VideoTab videoTab { get; set; }
    }
    public class PersonalTab
    {
        public PersonalTab()
        {
            this.PatientPersonalModel = new PatientPersonalModel();
            this.PatientInsuranceModel = new PatientInsuranceModel();
            this.patientMedicationList = new List<PatientMedicationModel>();
            this.AllergyList = new List<PatientAllergiesModel>();
            this.patientClinicalViewModel = new PatientClinicalViewModel();
        }
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accepted terms")]
        public bool TermAndCondition { get; set; }
        public PatientPersonalModel PatientPersonalModel { get; set; }
        public PatientInsuranceModel PatientInsuranceModel { get; set; }
        public List<PatientMedicationModel> patientMedicationList { get; set; }
        public List<PatientAllergiesModel> AllergyList { get; set; }
        public PatientClinicalViewModel patientClinicalViewModel { get; set; }
    }
    public class ComplaintTab
    {
        public int Step1Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string A1 { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string A2 { get; set; }
        public int A3 { get; set; }
        public string A4 { get; set; }
        public string A5 { get; set; }
        public int A6 { get; set; }
        public int A7 { get; set; }
        public int A8 { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string A9 { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string A10 { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string A11 { get; set; }
        public int DoctorId { get; set; }
        public List<NameIdModel> ComplainTypeList { get; set; }
        public List<NameIdModel> BloodPressureList { get; set; }
        public List<NameIdModel> BreathingList { get; set; }
        public List<NameIdModel> WeightList { get; set; }
        public List<Symptoms> SymptomsList { get; set; }

    }
    public class ConsentForm
    {
        [Required(ErrorMessage = "SSN Number is Required")]
        public string SSNNumber { get; set; }
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accepted terms")]
        public bool TermAndCondition { get; set; }
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accepted terms")]
        public bool IsLocatedAlaska { get; set; }

        [Required(ErrorMessage = "You must choose one of these")]
        public bool? PatientOrGuardian { get; set; }
        public static int DoctorId { get; set; }
        public static int ServiceType { get; set; }
    }

    public class Symptoms
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public bool IsChecked { get; set; }
    }

    public class VideoTab
    {
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string Subject { get; set; }
        public string Title { get; set; }
        public string DoctorEmail { get; set; }
        public string NurseEmail { get; set; }
        public string phone { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string TextMessage { get; set; }
        public int DoctorId { get; set; }

    }
}
