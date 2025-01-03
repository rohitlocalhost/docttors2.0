using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("DailyAppointmentScheduleMaster")]
    public class DailyAppointmentScheduleMaster : BaseClass
    {
        public int Id { get; set; }
        public int? ScheduleId { get; set; }
        public string ScheduleName { get; set; }
        public string ScheduleDisplayName { get; set; }
        public bool? Active { get; set; }

    }
}
