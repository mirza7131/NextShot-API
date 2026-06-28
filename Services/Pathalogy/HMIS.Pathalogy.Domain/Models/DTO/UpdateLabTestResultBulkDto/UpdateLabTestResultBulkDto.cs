using HMIS.Pathalogy.Domain.Models.DTO.PatientLabTestDetailDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Pathalogy.Domain.Models.DTO.UpdateLabTestResultBulkDto
{
    public class UpdateLabTestResultBulkDto
    {
        //public UpdateLabTestResultBulkDto()
        //{
        //    PatientLabTestDetails = new List<CreateOrEditPatientLabTestDetailDto>();
        //}
        public UpdateLabTestResultBulkDto()
        {
            Tests = new List<TestListDto>();
        }
        public Guid? PatientLabTestId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public int? LabTestId { get; set; }
        public int? IsEdit { get; set; }

        public bool? IsExternal { get; set; } = false;

        public List<TestListDto> Tests { get; set; }
    }
    public class TestListDto
    {
        //public List<TestDetailsDto> TestDetails { get; set; }
        public TestListDto()
        {
            PatientLabTestDetails = new List<CreateOrEditPatientLabTestDetailDto>();
        }
        public Guid PatientLabTestId { get; set; }
        public Guid? PatientId { get; set; }
        public Guid? PatientVisitId { get; set; }
        public int? LabTestId { get; set; }
        public int? IsEdit { get; set; }

        public bool? IsExternal { get; set; } = false;
        public string BarcodeNo { get; set; }
        public string? PreGenratedBarcodeNo { get; set; }
        public string? BatchNumber { get; set; }
        public string? Result { get; set; }
        public string? TestName { get; set; }
        public int? ViralLoad { get; set; }
        public List<CreateOrEditPatientLabTestDetailDto> PatientLabTestDetails { get; set; }

    }
    //public class TestDetailsDto
    //{
    //    public string BarcodeNo { get; set; }
    //    public bool? IsActive { get; set; }
    //    public string Result { get; set; }
    //    public string TestName { get; set; }
    //    public string ValueName { get; set; }
    //}
}
