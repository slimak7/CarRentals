using Backend.ResponsesModels;

namespace Backend.Services.Interfaces
{
    public interface ICarsService
    {
        Task<AvailableCarsResponse> GetAvailableCars(Guid locationID, DateTime fromDate, DateTime toDate);
    }
}
