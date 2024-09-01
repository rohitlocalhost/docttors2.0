using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Common.Models
{
    public class PatientSearchModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //[DataType(DataType.Date)]
        public DateTime DOB { get; set; }
        public string MR { get; set; }
    }
}
