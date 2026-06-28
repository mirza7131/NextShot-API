using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.UnionCouncilDto;
using AuthDAL.Repositories.UOW;
using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UnionCouncilController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly UnionCouncilService<UnionCouncil> _UnionCouncilService;

        #endregion

        #region Constructor

        public UnionCouncilController(ILogger<AuthenticationController> logger, TokenService tokenService, UnionCouncilService<UnionCouncil> UnionCouncilService)
        {
            _logger = logger;
            _tokenService = tokenService;
            _UnionCouncilService = UnionCouncilService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditUnionCouncilDto input)
        {
            var obj = await _UnionCouncilService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int Id)
        {
            var obj = await _UnionCouncilService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _UnionCouncilService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetAllUcs")]
        public async Task<IActionResult> GetAllUcs()
        {
            var list = await _UnionCouncilService.GetAllUcs();
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(int Id)
        {
            var obj = await _UnionCouncilService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetAllByTehsilId")]
        public async Task<IActionResult> GetAllByTehsilId(int TehsilId)
        {
            var obj = await _UnionCouncilService.GetAllByTehsilId(TehsilId);
            return Ok(new ResponseSuccess { data = obj });
        }

        #endregion

        #region Helper Methods


        #endregion
    }
}
