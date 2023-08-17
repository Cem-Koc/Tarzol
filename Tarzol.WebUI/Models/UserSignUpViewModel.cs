using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tarzol.WebUI.Models
{
    public class UserSignUpViewModel
    {
        [Display(Name ="İsim")]
        [Required(ErrorMessage ="Lütfen İsminizi Giriniz")]
        public string FirstName { get; set; }

        [Display(Name = "Soyisim")]
        [Required(ErrorMessage = "Lütfen Soyisminizi Giriniz")]
        public string LastName { get; set; }

        [Display(Name = "Mail")]
        [Required(ErrorMessage = "Lütfen Mail Adresinizi Giriniz")]
        public string Email { get; set; }

        [Display(Name = "Telefon Numarası")]
        [Required(ErrorMessage = "Lütfen Telefon Numaranızı Giriniz")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Şifre")]
        [Required(ErrorMessage = "Lütfen Şifrenizi Giriniz")]
        public string Password { get; set; }

        [Display(Name = "Şifre Tekrar")]
        [Required(ErrorMessage = "Lütfen Şifrenizi Tekrar Giriniz")]
        public string ComfirmPassword { get; set; }

        [Display(Name = "Doğum Tarihi")]
        [Required(ErrorMessage = "Lütfen Doğum Tarihinizi Giriniz")]
        public DateTime BirthDate { get; set; }

       
    }
}
