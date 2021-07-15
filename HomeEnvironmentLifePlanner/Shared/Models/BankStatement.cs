using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomeEnvironmentLifePlanner.Shared.Models
{
    public class BankStatementHeader
    {
        [Key]
        public int BsH_Id { get; set; }
        public string BsH_Name { get; set; }
        public DateTime BsH_CreateDate { get; set; }
        public DateTime BsH_DateFrom { get; set; }
        public DateTime BsH_DateTo { get; set; }
        public ICollection<BankStatementPosition> BankStatementPositions { get; set; }
    }
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
        public bool BsP_IsImportedToTransactions { get; set; }
        public bool Bsp_IsPreparedToImport { get; set; }
        public DateTime BsP_ImportDate { get; set; }
        public int? BsP_RecommendedContractorId { get; set; }
        public int? BsP_RecommendedAccountId { get; set; }

        [ForeignKey("BsP_BSHID")]
        [JsonIgnore]
        public BankStatementHeader BankStatmentHeader { get; set; }
        [ForeignKey("BsP_CURID")]
        public Currency Currency { get; set; }
        [JsonIgnore]
        [ForeignKey("BsP_RecommendedContractorId")]
        public virtual Contractor RecommendedContractor { get; set; }
        [ForeignKey("BsP_RecommendedAccountId")]
        public virtual Account RecommendedAccount { get; set; }
        public ICollection<BankStatementSubPosition> BankStatementSubPositions { get; set; }
    }
    public class BankStatementSubPosition
    {
        [Key]
        public int BsS_Id { get; set; }
        public int BsS_BSPID { get; set; }
        public int? BsS_CATID { get; set; }
        public decimal BsS_Amount { get; set; }

        public string BsS_Description { get; set; }
        [ForeignKey("BsS_CATID")]
        public Category Category { get; set; }
        [ForeignKey("BsS_BSPID")]
        [JsonIgnore]
        public BankStatementPosition BankStatementPosition { get; set; }
    }
}
