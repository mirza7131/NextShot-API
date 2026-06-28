using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.InvoiceDto;
using AuthDAL.Models.Dto.UserDto;
using CommonDTOs.ResponseDTO;
using HMIS.MIMS.Domain.Models.DTO.InventoryDetailDto;
using HMIS.MIMS.Domain.Models.DTO.InventoryMasterDto;
using HMIS.MIMS.Domain.Models.DTO.PaginationDto;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class InvoiceController : ControllerBase
    {
      
       #region Class Fields & Propertities

            private readonly ILogger<AuthenticationController> _logger;
            private readonly TokenService _tokenService;
            private readonly InvoiceService<InvoiceMaster> _InvoiceService;

            #endregion

            #region Constructor

            public InvoiceController(ILogger<AuthenticationController> logger, TokenService tokenService, InvoiceService<InvoiceMaster> InvoiceService)
            {
                _logger = logger;
                _tokenService = tokenService;
                _InvoiceService = InvoiceService;
            }

            #endregion

            #region CUD Operations

            [HttpPost]
            [Route("CreateOrEdit")]
            public async Task<IActionResult> CreateOrEdit(CreateAndEditInvoiceDto input)
            {
                var obj = await _InvoiceService.CreateOrEdit(input);
                return Ok(new ResponseSave { data = obj });
            }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _InvoiceService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        //[HttpGet]
        //[Route("GetAll")]
        //public async Task<IActionResult> GetAll()
        //{
        //    var list = await _InvoiceService.GetAll();
        //    return Ok(new ResponseSuccess { data = list });
        //}

        //[HttpGet]
        //[Route("GetAllWithHealthFacilityId")]
        //public async Task<IActionResult> GetAllWithHealthFacilityId(int? HealthFacilityId = null)
        //{
        //    var list = await _InvoiceService.GetAllWithHealthFacilityId(HealthFacilityId);
        //    return Ok(new ResponseSuccess { data = list });
        //}

        [HttpGet]
        [Route("GetAllWithPagination")]
        public async Task<IActionResult> GetAllWithPagination([FromQuery] FilterUserDto filterUserDto)
        {
            var response = await _InvoiceService.GetAllWithPagination(filterUserDto);
            return Ok(new ResponseSuccess { data = response });
        }


        [HttpPost]
        [AllowAnonymous]
        [Route("GetInvoiceDetailById")]
        public async Task<IActionResult> GetInvoiceDetailById([FromBody] StockCheckboxCheckFilter? filter)
        {
            var response = await _InvoiceService.GetInvoiceDetailById(filter);
            return Ok(new ResponseSuccess { data = response as InvoiceDashboardCountAndListDto });

            //if (filter?.IsBatchWise == true)
            //{
            //    return Ok(new ResponseSuccess { data = response as List<ViewInventoryDetailSP_DTO> });
            //}
            //else
            //{
            //    return Ok(new ResponseSuccess { data = response as List<ViewInventoryMasterSP_DTO> });
            //}
        }


        
        [HttpGet]
        [Route("getInvoiceDetailByGuidId")]
        public async Task<IActionResult> getInvoiceDetailByGuidId()
        {
            var list = await _InvoiceService.getInvoiceDetailByGuidId();
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpPost]
        [Route("GetViewInvoiceById")]
        public async Task<IActionResult> GetViewInvoiceById(Guid Id)
        {
            var list = await _InvoiceService.GetViewInvoiceById(Id);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpPost]
        [Route("updateDueAmount")]
        public async Task<IActionResult> updateDueAmount(updateDueAmountDto obj)
        {
            var list = await _InvoiceService.updateDueAmount(obj);
            return Ok(new ResponseSuccess { data = list });
        }
        [HttpGet]
        [Route("getBillToAndItemAutoComplete")]
        public async Task<IActionResult> getBillToAndItemAutoComplete()
        {
            var list = await _InvoiceService.getBillToAndItemAutoComplete();
            return Ok(new ResponseSuccess { data = list });
        }
        //[HttpGet]
        //[Route("GetById")]
        //public async Task<IActionResult> GetById(int Id)
        //{
        //    var obj = await _InvoiceService.GetById(Id);
        //    return Ok(new ResponseSuccess { data = obj });
        //}

        //[HttpGet]
        //[Route("GetAllByDepartmentId")]
        //public async Task<IActionResult> GetAllByDepartmentId(Guid DepartmentProfileId)
        //{
        //    var list = await _InvoiceService.GetAll(DepartmentProfileId);
        //    return Ok(new ResponseSuccess { data = list });
        //}

        //[HttpGet]
        //[Route("GetAllCacheLabTest")]
        //public async Task<IActionResult> GetAllCacheLabTest()
        //{
        //    var list = await _InvoiceService.GetAllCacheLabTest();
        //    return Ok(new ResponseSuccess { data = list });
        //}


        #endregion

        #region Helper Methods


        #endregion

    }
}
