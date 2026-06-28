using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using AuthDAL.Models.Dto.InvoiceDto;
using AuthDAL.Models.Dto.LocationDto;
using HMIS.MIMS.Domain.Models.DTO.InventoryDetailDto;
using HMIS.MIMS.Domain.Models.DTO.InventoryMasterDto;
using HMIS.MIMS.Domain.Models.DTO.PaginationDto;




namespace AuthBAL
{
    public class InvoiceService<TEntity> where TEntity : class
    {

        #region Class Fields & Propertities

        private readonly IMapper _mapper;
        private UnitOfWork<InvoiceMaster> _uowInvoiceMaster;
        private readonly TokenService _tokenService;
        private readonly IRedisCacheService _cacheService;

        #endregion

        #region Constructor

        public InvoiceService(TokenService tokenService,
            UnitOfWork<InvoiceMaster> uowInvoiceMaster,
            IMapper mapper,
            IRedisCacheService redisCacheService
        )
        {
            _mapper = mapper;
            _uowInvoiceMaster = uowInvoiceMaster;
            _tokenService = tokenService;
            _cacheService = redisCacheService;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateAndEditInvoiceDto> CreateOrEdit(CreateAndEditInvoiceDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.invoiceMasterDto.InvoiceMasterId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateAndEditInvoiceDto> Create(CreateAndEditInvoiceDto input)
        {
            var _uowInvoiceItemList = new UnitOfWork<InvoiceItemList>(_uowInvoiceMaster.GetDbContext());
            var _uowDocumentList = new UnitOfWork<InvoiceDocumentList>(_uowInvoiceMaster.GetDbContext());

            InvoiceMaster invoiceMaster = new InvoiceMaster();
            invoiceMaster = _mapper.Map<InvoiceMaster>(input.invoiceMasterDto);
            FillEntityInvoiceMaster(invoiceMaster);
            await _uowInvoiceMaster.Repository.Insert(invoiceMaster);
            await _uowInvoiceMaster.Save();

            List<InvoiceItemList> invoiceItemList = new List<InvoiceItemList>();
            invoiceItemList = _mapper.Map<List<InvoiceItemList>>(input.invoiceItemListDto);
            foreach (var item in invoiceItemList)
            {
                item.InvoiceMasterId = invoiceMaster.InvoiceMasterId;
                FillEntityInvoiceItem(item);
                await _uowInvoiceItemList.Repository.Insert(item);

            }
            await _uowInvoiceItemList.Save();



            List<InvoiceDocumentList> invoicedocumentList = new List<InvoiceDocumentList>();
            invoicedocumentList = _mapper.Map<List<InvoiceDocumentList>>(input.invoiceDocumentListDto);
            foreach (var item in invoicedocumentList)
            {
                item.InvoiceMasterId = invoiceMaster.InvoiceMasterId;
                FillEntityInvoiceDocument(item);
                await _uowDocumentList.Repository.Insert(item);

            }
            await _uowDocumentList.Save();

            return input;
        }
        #endregion
        private async Task<CreateAndEditInvoiceDto> Update(CreateAndEditInvoiceDto input)
        {
            var _uowInvoiceItemList = new UnitOfWork<InvoiceItemList>(_uowInvoiceMaster.GetDbContext());
            var _uowDocumentList = new UnitOfWork<InvoiceDocumentList>(_uowInvoiceMaster.GetDbContext());
            var dbObj = await _uowInvoiceMaster.Repository.GetALL(x => x.InvoiceMasterId == input.invoiceMasterDto.InvoiceMasterId && x.IsActive == true && x.IsDelete == false).FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input.invoiceMasterDto, dbObj);
            FillEntityInvoiceMaster(obj!);
            _uowInvoiceMaster.Repository.Update(obj!);
            await _uowInvoiceMaster.CommitAsync();


            var dbObjItem = await _uowInvoiceItemList.Repository.GetALL(x => x.InvoiceMasterId == input.invoiceMasterDto.InvoiceMasterId && x.IsActive == true && x.IsDelete == false).ToListAsync();
            foreach (var item in dbObjItem)
            {

                _uowInvoiceItemList.Repository.Delete(item);

                //item.IsActive = false;
                //item.IsDelete = true;
                //FillEntityInvoiceItemUpdate(item);
                //_uowInvoiceItemList.Repository.Update(item!);
            }
            await _uowInvoiceItemList.Save();


            List<InvoiceItemList> invoiceItemList = new List<InvoiceItemList>();
            invoiceItemList = _mapper.Map<List<InvoiceItemList>>(input.invoiceItemListDto);
            foreach (var item in invoiceItemList)
            {
                item.InvoiceMasterId = input.invoiceMasterDto.InvoiceMasterId;
                FillEntityInvoiceItem(item);
                await _uowInvoiceItemList.Repository.Insert(item);
            }
            await _uowInvoiceItemList.Save();


            var dbObjDoc = await _uowDocumentList.Repository.GetALL(x => x.InvoiceMasterId == input.invoiceMasterDto.InvoiceMasterId && x.IsActive == true && x.IsDelete == false).ToListAsync();
            foreach (var item in dbObjDoc)
            {

                _uowDocumentList.Repository.Delete(item);

                //item.IsActive = false;
                //item.IsDelete = true;
                //FillEntityInvoiceDocumentUpdate(item);
                //_uowDocumentList.Repository.Update(item!);
            }
            await _uowDocumentList.Save();


            List<InvoiceDocumentList> invoiceDocList = new List<InvoiceDocumentList>();
            invoiceDocList = _mapper.Map<List<InvoiceDocumentList>>(input.invoiceDocumentListDto);
            foreach (var item in invoiceDocList)
            {
                item.InvoiceMasterId = input.invoiceMasterDto.InvoiceMasterId;
                FillEntityInvoiceDocument(item);
                await _uowDocumentList.Repository.Insert(item);
            }
            await _uowDocumentList.Save();


            return input;
        }

        public async Task<bool> Delete(Guid Id)
        {
            var _uowInvoiceItemList = new UnitOfWork<InvoiceItemList>(_uowInvoiceMaster.GetDbContext());
            var _uowDocumentList = new UnitOfWork<InvoiceDocumentList>(_uowInvoiceMaster.GetDbContext());
            var dbObj = await _uowInvoiceMaster.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityInvoiceMasterDelete(dbObj!);
            _uowInvoiceMaster.Repository.Update(dbObj!);
            await _uowInvoiceMaster.CommitAsync();

            var dbObjItem = await _uowInvoiceItemList.Repository.GetALL(x => x.InvoiceMasterId == Id && x.IsActive == true && x.IsDelete == false).ToListAsync();
            foreach (var item in dbObjItem)
            {
                FillEntityInvoiceItemDelete(item);
                _uowInvoiceItemList.Repository.Update(item!);
            }
            await _uowInvoiceItemList.Save();

            var dbObjDoc = await _uowDocumentList.Repository.GetALL(x => x.InvoiceMasterId == Id && x.IsActive == true && x.IsDelete == false).ToListAsync();
            foreach (var item in dbObjDoc)
            {
                FillEntityInvoiceDocumentDelete(item);
                _uowDocumentList.Repository.Update(item!);
            }
            await _uowDocumentList.Save();

            //await RefreshLabTestCacheList();
            return true;
        }

        //#endregion

        //#region Read Operations

        //public async Task<List<ViewLabTest>> GetAll(Guid DepartmentProfileId = new Guid())
        //{
        //    List<ViewLabTest> responseObj = await _uowLabTest.GetDbContext().ViewLabTests
        //        .WhereIf(!AppCommonMethod.IsNullOrEmptyGuid(DepartmentProfileId), x => x.DepartmentProfileId == DepartmentProfileId)
        //        .Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
        //        .OrderByDescending(x => x.DepartmentShortName)
        //        .ThenBy(x => x.Name)
        //        .ToListAsync();

        //    return responseObj;
        //}

        //public async Task<List<SPViewLabTest>> GetAllWithHealthFacilityId(int? HealthFacilityId)
        //{
        //    var userHfId = TokenService.GetUserHfId();

        //    var conn = _uowLabTest.GetDbContext().Database.GetDbConnection();
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        SqlCommand sqlComm = new SqlCommand("[dbo].[SPGetAllLabTestByHealthFacilityId]", (SqlConnection)conn);
        //        sqlComm.CommandType = CommandType.StoredProcedure;

        //        if (!AppCommonMethod.IsNullorZeroInt(userHfId))
        //            sqlComm.Parameters.AddWithValue("@HealthFacilityId", userHfId);

        //        SqlDataAdapter da = new SqlDataAdapter();
        //        da.SelectCommand = sqlComm;
        //        await Task.Run(() => da.Fill(ds));
        //        var lst = ds.Tables[0].ToList<SPViewLabTest>();
        //        return lst;
        //        //return responseObject;

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }


        //    //List<ViewLabTest> responseObj = await _uowLabTest.GetDbContext().ViewLabTests
        //    //    .WhereIf(!AppCommonMethod.IsNullOrEmptyGuid(DepartmentProfileId), x => x.DepartmentProfileId == DepartmentProfileId)
        //    //    .Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
        //    //    .OrderByDescending(x => x.DepartmentShortName)
        //    //    .ThenBy(x => x.Name)
        //    //    .ToListAsync();

        //    //return responseObj;
        //}

        //public async Task RefreshLabTestCacheList()
        //{
        //    var dbLabTests = await _uowLabTest.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).OrderBy(x => x.Name).ToListAsync();

        //    var cacheData = _mapper.Map<List<CacheLabTestDto>>(dbLabTests);

        //    _cacheService.Set<List<CacheLabTestDto>?>(CacheKeyConstant.LabTest, cacheData, null, null);
        //}

        //public async Task<List<CacheLabTestDto>> GetAllCacheLabTest()
        //{
        //    var labTests = _cacheService.Get<List<CacheLabTestDto>?>(CacheKeyConstant.LabTest);

        //    if (labTests == null)
        //    {
        //        var dbLabTests = await _uowLabTest.Repository.GetALL(x => x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted).OrderBy(x => x.Name).ToListAsync();

        //        var cacheData = _mapper.Map<List<CacheLabTestDto>>(dbLabTests);

        //        labTests = _cacheService.Set<List<CacheLabTestDto>?>(CacheKeyConstant.LabTest, cacheData, null, null);
        //    }

        //    return labTests!.ToList();
        //}




        public async Task<AuthDAL.Models.Dto.PaginationDto.ViewPagerDto<InvoiceMaster>> GetAllWithPagination(FilterUserDto filter)
        {

            var list = _uowInvoiceMaster.Repository.GetALL(x => x.IsActive == true)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.BillToName!.ToLower().Contains(filter.SearchString))
                .OrderByDescending(x => x.CreatedOn);

            //IQueryable<InvoiceMaster> IQueryableList = list.Select(x =>
            //    new InvoiceMaster
            //    {
            //        LabTestId = x.LabTestId,
            //        Name = x.Name,
            //        Description = x.Description,
            //        DepartmentProfileId = x.DepartmentProfileId,
            //        DoctorShare = x.DoctorShare,
            //        GovtShare = x.GovtShare,
            //        IsActive = x.IsActive,
            //        LabTestCategoryProfileId = x.LabTestCategoryProfileId,
            //        LabTestTypeProfileId = x.LabTestTypeProfileId,
            //        IsSampleRequired = x.IsSampleRequired,
            //        SampleType = x.SampleType,
            //        StaffShare = x.StaffShare,
            //        TestPrice = x.TestPrice,
            //        LabTestDetails = _mapper.Map<List<CreateOrEditLabTestDetailDto>>(x.LabTestDetails.ToList()),
            //    });

            var pagedList = await AuthDAL.Models.Dto.PaginationDto.PagedListDto<InvoiceMaster>.ToPagedListAsync(
                 //  IQueryableList,
                   list,
                   filter.PageNumber,
                   filter.PageSize
                   );

            //foreach (var item in pagedList)
            //{
            //    item.CreatedByName = userList.Where(x => x.UserId == item.CreatedBy).Select(x => x.FullName).FirstOrDefault();
            //    item.UpdatedByName = userList.Where(x => x.UserId == item.UpdatedBy).Select(x => x.FullName).FirstOrDefault();
            //}

            var responseObject = new AuthDAL.Models.Dto.PaginationDto.ViewPagerDto<InvoiceMaster>
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



        public async Task<InvoiceDashboardCountAndListDto> GetInvoiceDetailById(StockCheckboxCheckFilter? inventory)
        {
            var conn = _uowInvoiceMaster.GetDbContext().Database.GetDbConnection();
            try
            {
                InvoiceDashboardCountAndListDto invoiceDashboardCountAndListDto = new InvoiceDashboardCountAndListDto();
                var datenearexpire = inventory.FromDate?.AddHours(5);
                var dateexpire = inventory.ToDate?.AddHours(5);

                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand("[dbo].[GetInvoiceDetailBySearch]", (SqlConnection)conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                //if (!AppCommonMethod.IsNullOrEmptyGuid(TokenService.GetUserLoggedInfo()?.MimsBranchId))
                //    sqlComm.Parameters.AddWithValue("@BranchId", TokenService.GetUserLoggedInfo()?.MimsBranchId);

                //sqlComm.Parameters.AddWithValue("@FromDate", inventory?.FromDate == null ? DateTime.Now.ToString() : inventory?.FromDate.Value.ToString());// DateTime.Now.Date.ToString());
                //sqlComm.Parameters.AddWithValue("@ToDate", inventory?.ToDate == null ? DateTime.Now.ToString() : inventory?.ToDate.Value.ToString());

                //sqlComm.Parameters.AddWithValue("@FromDate", inventory?.FromDate == null ? DateTime.Now.ToString("yyyy-MM-dd") : inventory?.FromDate.Value.ToString("yyyy-MM-dd"));
                //sqlComm.Parameters.AddWithValue("@ToDate", inventory?.ToDate == null ? DateTime.Now.ToString("yyyy-MM-dd") : inventory?.ToDate.Value.ToString("yyyy-MM-dd"));

                if (!AppCommonMethod.IsNullOrEmptyGuid(inventory?.GuidId))
                    sqlComm.Parameters.AddWithValue("@GuidId", inventory?.GuidId);

                if (!AppCommonMethod.IsNullBool(inventory?.IsDueAmount))
                    sqlComm.Parameters.AddWithValue("@IsDueAmount", inventory?.IsDueAmount);

                if (!AppCommonMethod.IsNullBool(inventory?.IsTotalAmount))
                    sqlComm.Parameters.AddWithValue("@IsTotalAmount", inventory?.IsTotalAmount);

                if (!AppCommonMethod.IsNullBool(inventory?.IsAllAmount))
                    sqlComm.Parameters.AddWithValue("@IsAllAmount", inventory?.IsAllAmount);

                if (!AppCommonMethod.IsNullorEmptyDate(inventory?.FromDate))
                    sqlComm.Parameters.AddWithValue("@FromDate", datenearexpire);

                if (!AppCommonMethod.IsNullorEmptyDate(inventory?.ToDate))
                    sqlComm.Parameters.AddWithValue("@ToDate", dateexpire);

                if (!AppCommonMethod.IsNullorZeroInt(inventory?.PageNumber))
                    sqlComm.Parameters.AddWithValue("@PageNumber", inventory?.PageNumber);

                if (!AppCommonMethod.IsNullorZeroInt(inventory?.PageSize))
                    sqlComm.Parameters.AddWithValue("@PageSize", inventory?.PageSize);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                await Task.Run(() => da.Fill(ds));

                invoiceDashboardCountAndListDto.invoiceMaster  = ds.Tables[0].ToList<InvoiceMaster>();
                invoiceDashboardCountAndListDto.invoiceDashboardCountDto  = ds.Tables[1].ToList<InvoiceDashboardCountDto>();

                return invoiceDashboardCountAndListDto;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }



        
        public async Task<List<InvoiceMasterDropdownDto>> getInvoiceDetailByGuidId()
        {
            //LabTest? responseObj = await _uowLabTest.Repository.GetById(input);
            List<InvoiceMasterDropdownDto>? responseObj = await _uowInvoiceMaster.Repository.GetALL(x => x.IsActive == true)
                .Select(x => new InvoiceMasterDropdownDto
                {
                    InvoiceMasterId = x.InvoiceMasterId,  // Correctly assign properties
                    BillToName = x.BillToName + ' ' + x.InvoiceNumber,
                    //InvoiceNumber = x.InvoiceNumber,
                })
                .ToListAsync();

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<List<InvoiceMasterDropdownDto>>(responseObj);
        }

        public async Task<getInvoiceAutoCompleteDto> getBillToAndItemAutoComplete()
        {
            getInvoiceAutoCompleteDto invoiceAutoCompleteDto = new getInvoiceAutoCompleteDto();
            var _uowInvoiceItemList = new UnitOfWork<InvoiceItemList>(_uowInvoiceMaster.GetDbContext());
            var _uowInvoiceReqDocumentList = new UnitOfWork<InvoiceDocumentList>(_uowInvoiceMaster.GetDbContext());
            //LabTest? responseObj = await _uowLabTest.Repository.GetById(input);
            invoiceAutoCompleteDto.invoiceMasterDropdownDto = await _uowInvoiceMaster.Repository.GetALL(x => x.IsActive == true)
                .Select(x => new InvoiceMasterDropdownDto
                {
                    InvoiceMasterId = x.InvoiceMasterId,  // Correctly assign properties
                    BillToName = x.BillToName,
                })
                .GroupBy(x => x.BillToName)  // Group by BillToName to remove duplicates
                .Select(g => g.First())      // Select only the first entry from each group
                .ToListAsync();

            invoiceAutoCompleteDto.invoiceItemDropdownDto = await _uowInvoiceItemList.Repository.GetALL(x => x.IsActive == true)
                .Select(x => new InvoiceItemDropdownDto
                {
                    ItemListId = x.ItemListId,  // Correctly assign properties
                    ItemName = x.ItemName,
                })
                .GroupBy(x => x.ItemName)  // Group by BillToName to remove duplicates
                .Select(g => g.First())      // Select only the first entry from each group
                .ToListAsync();


            invoiceAutoCompleteDto.invoiceReqDocumentDropdownDto = await _uowInvoiceReqDocumentList.Repository.GetALL(x => x.IsActive == true)
               .Select(x => new InvoiceReqDocumentDropdownDto
               {
                   InvoiceMasterId = x.InvoiceMasterId,   // Correctly assign properties
                   DocumentName = x.DocumentName,
               })
               .GroupBy(x => x.DocumentName)  // Group by BillToName to remove duplicates
               .Select(g => g.First())      // Select only the first entry from each group
               .ToListAsync();


            //invoiceAutoCompleteDto.invoiceTermAndConDropdownDto = await _uowInvoiceMaster.Repository.GetALL(x => x.IsActive == true)
            //   .Select(x => new InvoiceTermAndConDropdownDto
            //   {
            //       InvoiceMasterId = x.InvoiceMasterId,   // Correctly assign properties
            //       TermAndCondition = x.TermAndCondition,
            //   })
            //   .GroupBy(x => x.TermAndCondition)  // Group by BillToName to remove duplicates
            //   .Select(g => g.First())      // Select only the first entry from each group
            //   .ToListAsync();
            //if (AppCommonMethod.IsNullObject(responseObj))
            //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return invoiceAutoCompleteDto;
        }
        

        public async Task<InvoiceMaster> GetViewInvoiceById(Guid Id)
        {
            var _uowInvoiceItemList = new UnitOfWork<InvoiceItemList>(_uowInvoiceMaster.GetDbContext());
            var _uowDocumentList = new UnitOfWork<InvoiceDocumentList>(_uowInvoiceMaster.GetDbContext());
            InvoiceMaster invoiceMaster = new InvoiceMaster();
            invoiceMaster = await _uowInvoiceMaster.Repository.GetALL(x => x.IsActive == true && x.InvoiceMasterId == Id).FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(invoiceMaster))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);
            invoiceMaster.InvoiceItemLists = await _uowInvoiceItemList.Repository.GetALL(x => x.IsActive == true && x.InvoiceMasterId == Id).ToListAsync();
            invoiceMaster.InvoiceDocumentLists = await _uowDocumentList.Repository.GetALL(x => x.IsActive == true && x.InvoiceMasterId == Id).ToListAsync();
            return _mapper.Map<InvoiceMaster>(invoiceMaster);
        }

        public async Task<InvoiceMaster> updateDueAmount(updateDueAmountDto obj)
        {
            
            InvoiceMaster invoiceMaster = new InvoiceMaster();
            invoiceMaster = await _uowInvoiceMaster.Repository.GetALL(x => x.IsActive == true && x.InvoiceMasterId == obj.InvoiceMasterId).FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(invoiceMaster))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                invoiceMaster.DueAmount = invoiceMaster.DueAmount - obj.DueAmountPay;
                invoiceMaster.ReceivedAmount = invoiceMaster.ReceivedAmount + obj.DueAmountPay;
                invoiceMaster.DueAmountPayBy = _tokenService.GetUserId();
                invoiceMaster.DueAmountPayOn = DateTime.Now;

            _uowInvoiceMaster.Repository.Update(invoiceMaster!);
            await _uowInvoiceMaster.CommitAsync();

            return _mapper.Map<InvoiceMaster>(invoiceMaster);
        }
        


        #region Helper Methods

        private void FillEntityInvoiceMaster(InvoiceMaster obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.InvoiceMasterId))
            {
                obj.InvoiceMasterId = Guid.NewGuid();
                obj.IsActive = true;
                obj.IsDelete = false;
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
            }
            else
            {
                obj.IsActive = true;
                obj.IsDelete = false;
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
            }
        }
        private void FillEntityInvoiceItem(InvoiceItemList obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.ItemListId))
            {
                obj.ItemListId = Guid.NewGuid();
                obj.IsActive = true;
                obj.IsDelete = false;
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
            }
            else
            {
                obj.IsActive = true;
                obj.IsDelete = false;
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
            }
        }
        private void FillEntityInvoiceDocument(InvoiceDocumentList obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.InvoiceDocumentId))
            {
                obj.InvoiceDocumentId = Guid.NewGuid();
                obj.IsActive = true;
                obj.IsDelete = false;
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
            }
            else
            {
                obj.IsActive = true;
                obj.IsDelete = false;
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
            }
        }
        
         private void FillEntityInvoiceItemUpdate(InvoiceItemList obj)
        {
            obj.UpdatedBy = _tokenService.GetUserId();
            obj.UpdatedOn = DateTime.Now;
        }
        private void FillEntityInvoiceDocumentUpdate(InvoiceDocumentList obj)
        {
            obj.UpdatedBy = _tokenService.GetUserId();
            obj.UpdatedOn = DateTime.Now;
        }


        private void FillEntityInvoiceMasterDelete(InvoiceMaster obj)
        {
            obj.IsDelete = true;
            obj.IsActive = false;
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
        }

        private void FillEntityInvoiceItemDelete(InvoiceItemList obj)
        {
            obj.IsDelete = true;
            obj.IsActive = false;
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
        }

        private void FillEntityInvoiceDocumentDelete(InvoiceDocumentList obj)
        {
            obj.IsDelete = true;
            obj.IsActive = false;
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
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