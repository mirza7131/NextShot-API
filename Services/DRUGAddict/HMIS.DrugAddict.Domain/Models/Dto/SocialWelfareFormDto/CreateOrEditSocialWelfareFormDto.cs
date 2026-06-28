using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.DrugAddict.Domain.Models.DTO.SocialWelfareFormDto
{
    public class CreateOrEditSocialWelfareFormDto
    {
        public Guid? Id { get; set; }

        public Guid? FormTypeProfileId { get; set; }
        public Guid? PatientVistId { get; set; }

        public string? PatientId { get; set; }

        public string? Mdrcprofession { get; set; }

        public string? MdrcmonthlyIncome { get; set; }

        public string? MdrcfhrdrugAddiction { get; set; }

        public string? MdrcifYesRelation { get; set; }

        public string? MdrcfamilyAttitude { get; set; }

        public string? MdrcpatientAttitude { get; set; }

        public string? Mdrccfodabuse { get; set; }

        public string? MdrcdetailOfCounsellingSessionsSesssionI { get; set; }

        public string? MdrcdetailOfCounsellingSessionsSesssionIi { get; set; }

        public string? MdrcdetailOfCounsellingSessionsSesssionIii { get; set; }

        public string? MdrcmsoprovisionReadingMaterial { get; set; }

        public string? MdrcindoorActivities { get; set; }

        public string? MdrcrecreationalActivities { get; set; }

        public string? MdrcanyOtherMso { get; set; }

        public int? ReferDistrictId { get; set; }
        public int? ReferDivisionId { get; set; }
        public int? SessionNo { get; set; }
        public bool? IsVisitClosed { get; set; }
        public string? Education { get; set; }

        public int? PtPositionInFamily { get; set; }

        public int? NoOfSisters { get; set; }

        public int? NoOfBrothers { get; set; }
    }
}
