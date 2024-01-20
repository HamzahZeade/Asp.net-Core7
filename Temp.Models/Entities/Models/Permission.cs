using System.ComponentModel.DataAnnotations;

namespace Temp.Models.Entities.Models
{
    public class Permission
    {

        public Guid Id { get; set; }
        [MaxLength(250)]
        public string ArabicName { get; set; }
        [MaxLength(250)]
        public string EnglishName { get; set; }
    }
}
