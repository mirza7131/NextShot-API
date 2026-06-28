using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.DashboardDto.PathologyDashboardPathologyDashboard
{

    #region Patient Lab Test Sample Collected Count By User
    public class PatientVisitLabTestSampleCollectedCountByUser
    {
        public Guid? extra { get; set; }
        public string? name { get; set; }
        public int? value { get; set; }
    }

    #endregion

    #region Patient Lab Test Report Generated Count By User
    public class PatientVisitLabTestReportGeneratedCountByUser
    {
        public Guid? extra { get; set; }
        public string? name { get; set; }
        public int? value { get; set; }
    }

    #endregion

    #region Pathology Dashboard Stats 
    public class PathologyDashboardStats
    {
        public int PatientsCount { get; set; }
        public int? LabTestCount { get; set; }
        public decimal LabTestAmount { get; set; }
        public int? ResultPedningMoreThan3Days { get; set; }
        public int? ResultPedningMoreThan7Days { get; set; }
        public int? ResultPedningMoreThan15Days { get; set; }
        public int? ResultPedningMoreThan30Days { get; set; }

    }

    #endregion

    #region Lab Test Count Vs Patients Count
    public class LabTestCountVsPatientCount
    {
        public int PatientsCount { get; set; }
        public int? LabTestCount { get; set; }
        public decimal LabTestAmount { get; set; }
        public string? LabTestName { get; set; }
        public string? LabType { get; set; }
    }
    public class InternalLabTestCountVsPatientCount
    {
        public int PatientsCount { get; set; }
        public int? LabTestCount { get; set; }
        public decimal LabTestAmount { get; set; }
        public string? LabTestName { get; set; }
    }


    public class ExternalLabTestCountVsPatientCount
    {
        public int PatientsCount { get; set; }
        public int? LabTestCount { get; set; }
        public decimal LabTestAmount { get; set; }
        public string? LabTestName { get; set; }
    }
    public class ExternalLabCountVsPatientCount
    {
        public List<InternalLabTestCountVsPatientCount> InternalLabTestCountVsPatientCount { get; set; }
        public List<ExternalLabTestCountVsPatientCount> ExternalLabTestCountVsPatientCount { get; set; }
    }


    #endregion
    #region Top 20 Test Recommended
    public class InternalExternalCountByLabTestDTO
    {
        public int? TestCount { get; set; }
        public string? LabType { get; set; }
        public string? LabTestName { get; set; }
        public int? LabTestId { get; set; }
    }
    #endregion

    #region Patient Pathology Visit Count By Gender
    public class PatientVisitPathologyCountByGender
    {
        public string? name { get; set; }
        public int? value { get; set; }

    }
    #endregion

    #region Patient Pathology Visit Count By Lab Test
    public class PatientVisitPathologyCountByLabTest
    {
        public string? name { get; set; }
        public int? value { get; set; }

    }
    #endregion

    #region Patient Pathology Lab Test Count By Department
    public class PatientVisitPathologyCountByDepartment
    {
        public string? name { get; set; }
        public int? value { get; set; }

    }
    #endregion

    #region Patient Pathology Lab Test Count By Status
    public class PathologyCountByStatus
    {
        public int? SampleNotCollected { get; set; } = 0;
        //public int? SampleCollected { get; set; } = 0;
        public int? ResultAwaited { get; set; } = 0;
        public int? ReportGenerated { get; set; } = 0;

    }
    #endregion

    public class PathologyCardsCount
    {
        public int InternalLabVisitCount { get; set; }
        public int SampleCollected { get; set; }
        public int InQueue { get; set; }
        public int ReportGernated { get; set; }
        public int PendingReport { get; set; }
    }

    public class LabDashboardTestViewModelDTO
    {
        public List<LabDashboardTestAllCountsDTO> AllCounts { get; set; }
        public List<SampleCollected> SampleCollected { get; set; }

    }
    public class LabDashboardTestAllCountsDTO
    {
        public int TotalLabTestCount { get; set; }
        public int InternalLabTestCount { get; set; }
        public int ExternalLabTestCount { get; set; }
        public int InternalExternalLabTestCount { get; set; }
        public int SampleTobeCollected { get; set; }
        public int SampleCollected { get; set; }
        public int SamplePendingTobeCollected { get; set; }
        public int ReportGenerated { get; set; }
        public int PendingReports { get; set; }
        public int SampleRejected { get; set; }

    }
    public class SampleCollected
    {
        public int SampleCollectedCount { get; set; }
        public string TestName { get; set; }

    }

    public class LabDashboardFromStartTillNowAllCountsDTO
    {
        public DateTime LabStartedDate { get; set; }
        public int TotalLabTestCount { get; set; }
        public int InternalLabTestCount { get; set; }
        public int ExternalLabTestCount { get; set; }
        public int InternalExternalLabTestCount { get; set; }
        public int SampleTobeCollected { get; set; }
        public int SampleCollected { get; set; }
        public int SamplePendingTobeCollected { get; set; }
        public int ReportGenerated { get; set; }
        public int PendingReports { get; set; }
        public int SampleRejected { get; set; }
        public int PendingReportInThreeDays { get; set; }
        public int PendingReportInSevenDays { get; set; }
        public int PendingReportIn15Days { get; set; }
        public int PendingReportIn30Days { get; set; }
        public int PendingReportGreater30Days { get; set; }
        public int RejectedInThreeDays { get; set; }
        public int RejectedInSevenDays { get; set; }
        public int RejectedIn15Days { get; set; }
        public int RejectedIn30Days { get; set; }
        public int RejectedGreater30Days { get; set; }
        public int PendingSampleInThreeDays { get; set; }
        public int PendingSampleInSevenDays { get; set; }
        public int PendingSampleIn15Days { get; set; }
        public int PendingSampleIn30Days { get; set; }
        public int PendingSampleGreater30Days { get; set; }

    }
    #region patholgy User login counts
    public class LabDashboardLoginUserTestAllCountsDTO
    {
        public int SampleCollected { get; set; }
        public int ReportGenerated { get; set; }
        public int PendingReports { get; set; }
        public int SampleRejected { get; set; }

    }
    #endregion
}
