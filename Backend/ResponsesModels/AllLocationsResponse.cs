namespace Backend.ResponsesModels
{
    public class AllLocationsResponse : BaseResponse
    {
        public List<LocationResponse> locations { get; set; }
        public AllLocationsResponse(List<string> errors, bool success, List<LocationResponse> locations) : base(errors, success)
        {
            this.locations = locations;
        }
    }
}
