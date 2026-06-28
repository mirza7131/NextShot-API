using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Dashboard.Domain.Models.DTO.DashboardDto.PharmacyDashboard
{
    #region Patient Pharmacy Visit Count By User
    public class PatientVisitPharmacyCountByUser
    {
        public Guid? extra { get; set; }
        public string? name { get; set; }
        public int? value { get; set; }
    }

    #endregion


    #region Pharmacy Internal External Medicine Count
    public class PharmacyInternalExternalMedicineAccumulateQuantity
    {
        public int? ExternalMedicineQuantity { get; set; }
        public int? InternalMedicineQuantity { get; set; }
        public string? MedicineName { get; set; }
        public int? VisitCount { get; set; }
        public int? MedicineId { get; set; }

    }
    public class PharmacyDashboardMedcineIssuedReport    {
     
        public int? InternalMedicineQuantity { get; set; }
        public string? MedicineName { get; set; }
        public int? VisitCount { get; set; }
        public int? MedicineId { get; set; }

    }
    public class PharmacyDashboardPatientDetailDTO
    {

        public Guid PatientVisitId { get; set; }
        public string? MedicineName { get; set; }
        public string? FullName { get; set; }
        public string? MRNo { get; set; }
        public int? InternalMedicineQuantity { get; set; }
        public Guid? PatientId { get; set; }
        public int? MedicineId { get; set; }
        public string? MobileNo { get; set; }
        public string? CNIC { get; set; }

    }
    public partial class MedicineStockOfflineDTO
    {
        public Guid MimsMedicineDataId { get; set; }
        public int? MedicineId { get; set; }
        public string? MedicineName { get; set; }
        public int? MedicineTypeId { get; set; }
        public string? MedicineTypeName { get; set; }
        public int? WardId { get; set; }
        public string? WardName { get; set; }
        public decimal? AvailableQuantity { get; set; }
        public decimal? TotalQuantity { get; set; }
        public decimal? TotalDispatchQuantity { get; set; }
        public int? HealthFacilityId { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool? isOffline { get;set; }
    }


    #endregion
    #region  Medicine Count Patient Wise

    public class PharmacyMedicinePatientWiseReport
    {
        public int? ExternalMedicineQuantity { get; set; }
        public int? InternalMedicineQuantity { get; set; }
        public string? MedicineName { get; set; }
        public int? VisitCount { get; set; }
        public int? MedicineId { get; set; }

        public string? FullName { get; set; }
        public string? CNIC { get; set; }
        public string? MobileNo { get; set; }
        public string? MRNo { get; set; }
    }
    #endregion

    #region Pharmacy Dashboard Card Counts

    public class PharmacyDashboardCardCount
    {
        public int PatientsCount { get; set; }
        public int InternalMedicineCount { get; set; }
        public int ExternalMedicineCount { get; set; }
        public decimal TotalAmount { get; set; }

    }
    public class PharmacyCardsCount
    {
        public int TokenIssuesCount { get; set; }
        public int RegistrationCount { get; set; }
        public int Prescribed { get; set; }
        public int MedicineIssue { get; set; }
        public int InQueue { get; set; }


    }
    #endregion

    #region Pharmacy Dashboard Internal External Stats 
    public class PharmacyDashboardInternalExternalStats
    {
        public int InternalMedicinePatientsCount { get; set; }
        public int ExternalMedicinePatientsCount { get; set; }
        public int? ExternalMedicineQuantity { get; set; }
        public int? InternalMedicineQuantity { get; set; }
        public int ExternalMedicineCount { get; set; }
        public int InternalMedicineCount { get; set; }
        public decimal TotalAmount { get; set; }
    }

    #endregion

    #region Pharmacy Dashboard Patient All Counts
    public class PharmacyDashboardPatientAllCountsDTO
    {
        public int? TotalRegistered { get; set; }
        public int? Served { get; set; }
        public int? NotServed { get; set; }
        public int? Prescribed { get; set; }
        public int? NotPerscribed { get; set; }
        public int? InternalPatientCount { get; set; }
        public int? ExternalPatientCount { get; set; }
        public int? IntExtPatientCount { get; set; }
        public int? MedicineToBeIssued { get; set; }
        public int? MedicineIssued { get; set; }
        public int? MedicineNotIssued { get; set; }

    }
    #endregion

    #region Pharmacy Dashboard Login User All Counts
    public class PharmacyDashboardLoginUserAllCountsDTO
    {
        public int? MedicineIssued { get; set; }
    }
    #endregion
}
