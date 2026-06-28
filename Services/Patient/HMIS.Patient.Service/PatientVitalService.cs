using System.Linq.Expressions;
using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.Common;
using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using HMIS.Patient.Domain.Models.DTO.PatientDto;
using HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto;
using HMIS.Patient.Domain.Models.DTO.PatientVitalDto;
using HMIS.Patient.Domain.Models.DTO.PatientWorkFlowLogDto;
using HMIS.Patient.Domain.Repositories.UOW;
using HMIS.Patient.Service.Common;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using SMSSender.DTO;
using SMSSender;
using DbModel = HMIS.Patient.Domain.Models.DbModels;
using AuthDAL.Models.Dto.HfDepartmentSectionDto;
using Microsoft.Data.SqlClient;
using System.Data;


namespace HMIS.Patient.Service
{
    public class PatientVitalService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly PatientWorkFlowLogService<PatientWorkFlowLog> _patientWorkFlowLogService;

        private readonly IMapper _mapper;
        private readonly UnitOfWork<PatientVital> _uowPatientVital;
        //private readonly UnitOfWork<PatientOpenVisit> _uowPatientOpenVisit;
        //private readonly UnitOfWork<HealthFacilityStation> _uowHealthFacilityStation;
        //private readonly UnitOfWork<User> _uowUser;
        //private readonly UnitOfWork<DbModel.Profile> _uowProfile;
        private readonly SMS _smsService;


        #endregion

        #region Constructor

        public PatientVitalService(TokenService tokenService, PatientWorkFlowLogService<PatientWorkFlowLog> patientWorkFlowLogService, UnitOfWork<DbModel.PatientVital> uowPatientVital, IMapper mapper, SMS smsService)
        {
            _tokenService = tokenService;
            _patientWorkFlowLogService = patientWorkFlowLogService;
            _uowPatientVital = uowPatientVital;
            //_uowPatientOpenVisit = uowPatientOpenVisit;
            //_uowHealthFacilityStation = uowHealthFacilityStation;
            //_uowUser = uowUser;
            //_uowProfile = uowProfile;
            _mapper = mapper;
            _smsService = smsService;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditPatientVitalDto> CreateOrEdit(CreateOrEditPatientVitalDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientVitalId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditPatientVitalDto> Create(CreateOrEditPatientVitalDto input)
        {
            var obj = _mapper.Map<DbModel.PatientVital>(input);
            FillEntity(obj);
            obj.VitalsCollectedBy = _tokenService.GetUserId();
            DbModel.PatientVital responseObj = await _uowPatientVital.Repository.Insert(obj);
            await _uowPatientVital.Save();
            return _mapper.Map<CreateOrEditPatientVitalDto>(responseObj);
        }

        private async Task<CreateOrEditPatientVitalDto> Update(CreateOrEditPatientVitalDto input)
        {
            var dbObj = await _uowPatientVital.Repository.GetById(input.PatientVitalId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);
            _uowPatientVital.Repository.Update(obj!);
            await _uowPatientVital.CommitAsync();
            return _mapper.Map<CreateOrEditPatientVitalDto>(obj);
        }

        public async Task<CreateOrEditPatientVitalDto> CreateOrEditPatientVitals(CreateOrEditPatientVitalDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientVitalId))
                return await CreatePatientVitals(input);
            else
                return input;
        }

        private async Task<CreateOrEditPatientVitalDto> CreatePatientVitals(CreateOrEditPatientVitalDto input)
        {
            using (var trans = _uowPatientVital.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    // create UOW
                    var _uowPatientWorkLog = new UnitOfWork<PatientWorkFlowLog>(_uowPatientVital.GetDbContext());
                    var _uowHealthFacilityStation = new UnitOfWork<HealthFacilityStation>(_uowPatientVital.GetDbContext());
                    var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowPatientVital.GetDbContext());
                    var _uowUser = new UnitOfWork<User>(_uowPatientVital.GetDbContext());
                    var _uowPatientWorkFlowLog = new UnitOfWork<PatientWorkFlowLog>(_uowPatientVital.GetDbContext());
                    var _uowPatient = new UnitOfWork<DbModel.Patient>(_uowPatientVital.GetDbContext());
                    var _uowDrugAddict = new UnitOfWork<PatientDrugAddiction>(_uowPatientVital.GetDbContext());
                    var _uowCoMorbid = new UnitOfWork<PatientCoMorbid>(_uowPatientVital.GetDbContext());

                    PatientOpenVisit? dbVisit = await _uowPatientOpenVisit.Repository.GetById(input.PatientVisitId!);

                    if (AppCommonMethod.IsNullObject(dbVisit))
                        throw new UserFriendlyException(CommonMessageConstant.VisitNotExists);

                    var isVisitClose = input.IsVisitClose;
                    var tokenUserId = _tokenService.GetUserId();
                    var user = await _uowUser.Repository.GetALL(x => x.UserId == tokenUserId).FirstOrDefaultAsync();
                    //check for next station
                    var stationsList = await _uowHealthFacilityStation.Repository.GetALL(x => x.HealthFacilityId == user!.HealthFacilityId && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true).OrderBy(x => x.SequenceNo).Include(x => x.StationProfile)
                    .Select(x => new ViewStationDto
                    {
                        StationProfileId = x.StationProfileId,
                        SequenceNo = x.SequenceNo,
                        ShortName = x.StationProfile!.ShortName,
                        Name = x.StationProfile.Name,
                    }).ToListAsync();

                    // If stations are not defined then fetch from lookup
                    if (AppCommonMethod.IsNullOrEmptyList(stationsList))
                    {
                        //Station from Profiles
                        var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatientVital.GetDbContext());

                        stationsList = await _uowProfile.Repository.GetALL(x => x.ProfileType.ShortName == CommonStringConstant.CounterStations && x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                            .OrderBy(x => x.SequenceNo)
                        .Select(x => new ViewStationDto
                        {
                            StationProfileId = x.ProfileId,
                            SequenceNo = x.SequenceNo,
                            ShortName = x.ShortName,
                            Name = x.Name
                        }).ToListAsync();
                    }

                    var thisStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.ShortName == CommonStringConstant.VitalStation).FirstOrDefault();

                    if (AppCommonMethod.IsNullObject(thisStation))
                        throw new UserFriendlyException(CommonMessageConstant.HealthFacilityNotHaveThisStation);

                    var nextStation = stationsList.OrderBy(x => x.SequenceNo).Where(x => x.SequenceNo > thisStation!.SequenceNo).FirstOrDefault();
                    if (AppCommonMethod.IsNullObject(nextStation))
                    {
                        isVisitClose = true;
                        nextStation = thisStation; // if Visit is close then Next Station is set to Current Station

                        var patientDetail = await _uowPatient.Repository.GetById(input.PatientId!);
                        // SMS on Visit Close
                        //SendSMSDto smsObj = new SendSMSDto()
                        //{
                        //    Receiver = patientDetail!.MobileNo!.Replace("-", ""),
                        //    Body = $"Dear {patientDetail!.FirstName}, Thank You for Your Visit."
                        //};

                        //_smsService.SendSMS(smsObj);
                    }

                    PatientVital? prevObj = await _uowPatientVital.Repository.GetALL(x => x.PatientVisitId == input.PatientVisitId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();

                    var newObj = _mapper.Map<DbModel.PatientVital>(input);

                    if (!string.IsNullOrEmpty(input!.FormType))
                    {
                        if (input!.FormType == CommonStringConstant.NCDClinicForm || input!.FormType == CommonStringConstant.MuawinClinicsForm)
                        {
                            if (input.IsFollowup)
                            {
                                if (!AppCommonMethod.IsNullOrEmptyGuid(input.PatientLastVisitId))
                                {
                                    var lastPatientVitals = await GetLastVisitVitals((Guid)input.PatientLastVisitId, _uowPatientVital);
                                    if (!AppCommonMethod.IsNullObject(lastPatientVitals))
                                    {
                                        newObj = await UpdatedObjForVitals(input, newObj, lastPatientVitals);
                                    }
                                }
                            }
                        }
                    }


                    FillEntity(newObj);
                    input.VitalsCollectedByDesignation = TokenService.GetUserLoggedInfo()?.DesignationName;

                    if (!AppCommonMethod.IsNullObject(prevObj))
                        FillEntityCheckIfChange(prevObj, newObj);


                    if (dbVisit!.IsFromPmis)
                        newObj.VitalsCollectedBy = dbVisit.AttendedBy;
                    else
                        newObj.VitalsCollectedBy = _tokenService.GetUserId();


                    DbModel.PatientVital responseObj = await _uowPatientVital.Repository.Insert(newObj);
                    input.PatientVitalId = responseObj.PatientVitalId;
                    await _uowPatientVital.Save();

                    // update Visit
                    if (!isVisitClose)
                    {
                        dbVisit!.CurrentStationProfileId = nextStation!.StationProfileId;
                        dbVisit!.IsAdmitted = true;
                    }

                    dbVisit!.IsDischarge = isVisitClose;
                    dbVisit!.IsOccupied = false;
                    dbVisit!.OccupiedBy = null;

                    if (!AppCommonMethod.IsNullOrEmptyGuid(input.PatientConditionProfileId))
                        dbVisit!.PatientConditionProfileId = input.PatientConditionProfileId;

                    dbVisit.UpdatedBy = _tokenService.GetUserId();
                    dbVisit.UpdatedOn = DateTime.Now;
                    dbVisit.ActionTypeId = (int)ActionTypeEnum.Edit;

                    var _uowSectionLookUp = new UnitOfWork<SectionLookup>(_uowPatient.GetDbContext());
                    var patientSection = await _uowSectionLookUp.Repository.GetALL(x => x.SectionLookupId == dbVisit.SectionLookupId).FirstOrDefaultAsync();
                    if (patientSection.FormType == CommonStringConstant.NCDClinicForm || patientSection.FormType == CommonStringConstant.MuawinClinicsForm)
                    {
                        await ReferPatient(input, dbVisit);
                        if (input.IsNcdPositive == false && input.IsMuawinPositive == false)
                        {
                            isVisitClose = true;
                            nextStation = thisStation;
                            dbVisit.CurrentStationProfileId = nextStation!.StationProfileId;
                            dbVisit.IsDischarge = isVisitClose;
                        }
                    }


                    _uowPatientOpenVisit.Repository.Update(dbVisit);
                    await _uowPatientVital.Save();

                    // log Data
                    // Create Patient Work Log
                    CreateOrEditPatientWorkFlowLogDto objPatientWorkFlowLog = new CreateOrEditPatientWorkFlowLogDto();

                    objPatientWorkFlowLog.PatientId = input.PatientId;
                    objPatientWorkFlowLog.PatientVisitId = input.PatientVisitId;
                    objPatientWorkFlowLog.HealthFacilityId = user!.HealthFacilityId;
                    objPatientWorkFlowLog.CurrentStationProfileId = thisStation!.StationProfileId;
                    objPatientWorkFlowLog.NextStationProfileId = !(isVisitClose) ? nextStation!.StationProfileId : null;
                    objPatientWorkFlowLog.IsVisitClose = isVisitClose;
                    objPatientWorkFlowLog.IsActive = true;

                    if (!AppCommonMethod.IsNullObject(input.DrugAddiction))
                    {
                        foreach (var _obj in input.DrugAddiction!)
                        {
                            PatientDrugAddiction patientDrugAddiction = new PatientDrugAddiction();
                            FillEntityDrugAddict(patientDrugAddiction);
                            patientDrugAddiction.AddictionProfileId = _obj.ProfileId;
                            patientDrugAddiction.PatientId = input.PatientId;
                            patientDrugAddiction.PatientVisitId = input.PatientVisitId;
                            await _uowDrugAddict.Repository.Insert(patientDrugAddiction);
                            await _uowDrugAddict.Save();

                        }
                    }



                    if (!AppCommonMethod.IsNullObject(input.CoMorbid))
                    {
                        foreach (var _obj in input.CoMorbid!)
                        {
                            PatientCoMorbid coMorbid = new PatientCoMorbid();
                            FillEntityCoMorbid(coMorbid);
                            coMorbid.CoMorbidProfileId = _obj.ProfileId;
                            coMorbid.PatientId = input.PatientId;
                            coMorbid.PatientVisitId = input.PatientVisitId;
                            await _uowCoMorbid.Repository.Insert(coMorbid);
                            await _uowCoMorbid.Save();

                        }
                    }


                    //var _newPatientWorkFlowLogService = new PatientWorkFlowLogService<PatientWorkFlowLog>(_tokenService, _uowPatientWorkLog, _mapper);

                    await _patientWorkFlowLogService.CreatePatientWorkLog(objPatientWorkFlowLog);

                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }

            return _mapper.Map<CreateOrEditPatientVitalDto>(input);
        }

        //private async Task<CreateOrEditPatientVitalDto> UpdatePatientVitals(CreateOrEditPatientVitalDto input)
        //{
        //    //var dbObj = await _uowPatientVital.Repository.GetById(input.PatientVitalId!);

        //    //if (AppCommonMethod.IsNullObject(dbObj))
        //    //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

        //    //var obj = _mapper.Map(input, dbObj);
        //    //FillEntity(obj!);
        //    //_uowPatientVital.Repository.Update(obj!);
        //    //await _uowPatientVital.CommitAsync();
        //    return _mapper.Map<CreateOrEditPatientVitalDto>(input);
        //}

        public async Task ReferPatient(CreateOrEditPatientVitalDto input, PatientOpenVisit dbVisit)
        {



            var _uowSectionLookUp = new UnitOfWork<SectionLookup>(_uowPatientVital.GetDbContext());
            //var patientSection =  await _uowSectionLookUp.Repository.GetALL(x => x.SectionLookupId == patientVisitObj.SectionLookupId).FirstOrDefaultAsync();


            if (input.IsNcdPositive == true && input.IsMuawinPositive == true)
            {
                int sectionId = await _uowSectionLookUp.Repository.GetALL(x => x.FormType == CommonStringConstant.NcdAndMuawinClinicForm).Select(x => x.SectionLookupId).FirstOrDefaultAsync();
                if (!AppCommonMethod.IsNullorZeroInt(sectionId))
                {
                    dbVisit.ReferredDepartmentLookupId = dbVisit.DepartementLookupId;
                    dbVisit.ReferredSectionLookupId = dbVisit.SectionLookupId;
                    dbVisit.SectionLookupId = sectionId;
                }
            }

            else if (input.IsNcdPositive == true)
            {
                int sectionId = await _uowSectionLookUp.Repository.GetALL(x => x.FormType == CommonStringConstant.NCDClinicForm).Select(x => x.SectionLookupId).FirstOrDefaultAsync();
                int regVisitId = await _uowSectionLookUp.Repository.GetALL(x => x.SectionLookupId == dbVisit.SectionLookupId).Select(x => x.SectionLookupId).FirstOrDefaultAsync();
                if (!AppCommonMethod.IsNullorZeroInt(sectionId))
                {
                    if (sectionId != regVisitId)
                    {
                        dbVisit.ReferredDepartmentLookupId = dbVisit.DepartementLookupId;
                        dbVisit.ReferredSectionLookupId = dbVisit.SectionLookupId;
                        dbVisit.SectionLookupId = sectionId;
                    }
                }
            }
            else if (input.IsMuawinPositive == true)
            {
                int sectionId = await _uowSectionLookUp.Repository.GetALL(x => x.FormType == CommonStringConstant.MuawinClinicsForm).Select(x => x.SectionLookupId).FirstOrDefaultAsync();
                int regVisitId = await _uowSectionLookUp.Repository.GetALL(x => x.SectionLookupId == dbVisit.SectionLookupId).Select(x => x.SectionLookupId).FirstOrDefaultAsync();
                if (!AppCommonMethod.IsNullorZeroInt(sectionId))
                {
                    if (sectionId != regVisitId)
                    {
                        dbVisit.ReferredDepartmentLookupId = dbVisit.DepartementLookupId;
                        dbVisit.ReferredSectionLookupId = dbVisit.SectionLookupId;
                        dbVisit.SectionLookupId = sectionId;
                    }
                }
            }

        }
        public async Task<PatientVital> GetLastVisitVitals(Guid PatientLastVisitId, UnitOfWork<PatientVital> _uowPatientVital)
        {
            PatientVital? lastVisitVitals = await _uowPatientVital.Repository.GetALL(x => x.PatientVisitId == PatientLastVisitId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();

            return lastVisitVitals;
        }


        public async Task<PatientVital> UpdatedObjForVitals(CreateOrEditPatientVitalDto input, PatientVital newObj, PatientVital? lastVisitVitals)
        {
            newObj = _mapper.Map<DbModel.PatientVital>(lastVisitVitals);
            newObj.PatientVitalId = new Guid();
            newObj.PatientVisitId = input.PatientVisitId;
            newObj.BloodSugar = input.BloodSugar;
            newObj.Bpsystolic = input.Bpsystolic;
            newObj.BpdiaSystolic = input.BpdiaSystolic;

            return newObj;
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowPatientVital.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowPatientVital.Repository.Update(dbObj!);
            await _uowPatientVital.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewPatientVitalDto>> GetAll(Expression<Func<DbModel.PatientVital, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<DbModel.PatientVital> responseObj = await _uowPatientVital.Repository.GetALL(filter!).ToListAsync();
            return _mapper.Map<List<ViewPatientVitalDto>>(responseObj);
        }

        public async Task<ViewPagerDto<ViewPatientVitalListDto>> GetAllPatientVitalsList(FilterPatientVitalDto filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowPatientVital.GetDbContext());


            var finalList = _uowPatientOpenVisit.GetDbContext().ViewPatientVitalLists
                .WhereIf(!string.IsNullOrEmpty(filter.FullName), x => x.PatientName!.ToLower().StartsWith(filter.FullName!))
                .WhereIf(!string.IsNullOrEmpty(filter.Cnic), x => x.Cnic == filter.Cnic)
                .WhereIf(!string.IsNullOrEmpty(filter.MobileNo), x => x.PatientMobileNo!.ToLower().StartsWith(filter.MobileNo!))
                .WhereIf(!string.IsNullOrEmpty(filter.Mrno), x => x.Mrno!.ToLower().StartsWith(filter.Mrno!))
                .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.CreatedBy.ToString()!))

                //.WhereIf(!TokenService.IsSuperAdmin(), x => x.HealthFacilityId == TokenService.GetUserHfId())
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.PatientProvinceId), x => x.PatientProvinceId == filter.PatientProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.MinWeight), x => Convert.ToInt16(x.Weight) >= filter.MinWeight)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.MaxWeight), x => Convert.ToInt16(x.Weight) <= filter.MaxWeight)

                .WhereIf(filter.NormalRespitary, x => Convert.ToInt32(x.ResperatoryRate) >= 12 && Convert.ToInt32(x.ResperatoryRate) <= 20)
                .WhereIf(filter.HighRespitary, x => Convert.ToInt32(x.ResperatoryRate) >= 21 && Convert.ToInt32(x.ResperatoryRate) <= 60)

                .WhereIf(filter.NormalTemperature, x => Convert.ToInt32(x.Temprature) >= 95 && Convert.ToInt32(x.Temprature) <= 99)
                .WhereIf(filter.HighTemperature, x => Convert.ToInt32(x.Temprature) >= 100)


                .WhereIf(filter.NormalPulse, x => Convert.ToInt32(x.Pulse) >= 60 && Convert.ToInt32(x.Pulse) <= 100)
                .WhereIf(filter.LowPulse, x => Convert.ToInt32(x.Pulse) < 60)
                .WhereIf(filter.HighPulse, x => Convert.ToInt32(x.Pulse) > 100)

                .WhereIf(filter.NormalBloodPressure, x => Convert.ToInt32(x.BpdiaSystolic) >= 60 && Convert.ToInt32(x.BpdiaSystolic) <= 90 && Convert.ToInt32(x.Bpsystolic) >= 80 && Convert.ToInt32(x.Bpsystolic) <= 120)
                .WhereIf(filter.LowBloodPressure, x => Convert.ToInt32(x.Bpsystolic) < 90)
                .WhereIf(filter.HighBloodPressure, x => Convert.ToInt32(x.Bpsystolic) >= 140)



                .WhereIf(!AppCommonMethod.IsNullOrEmptyGuid(filter.GenderId), x => x.GenderId == filter.GenderId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.ProvinceId == filter.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value < filter.EndDate!.Value)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x =>
                new ViewPatientVitalListDto
                {
                    PatientVisitId = x.PatientVisitId,
                    PatientId = x.PatientId,
                    PatientVitalId = x.PatientVitalId,
                    FullName = x.PatientName,
                    MobileNo = x.PatientMobileNo,
                    Mrno = x.Mrno,
                    Cnic = x.Cnic,
                    VisitDate = x.VisitDate,
                    CreatedBy = x.CreatedByName!,
                    CreatedOn = x.CreatedOn,
                    UpdatedBy = x.UpdatedByName!,
                    UpdatedOn = x.UpdatedOn,
                    HealthFacilityId = x.HealthFacilityId,
                    HealthFacilityName = x.HealthFacilityName,
                });

            var pagedList = await PagedListDto<ViewPatientVitalListDto>.ToPagedListAsync(
                   finalList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewPatientVitalListDto>
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

        public async Task<ViewPagerDto<ViewPatientVitalListDto>> GetRefferedPatientVitalsList(FilterPatientVitalDto filter)
        {

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowPatientVital.GetDbContext());


            var finalList = _uowPatientOpenVisit.GetDbContext().ViewPatientOpenVisitDetails
                .WhereIf(!string.IsNullOrEmpty(filter.FullName), x => x.FullName!.ToLower().StartsWith(filter.FullName!))
                .WhereIf(!string.IsNullOrEmpty(filter.Cnic), x => x.Cnic == filter.Cnic)
                .WhereIf(!string.IsNullOrEmpty(filter.MobileNo), x => x.MobileNo!.ToLower().StartsWith(filter.MobileNo!))
                .WhereIf(!string.IsNullOrEmpty(filter.Mrno), x => x.Mrno!.ToLower().StartsWith(filter.Mrno!))
                .WhereIf(!string.IsNullOrEmpty(filter.User), x => user.Contains(x.PatientVisitCreatedBy.ToString()!))
                .WhereIf(!string.IsNullOrEmpty(filter.CurrentStation), x => x.CurrentStation == filter.CurrentStation)

                //.WhereIf(!TokenService.IsSuperAdmin(), x => x.HealthFacilityId == TokenService.GetUserHfId())
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.PatientProvinceId), x => x.PatientProvinceId == filter.PatientProvinceId)


                //.WhereIf(!AppCommonMethod.IsNullOrEmptyGuid(filter.GenderId), x => x.GenderId == filter.GenderId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.ProvinceId == filter.ProvinceId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId), x => x.DepartementLookupId == filter.DepartmentId)
                // .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.SectionId), x => x.SectionLookupId == filter.SectionId)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value >= filter.StartDate!.Value)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value < filter.EndDate!.Value)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x =>
                new ViewPatientVitalListDto
                {
                    FullName = x.FullName,
                    MobileNo = x.MobileNo,
                    Mrno = x.Mrno,
                    Cnic = x.Cnic,
                    VisitDate = x.VisitDate,
                    CreatedBy = x.PatientVisitCreatedByName,
                    CreatedOn = x.CreatedOn,
                    HealthFacilityId = x.HealthFacilityId,
                    HealthFacilityName = x.VisitHf,
                });

            var pagedList = await PagedListDto<ViewPatientVitalListDto>.ToPagedListAsync(
                   finalList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewPatientVitalListDto>
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
        public async Task<ViewPatientVitalDto> GetById(Guid input)
        {
            DbModel.PatientVital? responseObj = await _uowPatientVital.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewPatientVitalDto>(responseObj);
        }


        public async Task<List<ViewPatientQueDto>> GetAllQue(int? HealthFacilityId)
        {
            //var _uowUser = new UnitOfWork<User>(_uowPatientVital.GetDbContext());
            var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowPatientVital.GetDbContext());

            //var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
            var dbUser = TokenService.GetUserLoggedInfo();

            //Vitals Station
            var _uowProfile = new UnitOfWork<DbModel.Profile>(_uowPatientVital.GetDbContext());
            Guid? vitalStation = await _uowProfile.Repository.GetALL(x => x.ShortName == CommonStringConstant.VitalStation).Select(x => x.ProfileId).FirstOrDefaultAsync();

            var responseObj = await _uowPatientOpenVisit.Repository.GetALL()
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.DepartmentId), x => x.DepartementLookupId == dbUser.DepartmentId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.SectionId), x => x.SectionLookupId == dbUser.SectionId)
                .WhereIf((AppCommonMethod.IsNullorZeroInt(dbUser!.DepartmentId) || dbUser!.DepartmentName == CommonStringConstant.OutPatientDepartment), x => x.VisitFor == CommonStringConstant.OPD || x.VisitFor == null)
                .WhereIf(dbUser!.DepartmentName == CommonStringConstant.InPatientDepartment, x => x.VisitFor == CommonStringConstant.IPD)
                .WhereIf(dbUser!.DepartmentName == CommonStringConstant.ERDepartment, x => x.VisitFor == CommonStringConstant.ER)

                .Include(x => x.Patient)
                .Include(x => x.DepartementLookup)
                .Include(x => x.SectionLookup)
                .Where(x =>
                    x.IsDischarge != true &&
                    x.VisitDate == DateTime.Today &&
                    x.HealthFacilityId == HealthFacilityId &&
                    x.CurrentStationProfileId == vitalStation &&
                    //(x.VitalCollectedBy != null ? x.VitalCollectedBy == _tokenService.GetUserId() : true)
                    (x.IsOccupied == true && x.OccupiedBy != null ? x.OccupiedBy == _tokenService.GetUserId() : true)
                    )
                .OrderBy(x => x.TokenNo)
                .Select(y => new ViewPatientQueDto
                {

                    PatientVisitId = y.PatientOpenVisitId,
                    PatientId = y.PatientId,
                    TokenNo = y.TokenNo,
                    Mrno = y.Patient!.Mrno,
                    CNIC = y.Patient.Cnic,
                    MobileNo = y.Patient.MobileNo,
                    FirstName = y.Patient.FirstName,
                    LastName = y.Patient.LastName,
                    FullName = y.Patient.FullName,
                    RelationProfileId = y.Patient.RelationProfileId,
                    DepartmentName = y.DepartementLookup!.Name,
                    SectionName = y.SectionLookup!.Name,

                }).ToListAsync();
            return _mapper.Map<List<ViewPatientQueDto>>(responseObj);
        }

        public async Task<List<ViewVitalSlipDto>> GetByVisitId(Guid VisitId)
        {
            var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowPatientVital.GetDbContext());
            var data = await _uowPatientOpenVisit.Repository.GetALL(x => x.PatientOpenVisitId == VisitId).FirstOrDefaultAsync();
            var roomAndFloorNo = await GetHfDepartmentSectionByHealthFacilityId((int)data.HealthFacilityId, (int)data.DepartementLookupId, (int)data.SectionLookupId);

            var responseObj = await _uowPatientVital.Repository.GetALL(x => x.PatientVisitId == VisitId)
                .Include(x => x.VitalsCollectedByNavigation)
                .Include(x => x.PatientVisit)
                    .ThenInclude(x => x!.HealthFacility)
                .Include(x => x.PatientVisit)
                    .ThenInclude(x => x!.PatientConditionProfile)
                .Include(x => x.Patient)
                .Select(x => new ViewVitalSlipDto
                {
                    PatientVitalId = x.PatientVitalId,
                    HealthFacilityName = x.PatientVisit!.HealthFacility!.Name!,
                    PatientName = x.Patient!.FullName!,
                    TokenNo = x.PatientVisit!.TokenNo!,
                    VisitNo = x.PatientVisit.VisitNo,
                    VisitDate = x.PatientVisit.CreatedOn,
                    CreatedOn = x.CreatedOn,
                    VitalsBy = x.VitalsCollectedByNavigation!.FullName!,
                    VitalsByDesignation = x.VitalsCollectedByNavigation!.DesignationProfile!.Name!,
                    MrNo = x.Patient.Mrno,
                    Bpsystolic = x.Bpsystolic,
                    BpdiaSystolic = x.BpdiaSystolic,
                    Pulse = x.Pulse,
                    Temprature = x.Temprature,
                    Height = x.Height,
                    Weight = x.Weight,
                    ResperatoryRate = x.ResperatoryRate,
                    PatientCondition = x.PatientVisit.PatientConditionProfile!.Name,
                    vitalsFloor = string.IsNullOrEmpty(roomAndFloorNo!.VitalsFloorNo) ? null : roomAndFloorNo!.VitalsFloorNo,
                    vitalsRoom = string.IsNullOrEmpty(roomAndFloorNo!.VitalsRoomNo) ? null : roomAndFloorNo!.VitalsRoomNo,
                    DoctorFloorNo = string.IsNullOrEmpty(roomAndFloorNo!.DoctorFloorNo) ? null : roomAndFloorNo!.DoctorFloorNo,
                    DoctorRoomNo = string.IsNullOrEmpty(roomAndFloorNo!.DoctorRoomNo) ? null : roomAndFloorNo!.DoctorRoomNo
                }).OrderByDescending(x => x.CreatedOn).ToListAsync();
            return responseObj!;
        }


        public async Task<PatientVital> GetVitalsByVisitId(Guid VisitId)
        {
            
            var responseObj = await _uowPatientVital.Repository.GetALL(x => x.PatientVisitId == VisitId)
                .Select(x => new PatientVital
                {
                    Pulse = x.Pulse,
                    Temprature = x.Temprature,
                    Height = x.Height,
                    Weight = x.Weight,
                    Waist = x.Waist,
                    Hip = x.Hip,
                    RatioHipToWaist = x.RatioHipToWaist,
                    ResperatoryRate = x.ResperatoryRate,
                    CreatedOn = x.CreatedOn,
                }).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();
            return responseObj!;
        }

        public async Task<ViewHfDepartmentSectionFloorRoomDto> GetHfDepartmentSectionByHealthFacilityId(int HealthFacilityId, int DepartmentId, int SectionId)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatientVital.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPGetHfDepartmentSectionByHealthFacilityId", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", HealthFacilityId);

                    if (!AppCommonMethod.IsNullorZeroInt((DepartmentId)))
                        sqlComm.Parameters.AddWithValue("@DepartmentLookupId", DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt((SectionId)))
                        sqlComm.Parameters.AddWithValue("@SectionLookupId", SectionId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<ViewHfDepartmentSectionFloorRoomDto> lst = ds.Tables[0].ToList<ViewHfDepartmentSectionFloorRoomDto>();
                    ViewHfDepartmentSectionFloorRoomDto objHfDepartmentSection = lst.FirstOrDefault();

                    return objHfDepartmentSection;
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
        }

        #endregion

        #region Helper Methods

        private void FillEntity(DbModel.PatientVital obj)
        {
            if (obj.PatientVitalId == Guid.Empty)
            {
                obj.PatientVitalId = Guid.NewGuid();
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

        private void FillEntityCheckIfChange(DbModel.PatientVital prevObj, DbModel.PatientVital newObj)
        {
            if (!AppCommonMethod.IsNullObject(prevObj))
            {
                if (prevObj.Bpsystolic != newObj.Bpsystolic)
                    newObj.IsChangeBpsystolic = true;

                if (prevObj.BpdiaSystolic != newObj.BpdiaSystolic)
                    newObj.IsChangeBpdiaSystolic = true;

                if (prevObj.Height != newObj.Height)
                    newObj.IsChangeHeight = true;

                if (prevObj.Weight != newObj.Weight)
                    newObj.IsChangeWeight = true;

                if (prevObj.Pulse != newObj.Pulse)
                    newObj.IsChangePulse = true;

                if (prevObj.Temprature != newObj.Temprature)
                    newObj.IsChangeTemprature = true;

                if (prevObj.ResperatoryRate != newObj.ResperatoryRate)
                    newObj.IsChangeResperatoryRate = true;

                if (prevObj.Bmi != newObj.Bmi)
                    newObj.IsChangeBmi = true;

            }
        }


        private void FillEntityDrugAddict(DbModel.PatientDrugAddiction obj)
        {
            if (obj.PatientDrugAddictionId == Guid.Empty)
            {
                obj.PatientDrugAddictionId = Guid.NewGuid();
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


        private void FillEntityCoMorbid(DbModel.PatientCoMorbid obj)
        {
            if (obj.PatientCoMorbidId == Guid.Empty)
            {
                obj.PatientCoMorbidId = Guid.NewGuid();
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
        private void FillEntityDelete(DbModel.PatientVital obj)
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
