using Docttors_portal.Common.Models;
using Docttors_portal.DataAccess.EntityModel;
using Docttors_portal.DataAccess.Interfaces;
using Docttors_portal.Entities.Classes;
using Docttors_portal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Docttors_portal.Services.Classes
{
    public class CommonUtilityService : ICommonUtilityService
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<State> _stateRepository;
        private IRepository<Country> _countryRepository;
        private IRepository<SpecialistInsurance> _insuranceRepository;
        private IRepository<DoctorSpecialty> _specialtyRepository;
        private IRepository<Entities.Classes.Type> _typeRepository;
        public CommonUtilityService(IUnitOfWork unitOfWork)
        {
            if (unitOfWork != null)
            {
                _unitOfWork = unitOfWork;
                _stateRepository = _unitOfWork.GetRepository<State>();
                _countryRepository = _unitOfWork.GetRepository<Country>();
                _insuranceRepository = _unitOfWork.GetRepository<SpecialistInsurance>();
                _specialtyRepository = _unitOfWork.GetRepository<DoctorSpecialty>();
                _typeRepository = _unitOfWork.GetRepository<Entities.Classes.Type>();
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

        #region Country List
        public List<NameIdModel> GetAllCountry()
        {
            try
            {
                var a =  _countryRepository.GetAll().Select(x => new NameIdModel() { Id = x.CountryId, Name = x.CountryName, Abbreviation = x.CountryName }).ToList();
                return a;
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

        #region TypeCategory List By CategoryId
        public List<NameIdModel> GetTypeCategoryByCategoryId(int TypeCategoryId)
        {
            Common.TypeCategory typeCategory = _typeRepository.GetEnumValue<Common.TypeCategory>(TypeCategoryId);
            string typeCategory1 = Enum.GetName(typeof(Common.TypeCategory), TypeCategoryId);
            try
            {
                //var test = _typeRepository.GetAll(x => x.TypeCategoryId == typeCategory).Select(x => new NameIdModel() { Id = x.TypeId, Name = x.Name, Abbreviation = x.Description })
                //    .ToList();

                return _typeRepository.GetAll().Select(x => new NameIdModel() { Id = x.TypeId, Name = x.Name, Abbreviation = x.Description })
                    .ToList();
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
