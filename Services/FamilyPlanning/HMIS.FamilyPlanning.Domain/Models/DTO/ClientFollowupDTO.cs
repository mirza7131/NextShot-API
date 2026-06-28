using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.FamilyPlanning.Domain.Models.DTO
{
	public partial class ClientFollowupDTO
	{
		public Guid? FollowupClientId { get; set; }

		public Guid? PatientId { get; set; }

		public Guid? PatientVisitId { get; set; }

		public Guid? MethodInUseProfileId { get; set; }

		public string? SatisfiedWithCurrentMethod { get; set; }

		public string? Reason { get; set; }

		public string? ContinueWithTheSame { get; set; }

		public int? Quantity { get; set; }

		public string? RemovalOfMethod { get; set; }

		public DateTime? RemovalMethodStartDate { get; set; }

		public DateTime? RemovalMethodEndDate { get; set; }

		public Guid? ReasonForRemovalProfileId { get; set; }

		public string? OtherReasonForRemoval { get; set; }

		public Guid? SwitchedMethodProfileId { get; set; }

		public int? SwitchedMethodQuantity { get; set; }

		public DateTime? FollowUpVisitDate { get; set; }
	}

}
