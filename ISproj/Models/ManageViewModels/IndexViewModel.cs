﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISproj.Models.ManageViewModels
{
    public class IndexViewModel
    {
        [Display(Name = "Utilizator")]
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Numar de telefon")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }
    }
}
