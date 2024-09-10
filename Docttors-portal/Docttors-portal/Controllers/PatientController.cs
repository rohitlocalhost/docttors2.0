using Docttors_portal.Common;
using Docttors_portal.Common.Models;
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
            //_userId = Convert.ToInt64(Session["UserId"]);
        }
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MyHealthRecord()
        {
            return View();
        }
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
        public ActionResult PersonalDetails()
        {
            var patientPersonalModel = new PatientPersonalModel();
            patientPersonalModel = _personalServices.LoadPersonalDetailsByUserId(Convert.ToInt32(Session["UserId"]));
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
                    patientRegisterationModel.UserId = Convert.ToInt32(Session["UserId"]);
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

        #region Physician details
        public ActionResult PhysicianDetails()
        {
            var physicanData = _physicianServices.LoadPhysicianDetailsByUserId(Convert.ToInt32(Session["UserId"]));
            physicanData = LoadlistPhysicianData(physicanData);
            return View(physicanData);
        }

        private PatientPhysicianModel LoadlistPhysicianData(PatientPhysicianModel physicianModel)
        {

            physicianModel.StateList = _commonUtilityService.GetAllStates();
            physicianModel.SpecialistTypesList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Specialty);
            return physicianModel;
        }

        [HttpPost]
        public ActionResult PatientPhysicianDetails(PatientPhysicianModel patientRegisterationModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    patientRegisterationModel.UserId = Convert.ToInt32(Session["UserId"]);
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
        #endregion

        #region Insuarance
        public ActionResult Insurance()
        {
            var insuranceModel = _personalServices.LoadInsuranceDetailsByUserId(Convert.ToInt32(Session["UserId"]));
            insuranceModel = LoadListInsuranceData(insuranceModel);
            return View(insuranceModel);
        }
        private PatientInsuranceModel LoadListInsuranceData(PatientInsuranceModel insuranceModel)
        {

            insuranceModel.StateList = _commonUtilityService.GetAllStates();
            insuranceModel.CountryList = _commonUtilityService.GetAllCountry();
            return insuranceModel;
        }
     
        [HttpPost]
        public ActionResult InsuranceDetails(PatientInsuranceModel patientInsuranceModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    patientInsuranceModel.UserId = Convert.ToInt32(Session["UserId"]);
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
                return PersonalDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}