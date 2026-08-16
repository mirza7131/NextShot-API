using AppCommonMethods;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.RoleDto;
using AuthDAL.Models.Dto.RoleMenuDto;
using AuthDAL.Repositories;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using JWTAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace AuthBAL
{
    public class RoleMenuService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<RoleMenu> _uowRoleMenu;

        #endregion

        #region Constructor

        public RoleMenuService(TokenService tokenService, UnitOfWork<RoleMenu> uowRoleMenu, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            
            _uowRoleMenu= uowRoleMenu;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditRoleMenuDto> CreateOrEdit(CreateOrEditRoleMenuDto input)
        {
            if(AppCommonMethod.IsNullOrEmptyGuid(input.RoleId) || AppCommonMethod.IsNullOrEmptyGuid(input.RoleMenuId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditRoleMenuDto> Create(CreateOrEditRoleMenuDto input)
        {
            var obj = _mapper.Map<RoleMenu>(input);
            FillEntity(obj);
            RoleMenu responseObj = await _uowRoleMenu.Repository.Insert(obj);
            await _uowRoleMenu.CommitAsync();
            return _mapper.Map<CreateOrEditRoleMenuDto>(responseObj);
        }

        private async Task<CreateOrEditRoleMenuDto> Update(CreateOrEditRoleMenuDto input)
        {
            var dbObj = await _uowRoleMenu.Repository.GetById(input.RoleMenuId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);
            _uowRoleMenu.Repository.Update(obj!);
            await _uowRoleMenu.CommitAsync();
            return _mapper.Map<CreateOrEditRoleMenuDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowRoleMenu.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);
            _uowRoleMenu.Repository.Update(dbObj!);
            await _uowRoleMenu.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewRoleMenuDto>> GetAll(Expression<Func<RoleMenu, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<RoleMenu> responseObj = await _uowRoleMenu.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewRoleMenuDto>>(responseObj);
        }

        public async Task<ViewRoleMenuDto> GetById(Guid input)
        {
            RoleMenu? responseObj = await _uowRoleMenu.Repository.GetById(input);
            return _mapper.Map<ViewRoleMenuDto>(responseObj);
        }

        public async Task<List<RoleMenuAccessDto>> GetCreateRoleMenuAccess()
        {

            //var menuList = await _RoleMenuRepository.GetCreateRoleMenuAccess();

            var menuList = await _uowRoleMenu.GetDbContext().ViewGetCreateRoleMenuAccesses.ToListAsync();

            var modules = menuList.Where(x => x.IsModule == true && x.ModuleId == null).Select(x => new RoleMenuAccessDto
           {
                Name = x.Name,
                DisplayName = x.DisplayName,
                IsModule= x.IsModule,
                HasAccess = x.HasAccess ?? false,
                IsOpened = false,
                MenuId= x.MenuId,
                ModuleId= x.ModuleId,
                RoleId= x.RoleId,
                RoleMenuId = x.RoleMenuId,
                ChildMenu = _mapper.Map<List<ViewGetEditRoleMenuAccess>>(menuList.Where(y => y.ModuleId == x.MenuId && y.IsModule == false).ToList())
            }).ToList();
            return modules;
        }

        public async Task<List<RoleMenuAccessDto>> GetEditRoleMenuAccess(Guid RoleId)
        {
            var menuList = await GetRoleMenuAccess(RoleId);
            
            return menuList.Where(x => x.IsModule == true && x.ModuleId == null).Select(x => new RoleMenuAccessDto
            {
                Name = x.Name,
                DisplayName = x.DisplayName,
                IsModule = x.IsModule,
                HasAccess = x.HasAccess,
                IsOpened = false,
                MenuId = x.MenuId,
                ModuleId = x.ModuleId,
                RoleId = x.RoleId,
                RoleMenuId = x.RoleMenuId,
                ChildMenu = _mapper.Map<List<ViewGetEditRoleMenuAccess>>(menuList.Where(y => y.ModuleId == x.MenuId && y.IsModule == false).ToList())
            }).ToList();

            // This is 2nd Commit
        }

        #endregion

        #region Helper Methods

        private void FillEntity(RoleMenu obj)
        {
            if (obj.RoleMenuId == Guid.Empty)
            {
                obj.RoleMenuId = Guid.NewGuid();
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
        private void FillEntityDelete(RoleMenu obj)
        {
            if (obj != null)
            {
                obj.DeletedBy = _tokenService.GetUserId();
                obj.DeletedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
            }

        }


        private async Task<List<ViewGetEditRoleMenuAccess>> GetRoleMenuAccess(Guid? RoleId)
        {
            using (var db = new NextShotContext())
            {
                var conn = _uowRoleMenu.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPGetEditRoleMenuAccess", (SqlConnection)conn);
                    sqlComm.Parameters.AddWithValue("@RoleId", RoleId);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<ViewGetEditRoleMenuAccess> lst = ds.Tables[0].ToList<ViewGetEditRoleMenuAccess>();
                    return lst;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        #endregion
    }
}
