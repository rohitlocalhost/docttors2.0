using Docttors_portal.Common.Models;
using Docttors_portal.Filter;
using Docttors_portal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docttors_portal.Controllers
{
    [SessionCheck]
    public class DoctorController : BaseController
    {
        #region Initialize
        private readonly IDoctorServices _doctorServices;
        private readonly IUserLogOnService _userLoginService;
        #endregion
        public DoctorController(IDoctorServices doctorServices, IUserLogOnService userLoginService)
        {
            _doctorServices = doctorServices;
            _userLoginService = userLoginService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchPatient()
        {
            var searchPatient = new PatientSearchModel();
            return View(searchPatient);
        }
        [HttpPost]
        public ActionResult SearchPatient(PatientSearchModel patientSearchModel)
        {
            var searchPatient = new PatientSearchModel();
            _doctorServices.GetPatientByDoctor(patientSearchModel);
            return View(searchPatient);
        }
        public ActionResult Systemcheck()
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
                changePasswordModel.UserId = Convert.ToInt32(Session["UserId"]);
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
        public ActionResult Inbox()
        {
            return View();
        }

        public ActionResult Transactions()
        {
            return View();
        }
        public ActionResult Appointment()
        {
            return View();
        }
        public ActionResult ManageWebsite()
        {
            return View();
        }
    }
}