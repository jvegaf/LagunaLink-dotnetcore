namespace LagunaLink.Web.Data
{

    using LagunaLink.Web.Data.Entities;
    using LagunaLink.Web.Helpers;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Threading.Tasks;
    
    public class SeedDB
    {
        private readonly DataContext context;
        private readonly IUserHelper userHelper;
        private Random random;

        public SeedDB(DataContext context, IUserHelper userHelper)
        {
            this.context = context;
            this.userHelper = userHelper;
            this.random = new Random();
        }

        public async Task SeedAsync()
        {
            await this.context.Database.EnsureCreatedAsync();

            await this.userHelper.CheckRoleAsync("Student");
            await this.userHelper.CheckRoleAsync("Company");
            await this.userHelper.CheckRoleAsync("Admin");

            var user = await this.userHelper.GetUserByEmailAsync("josevega234@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    Email = "josevega234@me.com",
                    UserName = "josevega234@me.com",
                    LagunaRole = 1,
                    Registered = false
                };

                var result = await this.userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }

            var isInRole = await this.userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await this.userHelper.AddUserToRoleAsync(user, "Admin");
            }

            var token = await this.userHelper.GenerateEmailConfirmationTokenAsync(user);
            await this.userHelper.ConfirmEmailAsync(user, token);
        }

    }
}
