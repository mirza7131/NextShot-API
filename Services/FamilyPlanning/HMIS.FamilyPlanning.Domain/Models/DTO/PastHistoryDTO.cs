using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.FamilyPlanning.Domain.Models.DTO
{
	public class PastHistoryDTO
	{
		public Guid? PatientId { get; set; }
		public Guid? PastHistoryId { get; set; }

		public bool? IsPreviousUser { get; set; }

		public Guid? MethodInUseProfileId { get; set; }
	}

}
