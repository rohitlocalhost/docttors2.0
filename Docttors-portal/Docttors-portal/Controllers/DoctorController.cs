using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docttors_portal.Controllers
{
    public class DoctorController : BaseController
    {
        // GET: Doctor
        public ActionResult Index()
        {
            return View();
        }
    }
}