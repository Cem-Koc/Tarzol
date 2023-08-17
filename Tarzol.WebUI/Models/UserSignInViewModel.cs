using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tarzol.WebUI.Models
{
    public class UserSignInViewModel
    {
        [Required(ErrorMessage ="Lütfen mail adresinizi giriniz")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Lütfen şifrenizi giriniz")]
        public string Password { get; set; }
    }
}
