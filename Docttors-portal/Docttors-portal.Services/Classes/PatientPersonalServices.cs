using Docttors_portal.Common.Models;
using Docttors_portal.DataAccess.Interfaces;
using Docttors_portal.Entities.Classes;
using Docttors_portal.Services.Interfaces;
using System;

namespace Docttors_portal.Services.Classes
{
    public class PatientPersonalServices : IPatientPersonalServices
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<PatientPersonalNew> _patientRepository;
        private IRepository<PatientPhysicianDetailsNew> _PhysicianRepository;
        private IRepository<PatientInsuranceNew> _insuranceRepository;

        public PatientPersonalServices(IUnitOfWork unitOfWork)
        {
            if (unitOfWork != null)
            {
                _unitOfWork = unitOfWork;
                _patientRepository = _unitOfWork.GetRepository<PatientPersonalNew>();
                _PhysicianRepository = _unitOfWork.GetRepository<PatientPhysicianDetailsNew>();
                _insuranceRepository = _unitOfWork.GetRepository<PatientInsuranceNew>();
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
            var ptData = new PatientPersonalModel();
            try
            {
                var patientpersonalData = _patientRepository.GetSingle(x => x.UserId == userId);
                if (patientpersonalData != null)
                {
                    ptData.PatientPersonalId = patientpersonalData.PatientPersonalId;
                    ptData.UserId = patientpersonalData.UserId;
                    ptData.Address = patientpersonalData.Address;
                    ptData.City = patientpersonalData.City;
                    ptData.ContactNo = patientpersonalData.ContactNo;
                    ptData.DOB = patientpersonalData.DOB.Date.ToString("yyyy-MM-dd");
                    ptData.EducationId = patientpersonalData.EducationId;
                    ptData.EmailId = patientpersonalData.EmailId;
                    ptData.FirstName = patientpersonalData.FirstName;
                    ptData.LastName = patientpersonalData.LastName;
                    ptData.EthnicityId = patientpersonalData.EthnicityId;
                    ptData.GenderId = patientpersonalData.GenderId;
                    ptData.GuardinshipReason = patientpersonalData.GuardinshipReason;
                    ptData.HeightId = patientpersonalData.HeightId;
                    ptData.IsGuardian = patientpersonalData.IsGuardian;
                    ptData.Ispatient = patientpersonalData.Ispatient;
                    ptData.MaritalStatusId = patientpersonalData.MaritalStatusId;
                    ptData.StateId = patientpersonalData.StateId;
                    ptData.MiddleName = patientpersonalData.MiddleName;
                    ptData.MRN = patientpersonalData.MRN;
                    ptData.SSN = patientpersonalData.SSN;
                    ptData.Weight = patientpersonalData.Weight;
                    ptData.WorkPhone = patientpersonalData.WorkPhone;
                    ptData.ZipCode = patientpersonalData.ZipCode;
                }
                else
                {
                    ptData = new PatientPersonalModel();
                }
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

        #region Insurance
        public int SavePatientInsuranceDetails(PatientInsuranceModel insurancelData)
        {
            try
            {
                var insurance = new PatientInsuranceNew()
                {
                    PatientInsuranceId = insurancelData.PatientInsuranceId,
                    UserId = insurancelData.UserId,
                    NameOfInsured = insurancelData.NameOfInsured,
                    RelationshipToPatient = insurancelData.RelationshipToPatient,
                    InsuranceIdNumber = insurancelData.InsuranceIdNumber,
                    InsuranceGroupId = insurancelData.InsuranceGroupId,
                    InsuranceCompanyName = insurancelData.InsuranceCompanyName,
                    InsuranceCompanyWebsite = insurancelData.InsuranceCompanyWebsite,
                    InsuranceCompanyAddress = insurancelData.InsuranceCompanyAddress,
                    InsuranceCompanyCity = insurancelData.InsuranceCompanyCity,
                    InsuranceCompanyStateId = insurancelData.InsuranceCompanyStateId,
                    InsuranceCompanyCountryId = insurancelData.InsuranceCompanyCountryId,
                    InsuranceCompanyZipcode = insurancelData.InsuranceCompanyZipcode,
                    InsuranceCompanyPhone = insurancelData.InsuranceCompanyPhone,
                    InsuranceCompanyFax = insurancelData.InsuranceCompanyFax,
                    EligibilityStartDate = Convert.ToDateTime(insurancelData.EligibilityStartDate),
                    EligibilityEndDate = Convert.ToDateTime(insurancelData.EligibilityEndDate),
                };
                _insuranceRepository.Add(insurance);
                return insurance.PatientInsuranceId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PatientInsuranceModel LoadInsuranceDetailsByUserId(int userId)
        {
            var ptData = new PatientInsuranceModel();
            try
            {
                var insurancelData = _insuranceRepository.GetSingle(x => x.UserId == userId);
                if (insurancelData != null)
                {
                    ptData.PatientInsuranceId = insurancelData.PatientInsuranceId;
                    ptData.UserId = insurancelData.UserId;
                    ptData.NameOfInsured = insurancelData.NameOfInsured;
                    ptData.RelationshipToPatient = insurancelData.RelationshipToPatient;
                    ptData.InsuranceIdNumber = insurancelData.InsuranceIdNumber;
                    ptData.InsuranceGroupId = insurancelData.InsuranceGroupId;
                    ptData.InsuranceCompanyName = insurancelData.InsuranceCompanyName;
                    ptData.InsuranceCompanyWebsite = insurancelData.InsuranceCompanyWebsite;
                    ptData.InsuranceCompanyAddress = insurancelData.InsuranceCompanyAddress;
                    ptData.InsuranceCompanyCity = insurancelData.InsuranceCompanyCity;
                    ptData.InsuranceCompanyStateId = insurancelData.InsuranceCompanyStateId;
                    ptData.InsuranceCompanyCountryId = insurancelData.InsuranceCompanyCountryId;
                    ptData.InsuranceCompanyZipcode = insurancelData.InsuranceCompanyZipcode;
                    ptData.InsuranceCompanyPhone = insurancelData.InsuranceCompanyPhone;
                    ptData.InsuranceCompanyFax = insurancelData.InsuranceCompanyFax;
                    ptData.EligibilityStartDate = insurancelData.EligibilityStartDate.Date.ToString("yyyy-MM-dd");
                    ptData.EligibilityEndDate = insurancelData.EligibilityEndDate.Date.ToString("yyyy-MM-dd");
                }
                else
                {
                    ptData = new PatientInsuranceModel();
                }
                return ptData;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool UpdateInsuranceDetails(PatientInsuranceModel insurancelData)
        {
            try
            {
                var patientInsurance = _insuranceRepository.GetSingle(x => x.PatientInsuranceId == insurancelData.PatientInsuranceId);
                if (patientInsurance != null)
                {
                    patientInsurance.PatientInsuranceId = insurancelData.PatientInsuranceId;
                    patientInsurance.UserId = insurancelData.UserId;
                    patientInsurance.NameOfInsured = insurancelData.NameOfInsured;
                    patientInsurance.RelationshipToPatient = insurancelData.RelationshipToPatient;
                    patientInsurance.InsuranceIdNumber = insurancelData.InsuranceIdNumber;
                    patientInsurance.InsuranceGroupId = insurancelData.InsuranceGroupId;
                    patientInsurance.InsuranceCompanyName = insurancelData.InsuranceCompanyName;
                    patientInsurance.InsuranceCompanyWebsite = insurancelData.InsuranceCompanyWebsite;
                    patientInsurance.InsuranceCompanyAddress = insurancelData.InsuranceCompanyAddress;
                    patientInsurance.InsuranceCompanyCity = insurancelData.InsuranceCompanyCity;
                    patientInsurance.InsuranceCompanyStateId = insurancelData.InsuranceCompanyStateId;
                    patientInsurance.InsuranceCompanyCountryId = insurancelData.InsuranceCompanyCountryId;
                    patientInsurance.InsuranceCompanyZipcode = insurancelData.InsuranceCompanyZipcode;
                    patientInsurance.InsuranceCompanyPhone = insurancelData.InsuranceCompanyPhone;
                    patientInsurance.InsuranceCompanyFax = insurancelData.InsuranceCompanyFax;
                    patientInsurance.EligibilityStartDate = Convert.ToDateTime(insurancelData.EligibilityStartDate);
                    patientInsurance.EligibilityEndDate = Convert.ToDateTime(insurancelData.EligibilityEndDate);
                    patientInsurance.ModifiedBy = insurancelData.UserId;
                    patientInsurance.ModifiedOn = DateTime.Now;
                    _insuranceRepository.Update(patientInsurance);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
