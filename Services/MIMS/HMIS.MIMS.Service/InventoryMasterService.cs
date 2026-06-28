using HMIS.MIMS.Domain.Models.DbModels;
using HMIS.MIMS.Domain.Models.DTO.MimsLookupsDto;
using HMIS.MIMS.Domain.Models.Repositories._UOW;
using JWTAuthentication;
using autoMapper = AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonDTOs.Enums;
using Microsoft.EntityFrameworkCore;
using HMIS.MIMS.Domain.Models.DTO.InventoryMasterDto;

namespace HMIS.MIMS.Service
{
    public class InventoryMasterService<TEntity> where TEntity : class
    {
        private UnitOfWork<InventoryMaster> _uowInventoryMaster;
        private readonly TokenService _tokenService;
        private readonly autoMapper.IMapper _mapper;

        public InventoryMasterService(
            TokenService tokenService,
            UnitOfWork<InventoryMaster> uowInventoryMaster,
            autoMapper.IMapper mapper
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowInventoryMaster = uowInventoryMaster;
        }


        public async Task<List<ViewInventoryMasterDto>> GetAllMedicineOfHealthFacilityInInventory()
        {
            List<InventoryMaster> responseObj = await _uowInventoryMaster.Repository
            .GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.HealthfacilityId == TokenService.GetUserHfId())
            .OrderBy(x => x.MimsBranchId)
            .ThenBy(x => x.MedicineName)
            .ToListAsync();

            return _mapper.Map<List<ViewInventoryMasterDto>>(responseObj);
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
