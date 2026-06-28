using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMSSystem.Domain.Models.DTO
{
    internal class DropDownDTO
    {
    }
    public class DivisionDTO
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
    public class DistrictDTO
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
    public class HealthFacilityDTO
    {
        public int HealthFacilityId { get; set; }
        public string? Name { get; set; }

    }
    public class HealthFacilityTypeDTO
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? ShortName { get; set; }

    }
    public class ServiceTypeDTO
    {
        public Guid? Id { get; set; }
        public string? DisplayName { get; set; }

    }
}
