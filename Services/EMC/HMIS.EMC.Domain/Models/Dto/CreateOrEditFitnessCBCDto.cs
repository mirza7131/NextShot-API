using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Domain.Models.Dto
{
    public class CreateOrEditFitnessCBCDto
    {
        public Guid? FitnessCbcid { get; set; }
        public string? Hb { get; set; }
        public string? Mcv { get; set; }
        public string? Hcv { get; set; }
        public decimal? Tlc { get; set; }
        public decimal? Neutorphils { get; set; }
        public decimal? Lymphocytes { get; set; }
        public decimal? Eosinophils { get; set; }
        public decimal? Platelets { get; set; }
        public decimal? Esr { get; set; }
        public decimal? Monocytes { get; set; }
    }
}
