using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docttors_portal.Controllers
{
    public class PatientController : BaseController
    {
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
    }
}