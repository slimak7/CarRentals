using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.DBLogic.DBModels
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid UserID { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Guid UserTypeIDFK { get; set; }

        [ForeignKey("UserTypeIDFK")]
        public virtual UserType UserType { get; set; }
    }
}
