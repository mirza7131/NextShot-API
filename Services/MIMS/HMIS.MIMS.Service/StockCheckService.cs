using HMIS.MIMS.Domain.Models.DbModels;
using JWTAuthentication;
using HMIS.MIMS.Domain.Models.Repositories._UOW;
using autoMapper = AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDTOs.Enums;
using HMIS.MIMS.Domain.Models.DTO.InventoryMasterDto;
using Microsoft.EntityFrameworkCore;
using HMIS.MIMS.Domain.Models.DTO.PaginationDto;
using Microsoft.AspNetCore.Mvc;
using AppCommonMethods;
using HMIS.MIMS.Domain.Models.DTO.IndentMasterDTO;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text.Json;
using HMIS.MIMS.Domain.Models.DTO.StockCheckDto;
using HMIS.MIMS.Domain.Models.DTO.InventoryDetailDto;
using HMIS.Aggregator.API.Models;

namespace HMIS.MIMS.Service
{
    public class StockCheckService<TEntity> where TEntity : class
    {
        private UnitOfWork<InventoryMaster> _uowInventoryMaster;
        private readonly TokenService _tokenService;
        private readonly autoMapper.IMapper _mapper;

        public StockCheckService(
            TokenService tokenService,
            UnitOfWork<InventoryMaster> uowInventoryMaster,
            autoMapper.IMapper mapper
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowInventoryMaster = uowInventoryMaster;
        }

        public async Task<List<StockCheckResponseDto>> GetStockByBrachOrByMedicine([FromQuery] StockCheckFilter filter)
        {
            using (var db = _uowInventoryMaster.GetDbContext())
            {
                var conn = db.Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[mims].[GetStockByBrachOrByMedicine]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(filter.MedicineId))
                        sqlComm.Parameters.AddWithValue("@MedicineId", filter.MedicineId);

                    if (!AppCommonMethod.IsNullOrEmptyGuid(filter.MimsBranchId))
                        sqlComm.Parameters.AddWithValue("@MimsBranchId", filter.MimsBranchId);

                    if (!AppCommonMethod.IsNullorZeroInt(TokenService.GetUserHfId()))
                        sqlComm.Parameters.AddWithValue("@HealthfacilityId", TokenService.GetUserHfId());

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;

                    await Task.Run(() => da.Fill(ds));

                    var res = ds.Tables[0].ToList<StockCheckResponseDto>();

                    return res;
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
        }

        //public async Task<List<ViewInventoryMasterSP_DTO>> GetInventoryDetailByBranchId(StockCheckboxCheckFilter? inventory)
        //{
        //    List<ViewInventoryMasterSP_DTO> lst = new List<ViewInventoryMasterSP_DTO>();
        //    var responseObject = new List<ViewInventoryMasterSP_DTO>();
        //    var a = TokenService.GetUserLoggedInfo();
        //    var conn = _uowInventoryMaster.GetDbContext().Database.GetDbConnection();
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        SqlCommand sqlComm = new SqlCommand("[mims].[GetInventoryDetailByBranchId]", (SqlConnection)conn);
        //        sqlComm.CommandType = CommandType.StoredProcedure;

        //        //sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString() : filter.StartDate.Value.ToString());// DateTime.Now.Date.ToString());
        //        //sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString() : filter.EndDate.Value.ToString());

        //        //sqlComm.Parameters.AddWithValue("@StartDate", filter.StartDate == null ? DateTime.Now.ToString("yyyy-MM-dd") : filter.StartDate.Value.ToString("yyyy-MM-dd"));
        //        //sqlComm.Parameters.AddWithValue("@EndDate", filter.EndDate == null ? DateTime.Now.ToString("yyyy-MM-dd") : filter.EndDate.Value.ToString("yyyy-MM-dd"));

        //        if (!AppCommonMethod.IsNullOrEmptyGuid(TokenService.GetUserLoggedInfo()?.MimsBranchId))
        //            sqlComm.Parameters.AddWithValue("@BranchId", TokenService.GetUserLoggedInfo()?.MimsBranchId);

        //        if (!AppCommonMethod.IsNullBool(inventory?.IsBatchWise))
        //            sqlComm.Parameters.AddWithValue("@IsBatchWise", inventory?.IsBatchWise);

        //        if (!AppCommonMethod.IsNullorZeroInt(inventory?.MedicineId))
        //            sqlComm.Parameters.AddWithValue("@MedicineId", inventory?.MedicineId);

        //        if (!AppCommonMethod.IsNullorEmptyDate(inventory?.NearestExpire))
        //            sqlComm.Parameters.AddWithValue("@NearestExpire", inventory?.NearestExpire);


        //        SqlDataAdapter da = new SqlDataAdapter();
        //        da.SelectCommand = sqlComm;
        //        await Task.Run(() => da.Fill(ds));

        //        var res = inventory.IsBatchWise == null;
        //        if ( == false)
        //            ds.Tables[0].ToList<ViewInventoryMasterSP_DTO>();
        //        else
        //            ds.Tables[0].ToList<ViewInventoryMasterSP_DTO>();

        //        //if(inventory?.IsBatchWise == true)
        //        //    res = ds.Tables[0].ToList<ViewInventoryDetailSP_DTO>();
        //        //else
        //        //    res = ds.Tables[0].ToList<ViewInventoryMasterSP_DTO>();


        //        ////var count = ds.Tables[0].ToList<ViewIndentMasterDetialListListTotalCount>();
        //        //lst = ds.Tables[0].ToList<ViewIndentMasterListByStatusFromSPDto>();

        //        //List<ViewInventoryDetailSP_DTO> res2 = ds.Tables[1].ToList<ViewInventoryDetailSP_DTO>();

        //        //responseObject.TotalCount = count.Select(x => x.TotalRecord).FirstOrDefault();
        //        //responseObject.PageSize = filter.PageSize;
        //        //responseObject.CurrentPage = filter.PageNumber;
        //        //responseObject.TotalPages = (int)Math.Ceiling(responseObject.TotalCount / (double)filter.PageSize);
        //        //responseObject.HasPrevious = filter.PageNumber > 1;
        //        //responseObject.HasNext = filter.PageNumber < responseObject.TotalPages;
        //        //responseObject.List = lst;

        //        //return responseObject;
        //        return res;

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //}

        public async Task<object> GetInventoryDetailByBranchId(StockCheckboxCheckFilter? inventory)
        {
            var conn = _uowInventoryMaster.GetDbContext().Database.GetDbConnection();
            try
            {
                var datenearexpire = inventory.NearestExpire?.AddHours(5);
                var dateexpire = inventory.IsExpired?.AddHours(5);

                DataSet ds = new DataSet();
                SqlCommand sqlComm = new SqlCommand("[mims].[GetInventoryDetailByBranchId]", (SqlConnection)conn);
                sqlComm.CommandType = CommandType.StoredProcedure;

                if (!AppCommonMethod.IsNullOrEmptyGuid(TokenService.GetUserLoggedInfo()?.MimsBranchId))
                    sqlComm.Parameters.AddWithValue("@BranchId", TokenService.GetUserLoggedInfo()?.MimsBranchId);

                if (!AppCommonMethod.IsNullBool(inventory?.IsBatchWise))
                    sqlComm.Parameters.AddWithValue("@IsBatchWise", inventory?.IsBatchWise);

                if (!AppCommonMethod.IsNullorZeroInt(inventory?.MedicineId))
                    sqlComm.Parameters.AddWithValue("@MedicineId", inventory?.MedicineId);

                if (!AppCommonMethod.IsNullorEmptyDate(inventory?.NearestExpire))
                    sqlComm.Parameters.AddWithValue("@NearestExpire", datenearexpire);

                if (!AppCommonMethod.IsNullorEmptyDate(inventory?.IsExpired))
                    sqlComm.Parameters.AddWithValue("@IsExpired", dateexpire);

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;
                await Task.Run(() => da.Fill(ds));

                if (inventory?.IsBatchWise == true)
                {
                    return ds.Tables[0].AsEnumerable()
                            .Select(row => new ViewInventoryDetailSP_DTO
                            {
                                InventoryMasterId = row.Field<Guid?>("InventoryMasterId"),
                                MedicineName = row["MedicineName"].ToString(),
                                BatchNo = row["BatchNo"].ToString(),
                                ExpDate = row.Field<DateTime?>("ExpDate"),
                                AvailableQty = row.Field<decimal?>("AvailableQty"),
                                IsSmlmedicine = row.Field<bool?>("IsSmlmedicine"),
                                UnitPrice = row.Field<decimal?>("UnitPrice")
                            })
                            .ToList();
                }
                else
                {
                    return ds.Tables[0].AsEnumerable()
                            .Select(row => new ViewInventoryMasterSP_DTO
                            {
                                InventoryMasterId = row.Field<Guid?>("InventoryMasterId"),
                                MedicineId = row.Field<int?>("MedicineId"),
                                MedicineName = row["MedicineName"].ToString(),
                                UnitPrice = row.Field<decimal?>("UnitPrice"),
                                AvailableQty = row.Field<decimal?>("AvailableQty"),
                                IsSmlmedicine = row.Field<bool?>("IsSmlmedicine")
                            })
                            .ToList();
                }
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



    }
}
