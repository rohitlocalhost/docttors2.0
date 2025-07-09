using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("DailyAppointmentSchedule")]
    public class DailyAppointmentSchedule : BaseClass
    {
        public int DailyAppointmentScheduleID { get; set; }
        public int DoctorId { get; set; }
        public DateTime AppointmentScheduleDate { get; set; }
        public int ScheduleId { get; set; }
        public int? PatientId { get; set; }
        public bool? Active { get; set; }
        public DateTime? EXPIRY_DATE { get; set; }
    }
}
