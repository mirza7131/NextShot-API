using AppCommonMethods;
using AuthDAL.Models.DbModels;
using DbModel = AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.HealthFacilityStationDto;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AuthDAL.Models.Dto.PaginationDto;

namespace AuthBAL
{
    public class HealthFacilityStationService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<HealthFacilityStation> _uowHealthFacilityStation;

        #endregion

        #region Constructor

        public HealthFacilityStationService(TokenService tokenService, UnitOfWork<HealthFacilityStation> uowHealthFacilityStation, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowHealthFacilityStation = uowHealthFacilityStation;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditHealthFacilityStationDto> CreateOrEdit(CreateOrEditHealthFacilityStationDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.HealthFacilityStationId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditHealthFacilityStationDto> Create(CreateOrEditHealthFacilityStationDto input)
        {
            //check if already exist
            HealthFacilityStation? dbObj = await _uowHealthFacilityStation.Repository.GetALL(x => x.HealthFacilityId == input.HealthFacilityId && x.StationProfileId == input.StationProfileId && x.ActionTypeId != (int)ActionTypeEnum.Deleted).FirstOrDefaultAsync();

            if (!AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.StationAlreadyAvailable);

            //Check if Available in Lookup & get SequenceNo
            var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowHealthFacilityStation.GetDbContext());
            DbModel.Profile? dbStations = await _uowProfile.Repository.GetById(input.StationProfileId!);

            if (AppCommonMethod.IsNullObject(dbStations))
                throw new UserFriendlyException(CommonMessageConstant.StationNotAvailable);

            input.SequenceNo = (short)dbStations!.SequenceNo!;

            var obj = _mapper.Map<HealthFacilityStation>(input);
            FillEntity(obj);
            HealthFacilityStation responseObj = await _uowHealthFacilityStation.Repository.Insert(obj);
            await _uowHealthFacilityStation.CommitAsync();
            return _mapper.Map<CreateOrEditHealthFacilityStationDto>(responseObj);
        }

        private async Task<CreateOrEditHealthFacilityStationDto> Update(CreateOrEditHealthFacilityStationDto input)
        {
            var dbObj = await _uowHealthFacilityStation.Repository.GetById(input.HealthFacilityStationId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            //check if already exist
            HealthFacilityStation? dbObjCheckIfAlreadyExist = await _uowHealthFacilityStation.Repository.GetALL(x => x.HealthFacilityId == input.HealthFacilityId && x.StationProfileId == input.StationProfileId && x.ActionTypeId != (int)ActionTypeEnum.Deleted).FirstOrDefaultAsync();

            if (!AppCommonMethod.IsNullObject(dbObjCheckIfAlreadyExist))
            {
                if(dbObjCheckIfAlreadyExist!.HealthFacilityStationId != input.HealthFacilityStationId)
                    throw new UserFriendlyException(CommonMessageConstant.StationAlreadyAvailable);
            }
                

            //Check if Available in Lookup & get SequenceNo
            var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowHealthFacilityStation.GetDbContext());
            DbModel.Profile? dbStations = await _uowProfile.Repository.GetById(input.StationProfileId!);

            if (AppCommonMethod.IsNullObject(dbStations))
                throw new UserFriendlyException(CommonMessageConstant.StationNotAvailable);

            input.SequenceNo = (short)dbStations!.SequenceNo!;

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowHealthFacilityStation.Repository.Update(obj!);
            await _uowHealthFacilityStation.CommitAsync();

            return _mapper.Map<CreateOrEditHealthFacilityStationDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowHealthFacilityStation.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowHealthFacilityStation.Repository.Update(dbObj!);
            await _uowHealthFacilityStation.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewHealthFacilityStationDto>> GetAll(Expression<Func<HealthFacilityStation, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<HealthFacilityStation> responseObj = await _uowHealthFacilityStation.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewHealthFacilityStationDto>>(responseObj);
        }

        public async Task<ViewHealthFacilityStationDto> GetById(int input)
        {
            HealthFacilityStation? responseObj = await _uowHealthFacilityStation.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewHealthFacilityStationDto>(responseObj);
        }

        public async Task<ViewPagerDto<ViewHealthFacilityStationWithDetailsDto>> GetAllWithDetails(PagerDto filter)
        {
            var _uowUser = new UnitOfWork<User>(_uowHealthFacilityStation.GetDbContext());
            var userList = _uowUser.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToList();

            var list = _uowHealthFacilityStation.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                .Include(x => x.HealthFacility)
                    .ThenInclude(x => x!.Tehsil)
                        .ThenInclude(x => x!.District)
                            .ThenInclude(x => x!.Division)
                                .ThenInclude(x => x!.Province)
                .Include(x => x.StationProfile)
                
                    //.WhereIf(!TokenService.IsSuperAdmin(), x => x.HealthFacilityId == TokenService.GetUserHfId())

                .WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.HealthFacility!.Name!.ToLower().Contains(filter.SearchString) || x.StationProfile!.Name.ToLower().StartsWith(filter.SearchString))

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.HealthFacility!.Tehsil!.District!.Division!.Province!.ProvinceId == filter.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.HealthFacility!.Tehsil!.District!.Division!.DivisionId == filter.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.HealthFacility!.Tehsil!.District!.DistrictId == filter.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.HealthFacility!.Tehsil!.TehsilId == filter.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacility!.HealthFacilityId == filter.HealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value.Date >= filter.StartDate!.Value.Date)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value.Date < filter.EndDate!.Value.AddDays(1).Date)
                .OrderByDescending(x => x.CreatedOn);

            //.ToListAsync();
            //return responseObj;

            IQueryable<ViewHealthFacilityStationWithDetailsDto> IQueryableList = list.Select(x => new ViewHealthFacilityStationWithDetailsDto
            {
                HealthFacilityStationId = x.HealthFacilityStationId,
                StationProfileId = x.StationProfileId,
                StationProfileName = x.StationProfile!.Name,
                HealthFacilityId = x.HealthFacilityId,
                HealthFacilityName = x.HealthFacility!.Name,
                SequenceNo = x.SequenceNo,
                CreatedOn = x.CreatedOn,
                CreatedBy = x.CreatedBy,
                UpdatedOn = x.UpdatedOn,
                UpdatedBy = x.UpdatedBy,
                IsActive = x.IsActive
            });

            var pagedList = await PagedListDto<ViewHealthFacilityStationWithDetailsDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            foreach (var item in pagedList)
            {
                item.CreatedByName = userList.Where(x => x.UserId == item.CreatedBy).Select(x => x.FullName).FirstOrDefault();
                item.UpdatedByName = userList.Where(x => x.UserId == item.UpdatedBy).Select(x => x.FullName).FirstOrDefault();
            }

            var responseObject = new ViewPagerDto<ViewHealthFacilityStationWithDetailsDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList,
            };

            return responseObject;
        }


        #endregion

        #region Helper Methods

        private void FillEntity(HealthFacilityStation obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.HealthFacilityStationId))
            {
                obj.HealthFacilityStationId = Guid.NewGuid();
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
        private void FillEntityDelete(HealthFacilityStation obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion
    }
}
