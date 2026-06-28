using DTOs.UserDTO;
using JWTAuthentication;
using Microsoft.AspNetCore.Mvc;

namespace AuthService
{
    public class AuthService
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;

        #endregion

        #region Constructor

        public AuthService(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        #endregion

        #region CUD Operations


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


        #endregion
    }
}
