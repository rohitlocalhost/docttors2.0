using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Common.procedureModels
{
    public class GetDoctorPatients
    {
        public string Name { get; set;}
        public string EmailAddress { get; set; }
        public int UserId { get; set;}
        public string Mrn { get; set; }
        public string CellPhone { get; set; }
        public DateTime DOB { get; set; }
    }
}
