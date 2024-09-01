using Docttors_portal.Common.Models;
using Docttors_portal.Common.procedureModels;
using Docttors_portal.DataAccess.Interfaces;
using Docttors_portal.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Services.Classes
{
    public class DoctorServices : IDoctorServices
    {
        private IUnitOfWork _unitOfWork;
        public DoctorServices(IUnitOfWork unitOfWork)
        {
            if (unitOfWork != null)
            {
                _unitOfWork = unitOfWork;
            }
        }
        public List<GetDoctorPatients> GetPatientByDoctor(PatientSearchModel patientSearchModel)
        {
            var patientList = new List<GetDoctorPatients>();
            string connectionString = ConfigurationManager.ConnectionStrings["DocttorsEntities"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetDoctorPatients", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@FirstName", SqlDbType.VarChar).Value = patientSearchModel.FirstName;
                    cmd.Parameters.Add("@LastName", SqlDbType.VarChar).Value = patientSearchModel.LastName;
                    cmd.Parameters.Add("@Mrn", SqlDbType.VarChar).Value = patientSearchModel.MR;
                    cmd.Parameters.Add("@DoctorId", SqlDbType.VarChar).Value = 2;
                    cmd.Parameters.Add("@DateOfBirth", SqlDbType.VarChar).Value = patientSearchModel.DOB;
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        //Accessing the data using the string key as index
                        GetDoctorPatients getDoctorPatients = new GetDoctorPatients()
                        {
                            patientId = Convert.ToInt32(rdr["PatientId"]),
                            FirstName=Convert.ToString(rdr["FirstName"]),
                            LastName = Convert.ToString(rdr["LastName"]),
                            EmailAddress = Convert.ToString(rdr["EmailAddress"]),
                            UserId = Convert.ToInt32(rdr["LoginId"]),
                            Mrn = Convert.ToString(rdr["MRN"]),
                            CellPhone = Convert.ToString(rdr["CellPhone"]),
                            DOB= Convert.ToDateTime(rdr["DOB"]),
                            CCMIsPatientEligible= Convert.ToString(rdr["CCMIsPatientEligible"]),
                            CCMIsConsentProvided= Convert.ToString(rdr["CCMConsentProvided"]),
                            FavoriteDoctors= Convert.ToString(rdr["FavoriteDoctors"])
                        };
                        patientList.Add(getDoctorPatients);
                    }

                }
            }
            return patientList;
        }
    }
}
