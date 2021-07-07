using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
namespace HomeEnvironmentLifePlanner.Shared.Models
{
    public class Product
    {
        [Key]
        public int PrD_Id { get; set; }
        public string PrD_Code { get; set; }
        public string PrD_Name { get; set; }
        public int PrD_PRGID { get; set; }
        public ICollection<ProductPrice> ProductPrices { get; set; }
        [ForeignKey("PrD_PRGID")]
        public ProductGroup ProductGroup { get; set; }
    }

    public class ProductGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int PrG_Id { get; set; }
        public string PrG_Code { get; set; }

        public string PrG_Name { get; set; }
        public int? PrG_ParentId { get; set; }
        [JsonIgnore]
        public ICollection<ProductGroup> PrG_Children { get; set; }
        public ProductGroup ProductGroup1 { get; set; }
    }
    public class ProductPrice
    {
        [Key]
        public int PrP_Id { get; set; }
        public int PrP_PRDID { get; set; }
        public decimal PrP_Price { get; set; }
        public int PrP_CTRID { get; set; }
        public DateTime PrP_Date { get; set; }
        [ForeignKey("PrP_CTRID")]
        public virtual Contractor Contractor { get; set; }
        [ForeignKey("PrP_PRDID"), JsonIgnore]
        public Product Product { get; set; }
    }

}
