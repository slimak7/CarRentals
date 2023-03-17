namespace Backend.ResponsesModels
{
    public class ReservationResponse
    {
        public string reservationID { get; set; }
        public string dateFrom { get; set; }
        public string dateTo { get; set; }
        public bool isPickedUp { get; set; }
        public string carModelName { get; set; }
        public float totalCost { get; set; }
        public string locationInfo { get; set; }
        
    }
}
