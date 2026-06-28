using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.ProfileModel
{
    internal class ProfileModel
    {

    }

    public class BundalDetailDTO
    {
        public int Id { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string Month { get; set; }
        public int? Year { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public List<PackagesDetailDTO> PackagesDetail { get; set; }


    }

    public class PackagesDetailDTO
    {
        public int Id { get; set; }
        public int? BundleId { get; set; }
        public string PackName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public List<HFPackageDTO> HFPackage { get; set; }
    }

    public class HFPackageDTO
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Name { get; set; }
        public int? BundleId { get; set; }
        public int? PackageId { get; set; }
        public string PackName { get; set; }
        public int? HFid { get; set; }
        public string HFName { get; set; }
        public string HFTypeCode { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }
    }



}
