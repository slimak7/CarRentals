namespace Backend.ResponsesModels
{
    public class AvailableCarsResponse : BaseResponse
    {
        public List<CarResponse> cars { get; set; }

        public AvailableCarsResponse(List<CarResponse> cars) : base(null, true)
        {
            this.cars = cars;
        }
    }
}
