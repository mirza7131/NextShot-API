using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.HCP.Domain.Models.DbModels;
using HMIS.HCP.Domain.Models.DTO;
using HMIS.HCP.Domain.Repositories._UOW;
using HMIS.Patient.Domain.Models.DTO.NewFolder;
//using HMIS.Patient.Domain.Models.DTO.NewFolder;
using JWTAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HCP.Service
{
    public class PatientScreeningService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<PatientScreening> _uowPatientScreening;

        #endregion

        #region Constructor

        public PatientScreeningService(TokenService tokenService, UnitOfWork<PatientScreening> uowPatientScreening, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowPatientScreening = uowPatientScreening;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditPatientScreeningDto> CreateOrEdit(CreateOrEditPatientScreeningDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientScreeningId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditPatientScreeningDto> Create(CreateOrEditPatientScreeningDto input)
        {
            var obj = _mapper.Map<PatientScreening>(input);
            FillEntity(obj);
            PatientScreening responseObj = await _uowPatientScreening.Repository.Insert(obj);
            await _uowPatientScreening.CommitAsync();
            return _mapper.Map<CreateOrEditPatientScreeningDto>(responseObj);
        }

        private async Task<CreateOrEditPatientScreeningDto> Update(CreateOrEditPatientScreeningDto input)
        {
            var dbObj = await _uowPatientScreening.Repository.GetById(input.PatientScreeningId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            dbObj.IsPreviouslyDiagnosedHbv = input.IsPreviouslyDiagnosedHbv;
            dbObj.IsPreviouslyDiagnosedHcv = input.IsPreviouslyDiagnosedHcv;
            dbObj.HasHbvpcrconfirmation = input.HasHbvpcrconfirmation;
            dbObj.HasHcvpcrconfirmation = input.HasHcvpcrconfirmation;
            dbObj.PatientTypeProfileId = input.PatientTypeProfileId;
            dbObj.IsDiagnosedHbvrepidKit = input.IsDiagnosedHbvrepidKit;
            dbObj.IsDiagnosedHcvrepidKit = input.IsDiagnosedHcvrepidKit;
            dbObj.IsDialysisPatient = input.IsDialysisPatient;


            //var obj = _mapper.Map(input, dbObj);
            FillEntity(dbObj!);

            _uowPatientScreening.Repository.Update(dbObj!);
            await _uowPatientScreening.CommitAsync();

            return _mapper.Map<CreateOrEditPatientScreeningDto>(dbObj);
        }


        public async Task<CreateOrEditCallDto> CreateOrEditCallDetail(CreateOrEditCallDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.CallDetailId))
                return await CreateCallDetail(input);
            else
                return await UpdateCallDetail(input);
        }

        private async Task<CreateOrEditCallDto> CreateCallDetail(CreateOrEditCallDto input)
        {
            var _uowCallDetail = new UnitOfWork<CallDetail>(_uowPatientScreening.GetDbContext());
            var obj = _mapper.Map<CallDetail>(input);
            obj.CallDetailId = Guid.NewGuid();
            obj.CreatedOn = DateTime.Now;
            obj.CreatedBy = _tokenService.GetUserId();
            obj.ActionTypeId = 1;
            obj.IsActive = true;

            CallDetail responseObj = await _uowCallDetail.Repository.Insert(obj);
            await _uowCallDetail.CommitAsync();
            return _mapper.Map<CreateOrEditCallDto>(responseObj);
        }

        private async Task<CreateOrEditCallDto> UpdateCallDetail(CreateOrEditCallDto input)
        {
            var _uowCallDetail = new UnitOfWork<CallDetail>(_uowPatientScreening.GetDbContext());
            var dbObj = await _uowCallDetail.Repository.GetById(input.CallDetailId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            obj.UpdatedOn = DateTime.Now;
            obj.UpdatedBy = _tokenService.GetUserId();
            obj.ActionTypeId = 2;


            _uowCallDetail.Repository.Update(obj!);
            await _uowCallDetail.CommitAsync();

            return _mapper.Map<CreateOrEditCallDto>(obj);
        }
        public async Task<PatientScreening> GetPatientPreviousScreening(Guid input)
        {
            //PatientScreening? responseObj = await _uowPatientScreening.Repository.GetById(input);
            var responseObj = await _uowPatientScreening.Repository.GetALL(x => x.PatientId == input && x.IsActive == true && x.ActionTypeId != 3).OrderBy(x=>x.CreatedOn).LastOrDefaultAsync();

            //if (AppCommonMethod.IsNullObject(responseObj))
            //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<PatientScreening>(responseObj);
        }
        #endregion

        #region Read

        public async Task<List<GetCallDetailDto>> GetSinglePatientCallDetailRecord(Guid PatientId)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatientScreening.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[cc].[SpGetCallDetail]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@PatientId", PatientId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetCallDetailDto> lst = ds.Tables[0].ToList<GetCallDetailDto>();



                    return lst;
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
        
        public async Task<List<GetCallInfoDetailDto>> SpGetSinglePatientCallDetail(Guid PatientId)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatientScreening.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[cc].[SpGetSinglePatientCallDetail]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@PatientId", PatientId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<GetCallInfoDetailDto> lst = ds.Tables[0].ToList<GetCallInfoDetailDto>();



                    return lst;
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

        private void FillEntity(PatientScreening obj)
        {
            if (obj.PatientScreeningId == Guid.Empty)
            {
                obj.PatientScreeningId = Guid.NewGuid();
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
        private void FillEntityDelete(PatientScreening obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion
    }
}
