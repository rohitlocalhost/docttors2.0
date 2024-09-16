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

        #region Allergies
        PatientAllergiesModel LoadAllergiesDetailsByUserId(int userId);
        int SaveAllergiesDetails(PatientAllergiesModel allergiesModel);
        bool UpdateAllergiesDetails(PatientAllergiesModel allergiesModel);
        #endregion

        #region Hospitals
        PatientHospitalModel LoadHospitalDetailsByUserId(int userId);
        int SaveHospitalDetails(PatientHospitalModel hospitalModel);
        bool UpdateHospitalDetails(PatientHospitalModel hospitalModel);
        #endregion

        #region PharmacyDetails
        PatientPharmacyModel LoadPharmacyDetailsByUserId(int userId);
        int SavePharmacyDetails(PatientPharmacyModel pharmacyModel);
        bool UpdatePharmacyDetails(PatientPharmacyModel pharmacyModel);
        #endregion

        #region Medication Details
        PatientMedicationModel LoadMedicationDetailsByUserId(int userId);
        int SaveMedicationDetails(PatientMedicationModel medicationModel);
        bool UpdateMedicationDetails(PatientMedicationModel medicationModel);
        #endregion
    }
}
