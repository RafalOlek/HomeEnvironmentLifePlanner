﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HomeEnvironmentLifePlanner.Shared.Models
{
    public class Currency
    {
        [Key]
        public int CuR_Id { get; set; }
        public string CuR_Name { get; set; }
    }
}
