using AppCommonMethods;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.AttachmentDto;
using AuthDAL.Models.Dto.FeatureDto;
using AuthDAL.Models.Dto.PaginationDto;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using FileHandler;
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
    public class FeatureService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<Feature> _uowFeature;
        private readonly UploadFiles _fileUploader;

        #endregion

        #region Constructor

        public FeatureService(TokenService tokenService, UnitOfWork<Feature> uowFeature, IMapper mapper, UploadFiles uploadFiles)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowFeature = uowFeature;
            _fileUploader = uploadFiles;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditFeatureDto> CreateOrEdit(CreateOrEditFeatureDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.FeatureId))
                return await Create(input);
            else
                return input;
        }

        private async Task<CreateOrEditFeatureDto> Create(CreateOrEditFeatureDto input)
        {
            var _uowAttachment = new UnitOfWork<Attachment>(_uowFeature.GetDbContext());
            var obj = _mapper.Map<Feature>(input);
            FillEntity(obj);


            foreach (var attachment in input.Attachments)
            {

                if (!string.IsNullOrEmpty(attachment.Base64))
                    attachment.ImageUrl = await _fileUploader.UploadFileToCDN(CommonStringConstant.FeatureAnnouncement, attachment.Base64, _tokenService.GetAccessToken()) ?? String.Empty;

                attachment.ParentId = obj.FeatureId;

                var objAttachment = _mapper.Map<Attachment>(attachment);
                FillEntityAttachment(objAttachment);
                await _uowAttachment.Repository.Insert(objAttachment);
            }

            //var objAttachments = _mapper.Map<List<Attachment>>(input.Attachments);


            Feature responseObj = await _uowFeature.Repository.Insert(obj);
            await _uowFeature.CommitAsync();
            //await _uowAttachment.CommitAsync();

            return _mapper.Map<CreateOrEditFeatureDto>(responseObj);
        }

        //private async Task<CreateOrEditFeatureDto> Update(CreateOrEditFeatureDto input)
        //{
        //    var dbObj = await _uowFeature.Repository.GetById(input.FeatureId!);

        //    if (AppCommonMethod.IsNullObject(dbObj))
        //        throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

        //    var obj = _mapper.Map(input, dbObj);
        //    FillEntity(obj!);

        //    _uowFeature.Repository.Update(obj!);
        //    await _uowFeature.CommitAsync();

        //    return _mapper.Map<CreateOrEditFeatureDto>(obj);
        //}

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowFeature.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowFeature.Repository.Update(dbObj!);
            await _uowFeature.CommitAsync();
            return true;
        }

        
        #endregion

        #region Read Operations

        public async Task<List<ViewFeatureDto>> GetAll(Expression<Func<Feature, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>
            ? orderBy = null,
            string includeProperties = "")
        {
            List<Feature> responseObj = await _uowFeature.Repository.GetALL(filter)
                .OrderBy(x => x.CreatedOn)
                .ToListAsync();
            return _mapper.Map<List<ViewFeatureDto>>(responseObj);
        }


        public async Task<ViewPagerDto<ViewFeatureDto>> GetAllWithPagination(FilterFeatureDto filter)
        {
            var list = _uowFeature.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.Title.ToLower().StartsWith(filter.SearchString))
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<ViewFeatureDto> IQueryableList = list.Select(x =>
                new ViewFeatureDto
                {
                    FeatureId = x.FeatureId,
                    Title = x.Title,
                    Description = x.Description,
                    CreatedOn = x.CreatedOn,
                });

            var pagedList = await PagedListDto<ViewFeatureDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewFeatureDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = _mapper.Map<List<ViewFeatureDto>>(pagedList)
            };

            return responseObject;
        }
        

        public async Task<ViewFeatureDto> GetById(Guid input)
        {
            Feature? responseObj = await _uowFeature.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewFeatureDto>(responseObj);
        }


        #endregion

        #region Helper Methods

        private void FillEntity(Feature obj)
        {
            if (obj.FeatureId == Guid.Empty)
            {
                obj.FeatureId = Guid.NewGuid();
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
        private void FillEntityAttachment(Attachment obj)
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

        private void FillEntityDelete(Feature obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion
    }
}
