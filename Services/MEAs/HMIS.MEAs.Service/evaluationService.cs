using AppCommonMethods;
using AutoMapper;
using CommonDTOs;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using DTOs.UserDTO;
//using FileHandler;
//using HMIS.Aggregator.API;
using HMIS.MEAs.Domain.Models.DbModels;
using HMIS.MEAs.Domain.Models.DTO.Common;

//using HMIS.MEAs.Domain.Models.DTO.FilterDto;
//using HMIS.MEAs.Domain.Models.DTO.HealthFacility;
//using HMIS.MEAs.Domain.Models.DTO.PaginationDto;
using HMIS.MEAs.Domain.Repositories._UOW;
using JWTAuthentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using DbModel = HMIS.MEAs.Domain.Models.DbModels;
using System.Reflection;
//using HMIS.MEAs.Service.DTO.Common;
//using HealthFacility = HMIS.MEAs.Domain.Models.DbModels.HealthFacility;

namespace HMIS.MEAs.Service
{
    public class evaluationService
    {


        public class ResponseListDTO
        {
            public int StatusCode { get; set; } = 200;
            public string Message { get; set; }
            public Boolean Error { get; set; } = false;
            public Object List { get; set; }
        }




        [HttpGet]
        public async Task<ResponseListDTO> GetHealthFacilitiesZone()
        {
            try
            {
                using (var db = new MeasallContext())
                {
                    var HealthFacilities = (from l in db.ViewLocationMeas
                                            where l.Active == 1
                                            select new
                                            {
                                                l.Active,
                                                l.DistrictCode,
                                                l.DistrictName,
                                                l.DivisionCode,
                                                l.DivisionName,
                                                l.HealthFacilityName,
                                                l.HfId,
                                             //   l.HFMISCode,
                                             //   l.lvl,
                                                l.ModeName,
                                                l.TehsilCode,
                                                l.TehsilName,
                                                l.ZoneId,
                                                l.AmbulanceNo
                                            }).ToList();
                    var zones = db.Zones.Where(x => x.IsActive == true).Select(x => new
                    {
                        x.ZoneId,
                        x.ZoneName,
                        x.TehsilCode,
                        x.DistrictCode,
                        x.DivisonCode,
                        x.ApplicationTypeId
                    }).ToList();
                    var hfTypes = db.HealthFacilityTypes.Where(x => x.IsActive == true).Select(x => new { x.FacilityTypeId, x.FaciltyTypeName }).ToList();
              //      var shifts = GetHFShifts();
              //      var modules = GetModules();
                    var applications = db.ApplicationTypes.Where(x => x.IsActive == true && x.ApplicationTypeId <= 2).Select(x => new { x.ApplicationTypeId, x.ApplicationTypeName }).ToList();
                  //  return new ResponseListDTO { Message = "Data Fecthed", List = new { HealthFacilities, zones, hfTypes, shifts, modules, applications } };
                    return new ResponseListDTO { Message = "Data Fecthed", List = new { HealthFacilities, zones, hfTypes, applications } };
                }
            }
            catch (Exception ex)
            {
                long ErrorLogId = await CommonMethods.LogError(ex);
                return new ResponseListDTO { Error = true, StatusCode = (int)System.Net.HttpStatusCode.BadRequest, Message = CommonMethods.serverSideError + ex.InnerException, List = "" };
            }
        }
    }

}
//}
