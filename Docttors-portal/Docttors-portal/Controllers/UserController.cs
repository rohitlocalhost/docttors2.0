using Docttors_portal.Common;
using Docttors_portal.Common.Models;
using Docttors_portal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docttors_portal.Controllers
{
    public class UserController : BaseController
    {
        #region Initialize
        private readonly IUserLogOnService _userLoginService;
        private readonly ICommonUtilityService _commonUtilityService;
        #endregion
        public UserController(IUserLogOnService userLoginService, ICommonUtilityService commonUtilityService)
        {
            _userLoginService = userLoginService;
            _commonUtilityService = commonUtilityService;
        }
        // GET: User
        public ActionResult DoctorRegisteration()
        {
            var userRegisterationData = LoadlistData();
            return View(userRegisterationData);
        }
        [HttpPost]
        public ActionResult DoctorRegisteration(UserRegistrationModel userRegistrationModel)
        {
            if (ModelState.IsValid)
            {
                //here will be insertion Code.
                userRegistrationModel.IsDoctor = true;
                userRegistrationModel.Password = Utilities.EncryptPassword(userRegistrationModel.CPassword);
                int NewUserId = _userLoginService.AddNewUser(userRegistrationModel);
                var userRegisterationData = LoadlistData();
                userRegisterationData.DoctorId = NewUserId;
                ModelState.Clear();
                return View(userRegisterationData);
            }
            return View();
        }
        public ActionResult patientregister()
        {
            var patientRegisterationModel = new PatientRegisterationModel();
            return View(patientRegisterationModel);
        }
        [HttpPost]
        public ActionResult patientregister(PatientRegisterationModel patientRegisterationModel)
        {
            if (ModelState.IsValid)
            {
                var userRegistertionModel = new UserRegistrationModel()
                {
                    FirstName = patientRegisterationModel.FirstName,
                    MiddleName = patientRegisterationModel.MiddleName,
                    LastName = patientRegisterationModel.LastName,
                    EmailAddress = patientRegisterationModel.EmailAddress,
                    Password = Utilities.EncryptPassword(patientRegisterationModel.Password),
                    IsDoctor = false
                };
                int NewUserId = _userLoginService.AddNewUser(userRegistertionModel);
                patientRegisterationModel.PatientId= NewUserId;
                ModelState.Clear();
            }
            return View(patientRegisterationModel);
        }
        private UserRegistrationModel LoadlistData()
        {
            var userRegisterationData = new UserRegistrationModel();
            userRegisterationData.StateList = _commonUtilityService.GetAllStates();
            userRegisterationData.AllInsaurance = _commonUtilityService.GetAllInsaurance();
            userRegisterationData.AllSpecialty = _commonUtilityService.GetAllSpeciality();
            return userRegisterationData;
        }
    }
}