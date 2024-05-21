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
    public class LoginController : BaseController
    {
        #region Initialize
        private readonly IUserLogOnService _userLoginService;
        #endregion

        public LoginController(IUserLogOnService userLoginService)
        {
            _userLoginService = userLoginService;
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(UserLogOnModel objUserLogOnModel)
        {
            if (ModelState.IsValid)
            {
                long loginId = _userLoginService.UserLogOn(objUserLogOnModel.Email, objUserLogOnModel.Password);
                if (loginId > 0)
                {
                    var userDetails = _userLoginService.GetUserDetailsByUserId(loginId);
                    //RedirectToAction("Index", "Patient");
                    Session["UserName"] = userDetails.EmailId;
                    Session["UserId"] = loginId;
                    if (userDetails.UserRoleId == (int)RoleEnum.Patient)
                    {
                        return RedirectToAction("Index", "Patient");
                    }
                    else if (userDetails.UserRoleId == (int)RoleEnum.Doctor)
                    {
                        return RedirectToAction("Index", "Doctor");
                    }
                }
            }

            return View(objUserLogOnModel);
        }
        // GET: Login/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Login/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Login/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Login/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Login/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
