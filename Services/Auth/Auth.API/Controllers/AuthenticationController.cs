using AuthBAL;
using CommonDTOs.ResponseDTO;
using CommonMessages;
using DTOs.UserDTO;
using JWTAuthentication;
using Microsoft.AspNetCore.Mvc;
using AuthDAL.Models.DbModels;
using AppCommonMethods;
using Microsoft.AspNetCore.Authorization;

namespace Auth.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly IApplicationBuilder _app;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly AuthService _authService;
        private readonly string ApiUrl;
        private readonly IHostEnvironment _env;
        private readonly string Environment;

        #endregion

        #region Constructor

        public AuthenticationController(ILogger<AuthenticationController> logger, TokenService tokenService, AuthService authService, IConfiguration config, IHostEnvironment env)
        {
            Environment = config.GetSection("Environment").Value ?? string.Empty;
            _env = env;
            _logger = logger;
            _tokenService = tokenService ?? throw new ArgumentNullException();
            _authService = authService ?? throw new ArgumentNullException();

            _logger.Log(LogLevel.Information, "Build Running In Environment : " + Environment);
        }

        #endregion

        #region Authentication Methods

        [HttpPost]
        [Route("Authenticate")] 
        public async Task<IActionResult> Authenticate(UserLoginDTO input)
        {
            if (_env.IsDevelopment())
            {
                var obj = await _authService.Authenticate(input);
                if (obj.IsDoctor)
                {
                    if (AppCommonMethod.IsNullorZeroInt(obj.DepartmentId) || AppCommonMethod.IsNullorZeroInt(obj.SectionId))
                    {
                        return Ok(new ResponseSuccess { message = CommonMessageConstant.Authenticated, data = CommonMessageConstant.DepartmentOrSectionNotDefined });
                    }
                }
                return Ok(new ResponseSuccess { message = CommonMessageConstant.Authenticated, data = obj });
            }
            else
            {
                var obj = await _authService.Authenticate(input);

                if (obj.IsDoctor)
                {
                    if (AppCommonMethod.IsNullorZeroInt(obj.DepartmentId) || AppCommonMethod.IsNullorZeroInt(obj.SectionId))
                    {
                        return Ok(new ResponseSuccess { message = CommonMessageConstant.Authenticated, data = CommonMessageConstant.DepartmentOrSectionNotDefined });
                    }
                }
                return Ok(new ResponseSuccess { message = CommonMessageConstant.Authenticated, data = obj });
            }
        }

       

        [HttpPost]
        [Route("Signout")]
        public async Task<IActionResult> Signout(UserSignoutDto input)
        {
            var obj = await _authService.Signout(input);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpPost]
        [Route("SendSms")]
        public async Task<IActionResult> SendSms(UserLoggedInfoDTO input)
        {
            var data = await _authService.SendSms(input);
            return Ok(new ResponseSuccess { data = data });
        }

        #endregion

        #region CUD Operations


        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid id, int number)
        {
            var obj = await _authService.Get();
            return Ok(new ResponseSuccess { data = obj });
        }


        [HttpGet]
        [Route("VerifyOTP")]
        public async Task<IActionResult> VerifyOTP(int Otp)
        {
            var obj = await _authService.VerifyOTP(Otp);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion

    }
}
