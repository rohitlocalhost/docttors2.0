using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Common.Models
{
    public class DoctorAppointmentData
    {
        public DoctorAppointmentData()
        {
            MorningAppointmentData = new List<AppointmentSchduleInfo>();
            DayAppointmentData = new List<AppointmentSchduleInfo>();
            EveningAppointmentData = new List<AppointmentSchduleInfo>();
        }
        public List<AppointmentSchduleInfo> MorningAppointmentData { get; set; }
        public List<AppointmentSchduleInfo> DayAppointmentData { get; set; }
        public List<AppointmentSchduleInfo> EveningAppointmentData { get; set; }
        [Required(ErrorMessage = "Please select Date First")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public string SelectedDate { get; set; }
        public int UserId { get; set; }
    }
    public class AppointmentSchduleInfo
    {
        public int Id { get; set; }
        public int? ScheduleId { get; set; }
        public string ScheduleName { get; set; }
        public string ScheduleDisplayName { get; set; }
        public bool? Active { get; set; }
        public bool IsChecked { get; set; }
    }
}
