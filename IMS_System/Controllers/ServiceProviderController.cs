using CommonDTOs.ResponseDTO;
using IMS_System.Service;
using IMSSystem.Domain.Models.DbModels;
using IMSSystem.Domain.Models.DTO.Invoice;
using IMSSystem.Domain.Models.DTO.ServiceProvider;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IMS_System.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProviderController : ControllerBase
    {
        private readonly ServiceProviderService _serviceProvder;
        #region Constructor

        public ServiceProviderController(ServiceProviderService serviceProvder)
        {

            //  _tokenService = tokenService;
            _serviceProvder = serviceProvder;

        }
        #endregion
        #region CUD Operations

        [HttpPost]
        [Route("RegisterCompany")]
        public async Task<IActionResult> RegisterCompany(RegisterSPDTO register)
        {
            var data = await _serviceProvder.RegisterCompany(register);
            return Ok(new ResponseSuccess { data = data });
        }
        #endregion
        #region Read Operations
        #endregion
        #region Helper
        #endregion





    }
}
