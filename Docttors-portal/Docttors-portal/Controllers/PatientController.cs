using Docttors_portal.Common;
using Docttors_portal.Common.Models;
using Docttors_portal.Filter;
using Docttors_portal.Services.Interfaces;
using System;
using System.Web.Mvc;

namespace Docttors_portal.Controllers
{
    [SessionCheck]
    [MyExceptionHandler]
    public class PatientController : BaseController
    {
        #region Initialize
        private readonly IPatientPersonalServices _personalServices;
        private readonly IPatientPhysicianServices _physicianServices;
        private readonly ICommonUtilityService _commonUtilityService;
        private readonly long _userId;
        #endregion
        public PatientController(IPatientPersonalServices personalServices, IPatientPhysicianServices physicianServices, ICommonUtilityService commonUtilityService)
        {
            _personalServices = personalServices;
            _commonUtilityService = commonUtilityService;
            _physicianServices = physicianServices;
            //_userId = Convert.ToInt64(Session["UserId"]);
        }
        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MyHealthRecord()
        {
            return View();
        }
        public ActionResult Message()
        {
            return View();
        }
        public ActionResult MyProvider()
        {
            return View();
        }
        public ActionResult Search()
        {
            return View();
        }
        public ActionResult SystemCheck()
        {
            return View();
        }
        public ActionResult PersonalDetails()
        {
            var patientPersonalModel = new PatientPersonalModel();
            patientPersonalModel = _personalServices.LoadPersonalDetailsByUserId(Convert.ToInt32(Session["UserId"]));
            patientPersonalModel = LoadlistData(patientPersonalModel);
            return View(patientPersonalModel);
        }

        [HttpPost]
        public ActionResult PersonalDetails(PatientPersonalModel patientRegisterationModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    patientRegisterationModel.UserId = Convert.ToInt32(Session["UserId"]);
                    if (patientRegisterationModel.PatientPersonalId > 0)
                    {
                        //Update personDetails
                        if (_personalServices.UpdatePatientDetails(patientRegisterationModel))
                        {
                            ViewBag.Message = "Data Updated Successfully";
                        }
                        else
                        {
                            ViewBag.Error = "Some issue occured, Data Not saved";
                        }
                    }
                    else
                    {
                        //Add new personDetails
                        int PatientPersonalId = _personalServices.SavePatientDetails(patientRegisterationModel);
                        patientRegisterationModel.PatientPersonalId = PatientPersonalId;
                        ViewBag.Message = "Data Saved Successfully";
                    }
                }
                return PersonalDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private PatientPersonalModel LoadlistData(PatientPersonalModel patientPersonalModel)
        {

            patientPersonalModel.StateList = _commonUtilityService.GetAllStates();
            patientPersonalModel.CountryList = _commonUtilityService.GetAllCountry();
            patientPersonalModel.GenderList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Gender);
            patientPersonalModel.MaritalMasterList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.MaritalStatus);
            patientPersonalModel.EducationMasterList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Education);
            patientPersonalModel.EthnicityList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Ethnicity);
            patientPersonalModel.HeightList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Height);
            return patientPersonalModel;
        }

        #region Physician details
        public ActionResult PhysicianDetails()
        {
            var physicanData = _physicianServices.LoadPhysicianDetailsByUserId(Convert.ToInt32(Session["UserId"]));
            physicanData = LoadlistPhysicianData(physicanData);
            return View(physicanData);
        }

        private PatientPhysicianModel LoadlistPhysicianData(PatientPhysicianModel physicianModel)
        {

            physicianModel.StateList = _commonUtilityService.GetAllStates();
            physicianModel.SpecialistTypesList = _commonUtilityService.GetTypeCategoryByCategoryId((int)TypeCategory.Specialty);
            return physicianModel;
        }

        [HttpPost]
        public ActionResult PatientPhysicianDetails(PatientPhysicianModel patientRegisterationModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    patientRegisterationModel.UserId = Convert.ToInt32(Session["UserId"]);
                    if (patientRegisterationModel.PatientPhysicianId > 0)
                    {
                        //Update Physician Details
                        if (_physicianServices.UpdatePatientPhysicianDetails(patientRegisterationModel))
                        {
                            ViewBag.Message = "Data Updated Successfully";
                        }
                        else
                        {
                            ViewBag.Error = "Some issue occured, Data Not saved";
                        }
                    }
                    else
                    {
                        //Add new Physician Details
                        int PatientPhysicianId = _physicianServices.SavePatientPhysicianDetails(patientRegisterationModel);
                        patientRegisterationModel.PatientPhysicianId = PatientPhysicianId;
                        ViewBag.Message = "Data Saved Successfully";
                    }
                }
                return PhysicianDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Insuarance
        public ActionResult Insurance()
        {
            var insuranceModel = _personalServices.LoadInsuranceDetailsByUserId(Convert.ToInt32(Session["UserId"]));
            insuranceModel = LoadListInsuranceData(insuranceModel);
            return View(insuranceModel);
        }
        private PatientInsuranceModel LoadListInsuranceData(PatientInsuranceModel insuranceModel)
        {

            insuranceModel.StateList = _commonUtilityService.GetAllStates();
            insuranceModel.CountryList = _commonUtilityService.GetAllCountry();
            return insuranceModel;
        }

        [HttpPost]
        public ActionResult InsuranceDetails(PatientInsuranceModel patientInsuranceModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    patientInsuranceModel.UserId = Convert.ToInt32(Session["UserId"]);
                    if (patientInsuranceModel.PatientInsuranceId > 0)
                    {
                        //Update patientInsurance Details
                        if (_personalServices.UpdateInsuranceDetails(patientInsuranceModel))
                        {
                            ViewBag.Message = "Data Updated Successfully";
                        }
                        else
                        {
                            ViewBag.Error = "Some issue occured, Data Not saved";
                        }
                    }
                    else
                    {
                        //Add new patientInsurance Details
                        int patientInsuranceId = _personalServices.SavePatientInsuranceDetails(patientInsuranceModel);
                        patientInsuranceModel.PatientInsuranceId = patientInsuranceId;
                        ViewBag.Message = "Data Saved Successfully";
                    }
                }
                return Insurance();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Patient Allergies
        public ActionResult PatientAllergies()
        {
            var allergiesModel = _personalServices.LoadAllergiesDetailsByUserId(Convert.ToInt32(Session["UserId"]));
            return View(allergiesModel);
        }
        [HttpPost]
        public ActionResult PatientAllergiesDetails(PatientAllergiesModel allergiesModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    allergiesModel.UserId = Convert.ToInt32(Session["UserId"]);
                    if (allergiesModel.PatientAllergyId > 0)
                    {
                        //Update Allergies Details
                        if (_personalServices.UpdateAllergiesDetails(allergiesModel))
                        {
                            ViewBag.Message = "Data Updated Successfully";
                        }
                        else
                        {
                            ViewBag.Error = "Some issue occured, Data Not saved";
                        }
                    }
                    else
                    {
                        //Add new Allergies Details
                        int patientAllergyId = _personalServices.SaveAllergiesDetails(allergiesModel);
                        allergiesModel.PatientAllergyId = patientAllergyId;
                        ViewBag.Message = "Data Saved Successfully";
                    }
                }
                return PatientAllergies();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Hospital Details
        public ActionResult HospitalDetails()
        {
            var hospitalModel = _personalServices.LoadHospitalDetailsByUserId(Convert.ToInt32(Session["UserId"]));
            hospitalModel = LoadListHospitalData(hospitalModel);
            return View(hospitalModel);
        }
        private PatientHospitalModel LoadListHospitalData(PatientHospitalModel hospitalModel)
        {
            hospitalModel.StateList = _commonUtilityService.GetAllStates();
            return hospitalModel;
        }

        [HttpPost]
        public ActionResult HospitalDetails(PatientHospitalModel hospitalModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    hospitalModel.UserId = Convert.ToInt32(Session["UserId"]);
                    if (hospitalModel.PatientHospitalId > 0)
                    {
                        //Update Allergies Details
                        if (_personalServices.UpdateHospitalDetails(hospitalModel))
                        {
                            ViewBag.Message = "Data Updated Successfully";
                        }
                        else
                        {
                            ViewBag.Error = "Some issue occured, Data Not saved";
                        }
                    }
                    else
                    {
                        //Add new Allergies Details
                        int patientAllergyId = _personalServices.SaveHospitalDetails(hospitalModel);
                        hospitalModel.PatientHospitalId = patientAllergyId;
                        ViewBag.Message = "Data Saved Successfully";
                    }
                }
                return HospitalDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Pharmacy Details
        public ActionResult PharmacyDetails()
        {
            var pharmacyModel = _personalServices.LoadPharmacyDetailsByUserId(Convert.ToInt32(Session["UserId"]));
            pharmacyModel = LoadListPharmacyData(pharmacyModel);
            return View(pharmacyModel);
        }
        private PatientPharmacyModel LoadListPharmacyData(PatientPharmacyModel pharmacyModel)
        {
            pharmacyModel.StateList = _commonUtilityService.GetAllStates();
            return pharmacyModel;
        }

        [HttpPost]
        public ActionResult PharmacyDetails(PatientPharmacyModel pharmacyModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    pharmacyModel.UserId = Convert.ToInt32(Session["UserId"]);
                    if (pharmacyModel.PatientPharmacyId > 0)
                    {
                        //Update Pharmacy Details
                        if (_personalServices.UpdatePharmacyDetails(pharmacyModel))
                        {
                            ViewBag.Message = "Data Updated Successfully";
                        }
                        else
                        {
                            ViewBag.Error = "Some issue occured, Data Not saved";
                        }
                    }
                    else
                    {
                        //Add new Pharmacy Details
                        int patientPharmacyId = _personalServices.SavePharmacyDetails(pharmacyModel);
                        pharmacyModel.PatientPharmacyId = patientPharmacyId;
                        ViewBag.Message = "Data Saved Successfully";
                    }
                }
                return PharmacyDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Mediacation Details
        public ActionResult MedicationDetails()
        {
            var medicationModel = _personalServices.LoadMedicationDetailsByUserId(Convert.ToInt32(Session["UserId"]));
            return View(medicationModel);
        }

        [HttpPost]
        public ActionResult MedicationDetails(PatientMedicationModel medicationModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    medicationModel.UserId = Convert.ToInt32(Session["UserId"]);
                    if (medicationModel.PatientMedicationId > 0)
                    {
                        //Update Pharmacy Details
                        if (_personalServices.UpdateMedicationDetails(medicationModel))
                        {
                            ViewBag.Message = "Data Updated Successfully";
                        }
                        else
                        {
                            ViewBag.Error = "Some issue occured, Data Not saved";
                        }
                    }
                    else
                    {
                        //Add new Pharmacy Details
                        int PatientMedicationId = _personalServices.SaveMedicationDetails(medicationModel);
                        medicationModel.PatientMedicationId = PatientMedicationId;
                        ViewBag.Message = "Data Saved Successfully";
                    }
                }
                return MedicationDetails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}