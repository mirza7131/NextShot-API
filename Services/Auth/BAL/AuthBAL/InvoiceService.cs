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





        // next shot


        public async Task<AuthDAL.Models.Dto.PaginationDto.ViewPagerDto<InventoryItem>> GetInventoryItems(FilterUserDto filter)
        {
            var _uowInventoryItem = new UnitOfWork<InventoryItem>(_uowInvoiceMaster.GetDbContext());
            var list = _uowInventoryItem.Repository
                .GetALL(x => x.IsDeleted == false)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString),
                    x => x.Name.ToLower().Contains(filter.SearchString.ToLower())
                      || x.Category.ToLower().Contains(filter.SearchString.ToLower()))
                .OrderByDescending(x => x.CreatedOn);

            var pagedList = await AuthDAL.Models.Dto.PaginationDto.PagedListDto<InventoryItem>.ToPagedListAsync(
                list,
                filter.PageNumber,
                filter.PageSize
            );

            var responseObject = new AuthDAL.Models.Dto.PaginationDto.ViewPagerDto<InventoryItem>
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

        public async Task<InventoryItem> GetInventoryItemById(int id)
        {
            var _uowInventoryItem = new UnitOfWork<InventoryItem>(_uowInvoiceMaster.GetDbContext());
            var item = await _uowInventoryItem.Repository
                .GetALL(x => x.InventoryItemId == id && x.IsDeleted == false).FirstOrDefaultAsync();

            return item;
        }

        public async Task<bool> CreateInventoryItem(CreateInventoryItemDto input)
        {
            var _uowInventoryItem = new UnitOfWork<InventoryItem>(_uowInvoiceMaster.GetDbContext());
            var obj = new InventoryItem
            {
                Name = input.Name,
                Category = input.Category,
                Price = input.Price,
                StockQty = input.StockQty,
                IsActive = true,
                IsDeleted = false,
                CreatedBy = input.CreatedBy,
                CreatedOn = DateTime.Now
            };

            await _uowInventoryItem.Repository.Insert(obj);
            await _uowInventoryItem.Save();

            return true;
        }

        public async Task<bool> UpdateInventoryItem(UpdateInventoryItemDto input)
        {
            var _uowInventoryItem = new UnitOfWork<InventoryItem>(_uowInvoiceMaster.GetDbContext());
            var item = await _uowInventoryItem.Repository
                .GetALL(x => x.InventoryItemId == input.InventoryItemId && x.IsDeleted == false).FirstOrDefaultAsync();

            if (item == null)
                return false;

            item.Name = input.Name;
            item.Category = input.Category;
            item.Price = input.Price;
            item.StockQty = input.StockQty;
            item.UpdatedBy = input.UpdatedBy;
            item.UpdatedOn = DateTime.Now;

            _uowInventoryItem.Repository.Update(item);
            await _uowInventoryItem.Save();

            return true;
        }

        public async Task<bool> DeleteInventoryItem(int id)
        {
            var _uowInventoryItem = new UnitOfWork<InventoryItem>(_uowInvoiceMaster.GetDbContext());
            var item = await _uowInventoryItem.Repository
                .GetALL(x => x.InventoryItemId == id && x.IsDeleted == false).FirstOrDefaultAsync();

            if (item == null)
                return false;

            item.IsDeleted = true;
            item.IsActive = false;
            item.DeletedOn = DateTime.Now;

            _uowInventoryItem.Repository.Update(item);
            await _uowInventoryItem.Save();

            return true;
        }

        public async Task<bool> ChangeInventoryStatus(ChangeInventoryStatusDto input)
        {
            var _uowInventoryItem = new UnitOfWork<InventoryItem>(_uowInvoiceMaster.GetDbContext());
            var item = await _uowInventoryItem.Repository
                .GetALL(x => x.InventoryItemId == input.InventoryItemId && x.IsDeleted == false).FirstOrDefaultAsync();

            if (item == null)
                return false;

            item.IsActive = input.IsActive;
            item.UpdatedBy = input.UpdatedBy;
            item.UpdatedOn = DateTime.Now;

            _uowInventoryItem.Repository.Update(item);
            await _uowInventoryItem.Save();

            return true;
        }

        public async Task<bool> ReduceInventoryStock(ReduceInventoryStockDto input)
        {
            var _uowInventoryItem = new UnitOfWork<InventoryItem>(_uowInvoiceMaster.GetDbContext());
            var item = await _uowInventoryItem.Repository
                .GetALL(x =>
                    x.InventoryItemId == input.InventoryItemId &&
                    x.IsActive == true &&
                    x.IsDeleted == false).FirstOrDefaultAsync();

            if (item == null)
                return false;

            if (item.StockQty < input.Quantity)
                return false;

            item.StockQty = item.StockQty - input.Quantity;
            item.UpdatedBy = input.UpdatedBy;
            item.UpdatedOn = DateTime.Now;

            _uowInventoryItem.Repository.Update(item);
            await _uowInventoryItem.Save();

            return true;
        }





        public async Task<ClubCustomer> CreateClubCustomer(CreateClubCustomerDto input)
        {
            var _context = new UnitOfWork<ClubCustomer>(_uowInvoiceMaster.GetDbContext());

            var existing = await _context.Repository.GetALL(x =>
                    x.IsDeleted == false &&
                    x.PhoneNo == input.PhoneNo &&
                    !string.IsNullOrEmpty(input.PhoneNo))
                .FirstOrDefaultAsync();

            if (existing != null)
                return existing;

            var customer = new ClubCustomer
            {
                CustomerName = input.CustomerName,
                PhoneNo = input.PhoneNo,
                BalanceAmount = 0,
                IsActive = true,
                IsDeleted = false,
                CreatedOn = DateTime.Now
            };

            _context.Repository.Insert(customer);
            await _context.Save();

            return customer;
        }

        public async Task<List<ClubCustomerSearchDto>> SearchClubCustomers(string? search)
        {
            var _context = new UnitOfWork<ClubCustomer>(_uowInvoiceMaster.GetDbContext());

            search = search?.Trim().ToLower() ?? "";

            var list = await _context.Repository.GetALL(x =>
                    x.IsActive == true &&
                    x.IsDeleted == false &&
                    (
                        string.IsNullOrEmpty(search) ||
                        x.CustomerName.ToLower().Contains(search) ||
                        (x.PhoneNo != null && x.PhoneNo.Contains(search))
                    ))
                .OrderBy(x => x.CustomerName)
                .Take(50)
                .Select(x => new ClubCustomerSearchDto
                {
                    ClubCustomerId = x.ClubCustomerId,
                    CustomerName = x.CustomerName,
                    PhoneNo = x.PhoneNo
                })
                .ToListAsync();

            return list;
        }
        public async Task<TableSession> StartTableSession(StartTableSessionDto input)
        {
            var _context = new UnitOfWork<TableSession>(_uowInvoiceMaster.GetDbContext());

            var running = await _context.Repository.GetALL(x =>
                    x.TableNo == input.TableNo &&
                    x.Status == "Running" &&
                    x.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (running != null)
                throw new Exception("This table is already running.");

            var session = new TableSession
            {
                TableNo = input.TableNo,
                TableName = input.TableName,
                TableType = input.TableType,

                ClubCustomerId = input.ClubCustomerId,
                CustomerName = input.CustomerName,
                CustomerPhone = input.CustomerPhone,

                PlayerCount = 0,
                SessionMode = input.SessionMode,
                MinuteRate = input.MinuteRate,
                HourlyRate = input.HourlyRate,
                GameRate = input.GameRate,

                StartTime = DateTime.Now,
                Status = "Running",

                TableAmount = 0,
                InventoryAmount = 0,
                GameAmount = 0,
                DiscountAmount = 0,
                GrossAmount = 0,
                NetAmount = 0,
              //  TotalAmount = 0,
                PaidAmount = 0,
                DueAmount = 0,
                PaymentStatus = "Unpaid",

                IsActive = true,
                IsDeleted = false,
                CreatedOn = DateTime.Now
            };

            _context.Repository.Insert(session);
            await _context.Save();

            return session;
        }
        public async Task<TableSessionPlayer> AddPlayerToSession(AddSessionPlayerDto input)
        {
            var _uowSession = new UnitOfWork<TableSession>(_uowInvoiceMaster.GetDbContext());
            var _uowPlayer = new UnitOfWork<TableSessionPlayer>(_uowInvoiceMaster.GetDbContext());

            var session = await _uowSession.Repository.GetALL(x =>
                    x.TableSessionId == input.TableSessionId &&
                    x.Status == "Running" &&
                    x.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (session == null)
                throw new Exception("Running session not found.");

            var player = new TableSessionPlayer
            {
                TableSessionId = input.TableSessionId,
                ClubCustomerId = input.ClubCustomerId,
                PlayerName = input.PlayerName,
                PhoneNo = input.PhoneNo,
                IsWalkIn = input.IsWalkIn,
                //IsActive = true,
                //IsDeleted = false,
                CreatedOn = DateTime.Now
            };

            _uowPlayer.Repository.Insert(player);

            session.PlayerCount = await _uowPlayer.Repository.GetALL(x =>
                    x.TableSessionId == input.TableSessionId )
                    //&&
                    //x.IsDeleted == false)
                .CountAsync() + 1;

            await _uowPlayer.Save();

            return player;
        }
        public async Task<TableSessionInventoryItem> AddInventoryItemToSession(AddSessionInventoryDto input)
        {
            var _uowSession = new UnitOfWork<TableSession>(_uowInvoiceMaster.GetDbContext());
            var _uowInventory = new UnitOfWork<InventoryItem>(_uowInvoiceMaster.GetDbContext());
            var _uowSessionItem = new UnitOfWork<TableSessionInventoryItem>(_uowInvoiceMaster.GetDbContext());

            //var session = await _uowSession.Repository.GetALL(x =>
            //        x.TableSessionId == input.TableSessionId &&
            //        x.Status == "Running" &&
            //        x.IsDeleted == false)
            //    .FirstOrDefaultAsync();
            var session = await _uowSession.Repository.GetALL(x =>
                    x.TableSessionId == input.TableSessionId &&
                    x.IsDeleted == false)
                    .FirstOrDefaultAsync();

            if (session == null)
                throw new Exception("Running session not found.");

            var item = await _uowInventory.Repository.GetALL(x =>
                    x.InventoryItemId == input.InventoryItemId &&
                    x.IsActive == true &&
                    x.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (item == null)
                throw new Exception("Inventory item not found.");

            if (item.StockQty < input.Quantity)
                throw new Exception("Stock is not enough.");

            var totalAmount = item.Price * input.Quantity;

            var sessionItem = new TableSessionInventoryItem
            {
                TableSessionId = input.TableSessionId,
                InventoryItemId = input.InventoryItemId,
                ItemName = item.Name,
                Price = item.Price,
                Quantity = input.Quantity,
                BuyerName = input.BuyerName,
                ClubCustomerId = input.ClubCustomerId,
                CreatedOn = DateTime.Now
            };

            item.StockQty = item.StockQty - input.Quantity;
            session.InventoryAmount = session.InventoryAmount + totalAmount;

            _uowSessionItem.Repository.Insert(sessionItem);

            await _uowSessionItem.Save();

            return sessionItem;
        }
        public async Task<TableSessionGame> AddGameToSession(AddSessionGameDto input)
        {
            var _uowSession = new UnitOfWork<TableSession>(_uowInvoiceMaster.GetDbContext());
            var _uowGame = new UnitOfWork<TableSessionGame>(_uowInvoiceMaster.GetDbContext());

            var session = await _uowSession.Repository.GetALL(x =>
                    x.TableSessionId == input.TableSessionId &&
                    x.Status == "Running" &&
                    x.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (session == null)
                throw new Exception("Running session not found.");

            var game = new TableSessionGame
            {
                TableSessionId = input.TableSessionId,
                Amount = input.GameRate,
                CreatedOn = DateTime.Now
            };

            session.GameCount = session.GameCount + 1;
            session.GameAmount = session.GameAmount + input.GameRate;

            _uowGame.Repository.Insert(game);

            await _uowGame.Save();

            return game;
        }
        public async Task<TableSession> EndTableSession(EndTableSessionDto input)
        {
            var _uowSession = new UnitOfWork<TableSession>(_uowInvoiceMaster.GetDbContext());
            var _uowPayment = new UnitOfWork<CustomerPayment>(_uowInvoiceMaster.GetDbContext());
            var _uowCustomer = new UnitOfWork<ClubCustomer>(_uowInvoiceMaster.GetDbContext());

            var session = await _uowSession.Repository.GetALL(x =>
                    x.TableSessionId == input.TableSessionId &&
                    x.Status == "Running" &&
                    x.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (session == null)
                throw new Exception("Running session not found.");

            session.EndTime = DateTime.Now;

            var totalMinutes = Math.Ceiling((session.EndTime.Value - session.StartTime).TotalMinutes);

            if (session.SessionMode == "Time")
                session.TableAmount = Convert.ToDecimal(totalMinutes) * session.MinuteRate;

            session.GrossAmount = session.TableAmount + session.InventoryAmount + session.GameAmount;

            session.DiscountAmount = input.DiscountAmount;
            session.NetAmount = session.GrossAmount - input.DiscountAmount;
            session.PaidAmount = input.PaidAmount;
            session.DueAmount = session.NetAmount - input.PaidAmount;

            if (session.DueAmount <= 0)
                session.PaymentStatus = "Paid";
            else if (session.PaidAmount > 0)
                session.PaymentStatus = "Partial";
            else
                session.PaymentStatus = "Unpaid";

            session.Status = "Completed";
            session.ReceiptNo = "NS-" + DateTime.Now.ToString("yyyyMMddHHmmss");
            session.UpdatedOn = DateTime.Now;

            if (input.PlayerPayments != null && input.PlayerPayments.Any())
            {
                foreach (var player in input.PlayerPayments)
                {
                    if (player.ClubCustomerId == null || player.ClubCustomerId <= 0)
                        continue;

                    var customerExists = await _uowCustomer.Repository.GetALL(x =>
                            x.ClubCustomerId == player.ClubCustomerId.Value)
                        .AnyAsync();

                    if (!customerExists)
                        continue;

                    var netAmount = Math.Max(0, player.Amount - player.DiscountAmount);
                    var dueAmount = Math.Max(0, netAmount - player.PaidAmount);

                    if (dueAmount <= 0)
                        continue;

                    var payment = new CustomerPayment
                    {
                        ClubCustomerId = player.ClubCustomerId.Value,
                        TableSessionId = session.TableSessionId,
                        TotalAmount = netAmount,
                        CashAmount = player.CashAmount,
                        CardAmount = player.CardAmount,
                        PaidAmount = player.CashAmount + player.CardAmount,
                        DiscountAmount = player.DiscountAmount,
                        DueAmount = dueAmount,
                        PaymentStatus = player.PaidAmount > 0 ? "Partial" : "Unpaid",
                        PaymentType = "Session",
                        IsActive = true,
                        IsDeleted = false,
                        CreatedOn = DateTime.Now
                    };

                    _uowPayment.Repository.Insert(payment);
                }
            }
            else if (session.ClubCustomerId != null && session.ClubCustomerId.Value > 0 && session.DueAmount > 0)
            {
                var customerExists = await _uowCustomer.Repository.GetALL(x =>
                        x.ClubCustomerId == session.ClubCustomerId.Value)
                    .AnyAsync();

                if (customerExists)
                {
                    var payment = new CustomerPayment
                    {
                        ClubCustomerId = session.ClubCustomerId.Value,
                        TableSessionId = session.TableSessionId,
                        TotalAmount = session.NetAmount,
                        PaidAmount = session.PaidAmount,
                        DiscountAmount = session.DiscountAmount,
                        DueAmount = session.DueAmount,
                        PaymentStatus = session.PaymentStatus,
                        PaymentType = "Session",
                        IsActive = true,
                        IsDeleted = false,
                        CreatedOn = DateTime.Now
                    };

                    _uowPayment.Repository.Insert(payment);
                }
            }

            await _uowPayment.Save();
            await _uowSession.Save();

            return session;
        }

        public async Task<List<TableSession>> GetRunningTableSessions()
        {
            var _context = new UnitOfWork<TableSession>(_uowInvoiceMaster.GetDbContext());

            var list = await _context.Repository.GetALL(x =>
                    x.Status == "Running" &&
                    x.IsActive == true &&
                    x.IsDeleted == false)
                .Include(x => x.TableSessionPlayers)
                .Include(x => x.TableSessionInventoryItems)
                .Include(x => x.TableSessionGames)
                .OrderBy(x => x.TableNo)
                .ToListAsync();

            return list;
        }

        public async Task<List<object>> GetTableSessionHistory(DateTime? fromDate, DateTime? toDate)
        {
            var context = _uowInvoiceMaster.GetDbContext();

            var sessionQuery = context.Set<TableSession>()
                .Where(x => x.Status == "Completed" && x.IsDeleted == false);

            var saleQuery = context.Set<InventorySale>()
                .Where(x => x.IsDeleted == false);

            if (fromDate.HasValue)
            {
                var startDate = fromDate.Value.Date;
                sessionQuery = sessionQuery.Where(x => x.EndTime >= startDate);
                saleQuery = saleQuery.Where(x => x.CreatedOn >= startDate);
            }

            if (toDate.HasValue)
            {
                var endDate = toDate.Value.Date.AddDays(1).AddTicks(-1);
                sessionQuery = sessionQuery.Where(x => x.EndTime <= endDate);
                saleQuery = saleQuery.Where(x => x.CreatedOn <= endDate);
            }

            var sessions = await sessionQuery
                .Include(x => x.TableSessionPlayers)
                .Include(x => x.TableSessionInventoryItems)
                .Include(x => x.TableSessionGames)
                .OrderByDescending(x => x.EndTime)
                .ToListAsync();

            var sales = await saleQuery
                .Include(x => x.InventorySaleItems)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();

            var result = new List<object>();

            foreach (var session in sessions)
            {
                var payments = await context.Set<CustomerPayment>()
                    .Where(x =>
                        x.TableSessionId == session.TableSessionId &&
                        x.IsActive == true &&
                        x.IsDeleted == false)
                    .ToListAsync();

                result.Add(new
                {
                    TableSessionId = session.TableSessionId,
                    InventorySaleId = (int?)null,
                    PaymentType = "Session",

                    session.ReceiptNo,
                    session.TableNo,
                    session.TableName,
                    session.TableType,
                    session.CustomerName,
                    session.StartTime,
                    session.EndTime,

                    GameCount = session.TableSessionGames?.Count ?? 0,

                    session.TableAmount,
                    session.InventoryAmount,
                    session.DiscountAmount,
                    session.NetAmount,

                    CashAmount = payments.Sum(x => x.CashAmount),
                    CardAmount = payments.Sum(x => x.CardAmount),
                    PaidAmount = payments.Any() ? payments.Sum(x => x.PaidAmount) : session.PaidAmount,
                    DueAmount = payments.Any() ? payments.Sum(x => x.DueAmount) : session.DueAmount,

                    session.Status,
                    session.CreatedOn,

                    TableSessionPlayers = session.TableSessionPlayers.Select(p => new
                    {
                        p.PlayerName,
                        p.PhoneNo,
                        p.ClubCustomerId
                    }).ToList(),

                    InventoryItems = session.TableSessionInventoryItems.Select(i => new
                    {
                        Name = i.ItemName,
                        i.Price,
                        i.Quantity,
                        i.BuyerName
                    }).ToList()
                });
            }

            foreach (var sale in sales)
            {
                result.Add(new
                {
                    TableSessionId = (int?)null,
                    InventorySaleId = (int?)sale.InventorySaleId,
                    PaymentType = "InventorySale",

                    sale.ReceiptNo,
                    TableNo = 0,
                    TableName = "Counter Sale",
                    TableType = "Inventory",
                    sale.CustomerName,
                    StartTime = (DateTime?)null,
                    EndTime = (DateTime?)sale.CreatedOn,

                    GameCount = 0,

                    TableAmount = 0m,
                    InventoryAmount = sale.TotalAmount,
                    sale.DiscountAmount,
                    sale.NetAmount,
                    sale.CashAmount,
                    sale.CardAmount,
                    sale.PaidAmount,
                    sale.DueAmount,

                    Status = sale.PaymentStatus,
                    sale.CreatedOn,

                    TableSessionPlayers = new List<object>(),

                    InventoryItems = sale.InventorySaleItems.Select(i => new
                    {
                        Name = i.ItemName,
                        i.Price,
                        i.Quantity,
                        BuyerName = sale.CustomerName
                    }).ToList()
                });
            }

            return result
                .OrderByDescending(x => ((dynamic)x).CreatedOn)
                .ToList();
        }

        public async Task<bool> CancelTableSession(int tableSessionId)
        {
            var _context = new UnitOfWork<TableSession>(_uowInvoiceMaster.GetDbContext());

            var session = await _context.Repository.GetALL(x =>
                    x.TableSessionId == tableSessionId)
                .FirstOrDefaultAsync();

            if (session == null)
                throw new Exception("Session not found.");

            session.Status = "Cancelled";
            session.UpdatedOn = DateTime.Now;

            await _context.Save();

            return true;
        }




        public async Task<ClubCustomer> UpdateClubCustomer(UpdateClubCustomerDto input)
        {
            var context = new UnitOfWork<ClubCustomer>(_uowInvoiceMaster.GetDbContext());

            var customer = await context.Repository
                .GetALL(x => x.ClubCustomerId == input.ClubCustomerId)
                .FirstOrDefaultAsync();

            if (customer == null)
                throw new Exception("Customer not found");

            customer.CustomerName = input.CustomerName;
            customer.PhoneNo = input.PhoneNo;
            customer.UpdatedOn = DateTime.Now;

            context.Repository.Update(customer);
            await context.Save();

            return customer;
        }

        public async Task<bool> DeleteClubCustomer(int id)
        {
            if (id <= 0)
                throw new Exception("Invalid customer id");

            var context = new UnitOfWork<ClubCustomer>(_uowInvoiceMaster.GetDbContext());

            var customer = await context.Repository
                .GetALL(x => x.ClubCustomerId == id)
                .FirstOrDefaultAsync();

            if (customer == null)
                throw new Exception("Customer not found");

            customer.IsActive = false;
            customer.IsDeleted = true;
            customer.DeletedOn = DateTime.Now;

            context.Repository.Update(customer);
            await context.Save();

            return true;
        }



public async Task<List<object>> GetCustomerPendingPayments()
    {
        var context = _uowInvoiceMaster.GetDbContext();
        var connection = context.Database.GetDbConnection();

        var list = new List<object>();

        if (connection.State != ConnectionState.Open)
            await connection.OpenAsync();

        using var command = connection.CreateCommand();
        command.CommandText = "dbo.GetCustomerPendingPayments";
        command.CommandType = CommandType.StoredProcedure;

        using var reader = await command.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(new
            {
                ClubCustomerId = reader["ClubCustomerId"] == DBNull.Value ? 0 : Convert.ToInt32(reader["ClubCustomerId"]),
                CustomerName = reader["CustomerName"]?.ToString(),
                PhoneNo = reader["PhoneNo"]?.ToString(),
                DueAmount = reader["DueAmount"] == DBNull.Value ? 0 : Convert.ToDecimal(reader["DueAmount"]),
                PaymentCount = reader["PaymentCount"] == DBNull.Value ? 0 : Convert.ToInt32(reader["PaymentCount"]),
                LastPaymentDate = reader["LastPaymentDate"] == DBNull.Value ? null : reader["LastPaymentDate"]
            });
        }

        return list;
    }




        //public async Task<bool> PayCustomerPendingAmount(PayCustomerPendingAmountDto input)
        //    {
        //        using var uow = new UnitOfWork<ClubCustomerPayment>(_uowInvoiceMaster.GetDbContext());

        //        var payments = await uow.Repository.GetALL(x =>
        //                x.IsActive == true &&
        //                x.IsDeleted == false &&
        //                x.ClubCustomerId == input.ClubCustomerId &&
        //                x.DueAmount > 0)
        //            .OrderBy(x => x.CreatedOn)
        //            .ToListAsync();

        //        var remainingPaid = input.PaidAmount;

        //        foreach (var payment in payments)
        //        {
        //            if (remainingPaid <= 0)
        //                break;

        //            var payNow = Math.Min(payment.DueAmount, remainingPaid);
        //            payment.PaidAmount += payNow;
        //            payment.DueAmount -= payNow;
        //            payment.PaymentStatus = payment.DueAmount <= 0 ? "Paid" : "Partial";
        //            payment.UpdatedOn = DateTime.Now;

        //            remainingPaid -= payNow;
        //        }

        //        await uow.Save();
        //        return true;
        //    }






        public async Task<List<CustomerPendingPaymentHistoryDto>> GetCustomerPendingPaymentHistory(int clubCustomerId)
        {
            var context = _uowInvoiceMaster.GetDbContext();

            var payments = await context.Set<CustomerPayment>()
                .Where(x =>
                    x.IsActive == true &&
                    x.IsDeleted == false &&
                    x.ClubCustomerId == clubCustomerId &&
                    x.DueAmount > 0)
                .OrderByDescending(x => x.CreatedOn)
                .ToListAsync();

            var customer = await context.Set<ClubCustomer>()
                .FirstOrDefaultAsync(x => x.ClubCustomerId == clubCustomerId);

            var response = new List<CustomerPendingPaymentHistoryDto>();

            foreach (var payment in payments)
            {
                var inventoryItems = new List<PendingInventoryItemDto>();
                var players = new List<string>();
                var totalTime = "";
                decimal tableTimeAmount = 0;
                decimal inventoryAmount = 0;
                string? receiptNo = payment.ReceiptNo;

                if (payment.InventorySaleId != null && payment.InventorySaleId > 0)
                {
                    var sale = await context.Set<InventorySale>()
                        .FirstOrDefaultAsync(x => x.InventorySaleId == payment.InventorySaleId);

                    receiptNo = payment.ReceiptNo ?? sale?.ReceiptNo;

                    inventoryItems = await context.Set<InventorySaleItem>()
                        .Where(x => x.InventorySaleId == payment.InventorySaleId)
                        .Select(x => new PendingInventoryItemDto
                        {
                            Name = x.ItemName,
                            Price = x.Price,
                            Quantity = x.Quantity,
                            BuyerName = sale != null ? sale.CustomerName : customer!.CustomerName
                        })
                        .ToListAsync();

                    inventoryAmount = inventoryItems.Sum(x => x.Price * x.Quantity);
                    tableTimeAmount = 0;
                }
                else if (payment.TableSessionId != null && payment.TableSessionId > 0)
                {
                    var session = await context.Set<TableSession>()
                        .FirstOrDefaultAsync(x => x.TableSessionId == payment.TableSessionId);

                    receiptNo = payment.ReceiptNo ?? session?.ReceiptNo;

                    players = await context.Set<TableSessionPlayer>()
                        .Where(x => x.TableSessionId == payment.TableSessionId)
                        .Select(x => x.PlayerName)
                        .ToListAsync();

                    inventoryItems = await context.Set<TableSessionInventoryItem>()
                        .Where(x =>
                            x.TableSessionId == payment.TableSessionId &&
                            x.ClubCustomerId == payment.ClubCustomerId)
                        .Select(x => new PendingInventoryItemDto
                        {
                            Name = x.ItemName,
                            Price = x.Price,
                            Quantity = x.Quantity,
                            BuyerName = x.BuyerName
                        })
                        .ToListAsync();

                    inventoryAmount = inventoryItems.Sum(x => x.Price * x.Quantity);

                    if (session?.StartTime != null && session.EndTime != null)
                    {
                        var duration = session.EndTime.Value - session.StartTime;
                        totalTime = $"{(int)duration.TotalHours:00}:{duration.Minutes:00}:{duration.Seconds:00}";
                    }

                    tableTimeAmount = Math.Max(0, payment.TotalAmount - inventoryAmount);
                }

                response.Add(new CustomerPendingPaymentHistoryDto
                {
                    CustomerPaymentId = payment.CustomerPaymentId,
                    ClubCustomerId = payment.ClubCustomerId,
                    CustomerName = customer?.CustomerName,
                    PhoneNo = customer?.PhoneNo,
                    TableSessionId = payment.TableSessionId,
                    InventorySaleId = payment.InventorySaleId,
                    ReceiptNo = receiptNo,
                    Players = string.Join(" vs ", players),
                    TotalTime = totalTime,
                    TableTimeAmount = tableTimeAmount,
                    InventoryAmount = inventoryAmount,
                    InventoryItems = inventoryItems,
                    TotalAmount = payment.TotalAmount,
                    PaidAmount = payment.PaidAmount,
                    DiscountAmount = payment.DiscountAmount,
                    DueAmount = payment.DueAmount,
                    PaymentStatus = payment.PaymentStatus,
                    PaymentType = payment.PaymentType,
                    CreatedOn = payment.CreatedOn
                });
            }

            return response;
        }


        public async Task<bool> PayCustomerPendingAmount(PayCustomerPendingAmountDto input)
        {
            using var uow = new UnitOfWork<CustomerPayment>(_uowInvoiceMaster.GetDbContext());

            var payments = await uow.Repository.GetALL(x =>
                    x.IsActive == true &&
                    x.IsDeleted == false &&
                    x.ClubCustomerId == input.ClubCustomerId &&
                    x.DueAmount > 0)
                .OrderBy(x => x.CreatedOn)
                .ToListAsync();

            var remainingPaid = input.PaidAmount;

            foreach (var payment in payments)
            {
                if (remainingPaid <= 0)
                    break;

                var payNow = Math.Min(payment.DueAmount, remainingPaid);

                payment.PaidAmount += payNow;
                payment.DueAmount -= payNow;
                payment.PaymentStatus = payment.DueAmount <= 0 ? "Paid" : "Partial";

                remainingPaid -= payNow;
            }

            await uow.Save();
            return true;
        }



        public async Task<InventorySale> CreateInventorySale(CreateInventorySaleDto input)
        {
            var uowSale = new UnitOfWork<InventorySale>(_uowInvoiceMaster.GetDbContext());
            var uowSaleItem = new UnitOfWork<InventorySaleItem>(_uowInvoiceMaster.GetDbContext());
            var uowInventory = new UnitOfWork<InventoryItem>(_uowInvoiceMaster.GetDbContext());
            var uowPayment = new UnitOfWork<CustomerPayment>(_uowInvoiceMaster.GetDbContext());

            var items = input.Items != null && input.Items.Any()
                ? input.Items
                : new List<CreateInventorySaleItemDto>
                {
            new CreateInventorySaleItemDto
            {
                InventoryItemId = input.InventoryItemId,
                Price = input.Price,
                Quantity = input.Quantity
            }
                };

            var inventoryIds = items.Select(x => x.InventoryItemId).ToList();

            var inventoryItems = await uowInventory.Repository
                .GetALL(x => inventoryIds.Contains(x.InventoryItemId))
                .ToListAsync();

            foreach (var saleItem in items)
            {
                var inventory = inventoryItems.FirstOrDefault(x => x.InventoryItemId == saleItem.InventoryItemId);

                if (inventory == null)
                    throw new Exception("Inventory item not found.");

                if (inventory.StockQty < saleItem.Quantity)
                    throw new Exception($"{inventory.Name} stock is not enough.");

                inventory.StockQty -= saleItem.Quantity;
            }

            var totalAmount = items.Sum(x =>
            {
                var inventory = inventoryItems.First(y => y.InventoryItemId == x.InventoryItemId);
                return inventory.Price * x.Quantity;
            });

            var discountAmount = Math.Min(Math.Max(0, input.DiscountAmount), totalAmount);
            var cashAmount = Math.Max(0, input.CashAmount);
            var cardAmount = Math.Max(0, input.CardAmount);
            var paidAmount = cashAmount + cardAmount;
            var dueAmount = (totalAmount - discountAmount) - paidAmount;

            if (paidAmount > totalAmount - discountAmount)
                throw new Exception("Paid amount cannot be greater than net amount.");

            var first = items.First();
            var firstInventory = inventoryItems.First(x => x.InventoryItemId == first.InventoryItemId);

            var sale = new InventorySale
            {
                ReceiptNo = "CS-" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                ClubCustomerId = input.ClubCustomerId,
                CustomerName = input.CustomerName,
                PhoneNo = input.PhoneNo,

                InventoryItemId = first.InventoryItemId,
                ItemName = firstInventory.Name,
                Price = firstInventory.Price,
                Quantity = first.Quantity,

                TotalAmount = totalAmount,
                DiscountAmount = discountAmount,
                CashAmount = cashAmount,
                CardAmount = cardAmount,
                PaymentStatus = dueAmount <= 0 ? "Paid" : paidAmount > 0 ? "Partial" : "Unpaid",
                IsActive = true,
                IsDeleted = false,
                CreatedOn = DateTime.Now
            };

            uowSale.Repository.Insert(sale);
            await uowSale.Save();

            foreach (var saleItem in items)
            {
                var inventory = inventoryItems.First(x => x.InventoryItemId == saleItem.InventoryItemId);

                uowSaleItem.Repository.Insert(new InventorySaleItem
                {
                    InventorySaleId = sale.InventorySaleId,
                    InventoryItemId = inventory.InventoryItemId,
                    ItemName = inventory.Name,
                    Price = inventory.Price,
                    Quantity = saleItem.Quantity,
                    CreatedOn = DateTime.Now
                });
            }

            if (dueAmount > 0 && input.ClubCustomerId.HasValue)
            {
                uowPayment.Repository.Insert(new CustomerPayment
                {
                    ClubCustomerId = input.ClubCustomerId.Value,
                    InventorySaleId = sale.InventorySaleId,
                    ReceiptNo = sale.ReceiptNo,
                    TotalAmount = totalAmount - discountAmount,
                    PaidAmount = paidAmount,
                    DiscountAmount = discountAmount,
                    DueAmount = dueAmount,
                    CashAmount = cashAmount,
                    CardAmount = cardAmount,
                    PaymentStatus = sale.PaymentStatus,
                    PaymentType = "InventorySale",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedOn = DateTime.Now
                });
            }

            await uowSaleItem.Save();

            return sale;
        }





        public async Task<List<ClubTable>> GetClubTables()
        {
            var context = new UnitOfWork<ClubTable>(_uowInvoiceMaster.GetDbContext());

            var list = await context.Repository.GetALL(x => x.IsDeleted == false)
                .OrderBy(x => x.TableNo)
                .ToListAsync();

            return list;
        }

        public async Task<ClubTable> CreateClubTable(CreateClubTableDto input)
        {
            var context = new UnitOfWork<ClubTable>(_uowInvoiceMaster.GetDbContext());

            var table = new ClubTable
            {
                TableNo = input.TableNo,
                TableName = input.TableName,
                TableType = input.TableType,
                HourlyRate = input.HourlyRate,
                GameRate = input.GameRate,
                DoubleGameRate = input.DoubleGameRate,
                DoubleHourlyRate = input.DoubleHourlyRate,
                IsActive = input.IsActive,
                IsDeleted = false,
                CreatedOn = DateTime.Now
            };

            context.Repository.Insert(table);
            await context.Save();

            return table;
        }

        public async Task<ClubTable> UpdateClubTable(UpdateClubTableDto input)
        {
            var context = new UnitOfWork<ClubTable>(_uowInvoiceMaster.GetDbContext());

            var table = await context.Repository
                .GetALL(x => x.ClubTableId == input.ClubTableId && x.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (table == null)
                throw new Exception("Table not found");

            table.TableNo = input.TableNo;
            table.TableName = input.TableName;
            table.TableType = input.TableType;
            table.HourlyRate = input.HourlyRate;
            table.GameRate = input.GameRate;
            table.DoubleGameRate = input.DoubleGameRate;
            table.DoubleHourlyRate = input.DoubleHourlyRate;
            table.IsActive = input.IsActive;
            table.UpdatedOn = DateTime.Now;

            context.Repository.Update(table);
            await context.Save();

            return table;
        }

        public async Task<bool> DeleteClubTable(int id)
        {
            if (id <= 0)
                throw new Exception("Invalid table id");

            var context = new UnitOfWork<ClubTable>(_uowInvoiceMaster.GetDbContext());

            var table = await context.Repository
                .GetALL(x => x.ClubTableId == id && x.IsDeleted == false)
                .FirstOrDefaultAsync();

            if (table == null)
                throw new Exception("Table not found");

            table.IsActive = false;
            table.IsDeleted = true;
            table.DeletedOn = DateTime.Now;

            context.Repository.Update(table);
            await context.Save();

            return true;
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