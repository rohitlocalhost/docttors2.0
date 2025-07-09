using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Docttors_portal.Common.Models
{
    public class PatientObservationModel
    {
        public int PatientObservationId { get; set; }
        public int UserId { get; set; }
        [Display(Name = "Observation Note")]
        [Required(ErrorMessage = "Observation Note is required")]
        public string ObservationNote { get; set; }
        [Display(Name = "Observation Date")]
        [Required(ErrorMessage = "Observation Date is required")]
        public string ObservationDate { get; set; }
        [Display(Name = "Observation Time")]
        [Required(ErrorMessage = "Observation Time is required")]
        public string ObservationTime { get; set; }

        public List<PatientObservationModel> ObservationData { get; set; }
    }
}
