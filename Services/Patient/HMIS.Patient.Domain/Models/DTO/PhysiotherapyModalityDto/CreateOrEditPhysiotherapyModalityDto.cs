using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PhysiotherapyModalityDto
{
    public class CreateOrEditPhysiotherapyModalityDto
    {
        public Guid? PhysiotherapyModalitiesId { get; set; }

        public Guid PhysiotherapyFormId { get; set; }

        public int? DepartmentLookupId { get; set; }

        public Guid? ModalitiesProfileId { get; set; }

        public string? Name { get; set; }

        public string? Value { get; set; }

        public bool IsActive { get; set; }
    }
}
