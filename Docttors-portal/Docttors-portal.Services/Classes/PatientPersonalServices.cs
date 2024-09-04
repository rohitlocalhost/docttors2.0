using Docttors_portal.Common.Models;
using Docttors_portal.DataAccess.Interfaces;
using Docttors_portal.Entities.Classes;
using Docttors_portal.Services.Interfaces;
using System;

namespace Docttors_portal.Services.Classes
{
    public class PatientPersonalServices : IPatientPersonalServices
    {
        private IRepository<PatientPersonal> _patientRepository;

        public int SavePatientDetails(PatientPersonalModel personalModel)
        {
            try
            {
                var patient = new PatientPersonal()
                {
                    FirstName = personalModel.FirstName,
                    LastName = personalModel.LastName,
                    MiddleName = personalModel.MiddleName,
                    WorkPhone = personalModel.WorkPhone,
                    HomePhone = personalModel.HomePhone,
                    CellPhone = personalModel.CellPhone,
                    Address1 = personalModel.Address1,
                    Address2 = personalModel.MiddleName,
                    City = personalModel.City,
                    StateId = personalModel.StateId,
                    ZipCode = personalModel.ZipCode,
                    CountryId = personalModel.CountryId,
                    GenderId = personalModel.GenderId,
                    DOB = personalModel.DOB,
                    SSN = personalModel.SSN,
                    Height = personalModel.Height,
                    Weight = personalModel.Weight,
                    BloodPressureId = personalModel.BloodPressureId,
                    EyeColorId = personalModel.EyeColorId,
                    NoOfChildren = personalModel.NoOfChildren,
                    SpousName = personalModel.SpousName,
                    FatherLiving = personalModel.FatherLiving,
                    MotherLiving = personalModel.MotherLiving,
                    SisterLiving = personalModel.SisterLiving,
                    BrotherLiving = personalModel.BrotherLiving,
                    MaritalId = personalModel.MaritalId,
                    EducationId = personalModel.EducationId,
                    RaceID = personalModel.RaceID,
                    HairColorID = personalModel.HairColorID,
                    Occupation = personalModel.Occupation,
                    OrganDonor = personalModel.OrganDonor,
                    RcopiaID = personalModel.RcopiaID,
                    ReasonForGuardianship = personalModel.ReasonForGuardianship,
                    InsuranceCompany = personalModel.InsuranceCompany,
                    SelfPaid = personalModel.SelfPaid,
                    MRN = personalModel.MRN,
                    Profession = personalModel.Profession,
                };
                _patientRepository.Add(patient);
                return patient.PatientPersonalId;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
