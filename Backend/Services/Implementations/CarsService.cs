using Backend.Context;
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
    public class CarsService : ICarsService
    {
        ICarsRepo _carsRepo;
        IReservationsRepo _reservationsRepo;
        AppDbContext _appDbContext;

        public CarsService(ICarsRepo carsRepo, AppDbContext appDbContext, IReservationsRepo reservationsRepo)
        {
            _carsRepo = carsRepo;
            _appDbContext = appDbContext;
            _reservationsRepo = reservationsRepo;
        }

        public async Task<AvailableCarsResponse> GetAvailableCars(Guid locationID, DateTime fromDate, DateTime toDate)
        {
            var nonReservedCars = new List<Car>();

            TimeRange givenTimeRange = new TimeRange(fromDate.AddDays(-1), toDate.AddDays(1));

            List<Car> cars;

            if (fromDate.Date == DateTime.Now.Date.AddDays(1))
            {
                cars = await _carsRepo.GetAllByCondition(x => x.LocationIDFK == locationID);

            }
            else
            {
                cars = await _carsRepo.GetAll();
            }

            foreach (var car in cars)
            {
                if (nonReservedCars.Any(x => x.Model.CarModelID == car.Model.CarModelID))
                {
                    continue;
                }

                var reservations = await _reservationsRepo.GetAllByCondition(x => x.CarIDFK == car.LicensePlateID);

                if (reservations.IsNullOrEmpty())
                {
                    nonReservedCars.Add(car);
                }
                else
                {
                    bool isAvailable = true;
                    foreach (var reservation in reservations)
                    {
                        TimeRange reservationTimeRange = new TimeRange(reservation.DateFrom, reservation.DateTo);

                        if (reservationTimeRange.IntersectsWith(givenTimeRange))
                        {
                            isAvailable = false; break;
                        }
                    }

                    if (isAvailable)
                    {
                        nonReservedCars.Add(car);
                    }
                }
            }

            if (nonReservedCars.Count > 0)
            {

                var carsModels = new List<CarModel>();

                foreach (var car in nonReservedCars)
                {
                    if (!carsModels.Contains(car.Model))
                    {
                        carsModels.Add(car.Model);
                    }

                    if (carsModels.Count == _appDbContext.CarsModels.Count())
                    {
                        break;
                    }
                }

                var carsResponses = new List<CarResponse>();

                foreach (var car in carsModels)
                {
                    carsResponses.Add(new CarResponse()
                    {
                        carModelID = car.CarModelID.ToString(),
                        modelName = car.ModelName,
                        modelRange = car.ModelRange,
                        acceleration = car.Acceleration,
                        maxNumberOfSeats = car.MaxNumberOfSeats,
                        pricePerDay = car.PricePerDay
                    });
                }

                return new AvailableCarsResponse(carsResponses);
            }
            else
            {
                throw new GetException("No cars available with given criteria");
            }
        }
    }
}
