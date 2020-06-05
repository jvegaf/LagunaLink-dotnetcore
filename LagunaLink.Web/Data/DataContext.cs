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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=lagunalink.db");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<LagunaLink.Web.Models.RegisterNewStudentViewModel> RegisterNewStudentViewModel { get; set; }

        public DbSet<LagunaLink.Web.Models.RegisterNewCompanyViewModel> RegisterNewCompanyViewModel { get; set; }
    }
}
