using Backend.Context;
using Backend.DBLogic.DBModels;
using Backend.DBLogic.Repos.Cars;
using Backend.DBLogic.Repos.Reservations;
using Backend.ResponsesModels;
using Backend.Services.Interfaces;

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

            if (fromDate.Date == DateTime.Now.Date.AddDays(1))
            {
                var cars = await _carsRepo.GetAllByCondition(x => x.LocationIDFK == locationID);

                foreach(var car in cars)
                {
                    if (nonReservedCars.Any(x => x.Model.CarModelID == car.Model.CarModelID))
                    {
                        continue;
                    }

                    var reservation = await _reservationsRepo.GetByCondition(x => x.CarIDFK == car.LicensePlateID);

                    if (reservation == null || reservation.DateTo.Date <= fromDate.Date.AddDays(-2))
                    {
                        nonReservedCars.Add(car);
                    }                    
                }
            }
            else
            {
                var cars = await _carsRepo.GetAll();

                foreach (var car in cars)
                {
                    if (nonReservedCars.Any(x => x.Model.CarModelID == car.Model.CarModelID))
                    {
                        continue;
                    }

                    var reservation = await _reservationsRepo.GetByCondition(x => x.CarIDFK == car.LicensePlateID);

                    if (reservation == null || reservation.DateTo.Date <= fromDate.Date.AddDays(-2))
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
                throw new Exception("No cars available with given criteria");
            }
        }
    }
}
