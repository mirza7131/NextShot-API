using HMIS.Pathalogy.Domain.Models.DTO.PatientLabTestDetailDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.PatientLabTestDto
{
    public class UpdateLabTestResultDto
    {
        public UpdateLabTestResultDto()
        {
            PatientLabTestDetails = new List<CreateOrEditPatientLabTestDetailDto>();
        }
        public Guid PatientLabTestId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public int? LabTestId { get; set; }
        public int? IsEdit { get; set; }

        public bool IsExternal { get; set; } = false;

        public List<CreateOrEditPatientLabTestDetailDto> PatientLabTestDetails { get; set; }
    }
}
