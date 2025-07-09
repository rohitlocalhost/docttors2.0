using Docttors_portal.Common.Models;
using System.Collections.Generic;

namespace Docttors_portal.Services.Interfaces
{
    public interface ICommonUtilityService
    {
        #region States List
        List<NameIdModel> GetAllStates();
        #endregion

        #region Insaurance List
        List<NameIdModel> GetAllInsaurance();
        #endregion
        #region Speciality List
        List<NameIdModel> GetAllSpeciality();
        #endregion

        #region Country List
        List<NameIdModel> GetAllCountry();
        #endregion

        #region Type List
        List<NameIdModel> GetTypeCategoryByCategoryId(int TypeCategoryId);
        #endregion

        #region SpecialistTypes List
        List<NameIdModel> GetAllSpecialistTypes();
        #endregion
    }
}
