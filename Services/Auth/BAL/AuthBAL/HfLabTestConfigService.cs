using AppCommonMethods;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.HfLabTestConfigDto;
using AuthDAL.Models.Dto.PaginationDto;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using JWTAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthBAL
{
    public class HfLabTestConfigService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<HfLabTestConfig> _uowHfLabTestConfig;
        #endregion

        #region Constructor

        public HfLabTestConfigService (
            TokenService tokenService,
            UnitOfWork<HfLabTestConfig> uowHfLabTestConfig,
            IMapper mapper
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowHfLabTestConfig = uowHfLabTestConfig;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditHfLabTestConfigListDto> CreateOrEdit(CreateOrEditHfLabTestConfigListDto input)
        {
            HfLabTestConfig? hfLabTestConfigExist = await _uowHfLabTestConfig.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.HealthFacilityId == input.HealthFacilityId).FirstOrDefaultAsync();


            if (AppCommonMethod.IsNullObject(hfLabTestConfigExist))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditHfLabTestConfigListDto> Create(CreateOrEditHfLabTestConfigListDto input)
        {
            var list = _mapper.Map<List<HfLabTestConfig>>(input.HfLabTestConfigList);
            foreach (var item in list)
            {
                item.HealthFacilityId = input.HealthFacilityId;
                FillEntity(item);
                await _uowHfLabTestConfig.Repository.Insert(item);
            }
            await _uowHfLabTestConfig.CommitAsync();
            return _mapper.Map<CreateOrEditHfLabTestConfigListDto>(input);
        }

        private async Task<CreateOrEditHfLabTestConfigListDto> Update(CreateOrEditHfLabTestConfigListDto input)
        {

            var dbHfLabTestConfigs = await _uowHfLabTestConfig.Repository.GetALL(x => x.HealthFacilityId == input.HealthFacilityId! && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

            //soft delete Record
            foreach (var dbHfLabTestConfig in dbHfLabTestConfigs.ToList())
            {
                if (!input.HfLabTestConfigList.Any(c => c.HfLabTestConfigId == dbHfLabTestConfig.HfLabTestConfigId))
                {
                    dbHfLabTestConfig.DeletedBy = _tokenService.GetUserId();
                    dbHfLabTestConfig.DeletedOn = DateTime.Now;
                    dbHfLabTestConfig.ActionTypeId = (int)ActionTypeEnum.Deleted;
                    _uowHfLabTestConfig.Repository.Update(dbHfLabTestConfig);
                    //await _uowPatientLabTest.Save();
                }
            }

            // Update and Insert PatientPrescription
            foreach (var hfLabTestConfig in input.HfLabTestConfigList.ToList())
            {
                var dbHfLabTestConfig = dbHfLabTestConfigs
                    .Where(c => c.HfLabTestConfigId == hfLabTestConfig.HfLabTestConfigId && c.HfLabTestConfigId != default(Guid))
                    .SingleOrDefault();

                if (dbHfLabTestConfig != null)
                {
                    // Update child
                    hfLabTestConfig.HealthFacilityId = input.HealthFacilityId;
                    var obj = _mapper.Map(hfLabTestConfig, dbHfLabTestConfig);
                    FillEntity(obj!);
                    _uowHfLabTestConfig.Repository.Update(obj);
                }
                else
                {
                    // Insert child
                    var objHfLabTestConfig = _mapper.Map<HfLabTestConfig>(hfLabTestConfig);
                    objHfLabTestConfig.HealthFacilityId = input.HealthFacilityId;
                    FillEntity(objHfLabTestConfig!);
                    await _uowHfLabTestConfig.Repository.Insert(objHfLabTestConfig);
                }
            }
            await _uowHfLabTestConfig.Save();





            //var dbObj = await _uowHfLabTestConfig.Repository.GetById(input.HfLabTestConfigId!);

            //if (AppCommonMethod.IsNullObject(dbObj))
            //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            //var obj = _mapper.Map(input, dbObj);
            //FillEntity(obj!);

            //_uowHfLabTestConfig.Repository.Update(obj!);
            //await _uowHfLabTestConfig.CommitAsync();
            return _mapper.Map<CreateOrEditHfLabTestConfigListDto>(input);
        }

        public async Task<bool> Delete(int HfId)
        {
            var dbList = await _uowHfLabTestConfig.Repository.GetALL(x => x.HealthFacilityId == HfId && x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToListAsync();

            if (AppCommonMethod.IsNullOrEmptyList<HfLabTestConfig>(dbList))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            foreach (var item in dbList)
            {
                FillEntityDelete(item!);
                _uowHfLabTestConfig.Repository.Update(item!);
            }
            
            await _uowHfLabTestConfig.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewHfLabTestConfigDto>> GetAll(Expression<Func<HfLabTestConfig, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<HfLabTestConfig> responseObj = await _uowHfLabTestConfig.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewHfLabTestConfigDto>>(responseObj);
        }

        public async Task<ViewPagerDto<SPHfLabTestConfigList>> GetAllConfigHealthFacilitiesByFilters(FilterHfLabTestConfigDto filter)
        {
            var loginUser = TokenService.GetUserLoggedInfo();

            List<SPHfLabTestConfigList> lst = new List<SPHfLabTestConfigList>();
            var responseObject = new ViewPagerDto<SPHfLabTestConfigList>();

            var conn = _uowHfLabTestConfig.GetDbContext().Database.GetDbConnection();
            try
            {
                //if (!AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && !string.IsNullOrEmpty(filter.SearchString))
                //    filter.StartDate = filter.StartDate!.Value.AddDays(-30);

                //var _uowUser = new UnitOfWork<User>(_uowPatientLabTest.GetDbContext());
                //var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand("[dbo].[SPHfLabTestConfigList]", (SqlConnection)conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                    sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                    sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                    sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                    sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);


                if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                    sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);


                //if (!AppCommonMethod.IsNullorZeroInt(filter.FilterBy))
                //    sqlComm.Parameters.AddWithValue("@FilterBy", filter.FilterBy);

                if (!string.IsNullOrEmpty(filter.SearchString))
                    sqlComm.Parameters.AddWithValue("@SearchString", filter.SearchString);

                if (!AppCommonMethod.IsNullorZeroInt(filter.PageNumber))
                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);

                if (!AppCommonMethod.IsNullorZeroInt(filter.PageSize))
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                await Task.Run(() => da.Fill(ds));
                var count = ds.Tables[0].ToList<SPHfLabTestConfigListTotalCount>();
                lst = ds.Tables[1].ToList<SPHfLabTestConfigList>();

                responseObject.TotalCount = count.Select(x => x.TotalRecord).FirstOrDefault();

                //return responseObject;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }





            //responseObject.TotalCount = count.Select(x => x.TotalRecord).FirstOrDefault();
            responseObject.PageSize = filter.PageSize;
            responseObject.CurrentPage = filter.PageNumber;
            responseObject.TotalPages = (int)Math.Ceiling(responseObject.TotalCount / (double)filter.PageSize);
            responseObject.HasPrevious = filter.PageNumber > 1;
            responseObject.HasNext = filter.PageNumber < responseObject.TotalPages;
            responseObject.List = lst;
            //responseObject.List = lst;

            return responseObject;
        }

        public async Task<SPGetHfLabTestConfigByHealthFacilityIdDto> GetHfLabTestConfigByHealthFacilityId(int? HealthFacilityId)
        {
            SPGetHfLabTestConfigByHealthFacilityIdDto responseObject = new SPGetHfLabTestConfigByHealthFacilityIdDto();

            //List<SPGetHfDataByHealthFacilityIdDto> hfData = new List<SPGetHfDataByHealthFacilityIdDto>();

            var conn = _uowHfLabTestConfig.GetDbContext().Database.GetDbConnection();
            try
            {
                
                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand("[dbo].[SPGetHfLabTestConfigByHealthFacilityId]", (SqlConnection)conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                if (!AppCommonMethod.IsNullorZeroInt(HealthFacilityId))
                    sqlComm.Parameters.AddWithValue("@HealthFacilityId", HealthFacilityId);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                await Task.Run(() => da.Fill(ds));
                //List<SPGetHfDataByHealthFacilityIdDto> hfData = ds.Tables[0].ToList<SPGetHfDataByHealthFacilityIdDto>();
                responseObject.HfLabTestConfigList = ds.Tables[0].ToList<HfLabTestConfigListDto>();

                responseObject.HealthFacilityId = responseObject.HfLabTestConfigList.Select(x => x.HealthFacilityId).FirstOrDefault();
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
        //public async Task<ViewPagerDto<ViewHfLabTestConfigDto>> GetAllWithPagination(FilterHfLabTestConfigDto filter)
        //{
        //    var list = _uowHfLabTestConfig.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
        //        .WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.Name.ToLower().StartsWith(filter.SearchString) || x.ShortName.ToLower().StartsWith(filter.SearchString) || x.HfLabTestConfigType.Name.ToLower().StartsWith(filter.SearchString))
        //        .OrderByDescending(x => x.CreatedOn);

        //    IQueryable<ViewHfLabTestConfigDto> IQueryableList = list.Select(x =>
        //       new ViewHfLabTestConfigDto
        //       {
        //           HfLabTestConfigId = x.HfLabTestConfigId,
        //           Name = x.Name,
        //           ShortName = x.ShortName,
        //           HfLabTestConfigTypeId = x.HfLabTestConfigTypeId,
        //           HfLabTestConfigTypeName = x.HfLabTestConfigType.Name,
        //           IsActive = x.IsActive,
        //           IsDssDisease = x.IsDssDisease
        //       });

        //    var pagedList = await PagedListDto<ViewHfLabTestConfigDto>.ToPagedListAsync(
        //           IQueryableList,
        //           filter.PageNumber,
        //           filter.PageSize
        //           );

        //    var responseObject = new ViewPagerDto<ViewHfLabTestConfigDto>
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

        public async Task<ViewHfLabTestConfigDto> GetById(Guid input)
        {
            HfLabTestConfig? responseObj = await _uowHfLabTestConfig.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewHfLabTestConfigDto>(responseObj);
        }



        #endregion

        #region Helper Methods

        private void FillEntity(HfLabTestConfig obj)
        {
            if (obj.HfLabTestConfigId == Guid.Empty)
            {
                obj.HfLabTestConfigId = Guid.NewGuid();
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
        private void FillEntityDelete(HfLabTestConfig obj)
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
