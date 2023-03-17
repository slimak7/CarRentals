using Backend.ResponsesModels;

namespace Backend.Services.Interfaces
{
    public interface IReservationsService
    {
        Task<BaseResponse> MakeReservation(Guid ClientID, Guid CarModelID, Guid LocationID, DateTime DateFrom, DateTime DateTo);

        Task<ReservationsResponse> GetUserReservations(Guid userID);
    }
}
