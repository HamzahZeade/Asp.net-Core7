using Temp.DAL.Data;
using Temp.Models.Entities.Models;
using Temp.Models.Enums;

namespace Temp.DAL.Seeds
{
    public static class DefaultUsers
    {
        public static async Task SeedAsync(ApplicationDbContext dbContext)
        {
            await SeedUserAsync(dbContext, "basicuser@gmail.com", AppRolesEnum.Basic);
            await SeedUserAsync(dbContext, "superadmin@gmail.com", AppRolesEnum.SuperAdmin);
            await SeedUserAsync(dbContext, "admin@gmail.com", AppRolesEnum.Admin);
        }

        private static async Task SeedUserAsync(ApplicationDbContext context, string userEmail, AppRolesEnum appRoleEnum)
        {
            var roleName = appRoleEnum.ToString();

            var defaultUser = new User
            {
                UserName = userEmail,
                Email = userEmail,
                Password = "123Pa$$word!", // Use a secure method to store password hashes in production
                IsActive = true,
                EnglishName = userEmail,
                ArabicName = userEmail
            };

            if (context.Users.Any(u => u.Email == defaultUser.Email))
            {
                return; // User already exists, no need to seed
            }

            context.Users.Add(defaultUser);
            var role = context.AppRoles.SingleOrDefault(r => r.EnglishName == roleName);
            if (role != null)
            {
                context.UserRoles.Add(new UserRole { UserId = defaultUser.Id, RoleId = role.Id });
            }
            await context.SaveChangesAsync();


        }


    }

}

