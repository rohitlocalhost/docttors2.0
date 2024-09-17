using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Common
{
    public class LoggedInUserDetails
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserRole { get; set; }
        public int UserRoleID { get; set; }
        public string Name
        {
            get
            {
                return String.IsNullOrEmpty(FirstName) ? "" : (FirstName + " ") + (String.IsNullOrEmpty(LastName) ? "" : (LastName + " "));
            }
        }
    }
}
