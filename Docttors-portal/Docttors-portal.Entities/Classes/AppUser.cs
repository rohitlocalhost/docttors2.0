using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    public class AppUser : BaseClass
    {
        public int Id { get; set; }
        public int UserRoleId { get; set; }
        public string FirstName { get; set; }
        public string MiddileName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int specialityId { get; set; }
        public string Position { get; set; }
        public byte[] ProfilePic { get; set; }
        public int InsuranceId { get; set; }
        public string StateLicense { get; set; }
        public string NPI { get; set; }
        public string SecondaryLicense { get; set; }
        public string Dea { get; set; }
        public string OfficeAddress { get; set; }
        public string city { get; set; }
        public int State { get; set; }
        public string ZipCode { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string PracticeName { get; set; }
        public byte[] PracticeLogo { get; set; }
    }
}
