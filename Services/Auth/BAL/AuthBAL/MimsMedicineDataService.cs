
using AppCommonMethods;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.MimsMedicineData;
using AuthDAL.Models.Dto.MimsMedicineIndentLog;
using AuthDAL.Models.Dto.PaginationDto;
using AuthDAL.Models.Dto.MimsMedicineIndentDetail;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Aggregator.API.Services;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AuthBAL
{
    public class MimsMedicineDataService
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly UnitOfWork<MimsMedicineDatum> _uowMimsMedicineData;
        private readonly UnitOfWork<MimsGetMedicineResponse> _uowMimsGetMedicineResponse;
        private readonly UnitOfWork<MimsMedicineIndentLog> _uowMimsMedicineIndentLog;
        private readonly UnitOfWork<MimsMedicineIndentDetail> _uowMimsMedicineIndentDetail;
        private readonly IMapper _mapper;
        private readonly MIMSService _mimsService;
        private readonly string _mimsBaseUrl;
        private readonly bool _isDevelopment;
        private readonly int _healthFacilityId;
        #endregion

        #region Constructor

        public MimsMedicineDataService(
            TokenService tokenService,
            UnitOfWork<MimsMedicineDatum> uowMimsMedicineData,
            UnitOfWork<MimsGetMedicineResponse> uowMimsGetMedicineResponse,
            UnitOfWork<MimsMedicineIndentLog> uowMimsMedicineIndentLog,
            UnitOfWork<MimsMedicineIndentDetail> uowMimsMedicineIndentDetail,
            IMapper mapper,
            MIMSService mimsService,
            IConfiguration config
        )
        {
            _tokenService = tokenService;
            _uowMimsMedicineData = uowMimsMedicineData;
            _uowMimsGetMedicineResponse = uowMimsGetMedicineResponse;
            _uowMimsMedicineIndentLog = uowMimsMedicineIndentLog;
            _uowMimsMedicineIndentDetail = uowMimsMedicineIndentDetail;
            _mapper = mapper;
            _mimsService = mimsService;
            _isDevelopment = config.GetValue<bool>("IsDevelopment") ? config.GetValue<bool>("IsDevelopment") : false;
            _healthFacilityId = config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") ?
                                  config.GetSection("SystemMode").GetSection("Offline").GetValue<int>("HealthFacilityId") : 0;
            if (_isDevelopment)
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Dev").GetSection("MIMS").Value ?? string.Empty;
            else
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Prod").GetSection("MIMS").Value ?? string.Empty;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditMimsMedicineDataDto> CreateOrEdit(CreateOrEditMimsMedicineDataDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.MimsMedicineDataId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditMimsMedicineDataDto> Create(CreateOrEditMimsMedicineDataDto input)
        {
            var obj = _mapper.Map<MimsMedicineDatum>(input);
            FillEntity(obj);
            MimsMedicineDatum responseObj = await _uowMimsMedicineData.Repository.Insert(obj);
            await _uowMimsMedicineData.CommitAsync();
            return _mapper.Map<CreateOrEditMimsMedicineDataDto>(responseObj);
        }

        private async Task<CreateOrEditMimsMedicineDataDto> Update(CreateOrEditMimsMedicineDataDto input)
        {
            var dbObj = await _uowMimsMedicineData.Repository.GetById(input.MimsMedicineDataId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowMimsMedicineData.Repository.Update(obj!);
            await _uowMimsMedicineData.CommitAsync();

            return _mapper.Map<CreateOrEditMimsMedicineDataDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowMimsMedicineData.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowMimsMedicineData.Repository.Update(dbObj!);
            await _uowMimsMedicineData.CommitAsync();
            return true;
        }

        public async Task ImportDataFromMims(bool offline = false)
        {
            //if (!CheckIfMedicineDataAlreadyImportedForToday())
            //{

            if (TokenService.GetHfHrId() != null && TokenService.GetMimsDepartmentId() != null && TokenService.GetUserHfId() != 0 && TokenService.GetUserHfId() == _healthFacilityId)
            {
                var list = await _mimsService.GetMedicineAvailableQuantityByHealthFacility(_mimsBaseUrl, TokenService.GetHfHrId(), TokenService.GetMimsDepartmentId(), offline);
                if (list != null && list.Data.Count() > 0)
                {
                    // Start save mims response in db

                    foreach (var item in list.Data)
                    {
                        var dbObj = await _uowMimsMedicineData.Repository.GetALL(x => x.WardId == item.WardId && x.MedicineId == item.MedicineId).FirstOrDefaultAsync();

                        CreateOrEditMimsMedicineDataDto obj = new CreateOrEditMimsMedicineDataDto();
                        if (dbObj != null)
                            obj = _mapper.Map<CreateOrEditMimsMedicineDataDto>(dbObj);

                        obj.HealthFacilityId = TokenService.GetUserHfId();
                        obj.MedicineId = item.MedicineId;
                        obj.MedicineName = item.MedicineName;
                        obj.MedicineTypeId = item.MedicineTypeId;
                        obj.MedicineTypeName = item.MedicineTypeName;
                        obj.WardId = item.WardId;
                        obj.WardName = item.WardName;
                        obj.IsActive = true;
                        obj.UnitPrice = item.PricePerItem;
                        obj.IsSMLMedicine = item.IsSMLMedicine;
                        obj.AvailableQuantity = Convert.ToDecimal(obj.AvailableQuantity ?? 0) + Convert.ToDecimal(item.AvailableQuantity);
                        obj.TotalQuantity = (obj.TotalQuantity ?? 0) + Convert.ToDecimal(item.AvailableQuantity);

                        await CreateOrEdit(obj);
                    }

                    // End save mims response in db

                    List<string> indentIds = new List<string>();
                    if (list.IndentIdList != null)
                        indentIds = list.IndentIdList.Split(',').ToList<string>();

                    foreach (var item in indentIds)
                    {
                        CreateOrEditMimsMedicineIndentLogDto createOrEditMimsMedicineIndentLogDto = new CreateOrEditMimsMedicineIndentLogDto();

                        createOrEditMimsMedicineIndentLogDto.HealthFacilityId = TokenService.GetUserHfId();
                        createOrEditMimsMedicineIndentLogDto.ResponseData = JsonConvert.SerializeObject(list.Data);
                        createOrEditMimsMedicineIndentLogDto.MimsIndentId = Convert.ToInt64(item);
                        createOrEditMimsMedicineIndentLogDto.WardId = list.Data[0].WardId;
                        createOrEditMimsMedicineIndentLogDto.WardName = list.Data[0].WardName;
                        createOrEditMimsMedicineIndentLogDto.SyncStatus = false;
                        createOrEditMimsMedicineIndentLogDto.IsActive = true;

                        var result  = await CreateMimsMedicineIndentLogMims(createOrEditMimsMedicineIndentLogDto);

                        var indentArray = new List<string>();
                        indentArray.Add(item);

                        var mimsResult = await _mimsService.UpdateIndentSyncStatus(_mimsBaseUrl, indentArray);

                        if (mimsResult.Status)
                        {
                            result.SyncStatus = true;
                            await UpdateMimsMedicineIndentLog(result);
                        }

                        // Mims Medicine Indent Detail

                        // Get Indent Detail from MIMS
                        var indnetDetail = await _mimsService.GetUnSyncIndentDetailByIndentId(_mimsBaseUrl, TokenService.GetHfHrId(), (long)Convert.ToDouble(item));

                        foreach (var item2 in indnetDetail.Data)
                        {
                            CreateOrEditMimsMedicineIndentDetailDto createOrEditMimsMedicineIndentDetail = new CreateOrEditMimsMedicineIndentDetailDto();

                            createOrEditMimsMedicineIndentDetail.IndentId = (long)Convert.ToDouble(item);
                            createOrEditMimsMedicineIndentDetail.MedicineId = item2.MedicineId;
                            createOrEditMimsMedicineIndentDetail.MedicineName = item2.MedicineName;
                            createOrEditMimsMedicineIndentDetail.MedicineTypeId = item2.MedicineTypeId;
                            createOrEditMimsMedicineIndentDetail.MedicineTypeName = item2.MedicineTypeName;
                            createOrEditMimsMedicineIndentDetail.WardId = item2.WardId;
                            createOrEditMimsMedicineIndentDetail.WardName = item2.WardName;
                            createOrEditMimsMedicineIndentDetail.IsActive = true;
                            createOrEditMimsMedicineIndentDetail.PricePerItem = item2.PricePerItem;
                            createOrEditMimsMedicineIndentDetail.AvailableQuantity = Convert.ToDecimal(item2.AvailableQuantity);
                            createOrEditMimsMedicineIndentDetail.HealthFacilityId = TokenService.GetUserHfId();

                            await CreateMimsMedicineIndentDetail(createOrEditMimsMedicineIndentDetail);
                        }

                    }
                }
            }
            //}
        }

        private async Task<CreateOrEditMimsMedicineIndentLogDto> CreateMimsMedicineIndentLogMims(CreateOrEditMimsMedicineIndentLogDto input)
        {
            var obj = _mapper.Map<MimsMedicineIndentLog>(input);
            FillEntityMimsMedicineIndentLog(obj);
            MimsMedicineIndentLog responseObj = await _uowMimsMedicineIndentLog.Repository.Insert(obj);
            await _uowMimsMedicineIndentLog.CommitAsync();
            return _mapper.Map<CreateOrEditMimsMedicineIndentLogDto>(responseObj);
        }

        private async Task<CreateOrEditMimsMedicineIndentLogDto> UpdateMimsMedicineIndentLog(CreateOrEditMimsMedicineIndentLogDto input)
        {
            var dbObj = await _uowMimsMedicineIndentLog.Repository.GetById(input.MimsMedicineIndentLogId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntityMimsMedicineIndentLog(obj!);

            _uowMimsMedicineIndentLog.Repository.Update(obj!);
            await _uowMimsMedicineIndentLog.CommitAsync();

            return _mapper.Map<CreateOrEditMimsMedicineIndentLogDto>(obj);
        }

        public async Task<CreateOrEditMimsMedicineIndentDetailDto> CreateOrEditMimsMedicineIndentDetail(CreateOrEditMimsMedicineIndentDetailDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.MimsMedicineIndentDetailId))
                return await CreateMimsMedicineIndentDetail(input);
            else
                return await UpdateMimsMedicineIndentDetail(input);
        }

        private async Task<CreateOrEditMimsMedicineIndentDetailDto> CreateMimsMedicineIndentDetail(CreateOrEditMimsMedicineIndentDetailDto input)
        {
            var obj = _mapper.Map<MimsMedicineIndentDetail>(input);
            FillEntityMimsMedicineIndentDetail(obj);
            MimsMedicineIndentDetail responseObj = await _uowMimsMedicineIndentDetail.Repository.Insert(obj);
            await _uowMimsMedicineIndentDetail.CommitAsync();
            return _mapper.Map<CreateOrEditMimsMedicineIndentDetailDto>(responseObj);
        }

        private async Task<CreateOrEditMimsMedicineIndentDetailDto> UpdateMimsMedicineIndentDetail(CreateOrEditMimsMedicineIndentDetailDto input)
        {
            var dbObj = await _uowMimsMedicineIndentDetail.Repository.GetById(input.MimsMedicineIndentDetailId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntityMimsMedicineIndentDetail(obj!);

            _uowMimsMedicineIndentDetail.Repository.Update(obj!);
            await _uowMimsMedicineIndentDetail.CommitAsync();

            return _mapper.Map<CreateOrEditMimsMedicineIndentDetailDto>(obj);
        }
        #endregion

        #region Read Operations
        public async Task<ViewPagerDto<ViewMimsMedicineDataDto>> GetAllWithPagination(FilterMimsMedicineDataDto filter)
        {
            var list = _uowMimsMedicineData.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<ViewMimsMedicineDataDto> IQueryableList = list.Select(x =>
               new ViewMimsMedicineDataDto
               {
                   MimsMedicineDataId = x.MimsMedicineDataId,
                   MedicineId = x.MedicineId,
                   MedicineName = x.MedicineName,
                   MedicineTypeId = x.MedicineTypeId,
                   MedicineTypeName = x.MedicineTypeName,
                   AvailableQuantity = x.AvailableQuantity,
                   WardId = x.WardId,
                   WardName = x.WardName,

               });

            var pagedList = await PagedListDto<ViewMimsMedicineDataDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewMimsMedicineDataDto>
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

        public bool CheckIfMedicineDataAlreadyImportedForToday()
        {
            bool medicineAlreadyImported = _uowMimsMedicineData.Repository.GetALL(x => x.CreatedOn!.Value.Date == DateTime.Now.Date).Count() > 0;
            return medicineAlreadyImported;
        }

        #endregion

        #region Helper Methods


        private void FillEntity(MimsMedicineDatum obj)
        {
            if (obj.MimsMedicineDataId == Guid.Empty)
            {
                obj.MimsMedicineDataId = Guid.NewGuid();
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
        private void FillEntityDelete(MimsMedicineDatum obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        private void FillEntityMimsGetMedicineResponse(MimsGetMedicineResponse obj)
        {
            if (obj.MimsGetMedicineResponseId == Guid.Empty)
            {
                obj.MimsGetMedicineResponseId = Guid.NewGuid();
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

        private void FillEntityMimsMedicineIndentLog(MimsMedicineIndentLog obj)
        {
            if (obj.MimsMedicineIndentLogId == Guid.Empty)
            {
                obj.MimsMedicineIndentLogId = Guid.NewGuid();
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

        private void FillEntityMimsMedicineIndentDetail(MimsMedicineIndentDetail obj)
        {
            if (obj.MimsMedicineIndentDetailId == Guid.Empty)
            {
                obj.MimsMedicineIndentDetailId = Guid.NewGuid();
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
        #endregion
    }
}