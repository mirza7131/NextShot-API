using HMIS.Patient.Domain.Models.DTO.DashboardDto.PatientRegistrationDashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.MLCDashboardDTO
{
    #region MLC Dashboard Counts DTO
    public class MedicolegalDashboardDTO
    {
        public List<MLCDashboardCountsDTO> AllCounts { get; set; }
        public List<MedicolegalHFWiseCountDTO> HFWiseCount { get; set; }
        public List<MedicolegalDayWiseCountDTO> DayWiseCount { get; set; }
        public List<MedicolegalMonthWiseCountDTO> MonthWiseCount { get; set; }
        public List<MedicolegalSeasonWiseCountDTO> SeasonWiseCount { get; set; }
    }
    public class MLCDashboardCountsDTO
    {
        public int? TotalPatientRegistered { get; set; }
        public int? TotalCases { get; set; }
        public int? MedicoLegalExaminations { get; set; }
        public int? PostMortemExaminations { get; set; }
        public int? MLEforSexualViolence { get; set; }
        public int? Doctors { get; set; }
        public int? Male { get; set; }
        public int? Female { get; set; }
        public int? Others { get; set; }
    }

    public class MedicolegalHFWiseCountDTO
    {
        public string DivisionName { get; set; }
        public string DistrictName { get; set; }
        public string TehsilName { get; set; }
        public string HealthFacilityName { get; set; }
        public int? MedicoLegalExaminations { get; set; }
        public int? PostMortemExaminations { get; set; }
        public int? MLEforSexualViolence { get; set; }
    }
    public class MedicolegalDayWiseCountDTO
    {
        public string Day { get; set; }
        public DateTime? Date { get; set; }
        public int? MedicoLegalExaminations { get; set; }
        public int? PostMortemExaminations { get; set; }
        public int? MLEforSexualViolence { get; set; }
    }
    public class MedicolegalMonthWiseCountDTO
    {
        public string Month { get; set; }
        public int? MedicoLegalExaminations { get; set; }
        public int? PostMortemExaminations { get; set; }
        public int? MLEforSexualViolence { get; set; }
    }
    public class MedicolegalSeasonWiseCountDTO
    {
        public string Season { get; set; }
        public int? MedicoLegalExaminations { get; set; }
        public int? PostMortemExaminations { get; set; }
        public int? MLEforSexualViolence { get; set; }
    }
    #endregion

    #region EMC Dashboard Counts DTO
    public class EMCDashboardDTO
    {
        public List<EMCDashboardCountsDTO> AllCounts { get; set; }
        public List<EMCHFWiseCountDTO> HFWiseCount { get; set; }
        public List<EMCDayWiseCountDTO> DayWiseCount { get; set; }
        public List<EMCMonthWiseCountDTO> MonthWiseCount { get; set; }
        public List<EMCSeasonWiseCountDTO> SeasonWiseCount { get; set; }
    }
    public class EMCDashboardCountsDTO
    {
        public int? TotalPatientRegistered { get; set; }
        public int? TotalCases { get; set; }
        public int? BirthCertificate { get; set; }
        public int? DeathCertificate { get; set; }
        public int? FitnessCertificate { get; set; }
        public int? ICVCertificate { get; set; }
        public int? Male { get; set; }
        public int? Female { get; set; }
        public int? Others { get; set; }
    }

    public class EMCHFWiseCountDTO
    {
        public string DivisionName { get; set; }
        public string DistrictName { get; set; }
        public string TehsilName { get; set; }
        public string HealthFacilityName { get; set; }
        public int? BirthCertificate { get; set; }
        public int? DeathCertificate { get; set; }
        public int? FitnessCertificate { get; set; }
        public int? ICVCertificate { get; set; }
    }
    public class EMCDayWiseCountDTO
    {
        public string Day { get; set; }
        public DateTime? Date { get; set; }
        public int? BirthCertificate { get; set; }
        public int? DeathCertificate { get; set; }
        public int? FitnessCertificate { get; set; }
        public int? ICVCertificate { get; set; }
    }
    public class EMCMonthWiseCountDTO
    {
        public string Month { get; set; }
        public int? BirthCertificate { get; set; }
        public int? DeathCertificate { get; set; }
        public int? FitnessCertificate { get; set; }
        public int? ICVCertificate { get; set; }
    }
    public class EMCSeasonWiseCountDTO
    {
        public string Season { get; set; }
        public int? BirthCertificate { get; set; }
        public int? DeathCertificate { get; set; }
        public int? FitnessCertificate { get; set; }
        public int? ICVCertificate { get; set; }
    }
    #endregion
}
