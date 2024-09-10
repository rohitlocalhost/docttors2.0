using Docttors_portal.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Services.Interfaces
{
    public interface IPatientPhysicianServices
    {
        PatientPhysicianModel LoadPhysicianDetailsByUserId(int userId);
        int SavePatientPhysicianDetails(PatientPhysicianModel physicianModel);
        bool UpdatePatientPhysicianDetails(PatientPhysicianModel physicianModel);
    }
}
