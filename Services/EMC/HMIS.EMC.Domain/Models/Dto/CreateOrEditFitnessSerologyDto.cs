using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class CreateOrEditFitnessSerologyDto
    {
        public Guid? FitnessSerologyId { get; set; }

        public string? SerologyTestOne { get; set; }

        public Guid? SerologyTestOneValueTypeProfileId { get; set; }

        public string? SerologyTestTwo { get; set; }

        public Guid? SerologyTestTwoValueTypeProfileId { get; set; }
    }
}
