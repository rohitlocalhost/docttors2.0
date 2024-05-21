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
        [AllowAnonymous]
        public ActionResult DoctorRegisteration(UserRegistrationModel userRegistrationModel)
        {
            if (ModelState.IsValid)
            {
                //here will be insertion Code.
                userRegistrationModel.Password = Utilities.EncryptPassword(userRegistrationModel.CPassword);
                int NewUserId = _userLoginService.AddNewUser(userRegistrationModel);
                var userRegisterationData = LoadlistData();
                userRegisterationData.RegistrationID = NewUserId;
                ModelState.Clear();
                return View(userRegisterationData);
            }
            return View();
        }
        public ActionResult patientregister()
        {
            return View();
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