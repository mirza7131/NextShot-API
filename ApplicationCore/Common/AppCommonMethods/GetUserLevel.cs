
using DTOs.UserDTO;

namespace AppCommonMethods
{
    public static class GetUserLevel
    {
        #region Class Fields & Propertities

        //private static readonly TokenService _tokenService;
        //private static readonly UnitOfWork<User> _uowUser;

        #endregion

        #region Constructor

        #endregion

        public static int GetUserRole(UserLoggedInfoDTO? loggedInUser)
        {
            //UnitOfWork<User> _uowUser = new UnitOfWork<User>();
            //Guid? userId = TokenService.GetUserLoggedInfo()!.UserId;

            //User? dbUser = await _uowUser.Repository.GetById(userId!);

            if (!AppCommonMethod.IsNullorZeroInt(loggedInUser!.HealthFacilityId))
                return 6;
            if (!AppCommonMethod.IsNullorZeroInt(loggedInUser!.UcId))
                return 5;
            if (!AppCommonMethod.IsNullorZeroInt(loggedInUser!.TehsilId))
                return 4;
            if (!AppCommonMethod.IsNullorZeroInt(loggedInUser!.DistrictId))
                return 3;
            if (!AppCommonMethod.IsNullorZeroInt(loggedInUser!.DivisionId))
                return 2;
            if (!AppCommonMethod.IsNullorZeroInt(loggedInUser!.ProvinceId))
                return 1;

            return 0;
        }
    }
}
