using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Backend.DBLogic.DBModels
{
    [Table("Locations")]
    public class Location
    {
        [Key]
        public Guid LocationID { get; set; }
        public string? LocationName { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? Zipcode { get; set; }
    }
}
