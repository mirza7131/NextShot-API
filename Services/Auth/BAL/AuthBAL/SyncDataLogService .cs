using AppCommonMethods;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.AuditDto;
using AuthDAL.Models.Dto.SyncDataLogDto;
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
    public class SyncDataLogService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<SyncDataLog> _uowSyncDataLog;

        #endregion

        #region Constructor

        public SyncDataLogService(TokenService tokenService, UnitOfWork<SyncDataLog> uowSyncDataLog, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowSyncDataLog = uowSyncDataLog;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditSyncDataLogDto> CreateOrEdit(CreateOrEditSyncDataLogDto input)
        { 
            if(AppCommonMethod.IsNullorZerolong(input.SyncDataLogId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditSyncDataLogDto> Create(CreateOrEditSyncDataLogDto input)
        {
            var obj = _mapper.Map<SyncDataLog>(input);
            FillEntity(obj);
            SyncDataLog responseObj = await _uowSyncDataLog.Repository.Insert(obj);
            await _uowSyncDataLog.CommitAsync();
            return _mapper.Map<CreateOrEditSyncDataLogDto>(responseObj);
        }

        private async Task<CreateOrEditSyncDataLogDto> Update(CreateOrEditSyncDataLogDto input)
        {
            var dbObj = await _uowSyncDataLog.Repository.GetById(input.SyncDataLogId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);
            _uowSyncDataLog.Repository.Update(obj!);
            await _uowSyncDataLog.CommitAsync();
            return _mapper.Map<CreateOrEditSyncDataLogDto>(obj);
        }

        //public async Task<bool> Delete(object Id)
        //{
        //    var dbObj = await _uowAuditLog.Repository.GetById(Id);

        //    if (AppCommonMethod.IsNullObject(dbObj))
        //        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

        //    FillEntityDelete(dbObj!);

        //    _uowAuditLog.Repository.Update(dbObj!);
        //    await _uowAuditLog.CommitAsync();
        //    return true;
        //}


        #endregion

        #region Read Operations

        public async Task<List<ViewSyncDataLogDto>> GetAll(Expression<Func<SyncDataLog, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<SyncDataLog> responseObj = await _uowSyncDataLog.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewSyncDataLogDto>>(responseObj);
        }


        public async Task<ViewSyncDataLogDto> GetById(long input)
        {
            SyncDataLog? responseObj = await _uowSyncDataLog.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewSyncDataLogDto>(responseObj);
        }


        #endregion

        #region Helper Methods

        private void FillEntity(SyncDataLog obj)
        {
            if (obj.SyncDataLogId.Equals(0))
            {
                //obj.AuditLogId = Guid.NewGuid();
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
        //private void FillEntityDelete(AuditLog obj)
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
