using Temp.DAL.Data;
using Temp.DAL.StaticClass;
using Temp.Models.Entities.Models;
using Temp.Models.Enums;

namespace Temp.DAL.Seeds
{
    public static class DefaulPermissions
    {
        public static async Task SeedAsync(ApplicationDbContext dbContext)
        {
            await SeedPermissions(dbContext, AppRolesEnum.SuperAdmin, "Spekers");
        }

        private async static Task SeedPermissions(ApplicationDbContext dbContext, AppRolesEnum superAdmin, string module)
        {
            if (!dbContext.Permissions.Any())
            {
                var allPermissions = Permissions.GeneratePermissionsForModule(module);

                foreach (var permission in allPermissions)
                {
                    dbContext.Permissions.Add(new Permission { EnglishName = permission, ArabicName = permission });
                    dbContext.SaveChanges();
                }
            }
        }
    }
}
