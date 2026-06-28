using AppCommonMethods;
using AuthBAL.Common;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.HealthFacilityDto;
using AuthDAL.Models.Dto.LabTestDetailDto;
using AuthDAL.Models.Dto.LabTestDto;
using AuthDAL.Repositories;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;

namespace AuthBAL
{
    public class LabTestDetailService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly IMapper _mapper;
        private UnitOfWork<LabTestDetail> _uowLabTestDetail;
        private readonly TokenService _tokenService;

        #endregion

        #region Constructor

        public LabTestDetailService(TokenService tokenService, UnitOfWork<LabTestDetail> uowLabTestDetail, IMapper mapper)
        {
            _mapper = mapper;
            _uowLabTestDetail = uowLabTestDetail;
            _tokenService = tokenService;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditLabTestDetailDto> CreateOrEdit(CreateOrEditLabTestDetailDto input)
        {
            if (AppCommonMethod.IsNullorZeroInt(input.LabTestId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditLabTestDetailDto> Create(CreateOrEditLabTestDetailDto input)
        {
            var obj = _mapper.Map<LabTestDetail>(input);
            FillEntity(obj);
            LabTestDetail responseObj = await _uowLabTestDetail.Repository.Insert(obj);
            await _uowLabTestDetail.CommitAsync();
            return _mapper.Map<CreateOrEditLabTestDetailDto>(responseObj);
        }

        private async Task<CreateOrEditLabTestDetailDto> Update(CreateOrEditLabTestDetailDto input)
        {
            var dbObj = await _uowLabTestDetail.Repository.GetById(input.LabTestDetailId);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowLabTestDetail.Repository.Update(obj!);
            await _uowLabTestDetail.CommitAsync();

            return _mapper.Map<CreateOrEditLabTestDetailDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var dbObj = await _uowLabTestDetail.Repository.GetById(Id);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowLabTestDetail.Repository.Update(dbObj!);
            await _uowLabTestDetail.CommitAsync();
            return true;
        }

        #endregion

        #region Read Operations

        public async Task<List<ViewLabTestDetailDto>> GetAll(Expression<Func<LabTestDetail, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<LabTestDetail> responseObj = await _uowLabTestDetail.Repository.GetALL(filter).OrderByDescending(x=>x.CreatedOn).ToListAsync();
            return _mapper.Map<List<ViewLabTestDetailDto>>(responseObj);
        }

        public async Task<ViewLabTestDetailDto> GetById(int input)
        {
            LabTestDetail? responseObj = await _uowLabTestDetail.Repository.GetById(input);
            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewLabTestDetailDto>(responseObj);
        }
        #endregion

        #region Helper Methods

        private void FillEntity(LabTestDetail obj)
        {
            if (AppCommonMethod.IsNullorZeroInt(obj.LabTestId))
            {
                //obj.HealthFacilityId = Guid.NewGuid();
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
        private void FillEntityDelete(LabTestDetail obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion

    }
}
