using Microsoft.EntityFrameworkCore;
using Temp.Models.Entities.Models;

namespace Temp.DAL.Data
{
    public class ApplicationDbContext : AuditableContext
    {
        #region //////////////// Properties ////////////////
        #endregion //////////////// Properties ////////////////

        #region  //////////////// Constructors ////////////////
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        #endregion //////////////// Constructors ////////////////

        #region //////////////// DbSets ////////////////
        //FrameWork Entities
        public DbSet<User> Users { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }




        //Event Management Entities 

        #endregion //////////////// DbSets ////////////////

        #region //////////////// Methods ////////////////
        public void Migrate()
        {
            //Policy
            //    .Handle<Exception>()
            //    .WaitAndRetry(3, r => TimeSpan.FromSeconds(5))
            //    .Execute(() => Database.Migrate());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ApplyForeignKeyDeleteBehavior(modelBuilder);
        }

        #region Private Methods
        private void ApplyForeignKeyDeleteBehavior(ModelBuilder modelBuilder)
        {
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(x => x.GetForeignKeys()))
                foreignKey.DeleteBehavior = DeleteBehavior.ClientNoAction;
        }
        #endregion Private Methods

        #endregion  //////////////// Methods ////////////////
    }
}
