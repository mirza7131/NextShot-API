using AppCommonMethods;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.OfflineVersionLogDto;
using AuthDAL.Models.Dto.PaginationDto;
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
    public class OfflineVersionLogService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<OfflineVersionLog> _uowOfflineVersionLog;

        #endregion

        #region Constructor

        public OfflineVersionLogService(TokenService tokenService, UnitOfWork<OfflineVersionLog> uowOfflineVersionLog, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowOfflineVersionLog = uowOfflineVersionLog;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditOfflineVersionLogDto> CreateOrEdit(CreateOrEditOfflineVersionLogDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.OfflineVersionLogId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditOfflineVersionLogDto> Create(CreateOrEditOfflineVersionLogDto input)
        {
            var obj = _mapper.Map<OfflineVersionLog>(input);
            FillEntity(obj);
            OfflineVersionLog responseObj = await _uowOfflineVersionLog.Repository.Insert(obj);
            await _uowOfflineVersionLog.CommitAsync();
            return _mapper.Map<CreateOrEditOfflineVersionLogDto>(responseObj);
        }

        private async Task<CreateOrEditOfflineVersionLogDto> Update(CreateOrEditOfflineVersionLogDto input)
        {
            var dbObj = await _uowOfflineVersionLog.Repository.GetById(input.OfflineVersionLogId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowOfflineVersionLog.Repository.Update(obj!);
            await _uowOfflineVersionLog.CommitAsync();

            return _mapper.Map<CreateOrEditOfflineVersionLogDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowOfflineVersionLog.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowOfflineVersionLog.Repository.Update(dbObj!);
            await _uowOfflineVersionLog.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewOfflineVersionLogDto>> GetAll(Expression<Func<OfflineVersionLog, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>
            ? orderBy = null,
            string includeProperties = "")
        {
            List<OfflineVersionLog> responseObj = await _uowOfflineVersionLog.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewOfflineVersionLogDto>>(responseObj);
        }

        public async Task<ViewPagerDto<ViewOfflineVersionLogDto>> GetAllWithPagination(OfflineVersionLogFilterDto filter)
        {
            var list = _uowOfflineVersionLog.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.HealthFacility.Name.ToLower().StartsWith(filter.SearchString))
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<ViewOfflineVersionLogDto> IQueryableList = list.Select(x =>
                new ViewOfflineVersionLogDto
                {
                    OfflineVersionLogId = x.OfflineVersionLogId,
                    HealthFacilityId = x.HealthFacilityId,
                    Name = x.HealthFacility.Name,
                    ProjectProfileId = x.ProjectProfileId,
                    VersionNumber = x.VersionNumber,
                    ReleaseDate = x.ReleaseDate,
                    CreatedOn = x.CreatedOn,
                });
            var pagedList = await PagedListDto<ViewOfflineVersionLogDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewOfflineVersionLogDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = _mapper.Map<List<ViewOfflineVersionLogDto>>(pagedList)
            };

            return responseObject;
        }


        public async Task<ViewOfflineVersionLogDto> GetById(Guid input)
        {
            OfflineVersionLog? responseObj = await _uowOfflineVersionLog.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewOfflineVersionLogDto>(responseObj);
        }

        public async Task<ViewOfflineVersionLogDto> GetByHfId(int input)
        {
            OfflineVersionLog result = await _uowOfflineVersionLog.Repository
                .GetALL()
                .FirstOrDefaultAsync(offlineVersionLog => offlineVersionLog.HealthFacilityId == input);

            if(result == null)
            {
                return null;
            }

            return _mapper.Map<ViewOfflineVersionLogDto>(result);
        }



        #endregion

        #region Helper Methods

        private void FillEntity(OfflineVersionLog obj)
        {
            if (obj.OfflineVersionLogId == Guid.Empty)
            {
                obj.OfflineVersionLogId = Guid.NewGuid();
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
        private void FillEntityDelete(OfflineVersionLog obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion
    }
}
