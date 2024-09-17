using Docttors_portal.Common;
using Docttors_portal.Common.Models;
using Docttors_portal.Common.procedureModels;
using Docttors_portal.Filter;
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
        private readonly long _userId;
        #endregion
        public PatientController(IPatientPersonalServices personalServices, IPatientPhysicianServices physicianServices, ICommonUtilityService commonUtilityService)
        {
            _personalServices = personalServices;
            _commonUtilityService = commonUtilityService;
            _physicianServices = physicianServices;
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