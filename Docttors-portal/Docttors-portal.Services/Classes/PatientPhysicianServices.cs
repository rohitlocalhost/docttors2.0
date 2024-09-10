using Docttors_portal.Common.Models;
using Docttors_portal.DataAccess.Interfaces;
using Docttors_portal.Entities.Classes;
using Docttors_portal.Services.Interfaces;
using System;

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
                var patientPhysicianData = _PhysicianRepository.GetSingle(x => x.UserId == userId);
                if (patientPhysicianData != null)
                {
                    ptData.PatientPhysicianId = patientPhysicianData.PatientPhysicianId;
                    ptData.UserId = patientPhysicianData.UserId;
                    ptData.Address1 = patientPhysicianData.Address1;
                    ptData.Address2 = patientPhysicianData.Address2;
                    ptData.City = patientPhysicianData.City;
                    ptData.Phone = patientPhysicianData.Phone;
                    ptData.PhysicianFName = patientPhysicianData.PhysicianFirstName;
                    ptData.PhysicianLName = patientPhysicianData.PhysicianLastName;
                    ptData.PracticeName = patientPhysicianData.PracticeName;
                    ptData.PhysicianSpecialtyId = patientPhysicianData.PhysicianSpecialtyId;
                    ptData.StateId = patientPhysicianData.StateId;
                   ptData.ZipCode = patientPhysicianData.ZipCode;
                }
                else
                {
                    ptData = new PatientPhysicianModel();
                }
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

    }

}
