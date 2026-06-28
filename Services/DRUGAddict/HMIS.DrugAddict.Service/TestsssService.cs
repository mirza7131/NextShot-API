using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.DrugAddict.Domain.Models.DbModels;
using HMIS.DrugAddict.Domain.Models.Dto.PaginationDto;
using HMIS.DrugAddict.Domain.Models.Dto.TestDto;
using HMIS.DrugAddict.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.DrugAddict.Service
{
    public class TestsssService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<Testsss> _uowTestsss;

        #endregion

        #region Constructor

        public TestsssService(TokenService tokenService, UnitOfWork<Testsss> uowProfileType, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowTestsss = uowProfileType;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditTestsssDto> CreateOrEdit(CreateOrEditTestsssDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.Id))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditTestsssDto> Create(CreateOrEditTestsssDto input)
        {
            var obj = _mapper.Map<Testsss>(input);
            FillEntity(obj);
            Testsss responseObj = await _uowTestsss.Repository.Insert(obj);
            await _uowTestsss.CommitAsync();
            return _mapper.Map<CreateOrEditTestsssDto>(responseObj);
        }

        private async Task<CreateOrEditTestsssDto> Update(CreateOrEditTestsssDto input)
        {
            var dbObj = await _uowTestsss.Repository.GetById(input.Id!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowTestsss.Repository.Update(obj!);
            await _uowTestsss.CommitAsync();

            return _mapper.Map<CreateOrEditTestsssDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowTestsss.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowTestsss.Repository.Update(dbObj!);
            await _uowTestsss.CommitAsync();
            return true;
        }


        #endregion

        #region Read Operations

        public async Task<List<ViewTestssDto>> GetAll(Expression<Func<Testsss, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>
            ? orderBy = null,
            string includeProperties = "")
        {
            List<Testsss> responseObj = await _uowTestsss.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewTestssDto>>(responseObj);
        }


        public async Task<ViewPagerDto<ViewTestssDto>> GetAllWithPagination(FilterTestssDto filter)
        {
            var list = _uowTestsss.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.Name.ToLower().StartsWith(filter.SearchString))
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<ViewTestssDto> IQueryableList = list.Select(x =>
                new ViewTestssDto
                {
                    Id = x.Id,
                    Name = x.Name,
                   
                });

            var pagedList = await PagedListDto<ViewTestssDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            var responseObject = new ViewPagerDto<ViewTestssDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = _mapper.Map<List<ViewTestssDto>>(pagedList)
            };

            return responseObject;
        }

        public async Task<ViewTestssDto> GetById(Guid input)
        {
            
            Testsss? responseObj = await _uowTestsss.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewTestssDto>(responseObj);
        }


        #endregion

        #region Helper Methods

        private void FillEntity(Testsss obj)
        {
            if (obj.Id == Guid.Empty)
            {
                obj.Id = Guid.NewGuid();
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
        private void FillEntityDelete(Testsss obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion

    }
}
