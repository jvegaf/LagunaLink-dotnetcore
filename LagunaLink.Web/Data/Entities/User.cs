namespace LagunaLink.Web.Data.Entities
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class User : IdentityUser
    {
        [Required]
        public bool Registered { get; set; }
        
        [Required]
        [Display(Name ="Laguna Role")]
        public int LagunaRole { get; set; }
    }
}
