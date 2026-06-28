using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.AttachmentDto;
using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using JWTAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly AttachmentService<Attachment> _AttachmentService;

        #endregion

        #region Constructor

        public AttachmentController(ILogger<AuthenticationController> logger, TokenService tokenService, AttachmentService<Attachment> AttachmentService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _AttachmentService = AttachmentService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditAttachmentDto input)
        {
            var obj = await _AttachmentService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _AttachmentService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _AttachmentService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = response });
        }


        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _AttachmentService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetByParentId")]
        public async Task<IActionResult> GetByParentId(Guid ParentId)
        {
            var obj = await _AttachmentService.GetByParentId(ParentId, x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}
