using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MIMS.Domain.Models.DTO.MimsLookupsDto
{
    public class CreateOrEditMimsBranchDto
    {
        public Guid? MimsBranchId { get; set; } 
        public int? HealthfacilityId { get; set; }   
        public string? BranchName { get; set; }
        public int? DepartmentLookupId { get; set; }
        public bool? IsActive { get; set; }  

    }
}
