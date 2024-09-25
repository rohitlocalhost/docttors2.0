using Docttors_portal.Common;
using Docttors_portal.Common.Models;
using Docttors_portal.Common.procedureModels;
using Docttors_portal.Filter;
using Docttors_portal.Services.Classes;
using Docttors_portal.Services.Interfaces;
using System;
using System.Web.Mvc;

namespace Docttors_portal.Controllers
{
    [SessionCheck]
    [MyExceptionHandler]
    public class PatientController : BaseController
    {
        #region Initialize
        private readonly IPatientPersonalServices _personalServices;
        private readonly IPatientPhysicianServices _physicianServices;
        private readonly ICommonUtilityService _commonUtilityService;
        private readonly IUserLogOnService _userLoginService;
        #endregion
        public PatientController(IPatientPersonalServices personalServices, IPatientPhysicianServices physicianServices, ICommonUtilityService commonUtilityService, IUserLogOnService userLoginService)
        {
            _personalServices = personalServices;
            _commonUtilityService = commonUtilityService;
            _physicianServices = physicianServices;
            _userLoginService = userLoginService;
        }
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }
        #region MHR Section
        #region MHR Load
        public ActionResult MyHealthRecord()
        {
            var mhrDataInfo = _personalServices.GetMHRData(SessionVariables.LoggedInUser.UserId);
            return View(mhrDataInfo);
        }
        #endregion

        #region Personal Details
        public ActionResult PersonalDetails()
        {
            var patientPersonalModel = new PatientPersonalModel();
            patientPersonalModel = _personalServices.LoadPersonalDetailsByUserId(SessionVariables.LoggedInUser.UserId);
            patientPersonalModel = LoadlistData(patientPersonalModel);
            return View(patientPersonalModel);
        }

        [HttpPost]
        public ActionResult PersonalDetails(PatientPersonalModel patientRegisterationModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    patientRegisterationModel.UserId = SessionVariables.LoggedInUser.UserId;
                    if (patientRegisterationModel.PatientPersonalId > 0)
                    {
                        //Update personDetails
                        if (_personalServices.UpdatePatientDetails(patientRegisterationModel))
                        {
                            ViewBag.Message = "Data Updated Successfully";
                        }
                        else
                        {
                            ViewBag.Error = "Some issue occured, Data Not saved";
                        }
                    }
                    else
                    {
                        //Add new personDetails
                        int PatientPersonalId = _personalServices.SavePatientDetails(patientRegisterationModel);
                        patientRegisterationModel.PatientPersonalId = PatientPersonalId;
                        ViewBag.Message = "Data Saved Successfully";
                    }
                }
                return PersonalDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region EmergencyContacts

        public ActionResult EmergencyContacts()
        {
            var patientEmergencyModel = new PatientEmergencyModel();
            patientEmergencyModel = _personalServices.LoadPatientEmergencyByUserId(SessionVariables.LoggedInUser.UserId);
            patientEmergencyModel.StateList = _commonUtilityService.GetAllStates();
            patientEmergencyModel.CountryList = _commonUtilityService.GetAllCountry();
            return View(patientEmergencyModel);
        }
        [HttpPost]
        public ActionResult EmergencyContacts(PatientEmergencyModel patientEmergencyModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    patientEmergencyModel.UserId = SessionVariables.LoggedInUser.UserId;
                    if (patientEmergencyModel.PatientEmergencyId > 0)
                    {
                        //Update personDetails
                        if (_personalServices.UpdatePatientEmergency(patientEmergencyModel))
                        {
                            ViewBag.Message = "Data Updated Successfully";
                        }
                        else
                        {
                            ViewBag.Error = "Some issue occured, Data Not saved";
                        }
                    }
                    else
                    {
                        //Add new personDetails
                        int PatientemergencyId = _personalServices.SavePatientEmergency(patientEmergencyModel);
                        patientEmergencyModel.PatientEmergencyId = PatientemergencyId;
                        ViewBag.Message = "Data Saved Successfully";
                    }
                }
                return EmergencyContacts();
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Physician details
        public ActionResult PhysicianDetails()
        {
            var physicanData = _physicianServices.LoadPhysicianDetailsByUserId(SessionVariables.LoggedInUser.UserId);
            physicanData = LoadlistPhysicianData(physicanData);
            return View(physicanData);
        }

        [HttpPost]
        public ActionResult PhysicianDetails(PatientPhysicianModel patientRegisterationModel)
        {
            try
            {
                if (ModelState.IsValid || patientRegisterationModel.IsNone)
                {
                    patientRegisterationModel.UserId = SessionVariables.LoggedInUser.UserId;
                    if (patientRegisterationModel.PatientPhysicianId > 0)
                    {
                        //Update Physician Details
                        if (_physicianServices.UpdatePatientPhysicianDetails(patientRegisterationModel))
                        {
                            ViewBag.Message = "Data Updated Successfully";
                        }
                        else
                        {
                            ViewBag.Error = "Some issue occured, Data Not saved";
                        }
                    }
                    else
                    {
                        //Add new Physician Details
                        int PatientPhysicianId = _physicianServices.SavePatientPhysicianDetails(patientRegisterationModel);
                        patientRegisterationModel.PatientPhysicianId = PatientPhysicianId;
                        ViewBag.Message = "Data Saved Successfully";
                    }
                }
                return PhysicianDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private PatientPhysicianModel LoadlistPhysicianData(PatientPhysicianModel physicianModel)
        {

            physicianModel.StateList = _commonUtilityService.GetAllStates();
            physicianModel.SpecialistTypesList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Specialty);
            return physicianModel;
        }

        public ActionResult DeletePhysicianDetails(int patientPhysicianId)
        {
            try
            {
                if (patientPhysicianId > 0)
                {
                    if (_physicianServices.DeletePatientPhysicianDetails(patientPhysicianId))
                    {
                        ViewBag.Message = "Physician Details Delete Successfully";
                    }
                    else
                    {
                        ViewBag.Error = "Some issue occured, Data Not saved";
                    }
                }
                var physicanData = _physicianServices.LoadPhysicianDetailsByUserId(SessionVariables.LoggedInUser.UserId);
                physicanData = LoadlistPhysicianData(physicanData);
                ModelState.Clear();
                return View("PhysicianDetails", LoadlistPhysicianData(physicanData));
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ActionResult LoadPhysicianDetails(int patientPhysicianId)
        {
            var physicanData = _physicianServices.LoadPhysicianDetailsByPhysicianId(patientPhysicianId, SessionVariables.LoggedInUser.UserId);
            physicanData = LoadlistPhysicianData(physicanData);
            return View("PhysicianDetails", physicanData);
        }

        #endregion

        #region Insuarance
        public ActionResult Insurance()
        {
            var insuranceModel = _personalServices.LoadInsuranceDetailsByUserId(SessionVariables.LoggedInUser.UserId);
            insuranceModel = LoadListInsuranceData(insuranceModel);
            return View(insuranceModel);
        }


        [HttpPost]
        public ActionResult Insurance(PatientInsuranceModel patientInsuranceModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    patientInsuranceModel.UserId = SessionVariables.LoggedInUser.UserId;
                    if (patientInsuranceModel.PatientInsuranceId > 0)
                    {
                        //Update patientInsurance Details
                        if (_personalServices.UpdateInsuranceDetails(patientInsuranceModel))
                        {
                            ViewBag.Message = "Data Updated Successfully";
                        }
                        else
                        {
                            ViewBag.Error = "Some issue occured, Data Not saved";
                        }
                    }
                    else
                    {
                        //Add new patientInsurance Details
                        int patientInsuranceId = _personalServices.SavePatientInsuranceDetails(patientInsuranceModel);
                        patientInsuranceModel.PatientInsuranceId = patientInsuranceId;
                        ViewBag.Message = "Data Saved Successfully";
                    }
                }
                return Insurance();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Patient Allergies
        public ActionResult PatientAllergies()
        {
            var allergiesModel = _personalServices.LoadAllergiesDetailsByUserId(SessionVariables.LoggedInUser.UserId);
            return View(allergiesModel);
        }
        [HttpPost]
        public ActionResult PatientAllergies(PatientAllergiesModel allergiesModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    allergiesModel.UserId = SessionVariables.LoggedInUser.UserId;
                    if (allergiesModel.PatientAllergyId > 0)
                    {
                        //Update Allergies Details
                        if (_personalServices.UpdateAllergiesDetails(allergiesModel))
                        {
                            ViewBag.Message = "Data Updated Successfully";
                        }
                        else
                        {
                            ViewBag.Error = "Some issue occured, Data Not saved";
                        }
                    }
                    else
                    {
                        //Add new Allergies Details
                        int patientAllergyId = _personalServices.SaveAllergiesDetails(allergiesModel);
                        allergiesModel.PatientAllergyId = patientAllergyId;
                        ViewBag.Message = "Data Saved Successfully";
                    }
                }
                return PatientAllergies();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult DeleteAllergyDetails(int patientAllergyId)
        {
            try
            {
                if (patientAllergyId > 0)
                {
                    if (_personalServices.DeleteAllergyDetails(patientAllergyId))
                    {
                        ViewBag.Message = "Patient Allergy Delete Successfully";
                    }
                    else
                    {
                        ViewBag.Error = "Some issue occured, Data Not saved";
                    }
                }
                var AllergyData = _personalServices.LoadAllergiesDetailsByUserId(SessionVariables.LoggedInUser.UserId);
                ModelState.Clear();
                return View("PatientAllergies", AllergyData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult LoadwAllergyDetails(int patientAllergyId)
        {
            var allergyData = _personalServices.LoadAllergyDetailsByAllergyId(patientAllergyId, SessionVariables.LoggedInUser.UserId);
            return View("PatientAllergies", allergyData);
        }

        #endregion

        #region Hospital Details
        public ActionResult HospitalDetails()
        {
            var hospitalModel = _personalServices.LoadHospitalDetailsByUserId(SessionVariables.LoggedInUser.UserId);
            hospitalModel = LoadListHospitalData(hospitalModel);
            return View(hospitalModel);
        }
        private PatientHospitalModel LoadListHospitalData(PatientHospitalModel hospitalModel)
        {
            hospitalModel.StateList = _commonUtilityService.GetAllStates();
            return hospitalModel;
        }

        [HttpPost]
        public ActionResult HospitalDetails(PatientHospitalModel hospitalModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    hospitalModel.UserId = SessionVariables.LoggedInUser.UserId;
                    if (hospitalModel.PatientHospitalId > 0)
                    {
                        //Update Allergies Details
                        if (_personalServices.UpdateHospitalDetails(hospitalModel))
                        {
                            ViewBag.Message = "Data Updated Successfully";
                        }
                        else
                        {
                            ViewBag.Error = "Some issue occured, Data Not saved";
                        }
                    }
                    else
                    {
                        //Add new Allergies Details
                        int patientAllergyId = _personalServices.SaveHospitalDetails(hospitalModel);
                        hospitalModel.PatientHospitalId = patientAllergyId;
                        ViewBag.Message = "Data Saved Successfully";
                    }
                }
                return HospitalDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult DeleteHospitalDetails(int patientHospitalId)
        {
            try
            {
                if (patientHospitalId > 0)
                {
                    if (_personalServices.DeletehospitalDetails(patientHospitalId))
                    {
                        ViewBag.Message = "Patient Allergy Delete Successfully";
                    }
                    else
                    {
                        ViewBag.Error = "Some issue occured, Data Not saved";
                    }
                }
                var hospitalData = _personalServices.LoadHospitalDetailsByUserId(SessionVariables.LoggedInUser.UserId);
                hospitalData.StateList = _commonUtilityService.GetAllStates();
                ModelState.Clear();
                return View("HospitalDetails", hospitalData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult LoadHistoryDetails(int patientHospitalId)
        {
            var hospitalData = _personalServices.LoadHospitalDetailsByHospitalId(patientHospitalId, SessionVariables.LoggedInUser.UserId);
            hospitalData.StateList = _commonUtilityService.GetAllStates();
            return View("HospitalDetails", hospitalData);
        }

        #endregion

        #region Pharmacy Details
        public ActionResult PharmacyDetails()
        {
            var pharmacyModel = _personalServices.LoadPharmacyDetailsByUserId(SessionVariables.LoggedInUser.UserId);
            pharmacyModel.StateList = _commonUtilityService.GetAllStates();
            return View(pharmacyModel);
        }

        [HttpPost]
        public ActionResult PharmacyDetails(PatientPharmacyModel pharmacyModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pharmacyModel.UserId = SessionVariables.LoggedInUser.UserId;
                    if (pharmacyModel.PatientPharmacyId > 0)
                    {
                        //Update Pharmacy Details
                        if (_personalServices.UpdatePharmacyDetails(pharmacyModel))
                        {
                            ViewBag.Message = "Data Updated Successfully";
                        }
                        else
                        {
                            ViewBag.Error = "Some issue occured, Data Not saved";
                        }
                    }
                    else
                    {
                        //Add new Pharmacy Details
                        int patientPharmacyId = _personalServices.SavePharmacyDetails(pharmacyModel);
                        pharmacyModel.PatientPharmacyId = patientPharmacyId;
                        ViewBag.Message = "Data Saved Successfully";
                    }
                }
                return PharmacyDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult DeletePharmacyDetails(int patientPharmacyId)
        {
            try
            {
                if (patientPharmacyId > 0)
                {
                    if (_personalServices.DeletePharmacyDetails(patientPharmacyId))
                    {
                        ViewBag.Message = "Patient Allergy Delete Successfully";
                    }
                    else
                    {
                        ViewBag.Error = "Some issue occured, Data Not saved";
                    }
                }
                var pharmacyData = _personalServices.LoadPharmacyDetailsByUserId(SessionVariables.LoggedInUser.UserId);
                pharmacyData.StateList = _commonUtilityService.GetAllStates();
                ModelState.Clear();
                return View("PharmacyDetails", pharmacyData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult LoadPharmacyDetails(int patientPharmacyId)
        {
            var pharmacyData = _personalServices.LoadPharmacyDetailsByPharmacyId(patientPharmacyId, SessionVariables.LoggedInUser.UserId);
            pharmacyData.StateList = _commonUtilityService.GetAllStates();
            return View("PharmacyDetails", pharmacyData);
        }

        #endregion

        #region Mediacation Details
        public ActionResult MedicationDetails()
        {
            var medicationModel = _personalServices.LoadMedicationDetailsByUserId(SessionVariables.LoggedInUser.UserId);
            return View(medicationModel);
        }

        [HttpPost]
        public ActionResult MedicationDetails(PatientMedicationModel medicationModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    medicationModel.UserId = SessionVariables.LoggedInUser.UserId;
                    if (medicationModel.PatientMedicationId > 0)
                    {
                        //Update Pharmacy Details
                        if (_personalServices.UpdateMedicationDetails(medicationModel))
                        {
                            ViewBag.Message = "Data Updated Successfully";
                        }
                        else
                        {
                            ViewBag.Error = "Some issue occured, Data Not saved";
                        }
                    }
                    else
                    {
                        //Add new Pharmacy Details
                        int PatientMedicationId = _personalServices.SaveMedicationDetails(medicationModel);
                        medicationModel.PatientMedicationId = PatientMedicationId;
                        ViewBag.Message = "Data Saved Successfully";
                    }
                }
                return MedicationDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult DeleteMedicalDetails(int patientMedicationId)
        {
            try
            {
                if (patientMedicationId > 0)
                {
                    if (_personalServices.DeleteMedicationDetails(patientMedicationId))
                    {
                        ViewBag.Message = "Physician Details Delete Successfully";
                    }
                    else
                    {
                        ViewBag.Error = "Some issue occured, Data Not saved";
                    }
                }
                var medicationData = _personalServices.LoadMedicationDetailsByUserId(SessionVariables.LoggedInUser.UserId);
                ModelState.Clear();
                return View("MedicationDetails", medicationData);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ActionResult LoadMedicalDetails(int patientMedicationId)
        {
            var medicationData = _personalServices.LoadMedicationDetailsByMedicalId(patientMedicationId, SessionVariables.LoggedInUser.UserId);
            return View("MedicationDetails", medicationData);
        }
        #endregion

        #region patient Observation
        public ActionResult ObservationDetails()
        {
            var patientObservationModel = new PatientObservationModel();
            patientObservationModel = _personalServices.LoadObservationByUserId(SessionVariables.LoggedInUser.UserId);
            return View(patientObservationModel);
        }
        [HttpPost]
        public ActionResult ObservationDetails(PatientObservationModel patientObservationModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    patientObservationModel.UserId = SessionVariables.LoggedInUser.UserId;
                    if (patientObservationModel.PatientObservationId > 0)
                    {
                        //Update personDetails
                        if (_personalServices.UpdateObservationDetails(patientObservationModel))
                        {
                            ViewBag.Message = "Data Updated Successfully";
                        }
                        else
                        {
                            ViewBag.Error = "Some issue occured, Data Not saved";
                        }
                    }
                    else
                    {
                        //Add new personDetails
                        int observationId = _personalServices.SaveObservationDetails(patientObservationModel);
                        patientObservationModel.PatientObservationId = observationId;
                        ViewBag.Message = "Data Saved Successfully";
                    }
                }
                return ObservationDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult LoadObservationDetails(int patientObservationId)
        {
            var obserVationData = _personalServices.LoadObservationDataByObservationId(patientObservationId, SessionVariables.LoggedInUser.UserId);
            return View("ObservationDetails", obserVationData);
        }
        #endregion

        #region Vital Sign
        public ActionResult VitalSign()
        {
            var patientVitalModel = new PatientVitalModel();
            patientVitalModel = _personalServices.LoadVitalByUserId(SessionVariables.LoggedInUser.UserId);
            return View(patientVitalModel);
        }
        [HttpPost]
        public ActionResult VitalSign(PatientVitalModel patientVitalModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    patientVitalModel.UserId = SessionVariables.LoggedInUser.UserId;
                    if (patientVitalModel.PatientVitalId > 0)
                    {
                        if (_personalServices.UpdateVitalDetails(patientVitalModel))
                        {
                            ViewBag.Message = "Data Updated Successfully";
                        }
                        else
                        {
                            ViewBag.Error = "Some issue occured, Data Not saved";
                        }
                    }
                    else
                    {
                        //Add new personDetails
                        int vitalId = _personalServices.SaveVitalDetails(patientVitalModel);
                        //patientVitalModel.PatientVitalId = vitalId;
                        ViewBag.Message = "Data Saved Successfully";
                        ModelState.Clear();
                    }
                }
                return VitalSign();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult LoadVitalSign(int patientVitalId)
        {
            var patientVitalModel = new PatientVitalModel();
            patientVitalModel = _personalServices.LoadVitalDataByVitalId(patientVitalId, SessionVariables.LoggedInUser.UserId);
            return View("VitalSign", patientVitalModel);
        }
        #endregion
        #endregion

        public ActionResult Message()
        {
            return View();
        }
        public ActionResult MyProvider()
        {
            return View();
        }
        public ActionResult Search()
        {
            return View();
        }
        public ActionResult SystemCheck()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            var changePasswordModel = new ChangePasswordModel();
            return View(changePasswordModel);
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel changePasswordModel)
        {
            if (ModelState.IsValid)
            {
                changePasswordModel.UserId = SessionVariables.LoggedInUser.UserId;
                changePasswordModel.IsPasswordChanged = _userLoginService.ChangePassword(changePasswordModel);
                if (!changePasswordModel.IsPasswordChanged)
                {
                    ViewBag.Message = "Old password Not matched, Please type correct password to change!";
                    ViewBag.alertClass = "danger";
                }
                else
                {
                    ViewBag.Message = "Password Changed Successfully!";
                    ViewBag.alertClass = "success";
                }
                ModelState.Clear();

            }
            return View(changePasswordModel);
        }
        #region private Methods
        private PatientPersonalModel LoadlistData(PatientPersonalModel patientPersonalModel)
        {

            patientPersonalModel.StateList = _commonUtilityService.GetAllStates();
            patientPersonalModel.CountryList = _commonUtilityService.GetAllCountry();
            patientPersonalModel.GenderList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Gender);
            patientPersonalModel.MaritalMasterList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.MaritalStatus);
            patientPersonalModel.EducationMasterList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Education);
            patientPersonalModel.EthnicityList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Ethnicity);
            patientPersonalModel.HeightList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Height);
            return patientPersonalModel;
        }
        private PatientInsuranceModel LoadListInsuranceData(PatientInsuranceModel insuranceModel)
        {

            insuranceModel.StateList = _commonUtilityService.GetAllStates();
            insuranceModel.CountryList = _commonUtilityService.GetAllCountry();
            return insuranceModel;
        }
        #endregion
    }
}