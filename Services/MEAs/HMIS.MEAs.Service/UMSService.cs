using HMIS.MEAs.Domain.Models.DTO.UMS;
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
using HMIS.MEAs.Domain.Models.DTO.Dashboard;
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
using HMIS.Aggregator.API.Services;
using HMIS.MEAs.Domain.Models.DTO.UsersModel;

namespace HMIS.MEAs.Service
{
    public class UMSService
    {
        #region Class Fields & Propertities
        private readonly JWTAuthentication.TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<User> _User;
        private readonly UnitOfWork<UserLog> _UserLog;
        private readonly UnitOfWork<Zone> _Zone;
        #endregion
        #region Constructor

        public UMSService(
           JWTAuthentication.TokenService tokenService,
           UnitOfWork<User> user,
           UnitOfWork<UserLog> userlog,
           UnitOfWork<Zone> zone,
           IMapper mapper)
        {
            _tokenService = tokenService;
            _User = user;
            _UserLog = userlog;
            _mapper = mapper;
            _Zone = zone;
        }

        #endregion
        #region CUD Opertations

        public async Task<List<UserZoneChangeDTO>> UpdateUsersZoneId(List<UserZoneChangeDTO> users)
        {
            List<UserZoneChangeDTO> updatedUsers = new List<UserZoneChangeDTO>();

            if (users.Count > 0)
            {
                foreach (var u in users)
                {
                    var user = _User.Repository.GetALL(x => x.Username.ToLower().Equals(u.UserName.ToLower())).FirstOrDefault();

                    if (user != null)
                    {
                        UserLog userLog = _mapper.Map<UserLog>(user);
                        FillEntity(userLog);
                        await _UserLog.Repository.Insert(userLog); 

                        var zone = _Zone.Repository.GetALL(x => x.ZoneName.ToLower().Equals(u.Zone.ToLower()) && x.IsActive == true).FirstOrDefault();

                        if (zone != null)
                        {
                            user.ZoneId = zone.ZoneId;
                            FillEntity(user);
                            _User.Repository.Update(user);

                            updatedUsers.Add(u);
                        }
                    }
                }

                await _UserLog.Save();
                await _User.Save();
            }

            return updatedUsers;
        }


        #endregion
        #region Read Operations
        #endregion
        #region Helper

        private void FillEntity(UserLog obj)
        {
               // obj.CreatedBy = _tokenService.GetUserIdForInt(); 
                obj.CreatedOn = DateTime.Now;
                obj.IsDelete = false;
        }
        private void FillEntity(User obj)
        {
         //   obj.UpdatedBy = _tokenService.GetUserIdForInt();
            obj.UpdatedOn = DateTime.Now;
        }
        #endregion
    }
}
