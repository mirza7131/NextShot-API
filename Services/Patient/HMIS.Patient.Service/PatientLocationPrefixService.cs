using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PatientLocationPrefixDto;
using HMIS.Patient.Domain.Repositories.UOW;
using JWTAuthentication;
using System.Linq.Expressions;

namespace HMIS.Patient.Service
{
    public class PatientLocationPrefixService
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<PatientLocationPrefix> _uowPatientLocationPrefix;

        #endregion

        #region Constructor

        public PatientLocationPrefixService(TokenService tokenService, UnitOfWork<PatientLocationPrefix> uowPatientLocationPrefix, IMapper mapper)
        {
            _tokenService = tokenService;
            _uowPatientLocationPrefix = uowPatientLocationPrefix;
            _mapper = mapper;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditPatientLocationPrefixDto> CreateOrEdit(CreateOrEditPatientLocationPrefixDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.Id))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditPatientLocationPrefixDto> Create(CreateOrEditPatientLocationPrefixDto input)
        {
            var obj = _mapper.Map<PatientLocationPrefix>(input);
            FillEntity(obj);
            PatientLocationPrefix responseObj = await _uowPatientLocationPrefix.Repository.Insert(obj);
            await _uowPatientLocationPrefix.CommitAsync();
            return _mapper.Map<CreateOrEditPatientLocationPrefixDto>(responseObj);
        }

        private async Task<CreateOrEditPatientLocationPrefixDto> Update(CreateOrEditPatientLocationPrefixDto input)
        {
            var dbObj = await _uowPatientLocationPrefix.Repository.GetById(input.Id!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowPatientLocationPrefix.Repository.Update(obj!);
            await _uowPatientLocationPrefix.CommitAsync();

            return _mapper.Map<CreateOrEditPatientLocationPrefixDto>(obj);

        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowPatientLocationPrefix.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowPatientLocationPrefix.Repository.Update(dbObj!);
            await _uowPatientLocationPrefix.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        //public async Task<List<ViewPatientLocationPrefixDto>> GetAll(Expression<Func<PatientLocationPrefix, bool>>? filter = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        //    string includeProperties = "")
        //{
        //    List<PatientLocationPrefix> responseObj = await _uowPatientLocationPrefix.Repository.GetALL(filter).ToListAsync();
        //    return _mapper.Map<List<ViewPatientLocationPrefixDto>>(responseObj);
        //}

        //public async Task<ViewPagerDto<ViewPatientVisitsListWithDetailDto>> GetPatientVisitsListWithDetail(FilterPatientVisitDto filter)
        //{
        //    var list = _uowPatientLocationPrefix.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)               
        //        .WhereIf(!TokenService.IsSuperAdmin(), x => x.HealthFacilityId == TokenService.GetUserHfId());


        //    IQueryable<ViewPatientVisitsListWithDetailDto> finalList = list.Select(x =>
        //        new ViewPatientVisitsListWithDetailDto
        //        {
        //            PatientVisitId = x.PatientLocationPrefixId,
        //            PatientId = x.PatientId,
        //            FullName = x.Patient!.FirstName + " " + x.Patient.LastName,
        //            MobileNo = x.Patient.MobileNo,
        //            Mrno = x.Patient.Mrno,
        //            Cnic = x.Patient.Cnic,
        //            VisitDate = x.VisitDate,
        //        });


        //    var pagedList = await PagedListDto<ViewPatientVisitsListWithDetailDto>.ToPagedListAsync(
        //           finalList,
        //           filter.PageNumber,
        //           filter.PageSize
        //           );

        //    var responseObject = new ViewPagerDto<ViewPatientVisitsListWithDetailDto>
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
        public async Task<ViewPatientLocationPrefixDto> GetById(Guid input)
        {
            PatientLocationPrefix? responseObj = await _uowPatientLocationPrefix.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewPatientLocationPrefixDto>(responseObj);
        }


  

        #endregion

        #region Helper Methods

        private void FillEntity(PatientLocationPrefix obj)
        {
            if (obj.Id <= 0)
            {
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
        private void FillEntityDelete(PatientLocationPrefix obj)
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
