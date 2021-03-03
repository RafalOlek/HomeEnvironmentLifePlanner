using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeEnvironmentLifePlanner.Shared.Models

{
    public class CategoryType
    {
        [Key]
        public int CtY_Id { get; set; }
        public string CtY_Name { get; set; }
        public int CtY_Value { get; set; }
    }
    public enum CategoryTypeValue
    {
        Credit=1,
        Debit=2,
    }
}
