using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Common.procedureModels
{
    public class GetDoctorsByPatients
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public String Address { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
        public string SpecialtyName { get; set; }
        public string City { get; set; }
        public string ProfilePic { get; set; }
        public string CCMIsPatientEligible { get; set; }
        public string CCMIsConsentProvided { get; set; }
        public string FavoriteDoctors { get; set; }
        public int FavoriteDoctorId { get; set; }
        public bool IsprimaryDoctor { get; set; }
        public decimal VideoChat { get; set; }
        public decimal VideoEmail { get; set; }
        public decimal AskDoctor { get; set; }
        public decimal Refill { get; set; }
    }
    public class GetHospitalData
    {
        public int HospitalId { get; set; }
        public string HospitalName { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string ZIP { get; set; }
        public string Address { get; set; }

    }
    public class GetInsuranceCompanyList
    {
        public int InsuaranceId { get; set; }
        public string CompanyName { get; set; }
        public string URL { get; set; }
        public string PlanType { get; set; }
    }
}
