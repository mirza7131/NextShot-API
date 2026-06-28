using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.MedicineLookupDto
{
    public class CreateOrEditMedicineLookupDto
    {
        public int? MedicineLookupId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? AvailableQuantity { get; set; }

        public bool IsActive { get; set; }
    }
}
