using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.PatientLabTestDto
{
    public class AssignRiderDto
    {
        public Guid PatientLabTestId { get; set; }
        public Guid? RiderUserId { get; set; }
    }
}
