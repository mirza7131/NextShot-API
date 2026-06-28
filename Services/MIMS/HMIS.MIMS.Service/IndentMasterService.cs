using AppCommonMethods;
//using AuthDAL.Models.Dto.ProfileDto;
using AutoMapper.Execution;
using Azure;
using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Aggregator.API.Models.MIMS;
using HMIS.Aggregator.API.Services;
using HMIS.MIMS.Domain.Models.DbModels;
using HMIS.MIMS.Domain.Models.DTO.IndentDetailDto;
using HMIS.MIMS.Domain.Models.DTO.IndentMasterDTO;
using HMIS.MIMS.Domain.Models.DTO.PaginationDto;
using HMIS.MIMS.Domain.Models.Repositories._UOW;
using JWTAuthentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RedisCache;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;
using static HMIS.MIMS.Domain.Models.DTO.IndentMasterDTO.UnsyncIndentListDTO;
using autoMapper = AutoMapper;

namespace HMIS.MIMS.Service
{
    public class IndentMasterService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly autoMapper.IMapper _mapper;
        private UnitOfWork<IndentMaster> _uowIndentMaster;
        private readonly MIMSService _mimsService;
        private readonly string _mimsBaseUrl;
        private readonly bool _isDevelopment;
        private readonly IRedisCacheService _cacheService;

        #endregion

        #region Constructor

        public IndentMasterService(
            TokenService tokenService,
            UnitOfWork<IndentMaster> uowIndentMaster,
            MIMSService mimsService,

            autoMapper.IMapper mapper,
            IRedisCacheService redisCacheService,
            IConfiguration config
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowIndentMaster = uowIndentMaster;
            _mimsService = mimsService;
            _cacheService = redisCacheService;

            _isDevelopment = config.GetValue<bool>("IsDevelopment") ? config.GetValue<bool>("IsDevelopment") : false;
            if (_isDevelopment)
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Dev").GetSection("MIMS").Value ?? string.Empty;
            else
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Prod").GetSection("MIMS").Value ?? string.Empty;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditIndentMasterDto> CreateOrEdit(CreateOrEditIndentMasterDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.IndentMasterId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditIndentMasterDto> Create(CreateOrEditIndentMasterDto input)
        {
            var obj = _mapper.Map<IndentMaster>(input);
            var objDetail = _mapper.Map<List<IndentDetail>>(input.IndentDetails);
            await FillEntityAsync(obj);

            if (!AppCommonMethod.IsNullOrEmptyList<IndentDetail>(objDetail!))
            {
                foreach (var item in objDetail)
                {
                    FillEntityIndentDetail(item);
                }
            }

            using (var db = _uowIndentMaster.GetDbContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[mims].[SPCreateIndent]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    // Converting date to acceptable format
                    var options = new JsonSerializerOptions();
                    options.Converters.Add(new CustomDateTimeConverter());

                    var objJson = sqlComm.Parameters.AddWithValue("@json", JsonSerializer.Serialize(obj, options));
                    var objJsonDetail = sqlComm.Parameters.AddWithValue("@jsonDetail", JsonSerializer.Serialize(objDetail, options));

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;

                    await Task.Run(() => da.Fill(ds));

                    var res = ds.Tables[0].ToList<ResponseCreateIndent>();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return input;
        }

        private async Task FillEntityAsync(IndentMaster obj)
        {
            if (obj.IndentMasterId == Guid.Empty)
            {
                obj.IndentMasterId = Guid.NewGuid();
                obj.HealthfacilityId = TokenService.GetUserHfId();
                obj.IsActive = true;
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                //obj.CreatedOn = new DateTime(2024, 03, 03, 16, 31, 22, 387); // Hardcoded start date
                obj.ActionTypeId = (int)ActionTypeEnum.Create;

                // Extract month and year components
                string sDate = obj.CreatedOn.ToString();
                DateTime datevalue = Convert.ToDateTime(sDate);
                string currentMonth = datevalue.Month.ToString("D2"); // Use "D2" format to ensure leading zeros for month
                string currentYear = datevalue.Year.ToString();
                if (currentYear.Length >= 2)
                {
                    currentYear = currentYear.Substring(currentYear.Length - 2); // Use Substring to get last two digits of year
                }

                // Generate IndentNumber
                var latestSequence = await GetLatestIndentSequenceAsync(TokenService.GetUserHfId());
                int newSequence;

                string latestMonth = string.Empty;
                string latestSequenceNumber = string.Empty;

                if (!string.IsNullOrEmpty(latestSequence))
                {
                    var parts = latestSequence.Split('-');
                    if (parts.Length == 3)
                    {
                        latestMonth = parts[1].Substring(0, 2); // Extract the month part
                        latestSequenceNumber = parts[2]; // Extract the sequence number part
                    }
                }

                // Check if the current month is different from the last created indent's month
                if (latestMonth != currentMonth)
                {
                    newSequence = 1; // Reset sequence to 1 for the new month
                }
                else
                {
                    newSequence = !string.IsNullOrEmpty(latestSequenceNumber) ? int.Parse(latestSequenceNumber) + 1 : 1; // Increment sequence or start new sequence
                }

                obj.IndentNumber = $"IND-{currentMonth}{currentYear}-{newSequence:D3}";
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }


        private async Task<CreateOrEditIndentMasterDto> Update(CreateOrEditIndentMasterDto input)
        {
            var dbObj = await _uowIndentMaster.Repository.GetById(input.IndentMasterId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowIndentMaster.Repository.Update(obj!);
            await _uowIndentMaster.CommitAsync();
            return _mapper.Map<CreateOrEditIndentMasterDto>(obj);
        }

        public async Task<bool> CreateIndent(List<IndentDetailDTO> indentDetails, int IndentNo = 0, string? indentIdsList = null)
        {
            // Incase of Opening Stock IndentId is Comma seprated String (Multiple Ids) and In Case of Single Indent use Input IndentNo
            var IndentId = "";
            var IsSynced = false;
            var tokenUser = TokenService.GetUserLoggedInfo();
            var j = JsonSerializer.Serialize(indentDetails);

            if (AppCommonMethod.IsNullorZeroInt(IndentNo))
                IndentId = indentIdsList;
            else
            {
                IndentId = IndentNo.ToString();
                var IndentExist = await _uowIndentMaster.Repository.GetALL(x => x.IndentNumber == IndentId && x.HealthfacilityId == tokenUser!.HealthFacilityId).FirstOrDefaultAsync();
                if (!AppCommonMethod.IsNullObject(IndentExist))
                    IsSynced = true;
            }

            List<ResponseCreateIndent> res = new List<ResponseCreateIndent>();

            // if Request Against Same INdent then ignore only Update Status on Mims and in IndentMaster
            if (!IsSynced) {
                using (var db = new HmisAuthContext())
                {
                    var conn = _uowIndentMaster.GetDbContext().Database.GetDbConnection();
                    try
                    {
                        DataSet ds = new DataSet();
                        SqlCommand sqlComm = new SqlCommand("[mims].[SPDumpMainStoreOpeningStoreHealthFacilityWiseFromJson]", (SqlConnection)conn);
                        sqlComm.CommandType = CommandType.StoredProcedure;

                        sqlComm.Parameters.AddWithValue("@json", JsonSerializer.Serialize(indentDetails));

                        if (!AppCommonMethod.IsNullOrEmptyGuid(tokenUser.UserId))
                            sqlComm.Parameters.AddWithValue("@UserId", tokenUser.UserId);

                        if (!AppCommonMethod.IsNullorZeroInt(tokenUser.HealthFacilityId))
                            sqlComm.Parameters.AddWithValue("@HealthFacilityId", tokenUser.HealthFacilityId);

                        if (!string.IsNullOrEmpty(IndentId))
                            sqlComm.Parameters.AddWithValue("@IndentNo", IndentId);

                        if (AppCommonMethod.IsNullorZeroInt(IndentNo))
                            sqlComm.Parameters.AddWithValue("@IsOpeningIndent", true);

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = sqlComm;

                        await Task.Run(() => da.Fill(ds));
                        res = ds.Tables[0].ToList<ResponseCreateIndent>();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                    finally
                    {
                        conn.Close();
                    }
                }
            }

            if (!AppCommonMethod.IsNullorZeroInt(IndentNo))
            {
                List<int> lst = new List<int> { IndentNo };
                LastUpdatedDateMIMsDTO indettypelist = await _mimsService.UpdateMainStoreIndentStatus(_mimsBaseUrl, lst);
                if (indettypelist.Status == true)
                    await UpdateIndentStatus(IndentNo);
            }

            if (!AppCommonMethod.IsNullOrEmptyList<ResponseCreateIndent>(res))
                return res.Select(x => x.Response).FirstOrDefault();
            else if (AppCommonMethod.IsNullOrEmptyList<ResponseCreateIndent>(res) && IsSynced)
                return true;
            else
                return false;

        }

        public async Task<bool> IsOpeningStockSynced()
        {
            var isAlreadySync = await _uowIndentMaster.Repository.GetALL(x => x.HealthfacilityId == TokenService.GetUserHfId() && x.IsOpeningStock == true).FirstOrDefaultAsync();

            if (!AppCommonMethod.IsNullObject(isAlreadySync))
                return true;
            else
                return false;
        }

        public async Task<List<IndentDetailDTO>> UpdateMainStoreOpeningStock()
        {
            var isAlreadySync = await IsOpeningStockSynced();

            if (isAlreadySync)
                throw new UserFriendlyException(CommonMessageConstant.OpeningStockSynced);

            var response = await _mimsService.GetMainStoreMedicineOpeningStockByHealthFacility(_mimsBaseUrl, TokenService.GetHfHrId());

            if (response.Data.Count > 0)
            {
                var res = await CreateIndent(response.Data, 0, response.IndentIdList);
                if (!res)
                    throw new UserFriendlyException(CommonMessageConstant.OpeningIndentNotSynced);
            }

            return response.Data;
        }

        public async Task<bool> UpdateMimsIndentStatus(UpdateIndentStatusDto input)
        {
            int listStatus = 0;
            List<UpdateIndentStatusDto> res = new List<UpdateIndentStatusDto>();
            using (var db = new HmisAuthContext())
            {
                var conn = _uowIndentMaster.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[mims].[SPUpdateIndentStatus]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    var json = sqlComm.Parameters.AddWithValue("@jsonDetail", JsonSerializer.Serialize(input.IndentDetails));

                    if (!AppCommonMethod.IsNullOrEmptyGuid(input.IndentMasterId))
                        sqlComm.Parameters.AddWithValue("@IndentMasterId", input.IndentMasterId);

                    if (!string.IsNullOrEmpty(input.StatusValue))
                    {
                        if (input.StatusValue == "Pending")
                        {
                            listStatus = 1;
                        } else if (input.StatusValue == "Received")
                        {
                            listStatus = 2;
                        }
                        sqlComm.Parameters.AddWithValue("@StatusValue", listStatus);
                    }

                    if (!string.IsNullOrEmpty(input.Remarks))
                        sqlComm.Parameters.AddWithValue("@Remarks", input.Remarks);

                    if (!AppCommonMethod.IsNullOrEmptyGuid(_tokenService.GetUserId()))
                        sqlComm.Parameters.AddWithValue("@UserId", _tokenService.GetUserId());

                    if (!AppCommonMethod.IsNullorZeroInt(TokenService.GetUserHfId()))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", TokenService.GetUserHfId());

                    if (!AppCommonMethod.IsNullOrEmptyGuid(TokenService.GetUserLoggedInfo()?.MimsBranchId))
                        sqlComm.Parameters.AddWithValue("@UserMimsBranchId", TokenService.GetUserLoggedInfo()?.MimsBranchId);

                    if (!AppCommonMethod.IsNullOrEmptyGuid(input.ToBranchId))
                        sqlComm.Parameters.AddWithValue("@RequestedMimsBranchId", input.ToBranchId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    Console.WriteLine(sqlComm.CommandText);
                    foreach (SqlParameter param in sqlComm.Parameters)
                    {
                        Console.WriteLine($"{param.ParameterName}: {param.Value}");
                    }
                    await Task.Run(() => da.Fill(ds));
                    //res = ds.Tables[0].ToList<UpdateIndentStatusDto>();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }

            return true;
        }


        public async Task<CreateOrEditIndentMasterDto> UpdateIndentStatus(int input)
        {
            var dbObj = await _uowIndentMaster.Repository.GetALL(x => x.IndentNumber == input.ToString()).FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            dbObj.IsMimsAcknowledged = true;
            //var obj = _mapper.Map(input, dbObj);
            FillEntity(dbObj!);

            _uowIndentMaster.Repository.Update(dbObj!);
            await _uowIndentMaster.CommitAsync();
            return _mapper.Map<CreateOrEditIndentMasterDto>(dbObj);
        }
        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowIndentMaster.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowIndentMaster.Repository.Update(dbObj!);
            await _uowIndentMaster.CommitAsync();
            return true;
        }

        #endregion

        #region Read Operations

        public async Task<List<ViewIndentMasterDto>> GetAll(Expression<Func<IndentMaster, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<IndentMaster> responseObj = await _uowIndentMaster.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewIndentMasterDto>>(responseObj);
        }

        //public async Task<ViewPagerDto<ViewIndentMasterDto>> GetAllWithPagination(FilterProfileDto filter)
        //{
        //    var list = _uowIndentMaster.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
        //        .OrderByDescending(x => x.CreatedOn);

        //    IQueryable<ViewIndentMasterDto> IQueryableList = list.Select(x =>
        //       new ViewIndentMasterDto
        //       {

        //           IndentMasterId = x.IndentMasterId,
        //           IndentNumber = x.IndentNumber,

        //       });

        //    var pagedList = await PagedListDto<ViewIndentMasterDto>.ToPagedListAsync(
        //           IQueryableList,
        //           filter.PageNumber,
        //           filter.PageSize
        //           );

        //    var responseObject = new ViewPagerDto<ViewIndentMasterDto>
        //    {
        //        TotalCount = pagedList.TotalCount,
        //        PageSize = pagedList.PageSize,
        //        CurrentPage = pagedList.CurrentPage,
        //        TotalPages = pagedList.TotalPages,
        //        HasNext = pagedList.HasNext,
        //        HasPrevious = pagedList.HasPrevious,
        //        List = pagedList
        //    };

        //    return responseObject;
        //}

        public async Task<ViewIndentMasterDto> GetById(Guid input)
        {
            IndentMaster? responseObj = await _uowIndentMaster.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewIndentMasterDto>(responseObj);
        }

        public async Task<ResponseUnSyncIndentListDto> GetUnSyncMainStoreMedicineIndentFromMims(string basUrl, string healthFacilityCode)
        {
            var list = await _mimsService.GetUnSyncMainStoreMedicineIndentFromMims(basUrl, healthFacilityCode);
            return _mapper.Map<ResponseUnSyncIndentListDto>(list);
        }

        public async Task<List<IndentDetailDTO>> SyncMainStoreIndentDetailForHMIS(string basUrl, string healthFacilityCode, int IndentId)
        {
            var list = await _mimsService.GetUnSyncMainStoreIndentDetailForHMIS(basUrl, healthFacilityCode, IndentId);
            if (!AppCommonMethod.IsNullOrEmptyList(list))
            {
                var res = await CreateIndent(list, IndentId);
                if (!res)
                    throw new UserFriendlyException(CommonMessageConstant.IndentNotSynced);
            }

            return _mapper.Map<List<IndentDetailDTO>>(list);
        }

        public async Task<List<IndentDetailDTO>> GetUnSyncMainStoreIndentDetailForHMIS(string basUrl, string healthFacilityCode, int IndentId)
        {
            var list = await _mimsService.GetUnSyncMainStoreIndentDetailForHMIS(basUrl, healthFacilityCode, IndentId);
            return _mapper.Map<List<IndentDetailDTO>>(list);
        }

        public async Task<ViewPagerDto<ViewIndentMasterListByStatusFromSPDto>> GetFilteredIndentListWithPagination(FilterIndentListDto? filter)
        {
            List<ViewIndentMasterListByStatusFromSPDto> lst = new List<ViewIndentMasterListByStatusFromSPDto>();
            var responseObject = new ViewPagerDto<ViewIndentMasterListByStatusFromSPDto>();
            var a = TokenService.GetUserLoggedInfo();
            var conn = _uowIndentMaster.GetDbContext().Database.GetDbConnection();
            try
            {
                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand("[mims].[SPMimsIndentList]", (SqlConnection)conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                if (!AppCommonMethod.IsNullorZeroInt(TokenService.GetUserHfId()))
                    sqlComm.Parameters.AddWithValue("@HealthFacilityId", TokenService.GetUserHfId());

                if (!AppCommonMethod.IsNullOrEmptyGuid(TokenService.GetUserLoggedInfo()?.MimsBranchId) && AppCommonMethod.IsNullBool(filter.ShowIndentsRequestedToMyBranch))
                    sqlComm.Parameters.AddWithValue("@MimsBranchId", TokenService.GetUserLoggedInfo()?.MimsBranchId);

                if (!AppCommonMethod.IsNullOrEmptyGuid(filter.FilterMimsBranchStatusProfileId))
                {
                    filter.IndentStatus = null; //if profile id is passed then no need of indent status
                    sqlComm.Parameters.AddWithValue("@FilterMimsBranchStatusProfileId", filter.FilterMimsBranchStatusProfileId);
                }

                if (!AppCommonMethod.IsNullOrEmptyGuid(filter.FilterMimsBranchId) && AppCommonMethod.IsNullBool(filter.ShowIndentsRequestedToMyBranch))
                    sqlComm.Parameters.AddWithValue("@FilterMimsBranchId", filter.FilterMimsBranchId);

                if (!AppCommonMethod.IsNullBool(filter.ShowIndentsRequestedToMyBranch) && !AppCommonMethod.IsNullOrEmptyGuid(filter.FilterMimsBranchId) && !AppCommonMethod.IsNullOrEmptyGuid(TokenService.GetUserLoggedInfo()?.MimsBranchId))
                {
                    filter.IndentStatus = null; //if profile id is passed then no need of indent status
                    sqlComm.Parameters.AddWithValue("@MimsBranchId", filter.FilterMimsBranchId);
                    sqlComm.Parameters.AddWithValue("@FilterMimsBranchId", TokenService.GetUserLoggedInfo()?.MimsBranchId);

                    //reverting this to show the requested indents to this branch
                }

                if (!string.IsNullOrEmpty(filter.ListType))
                    sqlComm.Parameters.AddWithValue("@ListType", filter.ListType);

                if (!AppCommonMethod.IsNullorZeroInt(filter.IndentStatus))
                    sqlComm.Parameters.AddWithValue("@IndentStatus", filter.IndentStatus);

                if (!AppCommonMethod.IsNullorZeroInt(filter.PageNumber))
                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);

                if (!AppCommonMethod.IsNullorZeroInt(filter.PageSize))
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                await Task.Run(() => da.Fill(ds));
                var count = ds.Tables[0].ToList<ViewIndentMasterDetialListListTotalCount>();
                lst = ds.Tables[1].ToList<ViewIndentMasterListByStatusFromSPDto>();

                responseObject.TotalCount = count.Select(x => x.TotalRecord).FirstOrDefault();
                responseObject.PageSize = filter.PageSize;
                responseObject.CurrentPage = filter.PageNumber;
                responseObject.TotalPages = (int)Math.Ceiling(responseObject.TotalCount / (double)filter.PageSize);
                responseObject.HasPrevious = filter.PageNumber > 1;
                responseObject.HasNext = filter.PageNumber < responseObject.TotalPages;
                responseObject.List = lst;

                return responseObject;

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

        public async Task<List<SPIndentDetailListDto>> GetMimsIndentDetailById(Guid? IndentMasterId)
        {
            //List<SPIndentDetailListDto> lst = new List<SPIndentDetailListDto>();
            var conn = _uowIndentMaster.GetDbContext().Database.GetDbConnection();
            try
            {
                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand("[mims].[SPMimsIndentDetailById]", (SqlConnection)conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                if (!AppCommonMethod.IsNullOrEmptyGuid(IndentMasterId))
                    sqlComm.Parameters.AddWithValue("@IndentMasterId", IndentMasterId);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                await Task.Run(() => da.Fill(ds));

                List<SPIndentDetailListDto> lst = ds.Tables[0].ToList<SPIndentDetailListDto>();

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

        public async Task<ViewPagerDto<ViewIndentMasterDto>> GetMedicineIndentLog(FilterIndentMasterDto? filter)
        {
            var listIndent = _uowIndentMaster.Repository.GetALL(x => x.FromMimsBranchId == filter.MimsBranchId && (x.IsMimsAcknowledged == true || x.IsOpeningStock == true ) )
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthfacilityId == filter.HealthFacilityId)
                .WhereIf(!string.IsNullOrEmpty(filter.IndentNumber),x=>x.IndentNumber == filter.IndentNumber)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value < filter.EndDate!.Value)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new ViewIndentMasterDto
                {
                    IndentMasterId = x.IndentMasterId,
                    IndentNumber = x.IndentNumber,
                    HealthfacilityId = x.HealthfacilityId,
                    CreatedOn = x.CreatedOn,
                    IsMimsAcknowledged = x.IsMimsAcknowledged
                });

            var pagedList = await PagedListDto<ViewIndentMasterDto>.ToPagedListAsync(
                   listIndent,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewIndentMasterDto>
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

        public async Task<List<ViewIndentDetailDTO>> GetSyncIndentDetail(Guid? IndentMasterId)
        {

            var _uowIndentDetail = new UnitOfWork<IndentDetail>(_uowIndentMaster.GetDbContext());
            var listIndent = await _uowIndentDetail.Repository.GetALL(x => x.IndentMasterId == IndentMasterId)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new ViewIndentDetailDTO
                {
                    IndentDetailId = x.IndentDetailId,
                    IndentMasterId = x.IndentMasterId,
                    MedicineId = x.MedicineId,
                    MedicineName = x.MedicineName,
                    ReceivedQty = x.ReceivedQty,
                    UnitPrice = x.UnitPrice,
                    MedicineMfgDate = x.MedicineMfgDate,
                    MedicineExpDate = x.MedicineExpDate,
                    CreatedOn = x.CreatedOn,
                    CreatedBy = x.CreatedBy,
                    BatchNo = x.BatchNo,
                    IsSMLMedicine = x.IsSmlmedicine
                }).ToListAsync();

            return _mapper.Map<List<ViewIndentDetailDTO>>(listIndent);

        }
        #endregion

        #region Helper Methods


        private async Task<string> GetLatestIndentSequenceAsync(int healthFacilityId)
        {
            var currentMonth = DateTime.Now.ToString("MM");
            var currentYear = DateTime.Now.ToString("yy");

            var latestIndent = await _uowIndentMaster.Repository
                .GetALL(singleRow =>
                    singleRow.HealthfacilityId == healthFacilityId
                    && singleRow.CreatedOn.Value.Month == DateTime.Now.Month
                    && singleRow.CreatedOn.Value.Year == DateTime.Now.Year)
                .OrderByDescending(singleRow => singleRow.CreatedOn)
                .Select(singleRow => singleRow.IndentNumber)
                .FirstOrDefaultAsync();

            return latestIndent;

            
        }


        private async void FillEntity(IndentMaster obj)
        {
            if (obj.IndentMasterId == Guid.Empty)
            {
                obj.IndentMasterId = Guid.NewGuid();
                obj.HealthfacilityId = TokenService.GetUserHfId();
                obj.IsActive = true;
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
        private void FillEntityDelete(IndentMaster obj)
        {
            if (obj != null)
            {
                obj.DeletedBy = _tokenService.GetUserId();
                obj.DeletedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
            }

        }

        private void FillEntityIndentDetail(IndentDetail obj)
        {
            if (obj.IndentDetailId == Guid.Empty)
            {
                obj.IndentDetailId = Guid.NewGuid();
                obj.IsActive = true;
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
        private void FillEntityIndentDetailDelete(IndentDetail obj)
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
    public class CustomDateTimeConverter : JsonConverter<DateTime>
    {
        private const string DateFormat = "yyyy-MM-ddTHH:mm:ss.fffZ"; // ISO 8601 format

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString(), DateFormat, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(DateFormat));
        }
    }
}
