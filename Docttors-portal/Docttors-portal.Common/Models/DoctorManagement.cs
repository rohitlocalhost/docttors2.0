using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Common.Models
{
    public class DoctorManagement
    {
        public DoctorManagement()
        {
            this.DoctorEmailConfigModel = new DoctorEmailConfigModel();
            this.DoctorNewsModel = new DoctorNewsModel();
            this.DoctorServiceFeesModel = new DoctorServiceFeesModel();
            this.DoctorContactModel = new DoctorContactModel();
            this.MarketingModel = new MarketingModel();
            this.AllDoctors = new List<NameIdModel>();
            this.AllNewsData = new List<DoctorNewsModel>();
        }
        public int UserId { get; set; }
        public int DoctorInfoId { get; set; }
        public string Name { get; set; }
        public byte[] ProfilePhoto { get; set; }
        public string AboutDoctor { get; set; }
        public string Affilation { get; set; }
        public string School { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string GraduationDate { get; set; }
        public string language { get; set; }
        public string Insurance { get; set; }
        public string Services { get; set; }
        public string RefferDoctor { get; set; }
        public string Message { get; set; }
        public DoctorEmailConfigModel DoctorEmailConfigModel { get; set; }
        public DoctorNewsModel DoctorNewsModel { get; set; }
        public DoctorServiceFeesModel DoctorServiceFeesModel { get; set; }
        public DoctorContactModel DoctorContactModel { get; set; }
        public MarketingModel MarketingModel { get; set; }
        public List<NameIdModel> StateList { get; set; }
        public List<NameIdModel> AllInsaurance { get; set; }
        public List<NameIdModel> AllSpecialty { get; set; }
        public List<NameIdModel> AllDoctors { get; set; }
        public List<DoctorNewsModel> AllNewsData { get; set; }

    }
    public class DoctorEmailConfigModel
    {
        public int Id { get; set; }
        public string DoctorEmail { get; set; }
        public string NurseEmail { get; set; }
        public string BillingEmail { get; set; }
        public string LaboratoryEmail { get; set; }
        public string DiagnosticEmail { get; set; }
    }
    public class DoctorNewsModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "News Subject/Title is required")]
        public string NewsSubject { get; set; }
        [Required(ErrorMessage = "PublishDate is required")]
        public DateTime? PublishDate { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
    }
    public class DoctorServiceFeesModel
    {
        public int Id { get; set; }
        public decimal? AskDoctor { get; set; }
        public decimal? VideoVisit { get; set; }
        public decimal? FaceToFace { get; set; }
        public decimal? RxRefill { get; set; }
    }
    public class DoctorContactModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "DoctorName is required")]
        public string DoctorName { get; set; }
        [Required(ErrorMessage = "PracticeName is required")]
        public string PracticeName { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter valid Mobile Number")]
        public int Phone { get; set; }
        [Required(ErrorMessage = "FaxNumber is required")]
        public string FaxNumber { get; set; }
        [Required(ErrorMessage = "OfficeEmail is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email is not valid")]
        public string OfficeEmail { get; set; }
        public string DoctorName_Office1 { get; set; }
        public string PracticeName_Office1 { get; set; }
        public string Address_Office1 { get; set; }
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter valid Mobile Number")]
        public int Phone_Office1 { get; set; }
        public string FaxNumber_Office1 { get; set; }
        public string OfficeEmail_Office1 { get; set; }
        public string DoctorName_Office2 { get; set; }
        public string PracticeName_Office2 { get; set; }
        public string Address_Office2 { get; set; }
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter valid Mobile Number")]
        public int Phone_Office2 { get; set; }
        public string FaxNumber_Office2 { get; set; }
        public string OfficeEmail_Office2 { get; set; }
        public string DoctorName_Office3 { get; set; }
        public string PracticeName_Office3 { get; set; }
        public string Address_Office3 { get; set; }
        [RegularExpression("^\\d{10}$", ErrorMessage = "Please enter valid Mobile Number")]
        public int Phone_Office3 { get; set; }
        public string FaxNumber_Office3 { get; set; }
        public string OfficeEmail_Office3 { get; set; }
    }
    public class MarketingModel
    {
        public byte[] FileData { get; }
    }
}

