using Microsoft.EntityFrameworkCore;

namespace LagunaLink.Web.Data
{
    public class DatabaseContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=LagunaLink.db");
        }
    }
}