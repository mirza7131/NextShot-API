using AppCommonMethods;
using AppCommonMethods.AppConstants;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.EventDto;
using AuthDAL.Models.Dto.UserDto;
using AuthDAL.Models.Dto.UserLogDto;
using AuthDAL.Repositories;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using DTOs.UserDTO;
using HMIS.Aggregator.API;
using HMIS.Aggregator.API.Services;
using JWTAuthentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using Microsoft.IdentityModel.Tokens;
using SMSSender;
using SMSSender.DTO;
using System.Data;

namespace AuthBAL
{
    public class AuthService
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly UnitOfWork<User> _uowUser;
        private static string? _expDate;
        private readonly IMapper _mapper;
        private readonly HealthFacilityService<HealthFacility> _healthFacilityService;
        private readonly HRService _hrService;
        private readonly bool AuthViaHR = false;
        private readonly bool UserCreationWithoutHR = true;
        private readonly MimsMedicineDataService _mimsMedicineDataService;
        private readonly string _hrTokneUserName;
        private readonly string _hrToknePassword;
        private readonly string _hrBaseUrl;
        private readonly bool _isOffline;
        private readonly bool _isPaidProcedures;
        private readonly SMS _smsService;
        #endregion

        #region Constructor

        public AuthService(TokenService tokenService, UnitOfWork<User> uowUser, HealthFacilityService<HealthFacility> healthFacilityService,
            HRService hrService, IMapper mapper, IConfiguration config,
             MimsMedicineDataService mimsMedicineDataService, SMS smsService
        )
        {
            _tokenService = tokenService;
            _expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value ?? string.Empty;
            _uowUser = uowUser;
            _healthFacilityService = healthFacilityService;
            _hrService = hrService;
            _mimsMedicineDataService = mimsMedicineDataService;
            _smsService = smsService;
            _mapper = mapper;
            AuthViaHR = config.GetSection("Authentication").GetValue<bool>("AuthViaHR");
            UserCreationWithoutHR = config.GetSection("Authentication").GetValue<bool>("UserCreationWithoutHR");
            _hrTokneUserName = config.GetSection("HrTokenUserName").Value ?? string.Empty;
            _hrToknePassword = config.GetSection("HrTokenPassword").Value ?? string.Empty;
            _hrBaseUrl = config.GetSection("HrBaseUrl").Value ?? string.Empty;
            _isOffline = (config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") == true) ? true : false;
            _isPaidProcedures = (config.GetSection("HealthFacilityConfiguartion").GetSection("Dental").GetValue<bool>("IsPaidProcedures") == true) ? true : false;
        }

        #endregion

        #region Authenticate

        public async Task<UserLoggedInfoDTO> Authenticate(UserLoginDTO userLoginDTO)
        {
            userLoginDTO.CNIC = userLoginDTO.CNIC.Replace("-", string.Empty);

            var user = await _uowUser.Repository.GetALL(x => x.Cnic == userLoginDTO.CNIC //&& x.Password == userLoginDTO.Password
            && x.IsActive == true && x.ActionTypeId != (int)ActionTypeEnum.Deleted)
                .Include(x => x.Section)
                .Include(x => x.DesignationProfile)
                .Include(x => x.Department)
                .Include(x => x.HealthFacility)
                .Include(x => x.UserRoles).ThenInclude(x => x.Role)
                .FirstOrDefaultAsync();

            if (AppCommonMethod.IsNullObject(user))
                throw new UserFriendlyException(CommonMessageConstant.UserNotFound);

            var isEventExist = await CheckIfEventExist(user.HealthFacilityId, user.UserId);
            
            // throw exception if Facility Type is Private and User is not Assigned any Event
            if (!AppCommonMethod.IsNullObject(user!.HealthFacility!) && 
                user!.HealthFacility!.HealthFacilityTypeCode == CommonConstant.MobileUnits)
            {
                if (AppCommonMethod.IsNullOrEmptyGuid(isEventExist!.EventId))
                    throw new UserFriendlyException(CommonStringConstant.NoEventFoundAgainstThisFacility);
            }


            //if (!AuthViaHR)
            //{
            //    var response = await _hrService.AuthenticateHrUser(_hrTokneUserName, _hrToknePassword, _hrBaseUrl, userLoginDTO.CNIC.Replace("-", string.Empty), userLoginDTO.Password);

            //    if (!response.Success)
            //        throw new UserFriendlyException("HR: " + response.Message);

            //    if (!response.Data.IsPresent)
            //        throw new UserFriendlyException("HR: " + response.Message);
            //}
            //else 
            if (!user!.Password.Equals(userLoginDTO.Password))
                throw new UserFriendlyException(CommonMessageConstant.IncorrectPassword);

            var IsBas = user.UserRoles.Where(x => x.Role.ShortName == CommonStringConstant.BAS).FirstOrDefault();

            var userObj = new UserLoggedInfoDTO()
            {
                UserId = user.UserId,
                Cnic = user.Cnic,
                IsUserLoginFirstTime = user.IsUserLoginFirstTime,
                PasswordChangedOn = user.PasswordChangedOn,
                ContactNo = user.ContactNo,
                Username = user.Username,
                Key = IsBas != null ? user.Password : null,
                Email = user.Email,
                FullName = user.FullName,
                HealthFacilityId = user.HealthFacilityId,
                HealthFacilityName = user.HealthFacility != null ? user.HealthFacility.Name : null,
                HealthFacilityCode = user.HealthFacility != null ? user.HealthFacility.Code : null,
                HfHrId = user.HealthFacility != null ? user.HealthFacility.HrId : null,
                ProvinceId = user.ProvinceId,
                DivisionId = user.DivisionId,
                DistrictId = user.DistrictId,
                TehsilId = user.TehsilId,
                DesignationName = user.DesignationProfile != null ? user.DesignationProfile.Name : null,
                DepartmentId = user.DepartmentId,
                DepartmentName = user.Department != null ? user.Department!.Name : "",
                SectionId = user.SectionId,
                SectionName = user.Section != null ? user.Section!.Name : null,
                IsConsultant = user.Section != null ? user.Section!.IsConsultant : false,
                ConsultantSectionId = user.Section != null ? user.Section!.ConsultantSectionLookupId : null,
                MimsDepartmentId = user.Section != null ? user.Section?.MimsWardId : user.Department?.MimsDepartmentId,
                AuthViaHR = AuthViaHR,
                UserCreationWithoutHR = UserCreationWithoutHR,
                IsShowRoleOnly = user.IsShowRoleOnly,
                IsOffline = _isOffline,
                IsFreeLabTest = (user.Section != null && user.Section!.IsFreeLabTest == true) ? true:false,
                IsSkipAlmoner = (user.Section != null && user.Section!.IsSkipAlmoner == true) ? true : false,
                MimsBranchId = user.MimsBranchId,
                UserRoleList = user.UserRoles.Select(y => new UserRoleDto
                {
                    Name = y.Role.Name,
                    ShortName = y.Role.ShortName,
                    RoleId = y.Role.RoleId,
                    RoutingUrl = y.Role?.RoutingUrl,
                    RoleType = y.Role?.RoleType
                }).ToList(),
            };

            if (userObj.UserRoleList.Where(x => x.RoleType != null && (x.RoleType?.ToLower() == RoleTypeConst.SuperAdmin.ToLower())).Count() > 0)
                userObj.IsSuperAdmin = true;

            if (userObj.UserRoleList.Where(x => x.RoleType != null && (x.RoleType?.ToLower() == RoleTypeConst.Admin.ToLower())).Count() > 0)
                userObj.IsAdmin = true;

            if (userObj.UserRoleList.Where(x => x.RoleType != null && (x.RoleType?.ToLower() == RoleTypeConst.SDP.ToLower())).Count() > 0)
                userObj.IsSeniorDataProcessor = true;

            if (userObj.UserRoleList.Where(x => x.RoleType != null && (x.RoleType?.ToLower() == RoleTypeConst.PMIS.ToLower())).Count() > 0)
                userObj.IsPMIS = true;

            if (userObj.UserRoleList.Where(x => x.RoleType != null && (x.RoleType?.ToLower() == RoleTypeConst.Doctor.ToLower())).Count() > 0)
                userObj.IsDoctor = true;

            var level = GetUserLevel(userObj);

            //userObj.UserLevel = 

            if (userObj.IsDoctor)
            {
                if (userObj.SectionId != null)
                {
                    var _uowSectionLookup = new UnitOfWork<SectionLookup>(_uowUser.GetDbContext());
                    SectionLookup sectionLookup = new SectionLookup();
                    sectionLookup = _uowSectionLookup.Repository.GetALL(x => x.SectionLookupId == user.SectionId).FirstOrDefault();
                    if (sectionLookup != null)
                    {
                        userObj.FormType = sectionLookup.FormType;
                    }
                    else
                    {
                        userObj.FormType = CommonStringConstant.GeneralForm;
                    }
                }
                else
                {
                    userObj.FormType = CommonStringConstant.GeneralForm;
                }
            }
            else
            {
                if (user.SectionId != null)
                {
                    var _uowSectionLookup = new UnitOfWork<SectionLookup>(_uowUser.GetDbContext());
                    SectionLookup sectionLookup = new SectionLookup();
                    sectionLookup = _uowSectionLookup.Repository.GetALL(x => x.SectionLookupId == user.SectionId).FirstOrDefault();
                    if (sectionLookup != null)
                    {
                        userObj.FormType = sectionLookup.FormType;
                    }
                    else
                    {
                        userObj.FormType = CommonStringConstant.GeneralForm;
                    }

                }
                else
                    userObj.FormType = CommonStringConstant.GeneralForm;

            }

            var obj = new UserLoggedInfoDTO()
            {
                Email = userObj.Email,
                FullName = userObj.FullName,
                Cnic = userObj.Cnic,
                IsUserLoginFirstTime = userObj.IsUserLoginFirstTime,
                PasswordChangedOn = userObj.PasswordChangedOn,
                ContactNo = userObj.ContactNo,
                UserId = userObj.UserId,
                HealthFacilityId = userObj.HealthFacilityId,
                HfHrId = userObj.HfHrId,
                ProvinceId = userObj.ProvinceId,
                DivisionId = userObj.DivisionId,
                DistrictId = userObj.DistrictId,
                TehsilId = userObj.TehsilId,
                DepartmentId = userObj.DepartmentId,
                DepartmentName = userObj.DepartmentName,
                DesignationName = userObj.DesignationName,
                SectionName = userObj.SectionName,
                SectionId = userObj.SectionId,
                IsConsultant = userObj.IsConsultant,
                ConsultantSectionId = userObj.ConsultantSectionId,
                HealthFacilityName = userObj.HealthFacilityName,
                HealthFacilityCode = userObj.HealthFacilityCode,
                IsSuperAdmin = userObj.IsSuperAdmin,
                IsSeniorDataProcessor = userObj.IsSeniorDataProcessor,
                IsDoctor = userObj.IsDoctor,
                IsPMIS = userObj.IsPMIS,
                UserLevel = userObj.UserLevel,
                UserRoleList = userObj.UserRoleList,
                IsShowRoleOnly = user.IsShowRoleOnly,
                AuthViaHR = userObj.AuthViaHR,
                UserCreationWithoutHR = UserCreationWithoutHR,
                MimsDepartmentId = userObj.MimsDepartmentId,
                IsOffline = _isOffline,
                IsFreeLabTest = userObj.IsFreeLabTest,
                IsSkipAlmoner = userObj.IsSkipAlmoner,
                MimsBranchId = userObj.MimsBranchId,
                FormType = userObj.FormType ?? CommonStringConstant.GeneralForm,
                EventId = (!AppCommonMethod.IsNullObject(isEventExist) && !AppCommonMethod.IsNullOrEmptyGuid(isEventExist.EventId) ? isEventExist.EventId : null),
            };
            

            userObj.Token = TokenService.GenerateSecurityToken(obj);
            userObj.IsPaidProcedures = _isPaidProcedures;
            userObj.EventId = obj.EventId;
            await CreateUserLog(userObj);

            //await SendSms(userObj);


            return await Task.FromResult(userObj);

        }


        //[HttpPost]
        //public async Task<ResponseDTO> UserLoginWeb(UserViewModel model)
        //{
        //    using (var db = new Context())
        //    {
        //        try
        //        {
        //            var userLoggedIn = db.Users.Where(x => x.Username == model.Username && x.Password == model.Password
        //                                                && x.IsActive == true && (x.IsDelete == false || x.IsDelete == null)
        //                                                && (x.UserTypeId == 2 || x.UserTypeId == 3)).FirstOrDefault();
        //            if (userLoggedIn != null)
        //            {
        //                string token = Token.GetToken(userLoggedIn);
        //                string LocationName = "";
        //                var province = "Punjab";
        //                string divisionId = "", districtId = "", tehsilId = "", ucId = "";
        //                var userslocation = userLoggedIn.UserLocations.ToList();
        //                if (userLoggedIn.LocationCode.Length == 3)
        //                {
        //                    var div = db.ViewLocationMEAs.Where(x => x.DivisionCode == userLoggedIn.LocationCode && x.lvl == "Division").FirstOrDefault();
        //                    divisionId = div.DivisionCode;
        //                    LocationName = div.DivisionName;
        //                }
        //                else if (userLoggedIn.LocationCode.Length == 6)
        //                {
        //                    var div = db.ViewLocationMEAs.Where(x => x.DistrictCode == userLoggedIn.LocationCode && x.lvl == "District").FirstOrDefault();
        //                    districtId = div.DistrictCode;
        //                    divisionId = div.DivisionCode;
        //                    LocationName = div.DistrictName;
        //                }
        //                else if (userLoggedIn.LocationCode.Length == 9)
        //                {
        //                    var div = db.ViewLocationMEAs.Where(x => x.TehsilCode == userLoggedIn.LocationCode && x.lvl == "Tehsil").FirstOrDefault();
        //                    tehsilId = div.TehsilCode;
        //                    districtId = div.DistrictCode;
        //                    divisionId = div.DivisionCode;
        //                    LocationName = div.TehsilName;
        //                }


        //                return new ResponseDTO
        //                {
        //                    Message = MessageEnum.LoggedIn,
        //                    Data = new
        //                    {
        //                        UserId = userLoggedIn.UserId,
        //                        Name = userLoggedIn.FullName,
        //                        Username = userLoggedIn.Username,
        //                        Token = token,
        //                        LocationCode = userLoggedIn.LocationCode,
        //                        LocationName = LocationName,
        //                        ZoneId = userLoggedIn.ZoneId,
        //                        ZoneName = userLoggedIn.Zone?.ZoneName,
        //                        ProvinceId = userLoggedIn?.ProvinceId,
        //                        designation = !string.IsNullOrEmpty(userLoggedIn.Designation) ? userLoggedIn.Designation : "",
        //                        provinceName = province,
        //                        divsionId = divisionId,
        //                        districtId = districtId,
        //                        tehsilId = tehsilId,
        //                        ucId = ucId,
        //                        userLocations = userslocation != null ? userslocation.Select(h => h.LocationCode).ToList() : new List<string>(),
        //                        DepartmentName = !string.IsNullOrEmpty(userLoggedIn.DepartmentName) ? userLoggedIn.DepartmentName : "",
        //                        Roles = userLoggedIn.UserRoles?.Select(x => new
        //                        {
        //                            roleId = x.RoleID,
        //                            roleName = x.Role.RoleName
        //                        }).ToList()
        //                    }
        //                };
        //            }
        //            else
        //            {
        //                return new ResponseDTO
        //                {
        //                    Error = true,
        //                    Message = MessageEnum.UnAuthenticated,
        //                    Data = ""
        //                };
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            await CommonMethods.LogError(ex);
        //            return new ResponseDTO { Error = true, StatusCode = (int)HttpStatusCode.BadRequest, Message = MessageEnum.serverSideError + ex.InnerException, Data = "" };
        //        }
        //    }

        //}


        public async Task<bool> Signout(UserSignoutDto input)
        {

            var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowUser.GetDbContext());

            var occupiedVisits = await _uowPatientOpenVisit.Repository.GetALL(x => x.OccupiedBy == input.UserId)
                .ToListAsync();

            if (!AppCommonMethod.IsNullOrEmptyList<PatientOpenVisit>(occupiedVisits))
            {
                

                // Modify properties of the retrieved entities
                foreach (var occupiedVisit in occupiedVisits)
                {
                    occupiedVisit.IsOccupied = false;
                    occupiedVisit.OccupiedBy = null;
                    // Update other properties as needed
                    _uowPatientOpenVisit.Repository.Update(occupiedVisit);
                }

                // Save changes to the database
                await _uowPatientOpenVisit.CommitAsync();
            }
            return true;

        }
        private async Task<CreateOrEditUserLogDto> CreateUserLog(UserLoggedInfoDTO input)
        {
            var _uowUserLog = new UnitOfWork<UserLog>(_uowUser.GetDbContext());
            var userLogObj = new UserLog();

            userLogObj.UserId = input.UserId;
            userLogObj.AccessToken = input.Token;
            userLogObj.ExpireOn = DateTime.Now.AddMinutes(double.Parse(_expDate ?? string.Empty));
            userLogObj.HealthFacilityCode = input.HealthFacilityCode;
            userLogObj.HealthFacilityId = input.HealthFacilityId;
            userLogObj.IsActive = true;
            await FillEntity(userLogObj);

            UserLog responseObj = await _uowUserLog.Repository.Insert(userLogObj);
            await _uowUserLog.Save();
            return _mapper.Map<CreateOrEditUserLogDto>(responseObj);

        }
        public async Task<Otpcode> SendSms(UserLoggedInfoDTO userObj)
        {
            int otp = AppCommonMethod.GenerateRandom4DigitNumber();
            var otpCode = await SaveOtp(otp, userObj.UserId);
            SendSMSDto smsObj = new SendSMSDto()
            {
                Receiver = userObj!.ContactNo!.Replace("-", ""),
                Body = $"Your OTP is {otp}",
            };

            _smsService.SendSMS(smsObj);

            return otpCode;

        }


        private async Task<Otpcode> SaveOtp(int otp, Guid userId)
        {
            var _uowOtp = new UnitOfWork<Otpcode>(_uowUser.GetDbContext());

            var userOtp = await _uowOtp.Repository.GetALL(x => x.UserId == userId).FirstOrDefaultAsync();
            if (AppCommonMethod.IsNullObject(userOtp))
            {
                Otpcode otpcode = new Otpcode();
                otpcode.OtpcodeId = Guid.NewGuid();
                otpcode.Otp = otp;
                otpcode.UserId = userId;
                otpcode.Datetime = DateTime.Now;

                await _uowOtp.Repository.Insert(otpcode);
                await _uowOtp.Save();
                return otpcode;
            }
            else
            {
                userOtp.Otp = otp;
                userOtp.Datetime = DateTime.Now;
                _uowOtp.Repository.Update(userOtp);
                await _uowOtp.Save();
                return userOtp;
            }

        }
        public async Task<Otpcode> VerifyOTP(int OTP)
        {
            var _uowOTP = new UnitOfWork<Otpcode>(_uowUser.GetDbContext());
            var loginUser = TokenService.GetUserLoggedInfo();
            var otp = await _uowOTP.Repository.GetALL(x => x.Otp == OTP && loginUser.UserId == x.UserId).OrderByDescending(x => x.Datetime).FirstOrDefaultAsync();

            if (!AppCommonMethod.IsNullObject(otp))
            {

                DateTime date1 = (DateTime)await _uowOTP.Repository.GetALL(x => x.Otp == OTP && loginUser.UserId == x.UserId).OrderByDescending(x => x.Datetime).Select(x => x.Datetime).FirstOrDefaultAsync();
                TimeSpan ts = DateTime.Now - date1;
                int minutes = (int)ts.TotalMinutes;


                if (minutes >= 1)
                {
                    throw new UserFriendlyExceptionForUI(CommonMessageConstant.OTPIsExpired);
                }



                return otp;
            }
            else
            {

                throw new UserFriendlyExceptionForUI(CommonMessageConstant.OTPIsNotValid);
            }
        }

        public async Task<ResponseEventExistDto> CheckIfEventExist(int? HealthFacilityId, Guid UserId)
        {
            //var _uowEventUser = new UnitOfWork<EventUser>(_uowUser.GetDbContext());
            var res = new ResponseEventExistDto();
            using (var db = new NextShotContext())
            {
                var conn = _uowUser.GetDbContext().Database.GetDbConnection();
                try
                {

                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[dbo].[CheckIfEventExist]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    if (!AppCommonMethod.IsNullorZeroInt(HealthFacilityId))
                        sqlComm.Parameters.AddWithValue("@HealthFacilityId", HealthFacilityId);

                    if (!AppCommonMethod.IsNullOrEmptyGuid(UserId))
                        sqlComm.Parameters.AddWithValue("@UserId", UserId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<ResponseEventExistDto> lst = ds.Tables[0].ToList<ResponseEventExistDto>();
                    if(lst.Count > 0)
                          res = lst.FirstOrDefault();
                    
                    return res;
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        #endregion

        #region Read Operations

        [HttpGet]
        [Route("Get")]
        public async Task<UserLoggedInfoDTO> Get()
        {
            //var obj = await _tcontext.Profile.FindAsync(Id);
            //return obj != null ? _mapper.Map<DbModel.Profile>(obj) : null;

            var obj = new UserLoggedInfoDTO()
            {
                Email = "maanhaider01@gmail.com",
                FirstName = "usman"
            };

            return await Task.FromResult(obj);
        }


        #endregion

        #region Helper Methods
        private async Task FillEntity(UserLog obj)
        {
            if (AppCommonMethod.IsNullorZerolong(obj.UserLogId))
            {
                //obj.UserId = Guid.NewGuid();
                obj.CreatedBy = obj.UserId;
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

        private async Task GetUserLevel(UserLoggedInfoDTO? obj)
        {
            if (obj!.IsSuperAdmin)
            {
                obj.UserLevel = 1;
                obj.UserLevelName = "Super Admin";
            }
            else
            {
                if (AppCommonMethod.IsNullorZeroInt(obj!.ProvinceId))
                {
                    obj.UserLevel = 2;
                    obj.UserLevelName = "National User";
                }

                if (!AppCommonMethod.IsNullorZeroInt(obj!.ProvinceId))
                {
                    obj.UserLevel = 3;
                    obj.UserLevelName = "Provincial User";
                }

                if (!AppCommonMethod.IsNullorZeroInt(obj!.DivisionId))
                {
                    obj.UserLevel = 4;
                    obj.UserLevelName = "Divisional User";
                }

                if (!AppCommonMethod.IsNullorZeroInt(obj!.DistrictId))
                {
                    obj.UserLevel = 5;
                    obj.UserLevelName = "District User";
                }

                if (!AppCommonMethod.IsNullorZeroInt(obj!.TehsilId))
                {
                    obj.UserLevel = 6;
                    obj.UserLevelName = "Tehsil User";
                }

                //if (!AppCommonMethod.IsNullorZeroInt(obj!.UcId))
                //{
                //  obj.UserLevel = 7;
                //  obj.UserLevelName = "Union Council User";
                //}

                if (!AppCommonMethod.IsNullorZeroInt(obj!.HealthFacilityId))
                {
                    obj.UserLevel = 8;
                    obj.UserLevelName = "Health Facility User";
                }

            }
        }

        #endregion
    }
}
