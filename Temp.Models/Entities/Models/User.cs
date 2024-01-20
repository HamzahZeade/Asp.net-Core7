using System.ComponentModel.DataAnnotations;
using Temp.Models.Entities.GenericEntities;

namespace Temp.Models.Entities.Models
{
    public class User : _AuditEntityBase<Guid>
    {
        [Required]
        public string UserName { get { return _userName; } set { _userName = value?.ToLower().TrimStart(); } }
        [Required]
        public string Email { get { return _email; } set { _email = value?.ToLower().TrimStart(); } }

        public string EnglishName { get; set; }
        [Required]
        public string ArabicName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; } = true;

        #region Navigation Properties
        public List<UserRole> UserRoles { get; set; }
        #endregion  Navigation Properties
        #region Fields
        private string _userName;
        private string _email;
        #endregion Fields
    }


}
