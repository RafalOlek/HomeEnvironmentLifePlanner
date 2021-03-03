using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

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
}
