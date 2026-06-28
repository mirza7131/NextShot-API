using CommonExceptionHandler;
using CommonMessages;
using DTOs.UserDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthentication
{
    public class TokenService
    {
        #region Fields and Propertities

        private static string? _secret;
        private static string? _expDate;
        private static string? _issuer;
        private static string? _audience;
        private static IHttpContextAccessor? _requestContext;

        #endregion

        #region CTOR
        public TokenService(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            _secret = config.GetSection("JwtConfig").GetSection("secret").Value ?? string.Empty;
            _expDate = config.GetSection("JwtConfig").GetSection("expirationInMinutes").Value ?? string.Empty;
            _issuer = config.GetSection("JwtConfig").GetSection("issuer").Value ?? string.Empty;
            _audience = config.GetSection("JwtConfig").GetSection("audience").Value ?? string.Empty;
            _requestContext = httpContextAccessor ?? throw new Exception(CommonMessageConstant.HttpContextCannotNull);
        }

        #endregion



        public static string GenerateSecurityToken(UserLoggedInfoDTO userDTO)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secret ?? string.Empty);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, JsonConvert.SerializeObject(userDTO)),
                }),
                Expires = DateTime.Now.AddMinutes(double.Parse(_expDate ?? string.Empty)),
                Issuer = _issuer,
                Audience = _audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }

        public static ClaimsPrincipal? GetPrincipal(string token)
        {
            try
            {

                var key = Encoding.ASCII.GetBytes(_secret ?? string.Empty);

                token = token.Replace("Bearer ", "").Replace("bearer ", "");
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null) return null;
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out securityToken);
                return principal;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static string ValidateToken(string token)
        {
            string username = null;
            ClaimsPrincipal principal = GetPrincipal(token);
            if (principal == null) return null;
            ClaimsIdentity identity = null;
            try
            {
                identity = (ClaimsIdentity)principal.Identity;
            }
            catch (NullReferenceException)
            {
                return null;
            }
            Claim usernameClaim = identity.FindFirst(ClaimTypes.Name);
            username = usernameClaim.Value;
            return username;
        }

        #region GetUser Info

        public static string GetUsername()
        {
            return GetValueFromHeder(GetUserInfoConstant.Username);
        }

        public static UserLoggedInfoDTO? GetUserLoggedInfo()
        {
            string userLoggedInfo = GetValueFromHeder(GetUserInfoConstant.UserLogged);
            if (userLoggedInfo != null)
            {
                return JsonConvert.DeserializeObject<UserLoggedInfoDTO>(userLoggedInfo);
            }
            return null;
        }

        public Guid GetUserId()
        {
            return string.IsNullOrEmpty(GetValueFromHeder(GetUserInfoConstant.UserId)) ? Guid.Empty : Guid.Parse(GetValueFromHeder(GetUserInfoConstant.UserId));
        }

        public Guid? GetUserEventId()
        {
            return string.IsNullOrEmpty(GetValueFromHeder(GetUserInfoConstant.EventId)) ? null : Guid.Parse(GetValueFromHeder(GetUserInfoConstant.EventId));
        }

        public static int GetUserHfId()
        {
            return string.IsNullOrEmpty(GetValueFromHeder(GetUserInfoConstant.HealthFacilityId)) ? 0 : int.Parse(GetValueFromHeder(GetUserInfoConstant.HealthFacilityId));
        }

        public static string GetHfHrId()
        {
            return GetValueFromHeder(GetUserInfoConstant.HfHrId);
        }

        public static string GetMimsDepartmentId()
        {
            return GetValueFromHeder(GetUserInfoConstant.MimsDepartmentId);
        }

        public static string GetUserHfCode()
        {
            return GetValueFromHeder(GetUserInfoConstant.HealthFacilityCode);
        }

        public static int GetUserDivisionId()
        {
            return string.IsNullOrEmpty(GetValueFromHeder(GetUserInfoConstant.DivisionId)) ? 0 : int.Parse(GetValueFromHeder(GetUserInfoConstant.DivisionId));
        }

        public static string GetUserEmail()
        {
            return GetValueFromHeder(GetUserInfoConstant.Email);
        }


        public static bool IsSuperAdmin()
        {
            return bool.Parse(GetValueFromHeder(GetUserInfoConstant.SuperAdmin));
        }

        public static bool IsPMISUser()
        {
            return bool.Parse(GetValueFromHeder(GetUserInfoConstant.Pmis));
        }

        public static bool IsSeniorDataProcessor()
        {
            return bool.Parse(GetValueFromHeder(GetUserInfoConstant.SeniorDataProcessor));
        }

        //public static bool IsSuperAdmin()
        //{
        //    string userRoles = GetValueFromHeder(GetUserInfoConstant.UserRoles);
        //    if (userRoles != null)
        //    {
        //        var userRoleList = JsonConvert.DeserializeObject<List<UserRoleDto>>(userRoles)!;
        //        List<string> userRoles = userRole.Split(',').ToList();
        //        userRoles.Remove(userRoles[userRoles.Count - 1]);
        //        return userRoles.Where(x => x.Contains(GetUserInfoConstant.SuperAdmin)) != null ? true : false;
        //    }
        //    return false;
        //}

        public static List<UserRoleDto?> UserRoles()
        {
            string userRoles = GetValueFromHeder(GetUserInfoConstant.UserRoles);
            if (userRoles != null)
            {
                return JsonConvert.DeserializeObject<List<UserRoleDto>>(userRoles)!;
            }
            return null;
        }

        #endregion

        #region Helper Methods

        public static string GetValueFromHeder(string Key)
        {
            try
            {
                var token = StringValues.Empty;
                var header = _requestContext?.HttpContext?.Request.Headers.TryGetValue("Authorization", out token);

                if (string.IsNullOrEmpty(token))
                    throw new UserFriendlyException(CommonMessageConstant.NoAccessTokenPresent);

                var encrptToken = token[0] ?? string.Empty;  // Get Encrypted Token
                if (!string.IsNullOrEmpty(encrptToken))
                {
                    var claimPricipal = GetPrincipal(encrptToken);
                    UserLoggedInfoDTO? userObj = null;
                    var userClaim = claimPricipal?.Identity?.Name;

                    if (!string.IsNullOrEmpty(userClaim))
                    {
                        userObj = new UserLoggedInfoDTO();
                        userObj = JsonConvert.DeserializeObject<UserLoggedInfoDTO>(userClaim);
                    }
                    if (userObj != null)
                    {
                        switch (Key)
                        {
                            case GetUserInfoConstant.UserLogged:
                                return userClaim ?? string.Empty;
                            case GetUserInfoConstant.Username:
                                return userObj.Username ?? string.Empty;
                            case GetUserInfoConstant.UserId:
                                return userObj.UserId.ToString();
                            case GetUserInfoConstant.Email:
                                return userObj?.Email?.ToString() ?? string.Empty;
                            case GetUserInfoConstant.SuperAdmin:
                                return userObj?.IsSuperAdmin.ToString() ?? string.Empty;
                            case GetUserInfoConstant.SeniorDataProcessor:
                                return userObj?.IsSeniorDataProcessor.ToString() ?? string.Empty;
                            case GetUserInfoConstant.Pmis:
                                return userObj?.IsPMIS.ToString() ?? string.Empty;
                            case GetUserInfoConstant.UserRoles:
                                return JsonConvert.SerializeObject(userObj.UserRoleList) ?? string.Empty;
                            case GetUserInfoConstant.HealthFacilityId:
                                return userObj.HealthFacilityId.ToString() ?? string.Empty;
                            case GetUserInfoConstant.HfHrId:
                                return userObj.HfHrId.ToString() ?? string.Empty;
                            case GetUserInfoConstant.HealthFacilityCode:
                                return userObj.HealthFacilityCode?.ToString() ?? string.Empty;
                            case GetUserInfoConstant.DivisionId:
                                return userObj.DivisionId?.ToString() ?? string.Empty;
                            case GetUserInfoConstant.MimsDepartmentId:
                                return userObj.MimsDepartmentId?.ToString() ?? string.Empty;
                            case GetUserInfoConstant.EventId:
                                return userObj.EventId.ToString() ?? string.Empty;
                            default:
                                return string.Empty;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return string.Empty;
        }

        public string GetAccessToken()
        {

            var token = StringValues.Empty;
            var header = _requestContext?.HttpContext?.Request.Headers.TryGetValue("Authorization", out token);

            if (string.IsNullOrEmpty(token))
                throw new UserFriendlyException(CommonMessageConstant.NoAccessTokenPresent);

            return token.ToString().Replace("Bearer " , "");

        }

        #endregion
    }
}
