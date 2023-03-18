using Backend.Exceptions;
using Backend.RequestsModels;
using Backend.ResponsesModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("[controller]")]
    public class ReservationsController : ControllerBase
    {
        IReservationsService _reservationsService;

        public ReservationsController(IReservationsService reservationsService)
        {
            _reservationsService = reservationsService;
        }

        [HttpPost]
        [Authorize]
        [Route("MakeReservation")]
        public async Task<IActionResult> MakeReservation([FromBody] ReservationRequest reservationRequest)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new BaseResponse(new List<string> { "Invalid data provided" }, false));
            }
            try
            {
                var response = await _reservationsService.MakeReservation(reservationRequest.ClientID, reservationRequest.CarModelID, reservationRequest.LocationID, reservationRequest.DateFrom, reservationRequest.DateTo);

                return Ok(response);
            }
            catch (PostException e)
            {
                return BadRequest(new BaseResponse(new List<string> { e.Message }, false));
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        [HttpGet]
        [Authorize]
        [Route("GetByUser/{userID}")]
        public async Task<IActionResult> GetClientsReservations(Guid userID)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new BaseResponse(new List<string> { "Invalid data provided" }, false));
            }
            try
            {
                var response = await _reservationsService.GetUserReservations(userID);

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

        [HttpGet]
        [Authorize(Roles = "Staff")]
        [Route("GetAll/{fromNumber}/{toNumber}")]
        public async Task<IActionResult> GetAll(int fromNumber, int toNumber)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new BaseResponse(new List<string> { "Invalid data provided" }, false));
            }
            try
            {
                var response = await _reservationsService.GetUsersReservationsStaff(fromNumber, toNumber);

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
