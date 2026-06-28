using AppCommonMethods;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.UserLogDto;
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
    public class UserLogService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<UserLog> _uowUserLog;

        #endregion

        #region Constructor

        public UserLogService(TokenService tokenService, UnitOfWork<UserLog> uowUserLog, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowUserLog = uowUserLog;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditUserLogDto> CreateOrEdit(CreateOrEditUserLogDto input)
        {
            if (AppCommonMethod.IsNullorZerolong(input.UserLogId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditUserLogDto> Create(CreateOrEditUserLogDto input)
        {
            var obj = _mapper.Map<UserLog>(input);
            FillEntity(obj);
            UserLog responseObj = await _uowUserLog.Repository.Insert(obj);
            await _uowUserLog.CommitAsync();
            return _mapper.Map<CreateOrEditUserLogDto>(responseObj);
        }

        private async Task<CreateOrEditUserLogDto> Update(CreateOrEditUserLogDto input)
        {
            var dbObj = await _uowUserLog.Repository.GetById(input.UserLogId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);
            _uowUserLog.Repository.Update(obj!);
            await _uowUserLog.CommitAsync();
            return _mapper.Map<CreateOrEditUserLogDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowUserLog.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowUserLog.Repository.Update(dbObj!);
            await _uowUserLog.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewUserLogDto>> GetAll(Expression<Func<UserLog, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<UserLog> responseObj = await _uowUserLog.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewUserLogDto>>(responseObj);
        }


        public async Task<ViewUserLogDto> GetById(long input)
        {
            UserLog? responseObj = await _uowUserLog.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewUserLogDto>(responseObj);
        }

        #endregion

        #region Helper Methods

        private void FillEntity(UserLog obj)
        {
            if (obj.UserLogId.Equals(0))
            {
                //obj.UserLogId = Guid.NewGuid();
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
        private void FillEntityDelete(UserLog obj)
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
