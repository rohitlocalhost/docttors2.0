using Docttors_portal.Common;
using Docttors_portal.Common.Models;
using Docttors_portal.DataAccess.EntityModel;
using Docttors_portal.DataAccess.Interfaces;
using Docttors_portal.Entities.Classes;
using Docttors_portal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Services.Classes
{
    public class UserLogOnService : IUserLogOnService
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<AppUser> _userRepository;

        public UserLogOnService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork != null)
            {
                _unitOfWork = unitOfWork;
                _userRepository = _unitOfWork.GetRepository<AppUser>();
            }
        }

        #region User Login

        /// <summary>
        /// Created By : Rohit Airi
        /// Date : 24-02-2023
        /// Details : Checks whether the user email and password exist in the database or not
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public long UserLogOn(string email, string password)
        {
            AppUser objUser = _userRepository.GetSingle(x => x.EmailId == email);
            if (objUser != null)
            {
                string userPwd = string.Empty;
                if (!string.IsNullOrEmpty(objUser.Password))
                {
                    userPwd = Utilities.EncryptPassword(objUser.Password);
                }
                else
                {
                    userPwd = Utilities.EncryptPassword(password);
                }
                userPwd = Utilities.EncryptPassword(password + objUser.Password);
                if (IsPasswordMatching(objUser.Password, userPwd))
                {
                    return objUser.Id;
                }
                else
                {
                    return 0;
                }

            }
            else
                return 0;
        }

        /// <summary>
        /// Created By : Rohit Airi
        /// Date : 24-02-2023
        /// Details : Returns the user details on the basis of userID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public AppUser GetUserDetailsByUserId(long userId)
        {
            return _userRepository.GetSingle(x => x.Id == userId);
        }

        public int AddNewUser(UserRegistrationModel userRegistrationModel)
        {
            try
            {
                var Newuser = new AppUser()
                {
                    city = userRegistrationModel.City,
                    CreatedBy = 0,
                    CreatedOn = DateTime.Now,
                    ModifiedBy=0,
                    ModifiedOn=DateTime.Now,
                    Dea = userRegistrationModel.DEA,
                    EmailId = userRegistrationModel.EmailAddress,
                    Fax = userRegistrationModel.Fax,
                    FirstName = userRegistrationModel.FirstName,
                    InsuranceId = userRegistrationModel.SelectedInsurance[0],
                    LastName = userRegistrationModel.LastName,
                    MiddileName = userRegistrationModel.MiddleName,
                    NPI = userRegistrationModel.NPI,
                    OfficeAddress = userRegistrationModel.Address,
                    Password = userRegistrationModel.Password,
                    Phone1 = userRegistrationModel.Phone1,
                    Phone2 = userRegistrationModel.Phone2,
                    Position = userRegistrationModel.Position,
                    PracticeName = userRegistrationModel.PracticeName,
                    SecondaryLicense = userRegistrationModel.SecLicense,
                    specialityId = userRegistrationModel.SpecialtyId,
                    State = userRegistrationModel.StateId,
                    StateLicense = userRegistrationModel.License,
                    UserRoleId = (int)RoleEnum.Doctor,
                    ZipCode = userRegistrationModel.ZipCode

                };
                _userRepository.Add(Newuser);
                return Newuser.Id;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        private bool IsPasswordMatching(string oPwd, string dbPwd)
        {
            if (string.Compare(oPwd, dbPwd) == 0)
                return true;
            else
                return false;
        }

    }
}
