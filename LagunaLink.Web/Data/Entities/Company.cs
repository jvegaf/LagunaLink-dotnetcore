namespace LagunaLink.Web.Data.Entities
{
    public class Company :IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        //TODO: City prop
    }
}
