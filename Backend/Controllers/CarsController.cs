using Backend.Exceptions;
using Backend.ResponsesModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Backend.Controllers
{
    [Route("[controller]")]
    public class CarsController : ControllerBase
    {
        ICarsService _carsService;

        public CarsController(ICarsService carsService)
        {
            _carsService = carsService;
        }

        [HttpGet]
        [Authorize(Roles = "Client, Staff")]
        [Route("GetAvailable/{locationID}/{dateFrom}/{dateTo}")]
        public async Task<IActionResult> GetAllLocations(Guid locationID, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var response = await _carsService.GetAvailableCars(locationID, dateFrom, dateTo);

                return Ok(response);
            }
            catch (GetException e)
            {
                return BadRequest(new BaseResponse(new List<string> { e.Message }, false));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }
    }
}
