using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("PatientVitalsNew")]
    public class PatientVitalsNew : BaseClass
    {
        [Key]
        public int PatientVitalId { get; set; }
        public int UserId { get; set; }
        public DateTime ObservationDateTime { get; set; }
        public string Weight { get; set; }
        public string WeightUnit { get; set; }
        public string Spirometer { get; set; }
        public string Temprature { get; set; }
        public string TempratureUnit { get; set; }
        public string Spo2 { get; set; }
        public string PulseRate { get; set; }
        public string Bloodglucose { get; set; }
        public string Sys { get; set; }
        public string Dia { get; set; }
    }
}
