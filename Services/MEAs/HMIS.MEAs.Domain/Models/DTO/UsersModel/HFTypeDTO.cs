using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.UsersModel
{
    public class HFTypeDTO
    {
        public int FacilityTypeId { get; set; }
        public string FaciltyTypeName { get; set; }
        public int CategoryId { get; set; }
        public List<int> ApplicationTypeIds { get; set; }
    }


    public class AmbulanceDTO
    {
        public int Id { get; set; }
        public int? HfId { get; set; }
        public string hfmiscode { get; set; }
        public string District { get; set; }
        public string HealthFacilityName { get; set; }
        public string ModeName { get; set; }
        public string lvl { get; set; }
        public string AmbulanceNo { get; set; }
        public string DHISCode { get; set; }

    }


    public class CampsDTO
    {
        public int Camp_Id { get; set; }
        public string Camp_Name { get; set; }
        public string Site { get; set; }
        public string DistrictCode { get; set; }
        public string DistrictName { get; set; }
        public string TehsilCode { get; set; }
        public string TehsilName { get; set; }
        public int? Shift_Id { get; set; }
        public bool? IsActive { get; set; }

    }
}
