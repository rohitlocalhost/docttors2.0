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
        private readonly ICommonUtilityService _commonUtilityService;
        private readonly long _userId;
        #endregion
        public PatientController(IPatientPersonalServices personalServices, ICommonUtilityService commonUtilityService)
        {
            _personalServices = personalServices;
            _commonUtilityService = commonUtilityService;
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
    }
}