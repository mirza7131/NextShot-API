using CommonDTOs.ResponseDTO;
using HMIS.EMC.Domain.Models.Dto;
using HMIS.EMC.Domain.Models.Dto.FilterDto;
using HMIS.EMC.Service;
using HMIS.EMC.Service.Interfaces;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.EMC.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostMortemController : ControllerBase
    {
        #region Class Fields & Properties
        private readonly TokenService _tokenService;
        private readonly IPostMortemForm postMortemForm;
        #endregion

        #region Constructor
        public PostMortemController(TokenService _tokenService,IPostMortemForm postMortemForm)
        {
            this._tokenService = _tokenService;
            this.postMortemForm = postMortemForm;
        }
        #endregion

        #region CUD
        [HttpPost]
        [Route("CreateOrEditGeneralForm")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditPostMortemGeneralFormDto input)
        {
            var obj = await postMortemForm.CreateOrEdit(input);
            //return Ok(new ResponseSave { data = obj });
            if (input.IsReportCount != true)
                return Ok(new ResponseSave { data = obj });
            else
                return Ok(new ResponseSuccess { data = obj });
        }

        [HttpPost]
        [Route("CreateOrEditExternalForm")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditPostMortemExternalFormDto input)
        {
            try
            {
                var obj = await postMortemForm.CreateOrEdit(input);
                return Ok(new ResponseSave { data = obj });
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }

        [HttpPost]
        [Route("CreateOrEditInternalForm")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditPostMortemInternalFormDto input)
        {
            var obj = await postMortemForm.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("CreateOrEditReportForm")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditPostMortemReportDto input)
        {
            var obj = await postMortemForm.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        #endregion

        #region Read
        [HttpGet]
        [Route("GetAllPostMortemPatientsList")]
        public async Task<IActionResult> GetAllPostMortemPatientsList([FromQuery] SearchFilterDto? filter)
        {
            var obj = await postMortemForm.GetAllPostMortemPatients(filter);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetSinglePostMortemFormByPatientId")]
        public async Task<IActionResult> GetSinglePostMortemFormByPatientId(Guid PatientId)
        {
            var res = await postMortemForm.GetSinglePostMortemFormByPatientId(PatientId);
            return Ok(new ResponseSuccess { data =  res });
        }
        #endregion
    }
}
