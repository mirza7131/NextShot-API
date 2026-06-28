using AppCommonMethods;
using AuthBAL.Common;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.DataSyncUtilityLog;
using AuthDAL.Models.Dto.PaginationDto;
using AuthDAL.Repositories;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuthBAL
{
    public class DataSyncUtilityLogService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<DataSyncUtilityLog> _uowDataSyncUtilityLog;

        #endregion

        #region Constructor

        public DataSyncUtilityLogService(TokenService tokenService, UnitOfWork<DataSyncUtilityLog> uowDataSyncUtilityLog, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowDataSyncUtilityLog = uowDataSyncUtilityLog;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditDataSyncUtilityLogDto> CreateOrEdit(CreateOrEditDataSyncUtilityLogDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.DataSyncUtilityLogId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditDataSyncUtilityLogDto> Create(CreateOrEditDataSyncUtilityLogDto input)
        {
            var obj = _mapper.Map<DataSyncUtilityLog>(input);
            FillEntity(obj);
            DataSyncUtilityLog responseObj = await _uowDataSyncUtilityLog.Repository.Insert(obj);
            await _uowDataSyncUtilityLog.CommitAsync();
            return _mapper.Map<CreateOrEditDataSyncUtilityLogDto>(responseObj);
        }

        private async Task<CreateOrEditDataSyncUtilityLogDto> Update(CreateOrEditDataSyncUtilityLogDto input)
        {
            var dbObj = await _uowDataSyncUtilityLog.Repository.GetById(input.DataSyncUtilityLogId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowDataSyncUtilityLog.Repository.Update(obj!);
            await _uowDataSyncUtilityLog.CommitAsync();

            return _mapper.Map<CreateOrEditDataSyncUtilityLogDto>(obj);
        }

        public async Task<CreateOrEditDataSyncUtilityLogDto> CreateDataSyncLogInfo(CreateOrEditDataSyncUtilityLogDto input)
        {
            DataSyncUtilityLog obj = new DataSyncUtilityLog();
            //var _uowDataSyncUtilityLog = new UnitOfWork<DataSyncUtilityLog>(_uowPatient.GetDbContext());

            var dbObj = await _uowDataSyncUtilityLog.Repository.GetALL(x => x.FileName == input.FileName && x.ServerType != "Offline").SingleOrDefaultAsync();

            if (!AppCommonMethod.IsNullObject(dbObj))
                obj = dbObj;
            else
            {
                obj = _mapper.Map<DataSyncUtilityLog>(input);
                obj.DataSyncUtilityLogId = Guid.NewGuid();
            }

            if (!string.IsNullOrEmpty(input.FileName))
            {
                string[] parts = input.FileName.Split('_');

                obj.HealthFacilityId = (parts.Length > 2) ? int.Parse(parts[1]) : int.Parse(parts[0]);
            }


            obj.ServerType = "Online";
            obj.FileName = input.FileName;
            //obj.FileSize = fileSize.ToString();
            obj.Status = input.Status;
            obj.StatusUpdatedOn = DateTime.Now;

            obj.UploadedOn = (obj.UploadedOn == null) ? obj.UploadedOn : input.UploadedOn;
            obj.ProcessedOn = (obj.ProcessedOn == null) ? obj.ProcessedOn : null;
            if (obj.Status == (int)DataSyncLogStatus.Completed)
            {
                obj.CompletedOn = DateTime.Now;
                obj.Message = null;
            }
            else if (obj.Status == (int)DataSyncLogStatus.Error)
                obj.Message = input.Message;

            if (AppCommonMethod.IsNullObject(dbObj))
                await _uowDataSyncUtilityLog.Repository.Insert(obj);
            else
                _uowDataSyncUtilityLog.Repository.Update(obj);

            await _uowDataSyncUtilityLog.CommitAsync();

            return _mapper.Map<CreateOrEditDataSyncUtilityLogDto>(obj);
        }

        //public async Task<bool> Delete(object Id)
        //{
        //    var dbObj = await _uowDataSyncUtilityLog.Repository.GetById(Id);

        //    if (AppCommonMethod.IsNullObject(dbObj))
        //        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

        //    FillEntityDelete(dbObj!);

        //    _uowDataSyncUtilityLog.Repository.Update(dbObj!);
        //    await _uowDataSyncUtilityLog.CommitAsync();
        //    return true;
        //}


        #endregion

        #region Read Operations

        public async Task<List<ViewDataSyncUtilityLogDto>> GetAll(Expression<Func<DataSyncUtilityLog, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>
            ? orderBy = null,
            string includeProperties = "")
        {
            List<DataSyncUtilityLog> responseObj = await _uowDataSyncUtilityLog.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewDataSyncUtilityLogDto>>(responseObj);
        }


       

        public async Task<ViewDataSyncUtilityLogDto> GetById(Guid input)
        {
            DataSyncUtilityLog? responseObj = await _uowDataSyncUtilityLog.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewDataSyncUtilityLogDto>(responseObj);
        }


        #endregion

        #region Helper Methods

        private void FillEntity(DataSyncUtilityLog obj)
        {
            if (obj.DataSyncUtilityLogId == Guid.Empty)
            {
                obj.DataSyncUtilityLogId = Guid.NewGuid();
                obj.Status = (int)DataSyncLogStatus.Pending;
                obj.StatusUpdatedOn = DateTime.Now;
            }
        }
        //private void FillEntityDelete(DataSyncUtilityLog obj)
        //{
        //    obj.DeletedBy = _tokenService.GetUserId();
        //    obj.DeletedOn = DateTime.Now;
        //    obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        //}

        #endregion
    }
}
