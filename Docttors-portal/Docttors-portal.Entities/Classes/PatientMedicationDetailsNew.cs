using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Docttors_portal.Entities.Classes
{
    [Table("PatientMedicationDetailsNew")]
    public class PatientMedicationDetailsNew : BaseClass
    {
        [Key]
        public int PatientMedicationId { get; set; }
        public int UserId { get; set; }
        public string MedicationName { get; set; }
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        public string PrescibedPhysician { get; set; }
        public string Prescription { get; set; }
        public string ReasonforPrescription { get; set; }
        public bool CurrentMedication { get; set; }
        public DateTime DateOfPrescription { get; set; }        
    }
}
