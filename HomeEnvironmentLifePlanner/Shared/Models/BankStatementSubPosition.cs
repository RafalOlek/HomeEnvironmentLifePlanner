using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeEnvironmentLifePlanner.Shared.Models
{
    public class BankStatementSubPosition
    {
        [Key]
        public int BsS_Id { get; set; }
        public int BsS_BSPID { get; set; }
        public int? BsS_CATID { get; set; }
        public decimal BsS_Amount { get; set; }

        [ForeignKey("BsS_CATID")]
        public Category Category { get; set; }
        [ForeignKey("BsS_BSPID")]
        [JsonIgnore]

        public BankStatementPosition BankStatementPosition { get; set; }

    }
}
