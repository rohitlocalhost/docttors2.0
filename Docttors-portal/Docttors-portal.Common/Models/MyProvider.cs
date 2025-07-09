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
            this.videoTab = new VideoTab();
            this.paymentTab = new paymentTab();
        }
        public PersonalTab PersonalTab { get; set; }

        public ComplaintTab ComplaintTab { get; set; }

        public VideoTab videoTab { get; set; }

        public paymentTab paymentTab { get; set; }
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
        public static int Step1Id { get; set; }
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
        public int Step1Id { get; set; }

    }

    public class paymentTab
    {
        public int VisitId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int PatientId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int step1Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                    @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                    @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                    ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string City { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Zip { get; set; }

        public string PaymentType { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int CardTypeId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public decimal Amount { get; set; }
        public string CardId { get; set; }
        public string CvvNumber { get; set; }
        public DateTime TransactionDate { get; set; }
        public string BillingType { get; set; }
        public string PaypalTrasactionId { get; set; }
        public string PaymentTransactionId { get; set; }
        public string RefundTransId { get; set; }
        public string RefundStatus { get; set; }
        public int CardExpMonthId { get; set; }
        public int CardExpYearId { get; set; }
        public List<NameIdModel> StateList { get; set; }
        public List<NameIdModel> CardExpMonthList { get; set; }
        public List<NameIdModel> CardExpYearList { get; set; }
        public List<NameIdModel> CardTypeList { get; set; }

    }
}
