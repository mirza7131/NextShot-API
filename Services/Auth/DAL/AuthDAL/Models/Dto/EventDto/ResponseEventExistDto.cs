using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.Dto.EventDto
{
    public class ResponseEventExistDto
    {
        public Guid? EventId { get; set; }
        public string? HealthFacilityTypeCode { get; set; } 
    }
}
