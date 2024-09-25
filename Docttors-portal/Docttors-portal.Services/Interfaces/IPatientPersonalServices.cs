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

        #region Allergies
        PatientAllergiesModel LoadAllergiesDetailsByUserId(int userId);
        int SaveAllergiesDetails(PatientAllergiesModel allergiesModel);
        bool UpdateAllergiesDetails(PatientAllergiesModel allergiesModel);
        bool DeleteAllergyDetails(int patientAllergyId);
        PatientAllergiesModel LoadAllergyDetailsByAllergyId(int patientAllergyId, int userId);
        #endregion

        #region Hospitals
        PatientHospitalModel LoadHospitalDetailsByUserId(int userId);
        int SaveHospitalDetails(PatientHospitalModel hospitalModel);
        bool UpdateHospitalDetails(PatientHospitalModel hospitalModel);
        bool DeletehospitalDetails(int patientHospitalId);
        PatientHospitalModel LoadHospitalDetailsByHospitalId(int patientHospitalId, int userId);
        #endregion

        #region PharmacyDetails
        PatientPharmacyModel LoadPharmacyDetailsByUserId(int userId);
        int SavePharmacyDetails(PatientPharmacyModel pharmacyModel);
        bool UpdatePharmacyDetails(PatientPharmacyModel pharmacyModel);
        bool DeletePharmacyDetails(int patientPharmacyId);
        PatientPharmacyModel LoadPharmacyDetailsByPharmacyId(int patientPharmacyId, int userId);
        #endregion

        #region Medication Details
        PatientMedicationModel LoadMedicationDetailsByUserId(int userId);
        int SaveMedicationDetails(PatientMedicationModel medicationModel);
        bool UpdateMedicationDetails(PatientMedicationModel medicationModel);
        bool DeleteMedicationDetails(int patientMedicationId);
        PatientMedicationModel LoadMedicationDetailsByMedicalId(int patientMedicationId, int userId);
        #endregion

        #region patient Observation Service
        int SaveObservationDetails(PatientObservationModel patientObservationModel);
        PatientObservationModel LoadObservationByUserId(int userId);
        bool UpdateObservationDetails(PatientObservationModel patientObservationModel);
        PatientObservationModel LoadObservationDataByObservationId(int observationId, int userId);
        #endregion
        #region patient Vital Service
        int SaveVitalDetails(PatientVitalModel patientObservationModel);
        PatientVitalModel LoadVitalByUserId(int userId);
        bool UpdateVitalDetails(PatientVitalModel patientObservationModel);
        PatientVitalModel LoadVitalDataByVitalId(int vitalId, int userId);
        #endregion

        #region Get Doctor By Patient
        List<GetDoctorsByPatients> GetDoctorByPatient(PatientSearchDoctorModel patientSearchModel);
        #endregion
    }
}
