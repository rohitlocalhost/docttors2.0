using Docttors_portal.Common;
using Docttors_portal.Common.Models;
using Docttors_portal.Common.procedureModels;
using Docttors_portal.DataAccess.EntityModel;
using Docttors_portal.DataAccess.Interfaces;
using Docttors_portal.Entities.Classes;
using Docttors_portal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Services.Classes
{
    public class DoctorServices : IDoctorServices
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<DailyAppointmentScheduleMaster> _dailyAppointMentScheduleRepository;
        private readonly IRepository<DailyAppointmentSchedule> _dailyAppointmentSchedule;
        private readonly IRepository<DoctorInformationNew> _doctorInformationNew;
        private readonly IRepository<DoctorEmailConfig> _doctorEmailConfig;
        private readonly IRepository<DoctorOnlineFees> _doctorOnlineFees;
        private readonly IRepository<DoctorContact> _doctorContact;
        private readonly IRepository<DoctoreNews> _doctorNews;
        private IRepository<OnlineVisitStep1> _onlineVisitStep1;
        private string connectionString = ConfigurationManager.ConnectionStrings["DocttorsEntities"].ConnectionString;
        public DoctorServices(IUnitOfWork unitOfWork)
        {
            if (unitOfWork != null)
            {
                _unitOfWork = unitOfWork;
                _dailyAppointMentScheduleRepository = _unitOfWork.GetRepository<DailyAppointmentScheduleMaster>();
                _dailyAppointmentSchedule = _unitOfWork.GetRepository<DailyAppointmentSchedule>();
                _doctorInformationNew = _unitOfWork.GetRepository<DoctorInformationNew>();
                _doctorEmailConfig = _unitOfWork.GetRepository<DoctorEmailConfig>();
                _doctorOnlineFees = _unitOfWork.GetRepository<DoctorOnlineFees>();
                _doctorContact = _unitOfWork.GetRepository<DoctorContact>();
                _doctorNews = _unitOfWork.GetRepository<DoctoreNews>();
                _onlineVisitStep1 = _unitOfWork.GetRepository<OnlineVisitStep1>();
            }
        }
        public List<GetDoctorPatients> GetPatientByDoctor(PatientSearchModel patientSearchModel)
        {
            var patientList = new List<GetDoctorPatients>();
            string connectionString = ConfigurationManager.ConnectionStrings["DocttorsEntities"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDoctorPatientsNew", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = patientSearchModel.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = patientSearchModel.LastName;
                    cmd.Parameters.Add("@Mrn", SqlDbType.VarChar).Value = patientSearchModel.MR;
                    cmd.Parameters.Add("@DateOfBirth", SqlDbType.DateTime).Value = string.IsNullOrEmpty(patientSearchModel.DOB) ? (DateTime?)null : Convert.ToDateTime(patientSearchModel.DOB).Date;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        //Accessing the data using the string key as index
                        GetDoctorPatients getDoctorPatients = new GetDoctorPatients()
                        {
                            UserId = Convert.ToInt32(rdr["UserId"]),
                            Name = Convert.ToString(rdr["Name"]),
                            EmailAddress = Convert.ToString(rdr["EmailId"]),
                            Mrn = Convert.ToString(rdr["MRN"]),
                            CellPhone = Convert.ToString(rdr["CellPhone"]),
                            DOB = Convert.ToDateTime(rdr["DOB"])
                        };
                        patientList.Add(getDoctorPatients);
                    }

                }
            }
            return patientList;
        }

        public DoctorAppointmentData LoadDoctorAppointmentData()
        {
            var doctorAppointmentData = new DoctorAppointmentData();
            try
            {
                doctorAppointmentData.MorningAppointmentData = _dailyAppointMentScheduleRepository.GetAll(x => x.Id <= (int)AppointmentSchedule.MorningScheduleId).Select(x => new AppointmentSchduleInfo
                {
                    Id = x.Id,
                    ScheduleDisplayName = x.ScheduleDisplayName,
                    ScheduleName = x.ScheduleName,
                }).ToList();
                doctorAppointmentData.DayAppointmentData = _dailyAppointMentScheduleRepository.GetAll(x => x.Id > (int)AppointmentSchedule.MorningScheduleId && x.Id <= (int)AppointmentSchedule.DayScheduleId).Select(x => new AppointmentSchduleInfo
                {
                    Id = x.Id,
                    ScheduleDisplayName = x.ScheduleDisplayName,
                    ScheduleName = x.ScheduleName,
                }).ToList();
                doctorAppointmentData.EveningAppointmentData = _dailyAppointMentScheduleRepository.GetAll(x => x.Id > (int)AppointmentSchedule.DayScheduleId).Select(x => new AppointmentSchduleInfo
                {
                    Id = x.Id,
                    ScheduleDisplayName = x.ScheduleDisplayName,
                    ScheduleName = x.ScheduleName,
                }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return doctorAppointmentData;
        }

        public DoctorAppointmentData LoadDoctorAppointmentDataBySelectedDate(DateTime selectedDate)
        {
            var doctorAppointmentData = new DoctorAppointmentData();
            try
            {
                doctorAppointmentData.MorningAppointmentData = _dailyAppointMentScheduleRepository.
                    GetAll(x => x.Id <= (int)AppointmentSchedule.MorningScheduleId).Select(x => new AppointmentSchduleInfo
                    {
                        Id = x.Id,
                        ScheduleDisplayName = x.ScheduleDisplayName,
                        ScheduleName = x.ScheduleName,
                    }).ToList();
                doctorAppointmentData.DayAppointmentData = _dailyAppointMentScheduleRepository.GetAll(x => x.Id > (int)AppointmentSchedule.MorningScheduleId && x.Id <= (int)AppointmentSchedule.DayScheduleId).Select(x => new AppointmentSchduleInfo
                {
                    Id = x.Id,
                    ScheduleDisplayName = x.ScheduleDisplayName,
                    ScheduleName = x.ScheduleName,
                }).ToList();
                doctorAppointmentData.EveningAppointmentData = _dailyAppointMentScheduleRepository.GetAll(x => x.Id > (int)AppointmentSchedule.DayScheduleId).Select(x => new AppointmentSchduleInfo
                {
                    Id = x.Id,
                    ScheduleDisplayName = x.ScheduleDisplayName,
                    ScheduleName = x.ScheduleName,
                }).ToList();
                var existingAppointmentData = _dailyAppointmentSchedule.GetAll(x => x.AppointmentScheduleDate == selectedDate).ToList();
                foreach (var item in doctorAppointmentData.MorningAppointmentData)
                {
                    var appointmentExist = existingAppointmentData.Find(x => x.ScheduleId == item.Id);
                    if (appointmentExist != null)
                    {
                        item.IsChecked = true;
                    }
                }
                foreach (var item in doctorAppointmentData.DayAppointmentData)
                {
                    var appointmentExist = existingAppointmentData.Find(x => x.ScheduleId == item.Id);
                    if (appointmentExist != null)
                    {
                        item.IsChecked = true;
                    }
                }
                foreach (var item in doctorAppointmentData.EveningAppointmentData)
                {
                    var appointmentExist = existingAppointmentData.Find(x => x.ScheduleId == item.Id);
                    if (appointmentExist != null)
                    {
                        item.IsChecked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return doctorAppointmentData;
        }
        public bool SaveDoctorAppointment(DoctorAppointmentData doctorAppointmentData)
        {
            try
            {
                DateTime selectedDate = Convert.ToDateTime(doctorAppointmentData.SelectedDate);
                //Delete Existing data for same Date.
                var existingAppointmentData = _dailyAppointmentSchedule.GetAll(x => x.AppointmentScheduleDate == selectedDate).ToList();
                _dailyAppointmentSchedule.DeleteAll(existingAppointmentData);
                var dailyAppointmentScheduleList = new List<DailyAppointmentSchedule>();
                //Add Morning appointmentData
                foreach (var item in doctorAppointmentData.MorningAppointmentData)
                {
                    if (item.IsChecked)
                    {
                        var dailyAppointmentSchedule = new DailyAppointmentSchedule()
                        {
                            DoctorId = doctorAppointmentData.UserId,
                            AppointmentScheduleDate = selectedDate,
                            ScheduleId = item.Id,
                            PatientId = null,
                            Active = true,
                            EXPIRY_DATE = null,
                            CreatedBy = doctorAppointmentData.UserId,
                            CreatedOn = DateTime.Now,
                            ModifiedBy = doctorAppointmentData.UserId,
                            ModifiedOn = DateTime.Now
                        };
                        dailyAppointmentScheduleList.Add(dailyAppointmentSchedule);
                    }
                }
                //Add Day appointmentData
                foreach (var item in doctorAppointmentData.DayAppointmentData)
                {
                    if (item.IsChecked)
                    {
                        var dailyAppointmentSchedule = new DailyAppointmentSchedule()
                        {
                            DoctorId = doctorAppointmentData.UserId,
                            AppointmentScheduleDate = selectedDate,
                            ScheduleId = item.Id,
                            PatientId = null,
                            Active = true,
                            EXPIRY_DATE = null,
                            CreatedBy = doctorAppointmentData.UserId,
                            CreatedOn = DateTime.Now,
                            ModifiedBy = doctorAppointmentData.UserId,
                            ModifiedOn = DateTime.Now
                        };
                        dailyAppointmentScheduleList.Add(dailyAppointmentSchedule);
                    }
                }
                //Add Evening appointmentData
                foreach (var item in doctorAppointmentData.EveningAppointmentData)
                {
                    if (item.IsChecked)
                    {
                        var dailyAppointmentSchedule = new DailyAppointmentSchedule()
                        {
                            DoctorId = doctorAppointmentData.UserId,
                            AppointmentScheduleDate = selectedDate,
                            ScheduleId = item.Id,
                            PatientId = null,
                            Active = true,
                            EXPIRY_DATE = null,
                            CreatedBy = doctorAppointmentData.UserId,
                            CreatedOn = DateTime.Now,
                            ModifiedBy = doctorAppointmentData.UserId,
                            ModifiedOn = DateTime.Now
                        };
                        dailyAppointmentScheduleList.Add(dailyAppointmentSchedule);
                    }
                }
                _dailyAppointmentSchedule.AddAll(dailyAppointmentScheduleList);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DoctorManagement LoadDoctorManageMentData(int UserId)
        {
            try
            {
                var doctorManagement = new DoctorManagement();
                string connectionString = ConfigurationManager.ConnectionStrings["DocttorsEntities"].ConnectionString;
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetDoctorPracticeInformation", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@UserId", SqlDbType.Int).Value = UserId;
                        con.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {

                            doctorManagement.UserId = Convert.ToInt32(rdr["Id"]);
                            doctorManagement.Name = Convert.ToString(rdr["Name"]);
                            //doctorManagement.ProfilePhoto = rdr["ProfilePic"] != null ? (byte[])rdr["ProfilePic"] : null;
                            doctorManagement.DoctorInfoId = Convert.ToInt32(rdr["DoctorinfoId"]);
                            doctorManagement.AboutDoctor = Convert.ToString(rdr["AboutDoctor"]);
                            doctorManagement.Affilation = Convert.ToString(rdr["Affilation"]);
                            doctorManagement.School = Convert.ToString(rdr["school"]);
                            doctorManagement.City = Convert.ToString(rdr["city"]);
                            doctorManagement.StateId = Convert.ToInt32(rdr["stateId"]);
                            doctorManagement.GraduationDate = Convert.ToString(rdr["GradutionDate"]);
                            doctorManagement.language = Convert.ToString(rdr["Languages"]);
                            doctorManagement.Insurance = Convert.ToString(rdr["InsuranceAdded"]);
                            doctorManagement.Services = Convert.ToString(rdr["ServicesAdded"]);
                            doctorManagement.RefferDoctor = Convert.ToString(rdr["DoctorAdded"]);
                            doctorManagement.Message = Convert.ToString(rdr["Message"]);
                        }

                    }
                }
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetRefferDoctor", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@DoctorId", SqlDbType.Int).Value = UserId;
                        con.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            NameIdModel model = new NameIdModel()
                            {
                                Id = Convert.ToInt32(rdr["Id"]),
                                Name = Convert.ToString(rdr["DoctorInfo"]),
                            };
                            doctorManagement.AllDoctors.Add(model);
                        }

                    }
                }
                doctorManagement.DoctorEmailConfigModel = _doctorEmailConfig.GetAll(x => x.UserId == UserId).Select(x => new DoctorEmailConfigModel()
                {
                    Id = x.Id,
                    BillingEmail = x.BillingEmail,
                    DiagnosticEmail = x.DiagonsticEmail,
                    DoctorEmail = x.DoctorEmail,
                    LaboratoryEmail = x.LaboratoryEmail,
                    NurseEmail = x.NurseEmail
                }).FirstOrDefault();
                doctorManagement.DoctorServiceFeesModel = _doctorOnlineFees.GetAll(x => x.UserId == UserId).Select(x => new DoctorServiceFeesModel()
                {
                    Id = x.Id,
                    AskDoctor = x.AskDoctor,
                    FaceToFace = x.face2face,
                    VideoVisit = x.VideoEmail,
                    RxRefill = x.RxRefill
                }).FirstOrDefault();
                doctorManagement.DoctorContactModel = _doctorContact.GetAll(x => x.UserId == UserId).Select(x => new DoctorContactModel()
                {
                    Id = x.Id,
                    DoctorName = x.DoctorName,
                    PracticeName = x.practiceName,
                    Address = x.Address,
                    Phone = x.Phone,
                    FaxNumber = x.FaxNumber,
                    OfficeEmail = x.OfficeEmail,
                    DoctorName_Office1 = x.Office1_DoctorName,
                    PracticeName_Office1 = x.Office1_practiceName,
                    Address_Office1 = x.Office1_Address,
                    Phone_Office1 = x.Office1_Phone,
                    FaxNumber_Office1 = x.Office1_FaxNumber,
                    OfficeEmail_Office1 = x.Office1_OfficeEmail,
                    DoctorName_Office2 = x.Office2_DoctorName,
                    PracticeName_Office2 = x.Office2_practiceName,
                    Address_Office2 = x.Office2_Address,
                    Phone_Office2 = x.Office2_Phone,
                    FaxNumber_Office2 = x.Office2_FaxNumber,
                    OfficeEmail_Office2 = x.Office2_OfficeEmail,
                    DoctorName_Office3 = x.Office3_DoctorName,
                    PracticeName_Office3 = x.Office3_practiceName,
                    Address_Office3 = x.Office3_Address,
                    Phone_Office3 = x.Office3_Phone,
                    FaxNumber_Office3 = x.Office3_FaxNumber,
                    OfficeEmail_Office3 = x.Office3_OfficeEmail
                }).FirstOrDefault();
                doctorManagement.AllNewsData = _doctorNews.GetAll(x => x.UserId == UserId).Select(x => new DoctorNewsModel()
                {
                    Id = x.Id,
                    NewsSubject = x.Subject,
                    Description = x.Description,
                    PublishDate = x.PublishDate
                }).ToList();
                return doctorManagement;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddUpdateDoctorInformation(DoctorManagement doctorManagement)
        {
            int currentUpdatedId = 0;
            try
            {
                if (doctorManagement.DoctorInfoId > 0)
                {
                    var currentDoctorInfoData = _doctorInformationNew.GetSingle(x => x.DoctorInfoId == doctorManagement.DoctorInfoId);
                    currentDoctorInfoData.UserId = doctorManagement.UserId;
                    currentDoctorInfoData.AboutDoctor = doctorManagement.AboutDoctor;
                    currentDoctorInfoData.Affilation = doctorManagement.Affilation;
                    currentDoctorInfoData.School = doctorManagement.School;
                    currentDoctorInfoData.City = doctorManagement.City;
                    currentDoctorInfoData.StateId = doctorManagement.StateId;
                    currentDoctorInfoData.GradutionDate = Convert.ToDateTime(doctorManagement.GraduationDate);
                    currentDoctorInfoData.Languages = doctorManagement.language;
                    currentDoctorInfoData.InsuranceAdded = doctorManagement.Insurance;
                    currentDoctorInfoData.ServicesAdded = doctorManagement.Services;
                    currentDoctorInfoData.DoctorAdded = doctorManagement.RefferDoctor;
                    currentDoctorInfoData.Message = doctorManagement.Message;
                    currentDoctorInfoData.ModifiedBy = doctorManagement.UserId;
                    currentDoctorInfoData.ModifiedOn = DateTime.Now;
                    _doctorInformationNew.Update(currentDoctorInfoData);
                    currentUpdatedId = currentDoctorInfoData.DoctorInfoId;
                }
                else
                {
                    var newDoctorInfo = new DoctorInformationNew()
                    {
                        UserId = doctorManagement.UserId,
                        AboutDoctor = doctorManagement.AboutDoctor,
                        Affilation = doctorManagement.Affilation,
                        School = doctorManagement.School,
                        City = doctorManagement.City,
                        StateId = doctorManagement.StateId,
                        GradutionDate = Convert.ToDateTime(doctorManagement.GraduationDate),
                        Languages = doctorManagement.language,
                        InsuranceAdded = doctorManagement.Insurance,
                        ServicesAdded = doctorManagement.Services,
                        DoctorAdded = doctorManagement.RefferDoctor,
                        Message = doctorManagement.Message,
                        CreatedBy = doctorManagement.UserId,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = doctorManagement.UserId,
                        ModifiedOn = DateTime.Now
                    };
                    _doctorInformationNew.Add(newDoctorInfo);
                    currentUpdatedId = newDoctorInfo.DoctorInfoId;
                }
                return currentUpdatedId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int AddUpdateDoctorEmailConfig(DoctorManagement doctorManagement)
        {
            try
            {
                int currentUpdatedId = 0;
                if (doctorManagement.DoctorEmailConfigModel.Id > 0)
                {
                    var currentDoctorEmailConfig = _doctorEmailConfig.GetSingle(x => x.Id == doctorManagement.DoctorEmailConfigModel.Id);
                    currentDoctorEmailConfig.UserId = doctorManagement.UserId;
                    currentDoctorEmailConfig.DoctorEmail = SessionVariables.LoggedInUser.Email;
                    currentDoctorEmailConfig.DiagonsticEmail = doctorManagement.DoctorEmailConfigModel.DiagnosticEmail;
                    currentDoctorEmailConfig.LaboratoryEmail = doctorManagement.DoctorEmailConfigModel.LaboratoryEmail;
                    currentDoctorEmailConfig.NurseEmail = doctorManagement.DoctorEmailConfigModel.NurseEmail;
                    currentDoctorEmailConfig.BillingEmail = doctorManagement.DoctorEmailConfigModel.BillingEmail;
                    currentDoctorEmailConfig.ModifiedBy = doctorManagement.UserId;
                    currentDoctorEmailConfig.ModifiedOn = DateTime.Now;
                    _doctorEmailConfig.Update(currentDoctorEmailConfig);
                    currentUpdatedId = currentDoctorEmailConfig.Id;
                }
                else
                {
                    var newDoctorEmailConfig = new DoctorEmailConfig()
                    {
                        UserId = doctorManagement.UserId,
                        DoctorEmail = SessionVariables.LoggedInUser.Email,
                        DiagonsticEmail = doctorManagement.DoctorEmailConfigModel.DiagnosticEmail,
                        LaboratoryEmail = doctorManagement.DoctorEmailConfigModel.LaboratoryEmail,
                        NurseEmail = doctorManagement.DoctorEmailConfigModel.NurseEmail,
                        BillingEmail = doctorManagement.DoctorEmailConfigModel.BillingEmail,
                        CreatedBy = doctorManagement.UserId,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = doctorManagement.UserId,
                        ModifiedOn = DateTime.Now
                    };
                    _doctorEmailConfig.Add(newDoctorEmailConfig);
                    currentUpdatedId = newDoctorEmailConfig.Id;
                }
                return currentUpdatedId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddUpdateDoctorOnlineFees(DoctorManagement doctorManagement)
        {
            try
            {
                int currentUpdatedId = 0;
                if (doctorManagement.DoctorServiceFeesModel.Id > 0)
                {
                    var currentDoctorFees = _doctorOnlineFees.GetSingle(x => x.Id == doctorManagement.DoctorServiceFeesModel.Id);
                    currentDoctorFees.UserId = doctorManagement.UserId;
                    currentDoctorFees.AskDoctor = doctorManagement.DoctorServiceFeesModel.AskDoctor;
                    currentDoctorFees.face2face = doctorManagement.DoctorServiceFeesModel.FaceToFace;
                    currentDoctorFees.RxRefill = doctorManagement.DoctorServiceFeesModel.RxRefill;
                    currentDoctorFees.VideoEmail = doctorManagement.DoctorServiceFeesModel.VideoVisit;
                    currentDoctorFees.ModifiedBy = doctorManagement.UserId;
                    currentDoctorFees.ModifiedOn = DateTime.Now;
                    _doctorOnlineFees.Update(currentDoctorFees);
                    currentUpdatedId = currentDoctorFees.Id;
                }
                else
                {
                    var newDoctorFees = new DoctorOnlineFees()
                    {
                        UserId = doctorManagement.UserId,
                        AskDoctor = doctorManagement.DoctorServiceFeesModel.AskDoctor,
                        face2face = doctorManagement.DoctorServiceFeesModel.FaceToFace,
                        RxRefill = doctorManagement.DoctorServiceFeesModel.RxRefill,
                        VideoEmail = doctorManagement.DoctorServiceFeesModel.VideoVisit,
                        CreatedBy = doctorManagement.UserId,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = doctorManagement.UserId,
                        ModifiedOn = DateTime.Now
                    };
                    _doctorOnlineFees.Add(newDoctorFees);
                    currentUpdatedId = newDoctorFees.Id;
                }
                return currentUpdatedId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddUpdateDoctorContact(DoctorManagement doctorManagement)
        {
            try
            {
                int currentUpdatedId = 0;
                if (doctorManagement.DoctorContactModel.Id > 0)
                {
                    var currentDoctorContact = _doctorContact.GetSingle(x => x.Id == doctorManagement.DoctorContactModel.Id);
                    currentDoctorContact.UserId = doctorManagement.UserId;
                    currentDoctorContact.DoctorName = doctorManagement.DoctorContactModel.DoctorName;
                    currentDoctorContact.practiceName = doctorManagement.DoctorContactModel.PracticeName;
                    currentDoctorContact.Address = doctorManagement.DoctorContactModel.Address;
                    currentDoctorContact.Phone = doctorManagement.DoctorContactModel.Phone;
                    currentDoctorContact.FaxNumber = doctorManagement.DoctorContactModel.FaxNumber;
                    currentDoctorContact.OfficeEmail = doctorManagement.DoctorContactModel.OfficeEmail;
                    currentDoctorContact.Office1_DoctorName = doctorManagement.DoctorContactModel.DoctorName_Office1;
                    currentDoctorContact.Office1_practiceName = doctorManagement.DoctorContactModel.PracticeName_Office1;
                    currentDoctorContact.Office1_Address = doctorManagement.DoctorContactModel.Address_Office1;
                    currentDoctorContact.Office1_Phone = doctorManagement.DoctorContactModel.Phone_Office1;
                    currentDoctorContact.Office1_FaxNumber = doctorManagement.DoctorContactModel.FaxNumber_Office1;
                    currentDoctorContact.Office1_OfficeEmail = doctorManagement.DoctorContactModel.OfficeEmail_Office1;
                    currentDoctorContact.Office2_DoctorName = doctorManagement.DoctorContactModel.DoctorName_Office2;
                    currentDoctorContact.Office2_practiceName = doctorManagement.DoctorContactModel.PracticeName_Office2;
                    currentDoctorContact.Office2_Address = doctorManagement.DoctorContactModel.Address_Office2;
                    currentDoctorContact.Office2_Phone = doctorManagement.DoctorContactModel.Phone_Office2;
                    currentDoctorContact.Office2_FaxNumber = doctorManagement.DoctorContactModel.FaxNumber_Office2;
                    currentDoctorContact.Office2_OfficeEmail = doctorManagement.DoctorContactModel.OfficeEmail_Office2;
                    currentDoctorContact.Office3_DoctorName = doctorManagement.DoctorContactModel.DoctorName_Office3;
                    currentDoctorContact.Office3_practiceName = doctorManagement.DoctorContactModel.PracticeName_Office3;
                    currentDoctorContact.Office3_Address = doctorManagement.DoctorContactModel.Address_Office3;
                    currentDoctorContact.Office3_Phone = doctorManagement.DoctorContactModel.Phone_Office3;
                    currentDoctorContact.Office3_FaxNumber = doctorManagement.DoctorContactModel.FaxNumber_Office3;
                    currentDoctorContact.Office3_OfficeEmail = doctorManagement.DoctorContactModel.OfficeEmail_Office3;
                    currentDoctorContact.ModifiedBy = doctorManagement.UserId;
                    currentDoctorContact.ModifiedOn = DateTime.Now;
                    _doctorContact.Update(currentDoctorContact);
                    currentUpdatedId = currentDoctorContact.Id;
                }
                else
                {
                    var newDoctorContact = new DoctorContact()
                    {
                        UserId = doctorManagement.UserId,
                        DoctorName = doctorManagement.DoctorContactModel.DoctorName,
                        practiceName = doctorManagement.DoctorContactModel.PracticeName,
                        Address = doctorManagement.DoctorContactModel.Address,
                        Phone = doctorManagement.DoctorContactModel.Phone,
                        FaxNumber = doctorManagement.DoctorContactModel.FaxNumber,
                        OfficeEmail = doctorManagement.DoctorContactModel.OfficeEmail,
                        Office1_DoctorName = doctorManagement.DoctorContactModel.DoctorName_Office1,
                        Office1_practiceName = doctorManagement.DoctorContactModel.PracticeName_Office1,
                        Office1_Address = doctorManagement.DoctorContactModel.Address_Office1,
                        Office1_Phone = doctorManagement.DoctorContactModel.Phone_Office1,
                        Office1_FaxNumber = doctorManagement.DoctorContactModel.FaxNumber_Office1,
                        Office1_OfficeEmail = doctorManagement.DoctorContactModel.OfficeEmail_Office1,
                        Office2_DoctorName = doctorManagement.DoctorContactModel.DoctorName_Office2,
                        Office2_practiceName = doctorManagement.DoctorContactModel.PracticeName_Office2,
                        Office2_Address = doctorManagement.DoctorContactModel.Address_Office2,
                        Office2_Phone = doctorManagement.DoctorContactModel.Phone_Office2,
                        Office2_FaxNumber = doctorManagement.DoctorContactModel.FaxNumber_Office2,
                        Office2_OfficeEmail = doctorManagement.DoctorContactModel.OfficeEmail_Office2,
                        Office3_DoctorName = doctorManagement.DoctorContactModel.DoctorName_Office3,
                        Office3_practiceName = doctorManagement.DoctorContactModel.PracticeName_Office3,
                        Office3_Address = doctorManagement.DoctorContactModel.Address_Office3,
                        Office3_Phone = doctorManagement.DoctorContactModel.Phone_Office3,
                        Office3_FaxNumber = doctorManagement.DoctorContactModel.FaxNumber_Office3,
                        Office3_OfficeEmail = doctorManagement.DoctorContactModel.OfficeEmail_Office3,
                        CreatedBy = doctorManagement.UserId,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = doctorManagement.UserId,
                        ModifiedOn = DateTime.Now
                    };
                    _doctorContact.Add(newDoctorContact);
                    currentUpdatedId = newDoctorContact.Id;
                }
                return currentUpdatedId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddUpdateDoctorenews(DoctorManagement doctorManagement)
        {
            try
            {
                int currentUpdatedId = 0;
                if (doctorManagement.DoctorNewsModel.Id > 0)
                {
                    var currentDoctorNews = _doctorNews.GetSingle(x => x.Id == doctorManagement.DoctorNewsModel.Id);
                    currentDoctorNews.UserId = doctorManagement.UserId;
                    currentDoctorNews.PublishDate = doctorManagement.DoctorNewsModel.PublishDate;
                    currentDoctorNews.Subject = doctorManagement.DoctorNewsModel.NewsSubject;
                    currentDoctorNews.Description = doctorManagement.DoctorNewsModel.Description;
                    currentDoctorNews.Description = doctorManagement.DoctorNewsModel.Description;
                    currentDoctorNews.ModifiedBy = doctorManagement.UserId;
                    currentDoctorNews.ModifiedOn = DateTime.Now;
                    _doctorNews.Update(currentDoctorNews);
                    currentUpdatedId = currentDoctorNews.Id;
                }
                else
                {
                    var newDoctorNews = new DoctoreNews()
                    {
                        UserId = doctorManagement.UserId,
                        PublishDate = doctorManagement.DoctorNewsModel.PublishDate,
                        Subject = doctorManagement.DoctorNewsModel.NewsSubject,
                        Description = doctorManagement.DoctorNewsModel.Description,
                        CreatedBy = doctorManagement.UserId,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = doctorManagement.UserId,
                        ModifiedOn = DateTime.Now
                    };
                    _doctorNews.Add(newDoctorNews);
                    currentUpdatedId = newDoctorNews.Id;
                }
                return currentUpdatedId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteNews(int doctorNewsId)
        {
            try
            {
                var currentNewsData = _doctorNews.GetSingle(x => x.Id == doctorNewsId);
                if (currentNewsData != null)
                {
                    _doctorNews.Delete(currentNewsData);
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public VideoTab LoadVideoTabData(int doctorId)
        {
            try
            {
                var videoTab = new VideoTab();
                var doctorEmailConfig = _doctorEmailConfig.GetSingle(x => x.Id == doctorId);
                if (doctorEmailConfig != null)
                {
                    videoTab.NurseEmail = doctorEmailConfig.NurseEmail;
                }
                return videoTab;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DoctorServiceFeesModel LoadDoctorFeesInfo(int doctorId)
        {
            try
            {
                return _doctorOnlineFees.GetAll().Where(x => x.UserId == doctorId).Select(x => new DoctorServiceFeesModel()
                {
                    Id = x.Id,
                    AskDoctor = x.AskDoctor,
                    VideoVisit = x.VideoEmail,
                    FaceToFace = x.face2face,
                    RxRefill = x.RxRefill
                }).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CompleteTreatment(int step1Id, int doctorId)
        {
            try
            {
                var currentStep1Data = _onlineVisitStep1.GetSingle(x => x.VisitId == step1Id);
                if (currentStep1Data != null && currentStep1Data.DoctorId == doctorId)
                {
                    currentStep1Data.IsTreated = true;
                    _onlineVisitStep1.Update(currentStep1Data);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DoctorServiceFeesModel LoadDoctorFeesByDoctorId(int doctorId)
        {
            try
            {
                var doctorServiceFeesModel = _doctorOnlineFees.GetAll(x => x.UserId == doctorId).Select(x => new DoctorServiceFeesModel()
                {
                    Id = x.Id,
                    AskDoctor = x.AskDoctor,
                    FaceToFace = x.face2face,
                    VideoVisit = x.VideoEmail,
                    RxRefill = x.RxRefill
                }).FirstOrDefault();
                return doctorServiceFeesModel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GetDoctorMessageDetails GetDoctorMessagesDetails(int visitId)
        {
            try
            {
                GetDoctorMessageDetails doctorMessages = new GetDoctorMessageDetails();
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("GetDoctorMessageDetails", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@visitId", SqlDbType.Int).Value = visitId;
                        con.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();
                        while (rdr.Read())
                        {
                            doctorMessages.VisitId = Convert.ToInt32(rdr["VisitId"]);
                            doctorMessages.PatientName = Convert.ToString(rdr["PatientName"]);
                            doctorMessages.DoctorName = Convert.ToString(rdr["DoctorName"]);
                            doctorMessages.Subject = Convert.ToString(rdr["Subject"]);
                            doctorMessages.CreatedOn = Convert.ToString(rdr["CreatedOn"]);
                            doctorMessages.Phone = Convert.ToString(rdr["Phone"]);
                            doctorMessages.PatientEmail = Convert.ToString(rdr["PatientEmail"]);
                            doctorMessages.Sex = Convert.ToString(rdr["Sex"]);
                            doctorMessages.Dob = Convert.ToString(rdr["Dob"]);
                            doctorMessages.HeightWeight = Convert.ToString(rdr["HeightWeight"]);
                            doctorMessages.Address = Convert.ToString(rdr["Address"]);
                            doctorMessages.InsuranceCompanyName = Convert.ToString(rdr["InsuranceCompanyName"]);
                            doctorMessages.InsuranceCompanyphone = Convert.ToString(rdr["InsuranceCompanyphone"]);
                            doctorMessages.InsuranceCompanyAddress = Convert.ToString(rdr["InsuranceCompanyAddress"]);
                            doctorMessages.InsuranceIdNumber = Convert.ToString(rdr["InsuranceIdNumber"]);
                            doctorMessages.InsuranceGroupId = Convert.ToString(rdr["InsuranceGroupId"]);
                            doctorMessages.PharmacyName = Convert.ToString(rdr["PharmacyName"]);
                            doctorMessages.PharmacyState = Convert.ToString(rdr["PharmacyState"]);
                            doctorMessages.PharmacyAddress = Convert.ToString(rdr["PharmacyAddress"]);
                            doctorMessages.PharmacyCity = Convert.ToString(rdr["PharmacyCity"]);
                            doctorMessages.PharmacyZipCode = Convert.ToString(rdr["PharmacyZipCode"]);
                            doctorMessages.AppointmentDate = Convert.ToString(rdr["AppointmentDate"]);
                            doctorMessages.TextMessage = Convert.ToString(rdr["TextMessage"]);
                            doctorMessages.Complaint = Convert.ToString(rdr["Complaint"]);
                            doctorMessages.Day = Convert.ToString(rdr["Day"]);
                            doctorMessages.CType = Convert.ToString(rdr["CType"]);
                            doctorMessages.Condition = Convert.ToString(rdr["Condition"]);
                            doctorMessages.CurrentPresc = Convert.ToString(rdr["CurrentPresc"]);
                            doctorMessages.BloodPressure = Convert.ToString(rdr["BloodPressure"]);
                            doctorMessages.Breathing = Convert.ToString(rdr["Breathing"]);
                            doctorMessages.Weight = Convert.ToString(rdr["Weight"]);
                            doctorMessages.DoctorVisitDate = Convert.ToString(rdr["DoctorVisitDate"]);
                            doctorMessages.OnlineVisitDate = Convert.ToString(rdr["OnlineVisitDate"]);
                        }

                    }
                    con.Close();
                }
                return doctorMessages;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
