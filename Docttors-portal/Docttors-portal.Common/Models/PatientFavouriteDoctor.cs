using Docttors_portal.Common.procedureModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Common.Models
{
    public class PatientFavouriteDoctor
    {
        public PatientFavouriteDoctor()
        {
            this.getDoctorsByPatients = new List<GetDoctorsByPatients>();
        }
        public List<GetDoctorsByPatients> getDoctorsByPatients { get; set; }
    }
}
