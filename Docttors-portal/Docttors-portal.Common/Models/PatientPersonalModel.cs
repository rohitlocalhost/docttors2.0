using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Common.Models
{
    public class PatientPersonalModel
    {
        public int PatientPersonalId { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MiddleName { get; set; }
        public string WorkPhone { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string ZipCode { get; set; }
        public int CountryId { get; set; }
        public int GenderId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }
        public string SSN { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public int BloodPressureId { get; set; }
        public int EyeColorId { get; set; }

        public int NoOfChildren { get; set; }
        public string SpousName { get; set; }
        public string FatherLiving { get; set; }
        public string MotherLiving { get; set; }
        public string SisterLiving { get; set; }
        public string BrotherLiving { get; set; }
        public int MaritalId { get; set; }
        public int EducationId { get; set; }
        public int RaceID { get; set; }
        public int HairColorID { get; set; }
        public string Occupation { get; set; }
        public bool OrganDonor { get; set; }
        public int RcopiaID { get; set; }
        public string ReasonForGuardianship { get; set; }
        public string InsuranceCompany { get; set; }
        public int SelfPaid { get; set; }
        public string MRN { get; set; }
        public string Profession { get; set; }

        public List<NameIdModel> MaritalMasterList { get; set; }
        public List<NameIdModel> GenderList { get; set; }
        public List<NameIdModel> StateList { get; set; }
        public List<NameIdModel> CountryList { get; set; }
        public List<NameIdModel> EducationMasterList { get; set; }
    }
}
