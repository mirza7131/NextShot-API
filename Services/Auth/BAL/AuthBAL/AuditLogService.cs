using AppCommonMethods;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.AuditDto;
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
    public class AuditLogService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<AuditLog> _uowAuditLog;

        #endregion

        #region Constructor

        public AuditLogService(TokenService tokenService, UnitOfWork<AuditLog> uowAuditLog, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowAuditLog = uowAuditLog;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditAuditLogDto> CreateOrEdit(CreateOrEditAuditLogDto input)
        { 
            if(AppCommonMethod.IsNullorZerolong(input.AuditLogId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditAuditLogDto> Create(CreateOrEditAuditLogDto input)
        {
            var obj = _mapper.Map<AuditLog>(input);
            FillEntity(obj);
            AuditLog responseObj = await _uowAuditLog.Repository.Insert(obj);
            await _uowAuditLog.CommitAsync();
            return _mapper.Map<CreateOrEditAuditLogDto>(responseObj);
        }

        private async Task<CreateOrEditAuditLogDto> Update(CreateOrEditAuditLogDto input)
        {
            var dbObj = await _uowAuditLog.Repository.GetById(input.AuditLogId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);
            _uowAuditLog.Repository.Update(obj!);
            await _uowAuditLog.CommitAsync();
            return _mapper.Map<CreateOrEditAuditLogDto>(obj);
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

        public async Task<List<ViewAuditLogDto>> GetAll(Expression<Func<AuditLog, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<AuditLog> responseObj = await _uowAuditLog.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewAuditLogDto>>(responseObj);
        }


        public async Task<ViewAuditLogDto> GetById(long input)
        {
            AuditLog? responseObj = await _uowAuditLog.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewAuditLogDto>(responseObj);
        }


        #endregion

        #region Helper Methods

        private void FillEntity(AuditLog obj)
        {
            if (obj.AuditLogId.Equals(0))
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
