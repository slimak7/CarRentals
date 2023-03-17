using System.ComponentModel.DataAnnotations;

namespace Backend.RequestsModels
{
    public class ReservationRequest
    {
        [Required]
        public Guid ClientID { get; set; }

        [Required]
        public Guid CarModelID { get; set; }

        [Required]
        public Guid LocationID { get; set; }

        [Required]
        public DateTime DateFrom { get; set; }

        [Required]
        public DateTime DateTo { get; set; }
    }
}
