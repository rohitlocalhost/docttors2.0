using Docttors_portal.Common.Models;

namespace Docttors_portal.Services.Interfaces
{
    public interface IPatientPersonalServices
    {
        int SavePatientDetails(PatientPersonalModel personalModel);
    }
}
