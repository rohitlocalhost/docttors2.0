using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("OnlineVisitStep6")]
    public class OnlineVisitStep6 : BaseClass
    {
        [Key]
        public int VisitId { get; set; }
        public int PatientId { get; set; }
        public int doctorId { get; set; }
        public int step1Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string Zip { get; set; }
    }
}
