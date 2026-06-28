using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class CreateOrEditFitnessUrineCEDto
    {
        public Guid? FitnessUrineCeid { get; set; }

        public string? Color { get; set; }

        public decimal? SpecificGravity { get; set; }

        public decimal? Ph { get; set; }

        public decimal? Protien { get; set; }

        public decimal? Glucose { get; set; }

        public decimal? Ketones { get; set; }

        public decimal? Urobilinogen { get; set; }

        public decimal? PussCells { get; set; }

        public decimal? Rbcs { get; set; }

        public decimal? Crystals { get; set; }

        public decimal? EpethlialCells { get; set; }

        public decimal? Bacteria { get; set; }

        public decimal? Casts { get; set; }
    }
}
