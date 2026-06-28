using AppCommonMethods;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using HMIS.Patient.Domain.Models.DTO.PatientWorkFlowLogDto;
using HMIS.Patient.Domain.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HMIS.Patient.Service.Common;

namespace HMIS.Patient.Service
{
    public class PatientWorkFlowLogService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<PatientWorkFlowLog> _uowPatientWorkFlowLog;

        #endregion

        #region Constructor

        public PatientWorkFlowLogService(TokenService tokenService, UnitOfWork<PatientWorkFlowLog> uowPatientWorkFlowLog, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowPatientWorkFlowLog = uowPatientWorkFlowLog;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditPatientWorkFlowLogDto> CreateOrEdit(CreateOrEditPatientWorkFlowLogDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientWorkFlowLogId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditPatientWorkFlowLogDto> Create(CreateOrEditPatientWorkFlowLogDto input)
        {
            var obj = _mapper.Map<PatientWorkFlowLog>(input);
            FillEntity(obj);
            PatientWorkFlowLog responseObj = await _uowPatientWorkFlowLog.Repository.Insert(obj);
            await _uowPatientWorkFlowLog.CommitAsync();
            return _mapper.Map<CreateOrEditPatientWorkFlowLogDto>(responseObj);
        }

        private async Task<CreateOrEditPatientWorkFlowLogDto> Update(CreateOrEditPatientWorkFlowLogDto input)
        {
            var dbObj = await _uowPatientWorkFlowLog.Repository.GetById(input.PatientWorkFlowLogId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowPatientWorkFlowLog.Repository.Update(obj!);
            await _uowPatientWorkFlowLog.CommitAsync();

            return _mapper.Map<CreateOrEditPatientWorkFlowLogDto>(obj);
        }

        public async Task<CreateOrEditPatientWorkFlowLogDto> CreatePatientWorkLog(CreateOrEditPatientWorkFlowLogDto input)
        {
            var dbPrevStation = await _uowPatientWorkFlowLog.Repository.GetALL(x => x.PatientVisitId == input.PatientVisitId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(dbPrevStation))
                input.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(DateTime.Now, DateTime.Now);
            else
                input.TimeDifference = CommonMethods.DifferenceBetweenTwoDates(dbPrevStation!.CreatedOn, DateTime.Now);

            var obj = _mapper.Map<PatientWorkFlowLog>(input);
            FillEntity(obj);
            //return obj;
            PatientWorkFlowLog responseObj = await _uowPatientWorkFlowLog.Repository.Insert(obj);
            await _uowPatientWorkFlowLog.Save();
            return _mapper.Map<CreateOrEditPatientWorkFlowLogDto>(responseObj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowPatientWorkFlowLog.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowPatientWorkFlowLog.Repository.Update(dbObj!);
            await _uowPatientWorkFlowLog.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewPatientWorkFlowLogDto>> GetAll(Expression<Func<PatientWorkFlowLog, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>
            ? orderBy = null,
            string includeProperties = "")
        {
            List<PatientWorkFlowLog> responseObj = await _uowPatientWorkFlowLog.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewPatientWorkFlowLogDto>>(responseObj);
        }

        public async Task<ViewPatientWorkFlowLogDto> GetById(Guid input)
        {
            PatientWorkFlowLog? responseObj = await _uowPatientWorkFlowLog.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewPatientWorkFlowLogDto>(responseObj);
        }


        #endregion

        #region Helper Methods

        private void FillEntity(PatientWorkFlowLog obj)
        {
            if (obj.PatientWorkFlowLogId == Guid.Empty)
            {
                obj.PatientWorkFlowLogId = Guid.NewGuid();
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
        private void FillEntityDelete(PatientWorkFlowLog obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion
    }
}
