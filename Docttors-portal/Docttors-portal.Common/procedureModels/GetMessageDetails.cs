using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Common.procedureModels
{
    public class GetMessageDetails
    {
        public int VisitId { get; set; }
        public string PatientName { get; set; }
        public string DoctorName { get; set; }
        public string Subject { get; set; }
        public string CreatedOn { get; set; }
        public string DoctorEmail { get; set; }
        public string Position { get; set; }
        public string NurseEmail { get; set; }
        public string AppointmentDate { get; set; }
        public string TextMessage { get; set; }
        public string Phone { get; set; }
    }
}
