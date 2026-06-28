using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.PatientDto
{
	public class MisealesPatientRecordDTO
	{
		public int HealthFacilityId { get; set; }
		public string FullName { get; set; }
		public string MobileNo { get; set; }
		public string NameOfCnicHolder { get; set; }
		public string Relation { get; set; }
		public string ParmanentAddress { get; set; }
		public string Province { get; set; }
		public string Division { get; set; }
		public string District { get; set; }
		public string Tehsil { get; set; }
		public string TehsilCode { get; set; }
		public DateTime DOB { get; set; }
		public string CNIC { get; set; }
		public string Gender { get; set; }
		public Guid PatientID { get; set; }
		public string HFName { get; set; }
		public string DiseaseName { get; set; }
		public DateTime ReportedDate { get; set; }
	}
}
