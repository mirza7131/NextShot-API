using AppCommonMethods;

using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using FileHandler;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PatientDocument;
using HMIS.Patient.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Service
{
    public class PatientDocumentService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<PatientDocument> _uowPatientDocument;
        private readonly UploadFiles _fileUploader;

        #endregion

        #region Constructor

        public PatientDocumentService(TokenService tokenService, UnitOfWork<PatientDocument> uowPatientDocument, IMapper mapper, UploadFiles fileUploader)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowPatientDocument = uowPatientDocument;
            _fileUploader = fileUploader;   
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditPatientDocumentDto> CreateOrEdit(CreateOrEditPatientDocumentDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientDocumentId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditPatientDocumentDto> Create(CreateOrEditPatientDocumentDto input)
        {
            input.Url = null;
            var obj = _mapper.Map<PatientDocument>(input);
            if (!string.IsNullOrEmpty(input.Base64))
                obj.Url = await _fileUploader.UploadFileToCDN(CommonStringConstant.PatientDocuments, input.Base64, _tokenService.GetAccessToken());

            FillEntity(obj);
            PatientDocument responseObj = await _uowPatientDocument.Repository.Insert(obj);
            await _uowPatientDocument.CommitAsync();
            return _mapper.Map<CreateOrEditPatientDocumentDto>(responseObj);
        }

        private async Task<CreateOrEditPatientDocumentDto> Update(CreateOrEditPatientDocumentDto input)
        {
            var dbObj = await _uowPatientDocument.Repository.GetById(input.PatientDocumentId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            input.Url = dbObj.Url;
            if (!string.IsNullOrEmpty(input.Base64))
            {
                if (input.Base64 != dbObj.Base64)
                    input.Url = await _fileUploader.UploadFileToCDN(CommonStringConstant.PatientDocuments, input.Base64, _tokenService.GetAccessToken());
            }
            else
                input.Url = null;

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowPatientDocument.Repository.Update(obj!);
            await _uowPatientDocument.CommitAsync();

            return _mapper.Map<CreateOrEditPatientDocumentDto>(obj);
        }


        public async Task<List<CreateOrEditPatientDocumentDto>> UpdateAndInsert(List<CreateOrEditPatientDocumentDto> input)
        {
            var _dbContext = _uowPatientDocument.GetDbContext();

            var dbObj = await _dbContext.PatientDocuments.Where(x => x.PatientVisitId == input[0].PatientVisitId!).ToListAsync();

            //if (AppCommonMethod.IsNullOrEmptyList(dbObj))
            //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            foreach (var document in dbObj.ToList())
            {
                if (!input.Any(c => c.PatientDocumentId == document.PatientDocumentId))
                {
                    FillEntityDelete(document);
                    _dbContext.Update(document);
                }
            }

            // Update and Insert Event Locations
            foreach (var document in input.ToList())
            {
                var dbDocument = dbObj
                    .Where(c => c.PatientDocumentId == document.PatientDocumentId && c.PatientDocumentId != default(Guid))
                    .SingleOrDefault();

                if (dbDocument != null)
                {
                    document.Url = dbDocument.Url;
                    if (!string.IsNullOrEmpty(document.Base64))
                    {
                        if (document.Base64 != dbDocument.Base64)
                            document.Url = await _fileUploader.UploadFileToCDN(CommonStringConstant.PatientDocuments, document.Base64, _tokenService.GetAccessToken());
                    }
                    else
                        document.Url = null;

                    var objDocument = _mapper.Map(document, dbDocument);
                    FillEntity(objDocument);
                    if (!AppCommonMethod.IsNullorZeroInt(document.Status) && document.Status != dbDocument.Status)
                    {
                        dbDocument.Status = document.Status;
                        if (document.Status == (int)SSCStatus.Claim_Rejected)
                            dbDocument.StatusReason = document.StatusReason;
                        else
                            dbDocument.StatusReason = null;
                        dbDocument.StatusUpdatedOn = DateTime.Now;
                        dbDocument.StatusUpdatedBy = _tokenService.GetUserId();
                    }

                    _dbContext.Update(objDocument);
                    // Update child
                    //_dbContext.Entry(eventLocation).CurrentValues.SetValues(eventLocationId);
                }
                else
                {
                    // Insert child
                    //var dbDocument = new PatientDocument();
                    document.Url = null;
                    var objDocument = _mapper.Map<PatientDocument>(document);
                    if (!string.IsNullOrEmpty(document.Base64))
                        objDocument.Url = await _fileUploader.UploadFileToCDN(CommonStringConstant.PatientDocuments, document.Base64, _tokenService.GetAccessToken());

                    FillEntity(objDocument);
                    objDocument.Status = (int)SscDocumentStatus.Pending;
                    objDocument.UpdatedBy = _tokenService.GetUserId();
                    objDocument.UpdatedOn = DateTime.Now;

                    dbObj.Add(objDocument);
                    _dbContext.Add(objDocument);
                }
            }

            //_dbContext.UpdateRange(dbObj);
            //_uowPatientDocument.Repository.Update(dbObj!);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<List<CreateOrEditPatientDocumentDto>>(dbObj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowPatientDocument.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowPatientDocument.Repository.Update(dbObj!);
            await _uowPatientDocument.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewPatientDocumentDto>> GetAll(Expression<Func<PatientDocument, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>
            ? orderBy = null,
            string includeProperties = "")
        {
            List<PatientDocument> responseObj = await _uowPatientDocument.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewPatientDocumentDto>>(responseObj);
        }

        public async Task<ViewPatientDocumentDto> GetById(Guid input)
        {
            PatientDocument? responseObj = await _uowPatientDocument.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewPatientDocumentDto>(responseObj);
        }

        public async Task<List<ViewPatientDocumentDto>> GetByVisitId(Guid input)
        {
            List<PatientDocument>? responseObj = await _uowPatientDocument.Repository.GetALL(x => x.PatientVisitId == input && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                .OrderBy(x => x.CreatedOn)
                .ToListAsync();

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<List<ViewPatientDocumentDto>>(responseObj);
        }


        #endregion

        #region Helper Methods

        private void FillEntity(PatientDocument obj)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(obj.PatientDocumentId))
            {
                obj.PatientDocumentId = Guid.NewGuid();
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
        private void FillEntityDelete(PatientDocument obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion
    }
}
