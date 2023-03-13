using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DBLogic.DBModels
{
    [Table("Reservations")]
    public class Reservation
    {
        [Key]
        public Guid ReservationID { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool isPickedUp { get; set; }
        public Guid UserIDFK { get; set; }
        public string CarIDFK { get; set; }

        [ForeignKey(nameof(UserIDFK))]
        public virtual User User { get; set; }

        [ForeignKey(nameof(CarIDFK))]
        public virtual Car Car { get; set; }
    }
}
