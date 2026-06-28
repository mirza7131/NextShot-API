using AutoMapper;
using FileHandler;
using AppCommonMethods;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.NursingEventDto;
using HMIS.Patient.Domain.Repositories.UOW;
using JWTAuthentication;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using HMIS.Patient.Domain.Models.DTO.PatientDto;
using CommonDTOs.TBScreeningDTO;
using System.Collections;

namespace HMIS.Patient.Service
{
    public class NursingEventsService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<NursingEvent> _uowNursingEvents;
        #endregion

        #region Constructor

        public NursingEventsService(TokenService tokenService, IMapper mapper, UnitOfWork<NursingEvent> uowNursingEvents)
    {
        _tokenService = tokenService;
            _uowNursingEvents = uowNursingEvents;
            _mapper = mapper;

    }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditNursingEventDto> CreateOrEdit(CreateOrEditNursingEventDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.NursingEventsId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditNursingEventDto> Create(CreateOrEditNursingEventDto input)
        {
            var obj = _mapper.Map<NursingEvent>(input);
            FillEntity(obj);
            NursingEvent responseObj = await _uowNursingEvents.Repository.Insert(obj);
            await _uowNursingEvents.Save();
            return _mapper.Map<CreateOrEditNursingEventDto>(responseObj);

        }

        private async Task<CreateOrEditNursingEventDto> Update(CreateOrEditNursingEventDto input)
        {
            var dbObj = await _uowNursingEvents.Repository.GetById(input.NursingEventsId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);
            _uowNursingEvents.Repository.Update(obj!);
            await _uowNursingEvents.CommitAsync();
            return _mapper.Map<CreateOrEditNursingEventDto>(obj);
        }

        public async Task<CreateOrEditNursingEventDto> CreateEvent(CreateOrEditNursingEventDto input)
        {
            var canCreate = true;
            var dbUser = TokenService.GetUserLoggedInfo();
            NursingEvent responseObj = new NursingEvent();

            input.HealthFacilityId = dbUser!.HealthFacilityId;
            input.DepartmentLookupId = (!AppCommonMethod.IsNullorZeroInt(dbUser.DepartmentId)) ? dbUser.DepartmentId : null;
            input.SectionLookupId = (!AppCommonMethod.IsNullorZeroInt(dbUser.SectionId)) ? dbUser.SectionId : null;

            var obj = _mapper.Map<NursingEvent>(input);
            FillEntity(obj);

            var dbObj = await _uowNursingEvents.Repository.GetALL(x => x.Events == input.Events
            && x.PatientId == input.PatientId
            && x.PatientDiagnoseId == input.PatientDiagnoseId 
            && x.PatientVisitId == input.PatientVisitId).FirstOrDefaultAsync();

            if(!AppCommonMethod.IsNullObject(dbObj))
            {
                TimeSpan ts = obj.CreatedOn - dbObj!.CreatedOn;
                var minutes = ts.TotalMinutes;
                if (minutes < 10)
                {
                    canCreate = false;
                }
            }

            if (AppCommonMethod.IsNullObject(dbObj) || canCreate)
            {
                responseObj = await _uowNursingEvents.Repository.Insert(obj);
                await _uowNursingEvents.Save();
            }
            return _mapper.Map<CreateOrEditNursingEventDto>(responseObj);

        }

        public async Task<CreateOrEditNursingEventDto> UpdateEventStatus(Guid input)
        {
            var dbObj = await _uowNursingEvents.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            dbObj!.AcknowledgedBy = _tokenService.GetUserId();
            dbObj.AcknowledgedOn = DateTime.Now;

            _uowNursingEvents.Repository.Update(dbObj!);
            await _uowNursingEvents.CommitAsync();

            return _mapper.Map<CreateOrEditNursingEventDto>(dbObj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowNursingEvents.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowNursingEvents.Repository.Update(dbObj!);
            await _uowNursingEvents.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations
        public async Task<ViewPagerDto<SPGetPatientNursingEventsDto>> GetNursingEventsListWithDetail(FilterNursingEventDto filter)
        {

            var loginUser = TokenService.GetUserLoggedInfo();
            var responseObject = new ViewPagerDto<SPGetPatientNursingEventsDto>();
            List<SPGetPatientNursingEventsDto> lst = new List<SPGetPatientNursingEventsDto>();

            using (var db = new HmisAuthContext())
            {

                var conn = _uowNursingEvents.GetDbContext().Database.GetDbConnection();
                try
                {
                    
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[dbo].[SPGetPatientNursingEvents]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    
                    //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                    if (!string.IsNullOrEmpty(filter.TokenNo))
                        sqlComm.Parameters.AddWithValue("@TokenNo", filter.TokenNo);

                    if (!AppCommonMethod.IsNullOrEmptyGuid (filter.PatientId))
                        sqlComm.Parameters.AddWithValue("@PatientId", filter.PatientId);

                    if (!AppCommonMethod.IsNullOrEmptyGuid(loginUser!.UserId))
                        sqlComm.Parameters.AddWithValue("@UserId", loginUser!.UserId);

                    if (!AppCommonMethod.IsNullorZeroInt(filter.ListType))
                        sqlComm.Parameters.AddWithValue("@ListType", filter.ListType);

                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    var count = ds.Tables[0].ToList<SPGetPatientNursingEventsCountDto>();
                    lst = ds.Tables[1].ToList<SPGetPatientNursingEventsDto>();


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

        }
        #endregion

        #region Helper Methods

        private void FillEntity(NursingEvent obj)
        {
            if (obj.NursingEventsId == Guid.Empty)
            {
                obj.NursingEventsId = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.IsActive = true;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }
        private void FillEntityDelete(NursingEvent obj)
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
