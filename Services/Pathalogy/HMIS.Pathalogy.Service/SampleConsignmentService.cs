
using AppCommonMethods;
using AutoMapper;
using CommonDTOs;
using CommonDTOs.Enums;
using CommonDTOs.TBScreeningDTO;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Pathalogy.Domain.Models.DbModels;
using HMIS.Pathalogy.Domain.Models.DTO.CreateOrEditBatchSampleDto;
using HMIS.Pathalogy.Domain.Models.DTO.PaginationDto;
using HMIS.Pathalogy.Domain.Models.DTO.SampleBatchListDto;
using HMIS.Pathalogy.Domain.Models.DTO.SampleCollectedConsignmentListDto;
using HMIS.Pathalogy.Domain.Models.DTO.SampleConsignmentDetailDto;
using HMIS.Pathalogy.Domain.Models.DTO.SampleConsignmentDto;
using HMIS.Pathalogy.Domain.Models.DTO.ViewPatientLabTestListDto;
using HMIS.Pathalogy.Domain.Repositories._UOW;
using JWTAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Profile = HMIS.Pathalogy.Domain.Models.DbModels.Profile;

namespace HMIS.Pathalogy.Service
{
    public class SampleConsignmentService<TEntity> where TEntity : class
    {

        #region Class Fields & Propertities
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<SampleConsignment> _uowSampleConsignment;
        private UnitOfWork<SampleConsignmentDetail> _uowSampleConsignmentDetail;
        #endregion

        #region Constructor

        public SampleConsignmentService(
            TokenService tokenService,
            UnitOfWork<SampleConsignment> uowSampleConsignment,
            UnitOfWork<SampleConsignmentDetail> uowSampleConsignmentDetail,
            IMapper mapper
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowSampleConsignment = uowSampleConsignment;
            _uowSampleConsignmentDetail = uowSampleConsignmentDetail;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditSampleConsignmentDto> CreateOrEdit(CreateOrEditSampleConsignmentDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.SampleConsignmentId))
                return await Create(input);
            else
                return await Update(input);
        }
        public async Task<CreateOrEditBatchSampleDto> CreateOrEditBatch(CreateOrEditBatchSampleDto input)
        {
            //if (AppCommonMethod.IsNullOrEmptyGuid(input.SampleConsignmentId))
            return await CreateBatch(input);
            //else
            //    return await UpdateBatch(input);
        }
        private async Task<CreateOrEditSampleConsignmentDto> Create(CreateOrEditSampleConsignmentDto input)
        {
            var obj = _mapper.Map<SampleConsignment>(input);
            obj.FromHealthFacilityId = input.FromHealthFacilityId != 0 ? input.FromHealthFacilityId : TokenService.GetUserHfId();
            obj.BatchNo = UpdateBacthNo(input.FromHealthFacilityId);
            obj.IsActive = true;
            obj.Status = (byte?)SampleConsignmentStatus.Pending;
            FillEntity(obj);
            SampleConsignment responseObj = await _uowSampleConsignment.Repository.Insert(obj);
            await _uowSampleConsignment.CommitAsync();


            //Update consignemtn detail in patient lab test

            foreach (var item in responseObj.SampleConsignmentDetails)
            {
                var _uowPatientLabTest = new UnitOfWork<PatientLabTest>(_uowSampleConsignment.GetDbContext());

                var tempPatientLabTest = await _uowPatientLabTest.Repository.GetALL(x => x.PatientLabTestId == item.PatientLabTestId).FirstOrDefaultAsync();

                tempPatientLabTest!.SampleConsignmentDetailId = item.SampleConsignmentDetailId;
                _uowPatientLabTest.Repository.Update(tempPatientLabTest!);
                await _uowPatientLabTest.CommitAsync();
            }

            return _mapper.Map<CreateOrEditSampleConsignmentDto>(responseObj);
        }

        private async Task<CreateOrEditSampleConsignmentDto> Update(CreateOrEditSampleConsignmentDto input)
        {
            var dbObj = await _uowSampleConsignment.Repository.GetById(input.SampleConsignmentId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);


            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            var sampleConsignmentDetails = obj!.SampleConsignmentDetails;

            obj!.SampleConsignmentDetails.Clear();

            _uowSampleConsignment.Repository.Update(obj!);
            await _uowSampleConsignment.CommitAsync();


            foreach (var item in input.SampleConsignmentDetails)
            {
                await UpdateConsignmentDetail(item);
            }

            dbObj = await _uowSampleConsignment.Repository.GetById(input.SampleConsignmentId!);

            return _mapper.Map<CreateOrEditSampleConsignmentDto>(dbObj);
        }
        private async Task<CreateOrEditBatchSampleDto> CreateBatch(CreateOrEditBatchSampleDto input)
        {
            //foreach (var item in input.SampleBatchDetails)
            //{
            //    var _uowPatientLabTest = new UnitOfWork<PatientLabTest>(_uowSampleConsignment.GetDbContext());

            //    var tempPatientLabTest = await _uowPatientLabTest.Repository.GetALL(x => x.PatientLabTestId == item.PatientLabTestId).FirstOrDefaultAsync();
            //    //tempPatientLabTest!.SampleConsignmentDetailId = item.SampleConsignmentDetailId;
            //    tempPatientLabTest.BatchNumber = input.BatchNumber;
            //    tempPatientLabTest.BatchCreatedOn = DateTime.Now;
            //    tempPatientLabTest.BatchCreatedBy = _tokenService.GetUserId();
            //    _uowPatientLabTest.Repository.Update(tempPatientLabTest!);
            //    await _uowPatientLabTest.CommitAsync();
            //}

            //return _mapper.Map<CreateOrEditSampleConsignmentDto>(responseObj);

            
            using (var trans = _uowSampleConsignment.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    var batchNumber = await GenerateUniqueBatchNumber(input.BatchNumber);


                    foreach (var item in input.SampleBatchDetails)
                    {
                        var _uowPatientLabTest = new UnitOfWork<PatientLabTest>(_uowSampleConsignment.GetDbContext());
                        var tempPatientLabTest = await _uowPatientLabTest.Repository.GetALL(x => x.PatientLabTestId == item.PatientLabTestId).FirstOrDefaultAsync();
                        tempPatientLabTest.BatchNumber = batchNumber;
                        tempPatientLabTest.BatchCreatedOn = DateTime.Now;
                        tempPatientLabTest.BatchCreatedBy = _tokenService.GetUserId();

                        _uowPatientLabTest.Repository.Update(tempPatientLabTest);
                        await _uowPatientLabTest.CommitAsync();
                    }

                    input.BatchNumber = batchNumber;
                    await trans.CommitAsync();
                    return input;
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }
        private async Task<string> GenerateUniqueBatchNumber(string tempBatchNumber)
        {
            string batchNumber;
            var random = new Random();
            var _db = new HmisAuthContext();
            var sequenceNumber = await _db.PatientLabTests.Where(x => x.BatchNumber != null).GroupBy(x => x.BatchNumber).CountAsync() + 1;

            // Generate 9 random digits
            //var randomDigits = random.Next(100000000, 999999999).ToString();
            long seed = Math.Abs((long)(DateTime.Now.Ticks % long.MaxValue));

            // Form the batch number
            batchNumber = $"HBV-{seed}-{sequenceNumber}";

            // Check uniqueness
            while (await _db.PatientLabTests.AnyAsync(x => x.BatchNumber == batchNumber))
            {
                sequenceNumber++;
                batchNumber = $"HBV-{seed}-{sequenceNumber}";
            }

            if (tempBatchNumber.Contains("HCV"))
            {
                batchNumber = batchNumber.Replace("HBV", "HCV");
            }

            return batchNumber;
        }

        //private async Task<CreateOrEditSampleConsignmentDto> UpdateBatch(CreateOrEditSampleConsignmentDto input)
        //{
        //    var dbObj = await _uowSampleConsignment.Repository.GetById(input.SampleConsignmentId!);

        //    if (AppCommonMethod.IsNullObject(dbObj))
        //        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);


        //    var obj = _mapper.Map(input, dbObj);
        //    FillEntity(obj!);

        //    var sampleConsignmentDetails = obj!.SampleConsignmentDetails;

        //    obj!.SampleConsignmentDetails.Clear();

        //    _uowSampleConsignment.Repository.Update(obj!);
        //    await _uowSampleConsignment.CommitAsync();


        //    foreach (var item in input.SampleConsignmentDetails)
        //    {
        //        await UpdateConsignmentDetail(item);
        //    }

        //    dbObj = await _uowSampleConsignment.Repository.GetById(input.SampleConsignmentId!);

        //return _mapper.Map<CreateOrEditSampleConsignmentDto>(dbObj);
        //}
        private async Task<CreateOrEditSampleConsignmentDetailDto> UpdateConsignmentDetail(CreateOrEditSampleConsignmentDetailDto input)
        {
            var dbObj = await _uowSampleConsignmentDetail.Repository.GetById(input.SampleConsignmentDetailId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntitySampleConsignmentDetail(obj!);

            _uowSampleConsignmentDetail.Repository.Update(obj!);
            await _uowSampleConsignmentDetail.CommitAsync();

            return _mapper.Map<CreateOrEditSampleConsignmentDetailDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowSampleConsignment.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);
            dbObj!.Status = 4;
            FillEntityDelete(dbObj!);

            _uowSampleConsignment.Repository.Update(dbObj!);
            await _uowSampleConsignment.CommitAsync();

            // remove sample consignment detail id from patient lab test also 
            var sampleConsignmentDetails = await _uowSampleConsignmentDetail.Repository.GetALL(x => x.SampleConsignmentId == (Guid)Id).ToListAsync();

            foreach (var item in sampleConsignmentDetails)
            {


                var _uowPatientLabTest = new UnitOfWork<PatientLabTest>(_uowSampleConsignment.GetDbContext());

                var tempPatientLabTest = await _uowPatientLabTest.Repository.GetALL(x => x.PatientLabTestId == item.PatientLabTestId).FirstOrDefaultAsync();

                tempPatientLabTest!.SampleConsignmentDetailId = null;
                tempPatientLabTest.UpdatedBy = _tokenService.GetUserId();
                tempPatientLabTest.UpdatedOn = DateTime.Now;
                _uowPatientLabTest.Repository.Update(tempPatientLabTest!);
                await _uowPatientLabTest.CommitAsync();

            }

            return true;
        }

        public async Task<CreateOrEditSampleConsignmentDto> UpdateConsignmentStatus(CreateOrEditSampleConsignmentDto input)
        {
            var dbObj = await _uowSampleConsignment.Repository.GetById(input.SampleConsignmentId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            dbObj!.Status = input.Status;

            FillEntity(dbObj!);

            _uowSampleConsignment.Repository.Update(dbObj!);
            await _uowSampleConsignment.CommitAsync();

            return _mapper.Map<CreateOrEditSampleConsignmentDto>(dbObj);
        }

        public async Task<bool> UpdateConsignmentDetailStatus(List<CreateOrEditSampleConsignmentDetailDto> input)
        {

            using (var trans = _uowSampleConsignmentDetail.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    foreach (var item in input)
                    {
                        var dbObj = await _uowSampleConsignmentDetail.Repository.GetById(item.SampleConsignmentDetailId!);

                        if (AppCommonMethod.IsNullObject(dbObj))
                            throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                        dbObj!.Status = item.Status;

                        FillEntitySampleConsignmentDetail(dbObj!);

                        _uowSampleConsignmentDetail.Repository.Update(dbObj!);
                        await _uowSampleConsignmentDetail.CommitAsync();
                    }
                    //await trans.CommitAsync();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
            return true;
        }

        #endregion

        #region Read Operations
        public async Task<ViewPagerDto<ViewSampleConsignmentWithDetailDto>> GetSampleConsignmentWithDetail(FilterSampleConsignmentDto filter)
        {

            var finalList = _uowSampleConsignment.GetDbContext().ViewSampleConsignmentLists

                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.SampleConsignmentCreatedOn!.Value >= filter.StartDate!.Value)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.SampleConsignmentCreatedOn!.Value < filter.EndDate!.Value)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.FromHealthFacilityId), x => x.FromHealthFacilityId == filter.FromHealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ToHealthFacilityId), x => x.ToHealthFacilityId == filter.ToHealthFacilityId)

                .WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByTitle, x => x.Title!.ToLower() == filter.SearchString)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByBatchNumber, x => x.BatchNo! == filter.SearchString)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByFromHealthFacility, x => x.FromHealthFacility! == filter.SearchString)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ConsignmentStatus), x => x.ConsignmentStatus == filter.ConsignmentStatus)

                //.WhereIf(!string.IsNullOrEmpty(filter.SearchString), 
                //x => x.Title!.ToLower().Contains(filter.SearchString) || 
                //x.FromHealthFacility!.ToLower().StartsWith(filter.SearchString) || 
                //x.BatchNo!.ToLower().StartsWith(filter.SearchString))

                .OrderByDescending(x => x.SampleConsignmentCreatedOn)
                .Select(x =>
                new ViewSampleConsignmentWithDetailDto
                {
                    SampleConsignmentId = x.SampleConsignmentId,
                    Title = x.Title,
                    ToHealthFacilityId = x.ToHealthFacilityId,
                    FromHealthFacilityId = x.FromHealthFacilityId,
                    FromHealthFacility = x.FromHealthFacility,
                    ToHealthFacility = x.ToHealthFacility,
                    ConsignmentStatusReason = x.ConsignmentStatusReason,
                    SampleConsignmentCreatedOn = x.SampleConsignmentCreatedOn,
                    ConsignmentStatus = x.ConsignmentStatus,
                    BatchNo = x.BatchNo

                });

            var pagedList = await PagedListDto<ViewSampleConsignmentWithDetailDto>.ToPagedListAsync(
                   finalList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewSampleConsignmentWithDetailDto>
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
        // For Pending Batch List
        public async Task<ViewPagerDto<ViewSampleBatchListDto>> GetAllWithPaginationForBatchList(FilterSampleBatchListDto filter)
        {
            if (!string.IsNullOrEmpty(filter.SearchString))
            {

                using (var db = new HmisAuthContext())
                {
                    var conn = _uowSampleConsignment.GetDbContext().Database.GetDbConnection();
                    try
                    {
                        DataSet ds = new DataSet();
                        SqlCommand sqlComm = new SqlCommand("SPGetPatientBatchNumber", (SqlConnection)conn);
                        sqlComm.CommandType = CommandType.StoredProcedure;
                        //sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                        //sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
                        ////sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

                        //if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        //    sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                        //if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        //    sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                        //if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        //    sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                        //if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        //    sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                        //if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        //    sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                        //if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        //    sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);


                        if (!AppCommonMethod.IsNullorZeroInt(filter.FilterBy))
                            sqlComm.Parameters.AddWithValue("@FilterBy", filter.FilterBy);

                        if (!AppCommonMethod.IsNullorZeroInt(filter.FilterBy))
                            sqlComm.Parameters.AddWithValue("@ListType", CommonStringConstant.PendingBatchList);

                        if (!AppCommonMethod.IsNullorZeroInt(filter.FilterBy))
                            sqlComm.Parameters.AddWithValue("@FilterString", filter.SearchString.Trim());

                        //if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        //    sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                        //if (!string.IsNullOrEmpty(filter.listType))
                        //    sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                        //if (!string.IsNullOrEmpty(filter.User))
                        //    sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));




                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = sqlComm;
                        await Task.Run(() => da.Fill(ds));
                        List<ViewSampleBatchListDto> lst = ds.Tables[0].ToList<ViewSampleBatchListDto>();

                        //var pagedList = await PagedListDto<ViewSampleBatchListDto>.ToPagedListAsync(
                        //           //lst.AsQueryable(),
                        //           //lst.ToList().AsQueryable(),
                        //           ds.Tables[0].ToList<ViewSampleBatchListDto>().AsQueryable(),
                        //           filter.PageNumber,
                        //           filter.PageSize
                        //           );

                        var responseObject = new ViewPagerDto<ViewSampleBatchListDto>
                        {
                            //TotalCount = pagedList.TotalCount,
                            //PageSize = pagedList.PageSize,
                            //CurrentPage = pagedList.CurrentPage,
                            //TotalPages = pagedList.TotalPages,
                            //HasNext = pagedList.HasNext,
                            //HasPrevious = pagedList.HasPrevious,
                            TotalCount = 1,
                            PageSize = 1,
                            CurrentPage = 1,
                            TotalPages = 1,
                            HasNext = false,
                            HasPrevious = false,
                            List = lst
                        };

                        return responseObject;

                        //return lst[0];

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
            else
            {
                var finalList = _uowSampleConsignment.GetDbContext().ViewSampleBatchLists.Where(x => x.BatchResultUploadedOn == null)

                //.WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.cnic!.ToLower().Contains(filter.SearchString) || x.FromHealthFacility!.ToLower().StartsWith(filter.SearchString) || x.BatchNo!.ToLower().StartsWith(filter.SearchString))

                .OrderByDescending(x => x.BatchCreatedOn)
                .Select(x =>
                new ViewSampleBatchListDto
                {
                    BatchCreatedOn = x.BatchCreatedOn,
                    BatchNumber = x.BatchNumber,
                    BatchCreatedBy = x.BatchCreatedBy,
                    //SamplesCount = x.SamplesCount,
                    BatchResultUploadedOn = x.BatchResultUploadedOn

                });


                var pagedList = await PagedListDto<ViewSampleBatchListDto>.ToPagedListAsync(
                       finalList,
                       filter.PageNumber,
                       filter.PageSize
                       );

                var responseObject = new ViewPagerDto<ViewSampleBatchListDto>
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
        }

        // For Completed Batch List
        public async Task<ViewPagerDto<ViewSampleBatchListDto>> GetAllWithPaginationForCompletedBatchList(FilterSampleBatchListDto filter)
        {
            if (!string.IsNullOrEmpty(filter.SearchString))
            {

                using (var db = new HmisAuthContext())
                {
                    var conn = _uowSampleConsignment.GetDbContext().Database.GetDbConnection();
                    try
                    {
                        DataSet ds = new DataSet();
                        SqlCommand sqlComm = new SqlCommand("SPGetPatientBatchNumber", (SqlConnection)conn);
                        sqlComm.CommandType = CommandType.StoredProcedure;
                        //sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
                        //sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());
                        ////sqlComm.Parameters.AddWithValue("@TakeRecords", 20);

                        //if (!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId))
                        //    sqlComm.Parameters.AddWithValue("@ProvinceId", filter.ProvinceId);

                        //if (!AppCommonMethod.IsNullorZeroInt(filter.DivisionId))
                        //    sqlComm.Parameters.AddWithValue("@DivisionId", filter.DivisionId);

                        //if (!AppCommonMethod.IsNullorZeroInt(filter.DistrictId))
                        //    sqlComm.Parameters.AddWithValue("@DistrictId", filter.DistrictId);


                        //if (!AppCommonMethod.IsNullorZeroInt(filter.TehsilId))
                        //    sqlComm.Parameters.AddWithValue("@TehsilId", filter.TehsilId);

                        //if (!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId))
                        //    sqlComm.Parameters.AddWithValue("@HealthFacilityId", filter.HealthFacilityId);

                        //if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                        //    sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);


                        if (!AppCommonMethod.IsNullorZeroInt(filter.FilterBy))
                            sqlComm.Parameters.AddWithValue("@FilterBy", filter.FilterBy);

                        if (!AppCommonMethod.IsNullorZeroInt(filter.FilterBy))
                            sqlComm.Parameters.AddWithValue("@ListType", CommonStringConstant.CompletedBatchList);

                        if (!AppCommonMethod.IsNullorZeroInt(filter.FilterBy))
                            sqlComm.Parameters.AddWithValue("@FilterString", filter.SearchString.Trim());

                        //if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                        //    sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                        //if (!string.IsNullOrEmpty(filter.listType))
                        //    sqlComm.Parameters.AddWithValue("@ListType", filter.listType);

                        //if (!string.IsNullOrEmpty(filter.User))
                        //    sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));




                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = sqlComm;
                        await Task.Run(() => da.Fill(ds));
                        List<ViewSampleBatchListDto> lst = ds.Tables[0].ToList<ViewSampleBatchListDto>();

                        //var pagedList = await PagedListDto<ViewSampleBatchListDto>.ToPagedListAsync(
                        //           //lst.AsQueryable(),
                        //           //lst.ToList().AsQueryable(),
                        //           ds.Tables[0].ToList<ViewSampleBatchListDto>().AsQueryable(),
                        //           filter.PageNumber,
                        //           filter.PageSize
                        //           );

                        var responseObject = new ViewPagerDto<ViewSampleBatchListDto>
                        {
                            //TotalCount = pagedList.TotalCount,
                            //PageSize = pagedList.PageSize,
                            //CurrentPage = pagedList.CurrentPage,
                            //TotalPages = pagedList.TotalPages,
                            //HasNext = pagedList.HasNext,
                            //HasPrevious = pagedList.HasPrevious,
                            TotalCount = 1,
                            PageSize = 1,
                            CurrentPage = 1,
                            TotalPages = 1,
                            HasNext = false,
                            HasPrevious = false,
                            List = lst
                        };

                        return responseObject;

                        //return lst[0];

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
            else
            {


                var finalList = _uowSampleConsignment.GetDbContext().ViewSampleBatchLists
                    .Where(x => x.BatchResultUploadedOn != null)

                    .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.BatchCreatedOn!.Value >= filter.StartDate!.Value)
                    .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.BatchCreatedOn!.Value < filter.EndDate!.Value)
                    //.WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.BatchNumber!.ToLower().Contains(filter.SearchString))


                    .WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByBatchNumber, x => x.BatchNumber.ToLower() == filter.SearchString)

                    //.WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByCNIC, x => x.Cnic.ToLower() == filter.SearchString)
                    //.WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByMrNo, x => x.PatientMobileNo.ToLower() == filter.SearchString)
                    //.WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByBarcode, x => x.MrNo.ToLower() == filter.SearchString)



                    .OrderByDescending(x => x.BatchResultUploadedOn)
                    .Select(x =>
                    new ViewSampleBatchListDto
                    {
                        BatchCreatedOn = x.BatchCreatedOn,
                        BatchNumber = x.BatchNumber,
                        BatchCreatedBy = x.BatchCreatedBy,
                        BatchResultUploadedOn = x.BatchResultUploadedOn

                    });


                var pagedList = await PagedListDto<ViewSampleBatchListDto>.ToPagedListAsync(
                       finalList,
                       filter.PageNumber,
                       filter.PageSize
                       );

                var responseObject = new ViewPagerDto<ViewSampleBatchListDto>
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
        }
        public async Task<ViewSampleConsignmentDto> GetById(Guid input)
        {
            SampleConsignment? responseObj = await _uowSampleConsignment.GetDbContext().SampleConsignments.Where(x => x.SampleConsignmentId == input).Include(x => x.SampleConsignmentDetails).FirstOrDefaultAsync(); // await _uowSampleConsignment.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewSampleConsignmentDto>(responseObj);
        }

        public async Task<List<ViewSampleConsignmentWithDetailDto>> GetConsignmentWithConsignmentDetailByConsignmentId(FilterSampleConsignmentDto filter)
        {
            var list = await _uowSampleConsignment.GetDbContext().ViewSampleConsignmentWithDetailLists
                .Where(x => x.SampleConsignmentId == filter.SampleConsignmentId)
                .Select(x =>
               new ViewSampleConsignmentWithDetailDto
               {
                   SampleConsignmentId = x.SampleConsignmentId,
                   Title = x.Title,
                   ToHealthFacilityId = x.ToHealthFacilityId,
                   FromHealthFacilityId = x.FromHealthFacilityId,
                   FromHealthFacility = x.FromHealthFacility,
                   ToHealthFacility = x.ToHealthFacility,
                   ConsignmentStatusReason = x.ConsignmentStatusReason,
                   SampleConsignmentCreatedOn = x.SampleConsignmentCreatedOn,
                   ConsignmentStatus = x.ConsignmentStatus,
                   BatchNo = x.BatchNo,
                   LabTestId = x.LabTestId,
                   PatientLabTestId = x.PatientLabTestId,
                   LabTestName = x.LabTestName,
                   BarcodeNo = x.BarcodeNo,
                   SampleConsignmentDetailId = x.SampleConsignmentDetailId,
                   PatientName = x.PatientName,
                   ConsignmentDetailStatus = x.ConsignmentDetailStatus,
                   ConsignmentDetailStatusReason = x.ConsignmentDetailStatusReason,
                   SampleConsignmentCreatedbyId = x.SampleConsignmentCreatedbyId,
                   SampleConsignmentCreatedBy = x.SampleConsignmentCreatedBy,
                   SampleConsignmentDetailCreatedbyId = x.SampleConsignmentDetailCreatedbyId,
                   SampleConsignmentDetailCreatedBy = x.SampleConsignmentDetailCreatedBy,
                   SampleConsignemntCreated = x.SampleConsignemntCreated,
                   SampleConsignemntDetailCreated = x.SampleConsignemntDetailCreated,
                   PreGeneratedBarcodeNo = x.PreGeneratedBarcodeNo


               }).ToListAsync();

            return list;
        }

        public async Task<ViewPagerDto<ViewSampleCollectedConsignmentLilst>> GetAllSampleCollectedConsignmentList(FilterSampleConsignmentDto filter)
        {
            var loginUser = TokenService.GetUserLoggedInfo();
            var IsRadiologist = loginUser!.UserRoleList.SingleOrDefault(x => x.ShortName == CommonStringConstant.Radiologist) ?? null;

            var _uowProfile = new UnitOfWork<Profile>(_uowSampleConsignment.GetDbContext());
            var ReasonsList = new List<Profile>();

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            if (filter.LabTestStage == CommonStringConstant.SampleRejected)
                ReasonsList = _uowProfile.Repository.GetALL()
                    .Where(x => x.ProfileType.ShortName == CommonStringConstant.LabSampleRejectedReasons)
                    .ToList();

            var list = _uowSampleConsignment.GetDbContext().ViewSampleCollectedConsignmentLists
                .Where(x => x.LabDepartmentShortName == CommonStringConstant.InternalLabTest)

                .WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByCNIC, x => x.Cnic.ToLower() == filter.SearchString)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByMobileNo, x => x.PatientMobileNo.ToLower() == filter.SearchString)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByMrNo, x => x.MrNo.ToLower() == filter.SearchString)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByBarcode, x => x.BarcodeNo.ToLower() == filter.SearchString || x.PreGeneratedBarcodeNo.ToLower() == filter.SearchString)


                //.WhereIf(!string.IsNullOrEmpty(filter.SearchString),
                //x => x.Cnic.ToLower().StartsWith(filter.SearchString) ||
                //x.MrNo!.ToLower().StartsWith(filter.SearchString) ||
                //x.PatientMobileNo!.ToLower().StartsWith(filter.SearchString) ||
                //x.BarcodeNo!.ToLower().StartsWith(filter.SearchString) ||
                //x.PreGeneratedBarcodeNo!.ToLower().StartsWith(filter.SearchString))

                //.WhereIf(!AppCommonMethod.IsNullBool(filter.Rider) && filter.Rider == true, x => x.RiderUserId != null)

                //.WhereIf(!AppCommonMethod.IsNullBool(filter.Rider) && filter.Rider == false, x => x.RiderUserId == null)

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.LabTestStage) && filter.LabTestStage == CommonStringConstant.PendingCollection, x => x.IsSampleCollected != true && x.IsReportGenerated != true && x.IsSampleRejected != true)

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.LabTestStage) && filter.LabTestStage == CommonStringConstant.SampleCollected, x => x.IsSampleCollected == true && x.IsReportGenerated != true && x.IsSampleRejected != true && x.LabTestName != CommonStringConstant.CXR)

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.LabTestStage) && filter.LabTestStage == CommonStringConstant.ReportGenerated, x => x.IsSampleCollected == true && x.IsReportGenerated == true && x.IsSampleRejected != true)

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.LabTestStage) && filter.LabTestStage == CommonStringConstant.SampleRejected, x => x.IsSampleRejected == true)



                .WhereIf(!AppCommonMethod.IsNullObject(IsRadiologist), x => x.LabTypeShortName == CommonStringConstant.LabTypeXray)

                //.WhereIf(!AppCommonMethod.IsNullBool(filter.IsExternalSource), x => x.IsAdvisedExternally == filter.IsExternalSource)

                .WhereIf(!AppCommonMethod.IsNullBool(filter.IsArchive), x => x.IsArchived == filter.IsArchive)

                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.ProvinceId == filter.ProvinceId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ToHealthFacilityId), x => x.ConsignmentToHealthFacilityId == filter.ToHealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.AdvisedOn!.Value.Date >= filter.StartDate!.Value.Date)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.AdvisedOn!.Value.Date <= filter.EndDate!.Value.Date)
                .WhereIf(!string.IsNullOrEmpty(filter.User) && !AppCommonMethod.IsNullorZeroInt(filter.LabTestStage) && filter.LabTestStage == CommonStringConstant.ReportInDashboard, x => x.IsSampleCollected == true && x.IsReportGenerated == true && user.Contains(x.ReportGeneratedById!.ToString()))
                .WhereIf(!string.IsNullOrEmpty(filter.User) && !AppCommonMethod.IsNullorZeroInt(filter.LabTestStage) && filter.LabTestStage == CommonStringConstant.SampleCollectedInDashboard, x => x.IsSampleCollected == true && user.Contains(x.SampleCollectedById!.ToString()))
                .OrderByDescending(x => x.AdvisedOn);

            IQueryable<ViewSampleCollectedConsignmentLilst> IQueryableList = list.Select(x =>
               new ViewSampleCollectedConsignmentLilst
               {
                   PatientLabTestId = x.PatientLabTestId,
                   PatientId = x.PatientId,
                   PatientVisitId = x.PatientVisitId,

                   StageName = x.StageName,
                   StatusCode = x.StatusCode,
                   StatusName = x.StatusName,

                   LabTestId = x.LabTestId,
                   BarcodeNo = x.BarcodeNo,
                   LabDepartmentName = x.LabDepartmentName,
                   Cnic = x.Cnic,
                   MrNo = x.MrNo,
                   PatientMobileNo = x.PatientMobileNo,

                   PatientName = x.PatientName,
                   LabTestName = x.LabTestName,
                   AdvisedBy = x.AdvisedBy,
                   AdvisedOn = x.AdvisedOn,
                   TestPrice = x.TestPrice,
                   SampleType = x.SampleType,

                   IsFromCallCenter = x.IsFromCallCenter,
                   IsSampleRequired = x.IsSampleRequired,

                   IsSampleCollected = x.IsSampleCollected,
                   SampleCollectedBy = x.SampleCollectedBy,
                   SampleCollectedOn = x.SampleCollectedOn,

                   IsReportGenerated = x.IsReportGenerated,
                   ReportGeneratedBy = x.ReportGeneratedBy,
                   ReportGeneratedOn = x.ReportGeneratedOn,

                   IsSampleRejected = x.IsSampleRejected,
                   SampleRejectedBy = x.SampleRejectedBy,
                   SampleRejectedOn = x.SampleRejectedOn,

                   IsAdvisedExternally = x.IsAdvisedExternally,

                   SourceDoctorName = x.SourceDoctorName,
                   HealthFacilityName = x.HealthFacilityName,

                   SampleRejectedReason = x.SampleRejectedReason!,

                   LabType = x.LabType,
                   LabTypeShortName = x.LabTypeShortName,
                   ReportLink = x.ReportLink,
                   ResultImageLink = x.ResultImageLink,
                   ConsignmentToHealthFacilityId = x.ConsignmentToHealthFacilityId

               });

            var pagedList = await PagedListDto<ViewSampleCollectedConsignmentLilst>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            foreach (var item in pagedList)
            {
                if (!string.IsNullOrEmpty(item.SampleRejectedReason))
                {
                    var sampleSelectedReasonList = item.SampleRejectedReason!.Split(',').Select(Guid.Parse).ToArray();

                    item.SampleRejectedReasonName = string.Join(',', ReasonsList
                        .Where(x => sampleSelectedReasonList.Contains(x.ProfileId)).Select(x => x.Name).ToList());
                }
            }
            var responseObject = new ViewPagerDto<ViewSampleCollectedConsignmentLilst>
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
        public async Task<ViewPagerDto<ViewSampleCollectedConsignmentLilst>> GetReceiveReceivedCollectedSampleList(FilterSampleConsignmentDto filter)
        {
            filter.LabTestStage = 2;
            var loginUser = TokenService.GetUserLoggedInfo();
            filter.ToHealthFacilityId = loginUser.HealthFacilityId;
            var IsRadiologist = loginUser!.UserRoleList.SingleOrDefault(x => x.ShortName == CommonStringConstant.Radiologist) ?? null;

            var _uowProfile = new UnitOfWork<Profile>(_uowSampleConsignment.GetDbContext());
            var ReasonsList = new List<Profile>();

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            if (filter.LabTestStage == CommonStringConstant.SampleRejected)
                ReasonsList = _uowProfile.Repository.GetALL()
                    .Where(x => x.ProfileType.ShortName == CommonStringConstant.LabSampleRejectedReasons)
                    .ToList();

            var list = _uowSampleConsignment.GetDbContext().ViewSampleCollectedConsignmentLists
                .Where(x => x.LabDepartmentShortName == CommonStringConstant.InternalLabTest &&
                (x.LabTestName.Contains(CommonStringConstant.PCR) ||
                x.LabTestName.Contains(CommonStringConstant.SVR)) &&
                x.BatchNumber == null && x.ConsignmentStatus == 2)

                .WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByCNIC, x => x.Cnic.ToLower() == filter.SearchString)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByMobileNo, x => x.PatientMobileNo.ToLower() == filter.SearchString)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByMrNo, x => x.MrNo.ToLower() == filter.SearchString)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByBarcode, x => x.BarcodeNo.ToLower() == filter.SearchString || x.PreGeneratedBarcodeNo.ToLower() == filter.SearchString)

                //.WhereIf(!string.IsNullOrEmpty(filter.SearchString),
                //x => x.Cnic.ToLower().StartsWith(filter.SearchString) ||
                //x.MrNo!.ToLower().StartsWith(filter.SearchString) ||
                //x.PatientMobileNo!.ToLower().StartsWith(filter.SearchString) ||
                //x.BarcodeNo!.ToLower().StartsWith(filter.SearchString) ||
                //x.PatientName!.ToLower().StartsWith(filter.SearchString) ||
                //x.PreGeneratedBarcodeNo!.ToLower().StartsWith(filter.SearchString))

                //.WhereIf(!AppCommonMethod.IsNullBool(filter.Rider) && filter.Rider == true, x => x.RiderUserId != null)

                //.WhereIf(!AppCommonMethod.IsNullBool(filter.Rider) && filter.Rider == false, x => x.RiderUserId == null)

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.LabTestStage) && filter.LabTestStage == CommonStringConstant.PendingCollection, x => x.IsSampleCollected != true && x.IsReportGenerated != true && x.IsSampleRejected != true)

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.LabTestStage) && filter.LabTestStage == CommonStringConstant.SampleCollected, x => x.IsSampleCollected == true && x.IsReportGenerated != true && x.IsSampleRejected != true && x.LabTestName != CommonStringConstant.CXR)

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.LabTestStage) && filter.LabTestStage == CommonStringConstant.ReportGenerated, x => x.IsSampleCollected == true && x.IsReportGenerated == true && x.IsSampleRejected != true)

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.LabTestStage) && filter.LabTestStage == CommonStringConstant.SampleRejected, x => x.IsSampleRejected == true)



                .WhereIf(!AppCommonMethod.IsNullObject(IsRadiologist), x => x.LabTypeShortName == CommonStringConstant.LabTypeXray)

                //.WhereIf(!AppCommonMethod.IsNullBool(filter.IsExternalSource), x => x.IsAdvisedExternally == filter.IsExternalSource)

                .WhereIf(!AppCommonMethod.IsNullBool(filter.IsArchive), x => x.IsArchived == filter.IsArchive)

                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.ProvinceId == filter.ProvinceId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ToHealthFacilityId), x => x.ConsignmentToHealthFacilityId == filter.ToHealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.AdvisedOn!.Value.Date >= filter.StartDate!.Value.Date)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.AdvisedOn!.Value.Date <= filter.EndDate!.Value.Date)
                .WhereIf(!string.IsNullOrEmpty(filter.User) && !AppCommonMethod.IsNullorZeroInt(filter.LabTestStage) && filter.LabTestStage == CommonStringConstant.ReportInDashboard, x => x.IsSampleCollected == true && x.IsReportGenerated == true && user.Contains(x.ReportGeneratedById!.ToString()))
                .WhereIf(!string.IsNullOrEmpty(filter.User) && !AppCommonMethod.IsNullorZeroInt(filter.LabTestStage) && filter.LabTestStage == CommonStringConstant.SampleCollectedInDashboard, x => x.IsSampleCollected == true && user.Contains(x.SampleCollectedById!.ToString()))
                .OrderByDescending(x => x.AdvisedOn);

            IQueryable<ViewSampleCollectedConsignmentLilst> IQueryableList = list.Select(x =>
               new ViewSampleCollectedConsignmentLilst
               {
                   PatientLabTestId = x.PatientLabTestId,
                   PatientId = x.PatientId,
                   PatientVisitId = x.PatientVisitId,

                   StageName = x.StageName,
                   StatusCode = x.StatusCode,
                   StatusName = x.StatusName,

                   LabTestId = x.LabTestId,
                   BarcodeNo = x.BarcodeNo,
                   LabDepartmentName = x.LabDepartmentName,
                   Cnic = x.Cnic,
                   MrNo = x.MrNo,
                   PatientMobileNo = x.PatientMobileNo,

                   PatientName = x.PatientName,
                   LabTestName = x.LabTestName,
                   AdvisedBy = x.AdvisedBy,
                   AdvisedOn = x.AdvisedOn,
                   TestPrice = x.TestPrice,
                   SampleType = x.SampleType,

                   IsFromCallCenter = x.IsFromCallCenter,
                   IsSampleRequired = x.IsSampleRequired,

                   IsSampleCollected = x.IsSampleCollected,
                   SampleCollectedBy = x.SampleCollectedBy,
                   SampleCollectedOn = x.SampleCollectedOn,

                   IsReportGenerated = x.IsReportGenerated,
                   ReportGeneratedBy = x.ReportGeneratedBy,
                   ReportGeneratedOn = x.ReportGeneratedOn,

                   IsSampleRejected = x.IsSampleRejected,
                   SampleRejectedBy = x.SampleRejectedBy,
                   SampleRejectedOn = x.SampleRejectedOn,

                   IsAdvisedExternally = x.IsAdvisedExternally,

                   SourceDoctorName = x.SourceDoctorName,
                   HealthFacilityName = x.HealthFacilityName,

                   SampleRejectedReason = x.SampleRejectedReason!,

                   LabType = x.LabType,
                   LabTypeShortName = x.LabTypeShortName,
                   ReportLink = x.ReportLink,
                   ResultImageLink = x.ResultImageLink,
                   ConsignmentToHealthFacilityId = x.ConsignmentToHealthFacilityId

               });

            var pagedList = await PagedListDto<ViewSampleCollectedConsignmentLilst>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            foreach (var item in pagedList)
            {
                if (!string.IsNullOrEmpty(item.SampleRejectedReason))
                {
                    var sampleSelectedReasonList = item.SampleRejectedReason!.Split(',').Select(Guid.Parse).ToArray();

                    item.SampleRejectedReasonName = string.Join(',', ReasonsList
                        .Where(x => sampleSelectedReasonList.Contains(x.ProfileId)).Select(x => x.Name).ToList());
                }
            }
            var responseObject = new ViewPagerDto<ViewSampleCollectedConsignmentLilst>
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

        // This Method is updated code of GetReceiveReceivedCollectedSampleList
        // This Method Is Updated Version Of GetReceiveReceivedCollectedSampleList Now Move TO Store Procedure 

        public async Task<SampleCollectedConsignmentListDTO> GetSampleCollectedConsignmentList(FilterSampleConsignmentDto filter)
        {
            filter.LabTestStage = 2; // esko b sp my ly jayn
            var loginUser = TokenService.GetUserLoggedInfo();
            filter.ToHealthFacilityId = loginUser.HealthFacilityId;
            var _uowProfile = new UnitOfWork<Profile>(_uowSampleConsignment.GetDbContext());

            SampleCollectedConsignmentListDTO SampleList = new SampleCollectedConsignmentListDTO();

            var conn = _uowProfile.GetDbContext().Database.GetDbConnection();
            try
            {
                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand("SpSampleCollectedConsignmentList", (SqlConnection)conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                if (!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy))
                    sqlComm.Parameters.AddWithValue("@FilterBy", filter.FilterBy);

                if (!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy))
                    sqlComm.Parameters.AddWithValue("@SearchString", filter.SearchString);

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

                if (!AppCommonMethod.IsNullorZeroInt(filter.ToHealthFacilityId))
                    sqlComm.Parameters.AddWithValue("@ToHealthFacilityId", filter.ToHealthFacilityId);

                //if (!AppCommonMethod.IsNullorZeroInt(filter.DepartmentId))
                //    sqlComm.Parameters.AddWithValue("@DepartmentId", filter.DepartmentId);

                //if (!AppCommonMethod.IsNullorZeroInt(filter.SectionId))
                //    sqlComm.Parameters.AddWithValue("@SectionId", filter.SectionId);

                if (!string.IsNullOrEmpty(filter.User))
                    sqlComm.Parameters.AddWithValue("@UserId", string.Join(",", filter!.User!.Split(',').Select(x => string.Format("'{0}'", x)).ToList()));

                sqlComm.Parameters.AddWithValue("@PageNumber", filter.PageNumber);
                sqlComm.Parameters.AddWithValue("@PageSize", filter.PageSize);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                await Task.Run(() => da.Fill(ds));
                SampleList.TotalCount = ds.Tables[0].ToList<SampleCollectedConsignmentListDTO>().FirstOrDefault().TotalCount;
                SampleList.List = ds.Tables[1].ToList<SampleCollectedConsignmentList>();

                return SampleList;
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
        public async Task<ViewPagerDto<ViewSampleCollectedConsignmentLilst>> GetHCPAllSampleCollectedConsignmentList(FilterSampleConsignmentDto filter)
        {
            var loginUser = TokenService.GetUserLoggedInfo();
            var IsRadiologist = loginUser!.UserRoleList.SingleOrDefault(x => x.ShortName == CommonStringConstant.Radiologist) ?? null;

            var _uowProfile = new UnitOfWork<Profile>(_uowSampleConsignment.GetDbContext());
            var ReasonsList = new List<Profile>();

            List<string> user = new List<string>();
            if (filter.User != null)
                user = filter!.User!.Split(',').ToList();

            if (filter.LabTestStage == CommonStringConstant.SampleRejected)
                ReasonsList = _uowProfile.Repository.GetALL()
                    .Where(x => x.ProfileType.ShortName == CommonStringConstant.LabSampleRejectedReasons)
                    .ToList();

            var list = _uowSampleConsignment.GetDbContext().ViewSampleCollectedConsignmentLists
                .Where(x => x.LabDepartmentShortName == CommonStringConstant.InternalLabTest && (x.LabTestName == CommonStringConstant.HBVPCRTest || x.LabTestName == CommonStringConstant.HCVPCRTest))

                .WhereIf(!string.IsNullOrEmpty(filter.SearchString),
                x => x.Cnic.ToLower().StartsWith(filter.SearchString) ||
                x.MrNo!.ToLower().StartsWith(filter.SearchString) ||
                x.PatientMobileNo!.ToLower().StartsWith(filter.SearchString) ||
                x.BarcodeNo!.ToLower().StartsWith(filter.SearchString) ||
                x.PreGeneratedBarcodeNo!.ToLower().StartsWith(filter.SearchString))

                //.WhereIf(!AppCommonMethod.IsNullBool(filter.Rider) && filter.Rider == true, x => x.RiderUserId != null)

                //.WhereIf(!AppCommonMethod.IsNullBool(filter.Rider) && filter.Rider == false, x => x.RiderUserId == null)

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.LabTestStage) && filter.LabTestStage == CommonStringConstant.PendingCollection, x => x.IsSampleCollected != true && x.IsReportGenerated != true && x.IsSampleRejected != true)

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.LabTestStage) && filter.LabTestStage == CommonStringConstant.SampleCollected, x => x.IsSampleCollected == true && x.IsReportGenerated != true && x.IsSampleRejected != true && x.LabTestName != CommonStringConstant.CXR)

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.LabTestStage) && filter.LabTestStage == CommonStringConstant.ReportGenerated, x => x.IsSampleCollected == true && x.IsReportGenerated == true && x.IsSampleRejected != true)

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.LabTestStage) && filter.LabTestStage == CommonStringConstant.SampleRejected, x => x.IsSampleRejected == true)



                .WhereIf(!AppCommonMethod.IsNullObject(IsRadiologist), x => x.LabTypeShortName == CommonStringConstant.LabTypeXray)

                //.WhereIf(!AppCommonMethod.IsNullBool(filter.IsExternalSource), x => x.IsAdvisedExternally == filter.IsExternalSource)

                .WhereIf(!AppCommonMethod.IsNullBool(filter.IsArchive), x => x.IsArchived == filter.IsArchive)

                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.ProvinceId == filter.ProvinceId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId)
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ToHealthFacilityId), x => x.ConsignmentToHealthFacilityId == filter.ToHealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.AdvisedOn!.Value.Date >= filter.StartDate!.Value.Date)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.AdvisedOn!.Value.Date <= filter.EndDate!.Value.Date)
                .WhereIf(!string.IsNullOrEmpty(filter.User) && !AppCommonMethod.IsNullorZeroInt(filter.LabTestStage) && filter.LabTestStage == CommonStringConstant.ReportInDashboard, x => x.IsSampleCollected == true && x.IsReportGenerated == true && user.Contains(x.ReportGeneratedById!.ToString()))
                .WhereIf(!string.IsNullOrEmpty(filter.User) && !AppCommonMethod.IsNullorZeroInt(filter.LabTestStage) && filter.LabTestStage == CommonStringConstant.SampleCollectedInDashboard, x => x.IsSampleCollected == true && user.Contains(x.SampleCollectedById!.ToString()))
                .OrderByDescending(x => x.AdvisedOn);

            IQueryable<ViewSampleCollectedConsignmentLilst> IQueryableList = list.Select(x =>
               new ViewSampleCollectedConsignmentLilst
               {
                   PatientLabTestId = x.PatientLabTestId,
                   PatientId = x.PatientId,
                   PatientVisitId = x.PatientVisitId,

                   StageName = x.StageName,
                   StatusCode = x.StatusCode,
                   StatusName = x.StatusName,

                   LabTestId = x.LabTestId,
                   BarcodeNo = x.BarcodeNo,
                   LabDepartmentName = x.LabDepartmentName,
                   Cnic = x.Cnic,
                   MrNo = x.MrNo,
                   PatientMobileNo = x.PatientMobileNo,

                   PatientName = x.PatientName,
                   LabTestName = x.LabTestName,
                   AdvisedBy = x.AdvisedBy,
                   AdvisedOn = x.AdvisedOn,
                   TestPrice = x.TestPrice,
                   SampleType = x.SampleType,

                   IsFromCallCenter = x.IsFromCallCenter,
                   IsSampleRequired = x.IsSampleRequired,

                   IsSampleCollected = x.IsSampleCollected,
                   SampleCollectedBy = x.SampleCollectedBy,
                   SampleCollectedOn = x.SampleCollectedOn,

                   IsReportGenerated = x.IsReportGenerated,
                   ReportGeneratedBy = x.ReportGeneratedBy,
                   ReportGeneratedOn = x.ReportGeneratedOn,

                   IsSampleRejected = x.IsSampleRejected,
                   SampleRejectedBy = x.SampleRejectedBy,
                   SampleRejectedOn = x.SampleRejectedOn,

                   IsAdvisedExternally = x.IsAdvisedExternally,

                   SourceDoctorName = x.SourceDoctorName,
                   HealthFacilityName = x.HealthFacilityName,

                   SampleRejectedReason = x.SampleRejectedReason!,

                   LabType = x.LabType,
                   LabTypeShortName = x.LabTypeShortName,
                   ReportLink = x.ReportLink,
                   ResultImageLink = x.ResultImageLink,
                   ConsignmentToHealthFacilityId = x.ConsignmentToHealthFacilityId

               });

            var pagedList = await PagedListDto<ViewSampleCollectedConsignmentLilst>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            foreach (var item in pagedList)
            {
                if (!string.IsNullOrEmpty(item.SampleRejectedReason))
                {
                    var sampleSelectedReasonList = item.SampleRejectedReason!.Split(',').Select(Guid.Parse).ToArray();

                    item.SampleRejectedReasonName = string.Join(',', ReasonsList
                        .Where(x => sampleSelectedReasonList.Contains(x.ProfileId)).Select(x => x.Name).ToList());
                }
            }

            var responseObject = new ViewPagerDto<ViewSampleCollectedConsignmentLilst>
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


        public async Task<ViewPagerDto<ViewRejectedConsignment>> GetRejectedConsignment(FilterSampleConsignmentDto filter)
        {
            var list = _uowSampleConsignment.GetDbContext().ViewRejectedConsignments.Where(x => x.UpdatedBy == _tokenService.GetUserId())

                .WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByCNIC, x => x.Cnic.ToLower() == filter.SearchString)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByMobileNo, x => x.MobileNo.ToLower() == filter.SearchString)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByMrNo, x => x.Mrno.ToLower() == filter.SearchString)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString) && !AppCommonMethod.IsNullorZeroInt(filter.FilterBy) && filter.FilterBy == CommonStringConstant.FilterByBatchNumber, x => x.BatchNo.ToLower() == filter.SearchString)

                .OrderByDescending(x => x.CreatedOn);

            IQueryable<ViewRejectedConsignment> IQueryableList = list.Select(x =>
               new ViewRejectedConsignment
               {
                   Cnic = x.Cnic,
                   Mrno = x.Mrno,
                   FullName = x.FullName,

                   MobileNo = x.MobileNo,
                   LabTestName = x.LabTestName,
                   BatchNo = x.BatchNo,
                   FromHealthFacility = x.FromHealthFacility,
                   ToHealthFacility = x.ToHealthFacility,
                   StatusReason = x.StatusReason,

                   CreatedOn = x.CreatedOn
               });

            var pagedList = await PagedListDto<ViewRejectedConsignment>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );



            var responseObject = new ViewPagerDto<ViewRejectedConsignment>
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

        #endregion

        #region Helper Methods

        public string UpdateBacthNo(int? HfId)
        {
            var LastBatchNo = _uowSampleConsignment.Repository.GetALL(x => x.FromHealthFacilityId == HfId)
                                    .OrderByDescending(x => x.BatchNo)
                                    .Select(x => x.BatchNo)
                                    .FirstOrDefault();

            if (string.IsNullOrEmpty(LastBatchNo))
                return "BAT-0000000001";
            else
                return "BAT-" + IncrementStringEnd(LastBatchNo.Remove(0, 4), 10);
        }
        public static string IncrementStringEnd(string name, int minNumericalCharacters = 1)
        {
            var prefix = System.Text.RegularExpressions.Regex.Match(name, @"\d+$");
            if (prefix.Success)
            {
                var capture = prefix.Captures[0];
                long number = long.Parse(capture.Value) + 1;
                name = name.Remove(capture.Index, capture.Length) + number.ToString("D" + minNumericalCharacters);
            }

            return name;
        }
        private void FillEntity(SampleConsignment obj)
        {
            if (obj.SampleConsignmentId == Guid.Empty)
            {
                obj.SampleConsignmentId = Guid.NewGuid();
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

            if (obj.SampleConsignmentDetails.Count > 0)
            {
                foreach (var SampleConsignmentDetail in obj.SampleConsignmentDetails)
                {
                    if (SampleConsignmentDetail.SampleConsignmentDetailId == Guid.Empty)
                    {
                        SampleConsignmentDetail.SampleConsignmentDetailId = Guid.NewGuid();
                        SampleConsignmentDetail.CreatedBy = _tokenService.GetUserId();
                        SampleConsignmentDetail.CreatedOn = DateTime.Now;
                        SampleConsignmentDetail.ActionTypeId = (int)ActionTypeEnum.Create;
                        SampleConsignmentDetail.Status = (byte?)SampleConsignmentDetailStatus.Pending;
                        SampleConsignmentDetail.IsActive = true;
                    }
                    else
                    {
                        SampleConsignmentDetail.UpdatedBy = _tokenService.GetUserId();
                        SampleConsignmentDetail.UpdatedOn = DateTime.Now;
                        SampleConsignmentDetail.ActionTypeId = (int)ActionTypeEnum.Edit;
                    }
                }
            }
        }
        private void FillEntityDelete(SampleConsignment obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }
        private void FillEntitySampleConsignmentDetail(SampleConsignmentDetail obj)
        {
            if (obj.SampleConsignmentDetailId == Guid.Empty)
            {
                obj.SampleConsignmentId = Guid.NewGuid();
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

        #endregion

    }

}
