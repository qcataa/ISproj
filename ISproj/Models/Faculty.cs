﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ISproj.Models
{
    public class Faculty
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
