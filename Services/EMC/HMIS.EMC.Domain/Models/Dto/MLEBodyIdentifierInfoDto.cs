using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class MLEBodyIdentifierInfoDto
    {
        public Guid PatientId { get; set; }

        public Guid PatientVisitId { get; set; }
        public Guid? MlcbodyIdentifierInfoId { get; set; }

        public string? IdentifierName { get; set; }

        public string? IdentifierCnic { get; set; }

        public Guid? IdentifierRelationTypeProfileId { get; set; }

        public string? IdentifierComments { get; set; }
    }
}
