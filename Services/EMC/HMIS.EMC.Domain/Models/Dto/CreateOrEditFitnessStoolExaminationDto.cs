using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class CreateOrEditFitnessStoolExaminationDto
    {
        public Guid? FitnessStoolExaminationId { get; set; }
        public string? Colour { get; set; }

        public string? Consistency { get; set; }

        public decimal? Mucus { get; set; }

        public decimal? Blood { get; set; }

        public decimal? PussCells { get; set; }

        public decimal? Ova { get; set; }

        public decimal? Rbcs { get; set; }

        public decimal? VegetativeForms { get; set; }

        public decimal? OccultBlood { get; set; }

        public string? XrayChestPaview { get; set; }
    }
}
