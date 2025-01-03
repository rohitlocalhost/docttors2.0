using Docttors_portal.Common;
using Docttors_portal.Common.Models;
using Docttors_portal.Common.procedureModels;
using Docttors_portal.Filter;
using Docttors_portal.Services.Classes;
using Docttors_portal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls.Expressions;
using static Antlr.Runtime.Tree.TreeWizard;
using static Docttors_portal.Common.Models.MyProvider;
using static System.ActivationContext;
using SearchType = Docttors_portal.Common.SearchType;

namespace Docttors_portal.Controllers
{
    [SessionCheck]
    [MyExceptionHandler]
    public class PatientController : BaseController
    {
        #region Initialize
        private readonly IPatientPersonalServices _patientServices;
        private readonly IPatientPhysicianServices _physicianServices;
        private readonly ICommonUtilityService _commonUtilityService;
        private readonly IUserLogOnService _userLoginService;
        private readonly IDoctorServices _doctorServices;
        #endregion
        public PatientController(IPatientPersonalServices patientServices, IPatientPhysicianServices physicianServices, ICommonUtilityService commonUtilityService, IUserLogOnService userLoginService, IDoctorServices doctorServices)
        {
            _patientServices = patientServices;
            _commonUtilityService = commonUtilityService;
            _physicianServices = physicianServices;
            _userLoginService = userLoginService;
            _doctorServices = doctorServices;
        }
        // GET: Patient
        public ActionResult Index()
        {
            var patientMessageData = LoadpatientMessage();
            return View(patientMessageData);
        }

        private List<GetpatientMessage> LoadpatientMessage()
        {
            return _patientServices.GetpatientMessages(SessionVariables.LoggedInUser.UserId);
        }

        #region MHR Section
        #region MHR Load
        public ActionResult MyHealthRecord()
        {
            var mhrDataInfo = _patientServices.GetMHRData(SessionVariables.LoggedInUser.UserId);
            return View(mhrDataInfo);
        }
        #endregion
        public ActionResult PersonalDetails()
        {
            var patientPersonalModel = new PatientPersonalModel();
            patientPersonalModel = _patientServices.LoadPersonalDetailsByUserId(SessionVariables.LoggedInUser.UserId);
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
                        if (_patientServices.UpdatePatientDetails(patientRegisterationModel))
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
                        int PatientPersonalId = _patientServices.SavePatientDetails(patientRegisterationModel);
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
            patientEmergencyModel = _patientServices.LoadPatientEmergencyByUserId(SessionVariables.LoggedInUser.UserId);
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
                        if (_patientServices.UpdatePatientEmergency(patientEmergencyModel))
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
                        int PatientemergencyId = _patientServices.SavePatientEmergency(patientEmergencyModel);
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
            var insuranceModel = _patientServices.LoadInsuranceDetailsByUserId(SessionVariables.LoggedInUser.UserId);
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
                        if (_patientServices.UpdateInsuranceDetails(patientInsuranceModel))
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
                        int patientInsuranceId = _patientServices.SavePatientInsuranceDetails(patientInsuranceModel);
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
            var allergiesModel = _patientServices.LoadAllergiesDetailsByUserId(SessionVariables.LoggedInUser.UserId);
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
                        if (_patientServices.UpdateAllergiesDetails(allergiesModel))
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
                        int patientAllergyId = _patientServices.SaveAllergiesDetails(allergiesModel);
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
                    if (_patientServices.DeleteAllergyDetails(patientAllergyId))
                    {
                        ViewBag.Message = "Patient Allergy Delete Successfully";
                    }
                    else
                    {
                        ViewBag.Error = "Some issue occured, Data Not saved";
                    }
                }
                var AllergyData = _patientServices.LoadAllergiesDetailsByUserId(SessionVariables.LoggedInUser.UserId);
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
            var allergyData = _patientServices.LoadAllergyDetailsByAllergyId(patientAllergyId, SessionVariables.LoggedInUser.UserId);
            return View("PatientAllergies", allergyData);
        }

        #endregion

        #region Hospital Details
        public ActionResult HospitalDetails()
        {
            var hospitalModel = _patientServices.LoadHospitalDetailsByUserId(SessionVariables.LoggedInUser.UserId);
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
                        if (_patientServices.UpdateHospitalDetails(hospitalModel))
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
                        int patientAllergyId = _patientServices.SaveHospitalDetails(hospitalModel);
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
                    if (_patientServices.DeletehospitalDetails(patientHospitalId))
                    {
                        ViewBag.Message = "Patient Allergy Delete Successfully";
                    }
                    else
                    {
                        ViewBag.Error = "Some issue occured, Data Not saved";
                    }
                }
                var hospitalData = _patientServices.LoadHospitalDetailsByUserId(SessionVariables.LoggedInUser.UserId);
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
            var hospitalData = _patientServices.LoadHospitalDetailsByHospitalId(patientHospitalId, SessionVariables.LoggedInUser.UserId);
            hospitalData.StateList = _commonUtilityService.GetAllStates();
            return View("HospitalDetails", hospitalData);
        }

        #endregion

        #region Pharmacy Details
        public ActionResult PharmacyDetails()
        {
            var pharmacyModel = _patientServices.LoadPharmacyDetailsByUserId(SessionVariables.LoggedInUser.UserId);
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
                        if (_patientServices.UpdatePharmacyDetails(pharmacyModel))
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
                        int patientPharmacyId = _patientServices.SavePharmacyDetails(pharmacyModel);
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
                    if (_patientServices.DeletePharmacyDetails(patientPharmacyId))
                    {
                        ViewBag.Message = "Patient Allergy Delete Successfully";
                    }
                    else
                    {
                        ViewBag.Error = "Some issue occured, Data Not saved";
                    }
                }
                var pharmacyData = _patientServices.LoadPharmacyDetailsByUserId(SessionVariables.LoggedInUser.UserId);
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
            var pharmacyData = _patientServices.LoadPharmacyDetailsByPharmacyId(patientPharmacyId, SessionVariables.LoggedInUser.UserId);
            pharmacyData.StateList = _commonUtilityService.GetAllStates();
            return View("PharmacyDetails", pharmacyData);
        }

        #endregion

        #region Mediacation Details
        public ActionResult MedicationDetails()
        {
            var medicationModel = _patientServices.LoadMedicationDetailsByUserId(SessionVariables.LoggedInUser.UserId);
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
                        if (_patientServices.UpdateMedicationDetails(medicationModel))
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
                        int PatientMedicationId = _patientServices.SaveMedicationDetails(medicationModel);
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
                    if (_patientServices.DeleteMedicationDetails(patientMedicationId))
                    {
                        ViewBag.Message = "Physician Details Delete Successfully";
                    }
                    else
                    {
                        ViewBag.Error = "Some issue occured, Data Not saved";
                    }
                }
                var medicationData = _patientServices.LoadMedicationDetailsByUserId(SessionVariables.LoggedInUser.UserId);
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
            var medicationData = _patientServices.LoadMedicationDetailsByMedicalId(patientMedicationId, SessionVariables.LoggedInUser.UserId);
            return View("MedicationDetails", medicationData);
        }
        #endregion

        #region patient Observation
        public ActionResult ObservationDetails()
        {
            var patientObservationModel = new PatientObservationModel();
            patientObservationModel = _patientServices.LoadObservationByUserId(SessionVariables.LoggedInUser.UserId);
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
                        if (_patientServices.UpdateObservationDetails(patientObservationModel))
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
                        int observationId = _patientServices.SaveObservationDetails(patientObservationModel);
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
            var obserVationData = _patientServices.LoadObservationDataByObservationId(patientObservationId, SessionVariables.LoggedInUser.UserId);
            return View("ObservationDetails", obserVationData);
        }
        #endregion

        #region Vital Sign
        public ActionResult VitalSign()
        {
            var patientVitalModel = new PatientVitalModel();
            patientVitalModel = _patientServices.LoadVitalByUserId(SessionVariables.LoggedInUser.UserId);
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
                        if (_patientServices.UpdateVitalDetails(patientVitalModel))
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
                        int vitalId = _patientServices.SaveVitalDetails(patientVitalModel);
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
            patientVitalModel = _patientServices.LoadVitalDataByVitalId(patientVitalId, SessionVariables.LoggedInUser.UserId);
            return View("VitalSign", patientVitalModel);
        }
        #endregion

        #region My clinical History
        public ActionResult PatientClinicalHistory()
        {
            var clinicalData = LoadAllData();
            return View(clinicalData);
        }
        [HttpPost]
        public ActionResult PatientClinicalHistory(PatientClinicalViewModel patientClinicalViewModel)
        {
            var isAdded = _patientServices.SavepatientClinicalData(patientClinicalViewModel);

            return RedirectToAction("PatientClinicalHistory");
        }

        [HttpPost]
        public ActionResult patientCondition(PatientConditionInfo patientConditionInfo)
        {
            if (!string.IsNullOrEmpty(patientConditionInfo.patientConditionViewModel.Condition))
            {
                _patientServices.SavePatientConditiondata(patientConditionInfo, SessionVariables.LoggedInUser.UserId);
            }
            var clinicalData = LoadAllData();
            if (string.IsNullOrEmpty(patientConditionInfo.patientConditionViewModel.Condition))
            {
                ModelState.AddModelError("Condition", "Required Field");
            }
            return View("PatientClinicalHistory", clinicalData);
        }
        //[HttpDelete]
        public ActionResult DeletePatientCondition(int patientConditionId)
        {
            if (patientConditionId > 0 && SessionVariables.LoggedInUser.UserId > 0)
            {
                _patientServices.DeletepatientConditionData(patientConditionId);
            }
            return RedirectToAction("PatientClinicalHistory");
        }

        private PatientClinicalViewModel LoadAllData()
        {
            var patientClinicalViewModel = _patientServices.GetPatientClinicalData(SessionVariables.LoggedInUser.UserId);
            patientClinicalViewModel.PatientId = SessionVariables.LoggedInUser.UserId;
            patientClinicalViewModel.YesNoSelectionList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.ClinicalHistory).Where(x => x.Name != "Do not Know").ToList();
            patientClinicalViewModel.YesNoDontKnowList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.ClinicalHistory).Where(x => x.Name != "Sometime").ToList();
            patientClinicalViewModel.GeneralConditionList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.GeneralCondition).ToList();
            patientClinicalViewModel.EndocrineorDiabetesList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.EndocrineorDiabetes).ToList();
            patientClinicalViewModel.StomachList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Stomach).ToList();
            patientClinicalViewModel.UrinaryList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Urinary).ToList();
            patientClinicalViewModel.NeurologicalList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Neurological).ToList();
            patientClinicalViewModel.CardiovascularList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Cardiovascular).ToList();
            patientClinicalViewModel.RespiratoryList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Respiratory).ToList();
            patientClinicalViewModel.EyesList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Eyes).ToList();
            patientClinicalViewModel.EarList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Ear).ToList();
            patientClinicalViewModel.ObOrGynList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.ObOrGyn).ToList();
            patientClinicalViewModel.MusclesOrJointsList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.MusclesOrJoints).ToList();
            patientClinicalViewModel.SkinList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Skin).ToList();
            patientClinicalViewModel.CancerOrHematologyList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.CancerOrHematology).ToList();
            patientClinicalViewModel.DentalList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Dental).ToList();
            patientClinicalViewModel.PsychologicalList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Psychological).ToList();
            return patientClinicalViewModel;
        }
        #endregion
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

        public ActionResult Message()
        {
            return View();
        }
        public ActionResult SentMessage()
        {
            var patientMessageData = LoadpatientMessage();
            return View(patientMessageData);
        }

        #region My Provider Section
        public ActionResult MyProvider()
        {
            var favouriteDoctor = _patientServices.GetFavouriteDoctors(SessionVariables.LoggedInUser.UserId);
            return View(favouriteDoctor);
        }
        public ActionResult OnlineVisitConfirmation(int pageType, int doctorId)
        {
            ViewBag.pageType = pageType;
            var consentForm = new ConsentForm();
            ConsentForm.DoctorId = doctorId;
            if (pageType == (int)DoctorServiceType.VideoChat || pageType == (int)DoctorServiceType.VideoEmail || pageType == (int)DoctorServiceType.AskADoctor || pageType == (int)DoctorServiceType.PrescriptionRefill)
            {
                ConsentForm.ServiceType = pageType;
                return View(consentForm);
            }
            else
            {
                return RedirectToAction("MyProvider");
            }
        }
        [HttpPost]
        public ActionResult OnlineVisitConfirmation(ConsentForm consentForm)
        {
            if (ModelState.IsValid)
            {
                var currentpatientInfo = _patientServices.LoadPersonalDetailsByUserId(SessionVariables.LoggedInUser.UserId);
                if (currentpatientInfo != null)
                {
                    if (currentpatientInfo.SSN.ToLower() == consentForm.SSNNumber.ToLower())
                    {
                        return RedirectToAction("OnlineVisitInfo");
                    }
                    else
                    {
                        ModelState.AddModelError("SSNNumber", "SSN Number should be match with Patient SSN.");
                    }
                }
                else
                {
                    ModelState.AddModelError("SSNNumber", "Please add SSN Number from Patient MHR Section");
                }
            }
            var consentForm2 = new ConsentForm();
            return View("OnlineVisitConfirmation", consentForm2);
        }
        public ActionResult OnlineVisitInfo()
        {
            var myProvider = LoadStep1Data();
            myProvider.ComplaintTab = LoadStep2Data();
            return View(myProvider);
        }
        [HttpPost]
        public ActionResult OnlineVisitInfo(PersonalTab personalTab)
        {
            if (ModelState.IsValid && ConsentForm.ServiceType > 0)
            {
                int visitId = _patientServices.SaveStep1Data(personalTab.TermAndCondition, SessionVariables.LoggedInUser.UserId, ConsentForm.DoctorId, ConsentForm.ServiceType);
                if (visitId > 0)
                {
                    return RedirectToAction("OnlineVisitStep", new { step = 2, VisitId = visitId });
                }
            }
            var myProvider = LoadStep1Data();
            myProvider.ComplaintTab = LoadStep2Data();
            return View("OnlineVisitInfo", myProvider);
        }
        public ActionResult OnlineVisitStep(int step, int VisitId)
        {
            var currentTabData = LoadStep1Data();
            currentTabData.ComplaintTab = LoadStep2Data();
            currentTabData.ComplaintTab.Step1Id = VisitId;
            currentTabData.ComplaintTab.DoctorId = ConsentForm.DoctorId;
            currentTabData.videoTab = LoadStep5Data();
            return View("OnlineVisitInfo", currentTabData);
        }
        [HttpPost]
        public ActionResult OnlineVisitStep2(ComplaintTab complaintTab)
        {
            if (ModelState.IsValid)
            {
                int step2Id = _patientServices.SaveStep2Data(complaintTab, SessionVariables.LoggedInUser.UserId);
                if (step2Id > 0)
                {
                    return RedirectToAction("OnlineVisitStep", new { step = 3, VisitId = step2Id });
                }
            }
            return RedirectToAction("OnlineVisitStep", new { step = 3, VisitId = complaintTab.Step1Id });
        }
        [HttpPost]
        public ActionResult OnlineVisitStep5(VideoTab videoTab)
        {
            if (ModelState.IsValid)
            {
                int step5Id = _patientServices.SaveStep5Data(videoTab, SessionVariables.LoggedInUser.UserId);
                if (step5Id > 0)
                {
                    var doctorFeesInfo = _doctorServices.LoadDoctorFeesInfo(videoTab.DoctorId);
                    if (doctorFeesInfo != null && (doctorFeesInfo.AskDoctor > 0m || doctorFeesInfo.VideoVisit > 0m || doctorFeesInfo.FaceToFace > 0m || doctorFeesInfo.RxRefill > 0m))
                    {
                        return RedirectToAction("OnlineVisitStep", new { step = 6, VisitId = 0 });
                    }
                    else
                    {
                        //Clearing session related token.
                        ConsentForm.DoctorId = 0;
                        return View("VisitComplete");
                    }

                }
            }
            return RedirectToAction("OnlineVisitStep", new { step = 3, VisitId = 0 });
        }
        public ActionResult VisitComplete()
        {
            return View();
        }
        private MyProvider LoadStep1Data()
        {
            MyProvider myProvider = new MyProvider();
            myProvider.PersonalTab = _patientServices.LoadPersonalTabData(SessionVariables.LoggedInUser.UserId);
            LoadlistData(myProvider.PersonalTab.PatientPersonalModel);
            myProvider.PersonalTab.patientClinicalViewModel = LoadAllData();
            return myProvider;
        }

        private ComplaintTab LoadStep2Data()
        {
            var complainTab = new ComplaintTab();
            complainTab.ComplainTypeList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.ComplaintType);
            complainTab.BloodPressureList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.BloodPressure);
            complainTab.BreathingList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Breathing);
            complainTab.WeightList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Weight);
            return complainTab;
        }

        private VideoTab LoadStep5Data()
        {
            var videoTab = _doctorServices.LoadVideoTabData(SessionVariables.LoggedInUser.UserId);
            var patientDetails = _userLoginService.GetUserDetailsByUserId(SessionVariables.LoggedInUser.UserId);
            videoTab.PatientName = patientDetails.FirstName + " " + patientDetails.LastName;
            var doctorDetails = _userLoginService.GetUserDetailsByUserId(ConsentForm.DoctorId);
            videoTab.DoctorName = doctorDetails.FirstName + " " + doctorDetails.LastName;
            videoTab.DoctorEmail = doctorDetails.EmailId;
            videoTab.Title = doctorDetails.Position;
            videoTab.phone = doctorDetails.Phone1;
            videoTab.DoctorId = ConsentForm.DoctorId;
            return videoTab;
        }
        #endregion

        public ActionResult Search()
        {
            var searchData = new PatientSearchDoctorModel();
            searchData = LoadSearchListData(searchData);
            return View(searchData);
        }
        private PatientSearchDoctorModel LoadSearchListData(PatientSearchDoctorModel physicianModel)
        {

            physicianModel.StateList = _commonUtilityService.GetAllStates();
            physicianModel.SpecialistTypesList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Specialty);
            return physicianModel;
        }
        [HttpPost]
        public ActionResult SearchDoctor(PatientSearchDoctorModel patientSearchModel)
        {
            var searchData = _patientServices.GetDoctorByPatient(patientSearchModel, SearchType.SearchDoctor);
            searchData = LoadSearchListData(searchData);
            //searchData.DoctorsList = DoctorList;
            return View("Search", searchData);
        }
        [HttpPost]
        public ActionResult SearchHospital(PatientSearchDoctorModel patientSearchModel)
        {
            var searchData = _patientServices.GetDoctorByPatient(patientSearchModel, SearchType.SearchHospital);
            searchData = LoadSearchListData(searchData);
            //searchData.DoctorsList = DoctorList;
            return View("Search", searchData);
        }
        [HttpPost]
        public ActionResult SearchInsurance(PatientSearchDoctorModel patientSearchModel)
        {
            var searchData = _patientServices.GetDoctorByPatient(patientSearchModel, SearchType.SearchInsurance);
            searchData = LoadSearchListData(searchData);
            return View("Search", searchData);
        }
        public ActionResult SystemCheck()
        {
            return View();
        }

        public ActionResult AddFavouriteDoctor(int doctorId)
        {
            if (doctorId > 0)
            {
                _patientServices.AddFavouriteDoctor(doctorId);
            }
            return RedirectToAction("Search");
        }
        public ActionResult RemoveFavouriteDoctor(int favouriteId, bool isFromSearchpage = false)
        {
            if (favouriteId > 0)
            {
                _patientServices.RemoveFavouriteDoctor(favouriteId);
            }
            if (isFromSearchpage)
            {
                return RedirectToAction("Search");
            }
            else
            {
                return RedirectToAction("MyProvider");
            }
        }
        [HttpPost]
        public bool UpdatePrimaryDoctor(int favouriteId, bool isprimary)
        {
            try
            {
                if (favouriteId > 0)
                {
                    _patientServices.UpdatePrimaryDoctor(favouriteId, isprimary);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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