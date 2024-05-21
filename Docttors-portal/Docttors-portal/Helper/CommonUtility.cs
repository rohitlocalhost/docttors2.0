using Docttors_portal.DataAccess.Interfaces;
using Docttors_portal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Docttors_portal.Helper
{
    public static class CommonUtility
    {
        private static IUnitOfWork _uow;
        private static ICommonUtilityService commonUtilityService { get; set; }
        private static IUserLogOnService userLoginService { get; set; }

        public static IUnitOfWork Uow
        {
            get { return _uow; }
            set { _uow = value; }
        }
        public static IUserLogOnService UserLogOnService
        {
            get { return userLoginService; }
            set { userLoginService = value; }
        }
        public static ICommonUtilityService CommonUtilityService
        {
            get { return commonUtilityService; }
            set { commonUtilityService = value; }
        }
    }
}