namespace LagunaLink.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterNewStudentViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name ="Nombre")]
        public string Name { get; set; }

        [Required]
        [Display(Name ="Primer Apellido")]
        public string FirstSurname { get; set; }

        [Required]
        [Display(Name ="Segundo Apellido")]
        public string LastSurname { get; set; }

        [Display(Name ="Telefono")]
        [MaxLength(9)]
        [MinLength(9)]
        public string Phone { get; set; }
    }
}
