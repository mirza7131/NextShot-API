using AppCommonMethods;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.AttachmentDto;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuthBAL
{
    public class AttachmentService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<Attachment> _uowAttachment;

        #endregion

        #region Constructor

        public AttachmentService(TokenService tokenService, UnitOfWork<Attachment> uowAttachment, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowAttachment = uowAttachment;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditAttachmentDto> CreateOrEdit(CreateOrEditAttachmentDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.AttachmentId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditAttachmentDto> Create(CreateOrEditAttachmentDto input)
        {
            var obj = _mapper.Map<Attachment>(input);
            FillEntity(obj);
            Attachment responseObj = await _uowAttachment.Repository.Insert(obj);
            await _uowAttachment.CommitAsync();
            return _mapper.Map<CreateOrEditAttachmentDto>(responseObj);
        }

        private async Task<CreateOrEditAttachmentDto> Update(CreateOrEditAttachmentDto input)
        {
            var dbObj = await _uowAttachment.Repository.GetById(input.AttachmentId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowAttachment.Repository.Update(obj!);
            await _uowAttachment.CommitAsync();

            return _mapper.Map<CreateOrEditAttachmentDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowAttachment.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowAttachment.Repository.Update(dbObj!);
            await _uowAttachment.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewAttachmentDto>> GetAll(Expression<Func<Attachment, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>
            ? orderBy = null,
            string includeProperties = "")
        {
            List<Attachment> responseObj = await _uowAttachment.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewAttachmentDto>>(responseObj);
        }


        public async Task<ViewAttachmentDto> GetById(Guid input)
        {
            Attachment? responseObj = await _uowAttachment.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewAttachmentDto>(responseObj);
        }

        public async Task<List<ViewAttachmentDto>> GetByParentId(Guid input,Expression<Func<Attachment, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>
            ? orderBy = null,
            string includeProperties = "")
        {
            List<Attachment> responseObj = await _uowAttachment.Repository.GetALL(filter).ToListAsync();
            List<Attachment> result = responseObj.FindAll(delegate (Attachment attachment)
            {
                return attachment.ParentId == input;
            });
            return _mapper.Map<List<ViewAttachmentDto>>(result);
        }

        #endregion

        #region Helper Methods

        private void FillEntity(Attachment obj)
        {
            if (obj.AttachmentId == Guid.Empty)
            {
                obj.AttachmentId = Guid.NewGuid();
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
        private void FillEntityDelete(Attachment obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion
    }
}
