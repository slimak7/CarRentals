namespace Backend.ResponsesModels
{
    public class ReservationsResponseStaff : BaseResponse
    {
        public List<ReservationResponseStaff> reservations { get; set; }
        public ReservationsResponseStaff(List<ReservationResponseStaff> reservations) : base(null, true)
        {
            this.reservations = reservations;
        }
    }
}
