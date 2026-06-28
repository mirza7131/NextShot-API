using HMIS.Dashboard.Domain.Models.DTO.DashboardDto.AdminReferedDashboard;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.DashboardDto.DoctorDashboard
{

    #region Patient Doctor Visit Count By User
    public class PatientVisitDoctorCountByUser
    {
        public Guid? extra { get; set; }
        public string? name { get; set; }
        public int? value { get; set; }
    }

    #endregion

    #region Patient Doctor Visit Count By Province
    public class PatientVisitDoctorCountByProvince
    {
        public string? name { get; set; }
        public int? value { get; set; }
    }

    #endregion

    #region Patient Doctor Visit Count By Gender
    public class PatientVisitDoctorCountByGender
    {
        public string? name { get; set; }
        public int? value { get; set; }

    }
    #endregion

    #region Patient Visit Doctor Count By Disease
    public class PatientVisitDoctorCountByDisease
    {
        public int? HealthFacilityId { get; set; }
        public int? VisitCount { get; set; }
        public decimal? PatientVisitRatio { get; set; }
        public string? DiagnoseName { get; set; }

    }
    #endregion

    #region Doctor Prescription External Medicine Count
    public class DoctorPrescribedMedicineAccumulateQuantity
    {
        public int? ExternalMedicineQuantity { get; set; }
        public int? InternalMedicineQuantity { get; set; }
        public string? MedicineName { get; set; }
        public int? MedicineId { get; set; }
        public int? VisitCount { get; set; }

    }
    #endregion

    #region Doctor Count External Internal Count
    public class PatientCountWithInterExternalMedicines
    {
        public int? InternalPatientCount { get; set; }
        public int? ExternalPatientCount { get; set; }
        public int? IntExtPatientCount { get; set; }
        public int? NotPerscribed { get; set; }
        public int? Prescribed { get; set; }
        public int? Served { get; set; }
        
    }
    public class PatientCountWithInterExternalMedicinesByDoctor
    {
        public int? InternalPatientCountDoctor { get; set; }
        public int? ExternalPatientCountDoctor { get; set; }
        public int? IntExtPatientCountDoctor { get; set; }
        public int? NotPerscribedDoctor { get; set; }
        public int? PrescribedDoctor { get; set; }
        public int? ServedDoctor { get; set; }

    }
    #endregion

    #region Doctor Recommended Lab Test Count
    public class DoctorRecommendedLabTestCount
    {
        public int? ExternalLabCount { get; set; }
        public int? InternalLabCount { get; set; }
        public string? TestName { get; set; }
        public int? VisitCount { get; set; }

    }
    #endregion

    #region  Lab Test Count Internal And External Patient
    public class InternalAndExternalLabCounts
    {
        public int? InternalLabVisitCount { get; set; }
        public int? ExternalLabVisitCount { get; set; }
        public int? InternalExternalLabVisitCount { get; set; }
        public int? TotalLabVisitcount { get; set; }
        public int? InQueueCount { get; set; }
    }
    #endregion

    #region Doctor Dashboard Card Counts

    public class DoctorDashboardCardCount
    {
        public int? PatientReferNoneVisitClose { get; set; }
        public int? PatientReferPharmacyVisitClose { get; set; }

    }
    public class DoctorDashboardHfCardsDTO
    {
        public int? RegistrationCount { get; set; }
        public int? TokenIssuesCount { get; set; }
        public int? VitalCollectedCount { get; set; }
        public int? InQueueCount { get; set; }

    }
    public class DoctorDashboardPatientAllCountsDTO
    {
        public List<DoctorDashboardPatientAllCounts> AllCounts { get; set; }
        public List<PatientServedCountByDoctor> ServedByDoctor { get; set; }
        public List<PatientCountDiseaseWise> DiseaseWiseCount { get; set; }
    }
    public class DoctorDashboardPatientAllCounts
    {
        public int? TotalRegistered { get; set; }
        public int? VitalCollected { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
        public int? Prescribed { get; set; }
        public int? NotPerscribed { get; set; }
        public int? InternalPatientCount { get; set; }
        public int? ExternalPatientCount { get; set; }
        public int? IntExtPatientCount { get; set; }
        public int? TotalLabVisitCount { get; set; }
        public int? InternalLabVisitCount { get; set; }
        public int? ExternalLabVisitCount { get; set; }
        public int? InternalExternalLabVisitCount { get; set; }

    }
    #endregion
    #region Doctor login Stats
    public class DoctorDashboardLoginUserAllCountsDTO
    {
        public int? Served { get; set; }
        public int? Prescribed { get; set; }
        public int? NotPerscribed { get; set; }
    }
    #endregion
    #region Patient Served By Doctor 
    public class PatientServedCountByDoctor
    {
        public Guid? extra { get; set; }
        public string? name { get; set; }
        public int? value { get; set; }
        public string? DistrictName { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? DiagnosedBy { get; set; }
        public string? Designation { get; set; }
        public int? PatientCount { get; set; }

    }
    #endregion

    #region Disease Wise Count 
    public class PatientCountDiseaseWise
    {
        public Guid? extra { get; set; }
        public string? name { get; set; }
        public int? value { get; set; }
        public string? DistrictName { get; set; }
        public string? HealthFacilityName { get; set; }
        public string? Disease { get; set; }
        public int? PatientCount { get; set; }

    }
    #endregion



}
