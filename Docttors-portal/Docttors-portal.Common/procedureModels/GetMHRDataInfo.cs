using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Common.procedureModels
{
    public class GetMHRDataInfo
    {
        public int UserId { get; set; }
        public string PersonalDetailsCreated { get; set; }
        public string PersonalDetailsModified { get; set; }
        public string LastpersonalDetailsModifiedTime { get; set; }
        public string EmergencyContactCreated { get; set; }
        public string EmergencyContactModified { get; set; }
        public string LastEmergencyContactModifiedTime { get; set; }
        public string PhysicianDetailCreated { get; set; }
        public string PhysicianDetailModified { get; set; }
        public string LastPhysicianDetailModifiedTime { get; set; }
        public string InsuranceCreated { get; set; }
        public string InsuranceModified { get; set; }
        public string InsuranceModifiedTime { get; set; }
        public string ObservationCreated { get; set; }
        public string ObservationModified { get; set; }
        public string ObservationModifiedTime { get; set; }

    }
}
