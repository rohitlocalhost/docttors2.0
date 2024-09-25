using Docttors_portal.Common.Models;
using Docttors_portal.Common.procedureModels;
using Docttors_portal.DataAccess.Interfaces;
using Docttors_portal.Entities.Classes;
using Docttors_portal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

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

        public PatientPersonalServices(IUnitOfWork unitOfWork)
        {
            if (unitOfWork != null)
            {
                _unitOfWork = unitOfWork;
                _patientRepository = _unitOfWork.GetRepository<PatientPersonalNew>();
                _PhysicianRepository = _unitOfWork.GetRepository<PatientPhysicianDetailsNew>();
                _insuranceRepository = _unitOfWork.GetRepository<PatientInsuranceNew>();
                _allergiesRepository = _unitOfWork.GetRepository<PatientAllergiesNew>();
                _hospitalrepository = _unitOfWork.GetRepository<PatientHospitalDetailsNew>();
                _pharmacyRepository = _unitOfWork.GetRepository<PatientPharmacyDetailsNew>();
                _medicationRepository = _unitOfWork.GetRepository<PatientMedicationDetailsNew>();
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

        #region Allergies
        public PatientAllergiesModel LoadAllergiesDetailsByUserId(int userId)
        {
            var ptData = new PatientAllergiesModel();
            try
            {
                var insurancelData = _allergiesRepository.GetSingle(x => x.UserId == userId);
                if (insurancelData != null)
                {
                    ptData.PatientAllergyId = insurancelData.PatientAllergyId;
                    ptData.UserId = insurancelData.UserId;
                    ptData.AllergyName = insurancelData.AllergyName;
                    ptData.Reaction = insurancelData.Reaction;
                }
                else
                {
                    ptData = new PatientAllergiesModel();
                }
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

        #endregion

        #region Hospital Details
        public PatientHospitalModel LoadHospitalDetailsByUserId(int userId)
        {
            var ptData = new PatientHospitalModel();
            try
            {
                var hospital = _hospitalrepository.GetSingle(x => x.UserId == userId);
                if (hospital != null)
                {
                    ptData.PatientHospitalId = hospital.PatientHospitalId;
                    ptData.UserId = hospital.UserId;
                    ptData.HospitalName = hospital.HospitalName;
                    ptData.Phone = hospital.Phone;
                    ptData.Address1 = hospital.Address1;
                    ptData.Address2 = hospital.Address2;
                    ptData.City = hospital.City;
                    ptData.StateId = hospital.StateId;
                    ptData.ZipCode = hospital.ZipCode;
                    ptData.ReasonForVisit = hospital.ReasonForVisit;
                }
                else
                {
                    ptData = new PatientHospitalModel();
                }
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

        #endregion

        #region Pharmacy
        public PatientPharmacyModel LoadPharmacyDetailsByUserId(int userId)
        {
            var ptData = new PatientPharmacyModel();
            try
            {
                var pharmacy = _pharmacyRepository.GetSingle(x => x.UserId == userId);
                if (pharmacy != null)
                {
                    ptData.PatientPharmacyId = pharmacy.PatientPharmacyId;
                    ptData.UserId = pharmacy.UserId;
                    ptData.IsPrimaryPharmacy = pharmacy.isPrimaryPharmacy;
                    ptData.Name = pharmacy.PharmacyName;
                    ptData.Phone = pharmacy.PhramacyPhone;
                    ptData.Address1 = pharmacy.Address1;
                    ptData.Address2 = pharmacy.Address2;
                    ptData.City = pharmacy.City;
                    ptData.StateId = pharmacy.StateId;
                    ptData.ZipCode = pharmacy.ZipCode;
                }
                else
                {
                    ptData = new PatientPharmacyModel();
                }
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

        #endregion

        #region Pharmacy
        public PatientMedicationModel LoadMedicationDetailsByUserId(int userId)
        {
            var ptData = new PatientMedicationModel();
            try
            {
                var pharmacy = _medicationRepository.GetSingle(x => x.UserId == userId);
                if (pharmacy != null)
                {
                    ptData.PatientMedicationId = pharmacy.PatientMedicationId;
                    ptData.UserId = pharmacy.UserId;
                    ptData.CurrentMedication = pharmacy.CurrentMedication;
                    ptData.MedicationName = pharmacy.MedicationName;
                    ptData.Dosage = pharmacy.Dosage;
                    ptData.Frequency = pharmacy.Frequency;
                    ptData.PrescibedPhysician = pharmacy.PrescibedPhysician;
                    ptData.Prescription = pharmacy.Prescription;
                    ptData.ReasonforPrescription = pharmacy.ReasonforPrescription;
                    ptData.DateOfPrescription = pharmacy.DateOfPrescription;
                }
                else
                {
                    ptData = new PatientMedicationModel();
                }
                return ptData;

            }
            catch (Exception ex)
            {

                throw;
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
                    medicationDetails.DateOfPrescription = medicationModel.DateOfPrescription;
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
                    DateOfPrescription = medicationModel.DateOfPrescription,
                };
                _medicationRepository.Add(medicationDetails);
                return medicationDetails.PatientMedicationId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region
        public List<GetDoctorsByPatients> GetDoctorByPatient(PatientSearchDoctorModel patientSearchModel)
        {
            var doctorList = new List<GetDoctorsByPatients>();
            string connectionString = ConfigurationManager.ConnectionStrings["DocttorsEntities"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDoctorsByPatients", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = patientSearchModel.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = patientSearchModel.LastName;
                    cmd.Parameters.Add("@PhysicianSpecialtyId", SqlDbType.VarChar).Value = patientSearchModel.PhysicianSpecialtyId;
                    cmd.Parameters.Add("@City", SqlDbType.VarChar).Value = patientSearchModel.City;
                    cmd.Parameters.Add("@StateId", SqlDbType.VarChar).Value = patientSearchModel.StateId;
                    cmd.Parameters.Add("@ZipCode", SqlDbType.VarChar).Value = patientSearchModel.ZipCode;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        //Accessing the data using the string key as index
                        GetDoctorsByPatients getDoctorPatients = new GetDoctorsByPatients()
                        {
                            patientId = Convert.ToInt32(rdr["PatientId"]),
                            FirstName = Convert.ToString(rdr["FirstName"]),
                            LastName = Convert.ToString(rdr["LastName"]),
                            EmailAddress = Convert.ToString(rdr["EmailAddress"]),
                            UserId = Convert.ToInt32(rdr["LoginId"]),
                            Mrn = Convert.ToString(rdr["MRN"]),
                            CellPhone = Convert.ToString(rdr["CellPhone"]),
                            DOB = Convert.ToDateTime(rdr["DOB"]),
                            CCMIsPatientEligible = Convert.ToString(rdr["CCMIsPatientEligible"]),
                            CCMIsConsentProvided = Convert.ToString(rdr["CCMConsentProvided"]),
                            FavoriteDoctors = Convert.ToString(rdr["FavoriteDoctors"])
                        };
                        doctorList.Add(getDoctorPatients);
                    }

                }
            }
            return doctorList;
        }

        #endregion
    }
}
