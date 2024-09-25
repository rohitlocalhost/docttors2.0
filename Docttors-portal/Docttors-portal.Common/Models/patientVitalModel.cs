using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Common.Models
{
    public class PatientVitalModel
    {
        public int PatientVitalId { get; set; }
        public int UserId { get; set; }
        public DateTime ObservationDateTime { get; set; }
        
        [Required(ErrorMessage = "Weight is required")]
        public string Weight { get; set; }
        public bool WeightUnit { get; set; }
        public string Spirometer { get; set; }
        public string Temprature { get; set; }
        public bool TempratureUnit { get; set; }
        [Display(Name = "Oxygen Saturation (SpO2%)")]
        [Required(ErrorMessage = "spo2 is required")]
        public string Spo2 { get; set; }
        [Display(Name = "Pulse Rate (bpm)")]
        [Required(ErrorMessage = "PulseRate is required")]
        public string PulseRate { get; set; }
        [Display(Name = "Blood Glucose (mg/dl)")]
        [Required(ErrorMessage = "Bloodglucose is required")]
        public string Bloodglucose { get; set; }
        public string Sys { get; set; }
        public string Dia { get; set; }
        public List<NameIdModel> VitalHistory { get; set; }
    }
}
