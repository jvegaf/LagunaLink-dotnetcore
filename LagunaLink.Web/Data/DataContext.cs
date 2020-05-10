using Microsoft.EntityFrameworkCore;
using LagunaLink.Web.Models;
namespace LagunaLink.Web.Data
{
    using LagunaLink.Web.Data.Entities;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    
    public class DataContext : IdentityDbContext<User>
    {

        public DbSet<Student> Students { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<LagunaLink.Web.Models.RegisterNewStudentViewModel> RegisterNewStudentViewModel { get; set; }

        public DbSet<LagunaLink.Web.Models.RegisterNewCompanyViewModel> RegisterNewCompanyViewModel { get; set; }
    }
}
