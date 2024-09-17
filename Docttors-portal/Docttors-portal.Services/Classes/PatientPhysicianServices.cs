using Docttors_portal.Common.Models;
using Docttors_portal.DataAccess.Interfaces;
using Docttors_portal.Entities.Classes;
using Docttors_portal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Docttors_portal.Services.Classes
{
    public class PatientPhysicianServices : IPatientPhysicianServices
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<PatientPhysicianDetailsNew> _PhysicianRepository;

        public PatientPhysicianServices(IUnitOfWork unitOfWork)
        {
            if (unitOfWork != null)
            {
                _unitOfWork = unitOfWork;
                _PhysicianRepository = _unitOfWork.GetRepository<PatientPhysicianDetailsNew>();
            }
        }

        public PatientPhysicianModel LoadPhysicianDetailsByUserId(int userId)
        {
            var ptData = new PatientPhysicianModel();
            try
            {
                ptData.patientPhysicianData = _PhysicianRepository.GetAll(x => x.UserId == userId).Select(x =>
                    new PatientPhysicianModel()
                    {
                        PatientPhysicianId = x.PatientPhysicianId,
                        UserId = x.UserId,
                        Address1 = x.Address1,
                        Address2 = x.Address2,
                        City = x.City,
                        Phone = x.Phone,
                        PhysicianFName = x.PhysicianFirstName,
                        PhysicianLName = x.PhysicianLastName,
                        PracticeName = x.PracticeName,
                        PhysicianSpecialtyId = x.PhysicianSpecialtyId,
                        StateId = x.StateId,
                        ZipCode = x.ZipCode,
                        IsNone = x.IsNoneSelected
                    }
                    ).ToList();
                return ptData;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public int SavePatientPhysicianDetails(PatientPhysicianModel physicianModel)
        {
            try
            {
                var patient = new PatientPhysicianDetailsNew()
                {
                    PhysicianFirstName = physicianModel.PhysicianFName,
                    PhysicianLastName = physicianModel.PhysicianLName,
                    PracticeName = physicianModel.PracticeName,
                    PhysicianSpecialtyId = physicianModel.PhysicianSpecialtyId,
                    Phone = physicianModel.Phone,
                    Address1 = physicianModel.Address1,
                    Address2 = physicianModel.Address2,
                    City = physicianModel.City,
                    StateId = physicianModel.StateId,
                    ZipCode = physicianModel.ZipCode,
                    IsNoneSelected = physicianModel.IsNone,
                    UserId = physicianModel.UserId,
                    CreatedBy = physicianModel.UserId,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = physicianModel.UserId,
                    ModifiedOn = DateTime.Now
                };
                _PhysicianRepository.Add(patient);
                return patient.PatientPhysicianId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public bool UpdatePatientPhysicianDetails(PatientPhysicianModel physicianModel)
        {
            try
            {
                var physicianDetails = _PhysicianRepository.GetSingle(x => x.PatientPhysicianId == physicianModel.PatientPhysicianId);
                if (physicianDetails != null)
                {
                    physicianDetails.PhysicianFirstName = physicianModel.PhysicianFName;
                    physicianDetails.PhysicianLastName = physicianModel.PhysicianLName;
                    physicianDetails.PracticeName = physicianModel.PracticeName;
                    physicianDetails.Phone = physicianModel.Phone;
                    physicianDetails.Address1 = physicianModel.Address1;
                    physicianDetails.Address2 = physicianModel.Address2;
                    physicianDetails.City = physicianModel.City;
                    physicianDetails.StateId = physicianModel.StateId;
                    physicianDetails.ZipCode = physicianModel.ZipCode;
                    physicianDetails.PhysicianSpecialtyId = physicianModel.PhysicianSpecialtyId;
                    physicianDetails.UserId = physicianModel.UserId;
                    physicianDetails.IsNoneSelected = physicianModel.IsNone;
                    physicianDetails.ModifiedBy = physicianModel.UserId;
                    physicianDetails.ModifiedOn = DateTime.Now;
                    _PhysicianRepository.Update(physicianDetails);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeletePatientPhysicianDetails(int patientPhysicianId)
        {
            try
            {
                var physicianDetails = _PhysicianRepository.GetSingle(x => x.PatientPhysicianId == patientPhysicianId);
                if (physicianDetails != null)
                {
                    _PhysicianRepository.Delete(physicianDetails);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PatientPhysicianModel LoadPhysicianDetailsByPhysicianId(int physicianId, int userId)
        {
            try
            {
                var AllpatientPhysicianData = LoadPhysicianDetailsByUserId(userId);
                var patientPhysicianData = _PhysicianRepository.GetSingle(x => x.PatientPhysicianId == physicianId);
                if (patientPhysicianData != null)
                {
                    AllpatientPhysicianData.PatientPhysicianId = patientPhysicianData.PatientPhysicianId;
                    AllpatientPhysicianData.UserId = patientPhysicianData.UserId;
                    AllpatientPhysicianData.Address1 = patientPhysicianData.Address1;
                    AllpatientPhysicianData.Address2 = patientPhysicianData.Address2;
                    AllpatientPhysicianData.City = patientPhysicianData.City;
                    AllpatientPhysicianData.Phone = patientPhysicianData.Phone;
                    AllpatientPhysicianData.PhysicianFName = patientPhysicianData.PhysicianFirstName;
                    AllpatientPhysicianData.PhysicianLName = patientPhysicianData.PhysicianLastName;
                    AllpatientPhysicianData.PracticeName = patientPhysicianData.PracticeName;
                    AllpatientPhysicianData.PhysicianSpecialtyId = patientPhysicianData.PhysicianSpecialtyId;
                    AllpatientPhysicianData.StateId = patientPhysicianData.StateId;
                    AllpatientPhysicianData.ZipCode = patientPhysicianData.ZipCode;
                }
                return AllpatientPhysicianData;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }

}
