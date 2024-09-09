using Docttors_portal.Common.Models;
using Docttors_portal.DataAccess.Interfaces;
using Docttors_portal.DataAccess.Repositories;
using Docttors_portal.Entities.Classes;
using Docttors_portal.Services.Interfaces;
using System;

namespace Docttors_portal.Services.Classes
{
    public class PatientPersonalServices : IPatientPersonalServices
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<PatientPersonalNew> _patientRepository;

        public PatientPersonalServices(IUnitOfWork unitOfWork)
        {
            if (unitOfWork != null)
            {
                _unitOfWork = unitOfWork;
                _patientRepository = _unitOfWork.GetRepository<PatientPersonalNew>();
            }
        }
        public int SavePatientDetails(PatientPersonalModel personalModel)
        {
            try
            {
                var patient = new PatientPersonalNew()
                {
                    FirstName = personalModel.FirstName,
                    LastName = personalModel.LastName,
                    MiddleName = personalModel.MiddleName,
                    WorkPhone = personalModel.WorkPhone,
                    ContactNo = personalModel.ContactNo,
                    Address = personalModel.Address,
                    City = personalModel.City,
                    StateId = personalModel.StateId,
                    ZipCode = personalModel.ZipCode,
                    GenderId = personalModel.GenderId,
                    DOB = Convert.ToDateTime(personalModel.DOB),
                    SSN = personalModel.SSN,
                    HeightId = personalModel.HeightId,
                    Weight = personalModel.Weight,
                    MaritalStatusId = personalModel.MaritalStatusId,
                    EducationId = personalModel.EducationId,
                    MRN = personalModel.MRN,
                    EmailId = personalModel.EmailId,
                    UserId = personalModel.UserId,
                    EthnicityId = personalModel.EthnicityId,
                    GuardinshipReason = personalModel.GuardinshipReason,
                    Ispatient = personalModel.Ispatient == true ? true : false,
                    IsGuardian = personalModel.Ispatient == false ? true : false,
                    CreatedBy = personalModel.UserId,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = personalModel.UserId,
                    ModifiedOn = DateTime.Now
                };
                _patientRepository.Add(patient);
                return patient.PatientPersonalId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public PatientPersonalModel LoadPersonalDetailsByUserId(int userId)
        {
            try
            {
                var patientpersonalData = _patientRepository.GetSingle(x => x.UserId == userId);
                var ptData = new PatientPersonalModel()
                {
                    PatientPersonalId = patientpersonalData.PatientPersonalId,
                    UserId = patientpersonalData.UserId,
                    Address = patientpersonalData.Address,
                    City = patientpersonalData.City,
                    ContactNo = patientpersonalData.ContactNo,
                    DOB = patientpersonalData.DOB.Date.ToString("yyyy-MM-dd"),
                    EducationId = patientpersonalData.EducationId,
                    EmailId = patientpersonalData.EmailId,
                    FirstName = patientpersonalData.FirstName,
                    LastName = patientpersonalData.LastName,
                    EthnicityId = patientpersonalData.EthnicityId,
                    GenderId = patientpersonalData.GenderId,
                    GuardinshipReason = patientpersonalData.GuardinshipReason,
                    HeightId = patientpersonalData.HeightId,
                    IsGuardian = patientpersonalData.IsGuardian,
                    Ispatient = patientpersonalData.Ispatient,
                    MaritalStatusId = patientpersonalData.MaritalStatusId,
                    StateId = patientpersonalData.StateId,
                    MiddleName = patientpersonalData.MiddleName,
                    MRN = patientpersonalData.MRN,
                    SSN = patientpersonalData.SSN,
                    Weight = patientpersonalData.Weight,
                    WorkPhone = patientpersonalData.WorkPhone,
                    ZipCode = patientpersonalData.ZipCode
                };
                return ptData;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool UpdatePatientDetails(PatientPersonalModel personalModel)
        {
            try
            {
                var currentpatientDetails = _patientRepository.GetSingle(x => x.PatientPersonalId == personalModel.PatientPersonalId);
                if (currentpatientDetails != null)
                {
                    currentpatientDetails.FirstName = personalModel.FirstName;
                    currentpatientDetails.LastName = personalModel.LastName;
                    currentpatientDetails.MiddleName = personalModel.MiddleName;
                    currentpatientDetails.WorkPhone = personalModel.WorkPhone;
                    currentpatientDetails.ContactNo = personalModel.ContactNo;
                    currentpatientDetails.Address = personalModel.Address;
                    currentpatientDetails.City = personalModel.City;
                    currentpatientDetails.StateId = personalModel.StateId;
                    currentpatientDetails.ZipCode = personalModel.ZipCode;
                    currentpatientDetails.GenderId = personalModel.GenderId;
                    currentpatientDetails.DOB = Convert.ToDateTime(personalModel.DOB);
                    currentpatientDetails.SSN = personalModel.SSN;
                    currentpatientDetails.HeightId = personalModel.HeightId;
                    currentpatientDetails.Weight = personalModel.Weight;
                    currentpatientDetails.MaritalStatusId = personalModel.MaritalStatusId;
                    currentpatientDetails.EducationId = personalModel.EducationId;
                    currentpatientDetails.MRN = personalModel.MRN;
                    currentpatientDetails.EmailId = personalModel.EmailId;
                    currentpatientDetails.UserId = personalModel.UserId;
                    currentpatientDetails.EthnicityId = personalModel.EthnicityId;
                    currentpatientDetails.GuardinshipReason = personalModel.GuardinshipReason;
                    currentpatientDetails.Ispatient = personalModel.Ispatient == true ? true : false;
                    currentpatientDetails.IsGuardian = personalModel.Ispatient == false ? true : false;
                    currentpatientDetails.ModifiedBy = personalModel.UserId;
                    currentpatientDetails.ModifiedOn = DateTime.Now;
                    _patientRepository.Update(currentpatientDetails);
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
