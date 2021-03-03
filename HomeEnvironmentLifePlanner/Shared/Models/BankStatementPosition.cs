using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace HomeEnvironmentLifePlanner.Shared.Models
{
    public class BankStatementPosition
    {
        [Key]
        public int BsP_Id { get; set; }
        public int BsP_BSHID { get; set; }
        public DateTime BsP_ExecutionDate { get; set; }
        public decimal BsP_Amount { get; set; }
        public int BsP_CURID { get; set; }
        public string BsP_SenderReceiver { get; set; }
        public string BsP_Description { get; set; }
        public string BsP_TransactionType { get; set; }
        public bool BsP_IsImported { get; set; }
        public DateTime BsP_ImportDate { get; set; }
        public int? BsP_RecommendedContractorId { get; set; }
        public int? BsP_RecommendedAccountId { get; set; }

        [ForeignKey("BsP_BSHID")]
        [JsonIgnore]
        public BankStatementHeader BankStatmentHeader { get; set; }
        [ForeignKey("BsP_CURID")]
        public Currency Currency { get; set; }
        [ForeignKey("BsP_RecommendedContractorId")]
        public virtual Contractor RecommendedContractor { get; set; }
        [ForeignKey("BsP_RecommendedAccountId")]
        public virtual  Account RecommendedAccount { get; set; }
        public ICollection<BankStatementSubPosition> BankStatementSubPositions { get; set; }

    }
}
