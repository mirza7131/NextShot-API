using AppCommonMethods;
using AppCommonMethods.AppConstants;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.PaginationDto;
using AuthDAL.Models.Dto.ProfileDto;
using AuthDAL.Models.Dto.RoleDto;
using AuthDAL.Repositories;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthBAL
{
    public class RoleService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<Role> _uowRole;
        private UnitOfWork<RoleMenu> _uowRoleMenu;
        private UnitOfWork<UserRole> _uowUserRoles;

        #endregion

        #region Constructor

        public RoleService(TokenService tokenService, UnitOfWork<Role> uowRole, UnitOfWork<RoleMenu> uowRoleMenu, UnitOfWork<UserRole> uowUserRoles, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowRole = uowRole;
            _uowRoleMenu = uowRoleMenu;
            _uowUserRoles = uowUserRoles;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditRoleDto> CreateOrEdit(CreateOrEditRoleDto input)
        {
            if(AppCommonMethod.IsNullOrEmptyGuid(input.RoleId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditRoleDto> Create(CreateOrEditRoleDto input)
        {
            var obj = _mapper.Map<Role>(input);
            await FillEntity(obj);
            Role responseObj = await _uowRole.Repository.Insert(obj);
            await _uowRole.Save();
            return _mapper.Map<CreateOrEditRoleDto>(responseObj);
        }

        private async Task<CreateOrEditRoleDto> Update(CreateOrEditRoleDto input)
        {
            //var dbObj = await _uowRole.Repository.GetALL(x => x.RoleId == input.RoleId).Include(x => x.RoleMenus).FirstOrDefaultAsync();
            //var dbObj = await _uowRole.Repository.GetById(input.RoleId);
            //var obj = _mapper.Map(input, dbObj);
            //await FillEntity(dbobj);
            using (var trans = _uowRole.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    var _uowRoleMenu = new UnitOfWork<RoleMenu>(_uowRole.GetDbContext());

                    var roleMenu = await _uowRoleMenu.Repository.GetALL(x => x.RoleId == input.RoleId).ToListAsync();

                    foreach(var roleMenuItem in roleMenu)
                    {
                        
                        _uowRoleMenu.Repository.Delete(roleMenuItem);
                        await _uowRoleMenu.Save();
                    }

                    var dbObj = await _uowRole.Repository.GetById(input.RoleId!);

                    if (AppCommonMethod.IsNullObject(dbObj))
                        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                    dbObj.Name = input.Name;
                    dbObj.ShortName = input.ShortName;
                    dbObj.RoutingUrl = input.RoutingUrl;
                    dbObj.IsActive = input.IsActive;
                    dbObj.UpdatedBy = _tokenService.GetUserId();
                    dbObj.UpdatedOn = DateTime.Now;
                    dbObj.ActionTypeId = (int)ActionTypeEnum.Edit;
                    _uowRole.Repository.Update(dbObj);
                    await _uowRole.Save();

                    foreach (var item in input.RoleMenus)
                    {
                        //if(AppCommonMethod.IsNullOrEmptyGuid(item.RoleMenuId)
                        //{
                            var obj = _mapper.Map<RoleMenu>(item);

                            obj.RoleMenuId = Guid.NewGuid();
                            obj.RoleId = dbObj.RoleId;
                            obj.CreatedBy = _tokenService.GetUserId();
                            obj.CreatedOn = DateTime.Now;
                            obj.ActionTypeId = (int)ActionTypeEnum.Create;
                            await _uowRoleMenu.Repository.Insert(obj);
                            await _uowRoleMenu.Save();
                        //}
                        //else
                        //{
                        //    var roleMenuDbObj = await _uowRoleMenu.Repository.GetById(item.RoleMenuId);
                        //    var obj = _mapper.Map(item, roleMenuDbObj);
                        //    obj.UpdatedBy = _tokenService.GetUserId();
                        //    obj.UpdatedOn = DateTime.Now;
                        //    obj.ActionTypeId = (int)ActionTypeEnum.Edit;
                        //    _uowRoleMenu.Repository.Update(obj);
                        //    await _uowRoleMenu.Save();
                        //}
                    }

                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
            return input;
            //return _mapper.Map<CreateOrEditRoleDto>(input);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowRole.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);
            _uowRole.Repository.Update(dbObj!);
            await _uowRole.CommitAsync();
            return true;

        }

        #endregion

        #region Read Operations

        public async Task<List<ViewRoleDto>> GetAll(Expression<Func<Role, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<Role> responseObj = new List<Role>();
            if (await CanOnlyCreateHFUsers())
            {
                var userId = _tokenService.GetUserId();
                responseObj = await _uowRole.Repository.GetALL(filter).Where(x => x.IsShowHfadmin == true).ToListAsync();
                return _mapper.Map<List<ViewRoleDto>>(responseObj);
            }

            responseObj = await _uowRole.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewRoleDto>>(responseObj);
        }
        public async Task<ViewPagerDto<ViewRoleDto>> GetAllWithPagination(FilterRoleDto filter)
        {
            var list = _uowRole.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.Name.ToLower().StartsWith(filter.SearchString) || x.ShortName!.ToLower().StartsWith(filter.SearchString))
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<ViewRoleDto> IQueryableList = list.Select(x =>
                new ViewRoleDto
                {
                    RoleId = x.RoleId,
                    Name = x.Name,
                    ShortName = x.ShortName,
                    IsActive = x.IsActive
                });

            var pagedList = await PagedListDto<ViewRoleDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewRoleDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList
            };

            return responseObject;
        }

        public async Task<ViewRoleDto> GetById(Guid input)
        {
            Role? responseObj = await _uowRole.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewRoleDto>(responseObj);
        }

        public async Task<ViewRoleDto> GetByIdWithRoleMenuDetails(Guid input)
        {
            Role? responseObj = await _uowRole.Repository.GetALL(x => x.RoleId.ToString() == input.ToString()).Include(x => x.RoleMenus).FirstOrDefaultAsync();
            return _mapper.Map<ViewRoleDto>(responseObj);
        }

        public async Task<bool> CanOnlyCreateHFUsers()
        {
            return await _uowUserRoles.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).Include(x => x.Role).Where(x => x.Role.Name == RoleConst.HFAdmin || x.Role.Name == RoleConst.SDP).CountAsync() > 0 ? true : false;
        }


        #endregion

        #region Helper Methods

        private async Task FillEntity(Role obj)
        {
            //var roleMenuDbList = await _uowRoleMenu.Repository.GetALL(x => x.RoleId == obj.RoleId).ToListAsync();
            //var roleMenuInput = obj.RoleMenus;
            //obj.RoleMenus = new List<RoleMenu>();

            if (obj.RoleId == Guid.Empty)
            {
                obj.RoleId = Guid.NewGuid();
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

            foreach (var roleMenu in obj.RoleMenus)
            {
                if (roleMenu.RoleMenuId == Guid.Empty)
                {
                    roleMenu.RoleMenuId = Guid.NewGuid();
                    roleMenu.RoleId = obj.RoleId;
                    roleMenu.CreatedBy = _tokenService.GetUserId();
                    roleMenu.CreatedOn = DateTime.Now;
                    roleMenu.ActionTypeId = (int)ActionTypeEnum.Create;
                }
                else
                {
                    roleMenu.CreatedBy = obj.CreatedBy;
                    roleMenu.CreatedOn = obj.CreatedOn;
                    roleMenu.RoleId = obj.RoleId;
                    roleMenu.UpdatedBy = _tokenService.GetUserId();
                    roleMenu.UpdatedOn = DateTime.Now;
                    roleMenu.ActionTypeId = (int)ActionTypeEnum.Edit;
                }
            }
        }


        private void FillEntityRolesMenu(RoleMenu roleMenu, Guid roleId)
        {
            if (roleMenu.RoleMenuId == Guid.Empty)
            {
                roleMenu.RoleMenuId = Guid.NewGuid();
                roleMenu.RoleId = roleId;
                roleMenu.CreatedBy = _tokenService.GetUserId();
                roleMenu.CreatedOn = DateTime.Now;
                roleMenu.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                roleMenu.UpdatedBy = _tokenService.GetUserId();
                roleMenu.UpdatedOn = DateTime.Now;
                roleMenu.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }


        private void FillEntityDelete(Role obj)
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
