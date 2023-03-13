namespace Backend.ResponsesModels
{
    public class CarResponse
    {
        public string carModelID { get; set; }
        public string modelName { get; set; }
        public int modelRange { get; set; }
        public float acceleration { get; set; }
        public int maxNumberOfSeats { get; set; }
        public float pricePerDay { get; set; }
        
    }
}
