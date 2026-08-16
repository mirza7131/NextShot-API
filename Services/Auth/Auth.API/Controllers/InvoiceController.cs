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













        //next shot

        [HttpGet]
        [Route("GetInventoryItems")]
        public async Task<IActionResult> GetInventoryItems([FromQuery] FilterUserDto filterUserDto)
        {
            var response = await _InvoiceService.GetInventoryItems(filterUserDto);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetInventoryItemById")]
        public async Task<IActionResult> GetInventoryItemById(int id)
        {
            var response = await _InvoiceService.GetInventoryItemById(id);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpPost]
        [Route("CreateInventoryItem")]
        public async Task<IActionResult> CreateInventoryItem(CreateInventoryItemDto input)
        {
            var response = await _InvoiceService.CreateInventoryItem(input);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpPut]
        [Route("UpdateInventoryItem")]
        public async Task<IActionResult> UpdateInventoryItem(UpdateInventoryItemDto input)
        {
            var response = await _InvoiceService.UpdateInventoryItem(input);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpDelete]
        [Route("DeleteInventoryItem")]
        public async Task<IActionResult> DeleteInventoryItem(int id)
        {
            var response = await _InvoiceService.DeleteInventoryItem(id);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpPost]
        [Route("ChangeInventoryStatus")]
        public async Task<IActionResult> ChangeInventoryStatus(ChangeInventoryStatusDto input)
        {
            var response = await _InvoiceService.ChangeInventoryStatus(input);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpPost]
        [Route("ReduceInventoryStock")]
        public async Task<IActionResult> ReduceInventoryStock(ReduceInventoryStockDto input)
        {
            var response = await _InvoiceService.ReduceInventoryStock(input);
            return Ok(new ResponseSuccess { data = response });
        }





        [HttpPost]
        [Route("CreateClubCustomer")]
        public async Task<IActionResult> CreateClubCustomer([FromBody] CreateClubCustomerDto input)
        {
            var response = await _InvoiceService.CreateClubCustomer(input);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("SearchClubCustomers")]
        public async Task<IActionResult> SearchClubCustomers([FromQuery] string? search = "")
        {
            var response = await _InvoiceService.SearchClubCustomers(search ?? "");
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpPost]
        [Route("StartTableSession")]
        public async Task<IActionResult> StartTableSession([FromBody] StartTableSessionDto input)
        {
            var response = await _InvoiceService.StartTableSession(input);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpPost]
        [Route("AddPlayerToSession")]
        public async Task<IActionResult> AddPlayerToSession([FromBody] AddSessionPlayerDto input)
        {
            var response = await _InvoiceService.AddPlayerToSession(input);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpPost]
        [Route("AddInventoryItemToSession")]
        public async Task<IActionResult> AddInventoryItemToSession([FromBody] AddSessionInventoryDto input)
        {
            var response = await _InvoiceService.AddInventoryItemToSession(input);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpPost]
        [Route("AddGameToSession")]
        public async Task<IActionResult> AddGameToSession([FromBody] AddSessionGameDto input)
        {
            var response = await _InvoiceService.AddGameToSession(input);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpPost]
        [Route("EndTableSession")]
        public async Task<IActionResult> EndTableSession([FromBody] EndTableSessionDto input)
        {
            var response = await _InvoiceService.EndTableSession(input);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetRunningTableSessions")]
        public async Task<IActionResult> GetRunningTableSessions()
        {
            var response = await _InvoiceService.GetRunningTableSessions();
            return Ok(new ResponseSuccess { data = response });
        }




        [HttpGet]
        [Route("GetTableSessionHistory")]
        public async Task<IActionResult> GetTableSessionHistory([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            var response = await _InvoiceService.GetTableSessionHistory(fromDate, toDate);
            return Ok(new ResponseSuccess { data = response });
        }


        [HttpPost]
        [Route("CancelTableSession")]
        public async Task<IActionResult> CancelTableSession([FromBody] CancelTableSessionDto input, [FromQuery] int? tableSessionId, [FromQuery] int? id)
        {
            var sessionId = input?.TableSessionId ?? tableSessionId ?? id ?? 0;
            var response = await _InvoiceService.CancelTableSession(sessionId);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpPut]
        [Route("UpdateClubCustomer")]
        public async Task<IActionResult> UpdateClubCustomer([FromBody] UpdateClubCustomerDto input)
        {
            var response = await _InvoiceService.UpdateClubCustomer(input);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpPost]
        [Route("DeleteClubCustomer")]
        public async Task<IActionResult> DeleteClubCustomer([FromBody] DeleteClubCustomerDto input, [FromQuery] int? id, [FromQuery] int? clubCustomerId)
        {
            var customerId = input?.ClubCustomerId ?? clubCustomerId ?? id ?? 0;
            var response = await _InvoiceService.DeleteClubCustomer(customerId);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpGet]
        [Route("GetClubTables")]
        public async Task<IActionResult> GetClubTables()
        {
            var response = await _InvoiceService.GetClubTables();
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpPost]
        [Route("CreateClubTable")]
        public async Task<IActionResult> CreateClubTable([FromBody] CreateClubTableDto input)
        {
            var response = await _InvoiceService.CreateClubTable(input);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpPut]
        [Route("UpdateClubTable")]
        public async Task<IActionResult> UpdateClubTable([FromBody] UpdateClubTableDto input)
        {
            var response = await _InvoiceService.UpdateClubTable(input);
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpPost]
        [Route("DeleteClubTable")]
        public async Task<IActionResult> DeleteClubTable([FromBody] DeleteClubTableDto input, [FromQuery] int? id, [FromQuery] int? clubTableId)
        {
            var tableId = input?.ClubTableId ?? clubTableId ?? id ?? 0;
            var response = await _InvoiceService.DeleteClubTable(tableId);
            return Ok(new ResponseSuccess { data = response });
        }


        [HttpGet]
        [Route("GetCustomerPendingPayments")]
        public async Task<IActionResult> GetCustomerPendingPayments()
        {
            var response = await _InvoiceService.GetCustomerPendingPayments();
            return Ok(new ResponseSuccess { data = response });
        }

        [HttpPost]
        [Route("PayCustomerPendingAmount")]
        public async Task<IActionResult> PayCustomerPendingAmount(PayCustomerPendingAmountDto input)
        {
            var response = await _InvoiceService.PayCustomerPendingAmount(input);
            return Ok(new ResponseSuccess { data = response });
        }


        [HttpGet]
        [Route("GetCustomerPendingPaymentHistory")]
        public async Task<IActionResult> GetCustomerPendingPaymentHistory(int clubCustomerId)
        {
            var response = await _InvoiceService.GetCustomerPendingPaymentHistory(clubCustomerId);
            return Ok(new ResponseSuccess { data = response });
        }


        [HttpPost]
        [Route("CreateInventorySale")]
        public async Task<IActionResult> CreateInventorySale(CreateInventorySaleDto input)
        {
            var response = await _InvoiceService.CreateInventorySale(input);
            return Ok(new ResponseSuccess { data = response });
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
