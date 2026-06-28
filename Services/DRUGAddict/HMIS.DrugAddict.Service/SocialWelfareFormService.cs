using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.DrugAddict.Domain.Models.DbModels;
using HMIS.DrugAddict.Domain.Models.Dto.FilterDto;
using HMIS.DrugAddict.Domain.Models.Dto.PaginationDto;
using HMIS.DrugAddict.Domain.Models.Dto.SocialWelfareFormDto;
using HMIS.DrugAddict.Domain.Models.Dto.TestDto;
using HMIS.DrugAddict.Domain.Models.DTO.SocialWelfareFormDto;
using HMIS.DrugAddict.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Profile = HMIS.DrugAddict.Domain.Models.DbModels.Profile;


namespace HMIS.DrugAddict.Service
{
    public class SocialWelfareFormService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<SocialWelfareForm> _uowWelfareForm;
        private UnitOfWork<ViewSocialWellfarePatientDetail> _uowSocialWellfarePatientDetail;
        private UnitOfWork<ViewSocialWellfareDeputyDirector> _uowSocialWellfareDeputyDirector;

        private UnitOfWork<Profile> _uowProfile;

        #endregion

        #region Constructor

        public SocialWelfareFormService(TokenService tokenService, UnitOfWork<SocialWelfareForm> uowWelfareFormDto, UnitOfWork<Profile> uowProfile, IMapper mapper, UnitOfWork<ViewSocialWellfarePatientDetail> uowSocialWellfarePatientDetail)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowWelfareForm = uowWelfareFormDto;
            _uowSocialWellfarePatientDetail = uowSocialWellfarePatientDetail;
            _uowProfile = uowProfile;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditSocialWelfareFormDto> CreateOrEdit(CreateOrEditSocialWelfareFormDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.Id))
            {
                input.SessionNo = GetSessionCount(input.PatientVistId);
                return await Create(input);
            }
            else
            {
                return await Update(input);
            }
        }

        private async Task<CreateOrEditSocialWelfareFormDto> Create(CreateOrEditSocialWelfareFormDto input)
        {
            var obj = _mapper.Map<SocialWelfareForm>(input);



            FillEntity(obj);
            var profile = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.SWF).FirstOrDefault();


            if (profile != null)
            {
                obj.FormTypeProfileId = profile.ProfileId;

            }

            SocialWelfareForm responseObj = await _uowWelfareForm.Repository.Insert(obj);

            await _uowWelfareForm.CommitAsync();
            return _mapper.Map<CreateOrEditSocialWelfareFormDto>(responseObj);
        }

        private async Task<CreateOrEditSocialWelfareFormDto> Update(CreateOrEditSocialWelfareFormDto input)
        {
            var dbObj = await _uowWelfareForm.Repository.GetById(input.Id!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);
            var profile = _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.SWF).FirstOrDefault();


            if (profile != null)
            {
                obj.FormTypeProfileId = profile.ProfileId;

            }


            _uowWelfareForm.Repository.Update(obj!);
            await _uowWelfareForm.CommitAsync();

            return _mapper.Map<CreateOrEditSocialWelfareFormDto>(obj);
        }
        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowWelfareForm.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowWelfareForm.Repository.Update(dbObj!);
            await _uowWelfareForm.CommitAsync();
            return true;
        }

        private int GetSessionCount(Guid? patientVisitID)
        {
            int AddSession = 0;
            var checksession = _uowProfile.GetDbContext().SocialWelfareForms.Where(x => x.PatientVistId == patientVisitID).Count();
            //if (checksession == 0)

            //{
            //    AddSession = 1;
            //}
            //else if (checksession == 1)
            //{
            //    AddSession = 2;
            //}

            //else if (checksession == 2)
            //{
            //    AddSession = 3;
            //}
            AddSession = checksession + 1;

            return AddSession;
        }
        public async Task<SocaialWelfareAssignDoctorDto> AssignDoctorToPatient(SocaialWelfareAssignDoctorDto swoAssignDoctor)
        {
            var getPatientIfAssign = await _uowWelfareForm.GetDbContext().SocialWelfareAssignDoctors
                                        .Where(x => x.PatientId == swoAssignDoctor.PatientId).FirstOrDefaultAsync();
            if (getPatientIfAssign == null)
            {
                swoAssignDoctor.Swdoctorassignid = Guid.NewGuid();
                swoAssignDoctor.CreatedOn = DateTime.Now;
                var assignDoctor = _mapper.Map<SocialWelfareAssignDoctor>(swoAssignDoctor);
                assignDoctor.IsAssignDoctor = true;
                await _uowWelfareForm.GetDbContext().SocialWelfareAssignDoctors.AddAsync(assignDoctor);
                await _uowWelfareForm.Save();
                return swoAssignDoctor;
            }
            throw new UserFriendlyException("Already assigned doctor to patient");
        }

        #endregion

        #region Read Operations

        public async Task<List<CreateOrEditSocialWelfareFormDto>> GetAll(Expression<Func<SocialWelfareForm, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>
            ? orderBy = null,
            string includeProperties = "")
        {
            List<SocialWelfareForm> responseObj = await _uowWelfareForm.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<CreateOrEditSocialWelfareFormDto>>(responseObj);
        }

        public async Task<ViewPagerDto<ViewSocialWelfareDeputyDirectorDto>> GetAllWithPagination(FilterSocialWelfareFormDto filter)
        {

            var list = _uowWelfareForm.GetDbContext().ViewSocialWellfareDeputyDirectors
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.FullName!.ToLower().StartsWith(filter.SearchString))
                .OrderByDescending(x => x.CreatedOn);

            var pagedList = await PagedListDto<ViewSocialWellfareDeputyDirector>.ToPagedListAsync(
                   list,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewSocialWelfareDeputyDirectorDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = _mapper.Map<List<ViewSocialWelfareDeputyDirectorDto>>(pagedList)
            };

            return responseObject;
        }



        //public async Task<ViewPagerDto<ViewSocialWellfarePatientDetail>> GetAllWithPagination(FilterSocialWelfareFormDto filter)
        //{

        //    var list = _uowSocialWellfarePatientDetail.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
        //        .WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.FullName.ToLower().StartsWith(filter.SearchString))
        //        .OrderByDescending(x => x.CreatedOn);


        //    var pagedList = await PagedListDto<ViewSocialWellfarePatientDetail>.ToPagedListAsync(
        //           list,
        //           filter.PageNumber,
        //           filter.PageSize
        //           );

        //    var responseObject = new ViewPagerDto<ViewSocialWellfarePatientDetail>
        //    {
        //        TotalCount = pagedList.TotalCount,
        //        PageSize = pagedList.PageSize,
        //        CurrentPage = pagedList.CurrentPage,
        //        TotalPages = pagedList.TotalPages,
        //        HasNext = pagedList.HasNext,
        //        HasPrevious = pagedList.HasPrevious,
        //        List = _mapper.Map<List<ViewSocialWellfarePatientDetail>>(pagedList)
        //    };

        //    return responseObject;
        //}


        //public async Task<List<ViewSocialWelfareDeputyDirectorDto>> GetPatientsByTheirDistrict(int ReferDistrictId,string? searchTerm= "")
        public async Task<ViewPagerDto<ViewSocialWelfareDeputyDirectorDto>> GetPatientsByTheirDistrict(GetAllPatientCountFilterDto? filterDataObj)
        {
            // SearchString is ReferDistrictId

            var dbObj = _uowWelfareForm.GetDbContext().ViewSocialWellfareDeputyDirectors

                                                        .Where(x => x.DistrictId == int.Parse(filterDataObj!.SearchString) && x.IsAssignDoctor == true && x.ReferDistrictId== int.Parse(filterDataObj!.SearchString))

                                                        .Where(x => x.IsVisitClosed == true)
                                                        .WhereIf(filterDataObj!.searchTerm != null, x => x.FullName!.ToLower().Contains(filterDataObj.searchTerm!.ToLower())).OrderByDescending(x=>x.CreatedOn)
                                                        ;


            var pagedList = await PagedListDto<ViewSocialWellfareDeputyDirector>.ToPagedListAsync(
                dbObj,
                filterDataObj.PageNumber,
                filterDataObj.PageSize
            );


            var responseObject = new ViewPagerDto<ViewSocialWelfareDeputyDirectorDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = _mapper.Map<List<ViewSocialWelfareDeputyDirectorDto>>(pagedList)
            };

            return responseObject;
        }
 public async Task<ViewPagerDto<ViewSocialWelfareDeputyDirectorDto>> GetUnassignPatientsByTheirDistrict(GetAllPatientCountFilterDto? filterDataObj)
        {
            // SearchString is ReferDistrictId

            var dbObj = _uowWelfareForm.GetDbContext().ViewSocialWellfareDeputyDirectors

                                                        .Where(x => (x.DistrictId == int.Parse(filterDataObj!.SearchString) || x.ReferDistrictId== int.Parse(filterDataObj!.SearchString)) && x.IsAssignDoctor == null)

                                                        .Where(x => x.IsVisitClosed == true)
                                                        .WhereIf(filterDataObj!.searchTerm != null, x => x.FullName!.ToLower().Contains(filterDataObj.searchTerm!.ToLower())).OrderByDescending(x=>x.CreatedOn)
                                                        ;


            var pagedList = await PagedListDto<ViewSocialWellfareDeputyDirector>.ToPagedListAsync(
                dbObj,
                filterDataObj.PageNumber,
                filterDataObj.PageSize
            );


            var responseObject = new ViewPagerDto<ViewSocialWelfareDeputyDirectorDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = _mapper.Map<List<ViewSocialWelfareDeputyDirectorDto>>(pagedList)
            };

            return responseObject;
        }

        public async Task<List<ViewSocialWellfareDoctor>> GetDrugAddictDoctorsByTheirDistrict(int ReferDistrictId)
        {
            List<ViewSocialWellfareDoctor> dbObj = await _uowWelfareForm.GetDbContext()
                                                .ViewSocialWellfareDoctors.Where(x => x.DistrictId == ReferDistrictId)
                                                .ToListAsync();

            if (AppCommonMethod.IsNullOrEmptyList(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return dbObj;
        }

        public async Task<ViewPagerDto<PatientsSingleSessionDetailDto>> GetAllPatientsByDistrictName(GetAllPatientCountFilterDto?  filterDataObj)
        {
            var objlist = _uowWelfareForm.GetDbContext().ViewSocialWellfareDeputyDirectors
              .Where(x => x.IsVisitClosed != null)

                .WhereIf(filterDataObj!.searchTerm != null, x => x.PatientDistrictName == filterDataObj.searchTerm)
                .WhereIf(!string.IsNullOrEmpty(filterDataObj.SearchString),x=>x.PtStatus==filterDataObj.SearchString)
                ;


            if (filterDataObj.SearchString == "Secondary")
            {
                objlist = _uowWelfareForm.GetDbContext().ViewSocialWellfareDeputyDirectors
                              .Where(x => x.IsVisitClosed != null)
                                .WhereIf(filterDataObj!.searchTerm != null, x => x.PatientDistrictName == filterDataObj.searchTerm)
                                .WhereIf(!string.IsNullOrEmpty(filterDataObj.SearchString), x => x.PtStatus == "Secondary" || x.PtStatus == "Rehabilitated" || x.PtStatus == "Relapsed" || x.PtStatus == "Expired")
                                ;
            }

                //var patientList = _mapper.Map<List<PatientsSingleSessionDetailDto>>(objlist);

                var pagedList = await PagedListDto<ViewSocialWellfareDeputyDirector>.ToPagedListAsync(
                objlist,
                filterDataObj.PageNumber,
                filterDataObj.PageSize
                );

            var responseObject = new ViewPagerDto<PatientsSingleSessionDetailDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = _mapper.Map<List<PatientsSingleSessionDetailDto>>(pagedList)
            };

            return responseObject;

        }
        public async Task<CreateOrEditSocialWelfareFormDto> GetById(Guid input)
        {

            SocialWelfareForm? responseObj = await _uowWelfareForm.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<CreateOrEditSocialWelfareFormDto>(responseObj);
        }


        public async Task<ViewPagerDto<ViewSocialWellfarePatientDetail>> GetPatientsByPatientVisitId(GetAllPatientCountFilterDto? filterDataObj)
        {
            try
            {
                var PatientVisit = _uowWelfareForm.GetDbContext().ViewSocialWellfarePatientDetails
                .WhereIf(filterDataObj!.searchTerm != null, x => x.PatientId == Guid.Parse(filterDataObj.searchTerm!))
                .OrderBy(x => x.CreatedOn);

                var pagedList = await PagedListDto<ViewSocialWellfarePatientDetail>.ToPagedListAsync(
                    PatientVisit,
                    filterDataObj.PageNumber,
                    filterDataObj.PageSize
                    );

                var responseObject = new ViewPagerDto<ViewSocialWellfarePatientDetail>
                {
                    TotalCount = pagedList.TotalCount,
                    PageSize = pagedList.PageSize,
                    CurrentPage = pagedList.CurrentPage,
                    TotalPages = pagedList.TotalPages,
                    HasNext = pagedList.HasNext,
                    HasPrevious = pagedList.HasPrevious,
                    List = _mapper.Map<List<ViewSocialWellfarePatientDetail>>(pagedList)
                };

                return responseObject;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> GetPatientsVisitClosed(Guid id)
        {
            try
            {
                var PatientSessionList = await _uowWelfareForm.GetDbContext().SocialWelfareForms
                    .Where(x => x.PatientVistId == id).ToListAsync();
                if (PatientSessionList != null && PatientSessionList.Count > 0)
                {
                    SocialWelfareForm sessionToClose = null;
                    SocialWelfareForm largerSession = PatientSessionList[0];

                    foreach (var curSession in PatientSessionList)
                    {
                        if (curSession.SessionNo >= largerSession.SessionNo)
                        {
                            largerSession = curSession;
                        }
                    }

                    sessionToClose = largerSession;
                    sessionToClose.IsVisitClosed = true;
                    _uowWelfareForm.GetDbContext().SocialWelfareForms.Update(sessionToClose);
                    await _uowWelfareForm.CommitAsync();
                    return true;
                }
                return false;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }


        public async Task<bool> PatientIsDrugAddictOrNot(Guid patientOpenVisitId)
        {
            try
            {
                var obj = await _uowWelfareForm.GetDbContext().PatientOpenVisits
                .Where(x => x.PatientOpenVisitId == patientOpenVisitId)
                .FirstOrDefaultAsync();

                if (obj!.IsDrugAddict == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task<ViewPagerDto<ViewGetAllPatientsCountSw>> GetAllPatientsCount(GetAllPatientCountFilterDto? filterDataObj)
        {
            try
            {
                var patientList = _uowWelfareForm.GetDbContext().ViewGetAllPatientsCountSws
                    .Where(x => x.ProvinceId == filterDataObj!.ProvinceId)
                .WhereIf(filterDataObj!.searchTerm != null, x => x.PatientDistrictName!.ToLower().Contains(filterDataObj.searchTerm!))
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filterDataObj.DistrictId), x => x.PatientDistrctId == filterDataObj.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filterDataObj.DivisionId), x => x.PatientDivisionId == filterDataObj.DivisionId)
                .WhereIf(filterDataObj.StartDate != null, x => x.CreatedOn!.Value.Date >= filterDataObj.StartDate!.Value.Date)
                .WhereIf(filterDataObj.EndDate != null, x => x.CreatedOn!.Value.Date < filterDataObj.EndDate!.Value.Date)
                ;

                //var patientList = _uowWelfareForm.GetDbContext().ViewGetAllPatientsCountSws;

                var pagedList = await PagedListDto<ViewGetAllPatientsCountSw>.ToPagedListAsync(
                    patientList,
                    filterDataObj.PageNumber,
                    filterDataObj.PageSize
                    );

                var responseObject = new ViewPagerDto<ViewGetAllPatientsCountSw>
                {
                    TotalPrimary = patientList.Sum(x=>x.TotalPatients ??0),
                    TotalCount = pagedList.TotalCount,
                    PageSize = pagedList.PageSize,
                    CurrentPage = pagedList.CurrentPage,
                    TotalPages = pagedList.TotalPages,
                    HasNext = pagedList.HasNext,
                    HasPrevious = pagedList.HasPrevious,
                    List = _mapper.Map<List<ViewGetAllPatientsCountSw>>(pagedList)
                };
                return responseObject;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        // All Patients Count of Community Development
        public async Task<ViewPagerDto<ViewGetAllPatientsCountCd>> GetAllPatientsCountCd(GetAllPatientCountFilterDto? filterDataObj)
        {
            try
            {
                var patientList = _uowWelfareForm.GetDbContext().ViewGetAllPatientsCountCds
                .Where(x => x.ProvinceId == filterDataObj!.ProvinceId)
                    .WhereIf(filterDataObj!.searchTerm != null, x => x.PatientDistrictName!.ToLower().Contains(filterDataObj.searchTerm!))
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filterDataObj.DistrictId), x => x.PatientDistrctId == filterDataObj.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filterDataObj.DivisionId), x => x.PatientDivisionId == filterDataObj.DivisionId)
                .WhereIf(filterDataObj.StartDate != null, x => x.CreatedOn!.Value.Date >= filterDataObj.StartDate!.Value.Date)
                .WhereIf(filterDataObj.EndDate != null, x => x.CreatedOn!.Value.Date == filterDataObj.EndDate!.Value.Date)
                ;

                //var patientList = _uowWelfareForm.GetDbContext().ViewGetAllPatientsCountSws;

                var pagedList = await PagedListDto<ViewGetAllPatientsCountCd>.ToPagedListAsync(
                    patientList,
                    filterDataObj.PageNumber,
                    filterDataObj.PageSize
                    );

                var responseObject = new ViewPagerDto<ViewGetAllPatientsCountCd>
                {
                    TotalCount = pagedList.TotalCount,
                    TotalSecondary = patientList.Sum(x => x.TotalPatients ?? 0),
                    PageSize = pagedList.PageSize,
                    CurrentPage = pagedList.CurrentPage,
                    TotalPages = pagedList.TotalPages,
                    HasNext = pagedList.HasNext,
                    HasPrevious = pagedList.HasPrevious,
                    List = _mapper.Map<List<ViewGetAllPatientsCountCd>>(pagedList)
                };
                return responseObject;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // All Patients of CD Secondary

        public async Task<ViewPagerDto<ViewDrugAddictFieldOfficer>> GetAllPatientsByDistrictNameCDSec(GetAllPatientCountFilterDto? filterDataObj)
        {
            try
            {
                var objlist = _uowWelfareForm.GetDbContext().ViewDrugAddictFieldOfficers

                    .Where(x => x.VisitDate != null || x.PtStatus == "Secondary" || x.PtStatus == "Rehabilitated" || x.PtStatus == "Relapsed" || x.PtStatus == "Expired")
                    .WhereIf(filterDataObj!.searchTerm != null, x => x.PatientDistrictName == filterDataObj.searchTerm);
                    //.WhereIf(!string.IsNullOrEmpty(filterDataObj.SearchString), x => x.PtStatus ==filterDataObj.SearchString); 

                //var patientList = _mapper.Map<List<PatientsSingleSessionDetailDto>>(objlist);


                var pagedList = await PagedListDto<ViewDrugAddictFieldOfficer>.ToPagedListAsync(
                    objlist,
                    filterDataObj.PageNumber,
                    filterDataObj.PageSize
                    );

                var responseObject = new ViewPagerDto<ViewDrugAddictFieldOfficer>
                {
                    TotalCount = pagedList.TotalCount,
                   
                    PageSize = pagedList.PageSize,
                    CurrentPage = pagedList.CurrentPage,
                    TotalPages = pagedList.TotalPages,
                    HasNext = pagedList.HasNext,
                    HasPrevious = pagedList.HasPrevious,
                    List = _mapper.Map<List<ViewDrugAddictFieldOfficer>>(pagedList)
                };

                return responseObject;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        // All Patients Count of CD Rehabilitation
        public async Task<ViewPagerDto<ViewGetAllPatientsCountByPtStatusCd>> GetAllPatientsCountRehabilitationCd(GetAllPatientCountFilterDto? filterDataObj)
        {
            try
            {
                var patientList = _uowWelfareForm.GetDbContext().ViewGetAllPatientsCountByPtStatusCds
                    .Where(x => x.PtStatus == "Rehabilitated" && x.ProvinceId == filterDataObj!.ProvinceId)
                .WhereIf(filterDataObj!.searchTerm != null, x => x.PatientDistrictName!.ToLower().Contains(filterDataObj.searchTerm!))
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filterDataObj.DistrictId), x => x.PatientDistrctId == filterDataObj.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filterDataObj.DivisionId), x => x.PatientDivisionId == filterDataObj.DivisionId)
                .WhereIf(filterDataObj.StartDate != null, x => x.CreatedOn!.Value.Date == filterDataObj.StartDate!.Value.Date)
                .WhereIf(filterDataObj.EndDate != null, x => x.CreatedOn!.Value.Date == filterDataObj.EndDate!.Value.Date)
                ;

                //var patientList = _uowWelfareForm.GetDbContext().ViewGetAllPatientsCountSws;

                var pagedList = await PagedListDto<ViewGetAllPatientsCountByPtStatusCd>.ToPagedListAsync(
                    patientList,
                    filterDataObj.PageNumber,
                    filterDataObj.PageSize
                    );

                var responseObject = new ViewPagerDto<ViewGetAllPatientsCountByPtStatusCd>
                {
                    TotalCount = pagedList.TotalCount,
                    TotalRehabilitated = patientList.Sum(x => x.TotalPatients ?? 0),
                    PageSize = pagedList.PageSize,
                    CurrentPage = pagedList.CurrentPage,
                    TotalPages = pagedList.TotalPages,
                    HasNext = pagedList.HasNext,
                    HasPrevious = pagedList.HasPrevious,
                    List = _mapper.Map<List<ViewGetAllPatientsCountByPtStatusCd>>(pagedList)
                };
                return responseObject;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ViewPagerDto<ViewDrugAddictFieldOfficer>> GetAllPatientRehablitation(GetAllPatientCountFilterDto? filterDataObj)
        {
            try
            {
                var ptRehabList = _uowWelfareForm.GetDbContext().ViewDrugAddictFieldOfficers
                    .Where(x => x.PatientDistrictName == filterDataObj!.searchTerm
                                && x.PtStatus == "Rehabilitated");

                var pagedList = await PagedListDto<ViewDrugAddictFieldOfficer>.ToPagedListAsync(
                    ptRehabList,
                    filterDataObj!.PageNumber,
                    filterDataObj.PageSize
                    );

                var responseObject = new ViewPagerDto<ViewDrugAddictFieldOfficer>
                {
                    TotalCount = pagedList.TotalCount,
                    PageSize = pagedList.PageSize,
                    CurrentPage = pagedList.CurrentPage,
                    TotalPages = pagedList.TotalPages,
                    HasNext = pagedList.HasNext,
                    HasPrevious = pagedList.HasPrevious,
                    List = _mapper.Map<List<ViewDrugAddictFieldOfficer>>(pagedList)
                };
                return responseObject;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        // All Patients Count of CD Relapse
        public async Task<ViewPagerDto<ViewGetAllPatientsCountByPtStatusCd>> GetAllPatientsCountRelapseCd(GetAllPatientCountFilterDto? filterDataObj)
        {
            try
            {
                var patientList = _uowWelfareForm.GetDbContext().ViewGetAllPatientsCountByPtStatusCds
                    .Where(x => x.PtStatus == "Relapsed" && x.ProvinceId == filterDataObj!.ProvinceId)
                .WhereIf(filterDataObj!.searchTerm != null, x => x.PatientDistrictName!.ToLower().Contains(filterDataObj.searchTerm!))
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filterDataObj.DistrictId), x => x.PatientDistrctId == filterDataObj.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filterDataObj.DivisionId), x => x.PatientDivisionId == filterDataObj.DivisionId)
                .WhereIf(filterDataObj.StartDate != null, x => x.CreatedOn!.Value.Date == filterDataObj.StartDate!.Value.Date)
                .WhereIf(filterDataObj.EndDate != null, x => x.CreatedOn!.Value.Date == filterDataObj.EndDate!.Value.Date)
                ;

                //var patientList = _uowWelfareForm.GetDbContext().ViewGetAllPatientsCountSws;

                var pagedList = await PagedListDto<ViewGetAllPatientsCountByPtStatusCd>.ToPagedListAsync(
                    patientList,
                    filterDataObj.PageNumber,
                    filterDataObj.PageSize
                    );

                var responseObject = new ViewPagerDto<ViewGetAllPatientsCountByPtStatusCd>
                {
                    TotalCount = pagedList.TotalCount,
                    TotalRelapsed = patientList.Sum(x => x.TotalPatients ?? 0),
                    PageSize = pagedList.PageSize,              
                    CurrentPage = pagedList.CurrentPage,
                    TotalPages = pagedList.TotalPages,
                    HasNext = pagedList.HasNext,
                    HasPrevious = pagedList.HasPrevious,
                    List = _mapper.Map<List<ViewGetAllPatientsCountByPtStatusCd>>(pagedList)
                };
                return responseObject;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ViewPagerDto<ViewDrugAddictFieldOfficer>> GetAllPatientsRelapse(GetAllPatientCountFilterDto? filterDataObj)
        {
            try
            {
                var ptRehabList = _uowWelfareForm.GetDbContext().ViewDrugAddictFieldOfficers
                    .Where(x => x.PatientDistrictName == filterDataObj!.searchTerm
                    && x.PtStatus == "Relapsed");

                var pagedList = await PagedListDto<ViewDrugAddictFieldOfficer>.ToPagedListAsync(
                    ptRehabList,
                    filterDataObj!.PageNumber,
                    filterDataObj.PageSize
                    );

                var responseObject = new ViewPagerDto<ViewDrugAddictFieldOfficer>
                {
                    TotalCount = pagedList.TotalCount,
                    PageSize = pagedList.PageSize,
                    CurrentPage = pagedList.CurrentPage,
                    TotalPages = pagedList.TotalPages,
                    HasNext = pagedList.HasNext,
                    HasPrevious = pagedList.HasPrevious,
                    List = _mapper.Map<List<ViewDrugAddictFieldOfficer>>(pagedList)
                };
                return responseObject;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        // All Patients Count of Expire
        public async Task<ViewPagerDto<ViewGetAllPatientsCountByPtStatusCd>> GetAllPatientsCountExpireCd(GetAllPatientCountFilterDto? filterDataObj)
        {
            try
            {
                var patientList = _uowWelfareForm.GetDbContext().ViewGetAllPatientsCountByPtStatusCds
                    .Where(x => x.PtStatus == "Expired" && x.ProvinceId == filterDataObj!.ProvinceId)
                .WhereIf(filterDataObj!.searchTerm != null, x => x.PatientDistrictName!.ToLower().Contains(filterDataObj.searchTerm!))
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filterDataObj.DistrictId), x => x.PatientDistrctId == filterDataObj.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filterDataObj.DivisionId), x => x.PatientDivisionId == filterDataObj.DivisionId)
                .WhereIf(filterDataObj.StartDate != null, x => x.CreatedOn!.Value.Date == filterDataObj.StartDate!.Value.Date)
                .WhereIf(filterDataObj.EndDate != null, x => x.CreatedOn!.Value.Date == filterDataObj.EndDate!.Value.Date)
                ;

                //var patientList = _uowWelfareForm.GetDbContext().ViewGetAllPatientsCountSws;

                var pagedList = await PagedListDto<ViewGetAllPatientsCountByPtStatusCd>.ToPagedListAsync(
                    patientList,
                    filterDataObj.PageNumber,
                    filterDataObj.PageSize
                    );

                var responseObject = new ViewPagerDto<ViewGetAllPatientsCountByPtStatusCd>
                {
                    TotalCount = pagedList.TotalCount,
                    PageSize = pagedList.PageSize,
                    CurrentPage = pagedList.CurrentPage,
                    TotalPages = pagedList.TotalPages,
                    HasNext = pagedList.HasNext,
                    HasPrevious = pagedList.HasPrevious,
                    List = _mapper.Map<List<ViewGetAllPatientsCountByPtStatusCd>>(pagedList)
                };
                return responseObject;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ViewPagerDto<ViewDrugAddictFieldOfficer>> GetAllPatientsExpire(GetAllPatientCountFilterDto? filterDataObj)
        {
            try
            {
                var ptRehabList = _uowWelfareForm.GetDbContext().ViewDrugAddictFieldOfficers
                    .Where(x => x.PatientDistrictName == filterDataObj!.searchTerm
                    && x.PtStatus == "Expired");

                var pagedList = await PagedListDto<ViewDrugAddictFieldOfficer>.ToPagedListAsync(
                    ptRehabList,
                    filterDataObj!.PageNumber,
                    filterDataObj.PageSize
                    );

                var responseObject = new ViewPagerDto<ViewDrugAddictFieldOfficer>
                {
                    TotalCount = pagedList.TotalCount,
                    PageSize = pagedList.PageSize,
                    CurrentPage = pagedList.CurrentPage,
                    TotalPages = pagedList.TotalPages,
                    HasNext = pagedList.HasNext,
                    HasPrevious = pagedList.HasPrevious,
                    List = _mapper.Map<List<ViewDrugAddictFieldOfficer>>(pagedList)
                };
                return responseObject;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }


        // Get All CD Patients by visit id

        public async Task<List<ViewDrugAddictCommunityDevelopment>> GetSinglePatientsVisitCD(Guid pvid)
        {
            try
            {
                var singlePatientSessionsList = await _uowWelfareForm.GetDbContext()
                .ViewDrugAddictCommunityDevelopments
                .Where(x => x.PatientVisitId == pvid).ToListAsync();
                if (AppCommonMethod.IsNullOrEmptyList(singlePatientSessionsList))
                    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                return singlePatientSessionsList;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }

        public async Task<bool> CheckPatientIsInTheDiaganoseORNot(Guid patientVisitID)
        {
            try
            {
                var list = await _uowWelfareForm.GetDbContext().PatientDiagnoses
                    .Where(x => x.PatientVisitId == patientVisitID).ToListAsync();
                if (list.Count > 0)
                    return true;
                else
                    return false;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
        }
        #endregion

        #region Helper Methods

        private void FillEntity(SocialWelfareForm obj)
        {
            if (obj.Id == Guid.Empty)
            {
                obj.Id = Guid.NewGuid();
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
        private void FillEntityDelete(SocialWelfareForm obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion

    }
}
