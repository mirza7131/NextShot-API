using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.FamilyPlanning.Domain.Models.DTO
{
	public class ExaminationDTO
	{
		public Guid? ExaminationId { get; set; }
		public Guid? PatientId { get; set; }
		public List<Guid>? ExaminationProfileId { get; set; }
	}
}
