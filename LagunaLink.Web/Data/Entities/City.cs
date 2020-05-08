namespace LagunaLink.Web.Data.Entities
{
    public class City : IEntity
    {
        public int Id { get; set; }
        public int ProvinceId { get; set; }
        public string Name { get; set; }

    }
}
