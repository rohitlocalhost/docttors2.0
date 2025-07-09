using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docttors_portal.Entities.Classes
{
    [Table("Payment")]
    public class Payment: BaseClass
    {
        [Key]
        public int PaymentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string PaymentType { get; set; } 
        public decimal Amount { get; set; }
        public string CardId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PaypalTrasactionId { get; set; }
        public string PaymentTransactionId { get; set;}
        public string RefundTransId { get; set; }
        public string RefundStatus { get; set;}
        public string CardExpDate { get; set;}
        public int VisitId { get; set; }
        public int CardTypeId { get; set; }
        public int CardExpMonthId { get; set; }
        public int CardExpYearId { get; set; }
    }
}
