using AppCommonMethods;
using AppCommonMethods.AppConstants;
using AuthBAL;
using AuthBAL.Common;
using AuthDAL.Models.DbModels;
using dbModel = AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.HealthFacilityDto;
using AuthDAL.Models.Dto.PaginationDto;
using AuthDAL.Models.Dto.ProfileDto;
using AuthDAL.Models.Dto.SyncDataLogDto;
using AuthDAL.Models.Dto.UserDto;
using AuthDAL.Models.Dto.UserRole;
using AuthDAL.Repositories;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HealthFacilityBAL;
using HMIS.Aggregator.API.Models;
using HMIS.Aggregator.API.Services;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using static HMIS.Aggregator.API.Models.HrUserDto;
using Microsoft.Extensions.Configuration;

namespace UserBAL
{
    public class UserService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserRepository<User> _UserRepository;
        private UnitOfWork<User> _uowUser;
        private readonly HealthFacilityService<HealthFacility> _healthFacilityService;
        private readonly HRService _HRService;
        private readonly ProvinceService<Province> _ProvinceService;
        private readonly SyncDataLogService<SyncDataLog> _SyncDataLogService;
        private readonly string _hrTokneUserName;
        private readonly string _hrToknePassword;
        private readonly string _hrBaseUrl;
        #endregion

        #region Constructor

        public UserService(TokenService tokenService, UserRepository<User> UserRepository, UnitOfWork<User> uowUser, IMapper mapper, 
            HealthFacilityService<HealthFacility> healthFacilityServic,
            HRService hrService,
            ProvinceService<Province> ProvinceService,
            SyncDataLogService<SyncDataLog> SyncDataLogService,
            IConfiguration config
        )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _UserRepository = UserRepository;
            //_UserRoleRepository = UserRoleRepository;
            _uowUser = uowUser;
            _healthFacilityService = healthFacilityServic;
            _HRService = hrService;
            _ProvinceService = ProvinceService;
            _SyncDataLogService = SyncDataLogService;
            _hrTokneUserName = config.GetSection("HrTokenUserName").Value ?? string.Empty;
            _hrToknePassword = config.GetSection("HrTokenPassword").Value ?? string.Empty;
            _hrBaseUrl = config.GetSection("HrBaseUrl").Value ?? string.Empty;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditUserDto> CreateOrEdit(CreateOrEditUserDto input)
        {
            if (AppCommonMethods.AppCommonMethod.IsNullOrEmptyGuid(input.UserId))
                return await Create(input);
            else
                return await Update(input);
        }

        public async Task<CreateOrEditUserAssignableRolesDTO> CreateUserAssignableRoles(CreateOrEditUserAssignableRolesDTO input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.UserId))
                throw new UserFriendlyException(CommonMessageConstant.UsernameNotFound);


            var dbObj = await _uowUser.Repository.GetById(input.UserId);
            if (AppCommonMethod.IsNullObject(dbObj))
            {
                throw new UserFriendlyException(CommonMessageConstant.UsernameNotFound);
            }

            dbObj.IsShowRoleOnly = true;
            _uowUser.Repository.Update(dbObj);
            await _uowUser.CommitAsync();


            var _uowAssignableUserRoles = new UnitOfWork<UserAssignableRole>(_uowUser.GetDbContext());

            var userRoles = await _uowAssignableUserRoles.Repository.GetALL(x => x.UserId == input.UserId).ToListAsync();

            foreach (var userRolesItem in userRoles)
            {
                _uowAssignableUserRoles.Repository.Delete(userRolesItem);
                await _uowAssignableUserRoles.Save();
            }


            foreach (var item in input.UserAssignableRoles)
            {
                var obj = _mapper.Map<UserAssignableRole>(item);

                obj.AssignableUserRoleId = Guid.NewGuid();
                obj.UserId = dbObj.UserId;
                obj.RoleId = item.RoleId;
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
                await _uowAssignableUserRoles.Repository.Insert(obj);
                await _uowAssignableUserRoles.Save();
            }

            return _mapper.Map<CreateOrEditUserAssignableRolesDTO>(input);
        }

        private async Task<CreateOrEditUserDto> Create(CreateOrEditUserDto input)
        {
            input.Cnic = Regex.Replace(input.Cnic, @"[^0-9]", "");

            var user = await _uowUser.Repository.GetALL(x => x.Email == input.Email || x.Cnic == input.Cnic || x.ContactNo == input.ContactNo).FirstOrDefaultAsync();
            
            if (user != null)
            {
                if (user.Email == input.Email)
                    throw new UserFriendlyException(CommonMessageConstant.EmailAlreadyExists);

                if (user.Username == input.Cnic)
                    throw new UserFriendlyException(CommonMessageConstant.CNICAlreadyExists);

                if (user.ContactNo == input.ContactNo)
                    throw new UserFriendlyException(CommonMessageConstant.ContactNoAlreadyExists);
                else
                    throw new UserFriendlyException(CommonMessageConstant.UserAlreadyExists);

            }

            var obj = _mapper.Map<User>(input);
            obj.Username = input.Cnic;

            if (AppCommonMethod.IsNullorZeroInt(input.ProvinceId))
                obj.ProvinceId = null;

            if (AppCommonMethod.IsNullorZeroInt(input.DivisionId))
                obj.DivisionId = null;

            if (AppCommonMethod.IsNullorZeroInt(input.DistrictId))
                obj.DistrictId = null;

            if (AppCommonMethod.IsNullorZeroInt(input.TehsilId))
                obj.TehsilId = null;

            if (AppCommonMethod.IsNullorZeroInt(input.UcId))
                obj.UcId = null;

            if (AppCommonMethod.IsNullorZeroInt(input.HealthFacilityId))
                obj.HealthFacilityId = null;

            if (AppCommonMethod.IsNullorZeroInt(input.DepartmentId))
                obj.DepartmentId = null;

            if (AppCommonMethod.IsNullorZeroInt(input.SectionId))
                obj.SectionId = null;

            await FillEntity(obj);
            
            User responseObj = await _uowUser.Repository.Insert(obj);
            await _uowUser.Save();
            return _mapper.Map<CreateOrEditUserDto>(responseObj);
        }

        private async Task<CreateOrEditUserDto> Update(CreateOrEditUserDto input)
        {

            using (var trans = _uowUser.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    input.Cnic = Regex.Replace(input.Cnic, @"[^0-9]", "");

                    var _uowUserRole = new UnitOfWork<UserRole>(_uowUser.GetDbContext());

                    var userRoles = await _uowUserRole.Repository.GetALL(x => x.UserId == input.UserId).ToListAsync();

                    foreach (var userRolesItem in userRoles)
                    {
                        _uowUserRole.Repository.Delete(userRolesItem);
                        await _uowUserRole.Save();
                    }

                    var dbObj = await _uowUser.Repository.GetById(input.UserId);

                    if (!AppCommonMethod.IsNullorZeroInt(input.ProvinceId))
                        dbObj.ProvinceId = input.ProvinceId;
                    else
                        dbObj.ProvinceId = null;

                    if (!AppCommonMethod.IsNullorZeroInt(input.DivisionId))
                        dbObj.DivisionId = input.DivisionId;
                    else
                        dbObj.DivisionId = null;

                    if (!AppCommonMethod.IsNullorZeroInt(input.DistrictId))
                        dbObj.DistrictId = input.DistrictId;
                    else
                        dbObj.DistrictId = null;

                    if (!AppCommonMethod.IsNullorZeroInt(input.TehsilId))
                        dbObj.TehsilId = input.TehsilId;
                    else
                        dbObj.TehsilId = null;

                    if (!AppCommonMethod.IsNullorZeroInt(input.UcId))
                        dbObj.UcId = input.UcId;
                    else
                        dbObj.UcId = null;

                    if (!AppCommonMethod.IsNullorZeroInt(input.HealthFacilityId))
                        dbObj.HealthFacilityId = input.HealthFacilityId;
                    else
                        dbObj.HealthFacilityId = null;

                    if (!AppCommonMethod.IsNullorZeroInt(input.DepartmentId))
                        dbObj.DepartmentId = input.DepartmentId;
                    else
                        dbObj.DepartmentId = null;

                    if (!AppCommonMethod.IsNullorZeroInt(input.SectionId))
                        dbObj.SectionId = input.SectionId;
                    else
                        dbObj.SectionId = null;

                    dbObj.Username = input.Cnic;
                    dbObj.FullName = input.FullName;
                    dbObj.Email = input.Email;
                    dbObj.Password = input.Password;
                    dbObj.ContactNo = input.ContactNo;
                    dbObj.ProfilePic = input.ProfilePic;
                    dbObj.Cnic = input.Cnic;
                    dbObj.DesignationProfileId = input.DesignationProfileId;
                    dbObj.UserTypeProfileId = input.UserTypeProfileId;
                    dbObj.GenderProfileId = input.GenderProfileId;
                    dbObj.Dob = input.Dob;
                    dbObj.FatherName = input.FatherName;
                    

                    dbObj.IsActive = input.IsActive;
                    dbObj.UpdatedBy = _tokenService.GetUserId();
                    dbObj.UpdatedOn = DateTime.Now;
                    dbObj.ActionTypeId = (int)ActionTypeEnum.Edit;

                    if (TokenService.IsSuperAdmin())
                        dbObj.HealthFacilityId = input.HealthFacilityId;

                    _uowUser.Repository.Update(dbObj);
                    await _uowUser.Save();

                    foreach (var item in input.UserRoles)
                    {
                        //if(AppCommonMethod.IsNullOrEmptyGuid(item.UserRoleId)
                        //{
                            var obj = _mapper.Map<UserRole>(item);

                            obj.UserRoleId = Guid.NewGuid();
                            obj.UserId = dbObj.UserId;
                            obj.RoleId = item.RoleId;
                            obj.CreatedBy = _tokenService.GetUserId();
                            obj.CreatedOn = DateTime.Now;
                            obj.ActionTypeId = (int)ActionTypeEnum.Create;
                            await _uowUserRole.Repository.Insert(obj);
                            await _uowUserRole.Save();
                        //}
                        //else
                        //{
                        //    var userRoleDbObj = await _uowUserRole.Repository.GetById(item.UserRoleId);
                        //    var obj = _mapper.Map(item, userRoleDbObj);
                        //    obj.UpdatedBy = _tokenService.GetUserId();
                        //    obj.UpdatedOn = DateTime.Now;
                        //    obj.ActionTypeId = (int)ActionTypeEnum.Edit;
                        //    _uowUserRole.Repository.Update(obj);
                        //    await _uowUserRole.Save();
                        //}
                    }

                    trans.Commit();
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
            return _mapper.Map<CreateOrEditUserDto>(input);
        }

        public async Task<CreateOrEditHrUserDto> CreateOrEditHrUser(CreateOrEditHrUserDto input)
        {
            var _uowProfile = new UnitOfWork<dbModel.Profile>(_uowUser.GetDbContext());
            var _uowRole = new UnitOfWork<Role>(_uowUser.GetDbContext());
            CreateOrEditUserDto objUser = new CreateOrEditUserDto();

            objUser = _mapper.Map<CreateOrEditUserDto>(input);

            input.Cnic = Regex.Replace(input.Cnic, @"[^0-9]", "");

            var dbUser = await _uowUser.Repository.GetALL(x => x.Cnic == input.Cnic).Include(x=>x.UserRoles).FirstOrDefaultAsync();
            var dbRole = await _uowRole.Repository.GetALL(x => x.ShortName == input.RoleShortName).FirstOrDefaultAsync();

            if (!string.IsNullOrEmpty(input.Gender))
                objUser.GenderProfileId = await _uowProfile.Repository.GetALL(x => x.Name == input.Gender).Select(x => x.ProfileId).FirstOrDefaultAsync();
            if (objUser.GenderProfileId == Guid.Empty)
                objUser.GenderProfileId = null;


            if (!string.IsNullOrEmpty(input.Designation))
                objUser.DesignationProfileId = await _uowProfile.Repository.GetALL(x => x.Name == input.Designation).Select(x => x.ProfileId).FirstOrDefaultAsync();

            if (objUser.DesignationProfileId == Guid.Empty)
                objUser.DesignationProfileId = null;

            if (!AppCommonMethod.IsNullObject(dbUser))
            {
                if (dbUser!.HrId != input.HrId)
                    throw new Exception(CommonMessageConstant.UserAlreadyExistWithDifferentHrId);

                objUser.UserId = dbUser.UserId;
            }

            var location = new ViewHfLocation();
            
            if (!string.IsNullOrEmpty(input.HealthFacilityCode))
            {
                location = await _uowUser.GetDbContext().ViewHfLocations.Where(x => x.HealthFacilityCode == input.HealthFacilityCode).FirstOrDefaultAsync();
                if (AppCommonMethod.IsNullObject(location))
                    throw new Exception(CommonMessageConstant.HealthFacilityNotFound);

                objUser.HealthFacilityId = location!.HealthFacilityId;
                objUser.TehsilId = location.TehsilId;
                objUser.DistrictId = location.DistrictId;
                objUser.DivisionId = location.DivisionId;
                objUser.ProvinceId = location.ProvinceId;
            }
            else if (!string.IsNullOrEmpty(input.TehsilCode))
            {
                location = await _uowUser.GetDbContext().ViewHfLocations.Where(x => x.TehsilCode
                == input.TehsilCode).FirstOrDefaultAsync();

                if (AppCommonMethod.IsNullObject(location))
                    throw new Exception(CommonMessageConstant.TehsilNotFound);

                objUser.TehsilId = location!.TehsilId;
                objUser.DistrictId = location.DistrictId;
                objUser.DivisionId = location.DivisionId;
                objUser.ProvinceId = location.ProvinceId;
            }
            else if (!string.IsNullOrEmpty(input.DistrictCode))
            {
                location = await _uowUser.GetDbContext().ViewHfLocations.Where(x => x.DistrictCode == input.DistrictCode).FirstOrDefaultAsync();

                if (AppCommonMethod.IsNullObject(location))
                    throw new Exception(CommonMessageConstant.DistrictNotFound);

                objUser.DistrictId = location!.DistrictId;
                objUser.DivisionId = location.DivisionId;
                objUser.ProvinceId = location.ProvinceId;
            }
            else if (!string.IsNullOrEmpty(input.DivisionCode))
            {
                location = await _uowUser.GetDbContext().ViewHfLocations.Where(x => x.DivisionCode == input.DivisionCode).FirstOrDefaultAsync();

                if (AppCommonMethod.IsNullObject(location))
                    throw new Exception(CommonMessageConstant.DivisionNotFound);

                objUser.DivisionId = location!.DivisionId;
                objUser.ProvinceId = location.ProvinceId;
            }
            else
            {
                location = await _uowUser.GetDbContext().ViewHfLocations.Where(x => x.ProvinceCode == input.ProvinceCode).FirstOrDefaultAsync();

                if (AppCommonMethod.IsNullObject(location))
                    throw new Exception(CommonMessageConstant.ProvinceNotFound);

                objUser.ProvinceId = location.ProvinceId;
            }


            // Already Assigned Role
            if(!AppCommonMethod.IsNullObject(dbUser))
            {
                foreach (var userRole in dbUser.UserRoles)
                {
                    objUser!.UserRoles.Add(new CreateOrEditUserRoleDto
                    {
                        RoleId = userRole!.RoleId,
                        IsActive = userRole.IsActive
                    });
                }
            }

            var isRoleExist = objUser!.UserRoles.Where(x => x.RoleId == dbRole.RoleId).Any();
            
            if (!isRoleExist) // Check if Bas role is not Assigned
            {
                // Add Aditional BAS Role
                objUser!.UserRoles.Add(new CreateOrEditUserRoleDto
                {
                    RoleId = dbRole!.RoleId,
                    IsActive = true
                });
            }
            

            var responseObj = await CreateOrEdit(objUser);
            
            return _mapper.Map<CreateOrEditHrUserDto>(responseObj);
        }

        

        public async Task<bool> Delete(object Id)
        {
            var input = await _UserRepository.GetById(Id);

            if (input == null)
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(input);

            return await _UserRepository.Delete(input);

        }


        #endregion

        #region chanegPassword

      
        public async Task<UpdatePasswordDto> UpdatePassword(UpdatePasswordDto input)

        {
            var UserId = _tokenService.GetUserId();
            var cuurentUser = await _UserRepository.GetById(UserId);

            if (input.OldPassword != cuurentUser!.Password)
                throw new UserFriendlyExceptionForUI(CommonMessageConstant.OldPasswordNotMatch);
            
            cuurentUser.Password = input.NewPassword;
            cuurentUser.IsUserLoginFirstTime = false;
            cuurentUser.PasswordChangedOn = DateTime.Now;
            await FillEntity(cuurentUser);

            _uowUser.Repository.Update(cuurentUser);
            await _uowUser.Save();


            return _mapper.Map<UpdatePasswordDto>(input);
        }

        #endregion

        #region Read Operations
        public async Task<List<ViewUserDto>> GetAll(Expression<Func<User, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            List<User> responseObj = await _UserRepository.GetALL(filter);
            return _mapper.Map<List<ViewUserDto>>(responseObj);
        }


        public async Task<List<UserAssignableRole>> GetUserAssignableRolesById(Guid input)
        {


            var _uowAssignableUserRoles = new UnitOfWork<UserAssignableRole>(_uowUser.GetDbContext());

            var userRoles = await _uowAssignableUserRoles.Repository.GetALL(x => x.UserId == input).ToListAsync();

            //if (AppCommonMethod.IsNullOrEmptyList(userRoles))
            //    throw new UserFriendlyException(CommonMessageConstant.UsernameNotFound);

            return userRoles;
        }


        public async Task<List<UserDropdownDto>> GetAllByUserLevelwise(string RoleConst, int? HealthFacilityId, int? DepartmentLookupId, int? SectionLookupId)
        {
            var dbUser = TokenService.GetUserLoggedInfo();

            var _uowRole = new UnitOfWork<Role>(_uowUser.GetDbContext());
            Role? userRole = await _uowRole.Repository.GetALL(x => x.ShortName == RoleConst).FirstOrDefaultAsync();

            var IsPMISUser = TokenService.IsPMISUser();

            List<UserDropdownDto> responseObj = await _uowUser.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.UserRoles.Any(x => x.RoleId == userRole!.RoleId))
                .Include(x => x.UserRoles)
                .Include(x => x.DesignationProfile)
                //.Include(x => x.Department)
                //.Include(x => x.Section)

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(HealthFacilityId), x => x.HealthFacilityId == HealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(DepartmentLookupId) && !TokenService.IsPMISUser(), x => x.DepartmentId == DepartmentLookupId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(SectionLookupId) && !TokenService.IsPMISUser(), x => x.SectionId == SectionLookupId)

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.ProvinceId), x => x.ProvinceId == dbUser.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser.DivisionId), x => x.DivisionId == dbUser.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser.DistrictId), x => x.DistrictId == dbUser.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser.TehsilId), x => x.TehsilId == dbUser.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser.HealthFacilityId), x => x.HealthFacilityId == dbUser.HealthFacilityId)
                
                .Select(x => new UserDropdownDto
                {
                    Id = x.UserId,
                    Name = (!AppCommonMethod.IsNullObject(x.DesignationProfile)) ?
                                x.FullName! + " (" + x.DesignationProfile!.Name + ")" : x.FullName!,
                    //Name = (!AppCommonMethod.IsNullObject(x.Section)) ?
                    //            x.FullName! + " (" + x.Department!.Name + " / " + x.Section!.Name + ")" : (!AppCommonMethod.IsNullObject(x.Section)) ?
                    //                x.FullName! + "(" + x.Department!.Name + ")" : x.FullName!,
                })
                .ToListAsync();
            return _mapper.Map<List<UserDropdownDto>>(responseObj);
        }

        
        public async Task<List<UserDropdownDto>> GetAllUsersByRole(string RoleConst)
        {
            var dbUser = TokenService.GetUserLoggedInfo();

            var _uowRole = new UnitOfWork<Role>(_uowUser.GetDbContext());
            Role? userRole = await _uowRole.Repository.GetALL(x => x.ShortName == RoleConst).FirstOrDefaultAsync();

            var IsPMISUser = TokenService.IsPMISUser();

            List<UserDropdownDto> responseObj = await _uowUser.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.UserRoles.Any(x => x.RoleId == userRole!.RoleId))
                .Include(x => x.UserRoles)

                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser!.ProvinceId), x => x.ProvinceId == dbUser.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser.DivisionId), x => x.DivisionId == dbUser.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser.DistrictId), x => x.DistrictId == dbUser.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser.TehsilId), x => x.TehsilId == dbUser.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(dbUser.HealthFacilityId), x => x.HealthFacilityId == dbUser.HealthFacilityId)

                .Select(x => new UserDropdownDto
                {
                    Id = x.UserId,
                    Name = x.FullName!,
                })
                .ToListAsync();
            return _mapper.Map<List<UserDropdownDto>>(responseObj);
        }

        public async Task<ViewPagerDto<ViewUserDto>> GetAllWithPagination(FilterUserDto filter)
        {
            
            //var userlogin = TokenService.GetUserLoggedInfo();

            var userList = _uowUser.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).ToList();

            var list = _uowUser.Repository.GetALL(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted) // && x.UserId != _tokenService.GetUserId())
                .Include(x => x.HealthFacility)
                .OrderByDescending(x=>x.CreatedOn)
                .WhereIf(!string.IsNullOrEmpty(filter.SearchString), x => x.FullName!.ToLower().Contains(filter.SearchString) || x.Cnic!.ToLower().StartsWith(filter.SearchString) || x.Email!.ToLower().StartsWith(filter.SearchString))
                //.WhereIf(userlogin!.IsShowRoleOnly == true, x => x.CreatedBy == _tokenService.GetUserId())  // need to comment because user listing was not showing to hf admin.It should be accordingly health facility
                //.WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.ProvinceId), x => x.ProvinceId == filter.ProvinceId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DivisionId), x => x.DivisionId == filter.DivisionId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.DistrictId), x => x.DistrictId == filter.DistrictId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.TehsilId), x => x.TehsilId == filter.TehsilId)
                .WhereIf(!AppCommonMethod.IsNullorZeroInt(filter.HealthFacilityId), x => x.HealthFacilityId == filter.HealthFacilityId)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.StartDate), x => x.CreatedOn!.Value.Date >= filter.StartDate!.Value.Date)
                .WhereIf(!AppCommonMethod.IsNullorEmptyDate(filter.EndDate), x => x.CreatedOn!.Value.Date < filter.EndDate!.Value.AddDays(1).Date)
                .OrderByDescending(x => x.CreatedOn);

            IQueryable<ViewUserDto> IQueryableList = list.Select(x =>
                new ViewUserDto
                {
                    UserId = x.UserId,
                    FullName = x.FullName,
                    FatherName = x.FatherName,
                    Cnic = x.Cnic,
                    Email = x.Email,
                    ContactNo = x.ContactNo,
                    ProvinceId = x.ProvinceId,
                    DistrictId = x.DistrictId,
                    DivisionId = x.DivisionId,
                    TehsilId = x.TehsilId,
                    DepartmentId = x.DepartmentId,
                    SectionId = x.SectionId,
                    Dob = x.Dob,
                    GenderProfileId = x.GenderProfileId,
                    DesignationProfileId = x.DesignationProfileId,
                    UcId = x.UcId,
                    HealthFacilityId = x.HealthFacilityId,
                    HealthFacilityName = x.HealthFacility!.Name,
                    //CreatedBy = userList.Where(x => userList.Contains()x.UserId == x.CreatedBy).Select(x => x.FullName).FirstOrDefault(),
                    CreatedBy = x.CreatedBy,
                    CreatedOn = x.CreatedOn,
                    //UpdatedBy = userList.Where(x => x.UserId == x.UpdatedBy).Select(x => x.FullName).FirstOrDefault(),
                    UpdatedOn = x.UpdatedOn,
                    Role = string.Join(",", x.UserRoles.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted).Select(x => x.Role!.Name)),
                    UpdatedBy = x.UpdatedBy,
                    IsActive = x.IsActive,
                    UserRoles = x.UserRoles.Select(x => new ViewUserRoleDto {
                        RoleId = x.RoleId,
                        UserId = x.UserId,
                    }).ToList()
                });;

            var pagedList = await PagedListDto<ViewUserDto>.ToPagedListAsync(
                   IQueryableList,
                   filter.PageNumber,
                   filter.PageSize
                   );

            foreach (var item in pagedList)
            {
                item.CreatedByName = userList.Where(x => x.UserId == item.CreatedBy).Select(x => x.FullName).FirstOrDefault();
                item.UpdatedByName = userList.Where(x => x.UserId == item.UpdatedBy).Select(x => x.FullName).FirstOrDefault();
            }

            var responseObject = new ViewPagerDto<ViewUserDto>
            {
                TotalCount = pagedList.TotalCount,
                PageSize = pagedList.PageSize,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                HasNext = pagedList.HasNext,
                HasPrevious = pagedList.HasPrevious,
                List = pagedList,
            };

            return responseObject;
        }


        public async Task<ViewUserDto> GetById(Guid input)
        {
            User responseObj = await _UserRepository.GetById(input!);
            return _mapper.Map<ViewUserDto>(responseObj);
        }

        public async Task<ViewUserDto> GetByCnic(string cnic)
        {
            cnic = Regex.Replace(cnic, @"[^0-9]", "");
            User responseObj = await _UserRepository.GetByCnic(cnic);
            return _mapper.Map<ViewUserDto>(responseObj);
        }

        public async Task<ViewUserDto> GetByIdWithUserRoleDetails(Guid input)
        {
            User? responseObj = await _UserRepository.GetByIdWithUserRoleDetails(input);
            return _mapper.Map<ViewUserDto>(responseObj);
        }

        public async Task<ViewUserDto> GetHrUserByCnic(string cnic)
        {
            cnic = Regex.Replace(cnic, @"[^0-9]", "");
            ResponseHrUserDto hrUserObj = await _HRService.GetHrUserByCnic(_hrTokneUserName, _hrToknePassword, _hrBaseUrl,cnic);
            ViewUserDto responseObj = new ViewUserDto();

            var qryGenderList = (from _profiles in _uowUser.GetDbContext().Profiles.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                                  join _profileType in _uowUser.GetDbContext().ProfileTypes.Where(x => x.ShortName == CommonConstants.Gender) on _profiles.ProfileTypeId equals _profileType.ProfileTypeId
                                  select new 
                                  {
                                      name = _profiles.Name,
                                      profileId = _profiles.ProfileId
                                  });

            var qryDesignationList = (from _profiles in _uowUser.GetDbContext().Profiles.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                                  join _profileType in _uowUser.GetDbContext().ProfileTypes.Where(x => x.ShortName == CommonConstants.Designation) on _profiles.ProfileTypeId equals _profileType.ProfileTypeId
                                  select new
                                  {
                                      name = _profiles.Name,
                                      profileId = _profiles.ProfileId
                                  });

            if (hrUserObj != null && hrUserObj.Success)
            {
                var allLocations = await _ProvinceService.GetAllLocations();
                responseObj.Cnic = hrUserObj.Data.CNIC;
                responseObj.ProvinceId = 1;
                var healthFacility = allLocations.HealthFacilityDropdown.Where(x => x.Code == hrUserObj.Data.HfmisCode).FirstOrDefault();
                if (healthFacility != null)
                {
                    var tehsil = allLocations.TehsilDropdown.Where(x => x.Id == healthFacility.ParentId).FirstOrDefault();
                    var district = allLocations.DistrictDropdown.Where(x => x.Id == tehsil.ParentId).FirstOrDefault();
                    var division = allLocations.DivisionDropdown.Where(x => x.Id == district.ParentId).FirstOrDefault();

                    responseObj.TehsilId = tehsil.Id;  //allLocations.TehsilDropdown.Where(x => x.Code == hrUserObj.Data.WorkingTehsilCode).Select(x => x.Id).FirstOrDefault();

                    responseObj.DivisionId = division.Id; // healthFacility.DivisionId; //allLocations.DivisionDropdown.Where(x => x.Code == hrUserObj.Data.WorkingDivisionCode).Select(x => x.Id).FirstOrDefault();

                    responseObj.DistrictId = district.Id;//allLocations.DistrictDropdown.Where(x => x.Code == hrUserObj.Data.WorkingDistrictCode).Select(x => x.Id).FirstOrDefault();



                    responseObj.HealthFacilityId = healthFacility.Id;//allLocations.HealthFacilityDropdown.Where(x => x.Code == hrUserObj.Data.HfmisCode).Select(x => x.Id).FirstOrDefault();
                }
                responseObj.FullName = hrUserObj.Data.EmployeeName;
                responseObj.FatherName = hrUserObj.Data.FatherName;
                responseObj.Email = hrUserObj.Data.EMaiL;
                responseObj.ContactNo = hrUserObj.Data.MobileNo;
                responseObj.HrId = hrUserObj.Data.Id;
                responseObj.GenderProfileId = qryGenderList.Where(x => x.name == hrUserObj.Data.Gender).Select(x => x.profileId).FirstOrDefault();
                responseObj.DesignationProfileId = qryDesignationList.Where(x => x.name == hrUserObj.Data.WDesignation_Name).Select(x => x.profileId).FirstOrDefault();
                responseObj.Dob = DateTime.Parse(hrUserObj.Data.DateOfBirth);

                // Check if Login User is not Super Admin and response User are of Same Health Facility
                if (!TokenService.IsSuperAdmin() && !TokenService.IsSeniorDataProcessor() && responseObj.HealthFacilityId != TokenService.GetUserHfId())
                    return null;

                if (TokenService.IsSeniorDataProcessor() && responseObj.DivisionId != TokenService.GetUserDivisionId())
                    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                return responseObj;

            }
            else
                return null;

        }

        public async Task GetHrDoctorListByHealthFacilityCode()

        {
            string[] cadreTypeClause = new string[] { "Dental Cadre", "General Cadre", "Specialist Cadre" }; // Health Facilities Types Allow Only
            string[] healthFacilityTypesClause = new string[] { "011", "012", "068", "024" }; // Health Facilities Types Allow Only


            var allLocations = await _ProvinceService.GetAllLocations();
            var healthFacilities = await _healthFacilityService.GetAll(x => healthFacilityTypesClause.Contains(x.HealthFacilityTypeCode));
            var qryGenderList = (from _profiles in _uowUser.GetDbContext().Profiles.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                                 join _profileType in _uowUser.GetDbContext().ProfileTypes.Where(x => x.ShortName == CommonConstants.Gender) on _profiles.ProfileTypeId equals _profileType.ProfileTypeId
                                 select new
                                 {
                                     name = _profiles.Name,
                                     profileId = _profiles.ProfileId
                                 }).ToList();

            var qryDesignationList = (from _profiles in _uowUser.GetDbContext().Profiles.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted && x.IsActive == true)
                                      join _profileType in _uowUser.GetDbContext().ProfileTypes.Where(x => x.ShortName == CommonConstants.Designation) on _profiles.ProfileTypeId equals _profileType.ProfileTypeId
                                      select new
                                      {
                                          name = _profiles.Name,
                                          profileId = _profiles.ProfileId
                                      }).ToList();

            var doctorRole = (from _roles in _uowUser.GetDbContext().Roles.Where(x => x.ActionTypeId != (int)ActionTypeEnum.Deleted 
                              && x.IsActive == true
                              && x.Name == RoleConst.Doctor
                              )
                               select new
                               {
                                   Name = _roles.Name,
                                   RoleId = _roles.RoleId
                               }).FirstOrDefault();
            try
            {
                foreach (var item in healthFacilities)
                {
                    if (item.HealthFacilityId == 4878)
                        continue;
                    ResponseListHrUserDto hrUserObj = await _HRService.GetHealthFacilityProfilesByHfmisCodeByDesignationId(_hrTokneUserName, _hrToknePassword, _hrBaseUrl,item.Code!, null);

                    if (hrUserObj != null && hrUserObj.Success)
                    {
                        List<HrUserDto> doctorList = hrUserObj.Data.Where(x => cadreTypeClause.Contains(x.Cadre_Name)).ToList();

                        foreach (var doctorItem in doctorList)
                        {
                            CreateOrEditUserDto userObj = new CreateOrEditUserDto();
                            //if (doctorItem.CNIC == "3520227025482")
                                
                            //    Console.WriteLine(doctorItem.CNIC);

                            userObj.Cnic = doctorItem.CNIC!;
                            userObj.TehsilId = item.TehsilId;
                            userObj.DistrictId = item.DistrictId;
                            userObj.DivisionId = item.DivisionId;
                            userObj.ProvinceId = item.ProvinceId;
                            userObj.HealthFacilityId = item.HealthFacilityId;
                            userObj.FullName = doctorItem.EmployeeName;
                            userObj.FatherName = doctorItem.FatherName;
                            userObj.Email = String.IsNullOrEmpty(doctorItem.EMaiL) || doctorItem.EMaiL == "abc@gmail.com" ? doctorItem.CNIC + "@gmail.com" : doctorItem.EMaiL;
                            userObj.ContactNo = doctorItem.MobileNo;
                            userObj.HrId = doctorItem.Id;

                            if (!string.IsNullOrEmpty(doctorItem.Gender))
                                userObj.GenderProfileId = qryGenderList.Where(x => x.name == doctorItem.Gender).Select(x => x.profileId).FirstOrDefault();
                            else
                                userObj.GenderProfileId = null;

                            if (!string.IsNullOrEmpty(doctorItem.WDesignation_Name))
                            {
                                var designation = qryDesignationList.Where(x => x.name == doctorItem.WDesignation_Name).Select(x => x.profileId).FirstOrDefault();
                                if (!AppCommonMethod.IsNullOrEmptyGuid(designation))
                                    userObj.DesignationProfileId = designation;
                                else
                                    userObj.DesignationProfileId = null;
                            }
                            else
                                userObj.DesignationProfileId = null;

                            userObj.Dob = doctorItem.DateOfBirth != null ? DateTime.Parse(doctorItem.DateOfBirth!) : null;
                            userObj.PmisUser = true;
                            userObj.Password = "123456";
                            userObj.IsActive = true;

                            userObj.UserRoles.Add(new CreateOrEditUserRoleDto
                            {
                                RoleId = doctorRole!.RoleId,
                                IsActive = true
                            });

                            var user = await _uowUser.Repository.GetALL(x => x.Email == userObj.Email || x.Cnic == userObj.Cnic || x.ContactNo == userObj.ContactNo).FirstOrDefaultAsync();

                            if (user != null)
                                continue;


                            var obj = _mapper.Map<User>(userObj);
                            obj.Username = userObj.Cnic;

                            await FillEntity(obj);

                            User responseObj = await _uowUser.Repository.Insert(obj);
                            await _uowUser.Save();
                        }
                    }
                }

                CreateOrEditSyncDataLogDto objSyncDataLogDto = new CreateOrEditSyncDataLogDto();
                objSyncDataLogDto.TableName = "Users";
                objSyncDataLogDto.ResourceSystem = CommonStringConstant.HumanResource;
                objSyncDataLogDto.LastSync = DateTime.Now;
                await _SyncDataLogService.CreateOrEdit(objSyncDataLogDto);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region Helper Methods

        private async Task FillEntity(User obj)
        {
            //var userRoleDbList = await _UserRepository.GetALL(x => x.UserId == obj.UserId);
            if (obj.UserId == Guid.Empty)
            {
                obj.UserId = Guid.NewGuid();
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

            foreach (var userRole in obj.UserRoles)
            {
                if (userRole.UserRoleId == Guid.Empty)
                {
                    userRole.UserRoleId = Guid.NewGuid();
                    userRole.UserId = obj.UserId;
                    userRole.CreatedBy = _tokenService.GetUserId();
                    userRole.CreatedOn = DateTime.Now;
                    userRole.ActionTypeId = (int)ActionTypeEnum.Create;
                }
                else
                {
                    userRole.CreatedBy = obj.CreatedBy;
                    userRole.CreatedOn = obj.CreatedOn;
                    userRole.UserId= obj.UserId;
                    userRole.UpdatedBy = _tokenService.GetUserId();
                    userRole.UpdatedOn = DateTime.Now;
                    userRole.ActionTypeId = (int)ActionTypeEnum.Edit;
                }
            }

        }
        private void FillEntityDelete(User obj)
        {
            if (obj != null)
            {
                obj.DeletedBy = _tokenService.GetUserId();
                obj.DeletedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
            }
        }

        #endregion
    }
}
