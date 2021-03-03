using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HomeEnvironmentLifePlanner.Shared.Models

{
    public class ContractorGroup
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int CtG_Id { get; set; }
        public string CtG_Name { get; set; }
        public int? CtG_ParentId { get; set; }

        [JsonIgnore]
        public ICollection<ContractorGroup> CtG_Children { get; set; }
        public ContractorGroup ContractorGroup1 { get; set; }
    }
}
