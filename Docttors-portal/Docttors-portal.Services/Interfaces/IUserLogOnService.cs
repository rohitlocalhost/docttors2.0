using Docttors_portal.Common.Models;
using Docttors_portal.DataAccess.EntityModel;
using Docttors_portal.Entities.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Services.Interfaces
{
    public interface IUserLogOnService
    {
        #region User Login

        long UserLogOn(string email, string password);

        AppUser GetUserDetailsByUserId(long userId);

        int AddNewUser(UserRegistrationModel userRegistrationModel);

        #endregion
        #region Password
        bool ChangePassword(ChangePasswordModel changePasswordModel);
        #endregion
    }
}
