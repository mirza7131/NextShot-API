using AppCommonMethods;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.UserRole;
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
    public class UserRoleService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<UserRole> _uowUserRole;

        #endregion

        #region Constructor

        public UserRoleService(TokenService tokenService, UserRoleRepository<TEntity> UserRoleRepository, UnitOfWork<UserRole> uowUserRole, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowUserRole = uowUserRole;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditUserRoleDto> CreateOrEdit(CreateOrEditUserRoleDto input)
        {
            if (AppCommonMethods.AppCommonMethod.IsNullOrEmptyGuid(input.UserRoleId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditUserRoleDto> Create(CreateOrEditUserRoleDto input)
        {
            var obj = _mapper.Map<UserRole>(input);
            FillEntity(obj);
            UserRole responseObj = await _uowUserRole.Repository.Insert(obj);
            await _uowUserRole.CommitAsync();
            return _mapper.Map<CreateOrEditUserRoleDto>(responseObj);
        }

        private async Task<CreateOrEditUserRoleDto> Update(CreateOrEditUserRoleDto input)
        {
            var dbObj = await _uowUserRole.Repository.GetById(input.UserRoleId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);
            _uowUserRole.Repository.Update(obj!);
            await _uowUserRole.CommitAsync();

            return _mapper.Map<CreateOrEditUserRoleDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowUserRole.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowUserRole.Repository.Update(dbObj!);
            await _uowUserRole.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewUserRoleDto>> GetAll(Expression<Func<UserRole, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<UserRole> responseObj = await _uowUserRole.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewUserRoleDto>>(responseObj);
        }


        public async Task<ViewUserRoleDto> GetById(Guid input)
        {
            UserRole? responseObj = await _uowUserRole.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewUserRoleDto>(responseObj);
        }


        #endregion

        #region Helper Methods

        private void FillEntity(UserRole obj)
        {
            if (obj.UserRoleId == Guid.Empty)
            {
                obj.UserRoleId = Guid.NewGuid();
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
        private void FillEntityDelete(UserRole obj)
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
