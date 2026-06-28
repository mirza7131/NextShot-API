using AppCommonMethods;
using AuthBAL.Common;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.MenuDto;
using AuthDAL.Repositories;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using FileHandler;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AuthBAL
{
    public class MenuService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UploadFiles _fileUploader;

        private static IList<ViewModulesDto> moduleList;
        private List<ViewModulesDto> menuList;

        private UnitOfWork<Menu> _uowMenu;
        private UnitOfWork<RoleMenu> _uowRoleMenu;


        #endregion

        #region Constructor

        public MenuService(TokenService tokenService, UnitOfWork<Menu> uowMenu, UnitOfWork<RoleMenu> uowRoleMenu, IMapper mapper ,UploadFiles fileUploader)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowMenu = uowMenu;
            _uowRoleMenu = uowRoleMenu;
            _fileUploader = fileUploader;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditMenuDto> CreateOrEdit(CreateOrEditMenuDto input)
        {
       

            if (!string.IsNullOrEmpty(input.ImageUrl))
                input.ImageUrl = await _fileUploader.UploadFileToCDN(CommonStringConstant.Menu, input.ImageUrl , _tokenService.GetAccessToken());

            if (AppCommonMethod.IsNullOrEmptyGuid(input.MenuId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditMenuDto> Create(CreateOrEditMenuDto input)
        {
            var obj = _mapper.Map<Menu>(input);
            FillEntity(obj);
            Menu? responseObj = await _uowMenu.Repository.Insert(obj);
            await _uowMenu.CommitAsync();
            return _mapper.Map<CreateOrEditMenuDto>(responseObj);
        }

        private async Task<CreateOrEditMenuDto> Update(CreateOrEditMenuDto input)
        {
            var dbObj = await _uowMenu.Repository.GetById(input.MenuId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);
            _uowMenu.Repository.Update(obj!);
            await _uowMenu.CommitAsync();
            return _mapper.Map<CreateOrEditMenuDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowMenu.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowMenu.Repository.Update(dbObj!);
            await _uowMenu.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewMenuDto>> GetAll(Expression<Func<Menu, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<Menu> responseObj = await _uowMenu.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewMenuDto>>(responseObj);
        }

        // ****** This is Used for Super admin at the moment ******* //
        public async Task<IList<GetAllmenuForSuperAdmin>> GetALLMenuPermissionsForSuperAdmin()
        {
            return await _uowMenu.GetDbContext().GetAllmenuForSuperAdmins.ToListAsync();
        }

        public async Task<IList<ViewGetAllRoleMenuAccess>> GetAllPermissionsWithUserId()
        {
            return await _uowMenu.GetDbContext().ViewGetAllRoleMenuAccesses.Where(x => x.UserId == _tokenService.GetUserId() && !string.IsNullOrEmpty(x.Url)).ToListAsync();
        }

        public async Task<IList<ViewModulesDto>> GetAllModules()
        {
            var menus = await _uowMenu.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).ToListAsync();
            
            menuList = _mapper.Map<List<ViewModulesDto>>(menus);
            return CommonMethods.BuildTree(menuList);
        }

        public async Task<IList<ViewModulesDto>> GetAllMenuAccessByUserRole()
        {
            var menus = await _uowMenu.GetDbContext().ViewGetAllRoleMenuAccesses.Where(x => x.UserId == _tokenService.GetUserId()).ToListAsync();

            var menusLabel = await _uowMenu.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true && x.IsLabel == true).ToListAsync();

            var menuLabelList = _mapper.Map<List<ViewModulesDto>>(menusLabel);
            menuList = _mapper.Map<List<ViewModulesDto>>(menus);
            if (menuLabelList.Count() > 0)
                menuList.AddRange(menuLabelList);

            return CommonMethods.BuildTree(menuList);
        }

        public async Task<ViewMenuDto> GetById(Guid input)
        {
            Menu? responseObj = await _uowMenu.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewMenuDto>(responseObj);
        }

        public async Task<IList<ViewModuleAccessDto>> GetModulesListByRoleId(Guid roleId)
        {
            var modulesList = await _uowRoleMenu.Repository.GetALL(x => x.RoleId == roleId).Include(x => x.Menu).Where(x => x.Menu.IsModule == true && x.Menu.IsActive == true)
                .Select(y => new ViewModuleAccessDto
            {
                DisplayName = y.Menu.DisplayName,
                Name = y.Menu.Name,
                ModuleId = y.Menu.MenuId,
                Url = y.Menu.Url,
                Icon = y.Menu.Icon,
                ImageURL = y.Menu.ImageUrl
            }).ToListAsync();

            return modulesList;
        }

        public async Task<IList<ViewModuleAccessDto>> GetCompleteModuleListByRoleId(Guid roleId)
        {
            var modulesList = await _uowMenu.Repository.GetALL().Include(x => x.RoleMenus).Where(x => x.IsModule == true && x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.CreatedOn.Value.Date >= DateTime.Parse("2023-06-06").Date )
                .Select(y => new ViewModuleAccessDto
                {
                    DisplayName = y.DisplayName,
                    Name = y.Name,
                    ModuleId = y.MenuId,
                    Url = y.Url,
                    Icon = y.Icon,
                    ImageURL = y.ImageUrl,
                    RoleHasAccess = (y.RoleMenus.FirstOrDefault(x => x.RoleId == roleId) != null) ? true:false,
                }).ToListAsync();

            return modulesList;
        }

        public async Task<IList<ViewMenuAccessDto>> GetMenuListByModuleId(Guid ModuleId)
        {
            var menuList = await _uowMenu.Repository.GetALL(x => x.IsApi == false && x.IsLabel == false && x.IsModule == false && x.IsActive == true)
                .Select(y => new ViewMenuAccessDto
                {
                    MenuId = y.MenuId,
                    DisplayName = y.DisplayName,
                    Name = y.Name,
                    Url = y.Url,
                    Icon = y.Icon,
                    IsDisplayMenu = y.IsDisplayMenu,
                }).ToListAsync();

            return menuList;
        }

        #endregion

        #region Helper Methods

        private void FillEntity(Menu obj)
        {
            if (obj.MenuId == Guid.Empty)
            {
                obj.MenuId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }
        private void FillEntityDelete(Menu obj)
        {
            if (obj != null)
            {
                obj.DeletedBy = _tokenService.GetUserId();
                obj.DeletedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
            }

        }

       

        #endregion
    }
}