using HomeEnvironmentLifePlanner.Shared.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeEnvironmentLifePlanner.Shared.Models
{
    public class Transaction
    {
        [Key]
        public int TrN_Id { get; set; }
        [RequiredGreaterThanZero(ErrorMessage ="Należy wybrać wartość")]
        public int TrN_PYTID { get; set; }
        public int? TrN_BSPID { get; set; }
        public string TrN_Description { get; set; }
        [RequiredGreaterThanZero(ErrorMessage = "Należy wybrać wartość")]
        public int TrN_CTRID { get; set; }
        public decimal TrN_Amount { get; set; }
        [RequiredGreaterThanZero(ErrorMessage = "Należy wybrać wartość")]
        public int TrN_CURID { get; set; }
        public DateTime TrN_CreateDate { get; set; }
        public DateTime TrN_ExecutionDate { get; set; }

        [ForeignKey("TrN_CURID")]
        public  Currency Currency { get; set; }
        [ForeignKey("TrN_CTRID")]
        public  Contractor Contractor { get; set; }
        [ForeignKey("TrN_BSPID")]
        public  BankStatementPosition BankStatmentPosition { get; set; }
        [ForeignKey("TrN_PYTID")]
        public  PaymentType PaymentType { get; set; }
        
    }
}
