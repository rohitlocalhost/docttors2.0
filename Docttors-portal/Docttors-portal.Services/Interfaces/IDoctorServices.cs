using Docttors_portal.Common.Models;
using Docttors_portal.Common.procedureModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Services.Interfaces
{
    public interface IDoctorServices
    {
        List<GetDoctorPatients> GetPatientByDoctor(PatientSearchModel patientSearchModel);
    }
}
