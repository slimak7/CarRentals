using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DBLogic.DBModels
{
    [Table("Cars")]
    public class Car
    {
        [Key]
        public string LicensePlateID { get; set; }
        public string? Color { get; set; }
        public Guid ModelIDFK { get; set; }
        public Guid? LocationIDFK { get; set; }

        [ForeignKey(nameof(ModelIDFK))]
        public virtual CarModel Model { get; set; }

        [ForeignKey(nameof(LocationIDFK))]
        public virtual Location Location { get; set; }
    }
}
