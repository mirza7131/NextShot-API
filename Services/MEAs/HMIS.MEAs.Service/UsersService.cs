using HMIS.MEAs.Domain.Models.DbModels;
using DbRegion = HMIS.MEAs.Domain.Models.DbModels.Region;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMIS.MEAs.Domain.Models.DTO.Common;
using HMIS.MEAs.Domain.Repositories._UOW;
using AppCommonMethods;
using HMIS.MEAs.Domain.Models.DTO.UsersModel;
using Microsoft.EntityFrameworkCore;
using System.Net;
using AutoMapper;
using JWTAuthentication;
using HMIS.Aggregator.API.Models;
using Microsoft.EntityFrameworkCore.Storage;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using Microsoft.Win32;
using System.Drawing;

using HMIS.MEAs.Domain.Models.DTO;



namespace HMIS.MEAs.Service
{
    public class UsersService
    {
        #region Class Fields & Propertities

        private readonly JWTAuthentication.TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<User> _User;
        private readonly UnitOfWork<UserLocation> _UserLocation;
        private readonly UnitOfWork<UserRole> _UserRole;
        private readonly UnitOfWork<UserType> _UserType;
        private readonly UnitOfWork<DbRegion> _regions;
        private readonly UnitOfWork<DivisionView> _division;
        private readonly UnitOfWork<DistrictView> _district;
        private readonly UnitOfWork<TehsilView> _tehsil;
        private readonly UnitOfWork<Role> _role;
        private readonly UnitOfWork<Zone> _zone;

        #endregion

        #region Constructor
        public UsersService(
            JWTAuthentication.TokenService tokenService, 
            UnitOfWork<User> user,
            UnitOfWork<UserLocation> userLocation,
            UnitOfWork<UserRole> userRole,
            UnitOfWork<UserType> userType,
            UnitOfWork<DbRegion> regions,
            UnitOfWork<Role> role,
            UnitOfWork<DivisionView> division,
            UnitOfWork<DistrictView> district,
            UnitOfWork<TehsilView> tehsil,
            UnitOfWork<Zone> zone,
            IMapper mapper)
        {
            _tokenService = tokenService;
            _User = user;
            _UserLocation = userLocation;
            _UserRole = userRole;
            _UserType = userType;
            _mapper = mapper;
            _regions = regions;
            _division = division;
            _district = district;
            _tehsil = tehsil;
            _role = role;
            _zone = zone;
        }
        #endregion

        #region CUD Opertations

        public async Task<RegisterDTO> CreateOrEdit(RegisterDTO register)
        {
            if (AppCommonMethod.IsNullIntId(register.UserId))
                return await Create(register);
            else
                return await Update(register);
        }
        public async Task<RegisterDTO> Create(RegisterDTO register)
        {
            var _uowUser = new UnitOfWork<User>(_User.GetDbContext());
            register.LocationCode = !string.IsNullOrEmpty(register.TehsilCode) ? register.TehsilCode :
                                    (!string.IsNullOrEmpty(register.DistrictCode) ? register.DistrictCode :
                                    (!string.IsNullOrEmpty(register.DivisionCode) ? register.DivisionCode : ""));
            var userEntity = _mapper.Map<User>(register);

            if (register.UserLocations != null && register.UserLocations.Any())
            {
                foreach (var location in register.UserLocations)
                {
                    location.LocationCode = register.LocationCode;
                    
                }
            }
            if (register.UserRoles != null && register.UserRoles.Any())
            {
                foreach (var role in register.UserRoles)
                {
                    role.IsActive = true;  
                }
            }
            _mapper.Map(register, userEntity);
            FillEntity(userEntity);
          
            await _User.Repository.Insert(userEntity);
            await _User.Save();

            return _mapper.Map<RegisterDTO>(userEntity);
        }
        public async Task<RegisterDTO> Update(RegisterDTO register)
        {

            var userEntity = _User.Repository.GetALL(x => x.UserId == register.UserId)
                .Include(u => u.UserRoles)
                .Include(u => u.UserLocations)
                .FirstOrDefault();

            if (userEntity == null)
            {
                throw new UserFriendlyException($"{CommonMessageConstant.RecordNotFound}: UserId {register.UserId}");
            }
            register.LocationCode = !string.IsNullOrEmpty(register.TehsilCode) ? register.TehsilCode :
                                    (!string.IsNullOrEmpty(register.DistrictCode) ? register.DistrictCode :
                                    (!string.IsNullOrEmpty(register.DivisionCode) ? register.DivisionCode : ""));
            FillEntityUpdate(userEntity, register);
           

            if (register.LocationCode != null)
            {
                UpdateUserLocations(userEntity, register);
            }

            if (register.UserRoles.Count > 0)
            {
                UpdateUserRoles(userEntity, register);
            }

            FillEntity(userEntity);
            _User.Repository.Update(userEntity);

            await _User.CommitAsync();


            return _mapper.Map<RegisterDTO>(userEntity);
        }
        public async Task<bool> DeleteUser(int userId)
        {
            var dbObj = await _User.Repository.GetById(userId);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _User.Repository.Update(dbObj!);
            await _User.CommitAsync();
            
            return true;
        }
        #endregion

        #region Read Operations
        public async Task<List<User>> GetUserList()
        {
            return await _User.Repository.GetALL().ToListAsync();
        }
        public async Task<List<UserType>> GetUserTypeList()
        {
            return await _UserType.Repository.GetALL().ToListAsync();
        }
        public async Task<List<Role>> GetRoleList()
        {
            return await _role.Repository.GetALL().ToListAsync();
        }
        public async Task<List<DbRegion>> GetRegionList()
        {
            return await _regions.Repository.GetALL().ToListAsync();
        }
        public async Task<List<DivisionView>> GetDivisionList()
        {
            return await _division.Repository.GetALL().ToListAsync();
        }
        public async Task<List<DistrictView>> GetDistrictList(string val)
        {
            return await _district.Repository.GetALL(x => x.Code.Contains(val)).ToListAsync(); 
        }
        public async Task<List<TehsilView>> GetTehsilList(string val)
        {
            return await _tehsil.Repository.GetALL(x => x.Code.Contains(val)).ToListAsync();
        }
        public async Task<List<Zone>> GetZoneList(string val)
        {
            return await _zone.Repository.GetALL(x => x.TehsilCode == val).ToListAsync(); 
        }
        public async Task<RegisterDTO> GetUserById(int userId)
        {
            var userEntity = await _User.Repository.GetALL(x => x.UserId == userId)
                .Include(u => u.UserRoles)
                .Include(u => u.UserLocations)
                .FirstOrDefaultAsync();

            if (userEntity == null)
            {
                throw new UserFriendlyException($"{CommonMessageConstant.RecordNotFound}: UserId {userId}");
            }

            RegisterDTO user = _mapper.Map<RegisterDTO>(userEntity);
            if(user.LocationCode != null) {
                if (user.LocationCode.Length == 3)
                {
                    user.DivisionCode = user.LocationCode;
                }
                else if (user.LocationCode.Length == 6)
                {
                    user.DivisionCode = user.LocationCode.Substring(0, 3);
                    user.DistrictCode = user.LocationCode;
                }
                else if (user.LocationCode.Length == 9)
                {
                    user.DivisionCode = user.LocationCode.Substring(0, 3);
                    user.DistrictCode = user.LocationCode.Substring(0, 6);
                    user.TehsilCode = user.LocationCode;
                }
            }
            return user;
        }
        #endregion

        #region Helper
        private void FillEntity(User obj)
        {
            if (obj.UserId == 0) 
            {
               //obj.CreatedBy = _tokenService.GetUserIdForInt(); 
                obj.CreatedOn = DateTime.Now;
                obj.IsDelete = false;
            }
            else
            {
                //obj.UpdatedBy = _tokenService.GetUserIdForInt(); 
                obj.UpdatedOn = DateTime.Now;
            }
        }
        private void FillEntityDelete(User obj)
        {
            if (obj != null)
            {
                //obj.DeletedBy = _tokenService.GetUserId();
                obj.IsDelete = true;
                obj.IsActive = false;
            }
        }
        private void FillEntityUpdate(User userEntity, RegisterDTO register)
        {
            if (userEntity != null)
            {
                userEntity.ProvinceId = register.ProvinceId;
                userEntity.DepartmentName = register.DepartmentName;
                userEntity.Username = register.Username;
                userEntity.FullName = register.FullName;
                userEntity.ContactNo = register.ContactNo;
                userEntity.Cnic = register.Cnic;
                userEntity.DesignationId = register.DesignationId;
                userEntity.Designation = register.Designation;
                userEntity.Email = register.Email;
                userEntity.UserTypeId = register.UserTypeId;
                userEntity.ZoneId = register.ZoneId;
                userEntity.RegionId = register.RegionId;
            }
        }
        private void UpdateUserLocations(User userEntity, RegisterDTO register)
        {
            var newLocation = register.UserLocations.FirstOrDefault();
            if (newLocation != null)
            {
                newLocation.UserId = register.UserId;
                newLocation.LocationCode = register.LocationCode;
            }

            if (register.LocationCode != userEntity.LocationCode)
            {
                userEntity.LocationCode = register.LocationCode;

                var existingLocation = userEntity.UserLocations.FirstOrDefault();
                if (existingLocation != null)
                {
                    existingLocation.LocationCode = register.LocationCode;
                }
                else
                {
                    register.UserLocations.Add(new UserLocationDto
                    {
                        UserId = userEntity.UserId,
                        LocationCode = register.LocationCode
                    });
                }
            }

        }

        private void UpdateUserRoles(User userEntity, RegisterDTO register)
        {
            var existingRoles = userEntity.UserRoles.ToList();

            foreach (var role in existingRoles)
            {
                var newRoles = register.UserRoles.FirstOrDefault(x=>x.RoleId == role.RoleId);
                if (newRoles == null)
                {
                    role.IsActive = false;
                }
                else
                {
                    if (role.IsActive == false || role.IsActive == null)
                    {
                        role.IsActive = true;
                    }
                }
            }

            foreach (var newRole in register.UserRoles)
            {
                var existingRole = existingRoles.FirstOrDefault(x => x.RoleId == newRole.RoleId);
                if (existingRole == null)
                {
                    userEntity.UserRoles.Add(new UserRole
                    {
                        RoleId = newRole.RoleId,
                        UserId = userEntity.UserId,
                        IsActive = true
                    });
                }
            }
        }

        #endregion
    }


}

