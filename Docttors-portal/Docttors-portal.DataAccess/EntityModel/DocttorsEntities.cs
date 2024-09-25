using Docttors_portal.DataAccess.Interfaces;
using Docttors_portal.Entities.Classes;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Docttors_portal.DataAccess.EntityModel
{
    public class DocttorsEntities : DbContext, IDbContext
    {
        public DocttorsEntities() : base("name=DocttorsEntities")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DocttorsEntities>(null);
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<SpecialistInsurance> SpecialistInsurances { get; set; }
        public DbSet<DoctorSpecialty> DoctorSpecialties { get; set; }
        public DbSet<PatientPersonalNew> PatientPersonals { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<TypeCategory> TypeCategories { get; set; }
        public DbSet<PatientPhysicianDetailsNew> PatientPhysicianDetails { get; set; }
        public DbSet<PatientInsuranceNew> PatientInsuranceNew { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<PatientEmergencyNew> PatientEmergencyNew { get; set; }
        public DbSet<PatientObservationNew> PatientObservationNew { get; set; }
        public DbSet<PatientAllergiesNew> patientAllergiesNew { get; set; }
        public DbSet<PatientHospitalDetailsNew> patientHospitalDetailsNews { get; set; }
        public DbSet<PatientPharmacyDetailsNew> patientPharmacyDetailsNews { get; set; }
        public DbSet<PatientVitalsNew> patientVitalsNews { get; set; }
        public DbSet<PatientMedicationDetailsNew> PatientMedicationDetailsNews { get; set; }
    }
}
