using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.FamilyPlanning.Domain.Models.DTO
{
	public class CounsellingAndProvisionDTO
	{
		public Guid CounslingAndProvisionalId { get; set; }
		public Guid? PatientId { get; set; }
		public Guid? PatientVisitId { get; set; }
		public bool? IsUseFpwheelCard { get; set; }
		public bool? IsCounselThePatient { get; set; }
		public Guid? MethodProposedProfileId { get; set; }
		public Guid? MethodClientProfileId { get; set; }
		public Guid? MethodAdoptedProfileId { get; set; }
		public Guid? ReasonProfileId { get; set; }
		public string? Remarks { get; set; }
		public DateTime? FollowUpVisitDate { get; set; }
	}

}
