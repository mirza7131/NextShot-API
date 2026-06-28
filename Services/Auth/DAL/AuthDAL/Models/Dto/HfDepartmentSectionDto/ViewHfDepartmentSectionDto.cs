using AuthDAL.Models.Dto.SectionLookupDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.HfDepartmentSectionDto
{
    public class ViewHfDepartmentSectionDto
    {
        public int HfDepartmentSectionId { get; set; }
        public int? HfDepartmentId { get; set; }
        public int? DepartmentLookupId { get; set; }
        public string? DepartmentName { get; set; }
        public int? SectionLookupId { get; set; }
        public string? SectionName { get; set; }
        public string? VitalsFloorNo { get; set; }
        public string? VitalsRoomNo { get; set; }
        public string? DoctorFloorNo { get; set; }
        public string? DoctorRoomNo { get; set; }
        public string? PharmacyFloorNo { get; set; }
        public string? PharmacyRoomNo { get; set; }
        public string? PathalogyFloorNo { get; set; }
        public string? PathalogyRoomNo { get; set; }

        public string? AlmonerFloorNo { get; set; }
        public string? AlmonerRoomNo { get; set; }
        public int? BedQuantityInWard { get; set; }
        public bool IsActive { get; set; }
        public virtual ViewSectionLookupDto? SectionLookup { get; set; }

    }

    public class ViewHfDepartmentSectionFloorRoomDto
    {
        public int HfDepartmentSectionId { get; set; }
        public int? HfDepartmentId { get; set; }
        public int? SectionLookupId { get; set; }
        public string? VitalsFloorNo { get; set; }
        public string? VitalsRoomNo { get; set; }
        public string? DoctorFloorNo { get; set; }
        public string? DoctorRoomNo { get; set; }
        public string? PharmacyFloorNo { get; set; }
        public string? PharmacyRoomNo { get; set; }
    


    }

}
