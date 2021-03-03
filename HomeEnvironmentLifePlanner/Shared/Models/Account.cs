using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeEnvironmentLifePlanner.Shared.Models
{
    public class Account
    {
        [Key]
        public int AcC_Id { get; set; }
        public string AcC_ReferenceNumber { get; set; }
        public string AcC_Name { get; set; }

     //   public  ICollection<PaymentType> PaymentTypes { get; set; }
    }
}
