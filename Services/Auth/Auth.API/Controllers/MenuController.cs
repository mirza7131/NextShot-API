using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.MenuDto;
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
    public class MenuController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly ILogger<AuthenticationController> _logger;
        private readonly TokenService _tokenService;
        private readonly MenuService<Menu> _MenuService;
        private readonly MimsMedicineDataService _MimsMedicineDataService;
        private readonly bool _isActiveOffline;
        #endregion

        #region Constructor

        public MenuController(ILogger<AuthenticationController> logger,
           TokenService tokenService,
           MenuService<Menu> MenuService,
           MimsMedicineDataService MimsMedicineDataService,
           IConfiguration config
        )
        {
            _logger = logger;
            _tokenService = tokenService;
            _MenuService = MenuService;
            _MimsMedicineDataService = MimsMedicineDataService;
            _isActiveOffline = config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") ?
                             config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") : false;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("CreateOrEdit")]
        public async Task<IActionResult> CreateOrEdit(CreateOrEditMenuDto input)
        {
            var obj = await _MenuService.CreateOrEdit(input);
            return Ok(new ResponseSave { data = obj });
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var obj = await _MenuService.Delete(Id);
            return Ok(new ResponseDelete { data = obj });
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _MenuService.GetAll(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted);
            return Ok(new ResponseSuccess { data = list });
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var obj = await _MenuService.GetById(Id);
            return Ok(new ResponseSuccess { data = obj });
        }

        //****** This Method Return all the menu as tree base For super admin user ********* //
        [HttpGet]
        [Route("GetAllModules")]
        public async Task<IActionResult> GetAllModules()
        {
            var moduleList = await _MenuService.GetAllModules();
            return Ok(new ResponseSuccess { data = moduleList });
        }

        //****** This Method Return the tree base menu which show on as a sidebar menu w.r.t user role ********* //
        [HttpGet]
        [Route("GetAllMenuAccessByUserRole")]
        public async Task<IActionResult> GetAllMenuAccessByUserRole()
        {
            IList<ViewModulesDto> moduleList = null;
            if (TokenService.IsSuperAdmin())
            {
                moduleList = await _MenuService.GetAllModules();
                return Ok(new ResponseSuccess { data = moduleList });
            }

            moduleList = await _MenuService.GetAllMenuAccessByUserRole();
            return Ok(new ResponseSuccess { data = moduleList });
        }


        //****** This Method is use to get the raw list of permissions so we can implement condition on client side ********* //
        //****** This Method is not for menu to show on screen ********* //
        [HttpGet]
        [Route("GetAllUserPermissions")]
        public async Task<IActionResult> GetAllUserPermissions()
        {
            if (TokenService.IsSuperAdmin())
            {
                var superAdminPermissionsList = await _MenuService.GetALLMenuPermissionsForSuperAdmin();
                return Ok(new ResponseSuccess { data = superAdminPermissionsList });
            }

            var userPermissionslist = await _MenuService.GetAllPermissionsWithUserId();
            return Ok(new ResponseSuccess { data = userPermissionslist });
        }




        [HttpGet]
        [Route("GetModulesListByRoleId")]
        public async Task<IActionResult> GetModulesListByRoleId(Guid RoleId)
        {
            var moduleList = await _MenuService.GetModulesListByRoleId(RoleId);
            return Ok(new ResponseSuccess { data = moduleList });
        }

        //Ubaid New Dashboard work
        [HttpGet]
        [Route("GetCompleteModuleListByRoleId")]
        public async Task<IActionResult> GetCompleteModuleListByRoleId(Guid RoleId)
        {
            var moduleList = await _MenuService.GetCompleteModuleListByRoleId(RoleId);
            // if system is running in offline mode then get medicine from mims to import into db
            if (_isActiveOffline)
            {
                try 
                {
                    await _MimsMedicineDataService.ImportDataFromMims(_isActiveOffline); // get Medicine from Mims whether its Online or Offline
                }
                catch (Exception ex) 
                { 
                    Console.Write(ex.ToString());
                }
            }
            return Ok(new ResponseSuccess { data = moduleList });
        }

        [HttpGet]
        [Route("GetMenuListByModuleId")]
        public async Task<IActionResult> GetMenuListByModuleId(Guid ModuleId)
        {
            var menuList = await _MenuService.GetMenuListByModuleId(ModuleId);
            return Ok(new ResponseSuccess { data = menuList});
        }


        #endregion

        #region Helper Methods


        #endregion
    }
}
