using CommonDTOs.ResponseDTO;
//using HMIS.MEAs.Domain.Models.DTO;
using HMIS.MEAs.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HMIS.MIMS.Domain.Models.DbModels;
using JWTAuthentication;



namespace HMIS.MEAs.WebAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]


    public class evaluationController : ControllerBase
    {
        private readonly evaluationService _evaluation;
        private readonly TokenService _tokenService;

        #region Constructor

        public evaluationController(evaluationService evaluation, TokenService tokenService)
        {

            _tokenService = tokenService;
            _evaluation = evaluation;

        }


        #endregion







        [HttpGet]
        [Route("GetHealthFacilitiesZone")]
        public async Task<IActionResult> GetUsersVisits()
        {
            var data = await _evaluation.GetHealthFacilitiesZone();
            return Ok(new ResponseSuccess { data = data });
        }


        //[HttpGet]
        //public async Task<IActionResult> GetUsersVisits()
        //{
        //    try
        //    {
        //      //  var userId = Token.GetUserId();
        //        var currentMonth = DateTime.Now;
        //        string month = currentMonth.ToString("MMMM");
        //        string year = currentMonth.Year.ToString();
        //        var userVisits = _evaluation.GetHealthFacilitiesZone();
        //        return new ResponseListDTO { Message = "Data Fecthed", List = userVisits };
        //    }
        //    catch (Exception ex)
        //    {
        //        long ErrorLogId = await CommonMethods.LogError(ex);
        //        return new ResponseListDTO { Error = true, StatusCode = (int)HttpStatusCode.BadRequest, Message = MessageEnum.serverSideError + ex.InnerException, List = "" };
        //    }
        //}


    }
}
