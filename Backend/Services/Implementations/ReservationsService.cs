using Backend.DBLogic.DBModels;
using Backend.DBLogic.Repos.Cars;
using Backend.DBLogic.Repos.Reservations;
using Backend.Exceptions;
using Backend.ResponsesModels;
using Backend.Services.Interfaces;
using Itenso.TimePeriod;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services.Implementations
{
    public class ReservationsService : IReservationsService
    {
        ICarsRepo _carsRepo;
        IReservationsRepo _reservationsRepo;

        public ReservationsService(ICarsRepo carsRepo, IReservationsRepo reservationsRepo)
        {
            _carsRepo = carsRepo;
            _reservationsRepo = reservationsRepo;
        }

        public async Task<ReservationsResponse> GetUserReservations(Guid userID)
        {
            var reservations = await _reservationsRepo.GetAllByCondition(x => x.UserIDFK == userID);

            if (reservations.IsNullOrEmpty())
            {
                throw new GetException("There are no reservations");
            }

            List<ReservationResponse> reservationResponses = new List<ReservationResponse>();

            foreach (var reservation in reservations)
            {
                TimeRange timeRange = new TimeRange(reservation.DateFrom, reservation.DateTo);

                reservationResponses.Add(new ReservationResponse()
                {
                    reservationID = reservation.ReservationID.ToString(),
                    dateFrom = reservation.DateFrom.ToString("dd-MM-yyyy"),
                    dateTo = reservation.DateTo.ToString("dd-MM-yyyy"),
                    carModelName = reservation.Car.Model.ModelName,
                    isPickedUp = reservation.isPickedUp,
                    locationInfo = reservation.Location.LocationName + ", " + 
                    reservation.Location.Street + ", " + reservation.Location.Zipcode + ", " + reservation.Location.City,
                    totalCost = (timeRange.Duration.Days + 1) * reservation.Car.Model.PricePerDay
                });
            }

            return new ReservationsResponse(reservationResponses);
        }

        public async Task<ReservationsResponseStaff> GetUsersReservationsStaff(int fromNumber, int toNumber)
        {
            var reservations = await _reservationsRepo.GetAll();

            if (reservations.IsNullOrEmpty())
            {
                throw new GetException("There are no reservations in database");
            }

            if (fromNumber > reservations.Count() - 1)
            {
                throw new GetException("No more entries");
            }

            int count = toNumber - fromNumber + 1;
            
            if (count > reservations.Count() - fromNumber)
            {
                count = reservations.Count() - fromNumber;
            }

            var selected = reservations.GetRange(fromNumber, count);

            List<ReservationResponseStaff> reservationResponses = new List<ReservationResponseStaff>();

            foreach (var reservation in selected)
            {
                TimeRange timeRange = new TimeRange(reservation.DateFrom, reservation.DateTo);
                reservationResponses.Add(new ReservationResponseStaff()
                {
                    reservationID = reservation.ReservationID.ToString(),
                    dateFrom = reservation.DateFrom.ToString("dd-MM-yyyy"),
                    dateTo = reservation.DateTo.ToString("dd-MM-yyyy"),
                    carModelName = reservation.Car.Model.ModelName,
                    isPickedUp = reservation.isPickedUp,
                    locationInfo = reservation.Location.LocationName + ", " +
                    reservation.Location.Street + ", " + reservation.Location.Zipcode + ", " + reservation.Location.City,
                    totalCost = (timeRange.Duration.Days + 1) * reservation.Car.Model.PricePerDay,
                    ClientID = reservation.User.UserID.ToString(),
                    ClientName = reservation.User.FirstName + " " + reservation.User.LastName
                });
            }

            return new ReservationsResponseStaff(reservationResponses);
        }

        public async Task<BaseResponse> MakeReservation(Guid ClientID, Guid CarModelID, Guid LocationID, DateTime DateFrom, DateTime DateTo)
        {
            bool limitToGivenLocation = DateFrom.Date == DateTime.Now.AddDays(1).Date;
            TimeRange givenTimeRange = new TimeRange(DateFrom.AddDays(-1), DateTo.AddDays(1));

            var reservations = await _reservationsRepo.GetAllByCondition(x =>
            {
                TimeRange reservationTimeRange = new TimeRange(x.DateFrom, x.DateTo);

                return (limitToGivenLocation ? x.Car.LocationIDFK == LocationID : true) && x.Car.ModelIDFK == CarModelID
                    && !givenTimeRange.IntersectsWith(reservationTimeRange);

            });

            if (!reservations.IsNullOrEmpty())
            {
                var car = reservations[0].Car;

                await _reservationsRepo.Add(new Reservation()
                {
                    UserIDFK = ClientID,
                    CarIDFK = car.LicensePlateID,
                    DateFrom = DateFrom,
                    DateTo = DateTo,
                    LocationIDFK = LocationID                        
                });

                return new BaseResponse(null, true);
            }
            else
            {
                var cars = await _carsRepo.GetAllByCondition(x =>
                {
                    return (limitToGivenLocation? x.LocationIDFK == LocationID : true) && x.ModelIDFK == CarModelID;
                });

                if (cars.IsNullOrEmpty())
                {
                    throw new PostException("Selected model is unavailable in this location");
                }

                var carReservations = await _reservationsRepo.GetAllByCondition(x =>
                {
                    TimeRange reservationTimeRange = new TimeRange(x.DateFrom, x.DateTo);

                    return (limitToGivenLocation? x.LocationIDFK == LocationID : true) && x.Car.ModelIDFK == CarModelID
                        && reservationTimeRange.IntersectsWith(givenTimeRange);

                });

                var carsInReservations = carReservations.Select(x => x.CarIDFK).ToList();
                foreach (var car in cars)
                {
                    if (!carsInReservations.Contains(car.LicensePlateID))
                    {
                        await _reservationsRepo.Add(new Reservation()
                        {
                            ReservationID = new Guid(),
                            UserIDFK = ClientID,
                            CarIDFK = car.LicensePlateID,
                            DateFrom = DateFrom,
                            DateTo = DateTo,
                            LocationIDFK = LocationID,
                            isPickedUp = false,
                        });

                        return new BaseResponse(null, true);
                    }
                }

                throw new PostException("Selected model is currently unavailable");
            }
 
        }
    }
}
