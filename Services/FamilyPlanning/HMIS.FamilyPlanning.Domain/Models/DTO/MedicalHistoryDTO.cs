using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.FamilyPlanning.Domain.Models.DTO
{
	public class MedicalHistoryDTO
	{
		public Guid? MedicalHistoryId { get; set; }
		public Guid? PatientId { get; set; }
		public bool? IsDiabetic { get; set; }

		public bool? IsHypertension { get; set; }

		public bool? IsMigraine { get; set; }

		public bool? IsSmoking { get; set; }

		public bool? IsBreastfeeding { get; set; }


		public Guid? LastDeliveryProfileId { get; set; }

		public Guid? MiscarriagesProfileId { get; set; }

		public Guid? AbortionProfileId { get; set; }

		public string? PelvicInflamatoryDisease { get; set; }
		public string? Investigations { get; set; }
		public string? OtherInvestigations { get; set; }
	}

}
