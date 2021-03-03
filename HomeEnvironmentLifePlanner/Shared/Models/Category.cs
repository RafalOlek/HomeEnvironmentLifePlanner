using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
//Qwerty!12345
namespace HomeEnvironmentLifePlanner.Shared.Models
{
    public class Category
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity),Key]
        public int CaT_Id { get; set; }
        public string CaT_Name { get; set; }
        public string CaT_Description { get; set; }
        public string CaT_ReferenceNumber { get; set; }
        public int? CaT_ParentId { get; set; }
        public int CaT_CTYID { get; set; }
        [ForeignKey("CaT_CTYID")]
        public CategoryType CategoryType { get; set; }
        [JsonIgnore]
        public ICollection<Category> CaT_Children  { get; set; }
        public Category Category1 { get; set; }

    }
}
