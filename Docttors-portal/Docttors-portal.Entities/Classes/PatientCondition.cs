using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("PatientCondition")]
    public class PatientCondition : BaseClass
    {
        [Key]
        public int Id { get; set; }
        public int PatientId { get; set; }
        public string Condition { get; set; }

    }
}
