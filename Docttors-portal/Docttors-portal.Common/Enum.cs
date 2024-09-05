using System.ComponentModel;

namespace Docttors_portal.Common
{
    public enum RoleEnum
    {
        Admin = 1,
        Patient,
        Doctor,
        Specialist,
        Center,
        FrontDesk,
        ForwardParty,
        AlliedHealthProf,
        DoctorNurse,
        DoctorBilling,
        DoctorLab,
        DoctorDiagnostics,
        DoctorReferal
    }

    public enum TypeCategory
    {
        [Description("Marital Status")]
        MaritalStatus = 1,
        [Description("Education Master")]
        EducationMaster = 2,
        Gender = 3,
    }
}
