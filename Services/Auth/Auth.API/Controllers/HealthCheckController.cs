using CommonMessages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        #region Class Fields & Propertities

        private string _projectVersion; // Version

        #endregion

        #region Constructor

        public HealthCheckController(IConfiguration config)
        {
            _projectVersion = config.GetSection("ProjectVersion").Value ?? "Not Defined";
        }

        #endregion

        #region CUD Operations

        #endregion

        #region Read Operations

       

        [HttpGet]
        [Route("Check")]
        public async Task<string> Check()
        {
            var msg = CommonMessageConstant.Working+" Version: "+_projectVersion;
            return await Task.FromResult(msg);
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}
