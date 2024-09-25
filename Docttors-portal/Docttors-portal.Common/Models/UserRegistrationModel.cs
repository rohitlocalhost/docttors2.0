using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Docttors_portal.Common.Models
{
    public class UserRegistrationModel
    {
        public int UserID { get; set; }
        public int DoctorId { get; set; }
        [Required(ErrorMessage ="FirstName is required")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +@"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\"+ @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",ErrorMessage = "Email is not valid")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required")]
        public string CPassword { get; set; }
        [Display(Name = "Speciality")]
        [Required(ErrorMessage = "Speciality is required")]
        public int SpecialtyId { get; set; }
        [Required(ErrorMessage = "Position is required")]
        public string Position { get; set; }
        public byte[] ProfilePic { get; set; }        
        #region LICENSE 
        [Required(ErrorMessage = "LICENSE is required")]
        public string License { get; set; }
        public string NPI { get; set; }
        public string SecLicense { get; set; }
        public string DEA { get; set; }
        #endregion
        #region Office
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        [Display(Name="State")]
        [Required(ErrorMessage = "State is required")]
        public int StateId { get; set; }
        [Required(ErrorMessage = "ZipCode is required")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Mobile Number is required")]
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        [RegularExpression(@"/^[0-9]+$/",ErrorMessage ="only Numeric")]
        public string Fax { get; set; }
        public string PracticeName { get; set; }
        public string PracticeLogo { get; set; }
        #endregion
        [Required(ErrorMessage = "Insurance is required")]
        public int[] SelectedInsurance { get; set; }
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accepted terms")]
        public bool TermsAndConditions { get; set; }
        public bool IsDoctor { get; set; }
        public int UserRoleID { get; set; }
        public List<NameIdModel> StateList { get; set; }
        public List<NameIdModel> AllInsaurance { get; set; }
        public List<NameIdModel> AllSpecialty { get; set; }
    }

    public class UserRoleModel
    {
        public int UserRoleID { get; set; }
        public long CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<long> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<long> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public bool IsActive { get; set; }
        public string RoleName { get; set; }
        

        //public virtual ICollection<UserRegistrationModel> Users { get; set; }
    }

    public class UserLogOnModel
    {
        public long UserID { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                    @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                    @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                    ErrorMessage = "Email is not valid")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
        public bool KeepMeLoggedIn { get; set; }
        public string LogOnStatus { get; set; }
        public string LogOnContent { get; set; }

        [Display(Name = "checkcaptcha")]
        public string checkcaptch { get; set; }

        [System.ComponentModel.DataAnnotations.CompareAttribute("CaptchaImageText", ErrorMessage = "Captcha didn't match.")]
        public string CaptchaTextMatch { get; set; }

        public string CaptchaImageText { get; set; }
        public bool RememberMe { get; set; }
    }
    public class PatientRegisterationModel
    {
        public int PatientId { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" + @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" + @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Email is not valid")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password is required")]
        public string CPassword { get; set; }
        [Range(typeof(bool), "true", "true", ErrorMessage = "You must accepted terms")]
        public bool TermsAndConditions { get; set; }
    }
}
