using CommonDTOs.ResponseDTO;
using IMS_System.Service;
using IMSSystem.Domain.Models.DbModels;
using IMSSystem.Domain.Models.DTO.Invoice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IMS_System.WepApi.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : Controller
    {
        private readonly InvoiceService _invoice;
        #region Constructor

        public InvoiceController(InvoiceService invoice)
        {

            //  _tokenService = tokenService;
            _invoice = invoice;

        }

        #endregion

        #region CUD Operations
        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateorEditInvoice(SpInvoiceDTO invoice)
        {
            var data = await _invoice.CreateorEditInvoice(invoice);
            return Ok(new ResponseSuccess { data = data });
        }
        [HttpPost]
        [Route("SavePartimeEmployee")]
        public async Task<IActionResult> SavePartimeEmployee(SaveEmployeeDTO employee)
        {
            var data = await _invoice.SavePartimeEmployee(employee);
            return Ok(new ResponseSuccess { data = data });
        }
        [HttpPost]
        [Route("AddNotes")]
        public async Task<IActionResult> AddNotes(InsertComments comments)
        {
            var data = await _invoice.AddNotes(comments);
            return Ok(new ResponseSuccess { data = data });
        }
        #endregion

        #region READ Operations
        [HttpGet]
        [Route("GetAllDivisions")]
        public async Task<IActionResult> GetAllDivisions()
        {
            var data = await _invoice.GetAllDivisions();
            return Ok(new ResponseSuccess { data = data });
        }
        [HttpGet]
        [Route("GetAllDistricts")]
        public async Task<IActionResult> GetAllDistricts(string val)
        {
            var data = await _invoice.GetAllDistricts(val);
            return Ok(new ResponseSuccess { data = data });
        }
        [HttpGet]
        [Route("GetHfTypes")]
        public async Task<IActionResult> GetHfTypes()
        {
            var data = await _invoice.GetHfTypes();
            return Ok(new ResponseSuccess { data = data });
        }
        [HttpGet]
        [Route("GetHealthFacilities")]
        public async Task<IActionResult> GetHealthFacilities(string hftCode, string code)
        {
            var data = await _invoice.GetHealthFacilities(hftCode, code);
            return Ok(new ResponseSuccess { data = data });
        }
        [HttpGet]
        [Route("GetServiceTypes")]
        public async Task<IActionResult> GetServiceTypes()
        {
            var data = await _invoice.GetServiceTypes();
            return Ok(new ResponseSuccess { data = data });
        }
        [HttpGet]
        [Route("GetInvoices")]
        public async Task<IActionResult> GetInvoices()
        {
            var data = await _invoice.GetInvoices();
            return Ok(new ResponseSuccess { data = data });
        }
        [HttpPost]
        [Route("GetEmployeeListForInvoice")]
        public async Task<IActionResult> GetEmployeeListForInvoice(FilterEmployeeDTO filter)
        {
            var data = await _invoice.GetEmployeeListForInvoice(filter);
            return Ok(new ResponseSuccess { data = data });
        }
        [HttpGet]
        [Route("GetAllEmployeList")]
        public async Task<IActionResult> GetAllEmployeList()
        {
            var data = await _invoice.GetAllEmployeList();
            return Ok(new ResponseSuccess { data = data });
        }
        [HttpPost]
        [Route("GetEmployeeToEdit")]
        public async Task<IActionResult> GetEmployeeToEdit([FromBody] GetEmployeeToEditRequest request)
        {
            var data = await _invoice.GetEmployeeToEdit(request.HfSpEmployeeId);
            return Ok(new ResponseSuccess { data = data });
        }
        [HttpGet]
        [Route("GetAllInvoiceStatus")]
        public async Task<IActionResult> GetAllInvoiceStatus()
        {
            var data = await _invoice.GetAllInvoiceStatus();
            return Ok(new ResponseSuccess { data = data });
        }
        [HttpGet]
        [Route("GetInvoiceStatusCount")]
        public async Task<IActionResult> GetInvoiceStatusCount()
        {
            var data = await _invoice.GetInvoiceStatusCount();
            return Ok(new ResponseSuccess { data = data });
        }
        
        [HttpGet]
        [Route("GetInvoiceById")]
        public async Task<IActionResult> GetInvoiceById(string? invoiceId)
        {
            var data = await _invoice.GetInvoiceById(invoiceId);
            return Ok(new ResponseSuccess { data = data });
        }
        #endregion
    }
}
