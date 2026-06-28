using AppCommonMethods;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.DepartmentLookupDto;
using AuthDAL.Models.Dto.PaginationDto;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthBAL
{
    public class DepartmentLookupService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<DepartmentLookup> _uowDepartmentLookup;
        private UnitOfWork<HfDepartment> _uowHfDepartment;
        private string[] myInClause;// = new string[] { "011", "012", "068","037" }; // Health Facilities Types Allow Only

        #endregion

        #region Constructor

        public DepartmentLookupService(TokenService tokenService, 
            UnitOfWork<DepartmentLookup> uowDepartmentLookup, UnitOfWork<HfDepartment> uowHfDepartment, 
            IMapper mapper, IConfiguration config
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowDepartmentLookup = uowDepartmentLookup;
            _uowHfDepartment = uowHfDepartment;
            myInClause = config.GetSection("HealthFacility").GetSection("TypesAllowed").Get<string[]>() ?? new string[0];
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditDepartmentLookupDto> CreateOrEdit(CreateOrEditDepartmentLookupDto input)
        {
            if (AppCommonMethod.IsNullorZeroInt(input.DepartmentLookupId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditDepartmentLookupDto> Create(CreateOrEditDepartmentLookupDto input)
        {
            var obj = _mapper.Map<DepartmentLookup>(input);
            FillEntity(obj);
            DepartmentLookup responseObj = await _uowDepartmentLookup.Repository.Insert(obj);
            await _uowDepartmentLookup.CommitAsync();
            return _mapper.Map<CreateOrEditDepartmentLookupDto>(responseObj);
        }

        private async Task<CreateOrEditDepartmentLookupDto> Update(CreateOrEditDepartmentLookupDto input)
        {
            var dbObj = await _uowDepartmentLookup.Repository.GetById(input.DepartmentLookupId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowDepartmentLookup.Repository.Update(obj!);
            await _uowDepartmentLookup.CommitAsync();

            return _mapper.Map<CreateOrEditDepartmentLookupDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowDepartmentLookup.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowDepartmentLookup.Repository.Update(dbObj!);
            await _uowDepartmentLookup.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewDepartmentLookupDto>> GetAll(Expression<Func<DepartmentLookup, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<DepartmentLookup> responseObj = await _uowDepartmentLookup.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewDepartmentLookupDto>>(responseObj);
        }


        public async Task<ViewPagerDto<ViewDepartmentLookupDto>> GetAllWithPagination(FilterDepartmentLookupDto filter)
        {
            var list = _uowDepartmentLookup.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.Name!.ToLower().StartsWith(filter.SearchString))
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<ViewDepartmentLookupDto> IQueryableList = list.Select(x =>
                new ViewDepartmentLookupDto
                {
                    DepartmentLookupId = x.DepartmentLookupId,
                    DisplayName = x.DisplayName,
                    Name = x.Name,
                    Description = x.Description,
                    IsActive = x.IsActive
                });

            var pagedList = await PagedListDto<ViewDepartmentLookupDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewDepartmentLookupDto>
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


        public async Task<ViewDepartmentLookupDto> GetById(Guid input)
        {
            DepartmentLookup? responseObj = await _uowDepartmentLookup.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewDepartmentLookupDto>(responseObj);
        }


        #endregion

        #region Helper Methods

        private void FillEntity(DepartmentLookup obj)
        {
            if (AppCommonMethod.IsNullorZeroInt(obj.DepartmentLookupId))
            {
                //obj.DepartmentId = Guid.NewGuid();
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
        private void FillEntityDelete(DepartmentLookup obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        // Seeding of HF Departments and Hf Department Sections 
        //public async Task SeedHfDepartmentsHfDepartmentSection()
        //{

        //    var departmentLookup = await _uowDepartmentLookup.GetDbContext().DepartmentLookups.Where(x => 
        //    x.DepartmentLookupId == Convert.ToInt16(DepartmentLookupEnum.Emergency) 
        
        //    ).Select(x => x.DepartmentLookupId).ToArrayAsync();

        //    var sectionLookup = await _uowDepartmentLookup.GetDbContext().SectionLookups.Where(x => departmentLookup.Contains(x.DepartmentLookupId)).ToListAsync();
        //    var healthFacilities = await _uowDepartmentLookup.GetDbContext().HealthFacilities.Where(x => myInClause.Contains(x.HealthFacilityTypeCode)).ToListAsync();

        //    List<HfDepartment> hfDepartments = new List<HfDepartment>();
        //    var allHfDepartmentLists = await _uowHfDepartment.GetDbContext().HfDepartments.Where(x => x.IsActive == true && x.ActionTypeId == (int)ActionTypeEnum.Create).ToListAsync();

        //    foreach (var item in healthFacilities)
        //    {
        //        if (allHfDepartmentLists.Where(x => x.HealthFacilityId == item.HealthFacilityId).Count() > 0)
        //            continue;
        //        foreach (var item2 in departmentLookup)
        //        {
        //            var tempHfDepartment = new HfDepartment();
        //            tempHfDepartment.HealthFacilityId = item.HealthFacilityId;
        //            tempHfDepartment.DepartmentLookupId = item2;
        //            tempHfDepartment.CreatedBy = _tokenService.GetUserId();
        //            tempHfDepartment.CreatedOn = DateTime.Now;
        //            tempHfDepartment.IsActive = true;
        //            tempHfDepartment.ActionTypeId = (int)ActionTypeEnum.Create;

        //            tempHfDepartment.HfDepartmentSections = new List<HfDepartmentSection>();
        //            foreach (var item3 in sectionLookup)
        //            {
        //                var tempHfDepartmentSection = new HfDepartmentSection();

        //                tempHfDepartmentSection.CreatedBy = _tokenService.GetUserId();
        //                tempHfDepartmentSection.CreatedOn = DateTime.Now;
        //                tempHfDepartmentSection.IsActive = true;
        //                tempHfDepartmentSection.ActionTypeId = (int)ActionTypeEnum.Create;
        //                tempHfDepartmentSection.SectionLookupId = item3.SectionLookupId;
        //                tempHfDepartment.HfDepartmentSections.Add(tempHfDepartmentSection);

        //            }
        //            //tempHfDepartment.HfDepartmentSections = hfDepartmentSection;
        //            hfDepartments.Add(tempHfDepartment);
        //        }
        //    }

        //    await _uowHfDepartment.Repository.AddRangeAsync(hfDepartments);
        //    await _uowHfDepartment.CommitAsync();

        //}
        #endregion
    }
}
