using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ISproj.Models.ManageViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Parola curenta")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Parola trebuie sa contina intre {2} si {1} caractere.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Parola noua")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirma parola")]
        [Compare("NewPassword", ErrorMessage = "Confirmarea nu se potriveste.")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
