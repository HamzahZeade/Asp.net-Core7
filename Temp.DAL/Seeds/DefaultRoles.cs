using Microsoft.EntityFrameworkCore;
using Temp.DAL.Data;
using Temp.Models.Entities.Models;
using Temp.Models.Enums;

namespace Temp.DAL.StaticClass
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(ApplicationDbContext dbContext)
        {
            await SeedRoleAsync(dbContext, AppRolesEnum.SuperAdmin, "SuperAdmin", 1, "مشرف النظام", "System Administrator");
            await SeedRoleAsync(dbContext, AppRolesEnum.Admin, "Admin", 2, "مدير النظام", "Region Administrator");
            await SeedRoleAsync(dbContext, AppRolesEnum.Basic, "Basic", 3, "مستخدم عادي", "Region Leader");
        }

        private static async Task SeedRoleAsync(ApplicationDbContext dbContext, AppRolesEnum appRoleEnum, string key, int value, string arabicName, string englishName)
        {
            var roleName = appRoleEnum.ToString();
            // Check if the role already exists in the database
            var existingRole = await dbContext.AppRoles.SingleOrDefaultAsync(r => r.EnglishName == roleName);
            if (existingRole == null)
            {
                // Role does not exist, create a new one
                var newRole = new AppRole { EnglishName = roleName, Key = key, Value = value, ArabicName = arabicName, };
                dbContext.AppRoles.Add(newRole);
                await dbContext.SaveChangesAsync();
            }
        }
    }


}
