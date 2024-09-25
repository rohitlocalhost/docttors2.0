using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("PatientObservationNew")]
    public class PatientObservationNew : BaseClass
    {
        [Key]
        public int PatientObservationId { get; set; }
        public int UserId { get; set; }
        public string ObservationNote { get; set; }
        public DateTime ObservationDate { get; set; }
        public TimeSpan ObservationTime { get; set; }
    }
}
