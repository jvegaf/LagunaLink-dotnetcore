namespace LagunaLink.Web.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Student : IEntity
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public string FirstSurname { get; set; }
        
        [Required]
        public string LastSurname { get; set; }
        
        public string Phone { get; set; }
    }
}
