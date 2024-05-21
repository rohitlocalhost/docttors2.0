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
    public class CommonUtilityService : ICommonUtilityService
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<State> _stateRepository;
        private IRepository<SpecialistInsurance> _insuranceRepository;
        private IRepository<DoctorSpecialty> _specialtyRepository;
        public CommonUtilityService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork != null)
            {
                _unitOfWork = unitOfWork;
                _stateRepository = _unitOfWork.GetRepository<State>();
                _insuranceRepository = _unitOfWork.GetRepository<SpecialistInsurance>();
                _specialtyRepository = _unitOfWork.GetRepository<DoctorSpecialty>();
            }
        }
        #region States List
        public List<NameIdModel> GetAllStates()
        {
            try
            {
                return _stateRepository.GetAll().Select(x => new NameIdModel() { Id = x.StateId, Name = x.StateName, Abbreviation = x.Abbreviation }).ToList();
            }
            catch
            {
                return new List<NameIdModel>();
            }
            finally
            {

            }
        }
        #endregion

        #region Insaurance List
        public List<NameIdModel> GetAllInsaurance()
        {
            var docttorsEntities = new DocttorsEntities();
            try
            {
                return _insuranceRepository.GetAll().Where(x => x.IsActive == true).Select(x => new NameIdModel() { Id = x.Id, Name = x.InsuranceName }).ToList();
            }
            catch (Exception ex)
            {
                return new List<NameIdModel>();
            }
            finally
            {
            }
        }
        #endregion

        #region Speciality List
        public List<NameIdModel> GetAllSpeciality()
        {
            try
            {
                return _specialtyRepository.GetAll().Select(x => new NameIdModel() { Id = x.SpecialtyId, Name = x.SpecialtyName }).ToList();
            }
            catch
            {
                return new List<NameIdModel>();
            }
            finally
            {
            }
        }
        #endregion
    }
}
