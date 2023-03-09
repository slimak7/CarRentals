using Backend.RequestsModels;
using Backend.ResponsesModels;
using Backend.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace Backend.Controllers
{
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest registerRequest)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new AuthResponse("Invalid data provided"));
            }
            try
            {
                var response = await _authService.Register(registerRequest.Email, registerRequest.Password, registerRequest.FirstName, registerRequest.LastName, registerRequest.PhoneNumber);

                return Ok(response);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new AuthResponse(e.Message));
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> LogInUser([FromBody] LogInRequest logInRequest)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(new AuthResponse("Invalid data provided"));
            }
            try
            {
                var response = await _authService.Login(logInRequest.Email, logInRequest.Password);

                return Ok(response);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new AuthResponse(e.Message));
            }
        }
    }
}
