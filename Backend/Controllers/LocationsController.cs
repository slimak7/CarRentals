using Backend.RequestsModels;
using Backend.ResponsesModels;
using Backend.Services.Implementations;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("[controller]")]
    public class LocationsController : ControllerBase
    {
        ILocationsService _locationsService;

        public LocationsController(ILocationsService locationsService)
        {
            _locationsService = locationsService;
        }

        [HttpGet]
        [Authorize(Roles = "Client, Staff")]
        [Route("AllLocations")]
        public async Task<IActionResult> GetAllLocations()
        {
            try
            {
                var response = await _locationsService.GetAllLocations();

                return Ok(response);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new AuthResponse(e.Message));
            }
        }
    }
}
