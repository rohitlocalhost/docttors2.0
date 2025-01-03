using Docttors_portal.Common;
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
        private readonly ICommonUtilityService _commonUtilityService;
        #endregion
        public DoctorController(IDoctorServices doctorServices, IUserLogOnService userLoginService, ICommonUtilityService commonUtilityService)
        {
            _doctorServices = doctorServices;
            _userLoginService = userLoginService;
            _commonUtilityService = commonUtilityService;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SearchPatient()
        {
            var searchPatient = new PatientSearchModel();
            searchPatient.IsSearchEnable = false;
            return View(searchPatient);
        }
        [HttpPost]
        public ActionResult SearchPatient(PatientSearchModel patientSearchModel)
        {
            var searchPatient = new PatientSearchModel();
            searchPatient.PatientData = _doctorServices.GetPatientByDoctor(patientSearchModel);
            searchPatient.IsSearchEnable = true;
            return View("SearchPatient", searchPatient);
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
        public ActionResult Appointment(string dateString)
        {
            try
            {
                DateTime searchDate = string.IsNullOrEmpty(dateString) ? DateTime.Now.Date : Convert.ToDateTime(dateString).Date;
                var appointmentData = _doctorServices.LoadDoctorAppointmentDataBySelectedDate(searchDate);
                appointmentData.SelectedDate = string.IsNullOrEmpty(dateString) ? DateTime.Today.Date.ToString("yyyy-MM-dd") : Convert.ToDateTime(dateString).Date.ToString("yyyy-MM-dd");

                return View(appointmentData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult Appointment(DoctorAppointmentData doctorAppointmentData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    doctorAppointmentData.UserId = SessionVariables.LoggedInUser.UserId;
                    _doctorServices.SaveDoctorAppointment(doctorAppointmentData);
                }
                var appointmentData = _doctorServices.LoadDoctorAppointmentData();
                return View(appointmentData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult ManageWebsite()
        {
            var doctorManagement = new DoctorManagement();
            doctorManagement = _doctorServices.LoadDoctorManageMentData(SessionVariables.LoggedInUser.UserId);
            doctorManagement.StateList = _commonUtilityService.GetAllStates();
            doctorManagement.AllInsaurance = _commonUtilityService.GetAllInsaurance();
            doctorManagement.AllSpecialty = _commonUtilityService.GetAllSpeciality();

            return View(doctorManagement);
        }

        [HttpPost]
        public ActionResult ManageWebsite(DoctorManagement doctorManagement)
        {
            try
            {
                _doctorServices.AddUpdateDoctorInformation(doctorManagement);
                return RedirectToAction("ManageWebsite");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult AddDoctorEmail(DoctorManagement doctorManagement)
        {
            try
            {
                _doctorServices.AddUpdateDoctorEmailConfig(doctorManagement);
                return RedirectToAction("ManageWebsite");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult AddDoctorFees(DoctorManagement doctorManagement)
        {
            try
            {
                _doctorServices.AddUpdateDoctorOnlineFees(doctorManagement);
                return RedirectToAction("ManageWebsite");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult AddDoctorContact(DoctorManagement doctorManagement)
        {
            try
            {
                _doctorServices.AddUpdateDoctorContact(doctorManagement);
                return RedirectToAction("ManageWebsite");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult AddDoctoreNews(DoctorManagement doctorManagement, string Command)
        {
            try
            {
                if (Command.ToLower() == "save")
                {
                    _doctorServices.AddUpdateDoctorenews(doctorManagement);
                }
                return RedirectToAction("ManageWebsite");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpDelete]
        public ActionResult DeleteNews(int Id)
        {
            try
            {
                return RedirectToAction("ManageWebsite");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}