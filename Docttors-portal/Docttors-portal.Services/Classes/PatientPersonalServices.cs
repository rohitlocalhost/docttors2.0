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
using System.Security.Cryptography;
using Docttors_portal.Common;
using System.Security.Policy;
using System.Data.Linq;
using static Docttors_portal.Common.Models.MyProvider;

namespace Docttors_portal.Services.Classes
{
    public class PatientPersonalServices : IPatientPersonalServices
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<PatientPersonalNew> _patientRepository;
        private IRepository<PatientPhysicianDetailsNew> _PhysicianRepository;
        private IRepository<PatientInsuranceNew> _insuranceRepository;
        private IRepository<PatientAllergiesNew> _allergiesRepository;
        private IRepository<PatientHospitalDetailsNew> _hospitalrepository;
        private IRepository<PatientPharmacyDetailsNew> _pharmacyRepository;
        private IRepository<PatientMedicationDetailsNew> _medicationRepository;
        private IRepository<PatientEmergencyNew> _emergencyRepository;
        private IRepository<PatientObservationNew> _patientObservationRepository;
        private IRepository<PatientVitalsNew> _patientVitalRepository;
        private IRepository<PatientFavoriteDoctor> _patientFavouriteDoctor;
        private IRepository<ChronicConditionsMaster> _chronicConditionsMaster;
        private IRepository<PatientChronicConditions> _patientChronicCondition;
        private IRepository<PatientSocialHisotry> _patientSocialHisotry;
        private IRepository<PatientClinicalHistory> _patientClinicalHistory;
        private IRepository<PatientSurgicalHistory> _patientSurgicalHistory;
        private IRepository<PatientReviewOfSystem> _patientReviewOfSystem;
        private IRepository<PatientCondition> _patientCondition;
        private IRepository<OnlineVisitStep1> _onlineVisitStep1;
        private IRepository<OnlineVisitStep2> _onlineVisitStep2;
        private IRepository<OnlineVisitStep5> _onlineVisitStep5;
        private string connectionString = ConfigurationManager.ConnectionStrings["DocttorsEntities"].ConnectionString;
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
                _allergiesRepository = _unitOfWork.GetRepository<PatientAllergiesNew>();
                _hospitalrepository = _unitOfWork.GetRepository<PatientHospitalDetailsNew>();
                _pharmacyRepository = _unitOfWork.GetRepository<PatientPharmacyDetailsNew>();
                _medicationRepository = _unitOfWork.GetRepository<PatientMedicationDetailsNew>();
                _patientVitalRepository = _unitOfWork.GetRepository<PatientVitalsNew>();
                _patientFavouriteDoctor = _unitOfWork.GetRepository<PatientFavoriteDoctor>();
                _chronicConditionsMaster = _unitOfWork.GetRepository<ChronicConditionsMaster>();
                _patientChronicCondition = _unitOfWork.GetRepository<PatientChronicConditions>();
                _patientSocialHisotry = _unitOfWork.GetRepository<PatientSocialHisotry>();
                _patientClinicalHistory = _unitOfWork.GetRepository<PatientClinicalHistory>();
                _patientSurgicalHistory = _unitOfWork.GetRepository<PatientSurgicalHistory>();
                _patientReviewOfSystem = _unitOfWork.GetRepository<PatientReviewOfSystem>();
                _patientCondition = _unitOfWork.GetRepository<PatientCondition>();
                _onlineVisitStep1 = _unitOfWork.GetRepository<OnlineVisitStep1>();
                _onlineVisitStep2 = _unitOfWork.GetRepository<OnlineVisitStep2>();
                _onlineVisitStep5 = _unitOfWork.GetRepository<OnlineVisitStep5>();
            }
        }

        #region patient Load Services
        public List<GetpatientMessage> GetpatientMessages(int patientId)
        {
            try
            {
                var patientMessages = new List<GetpatientMessage>();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetpatientMessage", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@patientId", SqlDbType.Int).Value = patientId;
                        con.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            var currentPatientMessage = new GetpatientMessage()
                            {
                                Step1Id = Convert.ToInt32(rdr["Step1Id"]),
                                Step2Id = Convert.ToInt32(rdr["Step2Id"]),
                                Status = Convert.ToString(rdr["Status"]),
                                ProviderName = Convert.ToString(rdr["providerName"]),
                                ServiceType = Convert.ToString(rdr["ServiceType"]),
                                Symptoms = Convert.ToString(rdr["Symptoms"]),
                                DateTimeRequested = Convert.ToString(rdr["DateTimeRequested"]),
                                AppointmentDate = Convert.ToString(rdr["AppointmentDate"]),
                                DateTreated = Convert.ToString(rdr["DateTreated"]),
                                Amount = Convert.ToString(rdr["Amount"])
                            };
                            patientMessages.Add(currentPatientMessage);
                        }

                    }
                    con.Close();
                }
                return patientMessages;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MHR Page Load Service
        public GetMHRDataInfo GetMHRData(int UserId)
        {
            try
            {
                var mhrDataInfo = new GetMHRDataInfo();
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
                            mhrDataInfo.AllergyCreated = Convert.ToString(rdr["AllergyCreated"]);
                            mhrDataInfo.AllergyModified = Convert.ToString(rdr["AllergyModified"]);
                            mhrDataInfo.AllergyModifiedTime = Convert.ToString(rdr["AllergyModifiedTime"]);
                            mhrDataInfo.HospitalCreated = Convert.ToString(rdr["HospitalCreated"]);
                            mhrDataInfo.HospitalModified = Convert.ToString(rdr["HospitalModified"]);
                            mhrDataInfo.HospitalModifiedTime = Convert.ToString(rdr["HospitalModifiedTime"]);
                            mhrDataInfo.MedicationCreated = Convert.ToString(rdr["MedicationCreated"]);
                            mhrDataInfo.MedicationModified = Convert.ToString(rdr["MedicationModified"]);
                            mhrDataInfo.MedicationModifiedTime = Convert.ToString(rdr["MedicationModifiedTime"]);
                            mhrDataInfo.PharmacyCreated = Convert.ToString(rdr["PharmacyCreated"]);
                            mhrDataInfo.PharmacyModified = Convert.ToString(rdr["PharmacyModified"]);
                            mhrDataInfo.PharmacyModifiedTime = Convert.ToString(rdr["PharmacyModifiedTime"]);
                            mhrDataInfo.VitalCreated = Convert.ToString(rdr["VitalCreated"]);
                            mhrDataInfo.VitalModified = Convert.ToString(rdr["VitalModified"]);
                            mhrDataInfo.VitalModifiedTime = Convert.ToString(rdr["VitalModifiedTime"]);
                            mhrDataInfo.ClinicalHistoryCreated = Convert.ToString(rdr["ClinicalHistoryCreated"]);
                            mhrDataInfo.ClinicalHistoryModified = Convert.ToString(rdr["ClinicalHistoryModified"]);
                            mhrDataInfo.ClinicalHistoryModifiedTime = Convert.ToString(rdr["ClinicalHistoryModifiedTime"]);
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
                throw ex;
            }
        }
        #endregion

        #region Allergies
        public PatientAllergiesModel LoadAllergiesDetailsByUserId(int userId)
        {
            var ptData = new PatientAllergiesModel();
            try
            {
                ptData.AllergyList = _allergiesRepository.GetAll(x => x.UserId == userId).Select(x => new PatientAllergiesModel()
                {
                    PatientAllergyId = x.PatientAllergyId,
                    UserId = x.UserId,
                    AllergyName = x.AllergyName,
                    Reaction = x.Reaction
                }).ToList();
                return ptData;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int SaveAllergiesDetails(PatientAllergiesModel allergiesModel)
        {
            try
            {
                var allergies = new PatientAllergiesNew()
                {
                    PatientAllergyId = allergiesModel.PatientAllergyId,
                    UserId = allergiesModel.UserId,
                    AllergyName = allergiesModel.AllergyName,
                    Reaction = allergiesModel.Reaction,
                    CreatedBy = allergiesModel.UserId,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = allergiesModel.UserId,
                    ModifiedOn = DateTime.Now
                };
                _allergiesRepository.Add(allergies);
                return allergies.PatientAllergyId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool UpdateAllergiesDetails(PatientAllergiesModel allergiesModel)
        {
            try
            {
                var patientAllergies = _allergiesRepository.GetSingle(x => x.PatientAllergyId == allergiesModel.PatientAllergyId);
                if (patientAllergies != null)
                {
                    patientAllergies.PatientAllergyId = allergiesModel.PatientAllergyId;
                    patientAllergies.UserId = allergiesModel.UserId;
                    patientAllergies.AllergyName = allergiesModel.AllergyName;
                    patientAllergies.Reaction = allergiesModel.Reaction;
                    patientAllergies.ModifiedBy = allergiesModel.UserId;
                    patientAllergies.ModifiedOn = DateTime.Now;
                    _allergiesRepository.Update(patientAllergies);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool DeleteAllergyDetails(int patientAllergyId)
        {
            try
            {
                var allergyDetails = _allergiesRepository.GetSingle(x => x.PatientAllergyId == patientAllergyId);
                if (allergyDetails != null)
                {
                    _allergiesRepository.Delete(allergyDetails);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public PatientAllergiesModel LoadAllergyDetailsByAllergyId(int patientAllergyId, int userId)
        {
            try
            {
                var AllAllergyData = LoadAllergiesDetailsByUserId(userId);
                var patientAllergyData = _allergiesRepository.GetSingle(x => x.PatientAllergyId == patientAllergyId);
                if (patientAllergyData != null)
                {
                    AllAllergyData.PatientAllergyId = patientAllergyData.PatientAllergyId;
                    AllAllergyData.UserId = patientAllergyData.UserId;
                    AllAllergyData.AllergyName = patientAllergyData.AllergyName;
                    AllAllergyData.Reaction = patientAllergyData.Reaction;
                }
                return AllAllergyData;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region Hospital Details
        public PatientHospitalModel LoadHospitalDetailsByUserId(int userId)
        {
            var ptData = new PatientHospitalModel();
            try
            {
                ptData.HospitalList = _hospitalrepository.GetAll(x => x.UserId == userId).Select(x => new PatientHospitalModel()
                {
                    PatientHospitalId = x.PatientHospitalId,
                    UserId = x.UserId,
                    HospitalName = x.HospitalName,
                    Phone = x.Phone,
                    Address1 = x.Address1,
                    Address2 = x.Address2,
                    City = x.City,
                    StateId = x.StateId,
                    ZipCode = x.ZipCode,
                    ReasonForVisit = x.ReasonForVisit
                }).ToList();
                return ptData;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool UpdateHospitalDetails(PatientHospitalModel hospitalModel)
        {
            try
            {
                var patientAllergies = _hospitalrepository.GetSingle(x => x.PatientHospitalId == hospitalModel.PatientHospitalId);
                if (patientAllergies != null)
                {
                    patientAllergies.PatientHospitalId = hospitalModel.PatientHospitalId;
                    patientAllergies.UserId = hospitalModel.UserId;
                    patientAllergies.HospitalName = hospitalModel.HospitalName;
                    patientAllergies.Phone = hospitalModel.Phone;
                    patientAllergies.Address1 = hospitalModel.Address1;
                    patientAllergies.Address2 = hospitalModel.Address2;
                    patientAllergies.City = hospitalModel.City;
                    patientAllergies.StateId = hospitalModel.StateId;
                    patientAllergies.ZipCode = hospitalModel.ZipCode;
                    patientAllergies.ReasonForVisit = hospitalModel.ReasonForVisit;
                    patientAllergies.ModifiedBy = hospitalModel.UserId;
                    patientAllergies.ModifiedOn = DateTime.Now;
                    _hospitalrepository.Update(patientAllergies);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int SaveHospitalDetails(PatientHospitalModel hospitalModel)
        {
            try
            {
                var patientHospital = new PatientHospitalDetailsNew()
                {
                    PatientHospitalId = hospitalModel.PatientHospitalId,
                    UserId = hospitalModel.UserId,
                    HospitalName = hospitalModel.HospitalName,
                    Phone = hospitalModel.Phone,
                    Address1 = hospitalModel.Address1,
                    Address2 = hospitalModel.Address2,
                    City = hospitalModel.City,
                    StateId = hospitalModel.StateId,
                    ZipCode = hospitalModel.ZipCode,
                    ReasonForVisit = hospitalModel.ReasonForVisit,
                    CreatedBy = hospitalModel.UserId,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = hospitalModel.UserId,
                    ModifiedOn = DateTime.Now
                };
                _hospitalrepository.Add(patientHospital);
                return patientHospital.PatientHospitalId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool DeletehospitalDetails(int patientHospitalId)
        {
            try
            {
                var hospitalDetails = _hospitalrepository.GetSingle(x => x.PatientHospitalId == patientHospitalId);
                if (hospitalDetails != null)
                {
                    _hospitalrepository.Delete(hospitalDetails);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PatientHospitalModel LoadHospitalDetailsByHospitalId(int patientHospitalId, int userId)
        {
            try
            {
                var AllHospitalData = LoadHospitalDetailsByUserId(userId);
                var patientHospitalData = _hospitalrepository.GetSingle(x => x.PatientHospitalId == patientHospitalId);
                if (patientHospitalData != null)
                {
                    AllHospitalData.PatientHospitalId = patientHospitalData.PatientHospitalId;
                    AllHospitalData.UserId = patientHospitalData.UserId;
                    AllHospitalData.HospitalName = patientHospitalData.HospitalName;
                    AllHospitalData.Phone = patientHospitalData.Phone;
                    AllHospitalData.Address1 = patientHospitalData.Address1;
                    AllHospitalData.Address2 = patientHospitalData.Address2;
                    AllHospitalData.City = patientHospitalData.City;
                    AllHospitalData.StateId = patientHospitalData.StateId;
                    AllHospitalData.ZipCode = patientHospitalData.ZipCode;
                    AllHospitalData.ReasonForVisit = patientHospitalData.ReasonForVisit;
                }
                return AllHospitalData;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion

        #region Pharmacy
        public PatientPharmacyModel LoadPharmacyDetailsByUserId(int userId)
        {
            var ptData = new PatientPharmacyModel();
            try
            {
                ptData.AllPharmacyData = _pharmacyRepository.GetAll(x => x.UserId == userId).Select(x => new PatientPharmacyModel()
                {
                    PatientPharmacyId = x.PatientPharmacyId,
                    UserId = x.UserId,
                    IsPrimaryPharmacy = x.isPrimaryPharmacy,
                    Name = x.PharmacyName,
                    Phone = x.PhramacyPhone,
                    Address1 = x.Address1,
                    Address2 = x.Address2,
                    City = x.City,
                    StateId = x.StateId,
                    ZipCode = x.ZipCode
                }).ToList();
                return ptData;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public bool UpdatePharmacyDetails(PatientPharmacyModel pharmacyModel)
        {
            try
            {
                var patientPharmacy = _pharmacyRepository.GetSingle(x => x.PatientPharmacyId == pharmacyModel.PatientPharmacyId);
                if (patientPharmacy != null)
                {
                    patientPharmacy.PatientPharmacyId = pharmacyModel.PatientPharmacyId;
                    patientPharmacy.UserId = pharmacyModel.UserId;
                    patientPharmacy.PharmacyName = pharmacyModel.Name;
                    patientPharmacy.isPrimaryPharmacy = pharmacyModel.IsPrimaryPharmacy;
                    patientPharmacy.PhramacyPhone = pharmacyModel.Phone;
                    patientPharmacy.Address1 = pharmacyModel.Address1;
                    patientPharmacy.Address2 = pharmacyModel.Address2;
                    patientPharmacy.City = pharmacyModel.City;
                    patientPharmacy.ZipCode = pharmacyModel.ZipCode;
                    patientPharmacy.StateId = pharmacyModel.StateId;
                    patientPharmacy.ModifiedBy = pharmacyModel.UserId;
                    patientPharmacy.ModifiedOn = DateTime.Now;
                    _pharmacyRepository.Update(patientPharmacy);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SavePharmacyDetails(PatientPharmacyModel pharmacyModel)
        {
            try
            {
                var patientPharmacy = new PatientPharmacyDetailsNew()
                {
                    PatientPharmacyId = pharmacyModel.PatientPharmacyId,
                    UserId = pharmacyModel.UserId,
                    PharmacyName = pharmacyModel.Name,
                    PhramacyPhone = pharmacyModel.Phone,
                    Address1 = pharmacyModel.Address1,
                    Address2 = pharmacyModel.Address2,
                    isPrimaryPharmacy = pharmacyModel.IsPrimaryPharmacy,
                    City = pharmacyModel.City,
                    StateId = pharmacyModel.StateId,
                    ZipCode = pharmacyModel.ZipCode,
                    CreatedBy = pharmacyModel.UserId,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = pharmacyModel.UserId,
                    ModifiedOn = DateTime.Now
                };
                _pharmacyRepository.Add(patientPharmacy);
                return patientPharmacy.PatientPharmacyId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool DeletePharmacyDetails(int patientPharmacyId)
        {
            try
            {
                var pharmacyDetails = _pharmacyRepository.GetSingle(x => x.PatientPharmacyId == patientPharmacyId);
                if (pharmacyDetails != null)
                {
                    _pharmacyRepository.Delete(pharmacyDetails);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PatientPharmacyModel LoadPharmacyDetailsByPharmacyId(int patientPharmacyId, int userId)
        {
            try
            {
                var AllpatientPharmacyData = LoadPharmacyDetailsByUserId(userId);
                var patientPharmacynData = _pharmacyRepository.GetSingle(x => x.PatientPharmacyId == patientPharmacyId);
                if (patientPharmacynData != null)
                {
                    AllpatientPharmacyData.PatientPharmacyId = patientPharmacynData.PatientPharmacyId;
                    AllpatientPharmacyData.UserId = patientPharmacynData.UserId;
                    AllpatientPharmacyData.IsPrimaryPharmacy = patientPharmacynData.isPrimaryPharmacy;
                    AllpatientPharmacyData.Name = patientPharmacynData.PharmacyName;
                    AllpatientPharmacyData.Phone = patientPharmacynData.PhramacyPhone;
                    AllpatientPharmacyData.Address1 = patientPharmacynData.Address1;
                    AllpatientPharmacyData.Address2 = patientPharmacynData.Address2;
                    AllpatientPharmacyData.StateId = patientPharmacynData.StateId;
                    AllpatientPharmacyData.ZipCode = patientPharmacynData.ZipCode;
                }
                return AllpatientPharmacyData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Medication Service
        public PatientMedicationModel LoadMedicationDetailsByUserId(int userId)
        {
            var ptData = new PatientMedicationModel();
            try
            {
                ptData.MedicationList = _medicationRepository.GetAll(x => x.UserId == userId).Select(x => new PatientMedicationModel()
                {
                    PatientMedicationId = x.PatientMedicationId,
                    UserId = x.UserId,
                    CurrentMedication = x.CurrentMedication,
                    MedicationName = x.MedicationName,
                    Dosage = x.Dosage,
                    Frequency = x.Frequency,
                    PrescibedPhysician = x.PrescibedPhysician,
                    Prescription = x.Prescription,
                    ReasonforPrescription = x.ReasonforPrescription,
                    DateOfPrescription = x.DateOfPrescription != null ? Convert.ToDateTime(x.DateOfPrescription).ToString("yyyy-MM-dd") : string.Empty
                }).ToList();
                return ptData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateMedicationDetails(PatientMedicationModel medicationModel)
        {
            try
            {
                var medicationDetails = _medicationRepository.GetSingle(x => x.PatientMedicationId == medicationModel.PatientMedicationId);
                if (medicationDetails != null)
                {
                    medicationDetails.PatientMedicationId = medicationModel.PatientMedicationId;
                    medicationDetails.UserId = medicationModel.UserId;
                    medicationDetails.MedicationName = medicationModel.MedicationName;
                    medicationDetails.CurrentMedication = medicationModel.CurrentMedication;
                    medicationDetails.Dosage = medicationModel.Dosage;
                    medicationDetails.Frequency = medicationModel.Frequency;
                    medicationDetails.PrescibedPhysician = medicationModel.PrescibedPhysician;
                    medicationDetails.Prescription = medicationModel.Prescription;
                    medicationDetails.ReasonforPrescription = medicationModel.ReasonforPrescription;
                    medicationDetails.DateOfPrescription = String.IsNullOrEmpty(medicationModel.DateOfPrescription) ? (DateTime?)null : Convert.ToDateTime(medicationModel.DateOfPrescription);
                    medicationDetails.ModifiedBy = medicationModel.UserId;
                    medicationDetails.ModifiedOn = DateTime.Now;
                    _medicationRepository.Update(medicationDetails);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveMedicationDetails(PatientMedicationModel medicationModel)
        {
            try
            {
                var medicationDetails = new PatientMedicationDetailsNew()
                {
                    PatientMedicationId = medicationModel.PatientMedicationId,
                    UserId = medicationModel.UserId,
                    MedicationName = medicationModel.MedicationName,
                    CurrentMedication = medicationModel.CurrentMedication,
                    Dosage = medicationModel.Dosage,
                    Frequency = medicationModel.Frequency,
                    PrescibedPhysician = medicationModel.PrescibedPhysician,
                    Prescription = medicationModel.Prescription,
                    ReasonforPrescription = medicationModel.ReasonforPrescription,
                    DateOfPrescription = String.IsNullOrEmpty(medicationModel.DateOfPrescription) ? (DateTime?)null : Convert.ToDateTime(medicationModel.DateOfPrescription),
                    CreatedBy = medicationModel.UserId,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = medicationModel.UserId,
                    ModifiedOn = DateTime.Now
                };
                _medicationRepository.Add(medicationDetails);
                return medicationDetails.PatientMedicationId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool DeleteMedicationDetails(int patientMedicationId)
        {
            try
            {
                var medicationDetails = _medicationRepository.GetSingle(x => x.PatientMedicationId == patientMedicationId);
                if (medicationDetails != null)
                {
                    _medicationRepository.Delete(medicationDetails);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PatientMedicationModel LoadMedicationDetailsByMedicalId(int patientMedicationId, int userId)
        {
            try
            {
                var AllpatientMedicationData = LoadMedicationDetailsByUserId(userId);
                var patientMedicationData = _medicationRepository.GetSingle(x => x.PatientMedicationId == patientMedicationId);
                if (patientMedicationData != null)
                {
                    AllpatientMedicationData.PatientMedicationId = patientMedicationData.PatientMedicationId;
                    AllpatientMedicationData.UserId = patientMedicationData.UserId;
                    AllpatientMedicationData.CurrentMedication = patientMedicationData.CurrentMedication;
                    AllpatientMedicationData.MedicationName = patientMedicationData.MedicationName;
                    AllpatientMedicationData.Dosage = patientMedicationData.Dosage;
                    AllpatientMedicationData.Frequency = patientMedicationData.Frequency;
                    AllpatientMedicationData.PrescibedPhysician = patientMedicationData.PrescibedPhysician;
                    AllpatientMedicationData.ReasonforPrescription = patientMedicationData.ReasonforPrescription;
                    AllpatientMedicationData.DateOfPrescription = patientMedicationData.DateOfPrescription != null ? Convert.ToDateTime(patientMedicationData.DateOfPrescription).ToString("yyyy-MM-dd") : string.Empty;
                }
                return AllpatientMedicationData;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        #endregion

        #region patient Vital Service
        public int SaveVitalDetails(PatientVitalModel patientVitalModel)
        {
            try
            {
                var vitalData = new PatientVitalsNew()
                {
                    Bloodglucose = patientVitalModel.Bloodglucose,
                    Dia = patientVitalModel.Dia,
                    ObservationDateTime = DateTime.Now,
                    PulseRate = patientVitalModel.PulseRate,
                    Spirometer = patientVitalModel.Spirometer,
                    Spo2 = patientVitalModel.Spo2,
                    Sys = patientVitalModel.Sys,
                    Temprature = patientVitalModel.Temprature,
                    TempratureUnit = patientVitalModel.TempratureUnit == true ? Utilities.GetEnumDescription(TempratureEnum.Fahrenheit) : Utilities.GetEnumDescription(TempratureEnum.Celsius),
                    Weight = patientVitalModel.Weight,
                    WeightUnit = patientVitalModel.WeightUnit == true ? Utilities.GetEnumDescription(WeightEnum.lbs) : Utilities.GetEnumDescription(WeightEnum.kg),
                    UserId = patientVitalModel.UserId,
                    CreatedBy = patientVitalModel.UserId,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = patientVitalModel.UserId,
                    ModifiedOn = DateTime.Now
                };
                _patientVitalRepository.Add(vitalData);
                return vitalData.PatientVitalId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PatientVitalModel LoadVitalByUserId(int userId)
        {
            var ptVitalData = new PatientVitalModel();
            try
            {
                ptVitalData.VitalHistory = GetAllPatientVitalList(userId);
                return ptVitalData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool UpdateVitalDetails(PatientVitalModel patientVitalModel)
        {

            try
            {
                var currentVitalData = _patientVitalRepository.GetSingle(x => x.PatientVitalId == patientVitalModel.PatientVitalId);
                if (currentVitalData != null)
                {
                    currentVitalData.Bloodglucose = patientVitalModel.Bloodglucose;
                    currentVitalData.Dia = patientVitalModel.Dia;
                    //currentVitalData.ObservationDateTime = patientVitalModel.ObservationDateTime;
                    currentVitalData.PulseRate = patientVitalModel.PulseRate;
                    currentVitalData.Spirometer = patientVitalModel.Spirometer;
                    currentVitalData.Spo2 = patientVitalModel.Spo2;
                    currentVitalData.Sys = patientVitalModel.Sys;
                    currentVitalData.Temprature = patientVitalModel.Temprature;
                    currentVitalData.TempratureUnit = patientVitalModel.TempratureUnit == true ? Utilities.GetEnumDescription(TempratureEnum.Fahrenheit) : Utilities.GetEnumDescription(TempratureEnum.Celsius);
                    currentVitalData.Weight = patientVitalModel.Weight;
                    currentVitalData.WeightUnit = patientVitalModel.WeightUnit == true ? Utilities.GetEnumDescription(WeightEnum.lbs) : Utilities.GetEnumDescription(WeightEnum.kg);
                    currentVitalData.ModifiedBy = patientVitalModel.UserId;
                    currentVitalData.ModifiedOn = DateTime.Now;
                    _patientVitalRepository.Update(currentVitalData);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PatientVitalModel LoadVitalDataByVitalId(int vitalId, int userId)
        {
            var ptVitalData = new PatientVitalModel();
            try
            {
                var patientVitalData = _patientVitalRepository.GetSingle(x => x.PatientVitalId == vitalId);
                if (patientVitalData != null)
                {
                    ptVitalData.Bloodglucose = patientVitalData.Bloodglucose;
                    ptVitalData.Dia = patientVitalData.Dia;
                    ptVitalData.PulseRate = patientVitalData.PulseRate;
                    ptVitalData.Spirometer = patientVitalData.Spirometer;
                    ptVitalData.Spo2 = patientVitalData.Spo2;
                    ptVitalData.Sys = patientVitalData.Sys;
                    ptVitalData.Temprature = patientVitalData.Temprature;
                    ptVitalData.TempratureUnit = patientVitalData.TempratureUnit == Utilities.GetEnumDescription(TempratureEnum.Fahrenheit) ? true : false;
                    ptVitalData.Weight = patientVitalData.Weight;
                    ptVitalData.WeightUnit = patientVitalData.WeightUnit == Utilities.GetEnumDescription(WeightEnum.lbs) ? true : false;
                }
                ptVitalData.VitalHistory = GetAllPatientVitalList(userId);
                return ptVitalData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<NameIdModel> GetAllPatientVitalList(int userId)
        {
            var AllVitalList = _patientVitalRepository.GetAll(x => x.UserId == userId).Select(x =>
                new NameIdModel()
                {
                    Id = x.PatientVitalId,
                    Name = x.ObservationDateTime.ToString("yyyy-MM-dd hh:mm")
                }).ToList();
            AllVitalList = AllVitalList.Prepend(new NameIdModel() { Id = 0, Name = "Enter New Vitals" }).ToList();
            return AllVitalList;
        }
        #endregion

        #region Search Functionality
        public PatientSearchDoctorModel GetDoctorByPatient(PatientSearchDoctorModel patientSearchModel, SearchType searchType)
        {
            var searchData = new PatientSearchDoctorModel();
            string connectionString = ConfigurationManager.ConnectionStrings["DocttorsEntities"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                switch ((int)searchType)
                {
                    case 1:
                        searchData.DoctorsList = SearchDoctor(patientSearchModel, searchData.DoctorsList, con);
                        searchData.SearchCount = searchData.DoctorsList.Count;
                        break;
                    case 2:
                        searchData.HospitalData = SearchHospital(patientSearchModel, searchData.HospitalData, con);
                        searchData.SearchCount = searchData.HospitalData.Count;
                        break;
                    case 3:
                        searchData.InsuranceList = SearchInsurance(patientSearchModel, searchData.InsuranceList, con);
                        searchData.SearchCount = searchData.InsuranceList.Count;
                        break;
                }

            }
            return searchData;
        }
        private List<GetDoctorsByPatients> SearchDoctor(PatientSearchDoctorModel patientSearchModel, List<GetDoctorsByPatients> doctorData, SqlConnection sqlConnection)
        {
            using (SqlCommand cmd = new SqlCommand("GetDoctorsByPatients", sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@firstName", SqlDbType.VarChar).Value = patientSearchModel.FirstName;
                cmd.Parameters.Add("@lastName", SqlDbType.VarChar).Value = patientSearchModel.LastName;
                cmd.Parameters.Add("@specilityId", SqlDbType.Int).Value = patientSearchModel.PhysicianSpecialtyId;
                cmd.Parameters.Add("@city", SqlDbType.VarChar).Value = patientSearchModel.City;
                cmd.Parameters.Add("@stateId", SqlDbType.Int).Value = patientSearchModel.StateId;
                cmd.Parameters.Add("@zipCode", SqlDbType.VarChar).Value = patientSearchModel.ZipCode;
                sqlConnection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    //Accessing the data using the string key as index
                    GetDoctorsByPatients getDoctorPatients = new GetDoctorsByPatients()
                    {
                        Name = Convert.ToString(rdr["name"]),
                        EmailAddress = Convert.ToString(rdr["EmailAddress"]),
                        UserId = Convert.ToInt32(rdr["userId"]),
                        Phone = Convert.ToString(rdr["phone"]),
                        Address = Convert.ToString(rdr["address"]),
                        SpecialtyName = Convert.ToString(rdr["SpecialtyName"]),
                        City = Convert.ToString(rdr["city"]),
                        ProfilePic = Convert.ToString(rdr["ProfilePic"]),
                        FavoriteDoctorId = Convert.ToInt32(rdr["FavoriteId"]),
                        IsprimaryDoctor = Convert.ToBoolean(rdr["IsprimaryDoctor"])
                    };
                    doctorData.Add(getDoctorPatients);
                }

            }
            sqlConnection.Close();
            return doctorData;
        }

        private List<GetHospitalData> SearchHospital(PatientSearchDoctorModel patientSearchModel, List<GetHospitalData> hospitalData, SqlConnection sqlConnection)
        {
            using (SqlCommand cmd = new SqlCommand("GetHospitalData", sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@city", SqlDbType.VarChar).Value = patientSearchModel.City;
                cmd.Parameters.Add("@Zip", SqlDbType.VarChar).Value = patientSearchModel.ZipCode;
                sqlConnection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    //Accessing the data using the string key as index
                    GetHospitalData getHospitalData = new GetHospitalData()
                    {
                        HospitalId = Convert.ToInt32(rdr["HospitalId"]),
                        HospitalName = Convert.ToString(rdr["HospitalName"]),
                        City = Convert.ToString(rdr["City"]),
                        PhoneNumber = Convert.ToString(rdr["PhoneNumber"]),
                        Address = Convert.ToString(rdr["address"]),
                        ZIP = Convert.ToString(rdr["ZIP"])
                    };
                    hospitalData.Add(getHospitalData);
                }

            }
            return hospitalData;
        }

        private List<GetInsuranceCompanyList> SearchInsurance(PatientSearchDoctorModel patientSearchModel, List<GetInsuranceCompanyList> InsuranceList, SqlConnection sqlConnection)
        {
            using (SqlCommand cmd = new SqlCommand("GetInsuranceCompanyList", sqlConnection))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@CompanyName", SqlDbType.VarChar).Value = patientSearchModel.CompanyName;
                sqlConnection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    //Accessing the data using the string key as index
                    GetInsuranceCompanyList getInsuranceData = new GetInsuranceCompanyList()
                    {
                        InsuaranceId = Convert.ToInt32(rdr["InsuaranceId"]),
                        CompanyName = Convert.ToString(rdr["CompanyName"]),
                        URL = Convert.ToString(rdr["Url"]),
                        PlanType = Convert.ToString(rdr["PlanType"])
                    };
                    InsuranceList.Add(getInsuranceData);
                }

            }
            return InsuranceList;
        }

        public void AddFavouriteDoctor(int doctorId)
        {
            try
            {
                PatientFavoriteDoctor patientFavoriteDoctor = new PatientFavoriteDoctor()
                {
                    PatientId = SessionVariables.LoggedInUser.UserId,
                    DoctorId = doctorId,
                    IsprimaryDoctor = false,
                    CreatedBy = SessionVariables.LoggedInUser.UserId,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = SessionVariables.LoggedInUser.UserId,
                    ModifiedOn = DateTime.Now
                };
                _patientFavouriteDoctor.Add(patientFavoriteDoctor);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RemoveFavouriteDoctor(int favouriteId)
        {
            try
            {
                var currentFavouriteData = _patientFavouriteDoctor.GetSingle(x => x.FavoriteId == favouriteId);
                _patientFavouriteDoctor.Delete(currentFavouriteData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdatePrimaryDoctor(int favouriteId, bool isPrimary)
        {
            try
            {
                var currentFavouriteData = _patientFavouriteDoctor.GetSingle(x => x.FavoriteId == favouriteId);
                currentFavouriteData.IsprimaryDoctor = isPrimary;
                _patientFavouriteDoctor.Update(currentFavouriteData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Patient Clinical History
        public PatientClinicalViewModel GetPatientClinicalData(int patientId)
        {
            var patientClinicalViewModel = new PatientClinicalViewModel();
            try
            {
                patientClinicalViewModel.allChronicConditions = _chronicConditionsMaster.GetAll().Select(x => new ChronicConditionsMasterViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
                patientClinicalViewModel.patientChronicConditionsModel = _patientChronicCondition.GetAll(x => x.PatientId == patientId).Select(x => new PatientChronicConditionsViewModel()
                {
                    Id = x.Id,
                    PatientId = x.PatientId,
                    ChronicConditionId = x.ChronicConditionId,
                    ChronicConditionText = x.ChronicConditionText
                }).ToList();
                foreach (var item in patientClinicalViewModel.patientChronicConditionsModel)
                {
                    var currentChronicCondition = patientClinicalViewModel.allChronicConditions.Where(x => x.Id == item.ChronicConditionId).FirstOrDefault();
                    currentChronicCondition.IsChecked = true;
                    currentChronicCondition.Text = item.ChronicConditionText;
                }
                patientClinicalViewModel.patientSocialHisotryModel = _patientSocialHisotry.GetAll(x => x.PatientId == patientId).Select(x => new PatientSocialHisotryViewModel()
                {
                    Id = x.Id,
                    PatientId = x.PatientId,
                    IsAlcohol = x.IsAlcohol,
                    IsDrugs = x.IsDrugs,
                    IsSmoke = x.IsSmoke,
                    Other = x.Other
                }).FirstOrDefault();
                patientClinicalViewModel.patientClinicalHistoryModel = _patientClinicalHistory.GetAll(x => x.PatientId == patientId).Select(x => new PatientClinicalHistoryViewModel()
                {
                    Id = x.Id,
                    PatientId = x.PatientId,
                    IsAlcoholAbuse = x.IsAlcoholAbuse,
                    IsBloodPressure = x.IsBloodPressure,
                    IsDiabetes = x.IsDiabetes,
                    IsCancer = x.IsCancer,
                    IsDrugAbuse = x.IsDrugAbuse,
                    IsHeartDisease = x.IsHeartDisease,
                }).FirstOrDefault();
                patientClinicalViewModel.patientSurgicalHistoryModel = _patientSurgicalHistory.GetAll(x => x.PatientId == patientId).Select(x => new PatientSurgicalHistoryViewModel()
                {
                    Id = x.Id,
                    PatientId = x.PatientId,
                    IsAppendix = x.IsAppendix,
                    IsCataracts = x.IsCataracts,
                    IsGallbladder = x.IsGallbladder,
                    IsHeartByPass = x.IsHeartByPass,
                    IsHysterectomy = x.IsHysterectomy,
                    IsStent = x.IsStent,
                    IsTonsils = x.IsTonsils
                }).FirstOrDefault();
                patientClinicalViewModel.patientReviewOfSystemModel = _patientReviewOfSystem.GetAll(x => x.PatientId == patientId).Select(x => new PatientReviewOfSystemViewModel()
                {
                    Id = x.Id,
                    PatientId = x.PatientId,
                    Cardiovascular = x.Cardiovascular,
                    MusclesOrJoints = x.MusclesOrJoints,
                    Hematology = x.Hematology,
                    Neurological = x.Neurological,
                    GeneralCondition = x.GeneralCondition,
                    Diabetes = x.Diabetes,
                    Eyes = x.Eyes,
                    Dental = x.Dental,
                    Ear = x.Ear,
                    ObOrGyn = x.ObOrGyn,
                    Psychological = x.Psychological,
                    Respiratory = x.Respiratory,
                    Skin = x.Skin,
                    Stomach = x.Stomach,
                    Urinary = x.Urinary
                }).FirstOrDefault();
                patientClinicalViewModel.PatientConditionInfo.patientConditionData = _patientCondition.GetAll(x => x.PatientId == patientId).Select(x => new PatientConditionViewModel()
                {
                    Id = x.Id,
                    PatientId = x.PatientId,
                    Condition = x.Condition
                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return patientClinicalViewModel;
        }
        public bool SavepatientClinicalData(PatientClinicalViewModel patientClinicalViewModel)
        {
            bool result = false;
            try
            {
                // Insert/Update Chrononical Condition section
                if (patientClinicalViewModel.allChronicConditions.Count > 0)
                {
                    List<PatientChronicConditions> patientChornicalConditionList = new List<PatientChronicConditions>();
                    PatientChronicConditions patientChornicalCondition;
                    //delete all assigned data first
                    var currentChornicaldata = _patientChronicCondition.GetAll(x => x.PatientId == SessionVariables.LoggedInUser.UserId).ToList();
                    if (currentChornicaldata.Count > 0)
                    {
                        _patientChronicCondition.DeleteAll(currentChornicaldata);
                    }

                    foreach (var item in patientClinicalViewModel.allChronicConditions)
                    {
                        if (item.IsChecked)
                        {
                            patientChornicalCondition = new PatientChronicConditions()
                            {
                                ChronicConditionId = item.Id,
                                ChronicConditionText = item.Text,
                                PatientId = patientClinicalViewModel.PatientId,
                                CreatedBy = patientClinicalViewModel.PatientId,
                                CreatedOn = DateTime.Now,
                                ModifiedBy = patientClinicalViewModel.PatientId,
                                ModifiedOn = DateTime.Now
                            };
                            patientChornicalConditionList.Add(patientChornicalCondition);
                        }
                    }
                    _patientChronicCondition.AddAll(patientChornicalConditionList);
                }
                //Insert/Update Social History
                if (patientClinicalViewModel.patientSocialHisotryModel != null)
                {
                    if (patientClinicalViewModel.patientSocialHisotryModel.Id > 0)
                    {
                        var currentSocialHistory = _patientSocialHisotry.GetSingle(x => x.Id == patientClinicalViewModel.patientSocialHisotryModel.Id);
                        currentSocialHistory.IsAlcohol = patientClinicalViewModel.patientSocialHisotryModel.IsAlcohol;
                        currentSocialHistory.IsDrugs = patientClinicalViewModel.patientSocialHisotryModel.IsDrugs;
                        currentSocialHistory.IsSmoke = patientClinicalViewModel.patientSocialHisotryModel.IsSmoke;
                        currentSocialHistory.Other = patientClinicalViewModel.patientSocialHisotryModel.Other;
                        currentSocialHistory.PatientId = patientClinicalViewModel.PatientId;
                        currentSocialHistory.ModifiedBy = patientClinicalViewModel.PatientId;
                        currentSocialHistory.ModifiedOn = DateTime.Now;
                        _patientSocialHisotry.Update(currentSocialHistory);
                    }
                    else
                    {
                        PatientSocialHisotry patientSocialHisotry = new PatientSocialHisotry();
                        patientSocialHisotry.IsAlcohol = patientClinicalViewModel.patientSocialHisotryModel.IsAlcohol;
                        patientSocialHisotry.IsDrugs = patientClinicalViewModel.patientSocialHisotryModel.IsDrugs;
                        patientSocialHisotry.IsSmoke = patientClinicalViewModel.patientSocialHisotryModel.IsSmoke;
                        patientSocialHisotry.Other = patientClinicalViewModel.patientSocialHisotryModel.Other;
                        patientSocialHisotry.PatientId = patientClinicalViewModel.PatientId;
                        patientSocialHisotry.CreatedBy = patientClinicalViewModel.PatientId;
                        patientSocialHisotry.CreatedOn = DateTime.Now;
                        patientSocialHisotry.ModifiedBy = patientClinicalViewModel.PatientId;
                        patientSocialHisotry.ModifiedOn = DateTime.Now;
                        _patientSocialHisotry.Add(patientSocialHisotry);
                    }
                }
                //Insert/Update family history
                if (patientClinicalViewModel.patientClinicalHistoryModel != null)
                {
                    if (patientClinicalViewModel.patientClinicalHistoryModel.Id > 0)
                    {
                        var currentClinicalHistory = _patientClinicalHistory.GetSingle(x => x.Id == patientClinicalViewModel.patientClinicalHistoryModel.Id);
                        currentClinicalHistory.IsAlcoholAbuse = patientClinicalViewModel.patientClinicalHistoryModel.IsAlcoholAbuse;
                        currentClinicalHistory.IsBloodPressure = patientClinicalViewModel.patientClinicalHistoryModel.IsBloodPressure;
                        currentClinicalHistory.IsCancer = patientClinicalViewModel.patientClinicalHistoryModel.IsCancer;
                        currentClinicalHistory.IsDiabetes = patientClinicalViewModel.patientClinicalHistoryModel.IsDiabetes;
                        currentClinicalHistory.IsDrugAbuse = patientClinicalViewModel.patientClinicalHistoryModel.IsDrugAbuse;
                        currentClinicalHistory.IsHeartDisease = patientClinicalViewModel.patientClinicalHistoryModel.IsHeartDisease;
                        currentClinicalHistory.PatientId = patientClinicalViewModel.PatientId;
                        currentClinicalHistory.ModifiedBy = patientClinicalViewModel.PatientId;
                        currentClinicalHistory.ModifiedOn = DateTime.Now;
                        _patientClinicalHistory.Update(currentClinicalHistory);
                    }
                    else
                    {
                        PatientClinicalHistory patientClinicalHistory = new PatientClinicalHistory()
                        {
                            IsAlcoholAbuse = patientClinicalViewModel.patientClinicalHistoryModel.IsAlcoholAbuse,
                            IsBloodPressure = patientClinicalViewModel.patientClinicalHistoryModel.IsBloodPressure,
                            IsCancer = patientClinicalViewModel.patientClinicalHistoryModel.IsCancer,
                            IsDiabetes = patientClinicalViewModel.patientClinicalHistoryModel.IsDiabetes,
                            IsDrugAbuse = patientClinicalViewModel.patientClinicalHistoryModel.IsDrugAbuse,
                            IsHeartDisease = patientClinicalViewModel.patientClinicalHistoryModel.IsHeartDisease,
                            PatientId = SessionVariables.LoggedInUser.UserId,
                            CreatedBy = SessionVariables.LoggedInUser.UserId,
                            CreatedOn = DateTime.Now,
                            ModifiedBy = SessionVariables.LoggedInUser.UserId,
                            ModifiedOn = DateTime.Now
                        };
                        _patientClinicalHistory.Add(patientClinicalHistory);
                    }
                }
                //Insert/update Surgical History
                if (patientClinicalViewModel.patientSurgicalHistoryModel != null)
                {
                    if (patientClinicalViewModel.patientSurgicalHistoryModel.Id > 0)
                    {
                        var currentSurgicalHistory = _patientSurgicalHistory.GetSingle(x => x.Id == patientClinicalViewModel.patientSurgicalHistoryModel.Id);
                        currentSurgicalHistory.IsAppendix = patientClinicalViewModel.patientSurgicalHistoryModel.IsAppendix;
                        currentSurgicalHistory.IsCataracts = patientClinicalViewModel.patientSurgicalHistoryModel.IsCataracts;
                        currentSurgicalHistory.IsGallbladder = patientClinicalViewModel.patientSurgicalHistoryModel.IsGallbladder;
                        currentSurgicalHistory.IsHeartByPass = patientClinicalViewModel.patientSurgicalHistoryModel.IsHeartByPass;
                        currentSurgicalHistory.IsHysterectomy = patientClinicalViewModel.patientSurgicalHistoryModel.IsHysterectomy;
                        currentSurgicalHistory.IsStent = patientClinicalViewModel.patientSurgicalHistoryModel.IsStent;
                        currentSurgicalHistory.IsTonsils = patientClinicalViewModel.patientSurgicalHistoryModel.IsTonsils;
                        currentSurgicalHistory.PatientId = patientClinicalViewModel.PatientId;
                        currentSurgicalHistory.ModifiedBy = patientClinicalViewModel.PatientId;
                        currentSurgicalHistory.ModifiedOn = DateTime.Now;
                        _patientSurgicalHistory.Update(currentSurgicalHistory);
                    }
                    else
                    {
                        PatientSurgicalHistory patientSurgicalHistory = new PatientSurgicalHistory()
                        {
                            IsAppendix = patientClinicalViewModel.patientSurgicalHistoryModel.IsAppendix,
                            IsCataracts = patientClinicalViewModel.patientSurgicalHistoryModel.IsCataracts,
                            IsGallbladder = patientClinicalViewModel.patientSurgicalHistoryModel.IsGallbladder,
                            IsHeartByPass = patientClinicalViewModel.patientSurgicalHistoryModel.IsHeartByPass,
                            IsHysterectomy = patientClinicalViewModel.patientSurgicalHistoryModel.IsHysterectomy,
                            IsStent = patientClinicalViewModel.patientSurgicalHistoryModel.IsStent,
                            IsTonsils = patientClinicalViewModel.patientSurgicalHistoryModel.IsTonsils,
                            PatientId = patientClinicalViewModel.PatientId,
                            CreatedBy = patientClinicalViewModel.PatientId,
                            CreatedOn = DateTime.Now,
                            ModifiedBy = patientClinicalViewModel.PatientId,
                            ModifiedOn = DateTime.Now
                        };
                        _patientSurgicalHistory.Add(patientSurgicalHistory);
                    }
                }
                //Insert/update Clinical History
                if (patientClinicalViewModel.patientReviewOfSystemModel != null)
                {
                    if (patientClinicalViewModel.patientReviewOfSystemModel.Id > 0)
                    {
                        var currentReviewOfSystemHistory = _patientReviewOfSystem.GetSingle(x => x.Id == patientClinicalViewModel.patientReviewOfSystemModel.Id);
                        currentReviewOfSystemHistory.Cardiovascular = patientClinicalViewModel.patientReviewOfSystemModel.Cardiovascular;
                        currentReviewOfSystemHistory.Dental = patientClinicalViewModel.patientReviewOfSystemModel.Dental;
                        currentReviewOfSystemHistory.Diabetes = patientClinicalViewModel.patientReviewOfSystemModel.Diabetes;
                        currentReviewOfSystemHistory.Ear = patientClinicalViewModel.patientReviewOfSystemModel.Ear;
                        currentReviewOfSystemHistory.Eyes = patientClinicalViewModel.patientReviewOfSystemModel.Eyes;
                        currentReviewOfSystemHistory.GeneralCondition = patientClinicalViewModel.patientReviewOfSystemModel.GeneralCondition;
                        currentReviewOfSystemHistory.Hematology = patientClinicalViewModel.patientReviewOfSystemModel.Hematology;
                        currentReviewOfSystemHistory.MusclesOrJoints = patientClinicalViewModel.patientReviewOfSystemModel.MusclesOrJoints;
                        currentReviewOfSystemHistory.Neurological = patientClinicalViewModel.patientReviewOfSystemModel.Neurological;
                        currentReviewOfSystemHistory.ObOrGyn = patientClinicalViewModel.patientReviewOfSystemModel.ObOrGyn;
                        currentReviewOfSystemHistory.Psychological = patientClinicalViewModel.patientReviewOfSystemModel.Psychological;
                        currentReviewOfSystemHistory.Respiratory = patientClinicalViewModel.patientReviewOfSystemModel.Respiratory;
                        currentReviewOfSystemHistory.Skin = patientClinicalViewModel.patientReviewOfSystemModel.Skin;
                        currentReviewOfSystemHistory.Stomach = patientClinicalViewModel.patientReviewOfSystemModel.Stomach;
                        currentReviewOfSystemHistory.Urinary = patientClinicalViewModel.patientReviewOfSystemModel.Urinary;
                        currentReviewOfSystemHistory.PatientId = patientClinicalViewModel.PatientId;
                        currentReviewOfSystemHistory.ModifiedBy = patientClinicalViewModel.PatientId;
                        currentReviewOfSystemHistory.ModifiedOn = DateTime.Now;
                        _patientReviewOfSystem.Update(currentReviewOfSystemHistory);
                    }
                    else
                    {

                        PatientReviewOfSystem patientReviewOfSystem = new PatientReviewOfSystem()
                        {
                            Cardiovascular = patientClinicalViewModel.patientReviewOfSystemModel.Cardiovascular,
                            Dental = patientClinicalViewModel.patientReviewOfSystemModel.Dental,
                            Diabetes = patientClinicalViewModel.patientReviewOfSystemModel.Diabetes,
                            Ear = patientClinicalViewModel.patientReviewOfSystemModel.Ear,
                            Eyes = patientClinicalViewModel.patientReviewOfSystemModel.Eyes,
                            GeneralCondition = patientClinicalViewModel.patientReviewOfSystemModel.GeneralCondition,
                            Hematology = patientClinicalViewModel.patientReviewOfSystemModel.Hematology,
                            MusclesOrJoints = patientClinicalViewModel.patientReviewOfSystemModel.MusclesOrJoints,
                            Neurological = patientClinicalViewModel.patientReviewOfSystemModel.Neurological,
                            ObOrGyn = patientClinicalViewModel.patientReviewOfSystemModel.ObOrGyn,
                            Psychological = patientClinicalViewModel.patientReviewOfSystemModel.Psychological,
                            Respiratory = patientClinicalViewModel.patientReviewOfSystemModel.Respiratory,
                            Skin = patientClinicalViewModel.patientReviewOfSystemModel.Skin,
                            Stomach = patientClinicalViewModel.patientReviewOfSystemModel.Stomach,
                            Urinary = patientClinicalViewModel.patientReviewOfSystemModel.Urinary,
                            PatientId = patientClinicalViewModel.PatientId,
                            CreatedBy = patientClinicalViewModel.PatientId,
                            CreatedOn = DateTime.Now,
                            ModifiedBy = patientClinicalViewModel.PatientId,
                            ModifiedOn = DateTime.Now
                        };
                        _patientReviewOfSystem.Add(patientReviewOfSystem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool SavePatientConditiondata(PatientConditionInfo patientConditionInfo, int patientId)
        {
            bool result = false;
            try
            {
                PatientCondition patientCondition = new PatientCondition()
                {
                    Condition = patientConditionInfo.patientConditionViewModel.Condition,
                    PatientId = patientId,
                    CreatedBy = patientId,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = patientId,
                    ModifiedOn = DateTime.Now

                };
                _patientCondition.Add(patientCondition);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool DeletepatientConditionData(int patientConditionId)
        {
            try
            {
                var patientConditionData = _patientCondition.GetSingle(x => x.Id == patientConditionId);
                if (patientConditionData != null)
                {
                    _patientCondition.Delete(patientConditionData);
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
        #region Providers
        public PatientFavouriteDoctor GetFavouriteDoctors(int patientId)
        {
            try
            {
                PatientFavouriteDoctor patientFavouriteDoctor = new PatientFavouriteDoctor();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("dbo.GetFavoriteDoctor", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@patientId", SqlDbType.Int).Value = patientId;
                        con.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            //Accessing the data using the string key as index
                            GetDoctorsByPatients getDoctorPatients = new GetDoctorsByPatients()
                            {
                                Name = Convert.ToString(rdr["name"]),
                                EmailAddress = Convert.ToString(rdr["EmailAddress"]),
                                UserId = Convert.ToInt32(rdr["userId"]),
                                Phone = Convert.ToString(rdr["phone"]),
                                Address = Convert.ToString(rdr["address"]),
                                SpecialtyName = Convert.ToString(rdr["SpecialtyName"]),
                                City = Convert.ToString(rdr["city"]),
                                ProfilePic = Convert.ToString(rdr["ProfilePic"]),
                                FavoriteDoctorId = Convert.ToInt32(rdr["FavoriteId"]),
                                IsprimaryDoctor = Convert.ToBoolean(rdr["IsprimaryDoctor"]),
                                VideoChat = Convert.ToDecimal(rdr["VideoChat"]),
                                VideoEmail = Convert.ToDecimal(rdr["VideoEmail"]),
                                AskDoctor = Convert.ToDecimal(rdr["AskDoctor"]),
                                Refill = Convert.ToDecimal(rdr["Refill"]),
                            };
                            patientFavouriteDoctor.getDoctorsByPatients.Add(getDoctorPatients);
                        }
                        con.Close();
                    }
                }
                return patientFavouriteDoctor;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public PersonalTab LoadPersonalTabData(int patientId)
        {
            try
            {
                PersonalTab personalTab = new PersonalTab();
                personalTab.PatientPersonalModel = LoadPersonalDetailsByUserId(patientId);
                personalTab.PatientInsuranceModel = LoadInsuranceDetailsByUserId(patientId);
                personalTab.patientMedicationList = LoadMedicationDetailsByUserId(patientId).MedicationList;
                personalTab.AllergyList = LoadAllergiesDetailsByUserId(patientId).AllergyList;
                return personalTab;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveStep1Data(bool isTermAndConditionChecked, int UserId, int doctorId, int serviceType)
        {
            try
            {
                OnlineVisitStep1 onlineVisitStep1 = new OnlineVisitStep1()
                {
                    CreatedBy = UserId,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = UserId,
                    ModifiedOn = DateTime.Now,
                    IsTncChecked = isTermAndConditionChecked,
                    PatientId = UserId,
                    DoctorId = doctorId,
                    ServiceType = serviceType
                };
                _onlineVisitStep1.Add(onlineVisitStep1);
                return onlineVisitStep1.VisitId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int SaveStep2Data(ComplaintTab complaintTab, int UserId)
        {
            try
            {
                OnlineVisitStep2 onlineVisitStep2 = new OnlineVisitStep2()
                {
                    PatientId = UserId,
                    A1 = complaintTab.A1,
                    A2 = complaintTab.A2,
                    A3 = complaintTab.A3,
                    A4 = complaintTab.A4,
                    A5 = complaintTab.A5,
                    A6 = complaintTab.A6,
                    A7 = complaintTab.A7,
                    A8 = complaintTab.A8,
                    A9 = Convert.ToDateTime(complaintTab.A9),
                    A10 = Convert.ToDateTime(complaintTab.A10),
                    A11 = Convert.ToDateTime(complaintTab.A11),
                    DoctorId = complaintTab.DoctorId,
                    step1Id = complaintTab.Step1Id,
                    CreatedBy = UserId,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = UserId,
                    ModifiedOn = DateTime.Now

                };
                _onlineVisitStep2.Add(onlineVisitStep2);
                return onlineVisitStep2.Step2VisitId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveStep5Data(VideoTab videoTab, int UserId)
        {
            try
            {
                OnlineVisitStep5 onlineVisitStep5 = new OnlineVisitStep5()
                {
                    PatientId = UserId,
                    DoctorId = videoTab.DoctorId,
                    TextMessage = videoTab.TextMessage,
                    CreatedBy = UserId,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = UserId,
                    ModifiedOn = DateTime.Now

                };
                _onlineVisitStep5.Add(onlineVisitStep5);
                return onlineVisitStep5.VisitId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
