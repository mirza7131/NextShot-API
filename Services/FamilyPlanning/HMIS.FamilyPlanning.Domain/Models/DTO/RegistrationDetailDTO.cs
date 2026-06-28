using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.FamilyPlanning.Domain.Models.DTO
{
	public class RegistrationDetailDTO
	{
		public Guid? RegistrationDetailId { get; set; }

		public Guid? PatientId { get; set; }

		public Guid? PatientVisitId { get; set; }

		public int? HealthFacilityId { get; set; }

		public int? DepartmentLookupId { get; set; }

		public int? SectionLookupId { get; set; }

		public int? RefferedHftypeId { get; set; }

		public int? RefferedHealthFacilityId { get; set; }

		public Guid? ReferredByProfileId { get; set; }

		public Guid? EducationProfileId { get; set; }

		public string? MarriagePassedYear { get; set; } = null!;

		public string? SpouseName { get; set; } = null!;

		public string? SpouseCnic { get; set; } = null!;

		public string? SpouseContactNo { get; set; } = null!;

		public Guid? SpouseEducationProfileId { get; set; }

		public int? TotalNoOfChildren { get; set; }

		public string? MaleAliveChildren { get; set; } = null!;

		public string? FemaleAliveChildren { get; set; } = null!;

		public string? NoOfChildrenDead { get; set; } = null!;

		public Guid? YoungestChildAgeProfileId { get; set; }

		public Guid? GeographicalAccessProfileId { get; set; }
	}

}
