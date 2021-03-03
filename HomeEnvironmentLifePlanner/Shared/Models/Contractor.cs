using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HomeEnvironmentLifePlanner.Shared.Models

{
    public class Contractor
    {
        [Key]
        public int CtR_Id { get; set; }
        public string CtR_Acronym { get; set; }
        public string CtR_Name { get; set; }
        public string CtR_Country { get; set; }
        public string CtR_City { get; set; }
        public string CtR_Street { get; set; }
        public string CtR_ReferenceNumber { get; set; }
        public int CtR_CTGID { get; set; }
        [ForeignKey("CtR_CTGID")]
        public  ContractorGroup ContractorGroup { get; set; }
    }
}
