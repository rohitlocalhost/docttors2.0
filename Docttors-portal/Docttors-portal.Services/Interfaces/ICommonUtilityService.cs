using Docttors_portal.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
