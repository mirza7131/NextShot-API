using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.NCD.Domain.Models.DTO
{
    public class GDMPatientDTO
    {
        public GDMPatientDTO()
        {
            GDMDemograhicViewModel = new GDMDemograhicDTO();
            GDMTrimesterViewModel = new GDMTrimesterDTO();
        }
        public Guid? PatientId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public Guid? GDMPatientID { get; set; }
        public GDMDemograhicDTO GDMDemograhicViewModel { get; set; }
        public GDMTrimesterDTO GDMTrimesterViewModel { get; set; }
    }
}
