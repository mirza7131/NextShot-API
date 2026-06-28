using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.NCD.Domain.Models.DTO
{
    public class GDMDemograhicDTO
    {
        public string? PregencyType { get; set; }
        public string? IsPregnancy { get; set; }
        public string? RiskFactors { get; set; }
        public string? IsOnRisk { get; set; }
        public string? PlanningPregnancy { get; set; }
        public int? TotalFollowups { get; set; }
    }
}
