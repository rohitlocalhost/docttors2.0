using Docttors_portal.Common.Models;
using Docttors_portal.Common.procedureModels;
using Docttors_portal.DataAccess.Interfaces;
using Docttors_portal.Entities.Classes;
using Docttors_portal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Linq;
using System.Globalization;

namespace Docttors_portal.Services.Classes
{
    public class PatientPersonalServices : IPatientPersonalServices
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<PatientPersonalNew> _patientRepository;
        private IRepository<PatientPhysicianDetailsNew> _PhysicianRepository;
        private IRepository<PatientInsuranceNew> _insuranceRepository;
        private IRepository<PatientEmergencyNew> _emergencyRepository;
        private IRepository<PatientObservationNew> _patientObservationRepository;

        public PatientPersonalServices(IUnitOfWork unitOfWork)
        {
            if (unitOfWork != null)
            {
                _unitOfWork = unitOfWork;
                _patientRepository = _unitOfWork.GetRepository<PatientPersonalNew>();
                _PhysicianRepository = _unitOfWork.GetRepository<PatientPhysicianDetailsNew>();
                _insuranceRepository = _unitOfWork.GetRepository<PatientInsuranceNew>();
                _emergencyRepository = _unitOfWork.GetRepository<PatientEmergencyNew>();
                _patientObservationRepository = _unitOfWork.GetRepository<PatientObservationNew>();
            }
        }
        #region MHR Page Load Service
        public GetMHRDataInfo GetMHRData(int UserId)
        {
            try
            {
                var mhrDataInfo = new GetMHRDataInfo();
                string connectionString = ConfigurationManager.ConnectionStrings["DocttorsEntities"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_GetMHRDataInfo", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                        con.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            //Accessing the data using the string key as index
                            mhrDataInfo.PersonalDetailsCreated = Convert.ToString(rdr["PersonalDetailsCreated"]);
                            mhrDataInfo.PersonalDetailsModified = Convert.ToString(rdr["PersonalDetailsModified"]);
                            mhrDataInfo.LastpersonalDetailsModifiedTime = Convert.ToString(rdr["LastpersonalDetailsModifiedTime"]);

                            mhrDataInfo.EmergencyContactCreated = Convert.ToString(rdr["EmergencyContactCreated"]);
                            mhrDataInfo.EmergencyContactModified = Convert.ToString(rdr["EmergencyContactModified"]);
                            mhrDataInfo.LastEmergencyContactModifiedTime = Convert.ToString(rdr["LastEmergencyContactModifiedTime"]);

                            mhrDataInfo.PhysicianDetailCreated = rdr["PhysicianDetailCreated"] != null ? Convert.ToString(rdr["PhysicianDetailCreated"]) : null;
                            mhrDataInfo.PhysicianDetailModified = rdr["PhysicianDetailModified"] != null ? Convert.ToString(rdr["PhysicianDetailModified"]) : null;
                            mhrDataInfo.LastPhysicianDetailModifiedTime = Convert.ToString(rdr["LastPhysicianDetailModifiedTime"]);
                            mhrDataInfo.InsuranceCreated = Convert.ToString(rdr["InsuranceCreated"]);
                            mhrDataInfo.InsuranceModified = Convert.ToString(rdr["InsuranceModified"]);
                            mhrDataInfo.InsuranceModifiedTime = Convert.ToString(rdr["InsuranceModifiedTime"]);
                            mhrDataInfo.ObservationCreated = Convert.ToString(rdr["ObservationCreated"]);
                            mhrDataInfo.ObservationModified = Convert.ToString(rdr["ObservationModified"]);
                            mhrDataInfo.ObservationModifiedTime = Convert.ToString(rdr["ObservationModifiedTime"]);
                        }

                    }
                }
                return mhrDataInfo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        #region patient Details Services
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

        #endregion

        #region Emergency Contact Service
        public int SavePatientEmergency(PatientEmergencyModel emergencyModel)
        {
            try
            {
                var emergencyContact = new PatientEmergencyNew()
                {
                    //primary Option
                    FirstName = emergencyModel.FirstName,
                    LastName = emergencyModel.LastName,
                    Relationship = emergencyModel.Relationship,
                    Address = emergencyModel.Address,
                    City = emergencyModel.City,
                    StateId = emergencyModel.StateId,
                    CountryId = emergencyModel.CountryId,
                    Zipcode = emergencyModel.Zipcode,
                    EmailId = emergencyModel.EmailId,
                    ContactNo = emergencyModel.ContactNo,
                    //Secondary Option
                    SecondaryFirstName = emergencyModel.SecondaryFirstName,
                    SecondaryLastName = emergencyModel.SecondaryLastName,
                    SecondaryRelationship = emergencyModel.SecondaryRelationship,
                    SecondaryAddress = emergencyModel.SecondaryAddress,
                    SecondaryCity = emergencyModel.SecondaryCity,
                    SecondaryStateId = emergencyModel.SecondaryStateId,
                    SecondaryCountryId = emergencyModel.SecondaryCountryId,
                    SecondaryZipcode = emergencyModel.SecondaryZipcode,
                    SecondaryEmailId = emergencyModel.SecondaryEmailId,
                    SecondaryContactNo = emergencyModel.SecondaryContactNo,
                    UserId = emergencyModel.UserId,
                    CreatedBy = emergencyModel.UserId,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = emergencyModel.UserId,
                    ModifiedOn = DateTime.Now
                };
                _emergencyRepository.Add(emergencyContact);
                return emergencyContact.PatientEmergencyId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PatientEmergencyModel LoadPatientEmergencyByUserId(int userId)
        {
            var ptEmergencyData = new PatientEmergencyModel();
            try
            {
                var emergencyContactData = _emergencyRepository.GetSingle(x => x.UserId == userId);
                if (emergencyContactData != null)
                {
                    ptEmergencyData.PatientEmergencyId = emergencyContactData.PatientEmergencyId;
                    ptEmergencyData.UserId = emergencyContactData.UserId;
                    ptEmergencyData.Address = emergencyContactData.Address;
                    ptEmergencyData.City = emergencyContactData.City;
                    ptEmergencyData.ContactNo = emergencyContactData.ContactNo;
                    ptEmergencyData.EmailId = emergencyContactData.EmailId;
                    ptEmergencyData.FirstName = emergencyContactData.FirstName;
                    ptEmergencyData.LastName = emergencyContactData.LastName;
                    ptEmergencyData.StateId = emergencyContactData.StateId;
                    ptEmergencyData.Zipcode = emergencyContactData.Zipcode;
                    ptEmergencyData.Relationship = emergencyContactData.Relationship;
                    ptEmergencyData.CountryId = emergencyContactData.CountryId;
                    ptEmergencyData.SecondaryCity = emergencyContactData.SecondaryCity;
                    ptEmergencyData.SecondaryContactNo = emergencyContactData.SecondaryContactNo;
                    ptEmergencyData.SecondaryEmailId = emergencyContactData.SecondaryEmailId;
                    ptEmergencyData.SecondaryFirstName = emergencyContactData.SecondaryFirstName;
                    ptEmergencyData.SecondaryLastName = emergencyContactData.SecondaryLastName;
                    ptEmergencyData.SecondaryStateId = emergencyContactData.SecondaryStateId;
                    ptEmergencyData.SecondaryZipcode = emergencyContactData.SecondaryZipcode;
                    ptEmergencyData.SecondaryRelationship = emergencyContactData.SecondaryRelationship;
                    ptEmergencyData.SecondaryCountryId = emergencyContactData.SecondaryCountryId;
                    ptEmergencyData.SecondaryAddress = emergencyContactData.SecondaryAddress;

                }
                return ptEmergencyData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdatePatientEmergency(PatientEmergencyModel emergencyModel)
        {
            try
            {
                var currentEmergencyContact = _emergencyRepository.GetSingle(x => x.PatientEmergencyId == emergencyModel.PatientEmergencyId);
                if (currentEmergencyContact != null)
                {
                    //primary Option
                    currentEmergencyContact.FirstName = emergencyModel.FirstName;
                    currentEmergencyContact.LastName = emergencyModel.LastName;
                    currentEmergencyContact.Relationship = emergencyModel.Relationship;
                    currentEmergencyContact.Address = emergencyModel.Address;
                    currentEmergencyContact.City = emergencyModel.City;
                    currentEmergencyContact.StateId = emergencyModel.StateId;
                    currentEmergencyContact.CountryId = emergencyModel.CountryId;
                    currentEmergencyContact.Zipcode = emergencyModel.Zipcode;
                    currentEmergencyContact.EmailId = emergencyModel.EmailId;
                    currentEmergencyContact.ContactNo = emergencyModel.ContactNo;
                    //Secondary Option
                    currentEmergencyContact.SecondaryFirstName = emergencyModel.SecondaryFirstName;
                    currentEmergencyContact.SecondaryLastName = emergencyModel.SecondaryLastName;
                    currentEmergencyContact.SecondaryRelationship = emergencyModel.SecondaryRelationship;
                    currentEmergencyContact.SecondaryAddress = emergencyModel.SecondaryAddress;
                    currentEmergencyContact.SecondaryCity = emergencyModel.SecondaryCity;
                    currentEmergencyContact.SecondaryStateId = emergencyModel.SecondaryStateId;
                    currentEmergencyContact.SecondaryCountryId = emergencyModel.SecondaryCountryId;
                    currentEmergencyContact.SecondaryZipcode = emergencyModel.SecondaryZipcode;
                    currentEmergencyContact.SecondaryEmailId = emergencyModel.SecondaryEmailId;
                    currentEmergencyContact.SecondaryContactNo = emergencyModel.SecondaryContactNo;
                    currentEmergencyContact.UserId = emergencyModel.UserId;
                    currentEmergencyContact.ModifiedBy = emergencyModel.UserId;
                    currentEmergencyContact.ModifiedOn = DateTime.Now;
                    _emergencyRepository.Update(currentEmergencyContact);
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

        #region Insurance Services
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
                    EligibilityStartDate = string.IsNullOrEmpty(insurancelData.EligibilityStartDate) ? (DateTime?)null : Convert.ToDateTime(insurancelData.EligibilityStartDate),
                    EligibilityEndDate = string.IsNullOrEmpty(insurancelData.EligibilityEndDate) ? (DateTime?)null : Convert.ToDateTime(insurancelData.EligibilityEndDate),
                    CreatedBy = insurancelData.UserId,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = insurancelData.UserId,
                    ModifiedOn = DateTime.Now
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
                    ptData.EligibilityStartDate = insurancelData.EligibilityStartDate != null ? Convert.ToDateTime(insurancelData.EligibilityStartDate).Date.ToString("yyyy-MM-dd") : null;
                    ptData.EligibilityEndDate = insurancelData.EligibilityEndDate != null ? Convert.ToDateTime(insurancelData.EligibilityEndDate).Date.ToString("yyyy-MM-dd") : null;
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
                    patientInsurance.EligibilityStartDate = string.IsNullOrEmpty(insurancelData.EligibilityStartDate) ? (DateTime?)null : Convert.ToDateTime(insurancelData.EligibilityStartDate);
                    patientInsurance.EligibilityEndDate = string.IsNullOrEmpty(insurancelData.EligibilityEndDate) ? (DateTime?)null : Convert.ToDateTime(insurancelData.EligibilityEndDate);
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

        #region patient Observation Service
        public int SaveObservationDetails(PatientObservationModel patientObservationModel)
        {
            try
            {
                var observationData = new PatientObservationNew()
                {
                    ObservationNote = patientObservationModel.ObservationNote,
                    ObservationDate = Convert.ToDateTime(patientObservationModel.ObservationDate),
                    ObservationTime = TimeSpan.Parse(patientObservationModel.ObservationTime),
                    UserId = patientObservationModel.UserId,
                    CreatedBy = patientObservationModel.UserId,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = patientObservationModel.UserId,
                    ModifiedOn = DateTime.Now
                };
                _patientObservationRepository.Add(observationData);
                return observationData.PatientObservationId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PatientObservationModel LoadObservationByUserId(int userId)
        {
            var ptObservationData = new PatientObservationModel();
            try
            {
                var fdfd = _patientObservationRepository.GetAll(x => x.UserId == userId).ToList();
                ptObservationData.ObservationData = _patientObservationRepository.GetAll(x => x.UserId == userId).Select(x =>
                new PatientObservationModel()
                {
                    ObservationNote = x.ObservationNote,
                    ObservationDate = x.ObservationDate.Date.ToString("yyyy-MM-dd"),
                    ObservationTime = x.ObservationTime.ToString()
                }).ToList();
                return ptObservationData;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public bool UpdateObservationDetails(PatientObservationModel patientObservationModel)
        {
            try
            {
                var currentPatientObservationData = _patientObservationRepository.GetSingle(x => x.PatientObservationId == patientObservationModel.PatientObservationId);
                if (currentPatientObservationData != null)
                {
                    currentPatientObservationData.ObservationNote = patientObservationModel.ObservationNote;
                    currentPatientObservationData.ObservationDate = Convert.ToDateTime(patientObservationModel.ObservationDate);
                    currentPatientObservationData.ObservationTime = TimeSpan.Parse(patientObservationModel.ObservationTime);
                    currentPatientObservationData.ModifiedBy = patientObservationModel.UserId;
                    currentPatientObservationData.ModifiedOn = DateTime.Now;
                    _patientObservationRepository.Update(currentPatientObservationData);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PatientObservationModel LoadObservationDataByObservationId(int observationId, int userId)
        {
            try
            {
                var AllpatientObservationData = LoadObservationByUserId(userId);
                var patientObservationData = _patientObservationRepository.GetSingle(x => x.PatientObservationId == observationId);
                if (patientObservationData != null)
                {
                    patientObservationData.ObservationNote = patientObservationData.ObservationNote;
                    patientObservationData.ObservationDate = patientObservationData.ObservationDate;
                    patientObservationData.ObservationTime = patientObservationData.ObservationTime;
                }
                return AllpatientObservationData;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion
    }
}
