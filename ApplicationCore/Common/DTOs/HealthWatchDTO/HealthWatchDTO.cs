
namespace CommonDTOs.HealthWatchDTO
{
    public class HealthWatchDTO
    {
        public int id { get; set; }
        public string? name{ get; set; }
        public string? fullName{ get; set; }
        public string? hfmisCode{ get; set; }
        public string? divisionCode{ get; set; }
        public string? divisionName{ get; set; }
        public string? districtCode{ get; set; }
        public string? districtName{ get; set; }
        public string? tehsilCode{ get; set; }
        public string? tehsilName{ get; set; }
        public string? categoryCode{ get; set; }
        public string? hfcategoryName{ get; set; }
        public string? hftypeCode{ get; set; }
        public string? hftypeName{ get; set; }
        public int? birthCertificates{ get; set; }
        public int? deathCertificates{ get; set; }
        public int? fitnessCertificates{ get; set; }
        public int? icvCertificates{ get; set; }
        public int? birthCertificatesUploaded{ get; set; }
        public int? deathCertificatesUploaded{ get; set; }
        public int? fitnessCertificatesUploaded{ get; set; }
        public int? icvCertificatesUploaded{ get; set; }



        public class ResponseHealthWatchDTO
        {
            public string err { get; set; }
            public string? message { get; set; }
            public List<HealthWatchDTO> data { get; set; }

        }

    }
}
