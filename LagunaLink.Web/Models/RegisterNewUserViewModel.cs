namespace LagunaLink.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterNewUserViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="Correo Electronico")]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string Confirm { get; set; }

        [Required]
        public int LagunaRole { get; set; }
    }
}
