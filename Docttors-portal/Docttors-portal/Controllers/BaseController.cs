using Docttors_portal.DataAccess.EntityModel;
using Docttors_portal.DataAccess.Interfaces;
using Docttors_portal.DataAccess.Repositories;
using Docttors_portal.Helper;
using Docttors_portal.Services.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Docttors_portal.Controllers
{
    //uncomment this [RequireHttps] tag to run on live server with SSL settings
    //[RequireHttps]
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        private IUnitOfWork _uow;

        public IUnitOfWork Uow
        {
            get { return _uow; }
            set { _uow = value; }
        }

        public BaseController()
            : this(new UnitOfWork<DocttorsEntities>())
        { }

        public BaseController(IUnitOfWork uow)
        {
            _uow = uow;
            CommonUtility.Uow = _uow;

            if (CommonUtility.CommonUtilityService == null)
            {
                CommonUtility.CommonUtilityService = new CommonUtilityService(CommonUtility.Uow);
            }
            if (CommonUtility.UserLogOnService == null)
            {
                CommonUtility.UserLogOnService = new UserLogOnService(CommonUtility.Uow);
            }
        }

        /// <summary>
        /// Created By : Rohit Airi
        /// Date : 24-02-2023
        /// Details : Renders the partial view as string
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public string RenderRazorViewToString(string viewName, object model)
        {
            try
            {
                ViewData.Model = model;
                using (var sw = new System.IO.StringWriter())
                {
                    var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                    var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                    viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                    viewResult.View.Render(viewContext, sw);
                    return Convert.ToString(sw.GetStringBuilder());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Created By : Rohit Airi
        /// Date : 20-13-2014
        /// Details : Handles the filtercontext on result executing
        /// </summary>
        public class NoCache : ActionFilterAttribute
        {
            public override void OnResultExecuting(ResultExecutingContext filterContext)
            {
                try
                {
                    filterContext.HttpContext.Response.Cache.SetExpires(System.DateTime.UtcNow.AddDays(-1));
                    filterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
                    filterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                    filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    filterContext.HttpContext.Response.Cache.SetNoStore();
                    base.OnResultExecuting(filterContext);
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Handles all the exceptions ouccring in the application
        /// </summary>
        /// <param name="objException"></param>
        public void LogException(Exception objException)
        {
            System.Diagnostics.StackTrace objTrace = new System.Diagnostics.StackTrace(objException, true);
            String ErrorMessage = String.Empty;
            string CustomMessage = string.Empty;

            if (objTrace != null)
            {
                String FileName = objTrace.GetFrame(0).GetFileName();

                ErrorMessage = "ControllerName = " + (FileName == null ? "" : FileName.Substring(FileName.LastIndexOf("\\") + 1))
                               + ", MethodName = " + objTrace.GetFrame(0).GetMethod().Name
                               + ", Line Number =" + objTrace.GetFrame(0).GetFileLineNumber()
                               + ", ColumnNumber = " + objTrace.GetFrame(0).GetFileColumnNumber()
                               + ", ErrorMessage:- " + objException.Message;
            }

            if (this.Request != null)
            {
                foreach (String item in this.Request.QueryString.AllKeys)
                    CustomMessage += "[" + item + " : " + Request.QueryString[item] + "], ";

                foreach (String item in this.Request.Form.AllKeys)
                    CustomMessage += "[" + item + " : " + Request.Form[item] + "], ";
            }

            LogExceptionIntoDatabase(objException, ErrorMessage, CustomMessage);
        }

        /// <summary>
        /// Logs the exceptions into the database
        /// </summary>
        /// <param name="objException"></param>
        /// <param name="ErrorMessage"></param>
        /// <param name="CustomMessage"></param>
        public void LogExceptionIntoDatabase(Exception objException, String ErrorMessage, String CustomMessage)
        {
            DocttorsEntities errRepo = new DocttorsEntities();
            try
            {
                System.Diagnostics.StackTrace objTrace = new System.Diagnostics.StackTrace(objException, true);
                String FileName = objTrace.GetFrame(0).GetFileName();
                //ErrorLog errolog = new ErrorLog();
                //errolog.EventDateTime = DateTime.UtcNow;
                //errolog.UserName = SessionVariables.LoggedInUser.Name;
                //errolog.MachineName = Request.ServerVariables["REMOTE_ADDR"];
                //errolog.EventMessage = CustomMessage;
                //errolog.ErrorSource = objException.Source;
                //errolog.ErrorClass = (FileName == null ? "" : FileName.Substring(FileName.LastIndexOf("\\") + 1));
                //errolog.ErrorMethod = objTrace.GetFrame(0).GetMethod().Name;
                //errolog.ErrorMessage = ErrorMessage;
                //errolog.InnerErrorMessage = Convert.ToString(objException.InnerException);
                //errRepo.ErrorLogs.Add(errolog);
                //errRepo.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                //errRepo.Dispose();
            }
        }

        /// <summary>
        /// Logs the exception into the database
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            LogException(filterContext.Exception);

            // Display a nice error page
            if (filterContext.HttpContext.IsCustomErrorEnabled)
            {
                filterContext.ExceptionHandled = true;
            }
        }
    }
}