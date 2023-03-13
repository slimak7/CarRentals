using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DBLogic.DBModels
{
    [Table("CarsModels")]
    public class CarModel
    {
        [Key]
        public Guid CarModelID { get; set; }
        public string? ModelName { get; set; }
        public int ModelRange { get; set; }
        public float Acceleration { get; set; }
        public int MaxNumberOfSeats { get; set; }
        public float PricePerDay { get; set; }
    }
}
