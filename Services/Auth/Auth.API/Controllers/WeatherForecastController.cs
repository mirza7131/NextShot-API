using CommonDTOs.ResponseDTO;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net;

namespace Auth.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        #region Class Fields & Propertities

        private static readonly string[] Summaries = new[]
       {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };


        private readonly ILogger<WeatherForecastController> _logger;
        private readonly TokenService _tokenService;

        #endregion

        #region Constructor
        public WeatherForecastController(ILogger<WeatherForecastController> logger, TokenService tokenService)
        {
            _logger = logger;
            _tokenService = tokenService ?? throw new ArgumentNullException();
        }

        #endregion

        #region CUD Operations


        #endregion

        #region Read Operations

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {

            var email = TokenService.GetUserEmail();
            var userId = _tokenService.GetUserId();
            var userInfo = TokenService.GetUserLoggedInfo();

            //try
            //{
            //    if (ModelState.IsValid)
            //    {
            //        //var result = await _MepgComplaintService.CreateAsync(value);
            //        if (result.Success)
            //            return Ok(new ResponseDTO { statusCode = HttpStatusCode.Created, message = result.Message, data = result.Payload });
            //    }
            //    return Ok(new ResponseDTO { status = true, statusCode = (int)HttpStatusCode.BadRequest, message = getValidationErrorMessages(), Content = "" });

            //}
            //catch (Exception ex)
            //{
            //    // Log In Database 
            //    //Guid ErrorLogId = await _errorlogService.LogError(ex);
            //    return Ok(new ResponseDTO { status = true, statusCode = HttpStatusCode.ExpectationFailed, message = MessageEnum.serverSideError + ErrorLogId, Content = "" });
            //}


            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        #endregion

        #region Helper Methods

        #endregion

    }
}