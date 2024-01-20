using Temp.DAL.Data;
using Temp.DAL.StaticClass;

namespace Temp.DAL.Seeds
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext dbContext)
        {
            await SeedRoles(dbContext);
            await SeedUsers(dbContext);
            await SeedPermissions(dbContext);
            await SeedRolePermissions(dbContext);
        }
        private static async Task SeedUsers(ApplicationDbContext context)
        {
            await DefaultUsers.SeedAsync(context);
        }
        private static async Task SeedRoles(ApplicationDbContext context)
        {
            await DefaultRoles.SeedAsync(context);
        }
        private static async Task SeedPermissions(ApplicationDbContext context)
        {
            await DefaulPermissions.SeedAsync(context);
        }
        private static async Task SeedRolePermissions(ApplicationDbContext context)
        {
            await DefaultRolePermissions.SeedAsync(context);

        }

    }

}
