using Docttors_portal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docttors_portal.Filter
{
    public class MyExceptionHandler : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            int userRoleId = Convert.ToInt32(HttpContext.Current.Session["UserRole"]);
            if (filterContext.ExceptionHandled || filterContext.HttpContext.IsCustomErrorEnabled)
            {
                Exception ex = filterContext.Exception;
                filterContext.ExceptionHandled = true;
                if (userRoleId == (int)RoleEnum.Patient)
                {
                    filterContext.Result = new ViewResult()
                    {
                        ViewName = "PatientError"
                    };
                }
                else if (userRoleId == (int)RoleEnum.Doctor)
                {
                    filterContext.Result = new ViewResult()
                    {
                        ViewName = "DoctorError"
                    };
                }
            }
            else
            {

                Exception e = filterContext.Exception;
                filterContext.ExceptionHandled = true;
                filterContext.Result = new ViewResult()
                {
                    ViewName = "Error"
                };
            }
        }
    }
}