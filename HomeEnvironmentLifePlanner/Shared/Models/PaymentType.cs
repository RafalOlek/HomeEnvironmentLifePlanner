using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Threading.Tasks;

namespace HomeEnvironmentLifePlanner.Shared.Models
{
    public class PaymentType
    {
        [Key]
        public int PyT_Id { get; set; }
        public string PyT_Name { get; set; }
        public string PyT_ReferenceNumber { get; set; }
        public int PyT_ACCID { get; set; }
        [ForeignKey("PyT_ACCID")]
        public  Account Account { get; set; }
    }
}
