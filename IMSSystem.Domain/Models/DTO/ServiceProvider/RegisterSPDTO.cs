using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSSystem.Domain.Models.DTO.ServiceProvider
{
    public class RegisterSPDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public Guid? StakeHolderTypeId { get; set; }
        public string? Name { get; set; }
        public string? Logo { get; set; }
        public string? Banner { get; set; }
        public string? OwnerName { get; set; }
        public string? OwnerCnic { get; set; }
        public string? OwnerMob { get; set; } 
        public string? DivisionCode { get; set; }
        public string? DistrictCode { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true; 
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow; 
        public List<SpServiceDto> SpServices { get; set; } = new(); 
    }

    public class SpServiceDto
    {
        public Guid? Id { get; set; } = Guid.NewGuid(); 
        public Guid? ServiceTypeId { get; set; }
        public bool IsActive { get; set; } = true; 
    }
}
