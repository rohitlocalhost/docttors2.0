using Docttors_portal.Common.Models;
using Docttors_portal.Common.procedureModels;
using System.Collections.Generic;

namespace Docttors_portal.Services.Interfaces
{
    public interface IPatientPersonalServices
    {
        #region MHR Load Interface
        GetMHRDataInfo GetMHRData(int UserId);
        #endregion

        #region patient personal Service
        int SavePatientDetails(PatientPersonalModel personalModel);
        PatientPersonalModel LoadPersonalDetailsByUserId(int userId);
        bool UpdatePatientDetails(PatientPersonalModel personalModel);
        #endregion

        #region Emergency Contact Service
        int SavePatientEmergency(PatientEmergencyModel emergencyModel);
        PatientEmergencyModel LoadPatientEmergencyByUserId(int userId);
        bool UpdatePatientEmergency(PatientEmergencyModel emergencyModel);
        #endregion

        #region Insurance
        int SavePatientInsuranceDetails(PatientInsuranceModel insuranceModel);
        PatientInsuranceModel LoadInsuranceDetailsByUserId(int userId);
        bool UpdateInsuranceDetails(PatientInsuranceModel insuranceModel);

        #endregion

        #region patient Observation Service
        int SaveObservationDetails(PatientObservationModel patientObservationModel);
        PatientObservationModel LoadObservationByUserId(int userId);
        bool UpdateObservationDetails(PatientObservationModel patientObservationModel);
        PatientObservationModel LoadObservationDataByObservationId(int observationId, int userId);
        #endregion
    }
}
