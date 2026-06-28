using AppCommonMethods;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.PaginationDto;
using AuthDAL.Models.Dto.EventDto;
using AutoMapper;
using JWTAuthentication;
using AuthDAL.Repositories.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonExceptionHandler;
using CommonMessages;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using CommonDTOs.Enums;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Text.Json;
using AuthDAL.Models.Dto.EventHealthFacilityDto;

namespace AuthBAL
{
    public class EventService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<Event> _uowEvent;

        #endregion

        #region Constructor

        public EventService(TokenService tokenService, UnitOfWork<Event> uowEvent, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowEvent = uowEvent;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditEventDto> CreateOrEdit(CreateOrEditEventDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.EventId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditEventDto> Create(CreateOrEditEventDto input)
        {
            var _uowEventHealthFacility = new UnitOfWork<EventHealthFacility>(_uowEvent.GetDbContext());
            var _uowEventUser = new UnitOfWork<EventUser>(_uowEvent.GetDbContext());
            var _uowUser = new UnitOfWork<User>(_uowEvent.GetDbContext());

            var eventObj = _mapper.Map<Event>(input);
            FillEntity(eventObj);
            
            Event responseObj = await _uowEvent.Repository.Insert(eventObj);
            //await _uowEvent.CommitAsync();

            // Add Details
            foreach (var item in input.EventHealthFacilities)
            {
                item.StartDateTime = input.StartDateTime;
                item.EndDateTime = input.EndDateTime;

                var isExist = await _uowEventHealthFacility.Repository.GetALL(x => x.HealthFacilityId == item.HealthFacilityId && 
                    ((item.StartDateTime >= x.StartDateTime && item.StartDateTime <= x.EndDateTime) || 
                    (item.EndDateTime >= x.StartDateTime && item.EndDateTime <= x.EndDateTime)) && 
                    x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                    .AnyAsync();

                if (isExist)
                    throw new UserFriendlyException(CommonMessageConstant.CannotCreateEvent);

                var eventHfObj = _mapper.Map<EventHealthFacility>(item);
                FillEntityEventHf(eventHfObj);

                eventHfObj.EventId = responseObj.EventId;
                EventHealthFacility eventHfResObj = await _uowEventHealthFacility.Repository.Insert(eventHfObj);
            }
            //await _uowEventHealthFacility.CommitAsync();
            // End Add Details

            // Add Users
            // filter Facilities
            //var facilityIds = input.EventHealthFacilities.Select(x => x.HealthFacilityId);

            //// Get Users against Facilities
            //var userList = _uowUser.Repository.GetALL(x => facilityIds.Contains(x.HealthFacilityId))
            //    .ToList();
            // Need to Improve
            foreach (var item in input.EventHealthFacilities)
            {
                //// Get Users against Facilities
                var userList = _uowUser.Repository.GetALL(x => x.HealthFacilityId == item.HealthFacilityId)
                    .Select(x => new
                    {
                        UserId = x.UserId,
                        HealthFacilityId = x.HealthFacilityId,
                        DepartmentId = x.DepartmentId,
                        SectionId = x.SectionId,
                    })
                .ToList();

                foreach (var user in userList)
                {
                    var eventUserObj = new EventUser();
                    FillEntityEventUser(eventUserObj);

                    eventUserObj.EventId = responseObj.EventId;
                    eventUserObj.HealthFacilityId = user.HealthFacilityId;
                    eventUserObj.DepartmentLookupId = user.DepartmentId;
                    eventUserObj.SectionLookupId = user.SectionId;
                    eventUserObj.UserId = user.UserId;
                    eventUserObj.StartDateTime = input.StartDateTime;
                    eventUserObj.EndDateTime = input.EndDateTime;
                    EventUser eventUserResObj = await _uowEventUser.Repository.Insert(eventUserObj);
                }
                
            }
            await _uowEvent.CommitAsync();
            // End Add Users

            return _mapper.Map<CreateOrEditEventDto>(input);
        }

        private async Task<CreateOrEditEventDto> Update(CreateOrEditEventDto input)
        {
            var dbObj = await _uowEvent.Repository.GetById(input.EventId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowEvent.Repository.Update(obj!);
            await _uowEvent.CommitAsync();

            return _mapper.Map<CreateOrEditEventDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowEvent.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowEvent.Repository.Update(dbObj!);
            await _uowEvent.CommitAsync();
            return true;
        }

        public async Task<bool> DeleteEventFacility(DeleteEventHealthFacilityDto input)
        {
            var _uowEventVisit = new UnitOfWork<EventVisit>(_uowEvent.GetDbContext());
            var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowEvent.GetDbContext());
            var _uowEventHealthFacility = new UnitOfWork<EventHealthFacility>(_uowEvent.GetDbContext());

            var dbObj = await _uowEventHealthFacility.Repository.GetALL(x => x.EventId == input.EventId && x.HealthFacilityId == input.HealthFacilityId).FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var visitExist = await _uowEventVisit.Repository.GetALL(x => x.HealthFacilityId == input.HealthFacilityId && x.EventId == dbObj!.EventId)
                .AnyAsync();

            if(visitExist)
                throw new UserFriendlyException(CommonMessageConstant.CannotDeleteEvent);

            FillEntityEventHfDelete(dbObj!);

            _uowEventHealthFacility.Repository.Update(dbObj!);
            await _uowEventHealthFacility.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewEventDto>> GetAll(Expression<Func<Event, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>
            ? orderBy = null,
            string includeProperties = "")
        {
            List<Event> responseObj = await _uowEvent.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewEventDto>>(responseObj);
        }


        public async Task<ViewPagerDto<SPResponseEventListDto>> GetAllWithPagination(FilterEventDto filter)
        {
            List<SPResponseEventListDto> lst = new List<SPResponseEventListDto>();
            var responseObject = new ViewPagerDto<SPResponseEventListDto>();

            var conn = _uowEvent.GetDbContext().Database.GetDbConnection();
            try
            {
                //if (!AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && !string.IsNullOrEmpty(filter.SearchString))
                //    filter.StartDate = filter.StartDate!.Value.AddDays(-30);

                //var _uowUser = new UnitOfWork<User>(_uowPatientLabTest.GetDbContext());
                //var dbUser = await _uowUser.Repository.GetALL(x => x.UserId == _tokenService.GetUserId()).FirstOrDefaultAsync();
                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand("[dbo].[SPEventList]", (SqlConnection)conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                //sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                //sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());

                //sqlComm.Parameters.AddWithValue("@startdate", startDate);
                //sqlComm.Parameters.AddWithValue("@enddate", endDate);

                //sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

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


                //if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                //    sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                //if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                //    sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                if (!AppCommonMethod.IsNullorZeroInt(filter.PageNumber))
                    sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);

                if (!AppCommonMethod.IsNullorZeroInt(filter.PageSize))
                    sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                await Task.Run(() => da.Fill(ds));
                var count = ds.Tables[0].ToList<SPResponseEventListTotalCountDto>();
                lst = ds.Tables[1].ToList<SPResponseEventListDto>();


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

        public async Task<ViewEventDto> GetById(Guid input)
        {
            Event? responseObj = await _uowEvent.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewEventDto>(responseObj);
        }


        #endregion

        #region Helper Methods

        private void FillEntity(Event obj)
        {
            if (obj.EventId == Guid.Empty)
            {
                obj.EventId = Guid.NewGuid();
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
        private void FillEntityDelete(Event obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        private void FillEntityEventHf(EventHealthFacility obj)
        {
            if (obj.EventId == Guid.Empty)
            {
                obj.EventHealthFacilityId = Guid.NewGuid();
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
        private void FillEntityEventHfDelete(EventHealthFacility obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        private void FillEntityEventUser(EventUser obj)
        {
            if (obj.EventId == Guid.Empty)
            {
                obj.EventUsersId = Guid.NewGuid();
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
        private void FillEntityEventUserDelete(EventUser obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion
    }
}
