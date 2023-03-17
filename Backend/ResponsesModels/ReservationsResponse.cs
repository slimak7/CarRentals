namespace Backend.ResponsesModels
{
    public class ReservationsResponse : BaseResponse
    {
        public List<ReservationResponse> reservations { get; set; }
        public ReservationsResponse(List<ReservationResponse> reservations) : base(null, true)
        {
            this.reservations = reservations;
        }
    }
}
