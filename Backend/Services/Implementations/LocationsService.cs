using Backend.DBLogic.Repos.Locations;
using Backend.ResponsesModels;
using Backend.Services.Interfaces;

namespace Backend.Services.Implementations
{
    public class LocationsService : ILocationsService
    {
        ILocationsRepo _locationsRepo;

        public LocationsService(ILocationsRepo locationsRepo)
        {
            _locationsRepo = locationsRepo;
        }

        public async Task<AllLocationsResponse> GetAllLocations()
        {
            var locations = await _locationsRepo.GetAll();

            if (locations != null && locations.Count > 0)
            {
                List<LocationResponse> locationResponses = new List<LocationResponse>();

                foreach (var location in locations)
                {
                    locationResponses.Add(new LocationResponse()
                    {
                        locationID = location.LocationID.ToString(),
                        locationName = location.LocationName,
                        Street = location.Street,
                        City = location.City,
                        Zipcode = location.Zipcode
                    });
                }

                return new AllLocationsResponse(null, true, locationResponses);
            }
            else
            {
                throw new Exception("Can not load locations");
            }

        }
    }
}
