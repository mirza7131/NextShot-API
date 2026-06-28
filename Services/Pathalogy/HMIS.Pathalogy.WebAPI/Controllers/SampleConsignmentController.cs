
using AutoMapper;
using CommonDTOs.ResponseDTO;
using HMIS.Pathalogy.Domain.Models.DbModels;
using HMIS.Pathalogy.Domain.Models.DTO.CreateOrEditBatchSampleDto;
using HMIS.Pathalogy.Domain.Models.DTO.SampleBatchListDto;
using HMIS.Pathalogy.Domain.Models.DTO.SampleConsignmentDetailDto;
using HMIS.Pathalogy.Domain.Models.DTO.SampleConsignmentDto;
using HMIS.Pathalogy.Service;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMIS.Pathalogy.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    ////[Authorize]

    public class SampleConsignmentController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly SampleConsignmentService<SampleConsignment> _SampleConsignmentService;
        #endregion

        #region Constructor

        public SampleConsignmentController( 
            TokenService tokenService,
            SampleConsignmentService<SampleConsignment> SampleConsignmentService
        )
        {
            _tokenService = tokenService;
            _SampleConsignmentService = SampleConsignmentService;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditSampleConsignmentDto input)
        {
            var obj = await _SampleConsignmentService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }
        [HttpPost]
        [Route("CreateOrEditBatch")]
        public async Task<IActionResult> CreateOrEditBatch(CreateOrEditBatchSampleDto input)
        {
            var obj = await _SampleConsignmentService.CreateOrEditBatch(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _SampleConsignmentService.Delete(Id);

            return Ok(new ResponseDelete { data = obj });

        }

        [HttpPost]
        [Route("UpdateConsignmentStatus")]
        public async Task<IActionResult> UpdateConsignmentStatus(CreateOrEditSampleConsignmentDto input)
        {
            var obj = await _SampleConsignmentService.UpdateConsignmentStatus(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("UpdateConsignmentDetailStatus")]
        public async Task<IActionResult> UpdateConsignmentDetailStatus(List<CreateOrEditSampleConsignmentDetailDto> input)
        {
            var obj = await _SampleConsignmentService.UpdateConsignmentDetailStatus(input);
            return Ok(new ResponseSave { data = obj });
        }


        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterSampleConsignmentDto filterUserDto)
        {
            var response = await _SampleConsignmentService.GetSampleConsignmentWithDetail(filterUserDto);
            return Ok(new ResponseSuccess { data = response });
        }
        [HttpGet]
        [Route("GetAllWithPaginationForBatchList")]
        public async Task<IActionResult> GetAllWithPaginationForBatchList([FromQuery] FilterSampleBatchListDto filterUserDto)
        {
            var response = await _SampleConsignmentService.GetAllWithPaginationForBatchList(filterUserDto);
            return Ok(new ResponseSuccess { data = response });
        }
        [HttpGet]
        [Route("GetAllWithPaginationForCompletedBatchList")]
        public async Task<IActionResult> GetAllWithPaginationForCompletedBatchList([FromQuery] FilterSampleBatchListDto filterUserDto)
        {
            var response = await _SampleConsignmentService.GetAllWithPaginationForCompletedBatchList(filterUserDto);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _SampleConsignmentService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetAllSampleCollectedConsignmentList")]
        public async Task<IActionResult> GetAllSampleCollectedConsignmentList([FromQuery] FilterSampleConsignmentDto filterUserDto)
        {
            var response = await _SampleConsignmentService.GetAllSampleCollectedConsignmentList(filterUserDto);
            return Ok(new ResponseSuccess { data = response });
        }
        [HttpGet]
        [Route("GetReceiveReceivedCollectedSampleList")]
        public async Task<IActionResult> GetReceiveReceivedCollectedSampleList([FromQuery] FilterSampleConsignmentDto filterUserDto)
        {
            var response = await _SampleConsignmentService.GetReceiveReceivedCollectedSampleList(filterUserDto);
            return Ok(new ResponseSuccess { data = response });
        }
        // This API Is Updated Version Of GetReceiveReceivedCollectedSampleList Now Move TO Store Procedure 
        [HttpGet]
        [Route("GetSampleCollectedConsignmentList")]
        public async Task<IActionResult> GetSampleCollectedConsignmentList([FromQuery] FilterSampleConsignmentDto filterUserDto)
        {
            var response = await _SampleConsignmentService.GetSampleCollectedConsignmentList(filterUserDto);
            return Ok(new ResponseSuccess { data = response });
        }
        [HttpGet]
        [Route("GetHCPAllSampleCollectedConsignmentList")]
        public async Task<IActionResult> GetHCPAllSampleCollectedConsignmentList([FromQuery] FilterSampleConsignmentDto filterUserDto)
        {
            var response = await _SampleConsignmentService.GetHCPAllSampleCollectedConsignmentList(filterUserDto);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetConsignmentWithConsignmentDetailByConsignmentId")]
        public async Task<IActionResult> GetConsignmentWithConsignmentDetailByConsignmentId([FromQuery] FilterSampleConsignmentDto filterUserDto)
        {
            var obj = await _SampleConsignmentService.GetConsignmentWithConsignmentDetailByConsignmentId(filterUserDto);
            return Ok(new ResponseSuccess { data = obj });
        }

        [HttpGet]
        [Route("GetRejectedConsignment")]
        public async Task<IActionResult> GetRejectedConsignment([FromQuery] FilterSampleConsignmentDto filterUserDto)
        {
            var obj = await _SampleConsignmentService.GetRejectedConsignment(filterUserDto);
            return Ok(new ResponseSuccess { data = obj });
        }


        #endregion

        #region Helper Methods


        #endregion
    }
}
