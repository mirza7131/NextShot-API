using AppCommonMethods;
using AuthBAL.Common;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.PaginationDto;
using AuthDAL.Models.Dto.ProfileDto;
using AuthDAL.Models.Dto.SectionLookupDto;
using AuthDAL.Repositories;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Aggregator.API.Models.Dto.Hr;
using HMIS.Aggregator.API.Services;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuthBAL
{
    public class SectionLookupService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly HRService _hrService;
        private UnitOfWork<SectionLookup> _uowSectionLookup;

        #endregion

        #region Constructor

        public SectionLookupService(TokenService tokenService, UnitOfWork<SectionLookup> uowSectionLookup, IMapper mapper, HRService hrService)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowSectionLookup = uowSectionLookup;
            _hrService = hrService;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditSectionLookupDto> CreateOrEdit(CreateOrEditSectionLookupDto input)
        {
            if (AppCommonMethod.IsNullorZeroInt(input.SectionLookupId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditSectionLookupDto> Create(CreateOrEditSectionLookupDto input)
        {
            var obj = _mapper.Map<SectionLookup>(input);
            FillEntity(obj);
            SectionLookup responseObj = await _uowSectionLookup.Repository.Insert(obj);
            await _uowSectionLookup.CommitAsync();
            return _mapper.Map<CreateOrEditSectionLookupDto>(responseObj);
        }

        private async Task<CreateOrEditSectionLookupDto> Update(CreateOrEditSectionLookupDto input)
        {
            var dbObj = await _uowSectionLookup.Repository.GetById(input.SectionLookupId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowSectionLookup.Repository.Update(obj!);
            await _uowSectionLookup.CommitAsync();

            return _mapper.Map<CreateOrEditSectionLookupDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowSectionLookup.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowSectionLookup.Repository.Update(dbObj!);
            await _uowSectionLookup.CommitAsync();
            return true;
        }

        public async Task<bool> DumpIpdSectionsFromHr(DumpSectionFromHrDto input)
        {

            var hrResponse = await _hrService.GetHealthFacilityWardBeds(0, input.IsActive);

            var dbSections = await _uowSectionLookup.Repository.GetALL(x => x.DepartmentLookup.DisplayName == CommonStringConstant.IPD).ToListAsync();

            var dbSectinLookupList = new List<SectionLookup>();

            if (!AppCommonMethod.IsNullOrEmptyList(hrResponse))
            {
                var wardList = hrResponse.DistinctBy(x => x.ward_Id).ToList();

                
                // Soft Delete
                foreach (var section in dbSections)
                {
                    if (!wardList.Any(c => c.ward_Id == section.HrWardId))
                    {
                        FillEntityDelete(section);
                        _uowSectionLookup.Repository.Update(section);
                    }
                }

                // Update and Insert
                foreach (var section in wardList)
                {
                    var dbSection = dbSections
                        .Where(c => c.HrWardId == (int)section.ward_Id)
                        .FirstOrDefault();

                    if (dbSection != null)
                    {
                        // Update
                        //var objSection = _mapper.Map(section, dbSection);
                        var objSection = dbSection;
                        FillEntity(objSection);

                        objSection.HrWardId = (int)section.ward_Id;
                        objSection.Name = section.wardName!.Trim();
                        objSection.DisplayName = section.wardName.Trim();
                        objSection.IsActive = input.IsActive;

                        _uowSectionLookup.Repository.Update(objSection);
                    }
                    else
                    {
                        // Insert 
                        var objSection = new SectionLookup();
                        //var objSection = _mapper.Map<SectionLookup>(section);

                        FillEntity(objSection);
                        objSection.DepartmentLookupId = 1;
                        objSection.FormType = CommonStringConstant.GeneralForm;
                        objSection.HrWardId = (int)section.ward_Id;
                        objSection.MimsWardId = CommonConstant.IPDWardID;

                        objSection.Name = section.wardName.Trim();
                        objSection.DisplayName = section.wardName.Trim();
                            
                        objSection.IsActive = input.IsActive;

                        //dbSections.Add(objSection);
                        await _uowSectionLookup.Repository.Insert(objSection);
                    }
                }

                
            }
            await _uowSectionLookup.CommitAsync();

            return true;
        }

        


        #endregion

        #region Read Operations

        public async Task<List<ViewSectionLookupDto>> GetAll(Expression<Func<SectionLookup, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<SectionLookup> responseObj = await _uowSectionLookup.Repository.GetALL(filter).OrderBy(x => x.Name).ToListAsync();
            return _mapper.Map<List<ViewSectionLookupDto>>(responseObj);
        }


        public async Task<ViewSectionLookupDto> GetById(Guid input)
        {
            SectionLookup? responseObj = await _uowSectionLookup.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewSectionLookupDto>(responseObj);
        }

        public async Task<List<ViewSectionLookupDto>> GetAllSectionWithDepartment()
        {
            var department = await _uowSectionLookup.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                .Include(x => x.DepartmentLookup)
                .Select(y => new ViewSectionLookupDto
                {
                    SectionLookupId = y.SectionLookupId,
                    Name = y.Name,
                    DisplayName = y.DisplayName,
                    DepartmentLookupId = y.DepartmentLookupId,
                    DepartmentName = y.DepartmentLookup.Name,
                    IsActive = y.IsActive
                }).ToListAsync();
            return _mapper.Map<List<ViewSectionLookupDto>>(department);
        }

        public async Task<List<ViewSectionLookupDto>> GetByDepartmentId(int input)
        {
            var department = await _uowSectionLookup.Repository.GetALL(x => x.DepartmentLookupId == input && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                .Include(x => x.DepartmentLookup)
                .Select(y => new ViewSectionLookupDto
            {
                SectionLookupId = y.SectionLookupId,
                Name = y.Name,
                DisplayName = y.DisplayName,
                DepartmentLookupId = y.DepartmentLookupId,
                DepartmentName = y.DepartmentLookup.Name,
                IsActive = y.IsActive
            }).ToListAsync();
            return _mapper.Map<List<ViewSectionLookupDto>>(department);
        }

        public async Task<ViewPagerDto<ViewSectionLookupDto>> GetAllWithDepartment(FilterSectionLookupDto filter)
        {
            //var department = await _uowSectionLookup.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Include(x => x.DepartmentLookup).Select(y => new ViewSectionLookupDto
            //{
            //    SectionLookupId = y.SectionLookupId,
            //    Name = y.Name,
            //    DisplayName = y.DisplayName,
            //    DepartmentLookupId = y.DepartmentLookupId,
            //    DepartmentName = y.DepartmentLookup.Name,
            //    IsActive = y.IsActive
            //}).ToListAsync();
            //return _mapper.Map<List<ViewSectionLookupDto>>(department);


            var list = _uowSectionLookup.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.Name!.ToLower().StartsWith(filter.SearchString) || x.DisplayName!.ToLower().StartsWith(filter.SearchString));

            IQueryable<ViewSectionLookupDto> IQueryableList = list.Select(x =>
                new ViewSectionLookupDto
                {
                    SectionLookupId = x.SectionLookupId,
                    Name = x.Name,
                    DisplayName = x.DisplayName,
                    DepartmentLookupId = x.DepartmentLookupId,
                    DepartmentName = x.DepartmentLookup.Name,
                    IsActive = x.IsActive,
                    IsConsultant = (x.IsConsultant == true) ? x.IsConsultant:false,
                    FormType = x.FormType,
                    IsFilterClinic = (x.IsFilterClinic == true) ? x.IsFilterClinic : false,
                    ConsultantSectionLookupId = x.ConsultantSectionLookupId,
                });

            var pagedList = await PagedListDto<ViewSectionLookupDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewSectionLookupDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = _mapper.Map<List<ViewSectionLookupDto>>(pagedList)
            };

            return responseObject;
        }


        #endregion

        #region Helper Methods

        private void FillEntity(SectionLookup obj)
        {
            if (AppCommonMethod.IsNullorZeroInt(obj.SectionLookupId))
            {
                //obj.SectionId = Guid.NewGuid();
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
        private void FillEntityDelete(SectionLookup obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        private void FillEntityHfDepartmentSection(HfDepartmentSection obj)
        {
            if (AppCommonMethod.IsNullorZeroInt(obj.HfDepartmentSectionId))
            {
                //obj.SectionId = Guid.NewGuid();
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
        private void FillEntityHfDepartmentSectionDelete(HfDepartmentSection obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        

        #endregion
    }
}
