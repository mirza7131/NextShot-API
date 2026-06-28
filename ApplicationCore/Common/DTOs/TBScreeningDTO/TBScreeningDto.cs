
using System.Xml.Linq;

namespace CommonDTOs.TBScreeningDTO
{
    public class ResponseTBScreeningDto
    {
        public string? Message { get; set; }
        public bool Status { get; set; }
        public data data { get; set; }

    }
    public class data
    {
        public List<table> table { get; set; }
        public List<table1> table1 { get; set; }
        public List<table2> table2 { get; set; }

    }

    public class table
    {


        public string? userName { get; set; }
        public string? districtName { get; set; }
        public string? districtCode { get; set; }
        public string? hfmisCode { get; set; }
        public string? fullName { get; set; }
        public int? tbPresumptivesIdentified { get; set; }
        public int? tbPatientsRegistered { get; set; }
    }
    
    public class table1
    {


        public string? userName { get; set; }
        public string? districtName { get; set; }
        public string? districtCode { get; set; }
        public string? hfmisCode { get; set; }
        public string? fullName { get; set; }
        public int? screenedForHIV { get; set; }
        public int? hivReactive { get; set; }
    }
    public class table2
    {


        public string? userName { get; set; }
        public string? districtName { get; set; }
        public string? districtCode { get; set; }
        public string? hfmisCode { get; set; }
        public string? fullName { get; set; }
        public int? tbPresumptivesMicroscopyTested { get; set; }
        public int? tbPresumptivesXRayTested { get; set; }
        public int? geneXpertTesting { get; set; }
        public int? rifampicinResistanceDetected { get; set; }
    }
}
