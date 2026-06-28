using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.FamilyPlanning.Domain.Models.DTO
{
	public class SurgicalHistoryDTO
	{
		public Guid? SurgicalHistoryId { get; set; }
		public Guid? PatientId { get; set; }
		public bool? IsHistoryOfVaginalBleeding { get; set; }
		public Guid? PreviousBirthProfileId { get; set; }
		public int? NumberOfCsections { get; set; }
	}

}
