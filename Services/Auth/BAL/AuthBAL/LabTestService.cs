using AppCommonMethods;
using AuthBAL.Common;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.HealthFacilityDto;
using AuthDAL.Models.Dto.HfLabTestConfigDto;
using AuthDAL.Models.Dto.LabTest;
using AuthDAL.Models.Dto.LabTestDetailDto;
using AuthDAL.Models.Dto.LabTestDto;
using AuthDAL.Models.Dto.PaginationDto;
using AuthDAL.Models.Dto.UserDto;
using AuthDAL.Repositories;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs;
using CommonDTOs.DropdownDTO;
using CommonDTOs.Enums;
using CommonDTOs.LocationDTO;
using CommonExceptionHandler;
using CommonMessages;
using JWTAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using RedisCache;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace AuthBAL
{
    public class LabTestService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly IMapper _mapper;
        private UnitOfWork<LabTest> _uowLabTest;
        private readonly TokenService _tokenService;
        private readonly IRedisCacheService _cacheService;

        #endregion

        #region Constructor

        public LabTestService(TokenService tokenService,
            UnitOfWork<LabTest> uowLabTest,
            IMapper mapper,
            IRedisCacheService redisCacheService
        )
        {
            _mapper = mapper;
            _uowLabTest = uowLabTest;
            _tokenService = tokenService;
            _cacheService = redisCacheService;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditLabTestDto> CreateOrEdit(CreateOrEditLabTestDto input)
        {
            if (AppCommonMethod.IsNullorZeroInt(input.LabTestId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditLabTestDto> Create(CreateOrEditLabTestDto input)
        {
            var obj = _mapper.Map<LabTest>(input);
            FillEntity(obj);
            LabTest responseObj = await _uowLabTest.Repository.Insert(obj);
            await _uowLabTest.CommitAsync();
            RefreshLabTestCacheList();
            return _mapper.Map<CreateOrEditLabTestDto>(responseObj);
        }

        private async Task<CreateOrEditLabTestDto> Update(CreateOrEditLabTestDto input)
        {
            var dbObj = await _uowLabTest.Repository.GetALL(x => x.LabTestId == input.LabTestId).FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);

            foreach (var item in obj!.LabTestDetails)
            {
                if (AppCommonMethod.IsNullorZeroInt(item.LabTestDetailId))
                {
                    item.CreatedBy = _tokenService.GetUserId();
                    item.CreatedOn = DateTime.Now;
                    item.ActionTypeId = (int)ActionTypeEnum.Create;
                }
                else
                {
                    item.UpdatedBy = _tokenService.GetUserId();
                    item.UpdatedOn = DateTime.Now;
                    item.ActionTypeId = (int)ActionTypeEnum.Edit;
                }

            }

            FillEntity(obj!);

            _uowLabTest.Repository.Update(obj!);
            await _uowLabTest.CommitAsync();
            await RefreshLabTestCacheList();
            return _mapper.Map<CreateOrEditLabTestDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowLabTest.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowLabTest.Repository.Update(dbObj!);
            await _uowLabTest.CommitAsync();
            await RefreshLabTestCacheList();
            return true;
        }

        #endregion

        #region Read Operations

        public async Task<List<ViewLabTest>> GetAll(Guid DepartmentProfileId = new Guid())
        {
            List<ViewLabTest> responseObj = null;
                
                //await _uowLabTest.GetDbContext().ViewLabTests
                //.WhereIf(!AppCommonMethod.IsNullOrEmptyGuid(DepartmentProfileId), x => x.DepartmentProfileId == DepartmentProfileId)
                //.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                //.OrderByDescending(x => x.DepartmentShortName)
                //.ThenBy(x => x.Name)
                //.ToListAsync();

            return responseObj;
        }

        public async Task<List<SPViewLabTest>> GetAllWithHealthFacilityId(int? HealthFacilityId)
        {
            var userHfId = TokenService.GetUserHfId();

            var conn = _uowLabTest.GetDbContext().Database.GetDbConnection();
            try
            {
                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand("[dbo].[SPGetAllLabTestByHealthFacilityId]", (SqlConnection)conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                if (!AppCommonMethod.IsNullorZeroInt(userHfId))
                    sqlComm.Parameters.AddWithValue("@HealthFacilityId", userHfId);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                await Task.Run(() => da.Fill(ds));
                var lst = ds.Tables[0].ToList<SPViewLabTest>();
                return lst;
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


            //List<ViewLabTest> responseObj = await _uowLabTest.GetDbContext().ViewLabTests
            //    .WhereIf(!AppCommonMethod.IsNullOrEmptyGuid(DepartmentProfileId), x => x.DepartmentProfileId == DepartmentProfileId)
            //    .Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
            //    .OrderByDescending(x => x.DepartmentShortName)
            //    .ThenBy(x => x.Name)
            //    .ToListAsync();

            //return responseObj;
        }

        public async Task RefreshLabTestCacheList()
        {
            var dbLabTests = await _uowLabTest.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).OrderBy(x => x.Name).ToListAsync();

            var cacheData = _mapper.Map<List<CacheLabTestDto>>(dbLabTests);

            _cacheService.Set<List<CacheLabTestDto>?>(CacheKeyConstant.LabTest, cacheData, null, null);
        }

        public async Task<List<CacheLabTestDto>> GetAllCacheLabTest()
        {
            var labTests = _cacheService.Get<List<CacheLabTestDto>?>(CacheKeyConstant.LabTest);

            if (labTests == null)
            {
                var dbLabTests = await _uowLabTest.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).OrderBy(x => x.Name).ToListAsync();

                var cacheData = _mapper.Map<List<CacheLabTestDto>>(dbLabTests);

                labTests = _cacheService.Set<List<CacheLabTestDto>?>(CacheKeyConstant.LabTest, cacheData, null, null);
            }

            return labTests!.ToList();
        }
        public async Task<ViewPagerDto<ViewLabTestDto>> GetAllWithPagination(FilterUserDto filter)
        {

            var list = _uowLabTest.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Include(x => x.LabTestDetails)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.Name!.ToLower().Contains(filter.SearchString))
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<ViewLabTestDto> IQueryableList = list.Select(x =>
                new ViewLabTestDto
                {
                    LabTestId = x.LabTestId,
                    Name = x.Name,
                    Description = x.Description,
                    DepartmentProfileId = x.DepartmentProfileId,
                    DoctorShare = x.DoctorShare,
                    GovtShare = x.GovtShare,
                    IsActive = x.IsActive,
                    LabTestCategoryProfileId = x.LabTestCategoryProfileId,
                    LabTestTypeProfileId = x.LabTestTypeProfileId,
                    IsSampleRequired = x.IsSampleRequired,
                    SampleType = x.SampleType,
                    StaffShare = x.StaffShare,
                    TestPrice = x.TestPrice,
                    LabTestDetails = _mapper.Map<List<CreateOrEditLabTestDetailDto>>(x.LabTestDetails.ToList()),
                });

            var pagedList = await PagedListDto<ViewLabTestDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            //foreach (var item in pagedList)
            //{
            //    item.CreatedByName = userList.Where(x => x.UserId == item.CreatedBy).Select(x => x.FullName).FirstOrDefault();
            //    item.UpdatedByName = userList.Where(x => x.UserId == item.UpdatedBy).Select(x => x.FullName).FirstOrDefault();
            //}

            var responseObject = new ViewPagerDto<ViewLabTestDto>
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

        public async Task<ViewLabTestDto> GetById(int input)
        {
            //LabTest? responseObj = await _uowLabTest.Repository.GetById(input);
            LabTest? responseObj = await _uowLabTest.Repository.GetALL(x => x.LabTestId == input).Include(x => x.LabTestDetails.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)).FirstOrDefaultAsync();
            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewLabTestDto>(responseObj);
        }
        #endregion

        #region Helper Methods

        private void FillEntity(LabTest obj)
        {
            if (AppCommonMethod.IsNullorZeroInt(obj.LabTestId))
            {
                //obj.HealthFacilityId = Guid.NewGuid();
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
        private void FillEntityDelete(LabTest obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion

    }
}
