namespace HMIS.Aggregator.API.Models
{
    public class HrUserDto
    {
        public int Id { get; set; }
        public string? EmployeeName { get; set; }
        public string? FatherName { get; set; }
        public string? DateOfBirth { get; set; }
        public string? CNIC { get; set; }
        public string? Gender { get; set; }
        public string Province { get; set; }
        public string MobileNo { get; set; }
        public string? HfmisCode { get; set; }
        public string? EMaiL { get; set; }
        public int? WDesignation_Id { get; set; }
        public string? WDesignation_Name { get; set; }
        public string? CurrentGradeBPS { get; set; }
        public string? Status { get; set; }
        public string? StatusName { get; set; }
        public string? Division { get; set; }
        public string? WorkingDivision { get; set; }
        public string? WorkingDivisionCode { get; set; }
        public string? District { get; set; }
        public string? WorkingDistrict { get; set; }
        public string? WorkingDistrictCode { get; set; }
        public string? Tehsil { get; set; }
        public string? WorkingTehsil { get; set; }
        public string? WorkingTehsilCode { get; set; }
        public string? Cadre_Name { get; set; }
        
        public class ResponseHrUserDto
        {
            public string? Message { get; set; }
            public bool Success { get; set; }
            public HrUserDto Data { get; set; }

        }

        public class ResponseListHrUserDto
        {
            public string? Message { get; set; }
            public bool Success { get; set; }
            public List<HrUserDto> Data { get; set; }

        }
    }
}
