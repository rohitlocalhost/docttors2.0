using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Common.procedureModels
{
    public class GetpatientMessage
    {
        public int Step1Id { get; set; }
        public int Step2Id { get; set; }
        public string Status { get; set; }
        public string patientName { get; set; }
        public string ProviderName { get; set; }
        public string ServiceType { get; set; }
        public string Symptoms { get; set; }
        public string DateTimeRequested { get; set; }
        public string AppointmentDate { get; set; }
        public string DateTreated { get; set; }
        public string Amount { get; set; }
        public string SSN { get; set; }
        public bool IsTreated { get; set; } 

    }
}
