using Temp.DAL.Data;
using Temp.Models.Entities.Models;
using Temp.Models.Enums;

namespace Temp.DAL.Seeds
{
    public class DefaultRolePermissions
    {
        public static async Task SeedAsync(ApplicationDbContext dbContext)
        {
            await SeedRolePermissions(dbContext, AppRolesEnum.SuperAdmin, PermissionsEnum.View);
        }


        private async static Task SeedRolePermissions(ApplicationDbContext context, AppRolesEnum superAdmin, PermissionsEnum module)
        {
            if (!context.RolePermissions.Any())
            {
                var adminRole = context.AppRoles.SingleOrDefault(r => r.EnglishName == AppRolesEnum.SuperAdmin.ToString());
                var canViewPermission = context.Permissions.SingleOrDefault(p => p.EnglishName == "Permissions.Spekers.View");

                if (adminRole != null && canViewPermission != null)
                {
                    context.RolePermissions.AddRange(
                    new RolePermission { RoleId = adminRole.Id, PermissionId = canViewPermission.Id }
                    // Add more role-permission relationships as needed
                    );

                    await context.SaveChangesAsync();
                }
            }
        }

    }
}
