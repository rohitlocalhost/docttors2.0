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
        public DbSet<SpecialistInsurance> specialistInsurances { get; set; }
        public DbSet<DoctorSpecialty> doctorSpecialties { get; set; }
        public DbSet<PatientPersonalNew> patientPersonals { get; set; }
        public DbSet<Country> countries { get; set; }
        public DbSet<Type> types { get; set; }
        public DbSet<TypeCategory> typeCategories { get; set; }
        public DbSet<PatientPhysicianDetailsNew> patientPhysicianDetails { get; set; }
        public DbSet<PatientInsuranceNew> patientInsuranceNew { get; set; }
        public DbSet<PatientAllergiesNew> patientAllergiesNew { get; set; }
        public DbSet<PatientHospitalDetailsNew> patientHospitalDetailsNews { get; set; }
        public DbSet<PatientPharmacyDetailsNew> patientPharmacyDetailsNews { get; set; }
    }
}
