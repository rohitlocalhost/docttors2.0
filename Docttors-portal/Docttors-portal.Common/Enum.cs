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
        MaritalStatus = 3,
        Education = 4,
        Height = 5,
        Ethnicity = 6,
        Specialty = 7,
        ClinicalHistory = 8,
        GeneralCondition = 9,
        EndocrineorDiabetes = 10,
        Stomach = 11,
        Urinary = 12,
        Neurological = 13,
        Cardiovascular = 14,
        Respiratory = 15,
        Eyes = 16,
        Ear = 17,
        ObOrGyn = 18,
        MusclesOrJoints = 19,
        Skin = 20,
        CancerOrHematology = 21,
        Dental = 22,
        Psychological = 23,
        ComplaintType = 24,
        BloodPressure = 25,
        Breathing = 26,
        Weight = 27
    }

    public enum WeightEnum
    {
        [Description("lbs")]
        lbs = 1,
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

    public enum SearchType
    {
        SearchDoctor = 1,
        SearchHospital,
        SearchInsurance
    }
    public enum AppointmentSchedule
    {
        MorningScheduleId = 16,
        DayScheduleId = 32,
        EveningScheduleId = 48
    }

    public enum DoctorServiceType
    {
        [Description("Video Chat")]
        VideoChat =2082,
        [Description("Video Email")]
        VideoEmail =2083,
        [Description("Ask A Doctor")]
        AskADoctor =2084,
        [Description("Prescription Refill")]
        PrescriptionRefill =2085
    }
}
