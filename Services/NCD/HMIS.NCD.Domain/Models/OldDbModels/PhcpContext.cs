using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class PhcpContext : DbContext
{
    public PhcpContext()
    {
    }

    public PhcpContext(DbContextOptions<PhcpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActiveHf> ActiveHfs { get; set; }

    public virtual DbSet<Administration> Administrations { get; set; }

    public virtual DbSet<ApprovalStatus> ApprovalStatuses { get; set; }

    public virtual DbSet<ApprovedBy> ApprovedBies { get; set; }

    public virtual DbSet<AssessmentMissingId> AssessmentMissingIds { get; set; }

    public virtual DbSet<CampaignSmsExcelLog> CampaignSmsExcelLogs { get; set; }

    public virtual DbSet<CampaignSmsLog> CampaignSmsLogs { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<DashboardCountsApi> DashboardCountsApis { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Division> Divisions { get; set; }

    public virtual DbSet<Driver> Drivers { get; set; }

    public virtual DbSet<ExceptionLog> ExceptionLogs { get; set; }

    public virtual DbSet<FacilityType> FacilityTypes { get; set; }

    public virtual DbSet<HealthFacility> HealthFacilities { get; set; }

    public virtual DbSet<HighPrevelanceDist> HighPrevelanceDists { get; set; }

    public virtual DbSet<HwdOldDatum> HwdOldData { get; set; }

    public virtual DbSet<HwdPatient> HwdPatients { get; set; }

    public virtual DbSet<HwdPatientSearchLog> HwdPatientSearchLogs { get; set; }

    public virtual DbSet<IntegratedLog> IntegratedLogs { get; set; }

    public virtual DbSet<LatlongPatient> LatlongPatients { get; set; }

    public virtual DbSet<MicroScreening> MicroScreenings { get; set; }

    public virtual DbSet<Monitor> Monitors { get; set; }

    public virtual DbSet<MonitringArea> MonitringAreas { get; set; }

    public virtual DbSet<MonitringZone> MonitringZones { get; set; }

    public virtual DbSet<Observation> Observations { get; set; }

    public virtual DbSet<ObservationsFacilityType> ObservationsFacilityTypes { get; set; }

    public virtual DbSet<SchoolVaccination> SchoolVaccinations { get; set; }

    public virtual DbSet<SiteSead> SiteSeads { get; set; }

    public virtual DbSet<SmsForMep> SmsForMeps { get; set; }

    public virtual DbSet<SubMonitringArea> SubMonitringAreas { get; set; }

    public virtual DbSet<TblApiConfig> TblApiConfigs { get; set; }

    public virtual DbSet<TblApiLog> TblApiLogs { get; set; }

    public virtual DbSet<TblApiRec> TblApiRecs { get; set; }

    public virtual DbSet<TblApriLog> TblApriLogs { get; set; }

    public virtual DbSet<TblBaseline> TblBaselines { get; set; }

    public virtual DbSet<TblBaselineStatus> TblBaselineStatuses { get; set; }

    public virtual DbSet<TblBatch> TblBatches { get; set; }

    public virtual DbSet<TblBatchSample> TblBatchSamples { get; set; }

    public virtual DbSet<TblBeauticianEvent> TblBeauticianEvents { get; set; }

    public virtual DbSet<TblBloodBankResult> TblBloodBankResults { get; set; }

    public virtual DbSet<TblBpNormalization> TblBpNormalizations { get; set; }

    public virtual DbSet<TblCallcenter> TblCallcenters { get; set; }

    public virtual DbSet<TblCallcenterStat> TblCallcenterStats { get; set; }

    public virtual DbSet<TblCbcParameter> TblCbcParameters { get; set; }

    public virtual DbSet<TblCompletedIssue> TblCompletedIssues { get; set; }

    public virtual DbSet<TblCronJobsLog> TblCronJobsLogs { get; set; }

    public virtual DbSet<TblDispatch> TblDispatches { get; set; }

    public virtual DbSet<TblDistrict> TblDistricts { get; set; }

    public virtual DbSet<TblDivision> TblDivisions { get; set; }

    public virtual DbSet<TblDrugInteractionMedSec> TblDrugInteractionMedSecs { get; set; }

    public virtual DbSet<TblDrugInteractionMedSecBk31Jan2020> TblDrugInteractionMedSecBk31Jan2020s { get; set; }

    public virtual DbSet<TblEvent> TblEvents { get; set; }

    public virtual DbSet<TblEventIndicator> TblEventIndicators { get; set; }

    public virtual DbSet<TblFamilyAssesment> TblFamilyAssesments { get; set; }

    public virtual DbSet<TblFinalStock> TblFinalStocks { get; set; }

    public virtual DbSet<TblFinalStockBk31jan20> TblFinalStockBk31jan20s { get; set; }

    public virtual DbSet<TblGender> TblGenders { get; set; }

    public virtual DbSet<TblHealthFacility> TblHealthFacilities { get; set; }

    public virtual DbSet<TblHealthFacilityType> TblHealthFacilityTypes { get; set; }

    public virtual DbSet<TblHealthFaciltyCategory> TblHealthFaciltyCategories { get; set; }

    public virtual DbSet<TblHealthcareFacilityReporting> TblHealthcareFacilityReportings { get; set; }

    public virtual DbSet<TblHospitalIndicatorDatum> TblHospitalIndicatorData { get; set; }

    public virtual DbSet<TblHouseholdContact> TblHouseholdContacts { get; set; }

    public virtual DbSet<TblIndicator> TblIndicators { get; set; }

    public virtual DbSet<TblIndicatorCat> TblIndicatorCats { get; set; }

    public virtual DbSet<TblLabAction> TblLabActions { get; set; }

    public virtual DbSet<TblLabBatch> TblLabBatches { get; set; }

    public virtual DbSet<TblLabBatchSample> TblLabBatchSamples { get; set; }

    public virtual DbSet<TblLabResult> TblLabResults { get; set; }

    public virtual DbSet<TblLabSample> TblLabSamples { get; set; }

    public virtual DbSet<TblLabortary> TblLabortaries { get; set; }

    public virtual DbSet<TblLostFollowupReason> TblLostFollowupReasons { get; set; }

    public virtual DbSet<TblMaritalStatus> TblMaritalStatuses { get; set; }

    public virtual DbSet<TblMedDeliveryLog> TblMedDeliveryLogs { get; set; }

    public virtual DbSet<TblMedicineDisbursementOldRegime> TblMedicineDisbursementOldRegimes { get; set; }

    public virtual DbSet<TblMedicinesRecommended> TblMedicinesRecommendeds { get; set; }

    public virtual DbSet<TblMissingUpdatedDatum> TblMissingUpdatedData { get; set; }

    public virtual DbSet<TblNewRegimeMedLog> TblNewRegimeMedLogs { get; set; }

    public virtual DbSet<TblNewRegimeMedicine> TblNewRegimeMedicines { get; set; }

    public virtual DbSet<TblNewRegimeStockLog> TblNewRegimeStockLogs { get; set; }

    public virtual DbSet<TblNextOfKin> TblNextOfKins { get; set; }

    public virtual DbSet<TblOccupation> TblOccupations { get; set; }

    public virtual DbSet<TblPatient> TblPatients { get; set; }

    public virtual DbSet<TblPatientAssessment> TblPatientAssessments { get; set; }

    public virtual DbSet<TblPatientAssessmentLog> TblPatientAssessmentLogs { get; set; }

    public virtual DbSet<TblPatientConsignmentNumber> TblPatientConsignmentNumbers { get; set; }

    public virtual DbSet<TblPatientDataCsv> TblPatientDataCsvs { get; set; }

    public virtual DbSet<TblPatientHistory> TblPatientHistories { get; set; }

    public virtual DbSet<TblPatientMedicineInfo> TblPatientMedicineInfos { get; set; }

    public virtual DbSet<TblPatientRefer> TblPatientRefers { get; set; }

    public virtual DbSet<TblPatientReferReceive> TblPatientReferReceives { get; set; }

    public virtual DbSet<TblPatientReferReceiveHistory> TblPatientReferReceiveHistories { get; set; }

    public virtual DbSet<TblPatientReferReceiveTreatment> TblPatientReferReceiveTreatments { get; set; }

    public virtual DbSet<TblPatientStage> TblPatientStages { get; set; }

    public virtual DbSet<TblPatientStageLog> TblPatientStageLogs { get; set; }

    public virtual DbSet<TblPatientTransfer> TblPatientTransfers { get; set; }

    public virtual DbSet<TblPatientVital> TblPatientVitals { get; set; }

    public virtual DbSet<TblPatientvaccination> TblPatientvaccinations { get; set; }

    public virtual DbSet<TblPrivateLabResult> TblPrivateLabResults { get; set; }

    public virtual DbSet<TblQualification> TblQualifications { get; set; }

    public virtual DbSet<TblReferDistrict> TblReferDistricts { get; set; }

    public virtual DbSet<TblReferHospital> TblReferHospitals { get; set; }

    public virtual DbSet<TblReferProvince> TblReferProvinces { get; set; }

    public virtual DbSet<TblReferTehsil> TblReferTehsils { get; set; }

    public virtual DbSet<TblRenalFunction> TblRenalFunctions { get; set; }

    public virtual DbSet<TblRole> TblRoles { get; set; }

    public virtual DbSet<TblSample> TblSamples { get; set; }

    public virtual DbSet<TblScreening> TblScreenings { get; set; }

    public virtual DbSet<TblScreeningMethod> TblScreeningMethods { get; set; }

    public virtual DbSet<TblSmsLog> TblSmsLogs { get; set; }

    public virtual DbSet<TblStock> TblStocks { get; set; }

    public virtual DbSet<TblStockLog> TblStockLogs { get; set; }

    public virtual DbSet<TblSvrFormDatum> TblSvrFormData { get; set; }

    public virtual DbSet<TblTcsBatch> TblTcsBatches { get; set; }

    public virtual DbSet<TblTcsBatchPatient> TblTcsBatchPatients { get; set; }

    public virtual DbSet<TblTcsConsignmentNumber> TblTcsConsignmentNumbers { get; set; }

    public virtual DbSet<TblTehsil> TblTehsils { get; set; }

    public virtual DbSet<TblTemp> TblTemps { get; set; }

    public virtual DbSet<TblTempString> TblTempStrings { get; set; }

    public virtual DbSet<TblTemporaryResultUpload> TblTemporaryResultUploads { get; set; }

    public virtual DbSet<TblTest> TblTests { get; set; }

    public virtual DbSet<TblTicket> TblTickets { get; set; }

    public virtual DbSet<TblTimeLog> TblTimeLogs { get; set; }

    public virtual DbSet<TblTreatment> TblTreatments { get; set; }

    public virtual DbSet<TblUc> TblUcs { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<TeamsMember> TeamsMembers { get; set; }

    public virtual DbSet<Testcnic> Testcnics { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    public virtual DbSet<TourComplianceStatus> TourComplianceStatuses { get; set; }

    public virtual DbSet<TourCondition> TourConditions { get; set; }

    public virtual DbSet<TourFacility> TourFacilities { get; set; }

    public virtual DbSet<TourObservation> TourObservations { get; set; }

    public virtual DbSet<TourObservationDetail> TourObservationDetails { get; set; }

    public virtual DbSet<TourObservationImage> TourObservationImages { get; set; }

    public virtual DbSet<TourStatus> TourStatuses { get; set; }

    public virtual DbSet<TourTeamMember> TourTeamMembers { get; set; }

    public virtual DbSet<UpdatedPatientsPacp> UpdatedPatientsPacps { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }

    public virtual DbSet<ViewHfDetail> ViewHfDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=172.16.0.50;Database=PHCP;Persist Security Info=False;User Id=shoaib;Password=asd@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActiveHf>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("active_hf", "phcp_120923");

            entity.Property(e => e.HfNam).HasColumnName("hf_nam");
        });

        modelBuilder.Entity<Administration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_administration_id");

            entity.ToTable("administration", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CellNo)
                .HasMaxLength(254)
                .HasColumnName("cell_no");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.Designation)
                .HasMaxLength(254)
                .HasColumnName("designation");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.Name)
                .HasMaxLength(254)
                .HasColumnName("name");
            entity.Property(e => e.OrderBy).HasColumnName("order_by");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(254)
                .HasColumnName("phone_no");
            entity.Property(e => e.Scale).HasColumnName("scale");
        });

        modelBuilder.Entity<ApprovalStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_approval_statuses_id");

            entity.ToTable("approval_statuses", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.OrderBy).HasColumnName("order_by");
            entity.Property(e => e.Title)
                .HasMaxLength(254)
                .HasColumnName("title");
        });

        modelBuilder.Entity<ApprovedBy>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("approved_by", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.OrderBy).HasColumnName("order_by");
            entity.Property(e => e.Title)
                .HasMaxLength(254)
                .HasColumnName("title");
        });

        modelBuilder.Entity<AssessmentMissingId>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_assessment_missing_ids_id");

            entity.ToTable("assessment_missing_ids", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HfName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hf_name");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.MrnNo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("mrn_no");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.PatientStage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("patient_stage");
            entity.Property(e => e.PatientType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("patient_type");
            entity.Property(e => e.SampleCollect)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("sample_collect");
            entity.Property(e => e.SampleNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("sample_number");
        });

        modelBuilder.Entity<CampaignSmsExcelLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_campaign_sms_excel_log_Id");

            entity.ToTable("campaign_sms_excel_log", "phcp_120923");

            entity.Property(e => e.CreatedDate)
                .HasPrecision(0)
                .HasColumnName("created_date");
            entity.Property(e => e.Message)
                .HasMaxLength(250)
                .HasColumnName("message");
            entity.Property(e => e.MonbileNo)
                .HasMaxLength(150)
                .HasColumnName("monbile_no");
            entity.Property(e => e.MrnNo)
                .HasMaxLength(100)
                .HasColumnName("mrn_no");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Response)
                .HasMaxLength(150)
                .HasColumnName("response");
            entity.Property(e => e.SmsType)
                .HasMaxLength(50)
                .HasColumnName("sms_type");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<CampaignSmsLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_campaign_sms_log_Id");

            entity.ToTable("campaign_sms_log", "phcp_120923");

            entity.Property(e => e.CreatedDate)
                .HasPrecision(0)
                .HasColumnName("created_date");
            entity.Property(e => e.Message)
                .HasMaxLength(250)
                .HasColumnName("message");
            entity.Property(e => e.MonbileNo)
                .HasMaxLength(150)
                .HasColumnName("monbile_no");
            entity.Property(e => e.MrnNo)
                .HasMaxLength(100)
                .HasColumnName("mrn_no");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Response)
                .HasMaxLength(150)
                .HasColumnName("response");
            entity.Property(e => e.SmsType)
                .HasMaxLength(50)
                .HasColumnName("sms_type");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("cities", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.Code)
                .HasMaxLength(254)
                .HasColumnName("code");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.OrderBy).HasColumnName("order_by");
            entity.Property(e => e.ShortCode)
                .HasMaxLength(254)
                .HasColumnName("short_code");
            entity.Property(e => e.Title)
                .HasMaxLength(254)
                .HasColumnName("title");
        });

        modelBuilder.Entity<DashboardCountsApi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_dashboard_counts_api_id");

            entity.ToTable("dashboard_counts_api", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedDate)
                .HasPrecision(0)
                .HasDefaultValueSql("('2021-11-02 14:30:00')")
                .HasColumnName("created_date");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(150)
                .HasColumnName("district_name");
            entity.Property(e => e.MedicineStockCounts).HasColumnName("medicine_stock_counts");
            entity.Property(e => e.TotalNewAssessmentCount).HasColumnName("total_new_assessment_count");
            entity.Property(e => e.TotalNewPatientCount).HasColumnName("total_new_patient_count");
            entity.Property(e => e.TotalPreDiagnosedCount).HasColumnName("total_pre_diagnosed_count");
            entity.Property(e => e.TotalRegistrationCount).HasColumnName("total_registration_count");
            entity.Property(e => e.TotalSamplesAccepted).HasColumnName("total_samples_accepted");
            entity.Property(e => e.TotalSamplesRejected).HasColumnName("total_samples_rejected");
            entity.Property(e => e.TotalSamplesResults).HasColumnName("total_samples_results");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("districts", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.CeoCell)
                .HasMaxLength(254)
                .HasColumnName("ceo_cell");
            entity.Property(e => e.CeoEmail)
                .HasMaxLength(254)
                .HasColumnName("ceo_email");
            entity.Property(e => e.CeoName)
                .HasMaxLength(254)
                .HasColumnName("ceo_name");
            entity.Property(e => e.CeoPhone)
                .HasMaxLength(254)
                .HasColumnName("ceo_phone");
            entity.Property(e => e.Code)
                .HasMaxLength(254)
                .HasColumnName("code");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.DhoCell)
                .HasMaxLength(254)
                .HasColumnName("dho_cell");
            entity.Property(e => e.DhoEmail)
                .HasMaxLength(254)
                .HasColumnName("dho_email");
            entity.Property(e => e.DhoName)
                .HasMaxLength(254)
                .HasColumnName("dho_name");
            entity.Property(e => e.DhoPhone)
                .HasMaxLength(254)
                .HasColumnName("dho_phone");
            entity.Property(e => e.District1)
                .HasMaxLength(254)
                .HasColumnName("district");
            entity.Property(e => e.DivisionId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("division_id");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.OrderBy).HasColumnName("order_by");
            entity.Property(e => e.ShortCode)
                .HasMaxLength(254)
                .HasColumnName("short_code");
        });

        modelBuilder.Entity<Division>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("divisions", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.Code)
                .HasMaxLength(254)
                .HasColumnName("code");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsSpecial)
                .HasMaxLength(3)
                .HasDefaultValueSql("(N'No')")
                .HasColumnName("is_special");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.OrderBy).HasColumnName("order_by");
            entity.Property(e => e.ShortCode)
                .HasMaxLength(254)
                .HasColumnName("short_code");
            entity.Property(e => e.Title)
                .HasMaxLength(254)
                .HasColumnName("title");
            entity.Property(e => e.ZoneId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("zone_id");
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("drivers", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.CellNo)
                .HasMaxLength(20)
                .HasColumnName("cell_no");
            entity.Property(e => e.CnicNo)
                .HasMaxLength(30)
                .HasColumnName("cnic_no");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.LicenceType)
                .HasMaxLength(254)
                .HasColumnName("licence_type");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.Name)
                .HasMaxLength(254)
                .HasColumnName("name");
            entity.Property(e => e.OrderBy).HasColumnName("order_by");
        });

        modelBuilder.Entity<ExceptionLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_exception_log_id");

            entity.ToTable("exception_log", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ExMessage)
                .HasMaxLength(500)
                .HasColumnName("ex_message");
            entity.Property(e => e.FkHcpid).HasColumnName("fk_HCPId");
        });

        modelBuilder.Entity<FacilityType>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("facility_types", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.OrderBy).HasColumnName("order_by");
            entity.Property(e => e.Title)
                .HasMaxLength(254)
                .HasColumnName("title");
        });

        modelBuilder.Entity<HealthFacility>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("health_facilities", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.Address)
                .HasMaxLength(254)
                .HasColumnName("address");
            entity.Property(e => e.CellNo)
                .HasMaxLength(254)
                .HasColumnName("cell_no");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.FacilityId).HasColumnName("facility_id");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.OrderBy)
                .HasDefaultValueSql("((0))")
                .HasColumnName("order_by");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(254)
                .HasColumnName("phone_no");
            entity.Property(e => e.SmoCellNo)
                .HasMaxLength(254)
                .HasColumnName("smo_cell_no");
            entity.Property(e => e.SmoDesignation)
                .HasMaxLength(254)
                .HasColumnName("smo_designation");
            entity.Property(e => e.SmoEmail)
                .HasMaxLength(254)
                .HasColumnName("smo_email");
            entity.Property(e => e.SmoName)
                .HasMaxLength(254)
                .HasColumnName("smo_name");
            entity.Property(e => e.SmoPhoneNo)
                .HasMaxLength(254)
                .HasColumnName("smo_phone_no");
            entity.Property(e => e.Title)
                .HasMaxLength(254)
                .HasColumnName("title");
            entity.Property(e => e.TypeId).HasColumnName("type_id");
        });

        modelBuilder.Entity<HighPrevelanceDist>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.IsFound }).HasName("PK_high_prevelance_dist_id");

            entity.ToTable("high_prevelance_dist", "phcp_120923");

            entity.HasIndex(e => e.Cnic, "cnic");

            entity.HasIndex(e => e.ContactNumber, "contact_number");

            entity.HasIndex(e => e.FirstName, "first_name");

            entity.HasIndex(e => e.Id, "id");

            entity.HasIndex(e => e.IsReception, "is_reception");

            entity.HasIndex(e => e.RegNo, "reg_no");

            entity.HasIndex(e => e.SampleId, "sample_id");

            entity.HasIndex(e => e.TestRequired, "test_required");

            entity.HasIndex(e => e.UserHospital, "user_hospital");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsFound)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_found");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.AltContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("alt_contact_number");
            entity.Property(e => e.Cnic)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cnic");
            entity.Property(e => e.CnicNotAvailable)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("cnic_not_available");
            entity.Property(e => e.CnicStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("cnic_status");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contact_number");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.District).HasColumnName("district");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("dob");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.HealthType)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'HW')")
                .HasColumnName("health_type");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.IsDischarge)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_discharge");
            entity.Property(e => e.IsHbvTest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_hbv_test");
            entity.Property(e => e.IsHcvTest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_hcv_test");
            entity.Property(e => e.IsPcrDrawn)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_pcr_drawn");
            entity.Property(e => e.IsReception)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_reception");
            entity.Property(e => e.LabReceptionistId).HasColumnName("lab_receptionist_id");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("marital_status");
            entity.Property(e => e.NextOfKin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("next_of_kin");
            entity.Property(e => e.NextOfKinCnic)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("next_of_kin_cnic");
            entity.Property(e => e.PcrOption)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("pcr_option");
            entity.Property(e => e.PcrType)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("pcr_type");
            entity.Property(e => e.ReferralClinic).HasColumnName("referral_clinic");
            entity.Property(e => e.RegNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("reg_no");
            entity.Property(e => e.RelContact)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("rel_contact");
            entity.Property(e => e.SampleId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sample_id");
            entity.Property(e => e.SpouseName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("spouse_name");
            entity.Property(e => e.Tehsil).HasColumnName("tehsil");
            entity.Property(e => e.TestRequired)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("test_required");
            entity.Property(e => e.UcNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("uc_number");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.UserHospital).HasColumnName("user_hospital");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<HwdOldDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_hwd_old_data_id");

            entity.ToTable("hwd_old_data", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.CmStatus).HasColumnName("cm_status");
            entity.Property(e => e.Cnic)
                .HasMaxLength(255)
                .HasColumnName("cnic");
            entity.Property(e => e.ContactNo)
                .HasMaxLength(255)
                .HasColumnName("contact_no");
            entity.Property(e => e.CreatedOn)
                .HasColumnType("datetime")
                .HasColumnName("Created_On");
            entity.Property(e => e.District)
                .HasMaxLength(255)
                .HasColumnName("district");
            entity.Property(e => e.FatherName)
                .HasMaxLength(255)
                .HasColumnName("father_name");
            entity.Property(e => e.FollowDate)
                .HasColumnType("datetime")
                .HasColumnName("Follow_Date");
            entity.Property(e => e.Gender)
                .HasMaxLength(255)
                .HasColumnName("gender");
            entity.Property(e => e.GuardianCnic)
                .HasMaxLength(255)
                .HasColumnName("Guardian_Cnic");
            entity.Property(e => e.HbvStatus).HasColumnName("hbv_status");
            entity.Property(e => e.HcvStatus).HasColumnName("hcv_status");
            entity.Property(e => e.HospitalName)
                .HasMaxLength(255)
                .HasColumnName("hospital_name");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(255)
                .HasColumnName("marital_status");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.NextDoseHospital)
                .HasMaxLength(255)
                .HasColumnName("Next_Dose_Hospital");
            entity.Property(e => e.OldRegistrationNumber)
                .HasMaxLength(20)
                .HasColumnName("Old_Registration_Number");
            entity.Property(e => e.PcrConfirmHbv)
                .HasMaxLength(255)
                .HasColumnName("PCR_Confirm_HBV");
            entity.Property(e => e.PcrConfirmHcv)
                .HasMaxLength(255)
                .HasColumnName("PCR_Confirm_HCV");
            entity.Property(e => e.PcrRecommended).HasColumnName("pcr_recommended");
            entity.Property(e => e.PcrSentTo)
                .HasMaxLength(255)
                .HasColumnName("PCR_Sent_To");
            entity.Property(e => e.PhcpUniqueKey)
                .HasMaxLength(255)
                .HasColumnName("PHCP_Unique_Key");
            entity.Property(e => e.ScSampleRn)
                .HasMaxLength(255)
                .HasColumnName("SC_Sample_RN");
            entity.Property(e => e.TestDate)
                .HasColumnType("datetime")
                .HasColumnName("test_date");
            entity.Property(e => e.VcStatus).HasColumnName("vc_status");
            entity.Property(e => e.VdaStatus).HasColumnName("vda_status");
        });

        modelBuilder.Entity<HwdPatient>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.IsFound }).HasName("PK_hwd_patient_id");

            entity.ToTable("hwd_patient", "phcp_120923");

            entity.HasIndex(e => e.Cnic, "cnic");

            entity.HasIndex(e => e.ContactNumber, "contact_number");

            entity.HasIndex(e => e.FirstName, "first_name");

            entity.HasIndex(e => e.Id, "id");

            entity.HasIndex(e => e.IsReception, "is_reception");

            entity.HasIndex(e => e.RegNo, "reg_no");

            entity.HasIndex(e => e.SampleId, "sample_id");

            entity.HasIndex(e => e.TestRequired, "test_required");

            entity.HasIndex(e => e.UserHospital, "user_hospital");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsFound)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_found");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.AltContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("alt_contact_number");
            entity.Property(e => e.Cnic)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cnic");
            entity.Property(e => e.CnicStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("cnic_status");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contact_number");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.District).HasColumnName("district");
            entity.Property(e => e.DistrictApi)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("district_api");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("dob");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.FormType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'walk-in-PCR')")
                .HasColumnName("form_type");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.GenderApi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gender_api");
            entity.Property(e => e.HealthType)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'HW')")
                .HasColumnName("health_type");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.IsHbvTest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_hbv_test");
            entity.Property(e => e.IsHcvTest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_hcv_test");
            entity.Property(e => e.IsPcrDrawn)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_pcr_drawn");
            entity.Property(e => e.IsReception)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_reception");
            entity.Property(e => e.IsTaken)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_taken");
            entity.Property(e => e.LabReceptionistId).HasColumnName("lab_receptionist_id");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.MaritalStatus).HasColumnName("marital_status");
            entity.Property(e => e.MaritalStatusApi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("marital_status_api");
            entity.Property(e => e.NextOfKin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("next_of_kin");
            entity.Property(e => e.NextOfKinCnic)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("next_of_kin_cnic");
            entity.Property(e => e.Occupation)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("occupation");
            entity.Property(e => e.PcrOption)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("pcr_option");
            entity.Property(e => e.PcrType)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("pcr_type");
            entity.Property(e => e.Qualification)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("qualification");
            entity.Property(e => e.RegNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("reg_no");
            entity.Property(e => e.RelContact)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("rel_contact");
            entity.Property(e => e.SampleId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sample_id");
            entity.Property(e => e.SpouseName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("spouse_name");
            entity.Property(e => e.Tehsil).HasColumnName("tehsil");
            entity.Property(e => e.TehsilApi)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tehsil_api");
            entity.Property(e => e.TestRequired)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("test_required");
            entity.Property(e => e.UcNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("uc_number");
            entity.Property(e => e.UniqueId)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("unique_id");
            entity.Property(e => e.UserHospital).HasColumnName("user_hospital");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<HwdPatientSearchLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_hwd_patient_search_log_id");

            entity.ToTable("hwd_patient_search_log", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cnic)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("cnic");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<IntegratedLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_integrated_logs_id");

            entity.ToTable("integrated_logs", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FkHcpid).HasColumnName("fk_HCPId");
        });

        modelBuilder.Entity<LatlongPatient>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("latlong_patients", "phcp_120923");

            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.PostalAddress).HasColumnName("postal_address");
        });

        modelBuilder.Entity<MicroScreening>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.IsFound }).HasName("PK_micro_screening_id");

            entity.ToTable("micro_screening", "phcp_120923");

            entity.HasIndex(e => e.ActivityType, "activity_type");

            entity.HasIndex(e => e.Cnic, "cnic");

            entity.HasIndex(e => e.ContactNumber, "contact_number");

            entity.HasIndex(e => e.EventId, "event_id");

            entity.HasIndex(e => e.FirstName, "first_name");

            entity.HasIndex(e => e.FrmDataPlug, "frm_data_plug");

            entity.HasIndex(e => e.HospitalId, "hospital_id");

            entity.HasIndex(e => e.Id, "id");

            entity.HasIndex(e => e.Id, "id_2");

            entity.HasIndex(e => e.IsFirstVaccine, "is_first_vaccine");

            entity.HasIndex(e => e.IsHbvTest, "is_hbv_test");

            entity.HasIndex(e => e.IsHcvTest, "is_hcv_test");

            entity.HasIndex(e => e.IsReception, "is_reception");

            entity.HasIndex(e => e.IsSampleAdd, "is_sample_add");

            entity.HasIndex(e => e.RegNo, "reg_no");

            entity.HasIndex(e => e.SampleId, "sample_id");

            entity.HasIndex(e => e.SampleId, "sample_id_2");

            entity.HasIndex(e => e.TestRequired, "test_required");

            entity.HasIndex(e => e.UserHospital, "user_hospital");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsFound)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_found");
            entity.Property(e => e.ActivityType)
                .HasMaxLength(18)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'micro')")
                .HasColumnName("activity_type");
            entity.Property(e => e.Address)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.AltContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("alt_contact_number");
            entity.Property(e => e.ApiDistrictName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("api_district_name");
            entity.Property(e => e.Cnic)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cnic");
            entity.Property(e => e.CnicNotAvailable)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("cnic_not_available");
            entity.Property(e => e.CnicStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("cnic_status");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contact_number");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Department)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("department");
            entity.Property(e => e.District).HasColumnName("district");
            entity.Property(e => e.DistrictsReferral)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("districts_referral");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("dob");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.FrmDataPlug)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("frm_data_plug");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.HealthType)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'HW')")
                .HasColumnName("health_type");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.ImeiNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("imei_no");
            entity.Property(e => e.IsAlreadyVaccinated)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_already_vaccinated");
            entity.Property(e => e.IsAssesment)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_assesment");
            entity.Property(e => e.IsDischarge)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_discharge");
            entity.Property(e => e.IsFirstVaccine)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_first_vaccine");
            entity.Property(e => e.IsHbvDetected)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_hbv_detected");
            entity.Property(e => e.IsHbvTest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_hbv_test");
            entity.Property(e => e.IsHcvDetected)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_hcv_detected");
            entity.Property(e => e.IsHcvTest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_hcv_test");
            entity.Property(e => e.IsHouseholdInfo)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_household_info");
            entity.Property(e => e.IsPatRegInEmr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_pat_reg_in_emr");
            entity.Property(e => e.IsPcrDrawn)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_pcr_drawn");
            entity.Property(e => e.IsReception)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_reception");
            entity.Property(e => e.IsSampleAdd)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_sample_add");
            entity.Property(e => e.IsSecondVaccine)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_second_vaccine");
            entity.Property(e => e.IsSoftDelete)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_soft_delete");
            entity.Property(e => e.IsThirdVaccine)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_third_vaccine");
            entity.Property(e => e.LabReceptionistId).HasColumnName("lab_receptionist_id");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("marital_status");
            entity.Property(e => e.MrnNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("mrn_no");
            entity.Property(e => e.NextOfKin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("next_of_kin");
            entity.Property(e => e.NextOfKinCnic)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("next_of_kin_cnic");
            entity.Property(e => e.PatientClass)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("patient_class");
            entity.Property(e => e.PcrOption)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("pcr_option");
            entity.Property(e => e.PcrType)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("pcr_type");
            entity.Property(e => e.ReferralClinic).HasColumnName("referral_clinic");
            entity.Property(e => e.RegNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("reg_no");
            entity.Property(e => e.RelContact)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("rel_contact");
            entity.Property(e => e.SampleCollectedDate).HasColumnName("sample_collected_date");
            entity.Property(e => e.SampleId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sample_id");
            entity.Property(e => e.SchoolName)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("school_name");
            entity.Property(e => e.SchoolRegistrationNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("school_registration_no");
            entity.Property(e => e.ScreeningDate).HasColumnName("screening_date");
            entity.Property(e => e.SecondVaccinationDoseDate).HasColumnName("second_vaccination_dose_date");
            entity.Property(e => e.SpouseName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("spouse_name");
            entity.Property(e => e.Tehsil).HasColumnName("tehsil");
            entity.Property(e => e.TehsilName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("tehsil_name");
            entity.Property(e => e.TehsilReferralName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tehsil_referral_name");
            entity.Property(e => e.TestRequired)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("test_required");
            entity.Property(e => e.ThirdVaccinationDoseDate).HasColumnName("third_vaccination_dose_date");
            entity.Property(e => e.TokenNo).HasColumnName("token_no");
            entity.Property(e => e.UcName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("uc_name");
            entity.Property(e => e.UcNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("uc_number");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.UserHospital).HasColumnName("user_hospital");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Monitor>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("monitors", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.CellNo)
                .HasMaxLength(254)
                .HasColumnName("cell_no");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.Designation)
                .HasMaxLength(254)
                .HasColumnName("designation");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.MonitringAreaId).HasColumnName("monitring_Area_id");
            entity.Property(e => e.Name)
                .HasMaxLength(254)
                .HasColumnName("name");
            entity.Property(e => e.OrderBy).HasColumnName("order_by");
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(254)
                .HasColumnName("phone_no");
            entity.Property(e => e.Scale).HasColumnName("scale");
        });

        modelBuilder.Entity<MonitringArea>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("monitring_areas", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.OrderBy).HasColumnName("order_by");
            entity.Property(e => e.Title)
                .HasMaxLength(254)
                .HasColumnName("title");
        });

        modelBuilder.Entity<MonitringZone>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("monitring_zones", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.Code).HasColumnName("code");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.OrderBy).HasColumnName("order_by");
            entity.Property(e => e.ShortCode)
                .HasMaxLength(254)
                .HasColumnName("short_code");
            entity.Property(e => e.Title)
                .HasMaxLength(254)
                .HasDefaultValueSql("(N'1')")
                .HasColumnName("title");
        });

        modelBuilder.Entity<Observation>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("observations", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.MonitringAreaId).HasColumnName("monitring_area_id");
            entity.Property(e => e.Observation1)
                .HasMaxLength(254)
                .HasColumnName("observation");
            entity.Property(e => e.OrderBy)
                .HasDefaultValueSql("((0))")
                .HasColumnName("order_by");
            entity.Property(e => e.Sequence).HasColumnName("sequence");
            entity.Property(e => e.SubAreaId).HasColumnName("sub_area_id");
        });

        modelBuilder.Entity<ObservationsFacilityType>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("observations_facility_types", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.CreatedBy)
                .HasDefaultValueSql("((0))")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.FacilityTypeId).HasColumnName("facility_type_id");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy)
                .HasDefaultValueSql("((0))")
                .HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.ObservationId).HasColumnName("observation_id");
            entity.Property(e => e.OrderBy)
                .HasDefaultValueSql("((0))")
                .HasColumnName("order_by");
        });

        modelBuilder.Entity<SchoolVaccination>(entity =>
        {
            entity.HasKey(e => new { e.Id, e.IsFound }).HasName("PK_school_vaccination_id");

            entity.ToTable("school_vaccination", "phcp_120923");

            entity.HasIndex(e => e.Cnic, "cnic");

            entity.HasIndex(e => e.ContactNumber, "contact_number");

            entity.HasIndex(e => e.FirstName, "first_name");

            entity.HasIndex(e => e.Id, "id");

            entity.HasIndex(e => e.IsReception, "is_reception");

            entity.HasIndex(e => e.RegNo, "reg_no");

            entity.HasIndex(e => e.SampleId, "sample_id");

            entity.HasIndex(e => e.TestRequired, "test_required");

            entity.HasIndex(e => e.UserHospital, "user_hospital");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsFound)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_found");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.AltContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("alt_contact_number");
            entity.Property(e => e.Cnic)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cnic");
            entity.Property(e => e.CnicNotAvailable)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("cnic_not_available");
            entity.Property(e => e.CnicStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("cnic_status");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contact_number");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.District).HasColumnName("district");
            entity.Property(e => e.DistrictsReferral)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("districts_referral");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("dob");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.HealthType)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'HW')")
                .HasColumnName("health_type");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.IsDischarge)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_discharge");
            entity.Property(e => e.IsHbvTest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_hbv_test");
            entity.Property(e => e.IsHcvTest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_hcv_test");
            entity.Property(e => e.IsPcrDrawn)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_pcr_drawn");
            entity.Property(e => e.IsReception)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_reception");
            entity.Property(e => e.LabReceptionistId).HasColumnName("lab_receptionist_id");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("marital_status");
            entity.Property(e => e.NextOfKin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("next_of_kin");
            entity.Property(e => e.NextOfKinCnic)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("next_of_kin_cnic");
            entity.Property(e => e.PatientClass)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("patient_class");
            entity.Property(e => e.PcrOption)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("pcr_option");
            entity.Property(e => e.PcrType)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("pcr_type");
            entity.Property(e => e.ReferralClinic).HasColumnName("referral_clinic");
            entity.Property(e => e.RegNo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("reg_no");
            entity.Property(e => e.RelContact)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("rel_contact");
            entity.Property(e => e.SampleId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sample_id");
            entity.Property(e => e.SchoolName)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("school_name");
            entity.Property(e => e.SchoolRegistrationNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("school_registration_no");
            entity.Property(e => e.SpouseName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("spouse_name");
            entity.Property(e => e.Tehsil).HasColumnName("tehsil");
            entity.Property(e => e.TehsilReferralName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tehsil_referral_name");
            entity.Property(e => e.TestRequired)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("test_required");
            entity.Property(e => e.UcNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("uc_number");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.UserHospital).HasColumnName("user_hospital");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<SiteSead>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("site_seads", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.PageSize).HasColumnName("page_size");
            entity.Property(e => e.SiteTitle)
                .HasMaxLength(254)
                .HasColumnName("site_title");
            entity.Property(e => e.SiteTitleShort)
                .HasMaxLength(254)
                .HasColumnName("site_title_short");
        });

        modelBuilder.Entity<SmsForMep>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("sms_for_mep", "phcp_120923");

            entity.Property(e => e.BothNegativeMessage).HasColumnName("Both_Negative_Message");
            entity.Property(e => e.PcrPositiveMessage).HasColumnName("PCR_positive_MEssage");
            entity.Property(e => e.SvrMessage).HasColumnName("SVR_Message");
        });

        modelBuilder.Entity<SubMonitringArea>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("sub_monitring_areas", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.MonitoringAreaId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("monitoring_area_id");
            entity.Property(e => e.OrderBy)
                .HasDefaultValueSql("((0))")
                .HasColumnName("order_by");
            entity.Property(e => e.ParentId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("parent_id");
            entity.Property(e => e.Sequences)
                .HasDefaultValueSql("((0))")
                .HasColumnName("sequences");
            entity.Property(e => e.Title)
                .HasMaxLength(254)
                .HasColumnName("title");
        });

        modelBuilder.Entity<TblApiConfig>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_api_config_id");

            entity.ToTable("tbl_api_config", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("created");
            entity.Property(e => e.FromDate)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("from_date");
            entity.Property(e => e.ToDate)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("to_date");
        });

        modelBuilder.Entity<TblApiLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_api_log_id");

            entity.ToTable("tbl_api_log", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppVersion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("app_version");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.LogDate)
                .HasColumnType("date")
                .HasColumnName("logDate");
            entity.Property(e => e.Module)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("module");
            entity.Property(e => e.ReqData)
                .IsUnicode(false)
                .HasColumnName("req_data");
        });

        modelBuilder.Entity<TblApiRec>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_api_rec_id");

            entity.ToTable("tbl_api_rec", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Cnic)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cnic");
            entity.Property(e => e.ContactNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("contact_no");
            entity.Property(e => e.Created)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("created");
            entity.Property(e => e.District)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("district");
            entity.Property(e => e.Dob)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("dob");
            entity.Property(e => e.FirstName)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.HbvTest)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("hbv_test");
            entity.Property(e => e.HcvTest)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("hcv_test");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("marital_status");
            entity.Property(e => e.Occupation)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("occupation");
            entity.Property(e => e.Qualification)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("qualification");
            entity.Property(e => e.RegDate)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("reg_date");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.SwoName)
                .IsUnicode(false)
                .HasColumnName("swo_name");
            entity.Property(e => e.Tehsil)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tehsil");
            entity.Property(e => e.TokenNo)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("token_no");
            entity.Property(e => e.UniqueId)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("unique_id");
        });

        modelBuilder.Entity<TblApriLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_apri_log_id");

            entity.ToTable("tbl_apri_log", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApriValue)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("apri_value");
            entity.Property(e => e.Comments)
                .IsUnicode(false)
                .HasColumnName("comments");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.TestType)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'HBV')")
                .HasColumnName("test_type");
        });

        modelBuilder.Entity<TblBaseline>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_baseline_id");

            entity.ToTable("tbl_baseline", "phcp_120923");

            entity.HasIndex(e => e.Created, "created");

            entity.HasIndex(e => e.HbvMedicine, "hbv_medicine");

            entity.HasIndex(e => e.HcvMedicine, "hcv_medicine");

            entity.HasIndex(e => e.Id, "id");

            entity.HasIndex(e => e.IsDemote, "is_demote");

            entity.HasIndex(e => e.IsHbvMedicine, "is_hbv_medicine");

            entity.HasIndex(e => e.IsHcvMedicine, "is_hcv_medicine");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.HasIndex(e => e.PatientId, "patient_id_2");

            entity.HasIndex(e => e.PrescribeMedicine, "prescribe_medicine");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alt).HasColumnName("alt");
            entity.Property(e => e.Apri).HasColumnName("apri");
            entity.Property(e => e.Ascites)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'Y')");
            entity.Property(e => e.Ast).HasColumnName("ast");
            entity.Property(e => e.Counselling)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("counselling");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Deferred)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("deferred");
            entity.Property(e => e.Discharge)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("discharge");
            entity.Property(e => e.FollowUpNumber).HasColumnName("follow_up_number");
            entity.Property(e => e.Genotyping)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("genotyping");
            entity.Property(e => e.HbvMedicine)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hbv_medicine");
            entity.Property(e => e.HbvPcr)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N/A')")
                .HasColumnName("hbv_pcr");
            entity.Property(e => e.HbvViralLoad).HasColumnName("hbv_viral_load");
            entity.Property(e => e.HcvMedicine)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hcv_medicine");
            entity.Property(e => e.HcvPcr)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N/A')")
                .HasColumnName("hcv_pcr");
            entity.Property(e => e.HcvViralLoad).HasColumnName("hcv_viral_load");
            entity.Property(e => e.Hemoglobin).HasColumnName("hemoglobin");
            entity.Property(e => e.IsDemote)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_demote");
            entity.Property(e => e.IsHbvMedicine)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_hbv_medicine");
            entity.Property(e => e.IsHcvMedicine)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_hcv_medicine");
            entity.Property(e => e.IsSkip)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_skip");
            entity.Property(e => e.Liver)
                .HasMaxLength(22)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'normal')")
                .HasColumnName("liver");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Platelet).HasColumnName("platelet");
            entity.Property(e => e.PrescribeMedicine)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("prescribe_medicine");
            entity.Property(e => e.ReferralPkli)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("referral_pkli");
            entity.Property(e => e.Spleen)
                .HasMaxLength(22)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'No Splenomegaly')")
                .HasColumnName("spleen");
            entity.Property(e => e.Tlc).HasColumnName("tlc");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UserHospital).HasColumnName("user_hospital");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Vaccination)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("vaccination");
            entity.Property(e => e.VaccinationOption)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("vaccination_option");
        });

        modelBuilder.Entity<TblBaselineStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_baseline_statuses_id");

            entity.ToTable("tbl_baseline_statuses", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.StatusName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<TblBatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_batch_id");

            entity.ToTable("tbl_batch", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BactchNumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("bactch_number");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.IsBatchDispatched)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_batch_dispatched");
            entity.Property(e => e.IsBatchReceived)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_batch_received");
            entity.Property(e => e.UserHospital).HasColumnName("user_hospital");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<TblBatchSample>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_batch_samples_id");

            entity.ToTable("tbl_batch_samples", "phcp_120923");

            entity.HasIndex(e => e.SampleNumber, "sample_number");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BatchId).HasColumnName("batch_id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.SampleNumber).HasColumnName("sample_number");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<TblBeauticianEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_beautician_event_id");

            entity.ToTable("tbl_beautician_event", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.AltContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("alt_contact_number");
            entity.Property(e => e.Cnic)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cnic");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contact_number");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.District).HasColumnName("district");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("dob");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.IsHbvTest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_hbv_test");
            entity.Property(e => e.IsHcvTest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_hcv_test");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("marital_status");
            entity.Property(e => e.Occupation).HasColumnName("occupation");
            entity.Property(e => e.Qualification).HasColumnName("qualification");
            entity.Property(e => e.SpouseName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("spouse_name");
            entity.Property(e => e.Tehsil).HasColumnName("tehsil");
            entity.Property(e => e.UcNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("uc_number");
            entity.Property(e => e.UserHospital).HasColumnName("user_hospital");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<TblBloodBankResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_blood_bank_results_id");

            entity.ToTable("tbl_blood_bank_results", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.HbvScreeningResult)
                .HasMaxLength(1)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("hbv_screening_result");
            entity.Property(e => e.HcvScreeningResult)
                .HasMaxLength(1)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("hcv_screening_result");
            entity.Property(e => e.PatientType)
                .HasMaxLength(45)
                .HasColumnName("patient_type");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.ScreeningMethods)
                .HasMaxLength(255)
                .HasColumnName("screening_methods");
        });

        modelBuilder.Entity<TblBpNormalization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_bp_normalization_id");

            entity.ToTable("tbl_bp_normalization", "phcp_120923");

            entity.HasIndex(e => e.BaselineType, "baseline_type");

            entity.HasIndex(e => e.Pid, "pid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BaselineType)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'HBV')")
                .HasColumnName("baseline_type");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Diastolic).HasColumnName("diastolic");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.Pulse).HasColumnName("pulse");
            entity.Property(e => e.SampleId).HasColumnName("sample_id");
            entity.Property(e => e.Systolic).HasColumnName("systolic");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Weight).HasColumnName("weight");
        });

        modelBuilder.Entity<TblCallcenter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_callcenter_id");

            entity.ToTable("tbl_callcenter", "phcp_120923");

            entity.HasIndex(e => e.CreatedB, "created");

            entity.HasIndex(e => e.District, "district");

            entity.HasIndex(e => e.Division, "division");

            entity.HasIndex(e => e.Gender, "gender");

            entity.HasIndex(e => e.Gender, "gender_2");

            entity.HasIndex(e => e.Hospital, "hospital");

            entity.HasIndex(e => e.NextOfKinCnic, "next_of_kin_cnic");

            entity.HasIndex(e => e.SelfCnic, "self_cnic");

            entity.HasIndex(e => e.Tehsil, "tehsil");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.AltPhone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("alt_phone");
            entity.Property(e => e.CnicStatus).HasColumnName("cnic_status");
            entity.Property(e => e.ContactPhone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("contact_phone");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.CreatedB)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("created_b");
            entity.Property(e => e.District).HasColumnName("district");
            entity.Property(e => e.Division).HasColumnName("division");
            entity.Property(e => e.Fathername)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("fathername");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Hbv).HasColumnName("hbv");
            entity.Property(e => e.Hcv).HasColumnName("hcv");
            entity.Property(e => e.Hospital).HasColumnName("hospital");
            entity.Property(e => e.IsPatient)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_patient");
            entity.Property(e => e.LabName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("lab_name");
            entity.Property(e => e.LandMark)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("land_mark");
            entity.Property(e => e.MonthlyIncome)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("monthly_income");
            entity.Property(e => e.MrnNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("mrn_no");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.NextOfKinCnic)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("next_of_kin_cnic");
            entity.Property(e => e.NoCnicReason)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("no_cnic_reason");
            entity.Property(e => e.NoOfDependents).HasColumnName("no_of_dependents");
            entity.Property(e => e.SelfCnic)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("self_cnic");
            entity.Property(e => e.Tehsil).HasColumnName("tehsil");
            entity.Property(e => e.TestDate)
                .HasColumnType("date")
                .HasColumnName("test_date");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<TblCallcenterStat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_callcenter_stats_id");

            entity.ToTable("tbl_callcenter_stats", "phcp_120923");

            entity.HasIndex(e => e.Date, "date");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AbandonedCalls).HasColumnName("abandoned_calls");
            entity.Property(e => e.AnsweredCalls).HasColumnName("answered_calls");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Hepatitis).HasColumnName("hepatitis");
            entity.Property(e => e.NumberOfCalls).HasColumnName("number_of_calls");
            entity.Property(e => e.Samplereceived).HasColumnName("samplereceived");
            entity.Property(e => e.TotalRegistration).HasColumnName("total_registration");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<TblCbcParameter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_cbc_parameter_id");

            entity.ToTable("tbl_cbc_parameter", "phcp_120923");

            entity.HasIndex(e => e.BaselineType, "baseline_type");

            entity.HasIndex(e => e.Pid, "pid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Alt).HasColumnName("alt");
            entity.Property(e => e.Apri)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apri");
            entity.Property(e => e.Ast).HasColumnName("ast");
            entity.Property(e => e.BaselineType)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'HBV')")
                .HasColumnName("baseline_type");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.FollowUpNo)
                .HasDefaultValueSql("((0))")
                .HasColumnName("follow_up_no");
            entity.Property(e => e.Hemoglobin).HasColumnName("hemoglobin");
            entity.Property(e => e.LabName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("lab_name");
            entity.Property(e => e.OtherLabName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("other_lab_name");
            entity.Property(e => e.PcrResult)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("pcr_result");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.Platelet).HasColumnName("platelet");
            entity.Property(e => e.ResultType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("result_type");
            entity.Property(e => e.SampleId).HasColumnName("sample_id");
            entity.Property(e => e.Tlc).HasColumnName("tlc");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.ViralCount).HasColumnName("viral_count");
        });

        modelBuilder.Entity<TblCompletedIssue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_completed_issue_id");

            entity.ToTable("tbl_completed_issue", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Hbv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("hbv");
            entity.Property(e => e.Hcv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("hcv");
            entity.Property(e => e.MrnNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("mrn_no");
        });

        modelBuilder.Entity<TblCronJobsLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_cron_jobs_log", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.CronType)
                .HasMaxLength(29)
                .IsUnicode(false)
                .HasColumnName("cron_type");
            entity.Property(e => e.ExecutionEndTime)
                .HasPrecision(0)
                .HasColumnName("execution_end_time");
            entity.Property(e => e.ExecutionStartTime)
                .HasPrecision(0)
                .HasColumnName("execution_start_time");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
        });

        modelBuilder.Entity<TblDispatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_dispatch_id");

            entity.ToTable("tbl_dispatch", "phcp_120923");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Batchno)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("batchno");
            entity.Property(e => e.Cnic)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("cnic");
            entity.Property(e => e.Consignment)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("consignment");
            entity.Property(e => e.Contactno)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contactno");
            entity.Property(e => e.Courier)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("courier");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Designation)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("designation");
            entity.Property(e => e.Dispatchmade)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("dispatchmade");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Organization)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("organization");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<TblDistrict>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_district_id");

            entity.ToTable("tbl_district", "phcp_120923");

            entity.HasIndex(e => e.DistrictCode, "district_code");

            entity.HasIndex(e => e.DivisionCode, "division_code");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DistrictCode).HasColumnName("district_code");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("district_name");
            entity.Property(e => e.DivisionCode).HasColumnName("division_code");
            entity.Property(e => e.IsActive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'Y')")
                .HasColumnName("is_active");
        });

        modelBuilder.Entity<TblDivision>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_division_id");

            entity.ToTable("tbl_division", "phcp_120923");

            entity.HasIndex(e => e.DivisionCode, "division_code");

            entity.HasIndex(e => e.DivisionName, "idx_tbl_division_division_name");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DivisionCode).HasColumnName("division_code");
            entity.Property(e => e.DivisionName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("division_name");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'Y')")
                .HasColumnName("status");
        });

        modelBuilder.Entity<TblDrugInteractionMedSec>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_drug_interaction_med_sec_id");

            entity.ToTable("tbl_drug_interaction_med_sec", "phcp_120923");

            entity.HasIndex(e => e.BaselineType, "baseline_type");

            entity.HasIndex(e => e.Created, "created");

            entity.HasIndex(e => e.Disburse3MnthDose, "disburse_3");

            entity.HasIndex(e => e.Disburse6MnthDose, "disburse_6");

            entity.HasIndex(e => e.Enticavir, "ent");

            entity.HasIndex(e => e.HospitalId, "hospital_id");

            entity.HasIndex(e => e.IsDemote, "is_demote");

            entity.HasIndex(e => e.Pid, "pid");

            entity.HasIndex(e => e.SdPack, "sd_pack");

            entity.HasIndex(e => e.SrPack, "sr_pack");

            entity.HasIndex(e => e.Telbuvidine, "telbu");

            entity.HasIndex(e => e.Tenofovir, "teno");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdminFollowUpDate).HasColumnName("admin_follow_up_date");
            entity.Property(e => e.AdminUser).HasColumnName("admin_user");
            entity.Property(e => e.BaselineType)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'HBV')")
                .HasColumnName("baseline_type");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Disburse3MnthDose)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("disburse_3_mnth_dose");
            entity.Property(e => e.Disburse6MnthDose)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("disburse_6_mnth_dose");
            entity.Property(e => e.DrugInteraction)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("drug_interaction");
            entity.Property(e => e.Enticavir)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("enticavir");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.IsAdminFollowUp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_admin_follow_up");
            entity.Property(e => e.IsDemote)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_demote");
            entity.Property(e => e.IsDuplicate)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_duplicate");
            entity.Property(e => e.IsImportedData)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_imported_data");
            entity.Property(e => e.IsTerminate)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_terminate");
            entity.Property(e => e.MedicineId).HasColumnName("medicine_id");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.SampleId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("sample_id");
            entity.Property(e => e.SampleRecommended)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("sample_recommended");
            entity.Property(e => e.SampleSvrFlag)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("sample_svr_flag");
            entity.Property(e => e.SdPack)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("sd_pack");
            entity.Property(e => e.SdrPack)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("sdr_pack");
            entity.Property(e => e.SrPack)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("sr_pack");
            entity.Property(e => e.Telbuvidine)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("telbuvidine");
            entity.Property(e => e.Tenofovir)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("tenofovir");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<TblDrugInteractionMedSecBk31Jan2020>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_drug_interaction_med_sec_bk_31_jan2020_id");

            entity.ToTable("tbl_drug_interaction_med_sec_bk_31_jan2020", "phcp_120923");

            entity.HasIndex(e => e.BaselineType, "baseline_type");

            entity.HasIndex(e => e.Created, "created");

            entity.HasIndex(e => e.Disburse3MnthDose, "disburse_3");

            entity.HasIndex(e => e.Disburse6MnthDose, "disburse_6");

            entity.HasIndex(e => e.Enticavir, "ent");

            entity.HasIndex(e => e.IsDemote, "is_demote");

            entity.HasIndex(e => e.Pid, "pid");

            entity.HasIndex(e => e.SdPack, "sd_pack");

            entity.HasIndex(e => e.SrPack, "sr_pack");

            entity.HasIndex(e => e.Telbuvidine, "telbu");

            entity.HasIndex(e => e.Tenofovir, "teno");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdminFollowUpDate).HasColumnName("admin_follow_up_date");
            entity.Property(e => e.AdminUser).HasColumnName("admin_user");
            entity.Property(e => e.BaselineType)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'HBV')")
                .HasColumnName("baseline_type");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Disburse3MnthDose)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("disburse_3_mnth_dose");
            entity.Property(e => e.Disburse6MnthDose)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("disburse_6_mnth_dose");
            entity.Property(e => e.DrugInteraction)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("drug_interaction");
            entity.Property(e => e.Enticavir)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("enticavir");
            entity.Property(e => e.IsAdminFollowUp)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_admin_follow_up");
            entity.Property(e => e.IsDemote)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_demote");
            entity.Property(e => e.IsDuplicate)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_duplicate");
            entity.Property(e => e.IsImportedData)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_imported_data");
            entity.Property(e => e.IsTerminate)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_terminate");
            entity.Property(e => e.MedicineId).HasColumnName("medicine_id");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.SampleId).HasColumnName("sample_id");
            entity.Property(e => e.SampleRecommended)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("sample_recommended");
            entity.Property(e => e.SampleSvrFlag)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("sample_svr_flag");
            entity.Property(e => e.SdPack)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("sd_pack");
            entity.Property(e => e.SdrPack)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("sdr_pack");
            entity.Property(e => e.SrPack)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("sr_pack");
            entity.Property(e => e.Telbuvidine)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("telbuvidine");
            entity.Property(e => e.Tenofovir)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("tenofovir");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<TblEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_events_id");

            entity.ToTable("tbl_events", "phcp_120923");

            entity.HasIndex(e => e.Created, "created");

            entity.HasIndex(e => e.DistrictId, "district_id");

            entity.HasIndex(e => e.DivisionId, "division_id");

            entity.HasIndex(e => e.EndDate, "end_date");

            entity.HasIndex(e => e.HospitalId, "hospital_id");

            entity.HasIndex(e => e.StartDate, "start_date");

            entity.HasIndex(e => e.Status, "status");

            entity.HasIndex(e => e.TehsilId, "tehsil_id");

            entity.HasIndex(e => e.Updated, "updated");

            entity.HasIndex(e => e.UpdatedBy, "updated_by");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Description)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.DivisionId).HasColumnName("division_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.EventName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("event_name");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.Industry)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("industry");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TehsilId).HasColumnName("tehsil_id");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<TblEventIndicator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_event_indicator_id");

            entity.ToTable("tbl_event_indicator", "phcp_120923");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(255)
                .HasColumnName("category");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasColumnName("status");
        });

        modelBuilder.Entity<TblFamilyAssesment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_family_assesment_id");

            entity.ToTable("tbl_family_assesment", "phcp_120923");

            entity.HasIndex(e => e.ParentId, "parent_id_index");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Cnic)
                .HasMaxLength(15)
                .HasColumnName("CNIC");
            entity.Property(e => e.Contact).HasMaxLength(45);
            entity.Property(e => e.Created)
                .HasPrecision(0)
                .HasColumnName("created");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.MrnNo)
                .HasMaxLength(255)
                .HasColumnName("mrn_no");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.PcrConfirmationHbv)
                .HasMaxLength(1)
                .HasColumnName("pcr_confirmation_hbv");
            entity.Property(e => e.PcrConfirmationHcv)
                .HasMaxLength(1)
                .HasColumnName("pcr_confirmation_hcv");
            entity.Property(e => e.PreviousHbvTest)
                .HasMaxLength(1)
                .HasColumnName("previous_hbv_test");
            entity.Property(e => e.PreviousHcvTest)
                .HasMaxLength(1)
                .HasColumnName("previous_hcv_test");
            entity.Property(e => e.Relation).HasMaxLength(50);
            entity.Property(e => e.Updated)
                .HasPrecision(0)
                .HasColumnName("updated");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
        });

        modelBuilder.Entity<TblFinalStock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_final_stock_id");

            entity.ToTable("tbl_final_stock", "phcp_120923");

            entity.HasIndex(e => e.HospitalId, "hospital_id");

            entity.HasIndex(e => e.MedicineId, "medicine_id");

            entity.HasIndex(e => e.RemainingStock, "remaining_stock");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Districts)
                .HasMaxLength(255)
                .HasColumnName("DISTRICTS");
            entity.Property(e => e.HealthcareFacility)
                .HasMaxLength(255)
                .HasColumnName("HEALTHCARE_FACILITY");
            entity.Property(e => e.HospitalId).HasColumnName("Hospital_ID");
            entity.Property(e => e.MedicineId).HasColumnName("medicine_id");
            entity.Property(e => e.RemainingStock).HasColumnName("remaining_stock");
            entity.Property(e => e.RemainingStock2)
                .HasDefaultValueSql("((0))")
                .HasColumnName("remaining_stock_2");
            entity.Property(e => e.SdStockEntry).HasColumnName("SD_Stock_Entry");
        });

        modelBuilder.Entity<TblFinalStockBk31jan20>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_final_stock_bk_31jan20_id");

            entity.ToTable("tbl_final_stock_bk_31jan20", "phcp_120923");

            entity.HasIndex(e => e.HospitalId, "hospital_id");

            entity.HasIndex(e => e.MedicineId, "medicine_id");

            entity.HasIndex(e => e.RemainingStock, "remaining_stock");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Districts)
                .HasMaxLength(255)
                .HasColumnName("DISTRICTS");
            entity.Property(e => e.HealthcareFacility)
                .HasMaxLength(255)
                .HasColumnName("HEALTHCARE_FACILITY");
            entity.Property(e => e.HospitalId).HasColumnName("Hospital_ID");
            entity.Property(e => e.MedicineId).HasColumnName("medicine_id");
            entity.Property(e => e.RemainingStock).HasColumnName("remaining_stock");
            entity.Property(e => e.RemainingStock2)
                .HasDefaultValueSql("((0))")
                .HasColumnName("remaining_stock_2");
            entity.Property(e => e.SdStockEntry).HasColumnName("SD_Stock_Entry");
        });

        modelBuilder.Entity<TblGender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_gender_id");

            entity.ToTable("tbl_gender", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'y')")
                .HasColumnName("status");
        });

        modelBuilder.Entity<TblHealthFacility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_health_facility_id");

            entity.ToTable("tbl_health_facility", "phcp_120923");

            entity.HasIndex(e => e.DistrictCode, "district_code");

            entity.HasIndex(e => e.DivisionCode, "division_code");

            entity.HasIndex(e => e.IsActive, "is_active");

            entity.HasIndex(e => e.TehsilCode, "tehsil_code");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category).HasColumnName("category");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DistrictCode).HasColumnName("district_code");
            entity.Property(e => e.DivisionCode).HasColumnName("division_code");
            entity.Property(e => e.HfName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hf_name");
            entity.Property(e => e.HfRegionCode).HasColumnName("hf_region_code");
            entity.Property(e => e.HfTypeCode).HasColumnName("hf_type_code");
            entity.Property(e => e.Hfac)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hfac");
            entity.Property(e => e.Identifier)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("identifier");
            entity.Property(e => e.IsActive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'Y')")
                .HasColumnName("is_active");
            entity.Property(e => e.MoPosition)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("mo_position");
            entity.Property(e => e.Province).HasColumnName("province");
            entity.Property(e => e.ResponsibleUser)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("responsible_user");
            entity.Property(e => e.TehsilCode).HasColumnName("tehsil_code");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<TblHealthFacilityType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_health_facility_type_id");

            entity.ToTable("tbl_health_facility_type", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HealthFacilityName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("health_facility_name");
            entity.Property(e => e.HfTypeCode).HasColumnName("hf_type_code");
            entity.Property(e => e.Sorting).HasColumnName("sorting");
        });

        modelBuilder.Entity<TblHealthFaciltyCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_health_facilty_category_id");

            entity.ToTable("tbl_health_facilty_category", "phcp_120923");

            entity.HasIndex(e => e.Status, "status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'Y')")
                .HasColumnName("status");
        });

        modelBuilder.Entity<TblHealthcareFacilityReporting>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_healthcare_facility_reporting", "phcp_120923");

            entity.HasIndex(e => e.PcrSampleAcceptedInLabCount, "PCR_sample_accepted_in_lab_count");

            entity.HasIndex(e => e.PcrSampleDetectedCount, "PCR_sample_detected_count");

            entity.HasIndex(e => e.PcrSampleNotDetectedCount, "PCR_sample_not_detected_count");

            entity.HasIndex(e => e.PcrSampleReceivedInLabCount, "PCR_sample_received_in_lab_count");

            entity.HasIndex(e => e.PcrSampleRejectedInLabCount, "PCR_sample_rejected_in_lab_count");

            entity.HasIndex(e => e.PcrSampleResampleCount, "PCR_sample_resample_count");

            entity.HasIndex(e => e.FirstMonthMedicineDeliveredForHcvCount, "first_month_medicine_delivered_for_HCV_count");

            entity.HasIndex(e => e.Id, "id");

            entity.HasIndex(e => e.NewPatientCount, "new_patient_count");

            entity.HasIndex(e => e.PatientRegisteredCount, "patient_registered_count");

            entity.HasIndex(e => e.PatientsEnrolledInTreatmentCount, "patients_enrolled_in_treatment_count");

            entity.HasIndex(e => e.PrediagnosedPatientCount, "prediagnosed_patient_count");

            entity.HasIndex(e => e.SampleCollectedCount, "sample_collected_count");

            entity.HasIndex(e => e.SampleCollectionPendingCount, "sample_collection_pending_count");

            entity.HasIndex(e => e.ScreeningPerformedCount, "screening_performed_count");

            entity.HasIndex(e => e.ScreeningPositiveForHbvCount, "screening_positive_for_HBV_count");

            entity.HasIndex(e => e.ScreeningPositiveForHcvCount, "screening_positive_for_HCV_count");

            entity.HasIndex(e => e.ScreeningPositiveForBothCount, "screening_positive_for_both_count");

            entity.HasIndex(e => e.SecondMonthMedicineDeliveredForHcvCount, "second_month_medicine_delivered_for_HCV_count");

            entity.HasIndex(e => e.StockDisbursedCount, "stock_disbursed_count");

            entity.HasIndex(e => e.StockInHandCount, "stock_in_hand_count");

            entity.HasIndex(e => e.StockMadeAvailableCount, "stock_made_available_count");

            entity.HasIndex(e => e.ThirdMonthMedicineDeliveredForHcvCount, "third_month_medicine_delivered_for_HCV_count");

            entity.HasIndex(e => e.VaccineDoseAdministeredCount, "vaccine_dose_administered_count");

            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.FacilityId).HasColumnName("facility_id");
            entity.Property(e => e.FacilityName)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("facility_name");
            entity.Property(e => e.FirstMonthMedicineDeliveredForHcvCount).HasColumnName("first_month_medicine_delivered_for_HCV_count");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.NewPatientCount).HasColumnName("new_patient_count");
            entity.Property(e => e.PatientRegisteredCount).HasColumnName("patient_registered_count");
            entity.Property(e => e.PatientsEnrolledInTreatmentCount).HasColumnName("patients_enrolled_in_treatment_count");
            entity.Property(e => e.PcrSampleAcceptedInLabCount).HasColumnName("PCR_sample_accepted_in_lab_count");
            entity.Property(e => e.PcrSampleDetectedCount).HasColumnName("PCR_sample_detected_count");
            entity.Property(e => e.PcrSampleNotDetectedCount).HasColumnName("PCR_sample_not_detected_count");
            entity.Property(e => e.PcrSampleReceivedInLabCount).HasColumnName("PCR_sample_received_in_lab_count");
            entity.Property(e => e.PcrSampleRejectedInLabCount).HasColumnName("PCR_sample_rejected_in_lab_count");
            entity.Property(e => e.PcrSampleResampleCount).HasColumnName("PCR_sample_resample_count");
            entity.Property(e => e.PrediagnosedPatientCount).HasColumnName("prediagnosed_patient_count");
            entity.Property(e => e.SampleCollectedCount).HasColumnName("sample_collected_count");
            entity.Property(e => e.SampleCollectionPendingCount).HasColumnName("sample_collection_pending_count");
            entity.Property(e => e.ScreeningPerformedCount).HasColumnName("screening_performed_count");
            entity.Property(e => e.ScreeningPositiveForBothCount).HasColumnName("screening_positive_for_both_count");
            entity.Property(e => e.ScreeningPositiveForHbvCount).HasColumnName("screening_positive_for_HBV_count");
            entity.Property(e => e.ScreeningPositiveForHcvCount).HasColumnName("screening_positive_for_HCV_count");
            entity.Property(e => e.SecondMonthMedicineDeliveredForHcvCount).HasColumnName("second_month_medicine_delivered_for_HCV_count");
            entity.Property(e => e.StockDisbursedCount).HasColumnName("stock_disbursed_count");
            entity.Property(e => e.StockInHandCount).HasColumnName("stock_in_hand_count");
            entity.Property(e => e.StockMadeAvailableCount).HasColumnName("stock_made_available_count");
            entity.Property(e => e.ThirdMonthMedicineDeliveredForHcvCount).HasColumnName("third_month_medicine_delivered_for_HCV_count");
            entity.Property(e => e.VaccineDoseAdministeredCount).HasColumnName("vaccine_dose_administered_count");
        });

        modelBuilder.Entity<TblHospitalIndicatorDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_hospital_indicator_data_id");

            entity.ToTable("tbl_hospital_indicator_data", "phcp_120923");

            entity.HasIndex(e => e.Created, "created");

            entity.HasIndex(e => e.HospitalId, "hospital_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActivationDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Activation_Date");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.EnrolledInTreatmentWithSd)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Enrolled_in_Treatment_with_SD");
            entity.Property(e => e.EnrolledInTreatmentWithSr)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Enrolled_in_Treatment_with_SR");
            entity.Property(e => e.EnrolledInTreatmentWithSrd)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Enrolled_in_Treatment_with_SRD");
            entity.Property(e => e.EntecavirBooked)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Entecavir_Booked");
            entity.Property(e => e.EntecavirDisbursed)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Entecavir_Disbursed");
            entity.Property(e => e.EntecavirRemaining)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Entecavir_Remaining");
            entity.Property(e => e.EntecavirSupplied)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Entecavir_Supplied");
            entity.Property(e => e.FacilityName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Facility_Name");
            entity.Property(e => e.FirstMonthMedicineDelivered)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_month_medicine_delivered");
            entity.Property(e => e.HbvPatientEnrolledInOldRegimen)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HBV_Patient_Enrolled_in_Old_Regimen");
            entity.Property(e => e.HbvPatientsEnrolledInNewRegimen)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HBV_Patients_Enrolled_in_New_Regimen");
            entity.Property(e => e.HcvPateintsCured)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HCV_Pateints_Cured");
            entity.Property(e => e.HcvPatientsEnrolledInNewRegimen)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HCV_Patients_Enrolled_in_New_Regimen");
            entity.Property(e => e.HcvPatientsEnrolledInOldRegimen)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HCV_Patients_Enrolled_in_Old_Regimen");
            entity.Property(e => e.HcvPatientsTreatmentFailed)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HCV_Patients_Treatment_Failed");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.IsRegister)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("is_register");
            entity.Property(e => e.NewPatient)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("new_patient");
            entity.Property(e => e.NumberOfActiveDays)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Number_of_Active_Days");
            entity.Property(e => e.PatientsPendingTreatmentEnollment)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Patients_pending_Treatment_Enollment");
            entity.Property(e => e.PcrSampleAcceptedInLab)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pcr_sample_accepted_in_lab");
            entity.Property(e => e.PcrSampleDetected)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pcr_sample_detected");
            entity.Property(e => e.PcrSampleNotDetected)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pcr_sample_not_detected");
            entity.Property(e => e.PcrSampleReceivedInLab)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pcr_sample_received_in_lab");
            entity.Property(e => e.PcrSampleRejectedInLab)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pcr_sample_rejected_in_lab");
            entity.Property(e => e.PcrSampleResample)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pcr_sample_resample");
            entity.Property(e => e.PreDiagnosed)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pre_diagnosed");
            entity.Property(e => e.SampleCollected)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sample_collected");
            entity.Property(e => e.SampleCollectionPending)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sample_collection_pending");
            entity.Property(e => e.ScreenedPosBoth)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("screened_pos_both");
            entity.Property(e => e.ScreenedPosHbv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("screened_pos_hbv");
            entity.Property(e => e.ScreenedPosHcv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("screened_pos_hcv");
            entity.Property(e => e.ScreeningPerformed)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("screening_performed");
            entity.Property(e => e.SdBooked)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SD_Booked");
            entity.Property(e => e.SdDisbursed)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SD_disbursed");
            entity.Property(e => e.SdRemaining)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SD_Remaining");
            entity.Property(e => e.SdSupplied)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SD_Supplied");
            entity.Property(e => e.SdTherapyFollowUpsDeafulted)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SD_Therapy_Follow_Ups_Deafulted");
            entity.Property(e => e.SdTherapyFollowUpsDue)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SD_Therapy_Follow_Ups_Due");
            entity.Property(e => e.SdTherapyFollowUpsOverdue)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SD_Therapy_Follow_Ups_Overdue");
            entity.Property(e => e.SecondMonthMedicineDelivered)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("second_month_medicine_delivered");
            entity.Property(e => e.SrBooked)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SR_Booked");
            entity.Property(e => e.SrDisbursed)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SR_Disbursed");
            entity.Property(e => e.SrRemaining)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SR_Remaining");
            entity.Property(e => e.SrSupplied)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SR_Supplied");
            entity.Property(e => e.SrTherapyFollowUpsDeafulted)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SR_Therapy_Follow_Ups_Deafulted");
            entity.Property(e => e.SrTherapyFollowUpsDue)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SR_Therapy_Follow_Ups_Due");
            entity.Property(e => e.SrTherapyFollowUpsOverdue)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SR_Therapy_Follow_Ups_Overdue");
            entity.Property(e => e.SrdTherapyFollowUpsDeafulted)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SRD_Therapy_Follow_Ups_Deafulted");
            entity.Property(e => e.SrdTherapyFollowUpsDue)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SRD_Therapy_Follow_Ups_Due");
            entity.Property(e => e.SrdTherapyFollowUpsOverdue)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SRD_Therapy_Follow_Ups_Overdue");
            entity.Property(e => e.StockDisbursed)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("stock_disbursed");
            entity.Property(e => e.StockInHand)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("stock_in_hand");
            entity.Property(e => e.StockMadeAvailable)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("stock_made_available");
            entity.Property(e => e.SvrCollectedCount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SVR_Collected_Count");
            entity.Property(e => e.SvrPending)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SVR_Pending");
            entity.Property(e => e.SvrPendingCountWithGap)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SVR_Pending_Count_with_gap");
            entity.Property(e => e.TelbuvidineBooked)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Telbuvidine_Booked");
            entity.Property(e => e.TelbuvidineDisbursed)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Telbuvidine_Disbursed");
            entity.Property(e => e.TelbuvidineRemaining)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Telbuvidine_Remaining");
            entity.Property(e => e.TelbuvidineSupplied)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Telbuvidine_Supplied");
            entity.Property(e => e.TenofovirBooked)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Tenofovir_Booked");
            entity.Property(e => e.TenofovirDisbursed)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Tenofovir_Disbursed");
            entity.Property(e => e.TenofovirRemaining)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Tenofovir_Remaining");
            entity.Property(e => e.TenofovirSupplied)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Tenofovir_Supplied");
            entity.Property(e => e.ThirdMonthMedicineDelivered)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("third_month_medicine_delivered");
            entity.Property(e => e.TreatmentWithEntecavir)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Treatment_with_Entecavir");
            entity.Property(e => e.TreatmentWithTelbuvidine)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Treatment_with_Telbuvidine");
            entity.Property(e => e.TreatmentWithTenofovir)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Treatment_with_Tenofovir");
            entity.Property(e => e.VaccinationSecondDoseDeafulters)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Vaccination_Second_Dose_Deafulters");
            entity.Property(e => e.VaccinationThirdDoseDeafulters)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Vaccination_Third_Dose_Deafulters");
            entity.Property(e => e.VaccineFirstDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Vaccine_First_Dose");
            entity.Property(e => e.VaccineFirstDosePendingCount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Vaccine_First_Dose_Pending_Count");
            entity.Property(e => e.VaccineSecondDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Vaccine_Second_Dose");
            entity.Property(e => e.VaccineSecondDoseOverdueCount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Vaccine_Second_Dose_Overdue_Count");
            entity.Property(e => e.VaccineThirdDose)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Vaccine_Third_Dose");
            entity.Property(e => e.VaccineThirdDoseOverdueCount)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Vaccine_Third_Dose_Overdue_Count");
        });

        modelBuilder.Entity<TblHouseholdContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_household_contacts_id");

            entity.ToTable("tbl_household_contacts", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cnic)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("cnic");
            entity.Property(e => e.ContactNo)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("contact_no");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Dob)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("dob");
            entity.Property(e => e.GenderId).HasColumnName("gender_id");
            entity.Property(e => e.MicroScreenId).HasColumnName("micro_screen_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.RelationId).HasColumnName("relation_id");
            entity.Property(e => e.TotalHouseheld).HasColumnName("total_househeld");
        });

        modelBuilder.Entity<TblIndicator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_indicator_id");

            entity.ToTable("tbl_indicator", "phcp_120923");

            entity.Property(e => e.Id)
                .HasMaxLength(255)
                .HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(255)
                .HasColumnName("category");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasColumnName("status");
            entity.Property(e => e.WeeklyReport)
                .HasMaxLength(1)
                .HasColumnName("weekly_report");
        });

        modelBuilder.Entity<TblIndicatorCat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_indicator_cat_id");

            entity.ToTable("tbl_indicator_cat", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'Y')")
                .HasColumnName("status");
        });

        modelBuilder.Entity<TblLabAction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_lab_action_id");

            entity.ToTable("tbl_lab_action", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'Y')")
                .HasColumnName("status");
        });

        modelBuilder.Entity<TblLabBatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_lab_batch_id");

            entity.ToTable("tbl_lab_batch", "phcp_120923");

            entity.HasIndex(e => e.AssignedBy, "assigned_by");

            entity.HasIndex(e => e.AssignedTo, "assigned_to");

            entity.HasIndex(e => e.BatchNumber, "batch_number");

            entity.HasIndex(e => e.Id, "id");

            entity.HasIndex(e => e.IsResult, "is_result");

            entity.HasIndex(e => e.ResultTime, "result_time");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssignedBy).HasColumnName("assigned_by");
            entity.Property(e => e.AssignedTo)
                .HasDefaultValueSql("((0))")
                .HasColumnName("assigned_to");
            entity.Property(e => e.BatchNumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("batch_number");
            entity.Property(e => e.CreatedTime).HasColumnName("created_time");
            entity.Property(e => e.IsResult)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_result");
            entity.Property(e => e.ResultTime).HasColumnName("result_time");
            entity.Property(e => e.UpdateTime).HasColumnName("update_time");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<TblLabBatchSample>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_lab_batch_sample", "phcp_120923");

            entity.HasIndex(e => e.BatchId, "batch_id");

            entity.HasIndex(e => e.SampleId, "sample_id");

            entity.Property(e => e.BatchId).HasColumnName("batch_id");
            entity.Property(e => e.IsDiscard)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_discard");
            entity.Property(e => e.SampleId).HasColumnName("sample_id");
        });

        modelBuilder.Entity<TblLabResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_lab_result_id");

            entity.ToTable("tbl_lab_result", "phcp_120923");

            entity.HasIndex(e => e.BatchId, "batch_id");

            entity.HasIndex(e => e.Created, "created");

            entity.HasIndex(e => e.Id, "id");

            entity.HasIndex(e => e.IsHbvDetected, "is_hbv_detected");

            entity.HasIndex(e => e.IsHcvDetected, "is_hcv_detected");

            entity.HasIndex(e => e.SampleId, "sample_id");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BatchId).HasColumnName("batch_id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Hbv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("hbv");
            entity.Property(e => e.Hcv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("hcv");
            entity.Property(e => e.IsBatchDiscard)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_batch_discard");
            entity.Property(e => e.IsHbvDetected)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_hbv_detected");
            entity.Property(e => e.IsHcvDetected)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_hcv_detected");
            entity.Property(e => e.IsSoftDelete)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_soft_delete");
            entity.Property(e => e.SampleId).HasColumnName("sample_id");
            entity.Property(e => e.TestRequired)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("test_required");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<TblLabSample>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_lab_sample_id");

            entity.ToTable("tbl_lab_sample", "phcp_120923");

            entity.HasIndex(e => e.ActionId, "action_id");

            entity.HasIndex(e => e.BatchNumber, "batch_number");

            entity.HasIndex(e => e.Created, "created");

            entity.HasIndex(e => e.HospitalId, "hospital_id");

            entity.HasIndex(e => e.HwPid, "hw_pid");

            entity.HasIndex(e => e.Id, "id");

            entity.HasIndex(e => e.IsLabBatch, "is_lab_batch");

            entity.HasIndex(e => e.IsRefered, "is_refered");

            entity.HasIndex(e => e.IsResample, "is_resample");

            entity.HasIndex(e => e.Pid, "pid");

            entity.HasIndex(e => e.SampleNumber, "sample_number");

            entity.HasIndex(e => e.TestType, "test_type");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActionId).HasColumnName("action_id");
            entity.Property(e => e.BatchNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("batch_number");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.HwPid).HasColumnName("hw_pid");
            entity.Property(e => e.IsIgnore)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_ignore");
            entity.Property(e => e.IsLabBatch)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_lab_batch");
            entity.Property(e => e.IsRefered)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_refered");
            entity.Property(e => e.IsResample)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_resample");
            entity.Property(e => e.IsSampleSvr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_sample_svr");
            entity.Property(e => e.IsTerminate)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_terminate");
            entity.Property(e => e.MicroId).HasColumnName("micro_id");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.RejectedBy).HasColumnName("rejected_by");
            entity.Property(e => e.RejectedTime).HasColumnName("rejected_time");
            entity.Property(e => e.SampleNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sample_number");
            entity.Property(e => e.TestType)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("test_type");
            entity.Property(e => e.TotalDuplicate)
                .HasDefaultValueSql("((0))")
                .HasColumnName("total_duplicate");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<TblLabortary>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_labortary_id");

            entity.ToTable("tbl_labortary", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.CreationDate)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CREATION_DATE");
            entity.Property(e => e.EnableFlag)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ENABLE_FLAG");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.UpdationDate)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("UPDATION_DATE");
            entity.Property(e => e.UpdtedBy)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("UPDTED_BY");
        });

        modelBuilder.Entity<TblLostFollowupReason>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_lost_followup_reasons_Id");

            entity.ToTable("tbl_lost_followup_reasons", "phcp_120923");

            entity.Property(e => e.Created)
                .HasPrecision(0)
                .HasColumnName("created");
            entity.Property(e => e.Reason).HasMaxLength(150);
        });

        modelBuilder.Entity<TblMaritalStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_marital_status_id");

            entity.ToTable("tbl_marital_status", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'y')")
                .HasColumnName("status");
        });

        modelBuilder.Entity<TblMedDeliveryLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_med_delivery_log_id");

            entity.ToTable("tbl_med_delivery_log", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BookingDate)
                .HasPrecision(0)
                .HasColumnName("booking_date");
            entity.Property(e => e.CloseCase)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("close_case");
            entity.Property(e => e.Consignee)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("consignee");
            entity.Property(e => e.ConsignmentNo).HasColumnName("consignment_no");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.DeliveredBy).HasColumnName("delivered_by");
            entity.Property(e => e.DeliveredTcsDate)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("delivered_tcs_date");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.IsDemote)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_demote");
            entity.Property(e => e.IsReceivedShipment)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_received_shipment");
            entity.Property(e => e.Location)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.MoDeliver)
                .HasDefaultValueSql("((0))")
                .HasColumnName("mo_deliver");
            entity.Property(e => e.MoStatus)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("mo_status");
            entity.Property(e => e.NoOfDosage).HasColumnName("no_of_dosage");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.PendingDeliveryAlert)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("pending_delivery_alert");
            entity.Property(e => e.PrescriptionNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("prescription_no");
            entity.Property(e => e.ReceivedBy)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("received_by");
            entity.Property(e => e.ReceivedHospitalId).HasColumnName("received_hospital_id");
            entity.Property(e => e.Status)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<TblMedicineDisbursementOldRegime>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_medicine_disbursement_old_regime_id");

            entity.ToTable("tbl_medicine_disbursement_old_regime", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created");
            entity.Property(e => e.DemoteStatus)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("demote_status");
            entity.Property(e => e.DoseDeliveredByHand)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("dose_delivered_by_hand");
            entity.Property(e => e.NoOfMedGiven).HasColumnName("no_of_med_given");
            entity.Property(e => e.PatientDemote)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("patient_demote");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
        });

        modelBuilder.Entity<TblMedicinesRecommended>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_medicines_recommended_id");

            entity.ToTable("tbl_medicines_recommended", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.LeafletColor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("leaflet_color");
            entity.Property(e => e.MedicineName)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("medicine_name");
            entity.Property(e => e.MedicineType)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("medicine_type");
        });

        modelBuilder.Entity<TblMissingUpdatedDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_missing_updated_data_id");

            entity.ToTable("tbl_missing_updated_data", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.B)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("b");
            entity.Property(e => e.C)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("c");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Mrn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mrn");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.UserHospital).HasColumnName("user_hospital");
        });

        modelBuilder.Entity<TblNewRegimeMedLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_new_regime_med_log_id");

            entity.ToTable("tbl_new_regime_med_log", "phcp_120923");

            entity.HasIndex(e => e.BaselineType, "baseline_type");

            entity.HasIndex(e => e.HospitalId, "hospital_id");

            entity.HasIndex(e => e.MedicineId, "medicine_id");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.HasIndex(e => e.IsDemote, "s_demote");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BaselineType)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'HBV')")
                .HasColumnName("baseline_type");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.IsDemote)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_demote");
            entity.Property(e => e.MedicineId).HasColumnName("medicine_id");
            entity.Property(e => e.NoOfDosage).HasColumnName("no_of_dosage");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
        });

        modelBuilder.Entity<TblNewRegimeMedicine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_new_regime_medicine_id");

            entity.ToTable("tbl_new_regime_medicine", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MedicineName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("medicine_name");
            entity.Property(e => e.MedicineType)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'HBV')")
                .HasColumnName("medicine_type");
        });

        modelBuilder.Entity<TblNewRegimeStockLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_new_regime_stock_log_id");

            entity.ToTable("tbl_new_regime_stock_log", "phcp_120923");

            entity.HasIndex(e => e.HospitalId, "hospital_id");

            entity.HasIndex(e => e.MedicineId, "medicine_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.MedicineId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("medicine_id");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TehsilId).HasColumnName("tehsil_id");
        });

        modelBuilder.Entity<TblNextOfKin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_next_of_kin_id");

            entity.ToTable("tbl_next_of_kin", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'y')")
                .HasColumnName("status");
        });

        modelBuilder.Entity<TblOccupation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_occupation_id");

            entity.ToTable("tbl_occupation", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'y')")
                .HasColumnName("status");
        });

        modelBuilder.Entity<TblPatient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_patient_id");

            entity.ToTable("tbl_patient", "phcp_120923");

            entity.HasIndex(e => e.Bbl, "BBL");

            entity.HasIndex(e => e.Cbl, "CBL");

            entity.HasIndex(e => e.Lname, "Lname");

            entity.HasIndex(e => e.PcrConfirmationHbv, "PCR_CONFIRM_HBV");

            entity.HasIndex(e => e.PcrConfirmationHcv, "PCR_CONFIRM_HCV");

            entity.HasIndex(e => e.CollectSample, "collect_sample");

            entity.HasIndex(e => e.ContactNoSelf, "contact_no_self");

            entity.HasIndex(e => e.Created, "created");

            entity.HasIndex(e => e.District, "district");

            entity.HasIndex(e => e.Division, "division");

            entity.HasIndex(e => e.Gender, "gender");

            entity.HasIndex(e => e.HcvFristMedicineDate, "hcv_first_medicine_date");

            entity.HasIndex(e => e.HcvInvestigationStatus, "hcv_investigation_status");

            entity.HasIndex(e => e.Hemoglobin, "hemoglobin");

            entity.HasIndex(e => e.HfSampleCollectedHospitalId, "hf_sample_collected_hospital_id");

            entity.HasIndex(e => e.Id, "id");

            entity.HasIndex(e => e.IsAssesment, "is_assesment");

            entity.HasIndex(e => e.IsCirrhoticPatient, "is_cirrhotic_patient");

            entity.HasIndex(e => e.IsIgnore, "is_ignore");

            entity.HasIndex(e => e.IsMedicineDelivered, "is_medicine_delivered");

            entity.HasIndex(e => e.IsOldRegime, "is_old_regime");

            entity.HasIndex(e => e.IsOldRegimeTcs, "is_old_regime_tcs");

            entity.HasIndex(e => e.IsPatientTransferStatus, "is_patient_transfer_status");

            entity.HasIndex(e => e.IsRefered, "is_refered");

            entity.HasIndex(e => e.IsRegister, "is_register");

            entity.HasIndex(e => e.IsSample, "is_sample");

            entity.HasIndex(e => e.IsSvrEligibleHospital, "is_svr_eligible_hospital");

            entity.HasIndex(e => e.IsSvrFormSubmitted, "is_svr_form_submitted");

            entity.HasIndex(e => e.IsSvrRecommended, "is_svr_recommended");

            entity.HasIndex(e => e.IsSvrSample, "is_svr_sample");

            entity.HasIndex(e => e.IsTypeChange, "is_type_change");

            entity.HasIndex(e => e.IsVacinate, "is_vacinate");

            entity.HasIndex(e => e.IsVital, "is_vital");

            entity.HasIndex(e => e.MicroId, "micro_id");

            entity.HasIndex(e => e.MrnNo, "mrn_no");

            entity.HasIndex(e => e.NextOfKin, "next_of_kin");

            entity.HasIndex(e => e.NextStatus, "next_status");

            entity.HasIndex(e => e.NoOfHbvMedicineDelivered, "no_of_hbv_med_delivered");

            entity.HasIndex(e => e.NoOfHcvFollowups, "no_of_hcv_followups");

            entity.HasIndex(e => e.NoOfHcvMedicineDelivered, "no_of_hcv_medicine_delivered");

            entity.HasIndex(e => e.NoOfMedicineDelivered, "no_of_medicine_delivered");

            entity.HasIndex(e => e.OldRegimeFirstBaselineDate, "old_regime_first_baseline_date");

            entity.HasIndex(e => e.PatientName, "patient_name");

            entity.HasIndex(e => e.PatientType, "patient_type");

            entity.HasIndex(e => e.PcrSampleCollectionPendingHospitalId, "pcr_sample_collection_pending_hospital_id");

            entity.HasIndex(e => e.PrevUserHospital, "prev_user_hospital");

            entity.HasIndex(e => e.SelfCnic, "self_cnic");

            entity.HasIndex(e => e.Tehsil, "tehsil");

            entity.HasIndex(e => e.Updated, "updated");

            entity.HasIndex(e => e.UserHospital, "user_hospital");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.HasIndex(e => e.Vaccinate, "vaccinate");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AFactor).HasColumnName("aFactor");
            entity.Property(e => e.AddressAvailable)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'y')")
                .HasColumnName("address_available");
            entity.Property(e => e.Alt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("alt");
            entity.Property(e => e.Apri)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apri");
            entity.Property(e => e.Ast)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ast");
            entity.Property(e => e.BFactor).HasColumnName("bFactor");
            entity.Property(e => e.BaselineLabName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("baseline_lab_name");
            entity.Property(e => e.BaselineResultType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("baseline_result_type");
            entity.Property(e => e.BatchCompletionDate)
                .HasColumnType("date")
                .HasColumnName("batch_completion_date");
            entity.Property(e => e.BbScreeningMethod)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("bb_screening_method");
            entity.Property(e => e.Bbl)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("BBL");
            entity.Property(e => e.BblUserId).HasColumnName("bbl_user_id");
            entity.Property(e => e.BloodSugarRandom)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("blood_sugar_random");
            entity.Property(e => e.CFactor)
                .HasDefaultValueSql("((0))")
                .HasColumnName("cFactor");
            entity.Property(e => e.CallCenterId1).HasColumnName("call_center_id");
            entity.Property(e => e.CallcenterId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("callcenter_id");
            entity.Property(e => e.Cbl)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("CBL");
            entity.Property(e => e.CloseCase)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("close_case");
            entity.Property(e => e.CnicStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("cnic_status");
            entity.Property(e => e.CollectSample)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("collect_sample");
            entity.Property(e => e.CompletedVacinationHbv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("completed_vacination_hbv");
            entity.Property(e => e.ConsignmentBatch)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("consignment_batch");
            entity.Property(e => e.ConsignmentNo)
                .HasDefaultValueSql("((0))")
                .HasColumnName("consignment_no");
            entity.Property(e => e.ContactNoSelf)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contact_no_self");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Creatinine)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("creatinine");
            entity.Property(e => e.CurrentHospitalName)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("current_hospital_name");
            entity.Property(e => e.DFactor)
                .HasDefaultValueSql("((0))")
                .HasColumnName("dFactor");
            entity.Property(e => e.Diastolic)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("diastolic");
            entity.Property(e => e.District).HasColumnName("district");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("district_name");
            entity.Property(e => e.Division).HasColumnName("division");
            entity.Property(e => e.DivisionName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("division_name");
            entity.Property(e => e.DodDate)
                .HasColumnType("date")
                .HasColumnName("dod_date");
            entity.Property(e => e.DoseEligibility).HasColumnName("dose_eligibility");
            entity.Property(e => e.EFactor)
                .HasDefaultValueSql("((0))")
                .HasColumnName("eFactor");
            entity.Property(e => e.EditPatientStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("edit_patient_status");
            entity.Property(e => e.EligibleForSvr)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("eligible_for_svr");
            entity.Property(e => e.ExHospitalName)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("ex_hospital_name");
            entity.Property(e => e.FFactor)
                .HasDefaultValueSql("((0))")
                .HasColumnName("fFactor");
            entity.Property(e => e.FamilyAssessmentId).HasColumnName("family_assessment_id");
            entity.Property(e => e.FatherName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("father_name");
            entity.Property(e => e.FingerPrint1)
                .IsUnicode(false)
                .HasColumnName("finger_print1");
            entity.Property(e => e.FingerPrint2)
                .IsUnicode(false)
                .HasColumnName("finger_print2");
            entity.Property(e => e.FingerPrintBlob).HasColumnName("finger_print_blob");
            entity.Property(e => e.FlagOfSvrSample)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("flag_of_svr_sample");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.GenderName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("gender_name");
            entity.Property(e => e.HbvFirstMedicineDate).HasColumnName("hbv_first_medicine_date");
            entity.Property(e => e.HbvInvestigationStatus).HasColumnName("hbv_investigation_status");
            entity.Property(e => e.HbvLastFollowup).HasColumnName("hbv_last_followup");
            entity.Property(e => e.HbvMedicineDuration).HasColumnName("hbv_medicine_duration");
            entity.Property(e => e.HbvMedicineName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hbv_medicine_name");
            entity.Property(e => e.HbvScreeningResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("hbv_screening_result");
            entity.Property(e => e.HcvFristMedicineDate).HasColumnName("hcv_frist_medicine_date");
            entity.Property(e => e.HcvFristMedicineDate2).HasColumnName("hcv_frist_medicine_date_2");
            entity.Property(e => e.HcvInvestigationStatus).HasColumnName("hcv_investigation_status");
            entity.Property(e => e.HcvLastFollowup).HasColumnName("hcv_last_followup");
            entity.Property(e => e.HcvLastFollowupDate3Report).HasColumnName("hcv_last_followup_date_3_report");
            entity.Property(e => e.HcvLastFollowupDate6Report).HasColumnName("hcv_last_followup_date_6_report");
            entity.Property(e => e.HcvMedicineDuration).HasColumnName("hcv_medicine_duration");
            entity.Property(e => e.HcvMedicineDuration2).HasColumnName("hcv_medicine_duration_2");
            entity.Property(e => e.HcvMedicineName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hcv_medicine_name");
            entity.Property(e => e.HcvScreeningResult)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("hcv_screening_result");
            entity.Property(e => e.Hemoglobin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("hemoglobin");
            entity.Property(e => e.HfName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hf_name");
            entity.Property(e => e.HfSampleCollectedHospitalId).HasColumnName("hf_sample_collected_hospital_id");
            entity.Property(e => e.Hospital).HasColumnName("hospital");
            entity.Property(e => e.IsAllMedDeliveredFrmBaseline)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_all_med_delivered_frm_baseline");
            entity.Property(e => e.IsAnnualPcr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_annual_pcr");
            entity.Property(e => e.IsAssesment)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_assesment");
            entity.Property(e => e.IsBloodBankPatient)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_blood_bank_patient");
            entity.Property(e => e.IsCirrhoticPatient)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_cirrhotic_patient");
            entity.Property(e => e.IsClosed)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_closed");
            entity.Property(e => e.IsConseledNClosed)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_conseled_n_closed");
            entity.Property(e => e.IsDischarge)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_discharge");
            entity.Property(e => e.IsDoorstep)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_doorstep");
            entity.Property(e => e.IsEditAssessment)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_edit_assessment");
            entity.Property(e => e.IsFollowUpOn)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_follow_up_on");
            entity.Property(e => e.IsGiReceived)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_gi_received");
            entity.Property(e => e.IsGiReferred)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_gi_referred");
            entity.Property(e => e.IsHbvDemote)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_hbv_demote");
            entity.Property(e => e.IsHcvDemote)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_hcv_demote");
            entity.Property(e => e.IsHealthWeekPatient)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_health_week_patient");
            entity.Property(e => e.IsHfc)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_hfc");
            entity.Property(e => e.IsHideTcs)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_hide_tcs");
            entity.Property(e => e.IsIgnore)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_ignore");
            entity.Property(e => e.IsInvalidAddress)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_invalid_address");
            entity.Property(e => e.IsLegacyData)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_legacy_data");
            entity.Property(e => e.IsLegacySvr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_legacy_svr");
            entity.Property(e => e.IsLmpDate)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_lmp_date");
            entity.Property(e => e.IsMedRecUpdate)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_med_rec_update");
            entity.Property(e => e.IsMedicineDelivered)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_medicine_delivered");
            entity.Property(e => e.IsMedicineDisbursFormSubmitted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_medicine_disburs_form_submitted");
            entity.Property(e => e.IsOldRegime)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_old_regime");
            entity.Property(e => e.IsOldRegimeTcs)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_old_regime_tcs");
            entity.Property(e => e.IsPatientPrison)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_patient_prison");
            entity.Property(e => e.IsPatientTransferStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_patient_transfer_status");
            entity.Property(e => e.IsPregnant)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_pregnant");
            entity.Property(e => e.IsPrisonRelease)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_prison_release");
            entity.Property(e => e.IsReRegister)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_re_register");
            entity.Property(e => e.IsReferal)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_referal");
            entity.Property(e => e.IsRefered)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_refered");
            entity.Property(e => e.IsRegCompleted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_reg_completed");
            entity.Property(e => e.IsRegister)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_register");
            entity.Property(e => e.IsSample)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_sample");
            entity.Property(e => e.IsSvrEligibleHospital).HasColumnName("is_svr_eligible_hospital");
            entity.Property(e => e.IsSvrFormSubmitted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_svr_form_submitted");
            entity.Property(e => e.IsSvrRecommended)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_svr_recommended");
            entity.Property(e => e.IsSvrSample)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_svr_sample");
            entity.Property(e => e.IsTerminate)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_terminate");
            entity.Property(e => e.IsTreatment)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_treatment");
            entity.Property(e => e.IsTypeChange)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_type_change");
            entity.Property(e => e.IsVacinate)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_vacinate");
            entity.Property(e => e.IsVital)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_vital");
            entity.Property(e => e.Labno)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("labno");
            entity.Property(e => e.Latitude)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("latitude");
            entity.Property(e => e.Lname)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Longitude)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("longitude");
            entity.Property(e => e.LostFollowupId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("lost_followup_id");
            entity.Property(e => e.MaritalStatus).HasColumnName("marital_status");
            entity.Property(e => e.MedicineDeliveryStatus)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("medicine_delivery_status");
            entity.Property(e => e.MicroId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("micro_id");
            entity.Property(e => e.MrnNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("mrn_no");
            entity.Property(e => e.NextOfKin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("next_of_kin");
            entity.Property(e => e.NextOfKinCnic)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("next_of_kin_cnic");
            entity.Property(e => e.NextOfKinRelation)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("next_of_kin_relation");
            entity.Property(e => e.NextStatus).HasColumnName("next_status");
            entity.Property(e => e.NextStatusUpdated).HasColumnName("next_status_updated");
            entity.Property(e => e.NoCnicReason)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("no_cnic_reason");
            entity.Property(e => e.NoOfDosesTaken).HasColumnName("no_of_doses_taken");
            entity.Property(e => e.NoOfFollowups).HasColumnName("no_of_followups");
            entity.Property(e => e.NoOfHbvFollowups).HasColumnName("no_of_hbv_followups");
            entity.Property(e => e.NoOfHbvMedicineCycles)
                .HasDefaultValueSql("((1))")
                .HasColumnName("no_of_HBV_medicine_cycles");
            entity.Property(e => e.NoOfHbvMedicineDelivered).HasColumnName("no_of_hbv_medicine_delivered");
            entity.Property(e => e.NoOfHcvFollowups)
                .HasDefaultValueSql("((0))")
                .HasColumnName("no_of_hcv_followups");
            entity.Property(e => e.NoOfHcvFollowups2).HasColumnName("no_of_hcv_followups_2");
            entity.Property(e => e.NoOfHcvMedicineDelivered)
                .HasDefaultValueSql("((0))")
                .HasColumnName("no_of_hcv_medicine_delivered");
            entity.Property(e => e.NoOfHcvMedicineDelivered2).HasColumnName("no_of_hcv_medicine_delivered_2");
            entity.Property(e => e.NoOfMedicineDelivered).HasColumnName("no_of_medicine_delivered");
            entity.Property(e => e.NoOfVaccinationDosesGiven)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("no_of_vaccination_doses_given");
            entity.Property(e => e.Occupation).HasColumnName("occupation");
            entity.Property(e => e.OldRegNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("old_reg_no");
            entity.Property(e => e.OldRegimeFirstBaselineDate).HasColumnName("old_regime_first_baseline_date");
            entity.Property(e => e.OldRegimeLastFollowupDate).HasColumnName("old_regime_last_followup_date");
            entity.Property(e => e.OtherContactno)
                .HasMaxLength(20)
                .HasColumnName("other_contactno");
            entity.Property(e => e.OtherLabName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("other_lab_name");
            entity.Property(e => e.Passport)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("passport");
            entity.Property(e => e.PatTransitBit)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("pat_transit_bit");
            entity.Property(e => e.PatientAge).HasColumnName("patient_age");
            entity.Property(e => e.PatientAge80)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("patient_age_80");
            entity.Property(e => e.PatientDob)
                .HasColumnType("date")
                .HasColumnName("patient_dob");
            entity.Property(e => e.PatientFrom)
                .HasMaxLength(18)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'emr')")
                .HasColumnName("patient_from");
            entity.Property(e => e.PatientName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("patient_name");
            entity.Property(e => e.PatientStage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("patient_stage");
            entity.Property(e => e.PatientType)
                .HasMaxLength(21)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'New Patient')")
                .HasColumnName("patient_type");
            entity.Property(e => e.PcrConfirmationHbv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("pcr_confirmation_hbv");
            entity.Property(e => e.PcrConfirmationHcv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("pcr_confirmation_hcv");
            entity.Property(e => e.PcrSampleCollectionPendingHospitalId).HasColumnName("pcr_sample_collection_pending_hospital_id");
            entity.Property(e => e.Pcrreq)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("pcrreq");
            entity.Property(e => e.Platelet)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("platelet");
            entity.Property(e => e.PostalAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("postal_address");
            entity.Property(e => e.PrevUserHospital).HasColumnName("prev_user_hospital");
            entity.Property(e => e.PreviousHbv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("previous_hbv");
            entity.Property(e => e.PreviousHcv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("previous_hcv");
            entity.Property(e => e.PrisonTransferStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("prison_transfer_status");
            entity.Property(e => e.PrisonType).HasColumnName("prison_type");
            entity.Property(e => e.Pulse)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pulse");
            entity.Property(e => e.Qualification).HasColumnName("qualification");
            entity.Property(e => e.RapidTesting)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("rapid_testing");
            entity.Property(e => e.RegNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reg_no");
            entity.Property(e => e.RelationContact)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("relation_contact");
            entity.Property(e => e.SampleAcceptedDate)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("sample_accepted_date");
            entity.Property(e => e.SampleCollectedDate)
                .HasColumnType("date")
                .HasColumnName("sample_collected_date");
            entity.Property(e => e.SampleCollectedNumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sample_collected_number");
            entity.Property(e => e.SampleResult)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sample_result");
            entity.Property(e => e.SampleStage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("sample_stage");
            entity.Property(e => e.SampleTestType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("sample_test_type");
            entity.Property(e => e.ScreeningDate)
                .HasPrecision(0)
                .HasColumnName("screening_date");
            entity.Property(e => e.ScreeningSampleResult)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("screening_sample_result");
            entity.Property(e => e.ScreeningStatus)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("screening_status");
            entity.Property(e => e.SelfCnic)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("self_cnic");
            entity.Property(e => e.SvrAlreadyDone)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("svr_already_done");
            entity.Property(e => e.SvrDate)
                .HasPrecision(0)
                .HasColumnName("svr_date");
            entity.Property(e => e.SvrRecommended)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("svr_recommended");
            entity.Property(e => e.SvrSampleCollectedNumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("svr_sample_collected_number");
            entity.Property(e => e.SvrSampleResult)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("svr_sample_result");
            entity.Property(e => e.Systolic)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("systolic");
            entity.Property(e => e.TcsSelected)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("tcs_selected");
            entity.Property(e => e.Tehsil).HasColumnName("tehsil");
            entity.Property(e => e.TehsilName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tehsil_name");
            entity.Property(e => e.Tlc)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tlc");
            entity.Property(e => e.TotalSampleCollected).HasColumnName("total_sample_collected");
            entity.Property(e => e.TreatmentCompletedHbv)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("treatment_completed_hbv");
            entity.Property(e => e.TreatmentCompletedHcv)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("treatment_completed_hcv");
            entity.Property(e => e.TreatmentHistory)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("treatment_history");
            entity.Property(e => e.TreatmentOptions)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("treatment_options");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.Urea)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("urea");
            entity.Property(e => e.UserHospital).HasColumnName("user_hospital");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Vaccinate)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("vaccinate");
            entity.Property(e => e.Vaccination)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("vaccination");
            entity.Property(e => e.VaccinationDoseDate1).HasColumnName("vaccination_dose_date_1");
            entity.Property(e => e.VaccinationDoseDate2).HasColumnName("vaccination_dose_date_2");
            entity.Property(e => e.VaccinationDoseDate3).HasColumnName("vaccination_dose_date_3");
            entity.Property(e => e.VacinationCompleted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("vacination_completed");
            entity.Property(e => e.ViralCount)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("viral_count");
            entity.Property(e => e.Weight)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("weight");
            entity.Property(e => e.XFactor)
                .HasDefaultValueSql("((0))")
                .HasColumnName("xFactor");
            entity.Property(e => e.YFactor)
                .HasDefaultValueSql("((0))")
                .HasColumnName("yFactor");
            entity.Property(e => e.ZFactor)
                .HasDefaultValueSql("((0))")
                .HasColumnName("zFactor");
        });

        modelBuilder.Entity<TblPatientAssessment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_patient_assessment_id");

            entity.ToTable("tbl_patient_assessment", "phcp_120923");

            entity.HasIndex(e => e.Created, "created");

            entity.HasIndex(e => e.Id, "id");

            entity.HasIndex(e => e.IsHbvTest, "is_hbv_test");

            entity.HasIndex(e => e.IsHcvTest, "is_hcv_test");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.HasIndex(e => e.PcrOption, "pcr_option");

            entity.HasIndex(e => e.RapidTesting, "rapid_testing");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BloodBank)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("blood_bank");
            entity.Property(e => e.BloodTransfusion)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("blood_transfusion");
            entity.Property(e => e.BloodTransfusionWhen)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("blood_transfusion_when");
            entity.Property(e => e.CloseContactIsOnTreatment)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("close_contact_is_on_treatment");
            entity.Property(e => e.CloseContactOfAKnownCaseOfHcvHbv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("close_contact_of_a_known_case_of_hcv_hbv");
            entity.Property(e => e.ConfirmedCaseOfStds)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("confirmed_case_of_stds");
            entity.Property(e => e.ConfirmedHivPositivePersons)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("confirmed_hiv_positive_persons");
            entity.Property(e => e.Counselling)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("counselling");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.DarkColoredUrine)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("dark_colored_urine");
            entity.Property(e => e.DentalClinic)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("dental_clinic");
            entity.Property(e => e.DentalIntervention)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("dental_intervention");
            entity.Property(e => e.DoseEligibility).HasColumnName("dose_eligibility");
            entity.Property(e => e.EarNosePirecing)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("ear_nose_pirecing");
            entity.Property(e => e.EverBeenHospitalized)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("ever_been_hospitalized");
            entity.Property(e => e.Fatigue)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("fatigue");
            entity.Property(e => e.FrequentTherapeuticInjections)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("frequent_therapeutic_injections");
            entity.Property(e => e.GastricIrritationBurning)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("gastric_irritation_burning");
            entity.Property(e => e.HistoryOfMultipleSexPartners)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("history_of_multiple_sex_partners");
            entity.Property(e => e.HospitalizationWithinLast2Years)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("hospitalization_within_last_2_years");
            entity.Property(e => e.IndividualsWithTattooingEarNosePiercing)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("individuals_with_tattooing_ear_nose_piercing");
            entity.Property(e => e.InjectableDrugUser)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("injectable_drug_user");
            entity.Property(e => e.InvasiveMedicalAndSurgicalIntervention)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("invasive_medical_and_surgical_intervention");
            entity.Property(e => e.IsAlreadyVaccinated)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_already_vaccinated");
            entity.Property(e => e.IsHbvTest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_hbv_test");
            entity.Property(e => e.IsHcvTest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_hcv_test");
            entity.Property(e => e.IsLegacySvr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_legacy_svr");
            entity.Property(e => e.IsNewPatient)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_new_patient");
            entity.Property(e => e.IsSampleSvr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_sample_svr");
            entity.Property(e => e.IsTemporaryDelete)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_temporary_delete");
            entity.Property(e => e.Jaundice)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("jaundice");
            entity.Property(e => e.LightColoredFaeces)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("light_colored_faeces");
            entity.Property(e => e.LossOfAppetite)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("loss_of_appetite");
            entity.Property(e => e.MusclePain)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("muscle_pain");
            entity.Property(e => e.Nausea)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("nausea");
            entity.Property(e => e.NoOfDosesTaken).HasColumnName("no_of_doses_taken");
            entity.Property(e => e.Note)
                .IsUnicode(false)
                .HasColumnName("note");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Pcr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("pcr");
            entity.Property(e => e.PcrOption)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("pcr_option");
            entity.Property(e => e.RapidTesting)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("rapid_testing");
            entity.Property(e => e.RightUpperQuadrantTenderness)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("right_upper_quadrant_tenderness");
            entity.Property(e => e.SharingHairComb)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("sharing_hair_comb");
            entity.Property(e => e.SharingToothbrush)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("sharing_toothbrush");
            entity.Property(e => e.StomachAche)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("stomach_ache");
            entity.Property(e => e.SurgeryType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("surgery_type");
            entity.Property(e => e.SurgeryWhen)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("surgery_when");
            entity.Property(e => e.Transgender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("transgender");
            entity.Property(e => e.TruckDriverOrTransgender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("truck_driver_or_transgender");
            entity.Property(e => e.UnexplainedFever)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("unexplained_fever");
            entity.Property(e => e.UnusualUrethralDischarge)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("unusual_urethral_discharge");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.UserHospital).HasColumnName("user_hospital");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VacAdministered)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("vac_administered");
            entity.Property(e => e.VacDoseDate1).HasColumnName("vac_dose_date_1");
            entity.Property(e => e.VacDoseDate2).HasColumnName("vac_dose_date_2");
            entity.Property(e => e.Vaccination)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("vaccination");
            entity.Property(e => e.Vacination)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("vacination");
        });

        modelBuilder.Entity<TblPatientAssessmentLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_patient_assessment_log_id");

            entity.ToTable("tbl_patient_assessment_log", "phcp_120923");

            entity.HasIndex(e => e.Created, "created");

            entity.HasIndex(e => e.Id, "id");

            entity.HasIndex(e => e.IsHbvTest, "is_hbv_test");

            entity.HasIndex(e => e.IsHcvTest, "is_hcv_test");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.HasIndex(e => e.PcrOption, "pcr_option");

            entity.HasIndex(e => e.RapidTesting, "rapid_testing");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BloodTransfusion)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("blood_transfusion");
            entity.Property(e => e.CloseContactOfAKnownCaseOfHcvHbv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("close_contact_of_a_known_case_of_hcv_hbv");
            entity.Property(e => e.ConfirmedCaseOfStds)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("confirmed_case_of_stds");
            entity.Property(e => e.ConfirmedHivPositivePersons)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("confirmed_hiv_positive_persons");
            entity.Property(e => e.Counselling)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("counselling");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.DarkColoredUrine)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("dark_colored_urine");
            entity.Property(e => e.DateTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("dateTime");
            entity.Property(e => e.DentalIntervention)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("dental_intervention");
            entity.Property(e => e.EverBeenHospitalized)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("ever_been_hospitalized");
            entity.Property(e => e.Fatigue)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("fatigue");
            entity.Property(e => e.FrequentTherapeuticInjections)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("frequent_therapeutic_injections");
            entity.Property(e => e.GastricIrritationBurning)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("gastric_irritation_burning");
            entity.Property(e => e.HistoryOfMultipleSexPartners)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("history_of_multiple_sex_partners");
            entity.Property(e => e.IndividualsWithTattooingEarNosePiercing)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("individuals_with_tattooing_ear_nose_piercing");
            entity.Property(e => e.InjectableDrugUser)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("injectable_drug_user");
            entity.Property(e => e.InvasiveMedicalAndSurgicalIntervention)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("invasive_medical_and_surgical_intervention");
            entity.Property(e => e.IsHbvTest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_hbv_test");
            entity.Property(e => e.IsHcvTest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_hcv_test");
            entity.Property(e => e.IsLegacySvr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_legacy_svr");
            entity.Property(e => e.IsNewPatient)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_new_patient");
            entity.Property(e => e.IsSampleSvr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_sample_svr");
            entity.Property(e => e.Jaundice)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("jaundice");
            entity.Property(e => e.LightColoredFaeces)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("light_colored_faeces");
            entity.Property(e => e.LossOfAppetite)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("loss_of_appetite");
            entity.Property(e => e.MusclePain)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("muscle_pain");
            entity.Property(e => e.Nausea)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("nausea");
            entity.Property(e => e.Note)
                .IsUnicode(false)
                .HasColumnName("note");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Pcr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("pcr");
            entity.Property(e => e.PcrOption)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("pcr_option");
            entity.Property(e => e.RapidTesting)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("rapid_testing");
            entity.Property(e => e.RightUpperQuadrantTenderness)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("right_upper_quadrant_tenderness");
            entity.Property(e => e.StomachAche)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("stomach_ache");
            entity.Property(e => e.TruckDriverOrTransgender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("truck_driver_or_transgender");
            entity.Property(e => e.UnexplainedFever)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("unexplained_fever");
            entity.Property(e => e.UnusualUrethralDischarge)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("unusual_urethral_discharge");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.UserHospital).HasColumnName("user_hospital");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Vaccination)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("vaccination");
            entity.Property(e => e.Vacination)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("vacination");
        });

        modelBuilder.Entity<TblPatientConsignmentNumber>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_patient_consignment_number_id");

            entity.ToTable("tbl_patient_consignment_number", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ConsignmentNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("consignment_no");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
        });

        modelBuilder.Entity<TblPatientDataCsv>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_patient_data_csv", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.AddressAvailable)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address_available");
            entity.Property(e => e.AssessmentCreateTime)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("assessment_create_time");
            entity.Property(e => e.BaselineDate)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("baseline_date");
            entity.Property(e => e.CallCenterId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("call_center_id");
            entity.Property(e => e.ChangeType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("change_type");
            entity.Property(e => e.ContactNoSelf)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("contact_no_self");
            entity.Property(e => e.CreateTime)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("create_time");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("district_name");
            entity.Property(e => e.DivisionName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("division_name");
            entity.Property(e => e.FatherName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("father_name");
            entity.Property(e => e.Gender)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("gender");
            entity.Property(e => e.HbMedDuration)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hb_med_duration");
            entity.Property(e => e.HbMedName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hb_med_name");
            entity.Property(e => e.HbvFirstMedicineDate)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hbv_first_medicine_date");
            entity.Property(e => e.HbvMedicineDuration)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hbv_medicine_duration");
            entity.Property(e => e.HbvPcr)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hbv_pcr");
            entity.Property(e => e.HcvFristMedicineDate)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hcv_frist_medicine_date");
            entity.Property(e => e.HcvMedicineDuration)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hcv_medicine_duration");
            entity.Property(e => e.HcvPcr)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hcv_pcr");
            entity.Property(e => e.HfName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hf_name");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.IsAssesment)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("is_assesment");
            entity.Property(e => e.IsClosed)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("is_closed");
            entity.Property(e => e.IsConseledNClosed)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("is_conseled_n_closed");
            entity.Property(e => e.IsHbvTest)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("is_hbv_test");
            entity.Property(e => e.IsHcvTest)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("is_hcv_test");
            entity.Property(e => e.IsHealthWeekPatient)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("is_health_week_patient");
            entity.Property(e => e.IsRefered)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("is_refered");
            entity.Property(e => e.IsRegister)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("is_register");
            entity.Property(e => e.IsSample)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("is_sample");
            entity.Property(e => e.IsScreening)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("is_screening");
            entity.Property(e => e.IsTreatment)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("is_treatment");
            entity.Property(e => e.IsVacinate)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("is_vacinate");
            entity.Property(e => e.IsVital)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("is_vital");
            entity.Property(e => e.Labno)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("labno");
            entity.Property(e => e.Lname)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MedType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("med_type");
            entity.Property(e => e.MedicineDeliveryDate1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("medicine_delivery_date_1");
            entity.Property(e => e.MedicineDeliveryDate2)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("medicine_delivery_date_2");
            entity.Property(e => e.MedicineDeliveryDate3)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("medicine_delivery_date_3");
            entity.Property(e => e.MrnNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("mrn_no");
            entity.Property(e => e.NextOfKin)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("next_of_kin");
            entity.Property(e => e.NextOfKinCnic)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("next_of_kin_cnic");
            entity.Property(e => e.NoOfHbvMedicineDelivered)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("no_of_hbv_medicine_delivered");
            entity.Property(e => e.NoOfHcvMedicineDelivered)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("no_of_hcv_medicine_delivered");
            entity.Property(e => e.NoOfMedicineDelivered)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("no_of_medicine_delivered");
            entity.Property(e => e.NumberOfDoseAdministered)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("number_of_dose_administered");
            entity.Property(e => e.OldRegNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("old_reg_no");
            entity.Property(e => e.OtherContactno)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("other_contactno");
            entity.Property(e => e.PatientAge)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("patient_age");
            entity.Property(e => e.PatientCategory)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("patient_category");
            entity.Property(e => e.PatientDob)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("patient_dob");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.PatientName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("patient_name");
            entity.Property(e => e.PatientType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("patient_type");
            entity.Property(e => e.Pcr)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PCR");
            entity.Property(e => e.PcrOption)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("pcr_option");
            entity.Property(e => e.Pcrreq)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("pcrreq");
            entity.Property(e => e.PostalAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("postal_address");
            entity.Property(e => e.RegNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reg_no");
            entity.Property(e => e.SampleNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("sample_number");
            entity.Property(e => e.ScreeningRecommended)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("screening_recommended");
            entity.Property(e => e.SelfCnic)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("self_cnic");
            entity.Property(e => e.Stage)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SvrSampleType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("svr_sample_type");
            entity.Property(e => e.TehsilName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("tehsil_name");
            entity.Property(e => e.VaccinationDoseDate1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("vaccination_dose_date_1");
            entity.Property(e => e.VaccinationDoseDate2)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("vaccination_dose_date_2");
            entity.Property(e => e.VaccinationDoseDate3)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("vaccination_dose_date_3");
            entity.Property(e => e.VitalsCreateTime)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("vitals_create_time");
        });

        modelBuilder.Entity<TblPatientHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_patient_history_id");

            entity.ToTable("tbl_patient_history", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.TransferId).HasColumnName("transfer_id");
            entity.Property(e => e.TransferInFacility).HasColumnName("transfer_in_facility");
            entity.Property(e => e.TransferOutFacility).HasColumnName("transfer_out_facility");
        });

        modelBuilder.Entity<TblPatientMedicineInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_patient_medicine_info_id");

            entity.ToTable("tbl_patient_medicine_info", "phcp_120923");

            entity.HasIndex(e => e.NewPrescriptionNo, "new_prescription_no");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.HasIndex(e => e.PrescriptionNo, "prescription_no");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AvailableDate).HasColumnName("available_date");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.DeliveryDate).HasColumnName("delivery_date");
            entity.Property(e => e.IsDemote)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_demote");
            entity.Property(e => e.NewPrescriptionNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("new_prescription_no");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.PrescriptionNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("prescription_no");
        });

        modelBuilder.Entity<TblPatientRefer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_patient_refer_id");

            entity.ToTable("tbl_patient_refer", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActionDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("action_date");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.PatientStage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("patient_stage");
            entity.Property(e => e.PatientType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("patient_type");
            entity.Property(e => e.ReferInDistrict).HasColumnName("refer_in_district");
            entity.Property(e => e.ReferInFacility).HasColumnName("refer_in_facility");
            entity.Property(e => e.ReferInProvince).HasColumnName("refer_in_province");
            entity.Property(e => e.ReferInTehsil).HasColumnName("refer_in_tehsil");
            entity.Property(e => e.ReferOutFacility).HasColumnName("refer_out_facility");
            entity.Property(e => e.ReferReason)
                .IsUnicode(false)
                .HasColumnName("refer_reason");
            entity.Property(e => e.ReferRequestDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("refer_request_date");
            entity.Property(e => e.ReferStatus)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'pending')")
                .HasColumnName("refer_status");
        });

        modelBuilder.Entity<TblPatientReferReceive>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_patient_refer_receive_id");

            entity.ToTable("tbl_patient_refer_receive", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.ContactNoSelf)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("contact_no_self");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.DivisionId).HasColumnName("division_id");
            entity.Property(e => e.Dob)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("dob");
            entity.Property(e => e.FatherName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("father_name");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.MrnNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("mrn_no");
            entity.Property(e => e.ReceiveDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("receive_date");
            entity.Property(e => e.ReferReason)
                .IsUnicode(false)
                .HasColumnName("refer_reason");
            entity.Property(e => e.ReferStatus)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'pending')")
                .HasColumnName("refer_status");
            entity.Property(e => e.ReqDept)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("req_dept");
            entity.Property(e => e.ReqHospital)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("req_hospital");
            entity.Property(e => e.SelfCnic)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("self_cnic");
            entity.Property(e => e.TehsilId).HasColumnName("tehsil_id");
        });

        modelBuilder.Entity<TblPatientReferReceiveHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_patient_refer_receive_history_id");

            entity.ToTable("tbl_patient_refer_receive_history", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cnic)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("cnic");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Diagnosis)
                .IsUnicode(false)
                .HasColumnName("diagnosis");
            entity.Property(e => e.InvestigationMo)
                .IsUnicode(false)
                .HasColumnName("investigation_mo");
            entity.Property(e => e.InvestigationNurse)
                .IsUnicode(false)
                .HasColumnName("investigation_nurse");
            entity.Property(e => e.Medicine)
                .IsUnicode(false)
                .HasColumnName("medicine");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'Y')")
                .HasColumnName("status");
            entity.Property(e => e.Symptoms)
                .IsUnicode(false)
                .HasColumnName("symptoms");
        });

        modelBuilder.Entity<TblPatientReferReceiveTreatment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_patient_refer_receive_treatment_id");

            entity.ToTable("tbl_patient_refer_receive_treatment", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Diagnosis)
                .IsUnicode(false)
                .HasColumnName("diagnosis");
            entity.Property(e => e.InvestigationMo)
                .IsUnicode(false)
                .HasColumnName("investigation_mo");
            entity.Property(e => e.InvestigationNurse)
                .IsUnicode(false)
                .HasColumnName("investigation_nurse");
            entity.Property(e => e.Medicine)
                .IsUnicode(false)
                .HasColumnName("medicine");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.ReceiveId).HasColumnName("receive_id");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'Y')")
                .HasColumnName("status");
            entity.Property(e => e.Symptoms)
                .IsUnicode(false)
                .HasColumnName("symptoms");
        });

        modelBuilder.Entity<TblPatientStage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_patient_stage_id");

            entity.ToTable("tbl_patient_stage", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.StageStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'Y')")
                .HasColumnName("stage_status");
        });

        modelBuilder.Entity<TblPatientStageLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_patient_stage_log_id");

            entity.ToTable("tbl_patient_stage_log", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.StageId).HasColumnName("stage_id");
        });

        modelBuilder.Entity<TblPatientTransfer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_patient_transfer_id");

            entity.ToTable("tbl_patient_transfer", "phcp_120923");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.HasIndex(e => e.TransferInFacility, "transfer_in");

            entity.HasIndex(e => e.TransferOutFacility, "transfer_out");

            entity.HasIndex(e => e.TransferStatus, "transfer_status");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActionDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("action_date");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.HbvFollowups).HasColumnName("hbv_followups");
            entity.Property(e => e.HbvMedDelivered).HasColumnName("hbv_med_delivered");
            entity.Property(e => e.HbvMedicineName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("hbv_medicine_name");
            entity.Property(e => e.HcvFollowups).HasColumnName("hcv_followups");
            entity.Property(e => e.HcvMedDelivered).HasColumnName("hcv_med_delivered");
            entity.Property(e => e.HcvMedicineName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("hcv_medicine_name");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.PatientStage)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("patient_stage");
            entity.Property(e => e.PatientType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("patient_type");
            entity.Property(e => e.ScreeningHbv)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("screening_hbv");
            entity.Property(e => e.ScreeningHcv)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("screening_hcv");
            entity.Property(e => e.TransferInFacility).HasColumnName("transfer_in_facility");
            entity.Property(e => e.TransferInFacilityName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("transfer_in_facility_name");
            entity.Property(e => e.TransferOutFacility).HasColumnName("transfer_out_facility");
            entity.Property(e => e.TransferReason)
                .IsUnicode(false)
                .HasColumnName("transfer_reason");
            entity.Property(e => e.TransferRequestDate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("transfer_request_date");
            entity.Property(e => e.TransferStatus)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'pending')")
                .HasColumnName("transfer_status");
            entity.Property(e => e.Vaccination)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.VaccinationDosesCount).HasColumnName("Vaccination_doses_count");
        });

        modelBuilder.Entity<TblPatientVital>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_patient_vital_id");

            entity.ToTable("tbl_patient_vital", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.HasIndex(e => e.Pid, "pid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BpDiastolic).HasColumnName("bp_diastolic");
            entity.Property(e => e.BpSystolic).HasColumnName("bp_systolic");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.Pulse).HasColumnName("pulse");
            entity.Property(e => e.Temperature).HasColumnName("temperature");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Weight).HasColumnName("weight");
        });

        modelBuilder.Entity<TblPatientvaccination>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_patientvaccination_id");

            entity.ToTable("tbl_patientvaccination", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.HasIndex(e => e.Pid, "pid");

            entity.HasIndex(e => e.Stage, "stage");

            entity.HasIndex(e => e.UserHospital, "user_hospital");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.DoseDate).HasColumnName("dose_date");
            entity.Property(e => e.EntryType)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("entry_type");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.Stage).HasColumnName("stage");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UserHospital).HasColumnName("user_hospital");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<TblPrivateLabResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_private_lab_result_id");

            entity.ToTable("tbl_private_lab_result", "phcp_120923");

            entity.HasIndex(e => e.Created, "created");

            entity.HasIndex(e => e.Id, "id");

            entity.HasIndex(e => e.IsHbvDetected, "is_hbv_detected");

            entity.HasIndex(e => e.IsHcvDetected, "is_hcv_detected");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Hbv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("hbv");
            entity.Property(e => e.Hcv)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("hcv");
            entity.Property(e => e.IsHbvDetected)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_hbv_detected");
            entity.Property(e => e.IsHcvDetected)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_hcv_detected");
            entity.Property(e => e.LabName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("lab_name");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.ResultType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("result_type");
            entity.Property(e => e.TestRequired)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("test_required");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<TblQualification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_qualification_id");

            entity.ToTable("tbl_qualification", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'y')")
                .HasColumnName("status");
        });

        modelBuilder.Entity<TblReferDistrict>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_refer_district_id");

            entity.ToTable("tbl_refer_district", "phcp_120923");

            entity.HasIndex(e => e.Created, "created");

            entity.HasIndex(e => e.DistrictCode, "district_code");

            entity.HasIndex(e => e.ProvinceId, "province_id");

            entity.HasIndex(e => e.Status, "status");

            entity.HasIndex(e => e.Updated, "updated");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.DistrictCode).HasColumnName("district_code");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("district_name");
            entity.Property(e => e.ProvinceId)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("province_id");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'Y')")
                .HasColumnName("status");
            entity.Property(e => e.Updated).HasColumnName("updated");
        });

        modelBuilder.Entity<TblReferHospital>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_refer_hospital_id");

            entity.ToTable("tbl_refer_hospital", "phcp_120923");

            entity.HasIndex(e => e.Created, "created");

            entity.HasIndex(e => e.Status, "status");

            entity.HasIndex(e => e.Updated, "updated");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .HasColumnName("city");
            entity.Property(e => e.ClinicAddress)
                .HasMaxLength(255)
                .HasColumnName("clinic_address");
            entity.Property(e => e.ClinicCode)
                .HasMaxLength(100)
                .HasColumnName("clinic_code");
            entity.Property(e => e.ClinicCodeNo).HasColumnName("clinic_code_no");
            entity.Property(e => e.ClinicLat)
                .HasMaxLength(30)
                .HasColumnName("clinic_lat");
            entity.Property(e => e.ClinicLon)
                .HasMaxLength(30)
                .HasColumnName("clinic_lon");
            entity.Property(e => e.ClinicName)
                .HasMaxLength(255)
                .HasColumnName("clinic_name");
            entity.Property(e => e.ClinicType)
                .HasMaxLength(255)
                .HasColumnName("clinic_type");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.FacilityType)
                .HasMaxLength(255)
                .HasColumnName("facility_type");
            entity.Property(e => e.IsPacslink)
                .HasMaxLength(1)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_pacslink");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.Status)
                .HasMaxLength(9)
                .HasDefaultValueSql("(N'active')")
                .HasColumnName("status");
            entity.Property(e => e.TehsilId).HasColumnName("tehsil_id");
            entity.Property(e => e.Updated).HasColumnName("updated");
        });

        modelBuilder.Entity<TblReferProvince>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_refer_province_id");

            entity.ToTable("tbl_refer_province", "phcp_120923");

            entity.HasIndex(e => e.Created, "created");

            entity.HasIndex(e => e.Status, "status");

            entity.HasIndex(e => e.Updated, "updated");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'Y')")
                .HasColumnName("status");
            entity.Property(e => e.Updated).HasColumnName("updated");
        });

        modelBuilder.Entity<TblReferTehsil>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_refer_tehsil_id");

            entity.ToTable("tbl_refer_tehsil", "phcp_120923");

            entity.HasIndex(e => e.Created, "created");

            entity.HasIndex(e => e.DistrictId, "district_id");

            entity.HasIndex(e => e.Status, "status");

            entity.HasIndex(e => e.Updated, "updated");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'Y')")
                .HasColumnName("status");
            entity.Property(e => e.TeshilCode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("teshil_code");
            entity.Property(e => e.Updated).HasColumnName("updated");
        });

        modelBuilder.Entity<TblRenalFunction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_renal_function_id");

            entity.ToTable("tbl_renal_function", "phcp_120923");

            entity.HasIndex(e => e.BaselineType, "baseline_type");

            entity.HasIndex(e => e.Pid, "pid");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BaselineType)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'HBV')")
                .HasColumnName("baseline_type");
            entity.Property(e => e.BloodSugarRandom).HasColumnName("blood_sugar_random");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.Creatinie)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("creatinie");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.SampleId).HasColumnName("sample_id");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.Urea).HasColumnName("urea");
        });

        modelBuilder.Entity<TblRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_roles_id");

            entity.ToTable("tbl_roles", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Roles)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("roles");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("status");
            entity.Property(e => e.Updated).HasColumnName("updated");
        });

        modelBuilder.Entity<TblSample>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_sample_id");

            entity.ToTable("tbl_sample", "phcp_120923");

            entity.HasIndex(e => e.ActionId, "action_id");

            entity.HasIndex(e => e.IsDispatch, "is_dispatch");

            entity.HasIndex(e => e.IsLabBatch, "is_lab_batch");

            entity.HasIndex(e => e.IsReception, "is_reception");

            entity.HasIndex(e => e.LabReceptionistId, "lab_receptionist_id");

            entity.HasIndex(e => e.Number, "number");

            entity.HasIndex(e => e.Pid, "pid");

            entity.HasIndex(e => e.UserHospital, "user_hospital");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActionId).HasColumnName("action_id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.IsDispatch)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_dispatch");
            entity.Property(e => e.IsLabBatch)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_lab_batch");
            entity.Property(e => e.IsReception)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_reception");
            entity.Property(e => e.IsSampleSvr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_sample_svr");
            entity.Property(e => e.LabReceptionistId).HasColumnName("lab_receptionist_id");
            entity.Property(e => e.Number)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("number");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.SampleTakenWith)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("sample_taken_with");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UserHospital).HasColumnName("user_hospital");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<TblScreening>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_screening_id");

            entity.ToTable("tbl_screening", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressAvailable)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'y')")
                .HasColumnName("address_available");
            entity.Property(e => e.CallcenterId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("callcenter_id");
            entity.Property(e => e.CnicStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("cnic_status");
            entity.Property(e => e.CompletedVacinationHbv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("completed_vacination_hbv");
            entity.Property(e => e.ContactNoSelf)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("contact_no_self");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.District).HasColumnName("district");
            entity.Property(e => e.Division).HasColumnName("division");
            entity.Property(e => e.FatherName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("father_name");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.Hospital).HasColumnName("hospital");
            entity.Property(e => e.IsReferal)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("is_referal");
            entity.Property(e => e.MaritalStatus).HasColumnName("marital_status");
            entity.Property(e => e.MrnNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("mrn_no");
            entity.Property(e => e.NextOfKin)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("next_of_kin");
            entity.Property(e => e.NextOfKinCnic)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("next_of_kin_cnic");
            entity.Property(e => e.NextStatus).HasColumnName("next_status");
            entity.Property(e => e.NextStatusUpdated).HasColumnName("next_status_updated");
            entity.Property(e => e.NoCnicReason)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("no_cnic_reason");
            entity.Property(e => e.Occupation).HasColumnName("occupation");
            entity.Property(e => e.OldRegNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("old_reg_no");
            entity.Property(e => e.OtherContactno)
                .HasMaxLength(20)
                .HasColumnName("other_contactno");
            entity.Property(e => e.PatientAge).HasColumnName("patient_age");
            entity.Property(e => e.PatientDob)
                .HasColumnType("date")
                .HasColumnName("patient_dob");
            entity.Property(e => e.PatientName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("patient_name");
            entity.Property(e => e.PatientType)
                .HasMaxLength(21)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'New Patient')")
                .HasColumnName("patient_type");
            entity.Property(e => e.PcrConfirmationHbv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("pcr_confirmation_hbv");
            entity.Property(e => e.PcrConfirmationHcv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("pcr_confirmation_hcv");
            entity.Property(e => e.PostalAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("postal_address");
            entity.Property(e => e.PreviousHbv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("previous_hbv");
            entity.Property(e => e.PreviousHcv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("previous_hcv");
            entity.Property(e => e.Qualification).HasColumnName("qualification");
            entity.Property(e => e.RegNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("reg_no");
            entity.Property(e => e.RelationContact)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("relation_contact");
            entity.Property(e => e.SelfCnic)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("self_cnic");
            entity.Property(e => e.Tehsil).HasColumnName("tehsil");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.VacinationCompleted)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("vacination_completed");
        });

        modelBuilder.Entity<TblScreeningMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_screening_methods_id");

            entity.ToTable("tbl_screening_methods", "phcp_120923");

            entity.HasIndex(e => e.Id, "tbl_screening_methods$id_UNIQUE").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TblSmsLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_sms_log_Id");

            entity.ToTable("tbl_sms_log", "phcp_120923");

            entity.Property(e => e.CreatedDate)
                .HasPrecision(0)
                .HasColumnName("created_date");
            entity.Property(e => e.Message)
                .HasMaxLength(250)
                .HasColumnName("message");
            entity.Property(e => e.MonbileNo)
                .HasMaxLength(150)
                .HasColumnName("monbile_no");
            entity.Property(e => e.MrnNo)
                .HasMaxLength(100)
                .HasColumnName("mrn_no");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Response)
                .HasMaxLength(150)
                .HasColumnName("response");
            entity.Property(e => e.SmsType)
                .HasMaxLength(50)
                .HasColumnName("sms_type");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasColumnName("status");
        });

        modelBuilder.Entity<TblStock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_stock_id");

            entity.ToTable("tbl_stock", "phcp_120923");

            entity.HasIndex(e => e.HospitalId, "hospital_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.Remaining).HasColumnName("remaining");
        });

        modelBuilder.Entity<TblStockLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_stock_log_id");

            entity.ToTable("tbl_stock_log", "phcp_120923");

            entity.HasIndex(e => e.HospitalId, "hospital_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Updated).HasColumnName("updated");
        });

        modelBuilder.Entity<TblSvrFormDatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_svr_form_data_id");

            entity.ToTable("tbl_svr_form_data", "phcp_120923");

            entity.HasIndex(e => e.Created, "created");

            entity.HasIndex(e => e.IsPatAchieveSvr, "is_pat_achieve_svr");

            entity.HasIndex(e => e.Pid, "pid");

            entity.HasIndex(e => e.SampleRecommended, "sample_recommended");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.EtrPcrLabname)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("etr_pcr_labname");
            entity.Property(e => e.EtrPcrLabnameOther)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("etr_pcr_labname_other");
            entity.Property(e => e.GapBtwTreatment)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("gap_btw_treatment");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.IsEtrPcr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_etr_pcr");
            entity.Property(e => e.IsNewRegimePat)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_new_regime_pat");
            entity.Property(e => e.IsPatAchieveEtr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_pat_achieve_etr");
            entity.Property(e => e.IsPatAchieveSvr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_pat_achieve_svr");
            entity.Property(e => e.IsSvrPcr)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_svr_pcr");
            entity.Property(e => e.NoOfMonthPatTreatmentCompleted).HasColumnName("no_of_month_pat_treatment_completed");
            entity.Property(e => e.NoOfMonthlyPackPatientReceived).HasColumnName("no_of_monthly_pack_patient_received");
            entity.Property(e => e.NoOfMonthlyPackPatientUsed).HasColumnName("no_of_monthly_pack_patient_used");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.SampleRecommended)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("sample_recommended");
            entity.Property(e => e.SampleTag)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("sample_tag");
            entity.Property(e => e.SvrPcrLabname)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("svr_pcr_labname");
            entity.Property(e => e.SvrPcrLabnameOther)
                .HasMaxLength(300)
                .IsUnicode(false)
                .HasColumnName("svr_pcr_labname_other");
        });

        modelBuilder.Entity<TblTcsBatch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_tcs_batch_id");

            entity.ToTable("tbl_tcs_batch", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BatchNumber)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("batch_number");
            entity.Property(e => e.BatchType)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("batch_type");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.HfcAddress)
                .IsUnicode(false)
                .HasColumnName("hfc_address");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<TblTcsBatchPatient>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_tcs_batch_patient", "phcp_120923");

            entity.Property(e => e.BatchId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("batch_id");
            entity.Property(e => e.ConsignmentNo).HasColumnName("consignment_no");
            entity.Property(e => e.NoOfDosage).HasColumnName("no_of_dosage");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
        });

        modelBuilder.Entity<TblTcsConsignmentNumber>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_tcs_consignment_numbers_id");

            entity.ToTable("tbl_tcs_consignment_numbers", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.IsActive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'Y')")
                .HasColumnName("is_active");
            entity.Property(e => e.MaxRange).HasColumnName("max_range");
            entity.Property(e => e.MinRange).HasColumnName("min_range");
        });

        modelBuilder.Entity<TblTehsil>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_tehsil_id");

            entity.ToTable("tbl_tehsil", "phcp_120923");

            entity.HasIndex(e => e.DistrictCode, "district_code");

            entity.HasIndex(e => e.DivisionCode, "division_code");

            entity.HasIndex(e => e.TehsilCode, "tehsil_code");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DestinationCode)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("destination_code");
            entity.Property(e => e.DistrictCode).HasColumnName("district_code");
            entity.Property(e => e.DivisionCode).HasColumnName("division_code");
            entity.Property(e => e.IsActive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'Y')")
                .HasColumnName("is_active");
            entity.Property(e => e.TehsilCode).HasColumnName("tehsil_code");
            entity.Property(e => e.TehsilName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tehsil_name");
        });

        modelBuilder.Entity<TblTemp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_temp_id");

            entity.ToTable("tbl_temp", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FollowupDate).HasColumnName("followup_date");
            entity.Property(e => e.MedDisburseDate).HasColumnName("med_disburse_date");
            entity.Property(e => e.MedicineDuration).HasColumnName("medicine_duration");
            entity.Property(e => e.MedicineId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("medicine_id");
            entity.Property(e => e.Mrn)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("mrn");
            entity.Property(e => e.Pid).HasColumnName("pid");
        });

        modelBuilder.Entity<TblTempString>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_temp_string_id");

            entity.ToTable("tbl_temp_string", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Pid).HasColumnName("pid");
        });

        modelBuilder.Entity<TblTemporaryResultUpload>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_temporary_result_upload_id");

            entity.ToTable("tbl_temporary_result_upload", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BatchId).HasColumnName("batch_id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.HbvViralLoad)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hbv_viral_load");
            entity.Property(e => e.HcvViralLoad)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hcv_viral_load");
            entity.Property(e => e.IsBatchCountMatched)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_batch_count_matched");
            entity.Property(e => e.IsFound)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_found");
            entity.Property(e => e.IsSvrSample)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("is_svr_sample");
            entity.Property(e => e.Pid).HasColumnName("pid");
            entity.Property(e => e.ResultHbv)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("result_hbv");
            entity.Property(e => e.ResultHcv)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("result_hcv");
            entity.Property(e => e.SampleId).HasColumnName("sample_id");
            entity.Property(e => e.SampleNo)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("sample_no");
            entity.Property(e => e.TestRequired)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("test_required");
        });

        modelBuilder.Entity<TblTest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_test_id");

            entity.ToTable("tbl_test", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Msg)
                .HasMaxLength(250)
                .HasColumnName("msg");
            entity.Property(e => e.Title)
                .HasMaxLength(45)
                .HasColumnName("title");
        });

        modelBuilder.Entity<TblTicket>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_tickets", "phcp_120923");

            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.MdId).HasColumnName("md_id");
            entity.Property(e => e.Message)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.Sender).HasColumnName("sender");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.TicketId)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ticket_id");
        });

        modelBuilder.Entity<TblTimeLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_time_log", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.EndTime)
                .HasPrecision(0)
                .HasColumnName("end_time");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.NameOperation)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name_operation");
            entity.Property(e => e.StartTime)
                .HasPrecision(0)
                .HasColumnName("start_time");
        });

        modelBuilder.Entity<TblTreatment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_treatment_id");

            entity.ToTable("tbl_treatment", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Counselling)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("counselling");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.Deferred)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("deferred");
            entity.Property(e => e.Discharge)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("discharge");
            entity.Property(e => e.HbvMedicine)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hbv_medicine");
            entity.Property(e => e.HcvMedicine)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hcv_medicine");
            entity.Property(e => e.IsHbvMedicine)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_hbv_medicine");
            entity.Property(e => e.IsHbvTest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_hbv_test");
            entity.Property(e => e.IsHcvMedicine)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_hcv_medicine");
            entity.Property(e => e.IsHcvTest)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_hcv_test");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Pcr)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("pcr");
            entity.Property(e => e.PrescribeMedicine)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("prescribe_medicine");
            entity.Property(e => e.RapidTesting)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("rapid_testing");
            entity.Property(e => e.ReferralPkli)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("referral_pkli");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Vaccination)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("vaccination");
            entity.Property(e => e.VaccinationOption)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'Skip')")
                .HasColumnName("vaccination_option");
        });

        modelBuilder.Entity<TblUc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_uc_id");

            entity.ToTable("tbl_uc", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValueSql("(N'Y')")
                .HasColumnName("is_active");
            entity.Property(e => e.TehsilCode).HasColumnName("tehsil_code");
            entity.Property(e => e.TehsilId).HasColumnName("tehsil_id");
            entity.Property(e => e.UcName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("uc_name");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tbl_user_id");

            entity.ToTable("tbl_user", "phcp_120923");

            entity.HasIndex(e => e.DistrictId, "district_id");

            entity.HasIndex(e => e.DivisionId, "division_id");

            entity.HasIndex(e => e.HospitalId, "hospital_id");

            entity.HasIndex(e => e.Id, "id");

            entity.HasIndex(e => e.Identifier, "identifier");

            entity.HasIndex(e => e.Password, "password");

            entity.HasIndex(e => e.RoleId, "role_id");

            entity.HasIndex(e => e.Status, "status");

            entity.HasIndex(e => e.TehsilId, "tehsil_id");

            entity.HasIndex(e => e.Username, "username");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created).HasColumnName("created");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.DivisionId).HasColumnName("division_id");
            entity.Property(e => e.EndRange).HasColumnName("end_range");
            entity.Property(e => e.FullName)
                .HasMaxLength(255)
                .HasColumnName("full_name");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.HospitalName)
                .HasMaxLength(255)
                .HasColumnName("hospital_name");
            entity.Property(e => e.Identifier)
                .HasMaxLength(255)
                .HasColumnName("identifier");
            entity.Property(e => e.Imei)
                .HasMaxLength(255)
                .HasColumnName("imei");
            entity.Property(e => e.IsEventUser)
                .HasMaxLength(1)
                .HasColumnName("is_event_user");
            entity.Property(e => e.IsJailUser)
                .HasMaxLength(1)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_jail_user");
            entity.Property(e => e.IsLoggedIn)
                .HasMaxLength(1)
                .HasDefaultValueSql("(N'N')")
                .HasColumnName("is_logged_in");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.StartRange).HasColumnName("start_range");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .HasDefaultValueSql("(N'Y')")
                .HasColumnName("status");
            entity.Property(e => e.TehsilId).HasColumnName("tehsil_id");
            entity.Property(e => e.Updated).HasColumnName("updated");
            entity.Property(e => e.Usercnic)
                .HasMaxLength(20)
                .HasColumnName("usercnic");
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_teams_id");

            entity.ToTable("teams", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.MonitringZoneId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("monitringZone_id");
            entity.Property(e => e.OrderBy).HasColumnName("order_by");
            entity.Property(e => e.TeamLeaderId).HasColumnName("team_leader_id");
            entity.Property(e => e.TeamTitle)
                .HasMaxLength(254)
                .HasColumnName("team_title");
        });

        modelBuilder.Entity<TeamsMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_teams_members_id");

            entity.ToTable("teams_members", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.MonitorId).HasColumnName("monitor_id");
            entity.Property(e => e.OrderBy).HasColumnName("order_by");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
        });

        modelBuilder.Entity<Testcnic>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("testcnic", "phcp_120923");

            entity.Property(e => e.Cnic).HasColumnName("CNIC");
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tours_id");

            entity.ToTable("tours", "phcp_120923");

            entity.HasIndex(e => e.Id, "id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ApprovalStatus).HasColumnName("approval_status");
            entity.Property(e => e.ApprovedBy).HasColumnName("approved_by");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.DivisionId).HasColumnName("division_id");
            entity.Property(e => e.DriverId).HasColumnName("driver_id");
            entity.Property(e => e.FromDate)
                .HasColumnType("date")
                .HasColumnName("from_date");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.MonitringZoneId).HasColumnName("monitringZone_id");
            entity.Property(e => e.OrderBy)
                .HasDefaultValueSql("((0))")
                .HasColumnName("order_by");
            entity.Property(e => e.ToDate)
                .HasColumnType("date")
                .HasColumnName("to_date");
            entity.Property(e => e.TourStatus).HasColumnName("tour_status");
            entity.Property(e => e.TourTitle)
                .HasMaxLength(254)
                .HasColumnName("tour_title");
            entity.Property(e => e.VehicleId).HasColumnName("vehicle_id");
        });

        modelBuilder.Entity<TourComplianceStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tour_compliance_status_id");

            entity.ToTable("tour_compliance_status", "phcp_120923");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.OrderBy).HasColumnName("order_by");
            entity.Property(e => e.Title)
                .HasMaxLength(254)
                .HasColumnName("title");
        });

        modelBuilder.Entity<TourCondition>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tour_conditions_id");

            entity.ToTable("tour_conditions", "phcp_120923");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.OrderBy).HasColumnName("order_by");
            entity.Property(e => e.Title)
                .HasMaxLength(254)
                .HasColumnName("title");
        });

        modelBuilder.Entity<TourFacility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tour_facilities_id");

            entity.ToTable("tour_facilities", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ComplianceBy).HasColumnName("compliance_by");
            entity.Property(e => e.ComplianceDate)
                .HasPrecision(0)
                .HasColumnName("compliance_date");
            entity.Property(e => e.ComplianceStatus)
                .HasDefaultValueSql("((0))")
                .HasColumnName("compliance_status");
            entity.Property(e => e.ComplianceSubmitDate)
                .HasPrecision(0)
                .HasColumnName("compliance_submit_date");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.FacilityId).HasColumnName("facility_id");
            entity.Property(e => e.FacilityVisitDate)
                .HasColumnType("date")
                .HasColumnName("facility_visit_date");
            entity.Property(e => e.FacilityVisitDays)
                .HasDefaultValueSql("((0))")
                .HasColumnName("facility_visit_days");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.TeamLeaderId).HasColumnName("team_leader_id");
            entity.Property(e => e.TourId).HasColumnName("tour_id");
        });

        modelBuilder.Entity<TourObservation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tour_observations_id");

            entity.ToTable("tour_observations", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ComplianceBy)
                .HasDefaultValueSql("((0))")
                .HasColumnName("compliance_by");
            entity.Property(e => e.ComplianceDate)
                .HasPrecision(0)
                .HasColumnName("compliance_date");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.FacilityId).HasColumnName("facility_id");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("is_deleted");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.MonitoringAreaId).HasColumnName("monitoring_area_id");
            entity.Property(e => e.Observation)
                .HasMaxLength(254)
                .HasColumnName("observation");
            entity.Property(e => e.ObservationCompliance).HasColumnName("Observation_Compliance");
            entity.Property(e => e.ObservationCondition)
                .HasDefaultValueSql("((0))")
                .HasColumnName("Observation_Condition");
            entity.Property(e => e.ObservationId).HasColumnName("observation_id");
            entity.Property(e => e.ObservationRemarks).HasColumnName("Observation_Remarks");
            entity.Property(e => e.OrderBy)
                .HasDefaultValueSql("((0))")
                .HasColumnName("order_by");
            entity.Property(e => e.Sequence).HasColumnName("sequence");
            entity.Property(e => e.SubAreaId).HasColumnName("sub_area_id");
            entity.Property(e => e.TourId).HasColumnName("tour_id");
        });

        modelBuilder.Entity<TourObservationDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tour_observation_detail_id");

            entity.ToTable("tour_observation_detail", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ObservationDetails).HasColumnName("Observation_Details");
            entity.Property(e => e.TourObservationId).HasColumnName("tour_observation_id");
        });

        modelBuilder.Entity<TourObservationImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tour_observation_image_id");

            entity.ToTable("tour_observation_image", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(254)
                .HasColumnName("image_path");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.MemberId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("member_id");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.OrderBy).HasColumnName("order_by");
            entity.Property(e => e.TourObservationId).HasColumnName("tour_observation_id");
            entity.Property(e => e.UloadedPath)
                .HasMaxLength(254)
                .HasColumnName("uloaded_path");
        });

        modelBuilder.Entity<TourStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tour_status_id");

            entity.ToTable("tour_status", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.OrderBy).HasColumnName("order_by");
            entity.Property(e => e.Title)
                .HasMaxLength(254)
                .HasColumnName("title");
        });

        modelBuilder.Entity<TourTeamMember>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_tour_team_members_id");

            entity.ToTable("tour_team_members", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.FacilityId).HasColumnName("facility_id");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.MemberAreaId).HasColumnName("member_area_id");
            entity.Property(e => e.MemberId).HasColumnName("member_id");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.TeamId).HasColumnName("team_id");
            entity.Property(e => e.TourId).HasColumnName("tour_id");
        });

        modelBuilder.Entity<UpdatedPatientsPacp>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("updated_patients_pacp", "phcp_120923");

            entity.Property(e => e.Cnic).HasColumnName("CNIC");
            entity.Property(e => e.Emrno).HasColumnName("EMRNo");
            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_users_id");

            entity.ToTable("users", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.Email)
                .HasMaxLength(254)
                .HasColumnName("email");
            entity.Property(e => e.ImagePath)
                .HasMaxLength(254)
                .HasColumnName("image_path");
            entity.Property(e => e.IsBlocked)
                .HasDefaultValueSql("((0))")
                .HasColumnName("is_blocked");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.OrderBy).HasColumnName("order_by");
            entity.Property(e => e.Password)
                .HasMaxLength(254)
                .HasColumnName("password");
            entity.Property(e => e.UploadedDir)
                .HasMaxLength(254)
                .HasColumnName("uploaded_dir");
            entity.Property(e => e.UserEntryId).HasColumnName("user_entry_id");
            entity.Property(e => e.UserType)
                .HasDefaultValueSql("((0))")
                .HasColumnName("user_type");
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_vehicles_id");

            entity.ToTable("vehicles", "phcp_120923");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasPrecision(0)
                .HasColumnName("created_on");
            entity.Property(e => e.IsDeleted).HasColumnName("is_deleted");
            entity.Property(e => e.Make)
                .HasMaxLength(254)
                .HasColumnName("make");
            entity.Property(e => e.Modal)
                .HasMaxLength(254)
                .HasColumnName("modal");
            entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");
            entity.Property(e => e.ModifiedOn)
                .HasPrecision(0)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("modified_on");
            entity.Property(e => e.OrderBy).HasColumnName("order_by");
            entity.Property(e => e.RegNo)
                .HasMaxLength(254)
                .HasColumnName("reg_no");
            entity.Property(e => e.Type)
                .HasMaxLength(254)
                .HasColumnName("type");
        });

        modelBuilder.Entity<ViewHfDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("view_hf_details", "phcp_120923");

            entity.Property(e => e.DistrictCode).HasColumnName("district_code");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("district_name");
            entity.Property(e => e.DivisionCode).HasColumnName("division_code");
            entity.Property(e => e.DivisionName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("division_name");
            entity.Property(e => e.HfCode).HasColumnName("hf_code");
            entity.Property(e => e.HfName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("hf_name");
            entity.Property(e => e.TehsilCode).HasColumnName("tehsil_code");
            entity.Property(e => e.TehsilName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tehsil_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
