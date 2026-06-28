using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.NCD.Domain.Models.DTO
{
    public class CVC_AssessmentViewModel
    {
        public string? MarriedAge { get; set; }
        public int? ScoreMarriedAge { get; set; }
        public string? Noofchildren { get; set; }
        public int? ScoreNoofchildren { get; set; }
        public string? Oralcontraceptive { get; set; }
        public int? ScoreOralcontraceptive { get; set; }
        public string? Smoking { get; set; }
        public int? ScoreSmoking { get; set; }
        public string? Morethanonemarriage { get; set; }
        public int? ScoreMorethanonemarriage { get; set; }
        public string? Postcoitalbleeding { get; set; }
        public int? ScorePostcoitalbleeding { get; set; }
        public int? ScoreTotal { get; set; }
        public string? RiskStatus { get; set; }

        public string? LocationOfAcctowhite { get; set; }
        public string? CryotherapyApplied { get; set; }
    }
}
