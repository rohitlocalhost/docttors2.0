using Docttors_portal.Common.Models;
using Docttors_portal.Common.procedureModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Services.Interfaces
{
    public interface IDoctorServices
    {
        List<GetDoctorPatients> GetPatientByDoctor(PatientSearchModel patientSearchModel);
        DoctorAppointmentData LoadDoctorAppointmentData();
        DoctorAppointmentData LoadDoctorAppointmentDataBySelectedDate(DateTime selectedDate);
        bool SaveDoctorAppointment(DoctorAppointmentData doctorAppointmentData);
        DoctorManagement LoadDoctorManageMentData(int UserId);
        int AddUpdateDoctorInformation(DoctorManagement doctorManagement);
        int AddUpdateDoctorEmailConfig(DoctorManagement doctorManagement);
        int AddUpdateDoctorOnlineFees(DoctorManagement doctorManagement);
        int AddUpdateDoctorContact(DoctorManagement doctorManagement);
        int AddUpdateDoctorenews(DoctorManagement doctorManagement);
        bool DeleteNews(int doctorNewsId);
        VideoTab LoadVideoTabData(int doctorId);
        DoctorServiceFeesModel LoadDoctorFeesInfo(int doctorId);
        bool CompleteTreatment(int step1Id, int doctorId);
        DoctorServiceFeesModel LoadDoctorFeesByDoctorId(int doctorId);
        GetDoctorMessageDetails GetDoctorMessagesDetails(int visitId);
    }
}
