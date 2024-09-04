using Docttors_portal.Common;
using Docttors_portal.Common.Models;
using Docttors_portal.Services.Interfaces;
using System.Web.Mvc;

namespace Docttors_portal.Controllers
{
    public class PatientController : BaseController
    {
        #region Initialize
        private readonly IPatientPersonalServices _personalServices;
        private readonly ICommonUtilityService _commonUtilityService;
        #endregion
        public PatientController(IPatientPersonalServices personalServices, ICommonUtilityService commonUtilityService)
        {
            _personalServices = personalServices;
            _commonUtilityService = commonUtilityService;
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
            var patientData = LoadlistData();
            return View(patientData);
        }

        [HttpPost]
        public ActionResult PatientPersonalDetails(PatientPersonalModel patientRegisterationModel)
        {
            if (ModelState.IsValid)
            {
                int PatientPersonalId = _personalServices.SavePatientDetails(patientRegisterationModel);
                patientRegisterationModel.PatientPersonalId = PatientPersonalId;
                ModelState.Clear();
            }
            return View(patientRegisterationModel);
        }


        private PatientPersonalModel LoadlistData()
        {
            var patientPersonalModel = new PatientPersonalModel();
            patientPersonalModel.StateList = _commonUtilityService.GetAllStates();
            patientPersonalModel.CountryList = _commonUtilityService.GetAllCountry();
            patientPersonalModel.GenderList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Gender);
           patientPersonalModel.MaritalMasterList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.MaritalStatus);
            patientPersonalModel.EducationMasterList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.EducationMaster);
            return patientPersonalModel;
        }
    }
}