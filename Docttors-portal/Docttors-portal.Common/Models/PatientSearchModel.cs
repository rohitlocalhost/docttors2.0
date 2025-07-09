using Docttors_portal.Common.procedureModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Common.Models
{
    public class PatientSearchModel
    {
        public PatientSearchModel()
        {
            PatientData = new List<GetDoctorPatients>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string DOB { get; set; }
        public string MR { get; set; }
        public bool IsSearchEnable { get; set; }
        public List<GetDoctorPatients> PatientData { get; set; }
    }
}
