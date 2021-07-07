using HomeEnvironmentLifePlanner.Shared.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace HomeEnvironmentLifePlanner.Shared.Models
{
    public class TransactionHeader
    {
        [Key]
        public int TrH_Id { get; set; }
        [RequiredGreaterThanZero(ErrorMessage ="Należy wybrać wartość")]
        public int? TrH_BSPID { get; set; }
        public string TrH_Description { get; set; }
        [RequiredGreaterThanZero(ErrorMessage = "Należy wybrać wartość")]
        public int TrH_CTRID { get; set; }
        //public decimal TrN_Amount { get; set; }
        [RequiredGreaterThanZero(ErrorMessage = "Należy wybrać wartość")]
        public int TrH_CURID { get; set; }
        public DateTime TrH_CreateDate { get; set; }
        public DateTime TrH_ExecutionDate { get; set; }

        [ForeignKey("TrH_CURID")]
        public virtual Currency Currency { get; set; }
        [ForeignKey("TrH_CTRID")]
        public virtual Contractor Contractor { get; set; }
        [ForeignKey("TrH_BSPID")]
        public virtual BankStatementPosition BankStatmentPosition { get; set; }
 
        public ICollection<TransactionPosition> TransactionPositions { get; set; }
    }
    public class TransactionPosition
    {
        [Key]
        public int TrP_Id { get; set; }
        public int TrP_TRHID { get; set; }
        public decimal? TrP_Price { get; set; }
        public decimal TrP_Amount { get; set;}
        public decimal? TrP_Quantity { get; set; }
        public int? TrP_PRDID { get; set; }
        public int? TrP_Unit { get; set; }
        public int? TrP_BSSID { get; set; }
        [RequiredGreaterThanZero(ErrorMessage = "Należy wybrać wartość")]
        public int? TrP_CATID { get; set; }

        [ForeignKey("TrP_BSSID")]
        public virtual BankStatementSubPosition BankStatementSubPosition { get; set; }
        [ForeignKey("TrP_CATID")]
        public virtual Category Category { get; set; }
        [ForeignKey("TrP_TRHID"),JsonIgnore]
        public TransactionHeader TransactionHeader { get; set; }

    }

}
