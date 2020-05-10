namespace LagunaLink.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterNewCompanyViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
