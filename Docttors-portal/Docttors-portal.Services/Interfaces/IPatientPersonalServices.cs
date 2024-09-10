using Docttors_portal.Common.Models;

namespace Docttors_portal.Services.Interfaces
{
    public interface IPatientPersonalServices
    {
        int SavePatientDetails(PatientPersonalModel personalModel);
        PatientPersonalModel LoadPersonalDetailsByUserId(int userId);
        bool UpdatePatientDetails(PatientPersonalModel personalModel);

        #region Insurance
        int SavePatientInsuranceDetails(PatientInsuranceModel insuranceModel);
        PatientInsuranceModel LoadInsuranceDetailsByUserId(int userId);
        bool UpdateInsuranceDetails(PatientInsuranceModel insuranceModel);

        #endregion
    }
}
