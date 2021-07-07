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
    public class ShoppingListHeader
    {
        [Key]
        public int SlH_Id { get; set; }

        public int SlH_Number { get; set; }
        public int SlH_Month { get; set; }
        public int SlH_Year { get; set; }
        public DateTime SlH_Date { get; set; }
        public int? SlH_BSPID { get; set; }
        [ForeignKey("SlH_BSPID")]
        public virtual BankStatementPosition BankStatmentPosition { get; set; }
        public ICollection<ShoppingListPosition> ShoppingListPositions { get; set; }

    }
    public class ShoppingListPosition
    {
        [Key]
        public int SlP_Id { get; set; }
        public int SlP_SLHID { get; set; }

        public int SlP_PRDID { get; set; }
        public decimal SlP_AssumedQuantity { get; set; }
        public decimal SlP_RealizedQuantity { get; set; }
        public decimal? SlP_Amount { get; set; }
        public int? SlP_BSSID { get; set; }


        [ForeignKey("SlP_BSSID")]
        public virtual BankStatementSubPosition BankStatementSubPosition { get; set; }
        [ForeignKey("SlP_SLHID"), JsonIgnore]
        public ShoppingListHeader ShoppingListHeader { get; set; }

    }
}
