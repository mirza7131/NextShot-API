using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class CreateOrEditFitnessGeneralOrWidalParameterDto
    {
        public Guid? FitnessGeneralParameterId { get; set; }

        public string? Vision { get; set; }

        public decimal? Chest { get; set; }

        public decimal? Height { get; set; }

        public decimal? Weight { get; set; }

        public string? MarkOfIdentification { get; set; }

        public Guid? PregnancyTestProfileId { get; set; }

        public string? Bsr { get; set; }

        public Guid? HivtestProfileId { get; set; }

        public Guid? HepBtestProfileId { get; set; }

        public Guid? HepCtestProfileId { get; set; }

        public string? Remarks { get; set; }

        public decimal? SalmonellaTyphiO { get; set; }

        public decimal? SalmonellaTyphiH { get; set; }

        public decimal? SalmonellaTyphiAo { get; set; }

        public decimal? SalmonellaTyphiAh { get; set; }

        public decimal? SalmonellaTyphiBo { get; set; }

        public decimal? SalmonellaTyphiBh { get; set; }

        public decimal? BpSystolic { get; set; }

        public decimal? BpDiaSystolic { get; set; }

        public bool? IsWidal { get; set; }
    }
}
