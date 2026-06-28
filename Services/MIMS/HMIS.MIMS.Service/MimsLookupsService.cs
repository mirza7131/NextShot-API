using CommonDTOs.Enums;
using HMIS.MIMS.Domain.Models.DbModels;
using HMIS.MIMS.Domain.Models.DTO.MimsLookupsDto;
using JWTAuthentication;
using autoMapper = AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HMIS.MIMS.Domain.Models.Repositories._UOW;

namespace HMIS.MIMS.Service
{
    public class MimsLookupsService<TEntity> where TEntity : class
    {

        private UnitOfWork<MimsBranch> _uowMimsBranch;
        private readonly TokenService _tokenService;
        private readonly autoMapper.IMapper _mapper;

        public MimsLookupsService(
            TokenService tokenService,
            UnitOfWork<MimsBranch> uowMimsBranch,
            autoMapper.IMapper mapper
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowMimsBranch = uowMimsBranch;
        }


        public async Task<List<ViewMimsBranchDto>> GetAllMimsBranches()
        {
            List<MimsBranch> responseObj = await _uowMimsBranch.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.HealthfacilityId == TokenService.GetUserHfId()).ToListAsync();
            return _mapper.Map<List<ViewMimsBranchDto>>(responseObj);
        }

        //public async Task<List<ViewMimsBranchDto>> GetAll(Expression<Func<Profile, bool>>? filter = null,
        //    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        //    string includeProperties = "")
        //{
        //    List<Profile> responseObj = await _uowProfile.Repository.GetALL(filter).ToListAsync();
        //    return _mapper.Map<List<ViewProfileDto>>(responseObj);
        //}
    }
}
