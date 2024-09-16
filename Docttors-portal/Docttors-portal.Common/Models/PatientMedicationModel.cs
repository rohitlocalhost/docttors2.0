using System;
using System.ComponentModel.DataAnnotations;

namespace Docttors_portal.Common.Models
{
    public  class PatientMedicationModel
    {
        public int PatientMedicationId { get; set; }
        public int UserId { get; set; }
        [Display(Name = "Medication Name")]
        [Required(ErrorMessage = "Medication Name is required")]
        public string MedicationName { get; set; }
        [Display(Name = "Dosage in MG")]
        public string Dosage { get; set; }
        public string Frequency { get; set; }
        [Display(Name = "Physician Prescribed")]
        public string PrescibedPhysician { get; set; }
        [Display(Name = "Prescription #")]
        public string Prescription { get; set; }
        [Display(Name = "Reason for Prescription")]
        public string ReasonforPrescription { get; set; }
        [Display(Name = "Current Medication")]
        public bool CurrentMedication { get; set; }
        public bool IsNone { get; set; }
        [Display(Name = "Date of Prescription")]
        public DateTime DateOfPrescription { get; set; }
    }
}
