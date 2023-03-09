using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DBLogic.DBModels
{
    [Table("UsersTypes")]
    public class UserType
    {
        [Key] 
        public Guid UserTypeID { get; set; }
        public string? TypeName { get; set; }
    }
}
