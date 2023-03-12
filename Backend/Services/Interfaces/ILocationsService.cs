using Backend.ResponsesModels;

namespace Backend.Services.Interfaces
{
    public interface ILocationsService
    {
        Task<AllLocationsResponse> GetAllLocations();
    }
}
