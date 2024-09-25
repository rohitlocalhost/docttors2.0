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
        [Description("User Type")]
        UserType = 1,
        Gender = 2,
        MaritalStatus=3,
        Education=4,
        Height=5,
        Ethnicity=6,
        Specialty = 7
    }

    public enum WeightEnum
    {
        [Description("lbs")]
        lbs=1,
        [Description("kg")]
        kg
    }
    public enum TempratureEnum
    {
        [Description("F")]
        Fahrenheit = 1,
        [Description("C")]
        Celsius
    }
}
