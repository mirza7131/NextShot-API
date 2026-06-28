using AppCommonMethods;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.DataSyncToOfflineDto;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HealthFacilityBAL;
using HMIS.Aggregator.API;
using HMIS.Aggregator.API.Models.Dto.DataSyncToOffline;
using JWTAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthBAL
{
    public class DataSyncToOfflineService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<DataSyncToOffline> _uowDataSyncToOffline;
        private readonly AuthCommonService _authCommonService;
        private string[] _dbTablestoSync; // Tables to Sync

        private readonly ProvinceService<Province> _provinceService;
        private readonly DivisionService<Division> _divisionService;
        private readonly DistrictService<District> _districtService;
        private readonly TehsilService<Tehsil> _tehsilService;
        //private readonly UnionCouncilService<UnionCouncil> _unionCouncilService;
        private readonly HealthFacilityService<HealthFacility> _healthFacilityService;
        private readonly HfDepartmentService<HfDepartment> _hfDepartmentService;
        private readonly HfDepartmentSectionService<HfDepartmentSection> _hfDepartmentSectionService;

        #endregion

        #region Constructor

        public DataSyncToOfflineService(TokenService tokenService, UnitOfWork<DataSyncToOffline> uowDataSyncToOffline, IMapper mapper, IConfiguration config, AuthCommonService authCommonService,
            ProvinceService<Province> provinceService,
            DivisionService<Division> divisionService,
            DistrictService<District> districtService,
            TehsilService<Tehsil> tehsilService,
            HealthFacilityService<HealthFacility> healthFacilityService,
            HfDepartmentService<HfDepartment> hfDepartmentService,
            HfDepartmentSectionService<HfDepartmentSection> hfDepartmentSectionService
            )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowDataSyncToOffline = uowDataSyncToOffline;
            _dbTablestoSync = config.GetSection("DbTablesSyncToOffline").Get<string[]>() ?? new string[0];
            _authCommonService = authCommonService; 
            _provinceService = provinceService;
            _divisionService = divisionService;
            _districtService = districtService;
            _tehsilService = tehsilService;
            _healthFacilityService = healthFacilityService; 
            _hfDepartmentService = hfDepartmentService;
            _hfDepartmentSectionService = hfDepartmentSectionService;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditDataSyncToOfflineDto> CreateOrEdit(CreateOrEditDataSyncToOfflineDto input)
        {
            if (AppCommonMethod.IsNullorZerolong(input.DataSyncToOfflineId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditDataSyncToOfflineDto> Create(CreateOrEditDataSyncToOfflineDto input)
        {
            var obj = _mapper.Map<DataSyncToOffline>(input);
            FillEntity(obj);
            DataSyncToOffline responseObj = await _uowDataSyncToOffline.Repository.Insert(obj);
            await _uowDataSyncToOffline.CommitAsync();
            return _mapper.Map<CreateOrEditDataSyncToOfflineDto>(responseObj);
        }

        private async Task<CreateOrEditDataSyncToOfflineDto> Update(CreateOrEditDataSyncToOfflineDto input)
        {
            var dbObj = await _uowDataSyncToOffline.Repository.GetById(input.DataSyncToOfflineId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowDataSyncToOffline.Repository.Update(obj!);
            await _uowDataSyncToOffline.CommitAsync();

            return _mapper.Map<CreateOrEditDataSyncToOfflineDto>(obj);
        }

        public async Task<List<CreateOrEditDataSyncToOfflineDto>> CreateOrEditList(List<CreateOrEditDataSyncToOfflineDto> input)
        {
            var _dbContext = _uowDataSyncToOffline.GetDbContext();

            var dbObj = await _dbContext.DataSyncToOfflines.Where(x => x.HealthFacilityId == input[0].HealthFacilityId!).ToListAsync();

            //if (AppCommonMethod.IsNullOrEmptyList(dbObj))
            //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            // delete 
            //foreach (var item in dbObj.ToList())
            //{
            //    if (!input.Any(c => c.DataSyncToOfflineId == item.DataSyncToOfflineId))
            //    {
            //        FillEntityDelete(item);
            //        _dbContext.Update(item);
            //    }
            //}

            // Update and Insert 
            foreach (var item in input.ToList())
            {
                var dbItem = dbObj
                    .Where(c => c.HealthFacilityId == item.HealthFacilityId && c.TableName == item.TableName)
                    .SingleOrDefault();

                if (dbItem != null)
                {
                    // Update Child
                    item.DataSyncToOfflineId = dbItem.DataSyncToOfflineId;
                    item.PrevSyncOn = dbItem.SyncOn;
                    var objItem = _mapper.Map(item, dbItem);
                    
                    FillEntity(objItem);
                    
                    _dbContext.Update(objItem);
                }
                else
                {
                    // Insert child
                    var objItem = _mapper.Map<DataSyncToOffline>(item);
                    
                    FillEntity(objItem);

                    dbObj.Add(objItem);
                    _dbContext.Add(objItem);
                }
            }

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<List<CreateOrEditDataSyncToOfflineDto>>(dbObj);

        }

        //public async Task<bool> Delete(object Id)
        //{
        //    var dbObj = await _uowDataSyncToOffline.Repository.GetById(Id);

        //    if (AppCommonMethod.IsNullObject(dbObj))
        //        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

        //    FillEntityDelete(dbObj!);

        //    _uowDataSyncToOffline.Repository.Update(dbObj!);
        //    await _uowDataSyncToOffline.CommitAsync();
        //    return true;
        //}

        public async Task<string> SyncData(FilterDataSyncToOfflineDto filter)
        {
            try
            {
                string message = "";

                var SyncOn = DateTime.Now;
                //ResponseDataSyncToOfflineDto responseObj = new ResponseDataSyncToOfflineDto();
                List<CreateOrEditDataSyncToOfflineDto> objCreateOrEditDataSyncToOffline = new List<CreateOrEditDataSyncToOfflineDto>();

                FilterSyncToOfflineDto objGetData = new FilterSyncToOfflineDto();
                message += "Calling Cloud API to get data";
                objGetData.HealthFacilityId = filter.HealthFacilityId;
                var responseData = await _authCommonService.GetDataToSync(objGetData);
                message += "--> Data is received From Cloud API";
                var arrayLength = _dbTablestoSync.Length;

                
                var syncStatus = new string[arrayLength]; // declaring Lenght of Array

                message += "--> Starting Local Data Insert";
                foreach (var item in _dbTablestoSync.Select((value, index) => new { value, index }))
                {
                    var msg = "--> Starting Working on " + item.value;
                    CreateOrEditDataSyncToOfflineDto obj = new CreateOrEditDataSyncToOfflineDto();

                    if (!string.IsNullOrEmpty(responseData[item.index]))
                    {
                        msg += "--> Calling SP for " + item.value;
                        var response = await InsertOrUpdateSyncedData(item.value, responseData[item.index]);
                        if(response)
                            msg += "--> Response Success Received for " + item.value;
                        else
                            msg += "--> Response Error Received for " + item.value;

                        obj.HealthFacilityId = filter.HealthFacilityId;
                        obj.TableName = item.value;
                        obj.SyncOn = SyncOn;
                        obj.Json = responseData[item.index];
                        obj.Type = CommonStringConstant.SyncToOfflineType;
                        if (response)
                            obj.Status = CommonStringConstant.SyncDataInserted_Completed;
                        else
                            obj.Status = CommonStringConstant.SyncDataInserted_Error;

                        msg += "--> Object Created for log " + item.value;
                        obj.WorkFlow = msg;
                        objCreateOrEditDataSyncToOffline.Add(obj);

                    }
                    msg += "--> Complete Working on " + item.value;
                    message += msg;
                }
                message += "--> Startig insert Log ";
                var res = new List<CreateOrEditDataSyncToOfflineDto>();
                if (!AppCommonMethod.IsNullOrEmptyList(objCreateOrEditDataSyncToOffline))
                    res = await CreateOrEditList(objCreateOrEditDataSyncToOffline);
                //return _mapper.Map<ResponseDataSyncToOfflineDto>(responseObj);
                message += "--> After Insert Log";

                await _provinceService.RefreshProvinceStaticCacheList();
                await _divisionService.RefreshDivisionStaticCacheList();
                await _districtService.RefreshDistrictStaticCacheList();
                await _tehsilService.RefreshTehsilStaticCacheList();
                await _healthFacilityService.RefreshHealthFacilityStaticCacheList();
                await _hfDepartmentService.RefreshHfDepartmentStaticCacheList();
                await _hfDepartmentSectionService.RefreshHfDepartmentSectionStaticCacheList();

                return message;
                
            }
            catch (Exception ex)
            {
                throw ;
            }
        }


        #endregion

        #region Read Operations

        //public async Task<List<ViewDataSyncToOfflineDto>> GetAll(Expression<Func<DataSyncToOffline, bool>>? filter = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>
        //    ? orderBy = null,
        //    string includeProperties = "")
        //{
        //    List<DataSyncToOffline> responseObj = await _uowDataSyncToOffline.Repository.GetALL(filter).ToListAsync();
        //    return _mapper.Map<List<ViewDataSyncToOfflineDto>>(responseObj);
        //}


        //public async Task<ViewPagerDto<ViewDataSyncToOfflineDto>> GetAllWithPagination(FilterDataSyncToOfflineDto filter)
        //{
        //    var list = _uowDataSyncToOffline.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
        //        .WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.Name.ToLower().StartsWith(filter.SearchString) || x.ShortName.ToLower().StartsWith(filter.SearchString))
        //        .OrderByDescending(x => x.CreatedOn);

        //    IQueryable<ViewDataSyncToOfflineDto> IQueryableList = list.Select(x =>
        //        new ViewDataSyncToOfflineDto
        //        {
        //            DataSyncToOfflineId = x.DataSyncToOfflineId,
        //            Name = x.Name,
        //            ShortName = x.ShortName,
        //            IsActive = x.IsActive
        //        });

        //    var pagedList = await PagedListDto<ViewDataSyncToOfflineDto>.ToPagedListAsync(
        //           IQueryableList,
        //           filter.PageNumber,
        //           filter.PageSize
        //           );

        //    var responseObject = new ViewPagerDto<ViewDataSyncToOfflineDto>
        //    {
        //        TotalCount = pagedList.TotalCount,
        //        PageSize = pagedList.PageSize,
        //        CurrentPage = pagedList.CurrentPage,
        //        TotalPages = pagedList.TotalPages,
        //        HasNext = pagedList.HasNext,
        //        HasPrevious = pagedList.HasPrevious,
        //        List = _mapper.Map<List<ViewDataSyncToOfflineDto>>(pagedList)
        //    };

        //    return responseObject;
        //}

        //public async Task<ViewDataSyncToOfflineDto> GetById(Guid input)
        //{
        //    DataSyncToOffline? responseObj = await _uowDataSyncToOffline.Repository.GetById(input);

        //    if (AppCommonMethod.IsNullObject(responseObj))
        //        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

        //    return _mapper.Map<ViewDataSyncToOfflineDto>(responseObj);
        //}


        public async Task<ResponseDataSyncToOfflineDto> GetDataToSync(FilterDataSyncToOfflineDto filter)
        {
            var SyncOn = DateTime.Now;
            ResponseDataSyncToOfflineDto responseObj = new ResponseDataSyncToOfflineDto();

            List<CreateOrEditDataSyncToOfflineDto> objCreateOrEditDataSyncToOffline = new List<CreateOrEditDataSyncToOfflineDto>();

            var arrayLength = _dbTablestoSync.Length;
            responseObj.JsonData = new string[arrayLength]; // declaring Lenght of Array

            foreach (var item in _dbTablestoSync.Select((value, index) => new { value, index }))
            {
                CreateOrEditDataSyncToOfflineDto obj = new CreateOrEditDataSyncToOfflineDto();
                responseObj.JsonData[item.index] = await GetJsonDataFromSP(item.index, item.value, filter.HealthFacilityId, SyncOn);

                obj.HealthFacilityId = filter.HealthFacilityId;
                obj.TableName = item.value;
                obj.SyncOn = SyncOn;
                obj.Json = responseObj.JsonData[item.index];
                obj.Type = CommonStringConstant.SyncToOnlineType;
                obj.Status = CommonStringConstant.SyncDataFethed_Completed;
                obj.WorkFlow = CommonStringConstant.SyncDataFethed_WorkFlow;

                objCreateOrEditDataSyncToOffline.Add(obj);
            }

            var res = await CreateOrEditList(objCreateOrEditDataSyncToOffline);

            return _mapper.Map<ResponseDataSyncToOfflineDto>(responseObj);
        }


        #endregion

        #region Helper Methods

        private void FillEntity(DataSyncToOffline obj)
        {
            if (AppCommonMethod.IsNullorZerolong(obj.DataSyncToOfflineId))
            {
                //obj.DataSyncToOfflineId = Guid.NewGuid();
                obj.ActionOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.ActionOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }
        private void FillEntityDelete(DataSyncToOffline obj)
        {
            obj.ActionOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        public async Task<String> GetJsonDataFromSP(int type, string tableName, int healthFacilityId, DateTime? syncOn)
        {
           
            var conn = _uowDataSyncToOffline.GetDbContext().Database.GetDbConnection();
            try
            {
                //var dt = DateTime.Now.ToString();
                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand("[sync].[SPGetDataByHealthFacilityTableAndDateTimeWise]", (SqlConnection)conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                if (!string.IsNullOrEmpty(tableName))
                    sqlComm.Parameters.AddWithValue("@Type", type);
                if (!string.IsNullOrEmpty(tableName))
                    sqlComm.Parameters.AddWithValue("@TableName", tableName);
                if (!string.IsNullOrEmpty(tableName))
                    sqlComm.Parameters.AddWithValue("@HealthFacilityId", healthFacilityId);
                if (!string.IsNullOrEmpty(tableName))
                    sqlComm.Parameters.AddWithValue("@SyncOn", syncOn);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                await Task.Run(() => da.Fill(ds));
                List<ResponseDataSyncFromSPDto> lst = ds.Tables[0].ToList<ResponseDataSyncFromSPDto>();

                return lst[0].JsonData;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> InsertOrUpdateSyncedData(string tableName, string Json)
        {
           
            var conn = _uowDataSyncToOffline.GetDbContext().Database.GetDbConnection();
            try
            {
                if (tableName.Contains('.'))
                {
                    string[] parts = tableName.Split('.');
                    tableName = parts[1];
                }

                //"SPMedicineDispatchInsertOrUpdateData"
                string spName = "[sync].[SP" + tableName + "InsertOrUpdateData]";

                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand(spName, (SqlConnection)conn);
                sqlComm.CommandTimeout = 60000; // seconds
                sqlComm.CommandType = CommandType.StoredProcedure;

                if (!string.IsNullOrEmpty(Json))
                    sqlComm.Parameters.AddWithValue("@json", Json);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                await Task.Run(() => da.Fill(ds));
                List<ResponseSyncDataInsertOrUpdateDto> lst = ds.Tables[0].ToList<ResponseSyncDataInsertOrUpdateDto>();

                return lst[0].Response;
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }

        #endregion
    }
}
