using HMIS.MEAs.Domain.Models.DbModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMIS.MEAs.Domain.Models.DTO.Common;
using Microsoft.EntityFrameworkCore;
using System.Net;
using HMIS.MEAs.Domain.Models.DTO.ProfileModel;



namespace HMIS.MEAs.Service
{
    public class ProfileServices
    {
 
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
                                                l.Hfmiscode,
                                                l.Lvl,
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





        [HttpGet]
        public async Task<ResponseListDTO> GetHealthFacilitiesByDistrict(string DistrictCode)
        {
            try
            {
                using (var db = new MeasallContext())
                {
                    var HealthFacilities = (from hfl in db.HflistModes
                                            join hfz in db.HfZones on hfl.Id equals hfz.HfId into hfzGroup
                                            from hfz in hfzGroup.DefaultIfEmpty()
                                            join z in db.Zones on hfz.ZoneId equals z.ZoneId into zGroup
                                            from z in zGroup.DefaultIfEmpty()
                                            where hfl.IsActive == true
                                                  && hfl.DistrictCode == DistrictCode
                                                  && (hfl.HftypeCode == "011"
                                                      || hfl.HftypeCode == "012"
                                                      || hfl.HftypeCode == "013"
                                                      || hfl.HftypeCode == "014")
                                            orderby z.ZoneId
                                            select new
                                            {
                                                hfl.Id,
                                                hfl.Hfmiscode,
                                                HealthFacilityName = hfl.Name,
                                                hfl.FullName,
                                                hfl.TehsilCode,
                                                hfl.TehsilName,
                                                hfl.DistrictCode,
                                                hfl.DistrictName,
                                                hfl.DivisionCode,
                                                hfl.DivisionName,
                                                hfl.HftypeCode,
                                                hfl.HftypeName,
                                                hfl.ModeName,
                                                ZoneId = z != null ? z.ZoneId : (int?)null,
                                                ZoneName = z != null ? z.ZoneName : null
                                            }





                                            //from l in db.HflistModes
                                            //                        where l.IsActive == true
                                            //                        && (l.HftypeCode == "011" || l.HftypeCode == "012" || l.HftypeCode == "013" || l.HftypeCode == "014")
                                            //                        && l.DistrictCode == DistrictCode

                                            //                        select new
                                            //                        {
                                            //                            l.Id,
                                            //                            l.Hfmiscode,
                                            //                            HealthFacilityName = l.Name,
                                            //                            l.FullName,
                                            //                            l.TehsilCode,
                                            //                            l.TehsilName,
                                            //                            l.DistrictCode,
                                            //                            l.DistrictName,
                                            //                            l.DivisionCode,
                                            //                            l.DivisionName,
                                            //                            l.HftypeCode,
                                            //                            l.HftypeName,
                                            //                            l.ModeName

                                            //                        }
                                            ).ToList();
                    var MEAsList = (from l in db.Users
                                    where l.IsActive == true && l.Designation == "MEA"
                                    && l.LocationCode.StartsWith(DistrictCode)

                                    select new
                                    {
                                        l.UserId,
                                        l.LocationCode,
                                        l.FullName,
                                        l.Username,
                                        l.Designation

                                    }).ToList();

                    return new ResponseListDTO { Message = "Data Fecthed", List = new { HealthFacilities, MEAsList } };
                }
            }
            catch (Exception ex)
            {
                long ErrorLogId = await CommonMethods.LogError(ex);
                return new ResponseListDTO { Error = true, StatusCode = (int)HttpStatusCode.BadRequest, Message = "serverSideError" + ex.InnerException, List = "" };
            }
        }


        public async Task<ResponseListDTO> IndicatorDetailData()
        {
            try
            {
                using (var db = new MeasallContext())
                {
                    var districts = db.ViewLocationMeas.Where(x => x.Lvl == "District").Select(x => new { x.DistrictCode, x.DistrictName }).ToList();
                    var hfTypes = db.ApplicationHftypes.Where(x => x.HfType.IsActive == true && x.ApplicationId == 1).Select(x => new { x.ApplicationHfTypeId, x.HfType.FaciltyTypeName }).ToList();
                    var shiftsHfTypes = db.Hfshifts.Select(x => new
                    {
                        x.HftypeId,
                        x.ShiftId,
                        x.Shift.ShiftName
                    }).ToList();
                    return new ResponseListDTO { Message = "Data Fetched", List = new { districts, hfTypes, shiftsHfTypes } };
                }
            }
            catch (Exception ex)
            {
                long ErrorLogId = await CommonMethods.LogError(ex);
                return new ResponseListDTO { Error = true, StatusCode = (int)HttpStatusCode.BadRequest, Message = MessageEnum.serverSideError + ex.InnerException, List = "" };
            }
        }
        [HttpPost]
        public async Task<ResponseDTO> SavePackagesDetail(BundalDetailDTO bundle)
        {
            using (var db = new MeasallContext())
            {
                try
                {
                    bool isUpdate = false;

                    var entry = db.MeasBundals.FirstOrDefault(c => c.DistrictCode == bundle.DistrictCode && c.Month == bundle.Month && c.Year == bundle.Year);

                    if (entry != null)
                    {
                        if (entry.Id > 0)
                        {
                            isUpdate = true;

                            var existbundle = db.MeasBundals.FirstOrDefault(x => x.Id == entry.Id && x.IsActive == true && x.IsDelete == false);
                            if (existbundle != null)
                            {
                                foreach (var packages in bundle.PackagesDetail)
                                {
                                    var measpkg = db.MeasPakages.FirstOrDefault(x => x.Id == packages.Id && x.IsActive == true && x.IsDelete == false);
                                    if (measpkg != null)
                                    {
                                        MeasPakage measpackk = new MeasPakage();
                                        measpkg.IsActive = false;
                                        measpkg.IsDelete = true;
                                        measpkg.UpdatedBy = bundle.UpdatedBy;
                                        measpkg.UpdatedOn = DateTime.Now;
                                        db.Entry(measpkg).State = EntityState.Modified;
                                        db.SaveChanges();

                                        MeasPakage measpack = new MeasPakage();
                                        measpack.BundleId = packages.BundleId;
                                        measpack.PackName = packages.PackName;
                                        measpack.IsActive = true;
                                        measpack.IsDelete = false;
                                        measpack.CreatedOn = DateTime.Now;
                                        measpack.CreatedBy = packages.CreatedBy;
                                        db.MeasPakages.Add(measpack);
                                        db.SaveChanges();



                                        var hfpack = packages.HFPackage.Where(xx => xx.HFid > 0).ToList();
                                        var hfpackuser = packages.HFPackage.Where(xx => xx.UserId > 0).FirstOrDefault();

                                        var hfpac = db.Hfpackages.Where(x => x.PackageId == packages.Id).ToList();
                                        if (hfpac.Count > 0)
                                        {
                                            foreach (var hfpa in hfpac)
                                            {
                                                hfpa.IsActive = false;
                                                hfpa.IsDelete = true;
                                                hfpa.UpdatedOn = DateTime.Now;
                                                db.Entry(hfpa).State = EntityState.Modified;
                                                db.SaveChanges();
                                            }
                                        }



                                        var usvi = db.UserVisits.Where(x => x.PackageId == packages.Id && x.IsActive == true).ToList();
                                        if (usvi.Count > 0)
                                        {
                                            foreach (var us in usvi)
                                            {
                                                us.IsActive = false;
                                                db.Entry(us).State = EntityState.Modified;
                                            }
                                            db.SaveChanges();
                                        }



                                        foreach (var pack in packages.HFPackage)
                                        {
                                            //var hfpkg = db.HFPackages.FirstOrDefault(x => x.Id == pack.Id && x.IsActive == true && x.IsDelete == false);
                                            //if (hfpkg != null)
                                            //{
                                            //HFPackage HFPackk = new HFPackage();
                                            //hfpkg.IsActive = false;
                                            //hfpkg.IsDelete = true;
                                            //hfpkg.UpdatedOn = DateTime.Now;
                                            //db.Entry(hfpkg).State = (System.Data.Entity.EntityState)EntityState.Modified;

                                            //db.SaveChanges();

                                            Hfpackage HFPack = new Hfpackage();
                                            HFPack.BundleId = packages.BundleId;
                                            HFPack.PackageId = packages.Id;
                                            HFPack.PackName = packages.PackName;
                                            HFPack.UserId = pack.UserId;
                                            HFPack.PackName = pack.Name;
                                            HFPack.Hfid = pack.HFid;
                                            HFPack.Hfname = pack.HFName;
                                            HFPack.HftypeCode = pack.HFTypeCode;
                                            HFPack.IsActive = true;
                                            HFPack.IsDelete = false;
                                            HFPack.CreatedOn = DateTime.Now;
                                            HFPack.CreatedBy = pack.CreatedBy;
                                            db.Hfpackages.Add(HFPack);
                                            db.SaveChanges();

                                            List<UserVisit> userVisitList = new List<UserVisit>();
                                            var shifts = (from l in db.HflistModes
                                                          join h in db.HealthFacilityTypes on l.ModeName equals h.FaciltyTypeName
                                                          join s in db.Hfshifts on h.FacilityTypeId equals s.HftypeId
                                                          where l.Id == pack.HFid
                                                          select s.ShiftId).ToList();






                                            if (shifts != null && shifts.Count > 0)
                                            {


                                                foreach (var shift in shifts)
                                                {

                                                    UserVisit uV = new UserVisit();
                                                    // uV.CreatedBy = userId.ToString();
                                                    uV.CreatedDate = DateTime.Now;
                                                    uV.Month = bundle.Month;
                                                    uV.Year = bundle.Year.ToString();
                                                    uV.HfId = pack.HFid;
                                                    uV.ApplicationTypeId = 1;
                                                    uV.IsActive = true;
                                                    uV.IsVisited = false;
                                                    uV.ShiftId = shift;
                                                    uV.UserId = hfpackuser.UserId;
                                                    uV.PackageId = measpack.Id;
                                                    //uV.ZoneId = user.ZoneId;
                                                    uV.IsRepeat = false;
                                                    uV.IsSpecial = false;
                                                    db.UserVisits.Add(uV);
                                                    userVisitList.Add(uV);



                                                }
                                            }
                                            //}


                                            db.SaveChanges();
                                        }
                                    }

                                }
                            }
                        }
                    }

                    else
                    {
                        MeasBundal measBundal = new MeasBundal();
                        isUpdate = false;
                        measBundal.DivisionCode = bundle.DivisionCode;
                        measBundal.DivisionName = bundle.DivisionName;
                        measBundal.DistrictCode = bundle.DistrictCode;
                        measBundal.DistrictName = bundle.DistrictName;
                        measBundal.Month = bundle.Month;
                        measBundal.Year = bundle.Year;
                        measBundal.IsActive = true;
                        measBundal.IsDelete = false;
                        measBundal.CreatedOn = DateTime.Now;
                        measBundal.CreatedBy = bundle.CreatedBy;
                        db.MeasBundals.Add(measBundal);
                        db.SaveChanges();

                        foreach (var packages in bundle.PackagesDetail)
                        {
                            var hfpack = packages.HFPackage.Where(xx => xx.HFid > 0).ToList();
                            var hfpackuser = packages.HFPackage.Where(xx => xx.UserId > 0).FirstOrDefault();

                            MeasPakage measpack = new MeasPakage();
                            measpack.BundleId = measBundal.Id;
                            //measpack.UserId = hfpackuser.UserId;
                            //measpack.AssignName = hfpackuser.Name;
                            measpack.PackName = packages.PackName;
                            measpack.IsActive = true;
                            measpack.IsDelete = false;
                            measpack.CreatedOn = DateTime.Now;
                            measpack.CreatedBy = packages.CreatedBy;
                            db.MeasPakages.Add(measpack);
                            db.SaveChanges();

                            foreach (var pack in packages.HFPackage)
                            {
                                Hfpackage HFPack = new Hfpackage();
                                HFPack.BundleId = measBundal.Id;

                                HFPack.PackageId = measpack.Id;
                                HFPack.PackName = measpack.PackName;
                                HFPack.UserId = pack.UserId;
                                HFPack.Name = pack.Name;
                                //HFPack.HFid = pack.HFid;
                                //HFPack.HFName = pack.HFName;
                                //HFPack.HFTypeCode = pack.HFTypeCode;
                                HFPack.IsActive = true;
                                HFPack.IsDelete = false;
                                HFPack.CreatedOn = DateTime.Now;
                                HFPack.CreatedBy = pack.CreatedBy;
                                db.Hfpackages.Add(HFPack);
                                db.SaveChanges();
                                List<UserVisit> userVisitList = new List<UserVisit>();
                                var shifts = (from l in db.HflistModes
                                              join h in db.HealthFacilityTypes on l.ModeName equals h.FaciltyTypeName
                                              join s in db.Hfshifts on h.FacilityTypeId equals s.HftypeId
                                              where l.Id == pack.HFid
                                              select s.ShiftId).ToList();
                                if (shifts != null && shifts.Count > 0)
                                {
                                    foreach (var shift in shifts)
                                    {
                                        UserVisit uV = new UserVisit();
                                        // uV.CreatedBy = userId.ToString();
                                        uV.CreatedDate = DateTime.Now;
                                        uV.Month = bundle.Month;
                                        uV.Year = bundle.Year.ToString();
                                        uV.HfId = pack.HFid;
                                        uV.ApplicationTypeId = 1;
                                        uV.IsActive = true;
                                        uV.IsVisited = false;
                                        uV.ShiftId = shift;
                                        uV.UserId = hfpackuser.UserId;
                                        uV.PackageId = measpack.Id;
                                        //uV.ZoneId = user.ZoneId;
                                        uV.IsRepeat = false;
                                        uV.IsSpecial = false;
                                        db.UserVisits.Add(uV);
                                        userVisitList.Add(uV);
                                    }
                                }

                                db.SaveChanges();
                            }

                        }



                    }
                    if (isUpdate)
                    {
                        return new ResponseDTO
                        {
                            Error = false,
                            Message = MessageEnum.updateSuccess,
                            Data = bundle
                        };
                    }
                    else
                    {
                        return new ResponseDTO
                        {
                            Error = false,
                            Message = MessageEnum.saveSuccess,
                            Data = bundle
                        };
                    }
                }
                catch (Exception ex)
                {
                    await CommonMethods.LogError(ex);
                    return new ResponseDTO { Error = true, StatusCode = (int)HttpStatusCode.BadRequest, Message = MessageEnum.serverSideError + ex.InnerException, Data = "" };
                }
            }
        }















    }
}
