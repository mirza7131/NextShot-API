using AppCommonMethods;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.ErrorLogDto;
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
    public class ErrorLogService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<ErrorLog> _uowErrorLog;

        #endregion

        #region Constructor

        public ErrorLogService(TokenService tokenService, UnitOfWork<ErrorLog> uowErrorLog, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowErrorLog = uowErrorLog;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditErrorLogDto> CreateOrEdit(CreateOrEditErrorLogDto input)
        {
            if (AppCommonMethod.IsNullorZerolong(input.ErrorLogId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditErrorLogDto> Create(CreateOrEditErrorLogDto input)
        {
            var obj = _mapper.Map<ErrorLog>(input);
            FillEntity(obj);
            ErrorLog? responseObj = await _uowErrorLog.Repository.Insert(obj);
            await _uowErrorLog.CommitAsync();
            return _mapper.Map<CreateOrEditErrorLogDto>(responseObj);
        }

        private async Task<CreateOrEditErrorLogDto> Update(CreateOrEditErrorLogDto input)
        {

            var dbObj = await _uowErrorLog.Repository.GetById(input.ErrorLogId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowErrorLog.Repository.Update(obj!);
            await _uowErrorLog.CommitAsync();
            return _mapper.Map<CreateOrEditErrorLogDto>(obj);

        }

        //public async Task<bool> Delete(object Id)
        //{
        //    var dbObj = await _uowErrorLog.Repository.GetById(Id);

        //    if (AppCommonMethod.IsNullObject(dbObj))
        //        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

        //    FillEntityDelete(dbObj!);

        //    _uowErrorLog.Repository.Update(dbObj!);
        //    await _uowErrorLog.CommitAsync();
        //    return true;
        //}


        #endregion

        #region Read Operations

        public async Task<List<ViewErrorLogDto>> GetAll(Expression<Func<ErrorLog, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            List<ErrorLog> responseObj = await _uowErrorLog.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewErrorLogDto>>(responseObj);
        }


        public async Task<ViewErrorLogDto> GetById(long input)
        {
            ErrorLog? responseObj = await _uowErrorLog.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewErrorLogDto>(responseObj);
        }

        #endregion

        #region Helper Methods

        private void FillEntity(ErrorLog obj)
        {
            if (obj.ErrorLogId.Equals(0))
            {
                //obj.ErrorLogId = Guid.NewGuid();
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
        //private void FillEntityDelete(ErrorLog obj)
        //{
        //    if (obj != null)
        //    {
        //        obj.DeletedBy = _tokenService.GetUserId();
        //        obj.DeletedOn = DateTime.Now;
        //        obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        //    }

        //}

        #endregion
    }
}
