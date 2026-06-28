using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class EmrvpDevContext : DbContext
{
    public EmrvpDevContext()
    {
    }

    public EmrvpDevContext(DbContextOptions<EmrvpDevContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityPlace> ActivityPlaces { get; set; }

    public virtual DbSet<ActivityQuestion> ActivityQuestions { get; set; }

    public virtual DbSet<ActivitySurvey> ActivitySurveys { get; set; }

    public virtual DbSet<ActivityType> ActivityTypes { get; set; }

    public virtual DbSet<Adjustment> Adjustments { get; set; }

    public virtual DbSet<Adult> Adults { get; set; }

    public virtual DbSet<AppLocationsUc> AppLocationsUcs { get; set; }

    public virtual DbSet<AppSetting> AppSettings { get; set; }

    public virtual DbSet<Artcenter> Artcenters { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<BarcodeGeneration> BarcodeGenerations { get; set; }

    public virtual DbSet<Child> Children { get; set; }

    public virtual DbSet<ComplaintsStatus> ComplaintsStatuses { get; set; }

    public virtual DbSet<ComponentCovered> ComponentCovereds { get; set; }

    public virtual DbSet<DashboardCount> DashboardCounts { get; set; }

    public virtual DbSet<Dfprofile> Dfprofiles { get; set; }

    public virtual DbSet<DiagnosisFacility> DiagnosisFacilities { get; set; }

    public virtual DbSet<DistributionPlanDetail> DistributionPlanDetails { get; set; }

    public virtual DbSet<DistributionPlanMaster> DistributionPlanMasters { get; set; }

    public virtual DbSet<DistrictWiseMonthlyReport> DistrictWiseMonthlyReports { get; set; }

    public virtual DbSet<Dtcprofile> Dtcprofiles { get; set; }

    public virtual DbSet<EmrvpsyncedDatum> EmrvpsyncedData { get; set; }

    public virtual DbSet<FollowUpTb> FollowUpTbs { get; set; }

    public virtual DbSet<FollowUpType> FollowUpTypes { get; set; }

    public virtual DbSet<FormImpcr> FormImpcrs { get; set; }

    public virtual DbSet<FormOltp> FormOltps { get; set; }

    public virtual DbSet<FormRmp> FormRmps { get; set; }

    public virtual DbSet<FormRp> FormRps { get; set; }

    public virtual DbSet<GeolvlV> GeolvlVs { get; set; }

    public virtual DbSet<HealthFacility> HealthFacilities { get; set; }

    public virtual DbSet<Hftype> Hftypes { get; set; }

    public virtual DbSet<LabPendingSamplesView> LabPendingSamplesViews { get; set; }

    public virtual DbSet<MedicalTest> MedicalTests { get; set; }

    public virtual DbSet<Medicine> Medicines { get; set; }

    public virtual DbSet<MedicineDispenser> MedicineDispensers { get; set; }

    public virtual DbSet<MedicineDispenser2> MedicineDispenser2s { get; set; }

    public virtual DbSet<MedicineDispenser3> MedicineDispenser3s { get; set; }

    public virtual DbSet<MedicineDispenserBackup> MedicineDispenserBackups { get; set; }

    public virtual DbSet<MonthlyPlan> MonthlyPlans { get; set; }

    public virtual DbSet<NotifiablePatientApp> NotifiablePatientApps { get; set; }

    public virtual DbSet<OldMisRhcHealthFacility> OldMisRhcHealthFacilities { get; set; }

    public virtual DbSet<Opdstat> Opdstats { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientBackup> PatientBackups { get; set; }

    public virtual DbSet<PatientBackup2> PatientBackup2s { get; set; }

    public virtual DbSet<PatientBackup3> PatientBackup3s { get; set; }

    public virtual DbSet<PatientBackup4> PatientBackup4s { get; set; }

    public virtual DbSet<PatientBackup5> PatientBackup5s { get; set; }

    public virtual DbSet<PatientBackup6> PatientBackup6s { get; set; }

    public virtual DbSet<PatientBackup7> PatientBackup7s { get; set; }

    public virtual DbSet<PatientContact> PatientContacts { get; set; }

    public virtual DbSet<PatientContactSymptom> PatientContactSymptoms { get; set; }

    public virtual DbSet<PatientContactsStatistic> PatientContactsStatistics { get; set; }

    public virtual DbSet<PatientHealthCard> PatientHealthCards { get; set; }

    public virtual DbSet<PatientMedicineAdjustment> PatientMedicineAdjustments { get; set; }

    public virtual DbSet<PatientMedicineHistory> PatientMedicineHistories { get; set; }

    public virtual DbSet<PatientMedicineHistoryLog> PatientMedicineHistoryLogs { get; set; }

    public virtual DbSet<PatientOutcome> PatientOutcomes { get; set; }

    public virtual DbSet<PatientSample> PatientSamples { get; set; }

    public virtual DbSet<PatientSchedule> PatientSchedules { get; set; }

    public virtual DbSet<PatientSymptom> PatientSymptoms { get; set; }

    public virtual DbSet<PatientTransferLog> PatientTransferLogs { get; set; }

    public virtual DbSet<PatientTreatmentInfoTb> PatientTreatmentInfoTbs { get; set; }

    public virtual DbSet<PatientTreatmentProgress> PatientTreatmentProgresses { get; set; }

    public virtual DbSet<PatientVitalsTb> PatientVitalsTbs { get; set; }

    public virtual DbSet<PatientsDatum> PatientsData { get; set; }

    public virtual DbSet<PatientsListDatum> PatientsListData { get; set; }

    public virtual DbSet<Ppaindicator> Ppaindicators { get; set; }

    public virtual DbSet<PpaindicatorsOption> PpaindicatorsOptions { get; set; }

    public virtual DbSet<PpaindicatorsTemp> PpaindicatorsTemps { get; set; }

    public virtual DbSet<Program> Programs { get; set; }

    public virtual DbSet<ProgramCenter> ProgramCenters { get; set; }

    public virtual DbSet<ReportEnableStatus> ReportEnableStatuses { get; set; }

    public virtual DbSet<RoleCategory> RoleCategories { get; set; }

    public virtual DbSet<Sheet1> Sheet1s { get; set; }

    public virtual DbSet<Sheet2> Sheet2s { get; set; }

    public virtual DbSet<Sheet3> Sheet3s { get; set; }

    public virtual DbSet<Sheet4> Sheet4s { get; set; }

    public virtual DbSet<Smslog> Smslogs { get; set; }

    public virtual DbSet<StockDetail> StockDetails { get; set; }

    public virtual DbSet<StockMaster> StockMasters { get; set; }

    public virtual DbSet<StockSource> StockSources { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Symptom> Symptoms { get; set; }

    public virtual DbSet<TbClinicsFeature> TbClinicsFeatures { get; set; }

    public virtual DbSet<TblComorbiditiesList> TblComorbiditiesLists { get; set; }

    public virtual DbSet<TblComorbidity> TblComorbidities { get; set; }

    public virtual DbSet<TbmappedHealthFacility> TbmappedHealthFacilities { get; set; }

    public virtual DbSet<TransferHistory> TransferHistories { get; set; }

    public virtual DbSet<UnitofMeasurement> UnitofMeasurements { get; set; }

    public virtual DbSet<VPatientDetail> VPatientDetails { get; set; }

    public virtual DbSet<VUserlist> VUserlists { get; set; }

    public virtual DbSet<VVisitDetail> VVisitDetails { get; set; }

    public virtual DbSet<ViewPatientListDashboard> ViewPatientListDashboards { get; set; }

    public virtual DbSet<ViewPatientSampleListDashboard> ViewPatientSampleListDashboards { get; set; }

    public virtual DbSet<Visit> Visits { get; set; }

    public virtual DbSet<Voucher> Vouchers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=172.16.0.6;Database=EMRVP_DEV;Persist Security Info=False;User Id=muddasir;Password=asd@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityPlace>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Activity__3213E83F788CF98E");

            entity.ToTable("ActivityPlace");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DeletionDate).HasColumnType("datetime");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.PlaceName).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.ActivityType).WithMany(p => p.ActivityPlaces)
                .HasForeignKey(d => d.ActivityTypeId)
                .HasConstraintName("FK_ActivityPlace_ActivityType");
        });

        modelBuilder.Entity<ActivityQuestion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Activity__3213E83FBA91B5AF");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DeletionDate).HasColumnType("datetime");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.Indicator).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ActivitySurvey>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Activity__3213E83F7C633433");

            entity.ToTable("ActivitySurvey");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Answer).HasColumnType("datetime");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DeletionDate).HasColumnType("datetime");
            entity.Property(e => e.DistrictCode).HasMaxLength(50);
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.Hfcode)
                .HasMaxLength(50)
                .HasColumnName("HFCode");
            entity.Property(e => e.Remarks).HasMaxLength(50);
            entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.Val).HasMaxLength(50);

            entity.HasOne(d => d.Indicator).WithMany(p => p.ActivitySurveys)
                .HasForeignKey(d => d.IndicatorId)
                .HasConstraintName("FK_ActivitySurvey_ActivityQuestions");

            entity.HasOne(d => d.Type).WithMany(p => p.ActivitySurveys)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_ActivitySurvey_ActivityType");
        });

        modelBuilder.Entity<ActivityType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Activity__3213E83F52E9F4E5");

            entity.ToTable("ActivityType");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActivityName).HasMaxLength(50);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DeletionDate).HasColumnType("datetime");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Adjustment>(entity =>
        {
            entity.ToTable("Adjustment");

            entity.Property(e => e.AdjustmentType).HasMaxLength(50);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DeletionDate).HasColumnType("datetime");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Voucher).WithMany(p => p.Adjustments)
                .HasForeignKey(d => d.VoucherId)
                .HasConstraintName("FK_Adjustment_Voucher");
        });

        modelBuilder.Entity<Adult>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Adult$");

            entity.Property(e => e.Id).HasMaxLength(255);
            entity.Property(e => e.IsAdult).HasMaxLength(255);
            entity.Property(e => e.MedicineCount).HasMaxLength(255);
            entity.Property(e => e.MedicineId).HasMaxLength(255);
            entity.Property(e => e.Month).HasMaxLength(255);
            entity.Property(e => e.PatientType).HasMaxLength(255);
            entity.Property(e => e.Phase).HasMaxLength(255);
            entity.Property(e => e.Tbtype)
                .HasMaxLength(255)
                .HasColumnName("TBType");
            entity.Property(e => e.TestId).HasMaxLength(255);
            entity.Property(e => e.TestResult).HasMaxLength(255);
            entity.Property(e => e.WeightMax).HasMaxLength(255);
            entity.Property(e => e.WeightMin).HasMaxLength(255);
        });

        modelBuilder.Entity<AppLocationsUc>(entity =>
        {
            entity.ToTable("AppLocationsUC");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.RecordStatus).HasMaxLength(20);
            entity.Property(e => e.Ucnumber)
                .HasMaxLength(50)
                .HasColumnName("UCNumber");
        });

        modelBuilder.Entity<AppSetting>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.AppVersion)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Artcenter>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ARTCenters");

            entity.Property(e => e.CenterId).HasColumnName("CenterID");
            entity.Property(e => e.CenterTypeId).HasColumnName("CenterTypeID");
            entity.Property(e => e.CentreType)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.DistrictName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.HasIndex(e => e.Geolvl, "XI_AspNetUsers");

            entity.HasIndex(e => e.LockoutEnabled, "X_AspNetUsers");

            entity.HasIndex(e => new { e.IsActive, e.UserName }, "X_AspNetUsers_IsActive_UserName");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Cnic).HasColumnName("CNIC");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Geolvl)
                .HasMaxLength(50)
                .HasColumnName("GEOLVL");
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<BarcodeGeneration>(entity =>
        {
            entity.ToTable("BarcodeGeneration");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Child$");

            entity.Property(e => e.Id).HasMaxLength(255);
            entity.Property(e => e.IsAdult).HasMaxLength(255);
            entity.Property(e => e.MedicineId).HasMaxLength(255);
            entity.Property(e => e.Month).HasMaxLength(255);
            entity.Property(e => e.PatientType).HasMaxLength(255);
            entity.Property(e => e.Phase).HasMaxLength(255);
            entity.Property(e => e.Tbtype)
                .HasMaxLength(255)
                .HasColumnName("TBType");
            entity.Property(e => e.TestId).HasMaxLength(255);
            entity.Property(e => e.TestResult).HasMaxLength(255);
            entity.Property(e => e.WeightMax).HasMaxLength(255);
            entity.Property(e => e.WeightMin).HasMaxLength(255);
        });

        modelBuilder.Entity<ComplaintsStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Complain__3213E83F82DAD63E");

            entity.ToTable("ComplaintsStatus");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DeletionDate).HasColumnType("datetime");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ComponentCovered>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Componen__3213E83FA5984975");

            entity.ToTable("ComponentCovered");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ComponentName).HasMaxLength(50);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DeletionDate).HasColumnType("datetime");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<DashboardCount>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("DashboardCount");
        });

        modelBuilder.Entity<Dfprofile>(entity =>
        {
            entity.ToTable("DFProfile");

            entity.Property(e => e.DfprofileId).HasColumnName("DFProfileId");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.Dfcnic)
                .HasMaxLength(20)
                .HasColumnName("DFCNIC");
            entity.Property(e => e.Dfemail)
                .HasMaxLength(200)
                .HasColumnName("DFEmail");
            entity.Property(e => e.DfmobileNo)
                .HasMaxLength(200)
                .HasColumnName("DFMobileNo");
            entity.Property(e => e.Dfname)
                .HasMaxLength(200)
                .HasColumnName("DFName");
            entity.Property(e => e.DtcprofileId).HasColumnName("DTCProfileId");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.HfmisCode).HasMaxLength(50);
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");

            entity.HasOne(d => d.Dtcprofile).WithMany(p => p.Dfprofiles)
                .HasForeignKey(d => d.DtcprofileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DFProfile_DTCProfile");
        });

        modelBuilder.Entity<DiagnosisFacility>(entity =>
        {
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DeletionDate).HasColumnType("datetime");
            entity.Property(e => e.DistrictCode).HasMaxLength(50);
            entity.Property(e => e.DivisionCode).HasMaxLength(50);
            entity.Property(e => e.Dst).HasColumnName("DST");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.HfmisCode).HasMaxLength(50);
            entity.Property(e => e.Lpa).HasColumnName("LPA");
            entity.Property(e => e.Spumicroscope).HasColumnName("SPUMicroscope");
            entity.Property(e => e.TehsilCode).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<DistributionPlanDetail>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.MedicineName).HasMaxLength(500);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);

            entity.HasOne(d => d.DistributionPlanMaster).WithMany(p => p.DistributionPlanDetails)
                .HasForeignKey(d => d.DistributionPlanMasterId)
                .HasConstraintName("FK_DistributionPlanDetails_DistributionPlanMaster");

            entity.HasOne(d => d.Medicine).WithMany(p => p.DistributionPlanDetails)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK_DistributionPlanDetails_Medicine");
        });

        modelBuilder.Entity<DistributionPlanMaster>(entity =>
        {
            entity.ToTable("DistributionPlanMaster");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.Hfmiscode)
                .HasMaxLength(50)
                .HasColumnName("HFMISCode");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
        });

        modelBuilder.Entity<DistrictWiseMonthlyReport>(entity =>
        {
            entity.ToTable("DistrictWiseMonthlyReport");

            entity.Property(e => e.AvailableAttadultStock)
                .HasMaxLength(100)
                .HasColumnName("AvailableATTAdultStock");
            entity.Property(e => e.AvailableAttpediatricStock)
                .HasMaxLength(100)
                .HasColumnName("AvailableATTPediatricStock");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DeletedDate).HasColumnType("datetime");
            entity.Property(e => e.DistrictCode).HasMaxLength(50);
            entity.Property(e => e.EnableReportDateFrom).HasColumnType("datetime");
            entity.Property(e => e.EnableReportDateTo).HasColumnType("datetime");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.HfmisCode).HasMaxLength(50);
            entity.Property(e => e.LhwcaseRegList).HasColumnName("LHWCaseRegList");
            entity.Property(e => e.MonthlyOpdcount).HasColumnName("MonthlyOPDCount");
            entity.Property(e => e.NoOfBmu).HasColumnName("NoOfBMU");
            entity.Property(e => e.NoOfHhcontactIdentified).HasColumnName("NoOfHHContactIdentified");
            entity.Property(e => e.NoOfHhcontactsFoundTb).HasColumnName("NoOfHHContactsFoundTB");
            entity.Property(e => e.NoOfHhscreenedContacts).HasColumnName("NoOfHHScreenedContacts");
            entity.Property(e => e.NoOfLhwinterventionRegCases).HasColumnName("NoOfLHWInterventionRegCases");
            entity.Property(e => e.NoOfLhwspresumptiveIdentified).HasColumnName("NoOfLHWSPresumptiveIdentified");
            entity.Property(e => e.NoOfPrivateSectorIdenPcases)
                .HasDefaultValueSql("((0))")
                .HasColumnName("NoOfPrivateSectorIdenPCases");
            entity.Property(e => e.NoOfPublicSectorIdenPcases).HasColumnName("NoOfPublicSectorIdenPCases");
            entity.Property(e => e.NoOfStaffShortageBmu).HasColumnName("NoOfStaffShortageBMU");
            entity.Property(e => e.PvtTotalTbcasesReg).HasColumnName("PvtTotalTBCasesReg");
            entity.Property(e => e.ReportingMonth).HasMaxLength(50);
            entity.Property(e => e.TotalDetectedRrcases).HasColumnName("TotalDetectedRRCases");
            entity.Property(e => e.TotalHivreactiveCases).HasColumnName("TotalHIVReactiveCases");
            entity.Property(e => e.TotalHivscreenedTbpatients).HasColumnName("TotalHIVScreenedTBPatients");
            entity.Property(e => e.TotalMicroscopyForAfb).HasColumnName("TotalMicroscopyForAFB");
            entity.Property(e => e.TotalPositiveAfbseamer).HasColumnName("TotalPositiveAFBSeamer");
            entity.Property(e => e.TotalRegisteredAllTypeTbcases).HasColumnName("TotalRegisteredAllTypeTBCases");
            entity.Property(e => e.TotalRegisteredChildhoodTbcases).HasColumnName("TotalRegisteredChildhoodTBCases");
            entity.Property(e => e.TotalRegisteredExtraPulmonaryTbcases).HasColumnName("TotalRegisteredExtraPulmonaryTBCases");
            entity.Property(e => e.TotalRegisteredGxpertTbcases).HasColumnName("TotalRegisteredGXpertTBCases");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Dtcprofile>(entity =>
        {
            entity.ToTable("DTCProfile");

            entity.Property(e => e.DtcprofileId).HasColumnName("DTCProfileId");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.DistrictCode).HasMaxLength(50);
            entity.Property(e => e.Dtccnic)
                .HasMaxLength(20)
                .HasColumnName("DTCCNIC");
            entity.Property(e => e.Dtcemail)
                .HasMaxLength(200)
                .HasColumnName("DTCEmail");
            entity.Property(e => e.DtcmobileNo)
                .HasMaxLength(200)
                .HasColumnName("DTCMobileNo");
            entity.Property(e => e.Dtcname)
                .HasMaxLength(200)
                .HasColumnName("DTCName");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
        });

        modelBuilder.Entity<EmrvpsyncedDatum>(entity =>
        {
            entity.ToTable("EMRVPSyncedData");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<FollowUpTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_FollowUp");

            entity.ToTable("FollowUpTB");

            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.PatientId).HasColumnName("Patient_Id");
            entity.Property(e => e.PatientReportingDate)
                .HasColumnType("datetime")
                .HasColumnName("Patient_Reporting_date");
            entity.Property(e => e.ProgramId).HasColumnName("Program_Id");
            entity.Property(e => e.Purpose).HasMaxLength(50);
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
            entity.Property(e => e.VisitId).HasColumnName("Visit_Id");

            entity.HasOne(d => d.FollowUpType).WithMany(p => p.FollowUpTbs)
                .HasForeignKey(d => d.FollowUpTypeId)
                .HasConstraintName("FK_FollowUpTB_FollowUpTypes");

            entity.HasOne(d => d.Patient).WithMany(p => p.FollowUpTbs)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_FollowUp_Patient");

            entity.HasOne(d => d.Program).WithMany(p => p.FollowUpTbs)
                .HasForeignKey(d => d.ProgramId)
                .HasConstraintName("FK_FollowUp_Program");

            entity.HasOne(d => d.Visit).WithMany(p => p.FollowUpTbs)
                .HasForeignKey(d => d.VisitId)
                .HasConstraintName("FK_FollowUpTB_Visit");
        });

        modelBuilder.Entity<FollowUpType>(entity =>
        {
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DeletionDate).HasColumnType("datetime");
            entity.Property(e => e.FollowUpType1)
                .HasMaxLength(50)
                .HasColumnName("FollowUpType");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<FormImpcr>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_D");

            entity.ToTable("FormIMPCR");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.AddressNotifying)
                .HasMaxLength(50)
                .HasColumnName("Address_Notifying");
            entity.Property(e => e.Cnic)
                .HasMaxLength(50)
                .HasColumnName("CNIC");
            entity.Property(e => e.ContactPersonNo)
                .HasMaxLength(50)
                .HasColumnName("Contact_Person_No");
            entity.Property(e => e.Created).HasMaxLength(50);
            entity.Property(e => e.DateOfExperiencing)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_Experiencing");
            entity.Property(e => e.DateOfSendingNotification)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_Sending_Notification");
            entity.Property(e => e.DesignationNotifying)
                .HasMaxLength(50)
                .HasColumnName("Designation_Notifying");
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.EmailReffering)
                .HasMaxLength(50)
                .HasColumnName("Email_Reffering");
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Lat).HasMaxLength(50);
            entity.Property(e => e.Lng).HasMaxLength(50);
            entity.Property(e => e.MobCreated).HasColumnName("Mob_Created");
            entity.Property(e => e.MobUpdated).HasColumnName("Mob_Updated");
            entity.Property(e => e.NameNotifying)
                .HasMaxLength(50)
                .HasColumnName("Name_Notifying");
            entity.Property(e => e.NameOfFatherHusband)
                .HasMaxLength(50)
                .HasColumnName("Name_of_Father_Husband");
            entity.Property(e => e.NameOfPatient)
                .HasMaxLength(50)
                .HasColumnName("Name_of_Patient");
            entity.Property(e => e.NightSweats).HasColumnName("Night_Sweats");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.SoftDelete).HasColumnName("Soft_Delete");
            entity.Property(e => e.Source).HasMaxLength(50);
            entity.Property(e => e.TestConducted)
                .HasMaxLength(50)
                .HasColumnName("Test_Conducted");
            entity.Property(e => e.Updated).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.WeightLoss).HasColumnName("Weight_Loss");
        });

        modelBuilder.Entity<FormOltp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_C");

            entity.ToTable("FormOLTP");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Cnic)
                .HasMaxLength(50)
                .HasColumnName("CNIC");
            entity.Property(e => e.ContactPersonNo)
                .HasMaxLength(50)
                .HasColumnName("Contact_Person_No");
            entity.Property(e => e.Created).HasMaxLength(50);
            entity.Property(e => e.CultureOfBodyFluid)
                .HasMaxLength(50)
                .HasColumnName("Culture_of_Body_Fluid");
            entity.Property(e => e.CultureOfSputum)
                .HasMaxLength(50)
                .HasColumnName("Culture_of_Sputum");
            entity.Property(e => e.DateOfSendingNotification)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_Sending_Notification");
            entity.Property(e => e.DateOfSpecimen)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_Specimen");
            entity.Property(e => e.Designation).HasMaxLength(50);
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.GXpert).HasColumnName("G_Xpert");
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Lat).HasMaxLength(50);
            entity.Property(e => e.Lng).HasMaxLength(50);
            entity.Property(e => e.MedicineInUse)
                .HasMaxLength(50)
                .HasColumnName("Medicine_in_Use");
            entity.Property(e => e.MicroResultOfFluid)
                .HasMaxLength(50)
                .HasColumnName("Micro_Result_of_Fluid");
            entity.Property(e => e.MicroResultOfSputum)
                .HasMaxLength(50)
                .HasColumnName("Micro_Result_of_Sputum");
            entity.Property(e => e.MobCreated).HasColumnName("Mob_Created");
            entity.Property(e => e.MobUpdated).HasColumnName("Mob_Updated");
            entity.Property(e => e.NameOfFatherHusband)
                .HasMaxLength(50)
                .HasColumnName("Name_of_Father_Husband");
            entity.Property(e => e.NameOfIncharge)
                .HasMaxLength(50)
                .HasColumnName("Name_of_Incharge");
            entity.Property(e => e.NameOfPatient)
                .HasMaxLength(50)
                .HasColumnName("Name_of_Patient");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.Qualification).HasMaxLength(50);
            entity.Property(e => e.Source).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TestAddress)
                .HasMaxLength(50)
                .HasColumnName("Test_Address");
            entity.Property(e => e.TestPersonDesignation)
                .HasMaxLength(50)
                .HasColumnName("Test_Person_Designation");
            entity.Property(e => e.TestPersonName)
                .HasMaxLength(50)
                .HasColumnName("Test_Person_Name");
            entity.Property(e => e.Updated).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UserId).HasColumnName("User_Id");
        });

        modelBuilder.Entity<FormRmp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_A");

            entity.ToTable("FormRMP");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.ChronicRenalDiseas).HasColumnName("Chronic_Renal_Diseas");
            entity.Property(e => e.Cnic)
                .HasMaxLength(50)
                .HasColumnName("CNIC");
            entity.Property(e => e.ConformityEvidence).HasColumnName("Conformity_Evidence");
            entity.Property(e => e.ContactPersonNo)
                .HasMaxLength(50)
                .HasColumnName("Contact_Person_No");
            entity.Property(e => e.Created).HasMaxLength(50);
            entity.Property(e => e.DateOfFirstVisit)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_First_Visit");
            entity.Property(e => e.DateOfOnsetIllness)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_Onset_Illness");
            entity.Property(e => e.DateOfSendingNotification)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_Sending_Notification");
            entity.Property(e => e.DiseaseSite)
                .HasMaxLength(50)
                .HasColumnName("Disease_Site");
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.EmailReffering)
                .HasMaxLength(50)
                .HasColumnName("Email_Reffering");
            entity.Property(e => e.FluidsAfb)
                .HasMaxLength(50)
                .HasColumnName("Fluids_Afb");
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityName)
                .HasMaxLength(50)
                .HasColumnName("Health_Facility_Name");
            entity.Property(e => e.IsCulture).HasColumnName("Is_Culture");
            entity.Property(e => e.IsRadio).HasColumnName("Is_Radio");
            entity.Property(e => e.IsXpert).HasColumnName("Is_Xpert");
            entity.Property(e => e.Lat).HasMaxLength(50);
            entity.Property(e => e.Lng).HasMaxLength(50);
            entity.Property(e => e.MobCreated).HasColumnName("Mob_Created");
            entity.Property(e => e.MobUpdated).HasColumnName("Mob_Updated");
            entity.Property(e => e.MtbDetected).HasColumnName("Mtb_Detected");
            entity.Property(e => e.NameOfFatherHusband)
                .HasMaxLength(50)
                .HasColumnName("Name_of_Father_Husband");
            entity.Property(e => e.NameOfPatient)
                .HasMaxLength(50)
                .HasColumnName("Name_of_Patient");
            entity.Property(e => e.NightSweats).HasColumnName("Night_Sweats");
            entity.Property(e => e.Occupation).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.PmdcNo)
                .HasMaxLength(50)
                .HasColumnName("PMDC_No");
            entity.Property(e => e.PmdcRegistration)
                .HasMaxLength(50)
                .HasColumnName("PMDC_Registration");
            entity.Property(e => e.ProviderCode)
                .HasMaxLength(50)
                .HasColumnName("Provider_Code");
            entity.Property(e => e.Radiological).HasMaxLength(50);
            entity.Property(e => e.RefferingPhysician)
                .HasMaxLength(50)
                .HasColumnName("Reffering_Physician");
            entity.Property(e => e.RefferingPhysicianAddress)
                .HasMaxLength(50)
                .HasColumnName("Reffering_Physician_Address");
            entity.Property(e => e.RifResistanceDetected).HasColumnName("Rif_Resistance_Detected");
            entity.Property(e => e.Source).HasMaxLength(50);
            entity.Property(e => e.Sputum).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TreatmentMedical)
                .HasMaxLength(50)
                .HasColumnName("Treatment_Medical");
            entity.Property(e => e.TypeOfPatient)
                .HasMaxLength(50)
                .HasColumnName("Type_of_Patient");
            entity.Property(e => e.Updated).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.Weight).HasMaxLength(50);
        });

        modelBuilder.Entity<FormRp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_B");

            entity.ToTable("FormRP");

            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.CaseOfRetreat)
                .HasMaxLength(50)
                .HasColumnName("Case_of_Retreat");
            entity.Property(e => e.Cnic)
                .HasMaxLength(50)
                .HasColumnName("CNIC");
            entity.Property(e => e.ConfirmEvidence).HasColumnName("Confirm_Evidence");
            entity.Property(e => e.ContactPersonNo)
                .HasMaxLength(50)
                .HasColumnName("Contact_Person_No");
            entity.Property(e => e.Created).HasMaxLength(50);
            entity.Property(e => e.DateOfFirstVisit)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_First_Visit");
            entity.Property(e => e.DateOfOnsetIllness)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_Onset_Illness");
            entity.Property(e => e.DateOfSendingNotification)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_Sending_Notification");
            entity.Property(e => e.DiseaseSite)
                .HasMaxLength(50)
                .HasColumnName("Disease_Site");
            entity.Property(e => e.Dob)
                .HasColumnType("datetime")
                .HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Fnac).HasMaxLength(50);
            entity.Property(e => e.GXpert).HasColumnName("G_Xpert");
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityName)
                .HasMaxLength(50)
                .HasColumnName("Health_Facility_Name");
            entity.Property(e => e.HistoryInFamily)
                .HasMaxLength(50)
                .HasColumnName("History_in_Family");
            entity.Property(e => e.Lat).HasMaxLength(50);
            entity.Property(e => e.Lng).HasMaxLength(50);
            entity.Property(e => e.MedicineInUse)
                .HasMaxLength(50)
                .HasColumnName("Medicine_in_Use");
            entity.Property(e => e.MobCreated).HasColumnName("Mob_Created");
            entity.Property(e => e.MobUpdated).HasColumnName("Mob_Updated");
            entity.Property(e => e.NameOfFatherHusband)
                .HasMaxLength(50)
                .HasColumnName("Name_of_Father_Husband");
            entity.Property(e => e.NameOfPatient)
                .HasMaxLength(50)
                .HasColumnName("Name_of_Patient");
            entity.Property(e => e.NightSweats).HasColumnName("Night_Sweats");
            entity.Property(e => e.Occupation).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.PractRegNo)
                .HasMaxLength(50)
                .HasColumnName("Pract_Reg_No");
            entity.Property(e => e.RadioFinding)
                .HasMaxLength(50)
                .HasColumnName("Radio_Finding");
            entity.Property(e => e.SEchoStatus)
                .HasMaxLength(50)
                .HasColumnName("S_Echo_Status");
            entity.Property(e => e.Source).HasMaxLength(50);
            entity.Property(e => e.SputumAfb)
                .HasMaxLength(50)
                .HasColumnName("Sputum_Afb");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TypeOfPatient)
                .HasMaxLength(50)
                .HasColumnName("Type_of_Patient");
            entity.Property(e => e.UnderTreatment)
                .HasMaxLength(50)
                .HasColumnName("Under_Treatment");
            entity.Property(e => e.Updated).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.WeightLoss).HasColumnName("Weight_Loss");
        });

        modelBuilder.Entity<GeolvlV>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GEOLVL_V");

            entity.Property(e => e.Code).HasColumnName("CODE");
            entity.Property(e => e.Fkcode).HasColumnName("FKCODE");
            entity.Property(e => e.Lnth).HasColumnName("LNTH");
            entity.Property(e => e.Lvl)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("LVL");
            entity.Property(e => e.Name).HasColumnName("NAME");
            entity.Property(e => e.Pkcode).HasColumnName("PKCODE");
        });

        modelBuilder.Entity<HealthFacility>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("HealthFacility");

            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Created_Date");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.EntityLifecycleId).HasColumnName("Entity_Lifecycle_Id");
            entity.Property(e => e.FaxNo).HasMaxLength(50);
            entity.Property(e => e.Hfac).HasColumnName("HFAC");
            entity.Property(e => e.HfcategoryName).HasColumnName("HFCategoryName");
            entity.Property(e => e.Hfmiscode).HasColumnName("HFMISCode");
            entity.Property(e => e.HftypeCode).HasColumnName("HFTypeCode");
            entity.Property(e => e.HftypeName).HasColumnName("HFTypeName");
            entity.Property(e => e.LastModifiedBy).HasColumnName("Last_Modified_By");
            entity.Property(e => e.Mauza).HasMaxLength(50);
            entity.Property(e => e.Na)
                .HasMaxLength(30)
                .HasColumnName("NA");
            entity.Property(e => e.PhoneNo).HasMaxLength(50);
            entity.Property(e => e.Pp)
                .HasMaxLength(30)
                .HasColumnName("PP");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.UcName).HasMaxLength(50);
            entity.Property(e => e.UcNo).HasMaxLength(10);
            entity.Property(e => e.UsersId)
                .HasMaxLength(128)
                .HasColumnName("Users_Id");
        });

        modelBuilder.Entity<Hftype>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("HFTypes");

            entity.Property(e => e.HfcatId).HasColumnName("HFCat_Id");
        });

        modelBuilder.Entity<LabPendingSamplesView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("LabPendingSamplesView");

            entity.Property(e => e.BarcodeNo)
                .HasMaxLength(50)
                .HasColumnName("Barcode_No");
            entity.Property(e => e.FromHf)
                .HasMaxLength(50)
                .HasColumnName("FromHF");
            entity.Property(e => e.Hfto)
                .HasMaxLength(50)
                .HasColumnName("HFTo");
            entity.Property(e => e.HftoFullName).HasColumnName("HFToFullName");
            entity.Property(e => e.HftypeName).HasColumnName("HFTypeName");
            entity.Property(e => e.LabNo).HasMaxLength(50);
            entity.Property(e => e.LabStatus)
                .HasMaxLength(50)
                .HasColumnName("Lab_Status");
            entity.Property(e => e.LabStatusUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Lab_Status_Update_Date");
            entity.Property(e => e.ReasonForRejection)
                .HasMaxLength(50)
                .HasColumnName("Reason_for_Rejection");
            entity.Property(e => e.ReceivedBy).HasColumnName("Received_By");
            entity.Property(e => e.ReceivingDate)
                .HasColumnType("datetime")
                .HasColumnName("Receiving_Date");
            entity.Property(e => e.ReceivingStatus)
                .HasMaxLength(50)
                .HasColumnName("Receiving_Status");
            entity.Property(e => e.Result).HasMaxLength(50);
            entity.Property(e => e.ResultUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Result_Update_Date");
            entity.Property(e => e.ResultUpdatedBy).HasColumnName("Result_Updated_By");
            entity.Property(e => e.SampleCollectionDate).HasColumnType("datetime");
            entity.Property(e => e.SampleNo)
                .HasMaxLength(50)
                .HasColumnName("Sample_No");
            entity.Property(e => e.SamplePerformDate).HasColumnType("datetime");
            entity.Property(e => e.SampleTransportMode).HasMaxLength(20);
            entity.Property(e => e.SampleTransportModeDescription).HasMaxLength(100);
            entity.Property(e => e.SamplingBy).HasColumnName("Sampling_By");
            entity.Property(e => e.SamplingDate)
                .HasColumnType("datetime")
                .HasColumnName("Sampling_Date");
            entity.Property(e => e.SpecimenType)
                .HasMaxLength(50)
                .HasColumnName("Specimen_Type");
        });

        modelBuilder.Entity<MedicalTest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Test");

            entity.ToTable("MedicalTest");

            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.ProgramId).HasColumnName("Program_Id");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.TestDescription).HasColumnName("Test_Description");
            entity.Property(e => e.TestName)
                .HasMaxLength(50)
                .HasColumnName("Test_Name");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
        });

        modelBuilder.Entity<Medicine>(entity =>
        {
            entity.ToTable("Medicine");

            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.GenericFormula)
                .HasMaxLength(500)
                .HasColumnName("Generic_Formula");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.ManufacturedBy)
                .HasMaxLength(200)
                .HasColumnName("Manufactured_By");
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.PackingSize)
                .HasMaxLength(50)
                .HasColumnName("Packing_Size");
            entity.Property(e => e.Potency).HasMaxLength(200);
            entity.Property(e => e.ProgramId).HasColumnName("Program_Id");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.Uomid).HasColumnName("UOMId");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");

            entity.HasOne(d => d.Uom).WithMany(p => p.Medicines)
                .HasForeignKey(d => d.Uomid)
                .HasConstraintName("FK_Medicine_UOM");
        });

        modelBuilder.Entity<MedicineDispenser>(entity =>
        {
            entity.ToTable("MedicineDispenser");

            entity.Property(e => e.MedicineCount).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.PatientType).HasMaxLength(50);
            entity.Property(e => e.Phase).HasMaxLength(50);
            entity.Property(e => e.Tbtype)
                .HasMaxLength(50)
                .HasColumnName("TBType");
            entity.Property(e => e.TestResult).HasMaxLength(50);
            entity.Property(e => e.WeightMax).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.WeightMin).HasColumnType("decimal(18, 5)");
        });

        modelBuilder.Entity<MedicineDispenser2>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MedicineDispenser2");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.MedicineCount).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.PatientType).HasMaxLength(50);
            entity.Property(e => e.Phase).HasMaxLength(50);
            entity.Property(e => e.Tbtype)
                .HasMaxLength(50)
                .HasColumnName("TBType");
            entity.Property(e => e.TestResult).HasMaxLength(50);
            entity.Property(e => e.WeightMax).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.WeightMin).HasColumnType("decimal(18, 5)");
        });

        modelBuilder.Entity<MedicineDispenser3>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MedicineDispenser3");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.MedicineCount).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.PatientType).HasMaxLength(50);
            entity.Property(e => e.Phase).HasMaxLength(50);
            entity.Property(e => e.Tbtype)
                .HasMaxLength(50)
                .HasColumnName("TBType");
            entity.Property(e => e.TestResult).HasMaxLength(50);
            entity.Property(e => e.WeightMax).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.WeightMin).HasColumnType("decimal(18, 5)");
        });

        modelBuilder.Entity<MedicineDispenserBackup>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MedicineDispenserBackup");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.MedicineCount).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.PatientType).HasMaxLength(50);
            entity.Property(e => e.Phase).HasMaxLength(50);
            entity.Property(e => e.Tbtype)
                .HasMaxLength(50)
                .HasColumnName("TBType");
            entity.Property(e => e.TestResult).HasMaxLength(50);
            entity.Property(e => e.WeightMax).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.WeightMin).HasColumnType("decimal(18, 5)");
        });

        modelBuilder.Entity<MonthlyPlan>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MonthlyP__3213E83F651EEE53");

            entity.ToTable("MonthlyPlan");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActivityDate).HasColumnType("datetime");
            entity.Property(e => e.ActivityMonth).HasMaxLength(50);
            entity.Property(e => e.Comments).HasMaxLength(50);
            entity.Property(e => e.CompletedBy).HasMaxLength(50);
            entity.Property(e => e.CompletedDate).HasColumnType("datetime");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DeletionDate).HasColumnType("datetime");
            entity.Property(e => e.Designation).HasMaxLength(50);
            entity.Property(e => e.FocalPerson).HasMaxLength(50);
            entity.Property(e => e.FromDistrict).HasMaxLength(50);
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.Hfcode)
                .HasMaxLength(50)
                .HasColumnName("HFCode");
            entity.Property(e => e.Images).HasMaxLength(255);
            entity.Property(e => e.Latitude).HasMaxLength(50);
            entity.Property(e => e.Longitude).HasMaxLength(50);
            entity.Property(e => e.ToDistrict).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Component).WithMany(p => p.MonthlyPlans)
                .HasForeignKey(d => d.ComponentId)
                .HasConstraintName("FK_MonthlyPlan_ComponentCovered");

            entity.HasOne(d => d.Type).WithMany(p => p.MonthlyPlans)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_MonthlyPlan_ActivityType");
        });

        modelBuilder.Entity<NotifiablePatientApp>(entity =>
        {
            entity.ToTable("NotifiablePatientApp");

            entity.Property(e => e.AddressNotifying)
                .HasMaxLength(50)
                .HasColumnName("Address_Notifying");
            entity.Property(e => e.CaseOfRetreat)
                .HasMaxLength(50)
                .HasColumnName("Case_of_Retreat");
            entity.Property(e => e.ChronicRenalDiseas).HasColumnName("Chronic_Renal_Diseas");
            entity.Property(e => e.ConfirmEvidence).HasColumnName("Confirm_Evidence");
            entity.Property(e => e.ConformityEvidence).HasColumnName("Conformity_Evidence");
            entity.Property(e => e.ContactPersonNo)
                .HasMaxLength(50)
                .HasColumnName("Contact_Person_No");
            entity.Property(e => e.CultureOfBodyFluid)
                .HasMaxLength(50)
                .HasColumnName("Culture_of_Body_Fluid");
            entity.Property(e => e.CultureOfSputum)
                .HasMaxLength(50)
                .HasColumnName("Culture_of_Sputum");
            entity.Property(e => e.DateOfExperiencing)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_Experiencing");
            entity.Property(e => e.DateOfFirstVisit)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_First_Visit");
            entity.Property(e => e.DateOfOnsetIllness)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_Onset_Illness");
            entity.Property(e => e.DateOfSendingNotification)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_Sending_Notification");
            entity.Property(e => e.DateOfSpecimen)
                .HasColumnType("datetime")
                .HasColumnName("Date_of_Specimen");
            entity.Property(e => e.Designation).HasMaxLength(50);
            entity.Property(e => e.DesignationNotifying)
                .HasMaxLength(50)
                .HasColumnName("Designation_Notifying");
            entity.Property(e => e.DiseaseSite)
                .HasMaxLength(50)
                .HasColumnName("Disease_Site");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.EmailReffering)
                .HasMaxLength(50)
                .HasColumnName("Email_Reffering");
            entity.Property(e => e.FluidsAfb)
                .HasMaxLength(50)
                .HasColumnName("Fluids_Afb");
            entity.Property(e => e.Fnac).HasMaxLength(50);
            entity.Property(e => e.GXpert).HasColumnName("G_Xpert");
            entity.Property(e => e.HealthFacilityName)
                .HasMaxLength(50)
                .HasColumnName("Health_Facility_Name");
            entity.Property(e => e.HistoryInFamily)
                .HasMaxLength(50)
                .HasColumnName("History_in_Family");
            entity.Property(e => e.IsCulture).HasColumnName("Is_Culture");
            entity.Property(e => e.IsRadio).HasColumnName("Is_Radio");
            entity.Property(e => e.IsXpert)
                .HasMaxLength(50)
                .HasColumnName("Is_Xpert");
            entity.Property(e => e.Lat).HasMaxLength(50);
            entity.Property(e => e.Lng).HasMaxLength(50);
            entity.Property(e => e.MedicineInUse)
                .HasMaxLength(50)
                .HasColumnName("Medicine_in_Use");
            entity.Property(e => e.MicroResultOfFluid)
                .HasMaxLength(50)
                .HasColumnName("Micro_Result_of_Fluid");
            entity.Property(e => e.MicroResultOfSputum)
                .HasMaxLength(50)
                .HasColumnName("Micro_Result_of_Sputum");
            entity.Property(e => e.MobCreated).HasColumnName("Mob_Created");
            entity.Property(e => e.MobUpdated).HasColumnName("Mob_Updated");
            entity.Property(e => e.MtbDetected).HasColumnName("Mtb_Detected");
            entity.Property(e => e.NameNotifying)
                .HasMaxLength(50)
                .HasColumnName("Name_Notifying");
            entity.Property(e => e.NameOfIncharge)
                .HasMaxLength(50)
                .HasColumnName("Name_of_Incharge");
            entity.Property(e => e.NightSweats).HasColumnName("Night_Sweats");
            entity.Property(e => e.PmdcRegistration)
                .HasMaxLength(50)
                .HasColumnName("PMDC_Registration");
            entity.Property(e => e.PractRegNo)
                .HasMaxLength(50)
                .HasColumnName("Pract_Reg_No");
            entity.Property(e => e.ProviderCode)
                .HasMaxLength(50)
                .HasColumnName("Provider_Code");
            entity.Property(e => e.Qualification).HasMaxLength(50);
            entity.Property(e => e.RadioFinding)
                .HasMaxLength(50)
                .HasColumnName("Radio_Finding");
            entity.Property(e => e.Radiological).HasMaxLength(50);
            entity.Property(e => e.RefferingPhysician)
                .HasMaxLength(50)
                .HasColumnName("Reffering_Physician");
            entity.Property(e => e.RefferingPhysicianAddress)
                .HasMaxLength(50)
                .HasColumnName("Reffering_Physician_Address");
            entity.Property(e => e.RifResistanceDetected).HasColumnName("Rif_Resistance_Detected");
            entity.Property(e => e.SEchoStatus)
                .HasMaxLength(50)
                .HasColumnName("S_Echo_Status");
            entity.Property(e => e.Source).HasMaxLength(50);
            entity.Property(e => e.Sputum).HasMaxLength(50);
            entity.Property(e => e.SputumAfb)
                .HasMaxLength(50)
                .HasColumnName("Sputum_Afb");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TestAddress)
                .HasMaxLength(50)
                .HasColumnName("Test_Address");
            entity.Property(e => e.TestConducted)
                .HasMaxLength(50)
                .HasColumnName("Test_Conducted");
            entity.Property(e => e.TestPersonDesignation)
                .HasMaxLength(50)
                .HasColumnName("Test_Person_Designation");
            entity.Property(e => e.TestPersonName)
                .HasMaxLength(50)
                .HasColumnName("Test_Person_Name");
            entity.Property(e => e.TreatmentMedical)
                .HasMaxLength(50)
                .HasColumnName("Treatment_Medical");
            entity.Property(e => e.TypeOfPatient)
                .HasMaxLength(50)
                .HasColumnName("Type_of_Patient");
            entity.Property(e => e.UnderTreatment)
                .HasMaxLength(50)
                .HasColumnName("Under_Treatment");
            entity.Property(e => e.Updated).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.WeightLoss).HasColumnName("Weight_Loss");

            entity.HasOne(d => d.Patient).WithMany(p => p.NotifiablePatientApps)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_NotifiablePatientApp_Patient");
        });

        modelBuilder.Entity<OldMisRhcHealthFacility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Sheet1$");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DistrictName).HasMaxLength(50);
            entity.Property(e => e.DivisionName).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityName).HasMaxLength(255);
            entity.Property(e => e.HfCategory).HasMaxLength(50);
            entity.Property(e => e.Identifier).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TehsilName).HasMaxLength(50);
        });

        modelBuilder.Entity<Opdstat>(entity =>
        {
            entity.ToTable("OPDStats");

            entity.HasIndex(e => new { e.RecordStatus, e.CreatedBy }, "XI_OPDStats");

            entity.HasIndex(e => new { e.CreationDate, e.RecordStatus, e.CreatedBy }, "X_OPDStats");

            entity.Property(e => e.CreationDate).HasColumnType("date");
            entity.Property(e => e.Hfcode)
                .HasMaxLength(50)
                .HasColumnName("HFCode");
            entity.Property(e => e.Opdcount).HasColumnName("OPDCount");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patient");

            entity.HasIndex(e => e.Gender, "XII_Patient");

            entity.HasIndex(e => e.Age, "XXII_Patient");

            entity.HasIndex(e => e.Guid, "XXI_Patient");

            entity.HasIndex(e => new { e.RecordStatus, e.HealthFacilityCode }, "XXXIII_Patient");

            entity.HasIndex(e => new { e.CreatedBy, e.RecordStatus, e.HealthFacilityCode }, "XXXI_Patient");

            entity.HasIndex(e => new { e.RecordStatus, e.Guid }, "XX_Patient");

            entity.HasIndex(e => new { e.Gender, e.RecordStatus, e.Status, e.CreationDate }, "X_Patient_Gender_Record_Status_Status_Creation_Date");

            entity.HasIndex(e => new { e.RecordStatus, e.CreationDate, e.Status, e.Age }, "X_Patient_Record_Status_Creation_Date_Status_Age");

            entity.HasIndex(e => new { e.RecordStatus, e.HealthFacilityCode, e.CreationDate }, "X_Patient_Record_Status_Health_Facility_Code_Creation_Date");

            entity.HasIndex(e => new { e.RecordStatus, e.Status, e.CreationDate, e.Age }, "X_Patient_Record_Status_Status_Creation_Date_Age");

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CnicGuardianRelation)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Guardian_Relation");
            entity.Property(e => e.CnicType)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Type");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DataAddedOn).HasColumnType("date");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("Date_Of_Birth");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.DepartmentRegistrationNo).HasMaxLength(50);
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(50)
                .HasColumnName("District_Code");
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(50)
                .HasColumnName("Division_Code");
            entity.Property(e => e.EmrRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Emr_Registration_No");
            entity.Property(e => e.FatherName)
                .HasMaxLength(50)
                .HasColumnName("Father_Name");
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.GuardianName).HasMaxLength(50);
            entity.Property(e => e.GuardianPhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.HealthFacilityCode)
                .HasMaxLength(50)
                .HasColumnName("Health_Facility_Code");
            entity.Property(e => e.HealthFacilityDistrict).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityDivision).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityTehsil).HasMaxLength(50);
            entity.Property(e => e.HouseNo).HasMaxLength(150);
            entity.Property(e => e.Latitude).HasMaxLength(250);
            entity.Property(e => e.Longitude).HasMaxLength(250);
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .HasColumnName("Marital_Status");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NoOfChildren).HasColumnName("No_Of_Children");
            entity.Property(e => e.Occupation).HasMaxLength(50);
            entity.Property(e => e.PatientReportingDate)
                .HasColumnType("datetime")
                .HasColumnName("Patient_Reporting_Date");
            entity.Property(e => e.PatientSource)
                .HasMaxLength(50)
                .HasColumnName("Patient_Source");
            entity.Property(e => e.PatientType).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasColumnName("Phone_Number");
            entity.Property(e => e.ProgramId).HasColumnName("Program_Id");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.StatusUpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Status_Updated_Date");
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .HasColumnName("Tehsil_Code");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
        });

        modelBuilder.Entity<PatientBackup>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PatientBackup");

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Cnic).HasMaxLength(30);
            entity.Property(e => e.CnicGuardianRelation)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Guardian_Relation");
            entity.Property(e => e.CnicType)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Type");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(500)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("Date_Of_Birth");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(150)
                .HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.DepartmentRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Department_Registration_No");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(50)
                .HasColumnName("District_Code");
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(50)
                .HasColumnName("Division_Code");
            entity.Property(e => e.EmrRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Emr_Registration_No");
            entity.Property(e => e.FatherName)
                .HasMaxLength(50)
                .HasColumnName("Father_Name");
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.HealthFacilityCode)
                .HasMaxLength(50)
                .HasColumnName("Health_Facility_Code");
            entity.Property(e => e.HouseNo).HasMaxLength(150);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .HasColumnName("Marital_Status");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NoOfChildren).HasColumnName("No_Of_Children");
            entity.Property(e => e.Occupation).HasMaxLength(50);
            entity.Property(e => e.PatientReportingDate)
                .HasColumnType("datetime")
                .HasColumnName("Patient_Reporting_Date");
            entity.Property(e => e.PatientSource)
                .HasMaxLength(50)
                .HasColumnName("Patient_Source");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.ProgramId).HasColumnName("Program_Id");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .HasColumnName("Tehsil_Code");
            entity.Property(e => e.Town).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(150)
                .HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
        });

        modelBuilder.Entity<PatientBackup2>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PatientBackup2");

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Cnic).HasMaxLength(30);
            entity.Property(e => e.CnicGuardianRelation)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Guardian_Relation");
            entity.Property(e => e.CnicType)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Type");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(500)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("Date_Of_Birth");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(150)
                .HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.DepartmentRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Department_Registration_No");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(50)
                .HasColumnName("District_Code");
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(50)
                .HasColumnName("Division_Code");
            entity.Property(e => e.EmrRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Emr_Registration_No");
            entity.Property(e => e.FatherName)
                .HasMaxLength(50)
                .HasColumnName("Father_Name");
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.HealthFacilityCode)
                .HasMaxLength(50)
                .HasColumnName("Health_Facility_Code");
            entity.Property(e => e.HouseNo).HasMaxLength(150);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .HasColumnName("Marital_Status");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NoOfChildren).HasColumnName("No_Of_Children");
            entity.Property(e => e.Occupation).HasMaxLength(50);
            entity.Property(e => e.PatientReportingDate)
                .HasColumnType("datetime")
                .HasColumnName("Patient_Reporting_Date");
            entity.Property(e => e.PatientSource)
                .HasMaxLength(50)
                .HasColumnName("Patient_Source");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.ProgramId).HasColumnName("Program_Id");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .HasColumnName("Tehsil_Code");
            entity.Property(e => e.Town).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(150)
                .HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
        });

        modelBuilder.Entity<PatientBackup3>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PatientBackup3");

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Cnic).HasMaxLength(30);
            entity.Property(e => e.CnicGuardianRelation)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Guardian_Relation");
            entity.Property(e => e.CnicType)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Type");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(500)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("Date_Of_Birth");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(150)
                .HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.DepartmentRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Department_Registration_No");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(50)
                .HasColumnName("District_Code");
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(50)
                .HasColumnName("Division_Code");
            entity.Property(e => e.EmrRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Emr_Registration_No");
            entity.Property(e => e.FatherName)
                .HasMaxLength(50)
                .HasColumnName("Father_Name");
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.HealthFacilityCode)
                .HasMaxLength(50)
                .HasColumnName("Health_Facility_Code");
            entity.Property(e => e.HouseNo).HasMaxLength(150);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .HasColumnName("Marital_Status");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NoOfChildren).HasColumnName("No_Of_Children");
            entity.Property(e => e.Occupation).HasMaxLength(50);
            entity.Property(e => e.PatientReportingDate)
                .HasColumnType("datetime")
                .HasColumnName("Patient_Reporting_Date");
            entity.Property(e => e.PatientSource)
                .HasMaxLength(50)
                .HasColumnName("Patient_Source");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.ProgramId).HasColumnName("Program_Id");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .HasColumnName("Tehsil_Code");
            entity.Property(e => e.Town).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(150)
                .HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
        });

        modelBuilder.Entity<PatientBackup4>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PatientBackup4");

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CnicGuardianRelation)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Guardian_Relation");
            entity.Property(e => e.CnicType)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Type");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("Date_Of_Birth");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(150)
                .HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.DepartmentRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Department_Registration_No");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(50)
                .HasColumnName("District_Code");
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(50)
                .HasColumnName("Division_Code");
            entity.Property(e => e.EmrRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Emr_Registration_No");
            entity.Property(e => e.FatherName)
                .HasMaxLength(50)
                .HasColumnName("Father_Name");
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.GuardianName).HasMaxLength(50);
            entity.Property(e => e.GuardianPhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.HealthFacilityCode)
                .HasMaxLength(50)
                .HasColumnName("Health_Facility_Code");
            entity.Property(e => e.HouseNo).HasMaxLength(150);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .HasColumnName("Marital_Status");
            entity.Property(e => e.NoOfChildren).HasColumnName("No_Of_Children");
            entity.Property(e => e.Occupation).HasMaxLength(50);
            entity.Property(e => e.PatientReportingDate)
                .HasColumnType("datetime")
                .HasColumnName("Patient_Reporting_Date");
            entity.Property(e => e.PatientSource)
                .HasMaxLength(50)
                .HasColumnName("Patient_Source");
            entity.Property(e => e.PhoneNumber).HasColumnName("Phone_Number");
            entity.Property(e => e.ProgramId).HasColumnName("Program_Id");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .HasColumnName("Tehsil_Code");
            entity.Property(e => e.Town).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(150)
                .HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
        });

        modelBuilder.Entity<PatientBackup5>(entity =>
        {
            entity.ToTable("PatientBackup5");

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CnicGuardianRelation)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Guardian_Relation");
            entity.Property(e => e.CnicType)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Type");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("Date_Of_Birth");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(150)
                .HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.DepartmentRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Department_Registration_No");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(50)
                .HasColumnName("District_Code");
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(50)
                .HasColumnName("Division_Code");
            entity.Property(e => e.EmrRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Emr_Registration_No");
            entity.Property(e => e.FatherName)
                .HasMaxLength(50)
                .HasColumnName("Father_Name");
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.GuardianName).HasMaxLength(50);
            entity.Property(e => e.GuardianPhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.HealthFacilityCode)
                .HasMaxLength(50)
                .HasColumnName("Health_Facility_Code");
            entity.Property(e => e.HouseNo).HasMaxLength(150);
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .HasColumnName("Marital_Status");
            entity.Property(e => e.NoOfChildren).HasColumnName("No_Of_Children");
            entity.Property(e => e.Occupation).HasMaxLength(50);
            entity.Property(e => e.PatientReportingDate)
                .HasColumnType("datetime")
                .HasColumnName("Patient_Reporting_Date");
            entity.Property(e => e.PatientSource)
                .HasMaxLength(50)
                .HasColumnName("Patient_Source");
            entity.Property(e => e.PhoneNumber).HasColumnName("Phone_Number");
            entity.Property(e => e.ProgramId).HasColumnName("Program_Id");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .HasColumnName("Tehsil_Code");
            entity.Property(e => e.Town).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(150)
                .HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
        });

        modelBuilder.Entity<PatientBackup6>(entity =>
        {
            entity.ToTable("PatientBackup6");

            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CnicGuardianRelation)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Guardian_Relation");
            entity.Property(e => e.CnicType)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Type");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(500)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("Date_Of_Birth");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(150)
                .HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.DepartmentRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Department_Registration_No");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(50)
                .HasColumnName("District_Code");
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(50)
                .HasColumnName("Division_Code");
            entity.Property(e => e.EmrRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Emr_Registration_No");
            entity.Property(e => e.FatherName)
                .HasMaxLength(50)
                .HasColumnName("Father_Name");
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.GuardianName).HasMaxLength(50);
            entity.Property(e => e.GuardianPhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.HealthFacilityCode)
                .HasMaxLength(50)
                .HasColumnName("Health_Facility_Code");
            entity.Property(e => e.HealthFacilityDistrict).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityDivision).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityName).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityTehsil).HasMaxLength(50);
            entity.Property(e => e.HouseNo).HasMaxLength(150);
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .HasColumnName("Marital_Status");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NoOfChildren).HasColumnName("No_Of_Children");
            entity.Property(e => e.Occupation).HasMaxLength(50);
            entity.Property(e => e.PatientReportingDate)
                .HasColumnType("datetime")
                .HasColumnName("Patient_Reporting_Date");
            entity.Property(e => e.PatientSource)
                .HasMaxLength(50)
                .HasColumnName("Patient_Source");
            entity.Property(e => e.PatientType).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasColumnName("Phone_Number");
            entity.Property(e => e.ProgramId).HasColumnName("Program_Id");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .HasColumnName("Tehsil_Code");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(150)
                .HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");

            entity.HasOne(d => d.Program).WithMany(p => p.PatientBackup6s)
                .HasForeignKey(d => d.ProgramId)
                .HasConstraintName("FK_Patient_Program1");

            entity.HasOne(d => d.UnionCouncil).WithMany(p => p.PatientBackup6s)
                .HasForeignKey(d => d.UnionCouncilId)
                .HasConstraintName("FK_Patient_AppLocationsUC1");
        });

        modelBuilder.Entity<PatientBackup7>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PatientBackup7");

            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CnicGuardianRelation)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Guardian_Relation");
            entity.Property(e => e.CnicType)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Type");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(500)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("Date_Of_Birth");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(150)
                .HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.DepartmentRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Department_Registration_No");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(50)
                .HasColumnName("District_Code");
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(50)
                .HasColumnName("Division_Code");
            entity.Property(e => e.EmrRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Emr_Registration_No");
            entity.Property(e => e.FatherName)
                .HasMaxLength(50)
                .HasColumnName("Father_Name");
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.GuardianName).HasMaxLength(50);
            entity.Property(e => e.GuardianPhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.HealthFacilityCode)
                .HasMaxLength(50)
                .HasColumnName("Health_Facility_Code");
            entity.Property(e => e.HealthFacilityDistrict).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityDivision).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityName).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityTehsil).HasMaxLength(50);
            entity.Property(e => e.HouseNo).HasMaxLength(150);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .HasColumnName("Marital_Status");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NoOfChildren).HasColumnName("No_Of_Children");
            entity.Property(e => e.Occupation).HasMaxLength(50);
            entity.Property(e => e.PatientReportingDate)
                .HasColumnType("datetime")
                .HasColumnName("Patient_Reporting_Date");
            entity.Property(e => e.PatientSource)
                .HasMaxLength(50)
                .HasColumnName("Patient_Source");
            entity.Property(e => e.PatientType).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasColumnName("Phone_Number");
            entity.Property(e => e.ProgramId).HasColumnName("Program_Id");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .HasColumnName("Tehsil_Code");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(150)
                .HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
        });

        modelBuilder.Entity<PatientContact>(entity =>
        {
            entity.HasIndex(e => e.PatientId, "X_PatientContacts");

            entity.Property(e => e.ActionTaken).HasMaxLength(50);
            entity.Property(e => e.Address).HasMaxLength(300);
            entity.Property(e => e.ChestXrayResult)
                .HasMaxLength(100)
                .HasColumnName("ChestXRayResult");
            entity.Property(e => e.Cnic).HasMaxLength(15);
            entity.Property(e => e.ContactNumber).HasMaxLength(15);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Diagnosis).HasMaxLength(20);
            entity.Property(e => e.Gender).HasMaxLength(20);
            entity.Property(e => e.GeneXpertResult)
                .HasMaxLength(100)
                .HasColumnName("GeneXPertResult");
            entity.Property(e => e.Igra)
                .HasMaxLength(30)
                .HasColumnName("IGRA");
            entity.Property(e => e.Montoux).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.OtherChestXrayResult)
                .HasMaxLength(200)
                .HasColumnName("OtherChestXRayResult");
            entity.Property(e => e.PatientId).HasColumnName("Patient_Id");
            entity.Property(e => e.RelationWithPatient).HasMaxLength(100);
            entity.Property(e => e.Ssmresult)
                .HasMaxLength(20)
                .HasColumnName("SSMResult");
            entity.Property(e => e.Treatment).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientContacts)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_PatientContacts_Patient");
        });

        modelBuilder.Entity<PatientContactSymptom>(entity =>
        {
            entity.Property(e => e.Symptom).HasMaxLength(50);

            entity.HasOne(d => d.PatientContact).WithMany(p => p.PatientContactSymptoms)
                .HasForeignKey(d => d.PatientContactId)
                .HasConstraintName("FK_PatientContactSymptoms_PatientContacts");
        });

        modelBuilder.Entity<PatientContactsStatistic>(entity =>
        {
            entity.HasOne(d => d.Patient).WithMany(p => p.PatientContactsStatistics)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_PatientContactsStatistics_Patient");
        });

        modelBuilder.Entity<PatientHealthCard>(entity =>
        {
            entity.ToTable("PatientHealthCard");

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CnicGuardianRelation)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Guardian_Relation");
            entity.Property(e => e.CnicType)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Type");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DataAddedOn).HasColumnType("date");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("Date_Of_Birth");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.DepartmentRegistrationNo).HasMaxLength(50);
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(50)
                .HasColumnName("District_Code");
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(50)
                .HasColumnName("Division_Code");
            entity.Property(e => e.EmrRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Emr_Registration_No");
            entity.Property(e => e.FatherName)
                .HasMaxLength(50)
                .HasColumnName("Father_Name");
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.GuardianName).HasMaxLength(50);
            entity.Property(e => e.GuardianPhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.HealthFacilityCode)
                .HasMaxLength(50)
                .HasColumnName("Health_Facility_Code");
            entity.Property(e => e.HealthFacilityDistrict).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityDivision).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityName).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityTehsil).HasMaxLength(50);
            entity.Property(e => e.HouseNo).HasMaxLength(150);
            entity.Property(e => e.Latitude).HasMaxLength(250);
            entity.Property(e => e.Longitude).HasMaxLength(250);
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .HasColumnName("Marital_Status");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NoOfChildren).HasColumnName("No_Of_Children");
            entity.Property(e => e.Occupation).HasMaxLength(50);
            entity.Property(e => e.PatientReportingDate)
                .HasColumnType("datetime")
                .HasColumnName("Patient_Reporting_Date");
            entity.Property(e => e.PatientSource)
                .HasMaxLength(50)
                .HasColumnName("Patient_Source");
            entity.Property(e => e.PatientType).HasMaxLength(50);
            entity.Property(e => e.ProgramId).HasColumnName("Program_Id");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .HasColumnName("Tehsil_Code");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
        });

        modelBuilder.Entity<PatientMedicineAdjustment>(entity =>
        {
            entity.ToTable("PatientMedicineAdjustment");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DeletionDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.PatientMedicineHistory).WithMany(p => p.PatientMedicineAdjustments)
                .HasForeignKey(d => d.PatientMedicineHistoryId)
                .HasConstraintName("FK_PatientMedicineAdjustment_PatientMedicineHistory");
        });

        modelBuilder.Entity<PatientMedicineHistory>(entity =>
        {
            entity.ToTable("PatientMedicineHistory");

            entity.HasIndex(e => new { e.Type, e.PatientTreatmentProgressId }, "XI_PatientMedicineHistory");

            entity.HasIndex(e => new { e.PatientId, e.Month, e.Type }, "X_PatientMedicineHistory");

            entity.Property(e => e.BatchNo).HasMaxLength(50);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DeletionDate).HasColumnType("datetime");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.OtherMedicineName).HasMaxLength(100);
            entity.Property(e => e.PerDayDosage).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.FollowUp).WithMany(p => p.PatientMedicineHistories)
                .HasForeignKey(d => d.FollowUpId)
                .HasConstraintName("FK_PatientMedicineHistory_FollowUpTB");

            entity.HasOne(d => d.Medicine).WithMany(p => p.PatientMedicineHistories)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK_PatientMedicineHistory_Medicine");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientMedicineHistories)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_PatientMedicineHistory_Patient");

            entity.HasOne(d => d.PatientTreatmentProgress).WithMany(p => p.PatientMedicineHistories)
                .HasForeignKey(d => d.PatientTreatmentProgressId)
                .HasConstraintName("FK_PatientMedicineHistory_PatientTreatmentProgress");

            entity.HasOne(d => d.Visit).WithMany(p => p.PatientMedicineHistories)
                .HasForeignKey(d => d.VisitId)
                .HasConstraintName("FK_PatientMedicineHistory_Visit");
        });

        modelBuilder.Entity<PatientMedicineHistoryLog>(entity =>
        {
            entity.ToTable("PatientMedicineHistoryLog");

            entity.Property(e => e.BatchNo).HasMaxLength(50);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DeletionDate).HasColumnType("datetime");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.FollowUp).WithMany(p => p.PatientMedicineHistoryLogs)
                .HasForeignKey(d => d.FollowUpId)
                .HasConstraintName("FK_PatientMedicineHistoryLog_FollowUpTB");

            entity.HasOne(d => d.Medicine).WithMany(p => p.PatientMedicineHistoryLogs)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK_PatientMedicineHistoryLog_Medicine");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientMedicineHistoryLogs)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_PatientMedicineHistoryLog_Patient");
        });

        modelBuilder.Entity<PatientOutcome>(entity =>
        {
            entity.ToTable("PatientOutcome");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Outcome).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasMaxLength(50);

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientOutcomes)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_PatientOutcome_Patient");
        });

        modelBuilder.Entity<PatientSample>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PatientSampleDetails");

            entity.ToTable("PatientSample");

            entity.HasIndex(e => e.TestId, "XIII_PatientSample");

            entity.HasIndex(e => e.PatientId, "XIV_PatientSample");

            entity.HasIndex(e => new { e.TestId, e.PatientTreatmentProgressId }, "XI_PatientSample");

            entity.HasIndex(e => e.PatientTreatmentProgressId, "XVII_PatientSample");

            entity.HasIndex(e => e.TestId, "XV_PatientSample");

            entity.HasIndex(e => new { e.ReceivingStatus, e.TestId, e.Hfto, e.FromHf }, "X_PatientSample_Receiving_Status_Test_Id_HFTo_FromHF");

            entity.HasIndex(e => new { e.RecordStatus, e.TestId, e.CreationDate }, "X_PatientSample_Record_Status_Test_Id_Creation_Date");

            entity.HasIndex(e => new { e.Result, e.RecordStatus, e.TestId, e.CreationDate }, "X_PatientSample_Result_Record_Status_Test_Id_Creation_Date");

            entity.HasIndex(e => new { e.TestId, e.Result, e.CreationDate }, "X_PatientSample_Test_Id_Result_Creation_Date");

            entity.HasIndex(e => new { e.TestId, e.RifampicinResistance, e.Result, e.CreationDate }, "X_PatientSample_Test_Id_RifampicinResistance_Result_Creation_Date");

            entity.Property(e => e.Amikacin10)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Amikacin_1_0");
            entity.Property(e => e.AmikacinResistance).HasMaxLength(150);
            entity.Property(e => e.AssayTested).HasMaxLength(50);
            entity.Property(e => e.BarcodeNo)
                .HasMaxLength(50)
                .HasColumnName("Barcode_No");
            entity.Property(e => e.Bedaquiline10)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("Bedaquiline_1_0");
            entity.Property(e => e.CapreomycinResistance).HasMaxLength(150);
            entity.Property(e => e.Clofazimine10)
                .HasMaxLength(50)
                .HasColumnName("Clofazimine_1_0");
            entity.Property(e => e.Comments).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.Delamanid06)
                .HasMaxLength(50)
                .HasColumnName("Delamanid_0_6");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.Ethambutol50)
                .HasMaxLength(50)
                .HasColumnName("Ethambutol_5_0");
            entity.Property(e => e.EthionamideResistance).HasMaxLength(150);
            entity.Property(e => e.FluoroquinoloneResistance).HasMaxLength(150);
            entity.Property(e => e.Fluoroquinolones).HasMaxLength(50);
            entity.Property(e => e.FromHf)
                .HasMaxLength(50)
                .HasColumnName("FromHF");
            entity.Property(e => e.GenotypicDrug).HasMaxLength(50);
            entity.Property(e => e.GenotypicGene).HasMaxLength(50);
            entity.Property(e => e.GenotypicPrediction).HasMaxLength(50);
            entity.Property(e => e.Grading).HasMaxLength(20);
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.Hfto)
                .HasMaxLength(50)
                .HasColumnName("HFTo");
            entity.Property(e => e.Injectables).HasMaxLength(50);
            entity.Property(e => e.Isoniazid).HasMaxLength(50);
            entity.Property(e => e.Isoniazid01)
                .HasMaxLength(50)
                .HasColumnName("Isoniazid_0_1");
            entity.Property(e => e.IsoniazidResistance).HasMaxLength(150);
            entity.Property(e => e.KanamycinResistance).HasMaxLength(150);
            entity.Property(e => e.LabId).HasColumnName("Lab_Id");
            entity.Property(e => e.LabNo).HasMaxLength(50);
            entity.Property(e => e.LabStatus)
                .HasMaxLength(50)
                .HasColumnName("Lab_Status");
            entity.Property(e => e.LabStatusUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Lab_Status_Update_Date");
            entity.Property(e => e.LabStatusUpdatedBy).HasColumnName("Lab_Status_Updated_By");
            entity.Property(e => e.Levofloxacin10)
                .HasMaxLength(50)
                .HasColumnName("Levofloxacin_1_0");
            entity.Property(e => e.Linezolid10)
                .HasMaxLength(50)
                .HasColumnName("Linezolid_1_0");
            entity.Property(e => e.LowLevelKanamycin).HasMaxLength(50);
            entity.Property(e => e.Moxifloxacin10)
                .HasMaxLength(50)
                .HasColumnName("Moxifloxacin_1_0");
            entity.Property(e => e.OtherXray)
                .HasMaxLength(150)
                .HasColumnName("OtherXRay");
            entity.Property(e => e.PatientId).HasColumnName("Patient_Id");
            entity.Property(e => e.PatientReportingDate)
                .HasColumnType("datetime")
                .HasColumnName("Patient_Reporting_Date");
            entity.Property(e => e.Ppascore).HasColumnName("PPAScore");
            entity.Property(e => e.PrlLpa)
                .HasMaxLength(50)
                .HasColumnName("PrlLPA");
            entity.Property(e => e.ProgramId).HasColumnName("Program_Id");
            entity.Property(e => e.Pyrazinamide1000)
                .HasMaxLength(50)
                .HasColumnName("Pyrazinamide_100_0");
            entity.Property(e => e.ReasonForRejection)
                .HasMaxLength(50)
                .HasColumnName("Reason_for_Rejection");
            entity.Property(e => e.ReasonForTesting).HasMaxLength(50);
            entity.Property(e => e.ReceivedBy).HasColumnName("Received_By");
            entity.Property(e => e.ReceivingDate)
                .HasColumnType("datetime")
                .HasColumnName("Receiving_Date");
            entity.Property(e => e.ReceivingStatus)
                .HasMaxLength(50)
                .HasColumnName("Receiving_Status");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.RefferedBy)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.RefferedByOther)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Report).HasMaxLength(50);
            entity.Property(e => e.Result).HasMaxLength(50);
            entity.Property(e => e.ResultUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Result_Update_Date");
            entity.Property(e => e.ResultUpdatedBy).HasColumnName("Result_Updated_By");
            entity.Property(e => e.Rifampicin).HasMaxLength(50);
            entity.Property(e => e.Rifampicin05)
                .HasMaxLength(50)
                .HasColumnName("Rifampicin_0_5");
            entity.Property(e => e.Rifampicin10)
                .HasMaxLength(50)
                .HasColumnName("Rifampicin_1_0");
            entity.Property(e => e.RifampicinResistance).HasMaxLength(30);
            entity.Property(e => e.RrValueTb)
                .HasMaxLength(50)
                .HasColumnName("RR_Value_TB");
            entity.Property(e => e.SampleCollectionDate).HasColumnType("datetime");
            entity.Property(e => e.SampleDescription).HasColumnName("Sample_Description");
            entity.Property(e => e.SampleNo)
                .HasMaxLength(50)
                .HasColumnName("Sample_No");
            entity.Property(e => e.SamplePerformDate).HasColumnType("datetime");
            entity.Property(e => e.SampleReceptionStatus)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.SampleRecievingDate).HasColumnType("datetime");
            entity.Property(e => e.SampleTestingTechnique)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.SampleTransportMode).HasMaxLength(20);
            entity.Property(e => e.SampleTransportModeDescription).HasMaxLength(100);
            entity.Property(e => e.SamplingBy).HasColumnName("Sampling_By");
            entity.Property(e => e.SamplingDate)
                .HasColumnType("datetime")
                .HasColumnName("Sampling_Date");
            entity.Property(e => e.SpecimenType)
                .HasMaxLength(50)
                .HasColumnName("Specimen_Type");
            entity.Property(e => e.Streptomycin10)
                .HasMaxLength(50)
                .HasColumnName("Streptomycin_1_0");
            entity.Property(e => e.SuggestForTb).HasColumnName("SuggestForTB");
            entity.Property(e => e.TbCondition)
                .HasMaxLength(50)
                .HasColumnName("TB_Condition");
            entity.Property(e => e.TbbacterialLoad)
                .HasMaxLength(30)
                .HasColumnName("TBBacterialLoad");
            entity.Property(e => e.TestId).HasColumnName("Test_Id");
            entity.Property(e => e.TestName).HasMaxLength(50);
            entity.Property(e => e.TestPerformDate).HasColumnType("datetime");
            entity.Property(e => e.TestTechnique)
                .HasMaxLength(50)
                .HasColumnName("Test_Technique");
            entity.Property(e => e.TypeOfClinicalCase).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
            entity.Property(e => e.VisitId).HasColumnName("Visit_Id");
            entity.Property(e => e.VisualAppearance)
                .HasMaxLength(50)
                .HasColumnName("Visual_Appearance");
            entity.Property(e => e.XrayNo)
                .HasMaxLength(50)
                .HasColumnName("XRayNo");
        });

        modelBuilder.Entity<PatientSchedule>(entity =>
        {
            entity.ToTable("PatientSchedule");

            entity.Property(e => e.LabResults).HasMaxLength(400);
            entity.Property(e => e.LastVisitDate).HasColumnType("datetime");
            entity.Property(e => e.Medicine).HasMaxLength(300);
            entity.Property(e => e.NextVisitDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientSymptom>(entity =>
        {
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientTransferLog>(entity =>
        {
            entity.ToTable("PatientTransferLog");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.FromHf)
                .HasMaxLength(50)
                .HasColumnName("FromHF");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.ToDistrict).HasMaxLength(50);
            entity.Property(e => e.ToDivision).HasMaxLength(50);
            entity.Property(e => e.ToHf)
                .HasMaxLength(50)
                .HasColumnName("ToHF");
            entity.Property(e => e.ToTehsil).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientTransferLogs)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_PatientTransferLog_Patient");
        });

        modelBuilder.Entity<PatientTreatmentInfoTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PatientTreatmentInfo");

            entity.ToTable("PatientTreatmentInfoTB");

            entity.HasIndex(e => e.TbType, "IXI_PatientTreatmentInfoTB_Tb_Type");

            entity.HasIndex(e => e.CreationDate, "XII_PatientTreatmentInfoTB");

            entity.HasIndex(e => e.PatientId, "XI_PatientTreatmentInfoTB");

            entity.HasIndex(e => new { e.CreationDate, e.TbType }, "X_PatientTreatmentInfoTB");

            entity.Property(e => e.BloodPressureDiastolic).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.BloodPressureSystolic).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.ContactsUnderFive).HasColumnName("Contacts_Under_Five");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.EprelevantInvestigation)
                .HasMaxLength(500)
                .HasColumnName("EPRelevantInvestigation");
            entity.Property(e => e.Epresult)
                .HasMaxLength(250)
                .HasColumnName("EPResult");
            entity.Property(e => e.EpsiteOfDisease)
                .HasMaxLength(50)
                .HasColumnName("EPSiteOfDisease");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.HouseHoldContacts).HasColumnName("House_Hold_Contacts");
            entity.Property(e => e.IsXray).HasColumnName("IsXRAY");
            entity.Property(e => e.MicroscopyValue).HasMaxLength(50);
            entity.Property(e => e.OtherSiteOfDisease).HasMaxLength(50);
            entity.Property(e => e.PatientId).HasColumnName("Patient_Id");
            entity.Property(e => e.PatientReportingDate)
                .HasColumnType("datetime")
                .HasColumnName("Patient_Reporting_Date");
            entity.Property(e => e.PatientType)
                .HasMaxLength(50)
                .HasColumnName("Patient_Type");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.ReferredBy).HasMaxLength(50);
            entity.Property(e => e.RespiratoryRate).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.TbType)
                .HasMaxLength(50)
                .HasColumnName("Tb_Type");
            entity.Property(e => e.Tbcondition)
                .HasMaxLength(150)
                .HasColumnName("TBCondition");
            entity.Property(e => e.TbregistrationNo)
                .HasMaxLength(50)
                .HasColumnName("TBRegistrationNo");
            entity.Property(e => e.Temperature).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.TreatmentStartDate)
                .HasColumnType("datetime")
                .HasColumnName("Treatment_Start_Date");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
            entity.Property(e => e.VisitId).HasColumnName("Visit_Id");
            entity.Property(e => e.Weight).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.Xrayvalue)
                .HasMaxLength(200)
                .HasColumnName("XRAYValue");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientTreatmentInfoTbs)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_PatientTreatmentInfoTB_Patient");

            entity.HasOne(d => d.Visit).WithMany(p => p.PatientTreatmentInfoTbs)
                .HasForeignKey(d => d.VisitId)
                .HasConstraintName("FK_PatientTreatmentInfoTB_Visit");
        });

        modelBuilder.Entity<PatientTreatmentProgress>(entity =>
        {
            entity.ToTable("PatientTreatmentProgress");

            entity.HasIndex(e => e.PatientId, "X_PatientTreatmentProgress");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientVitalsTb>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PatientVitals");

            entity.ToTable("PatientVitalsTB");

            entity.HasIndex(e => new { e.CreationDate, e.Hivresult, e.RecordStatus }, "XIII_PatientVitalsTB");

            entity.HasIndex(e => new { e.Hivresult, e.CreationDate }, "XIV_PatientVitalsTB");

            entity.HasIndex(e => e.HivScreened, "XI_PatientVitalsTB");

            entity.HasIndex(e => new { e.PatientId, e.RecordStatus }, "XVI_PatientVitalsTB");

            entity.HasIndex(e => new { e.Hivresult, e.CreationDate, e.RecordStatus }, "X_PatientVitalsTB");

            entity.Property(e => e.ContactsUnderFive).HasColumnName("Contacts_Under_Five");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.HivScreened).HasColumnName("HIV_Screened");
            entity.Property(e => e.Hivresult)
                .HasMaxLength(50)
                .HasColumnName("HIVResult");
            entity.Property(e => e.HouseHoldContacts).HasColumnName("House_Hold_Contacts");
            entity.Property(e => e.PatientId).HasColumnName("Patient_Id");
            entity.Property(e => e.PatientReportingDate)
                .HasColumnType("datetime")
                .HasColumnName("Patient_Reporting_Date");
            entity.Property(e => e.PatientWillingForHivscreening).HasColumnName("PatientWillingForHIVScreening");
            entity.Property(e => e.Reason).HasMaxLength(50);
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.ReferToArt).HasColumnName("ReferToART");
            entity.Property(e => e.ReferredArt)
                .HasMaxLength(50)
                .HasColumnName("ReferredART");
            entity.Property(e => e.ReferredArtid).HasColumnName("ReferredARTId");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
            entity.Property(e => e.VisitId).HasColumnName("Visit_Id");
            entity.Property(e => e.Weight).HasColumnType("decimal(18, 5)");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientVitalsTbs)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_PatientVitalsTB_Patient");

            entity.HasOne(d => d.Visit).WithMany(p => p.PatientVitalsTbs)
                .HasForeignKey(d => d.VisitId)
                .HasConstraintName("FK_PatientVitalsTB_Visit");
        });

        modelBuilder.Entity<PatientsDatum>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("PatientsData");

            entity.Property(e => e.BarcodeNo)
                .HasMaxLength(50)
                .HasColumnName("Barcode_No");
            entity.Property(e => e.BloodPressureDiastolic).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.BloodPressureSystolic).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CnicGuardianRelation)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Guardian_Relation");
            entity.Property(e => e.CnicType)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Type");
            entity.Property(e => e.ContactsUnderFive).HasColumnName("Contacts_Under_Five");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DataAddedOn).HasColumnType("date");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("Date_Of_Birth");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.DepartmentRegistrationNo).HasMaxLength(50);
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(50)
                .HasColumnName("District_Code");
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(50)
                .HasColumnName("Division_Code");
            entity.Property(e => e.EmrRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Emr_Registration_No");
            entity.Property(e => e.EprelevantInvestigation)
                .HasMaxLength(500)
                .HasColumnName("EPRelevantInvestigation");
            entity.Property(e => e.Epresult)
                .HasMaxLength(250)
                .HasColumnName("EPResult");
            entity.Property(e => e.EpsiteOfDisease)
                .HasMaxLength(50)
                .HasColumnName("EPSiteOfDisease");
            entity.Property(e => e.Expr10).HasColumnType("datetime");
            entity.Property(e => e.Expr13).HasMaxLength(50);
            entity.Property(e => e.Expr18).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.Expr31).HasColumnType("datetime");
            entity.Property(e => e.Expr44).HasColumnType("datetime");
            entity.Property(e => e.FatherName)
                .HasMaxLength(50)
                .HasColumnName("Father_Name");
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Grading).HasMaxLength(20);
            entity.Property(e => e.GuardianName).HasMaxLength(50);
            entity.Property(e => e.GuardianPhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.HealthFacilityCode)
                .HasMaxLength(50)
                .HasColumnName("Health_Facility_Code");
            entity.Property(e => e.HealthFacilityDistrict).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityDivision).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityName).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityTehsil).HasMaxLength(50);
            entity.Property(e => e.HivScreened).HasColumnName("HIV_Screened");
            entity.Property(e => e.Hivresult)
                .HasMaxLength(50)
                .HasColumnName("HIVResult");
            entity.Property(e => e.HouseHoldContacts).HasColumnName("House_Hold_Contacts");
            entity.Property(e => e.HouseNo).HasMaxLength(150);
            entity.Property(e => e.IsXray).HasColumnName("IsXRAY");
            entity.Property(e => e.LabId).HasColumnName("Lab_Id");
            entity.Property(e => e.LabNo).HasMaxLength(50);
            entity.Property(e => e.LabStatus)
                .HasMaxLength(50)
                .HasColumnName("Lab_Status");
            entity.Property(e => e.LabStatusUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Lab_Status_Update_Date");
            entity.Property(e => e.LabStatusUpdatedBy).HasColumnName("Lab_Status_Updated_By");
            entity.Property(e => e.Latitude).HasMaxLength(250);
            entity.Property(e => e.Longitude).HasMaxLength(250);
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .HasColumnName("Marital_Status");
            entity.Property(e => e.MicroscopyValue).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NoOfChildren).HasColumnName("No_Of_Children");
            entity.Property(e => e.Occupation).HasMaxLength(50);
            entity.Property(e => e.OtherComorbidity)
                .HasMaxLength(50)
                .HasColumnName("otherComorbidity");
            entity.Property(e => e.OtherSiteOfDisease).HasMaxLength(50);
            entity.Property(e => e.OtherXray)
                .HasMaxLength(150)
                .HasColumnName("OtherXRay");
            entity.Property(e => e.PatientReportingDate)
                .HasColumnType("datetime")
                .HasColumnName("Patient_Reporting_Date");
            entity.Property(e => e.PatientSource)
                .HasMaxLength(50)
                .HasColumnName("Patient_Source");
            entity.Property(e => e.PatientType).HasMaxLength(50);
            entity.Property(e => e.PatientType1)
                .HasMaxLength(50)
                .HasColumnName("Patient_Type");
            entity.Property(e => e.PatientWillingForHivscreening).HasColumnName("PatientWillingForHIVScreening");
            entity.Property(e => e.PhoneNumber).HasColumnName("Phone_Number");
            entity.Property(e => e.Ppascore).HasColumnName("PPAScore");
            entity.Property(e => e.ProgramId).HasColumnName("Program_Id");
            entity.Property(e => e.Reason).HasMaxLength(50);
            entity.Property(e => e.ReasonForRejection)
                .HasMaxLength(50)
                .HasColumnName("Reason_for_Rejection");
            entity.Property(e => e.ReceivedBy).HasColumnName("Received_By");
            entity.Property(e => e.ReceivingDate)
                .HasColumnType("datetime")
                .HasColumnName("Receiving_Date");
            entity.Property(e => e.ReceivingStatus)
                .HasMaxLength(50)
                .HasColumnName("Receiving_Status");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.ReferToArt).HasColumnName("ReferToART");
            entity.Property(e => e.ReferredArt)
                .HasMaxLength(50)
                .HasColumnName("ReferredART");
            entity.Property(e => e.ReferredArtid).HasColumnName("ReferredARTId");
            entity.Property(e => e.ReferredBy).HasMaxLength(50);
            entity.Property(e => e.Report).HasMaxLength(50);
            entity.Property(e => e.RespiratoryRate).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.Result).HasMaxLength(50);
            entity.Property(e => e.ResultUpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("Result_Update_Date");
            entity.Property(e => e.ResultUpdatedBy).HasColumnName("Result_Updated_By");
            entity.Property(e => e.RifampicinResistance).HasMaxLength(30);
            entity.Property(e => e.RrValueTb)
                .HasMaxLength(50)
                .HasColumnName("RR_Value_TB");
            entity.Property(e => e.SampleCollectionDate).HasColumnType("datetime");
            entity.Property(e => e.SampleDescription).HasColumnName("Sample_Description");
            entity.Property(e => e.SampleNo)
                .HasMaxLength(50)
                .HasColumnName("Sample_No");
            entity.Property(e => e.SamplePerformDate).HasColumnType("datetime");
            entity.Property(e => e.SampleTransportMode).HasMaxLength(20);
            entity.Property(e => e.SampleTransportModeDescription).HasMaxLength(100);
            entity.Property(e => e.SamplingBy).HasColumnName("Sampling_By");
            entity.Property(e => e.SamplingDate)
                .HasColumnType("datetime")
                .HasColumnName("Sampling_Date");
            entity.Property(e => e.SpecimenType)
                .HasMaxLength(50)
                .HasColumnName("Specimen_Type");
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.SuggestForTb).HasColumnName("SuggestForTB");
            entity.Property(e => e.TbCondition1)
                .HasMaxLength(50)
                .HasColumnName("TB_Condition");
            entity.Property(e => e.TbType)
                .HasMaxLength(50)
                .HasColumnName("Tb_Type");
            entity.Property(e => e.TbbacterialLoad)
                .HasMaxLength(30)
                .HasColumnName("TBBacterialLoad");
            entity.Property(e => e.Tbcondition)
                .HasMaxLength(150)
                .HasColumnName("TBCondition");
            entity.Property(e => e.TbregistrationNo)
                .HasMaxLength(50)
                .HasColumnName("TBRegistrationNo");
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .HasColumnName("Tehsil_Code");
            entity.Property(e => e.Temperature).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.TestId).HasColumnName("Test_Id");
            entity.Property(e => e.TestName).HasMaxLength(50);
            entity.Property(e => e.TestTechnique)
                .HasMaxLength(50)
                .HasColumnName("Test_Technique");
            entity.Property(e => e.TreatmentStartDate)
                .HasColumnType("datetime")
                .HasColumnName("Treatment_Start_Date");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
            entity.Property(e => e.VisitId).HasColumnName("Visit_Id");
            entity.Property(e => e.VisualAppearance)
                .HasMaxLength(50)
                .HasColumnName("Visual_Appearance");
            entity.Property(e => e.Weight).HasColumnType("decimal(18, 5)");
            entity.Property(e => e.XrayNo)
                .HasMaxLength(50)
                .HasColumnName("XRayNo");
            entity.Property(e => e.Xrayvalue)
                .HasMaxLength(200)
                .HasColumnName("XRAYValue");
        });

        modelBuilder.Entity<PatientsListDatum>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("PatientsListData");

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.CnicGuardianRelation)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Guardian_Relation");
            entity.Property(e => e.CnicType)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Type");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DataAddedOn).HasColumnType("date");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("Date_Of_Birth");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.DepartmentRegistrationNo).HasMaxLength(50);
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(50)
                .HasColumnName("District_Code");
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(50)
                .HasColumnName("Division_Code");
            entity.Property(e => e.EmrRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Emr_Registration_No");
            entity.Property(e => e.FatherName)
                .HasMaxLength(50)
                .HasColumnName("Father_Name");
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.GuardianName).HasMaxLength(50);
            entity.Property(e => e.GuardianPhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.HealthFacilityCode)
                .HasMaxLength(50)
                .HasColumnName("Health_Facility_Code");
            entity.Property(e => e.HealthFacilityDistrict).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityDivision).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityName).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityTehsil).HasMaxLength(50);
            entity.Property(e => e.HouseNo).HasMaxLength(150);
            entity.Property(e => e.Latitude).HasMaxLength(250);
            entity.Property(e => e.Longitude).HasMaxLength(250);
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .HasColumnName("Marital_Status");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NoOfChildren).HasColumnName("No_Of_Children");
            entity.Property(e => e.Occupation).HasMaxLength(50);
            entity.Property(e => e.PatientReportingDate)
                .HasColumnType("datetime")
                .HasColumnName("Patient_Reporting_Date");
            entity.Property(e => e.PatientSource)
                .HasMaxLength(50)
                .HasColumnName("Patient_Source");
            entity.Property(e => e.PatientType).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasColumnName("Phone_Number");
            entity.Property(e => e.ProgramId).HasColumnName("Program_Id");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .HasColumnName("Tehsil_Code");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
        });

        modelBuilder.Entity<Ppaindicator>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PPAIndicators_1");

            entity.ToTable("PPAIndicators");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.DeleteBy).HasMaxLength(50);
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.IndicatorName).HasMaxLength(300);
            entity.Property(e => e.UpdateBy).HasMaxLength(300);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<PpaindicatorsOption>(entity =>
        {
            entity.ToTable("PPAIndicatorsOptions");

            entity.Property(e => e.OptionName).HasMaxLength(150);

            entity.HasOne(d => d.Indicator).WithMany(p => p.PpaindicatorsOptions)
                .HasForeignKey(d => d.IndicatorId)
                .HasConstraintName("FK_PPAIndicatorsOptions_PPAIndicators");
        });

        modelBuilder.Entity<PpaindicatorsTemp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PPAIndicators");

            entity.ToTable("PPAIndicatorsTemp");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedBy).HasMaxLength(50);
            entity.Property(e => e.IndicatorName).HasMaxLength(300);
            entity.Property(e => e.IndicatorOption).HasMaxLength(150);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
        });

        modelBuilder.Entity<Program>(entity =>
        {
            entity.ToTable("Program");

            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
        });

        modelBuilder.Entity<ProgramCenter>(entity =>
        {
            entity.ToTable("ProgramCenter");

            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.FacilityId).HasColumnName("Facility_Id");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.ProgramId).HasColumnName("Program_Id");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");

            entity.HasOne(d => d.Program).WithMany(p => p.ProgramCenters)
                .HasForeignKey(d => d.ProgramId)
                .HasConstraintName("FK_ProgramCenter_Program");
        });

        modelBuilder.Entity<ReportEnableStatus>(entity =>
        {
            entity.ToTable("ReportEnableStatus");

            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.EnableReportDateFrom).HasColumnType("datetime");
            entity.Property(e => e.EnableReportDateTo).HasColumnType("datetime");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.ReportName).HasMaxLength(500);
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
        });

        modelBuilder.Entity<RoleCategory>(entity =>
        {
            entity.ToTable("RoleCategory");

            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
        });

        modelBuilder.Entity<Sheet1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Sheet1$");

            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .HasColumnName("address");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Cnic)
                .HasMaxLength(255)
                .HasColumnName("cnic");
            entity.Property(e => e.CnicStatus)
                .HasMaxLength(255)
                .HasColumnName("cnic_status");
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(255)
                .HasColumnName("contact_number");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(500)
                .HasColumnName("created_by");
            entity.Property(e => e.CronDate)
                .HasMaxLength(255)
                .HasColumnName("cron_date");
            entity.Property(e => e.CultureRecieveDate)
                .HasMaxLength(255)
                .HasColumnName("culture_recieve_date");
            entity.Property(e => e.CurrentFollowupCount).HasColumnName("current_followup_count");
            entity.Property(e => e.CurrentMedCount).HasColumnName("current_med_count");
            entity.Property(e => e.DiabetesKnown)
                .HasMaxLength(255)
                .HasColumnName("diabetes_known");
            entity.Property(e => e.Districts).HasColumnName("districts");
            entity.Property(e => e.Division).HasColumnName("division");
            entity.Property(e => e.Gender).HasColumnName("gender");
            entity.Property(e => e.GuardianCnic)
                .HasMaxLength(255)
                .HasColumnName("guardian_cnic");
            entity.Property(e => e.GuardianName)
                .HasMaxLength(255)
                .HasColumnName("guardian_name");
            entity.Property(e => e.Hiv)
                .HasMaxLength(255)
                .HasColumnName("hiv");
            entity.Property(e => e.Hospital).HasColumnName("hospital");
            entity.Property(e => e.HouseHoldContacts).HasColumnName("house_hold_contacts");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsCulture)
                .HasMaxLength(255)
                .HasColumnName("is_culture");
            entity.Property(e => e.IsCultureRecieved)
                .HasMaxLength(255)
                .HasColumnName("is_culture_recieved");
            entity.Property(e => e.IsCxr)
                .HasMaxLength(255)
                .HasColumnName("is_cxr");
            entity.Property(e => e.IsFollowup)
                .HasMaxLength(255)
                .HasColumnName("is_followup");
            entity.Property(e => e.IsMedCatTypeChange)
                .HasMaxLength(255)
                .HasColumnName("is_med_cat_type_change");
            entity.Property(e => e.IsSsm)
                .HasMaxLength(255)
                .HasColumnName("is_ssm");
            entity.Property(e => e.IsXPert)
                .HasMaxLength(255)
                .HasColumnName("is_x_pert");
            entity.Property(e => e.LastFollowupDate)
                .HasColumnType("datetime")
                .HasColumnName("last_followup_date");
            entity.Property(e => e.MaritalStatus).HasColumnName("marital_status");
            entity.Property(e => e.MedCatType)
                .HasMaxLength(255)
                .HasColumnName("med_cat_type");
            entity.Property(e => e.MedCatTypeChangeDate)
                .HasMaxLength(255)
                .HasColumnName("med_cat_type_change_date");
            entity.Property(e => e.MedCount).HasColumnName("med_count");
            entity.Property(e => e.MedicineResult).HasColumnName("medicine_result");
            entity.Property(e => e.MrnNo)
                .HasMaxLength(255)
                .HasColumnName("mrn_no");
            entity.Property(e => e.Occupation)
                .HasMaxLength(255)
                .HasColumnName("occupation");
            entity.Property(e => e.PatientName)
                .HasMaxLength(255)
                .HasColumnName("patient_name");
            entity.Property(e => e.PatientStatus).HasColumnName("patient_status");
            entity.Property(e => e.PatientType)
                .HasMaxLength(255)
                .HasColumnName("patient_type");
            entity.Property(e => e.ReferredBy)
                .HasMaxLength(255)
                .HasColumnName("referred_by");
            entity.Property(e => e.Relation)
                .HasMaxLength(255)
                .HasColumnName("relation");
            entity.Property(e => e.RelationContact)
                .HasMaxLength(255)
                .HasColumnName("relation_contact");
            entity.Property(e => e.Result)
                .HasMaxLength(255)
                .HasColumnName("result");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
            entity.Property(e => e.Tehsil).HasColumnName("tehsil");
            entity.Property(e => e.TotalMedCount).HasColumnName("total_med_count");
            entity.Property(e => e.UnderFiveContacts).HasColumnName("under_five_contacts");
            entity.Property(e => e.UpdatedAt)
                .HasMaxLength(255)
                .HasColumnName("updated_at");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(150)
                .HasColumnName("updated_by");
            entity.Property(e => e.Weight).HasColumnName("weight");
            entity.Property(e => e.WeightResult).HasColumnName("weight_result");
        });

        modelBuilder.Entity<Sheet2>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Sheet2$");

            entity.Property(e => e.Cnic)
                .HasMaxLength(255)
                .HasColumnName("CNIC");
            entity.Property(e => e.CnicUpdated)
                .HasMaxLength(255)
                .HasColumnName("CNIC Updated");
            entity.Property(e => e.DateOfBirth)
                .HasMaxLength(255)
                .HasColumnName("Date of Birth");
            entity.Property(e => e.Gender).HasMaxLength(255);
            entity.Property(e => e.HealthFacilityDistrict)
                .HasMaxLength(255)
                .HasColumnName("Health Facility District");
            entity.Property(e => e.HealthFacilityDivision)
                .HasMaxLength(255)
                .HasColumnName("Health Facility Division");
            entity.Property(e => e.HealthFacilityName)
                .HasMaxLength(255)
                .HasColumnName("Health Facility Name");
            entity.Property(e => e.HealthFacilityTehsil)
                .HasMaxLength(255)
                .HasColumnName("Health Facility Tehsil");
            entity.Property(e => e.MedicineDeliveredDates)
                .HasColumnType("datetime")
                .HasColumnName("Medicine Delivered Dates");
            entity.Property(e => e.Mobile).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Patient).HasMaxLength(255);
            entity.Property(e => e.PatientAddress)
                .HasMaxLength(255)
                .HasColumnName("Patient Address");
            entity.Property(e => e.PatientDistrictName)
                .HasMaxLength(255)
                .HasColumnName("Patient District Name");
            entity.Property(e => e.PatientDivisionName)
                .HasMaxLength(255)
                .HasColumnName("Patient Division Name");
            entity.Property(e => e.PatientMobile)
                .HasMaxLength(255)
                .HasColumnName("Patient Mobile ");
            entity.Property(e => e.PatientMobileNo)
                .HasMaxLength(255)
                .HasColumnName("Patient Mobile no#");
            entity.Property(e => e.PatientRegistrationDate)
                .HasColumnType("datetime")
                .HasColumnName("Patient Registration Date");
            entity.Property(e => e.PatientTehsilName)
                .HasMaxLength(255)
                .HasColumnName("Patient Tehsil Name");
            entity.Property(e => e.PatientType)
                .HasMaxLength(255)
                .HasColumnName("Patient Type");
            entity.Property(e => e.Sr).HasColumnName("Sr#");
        });

        modelBuilder.Entity<Sheet3>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Sheet3$");

            entity.Property(e => e.PatientAddress)
                .IsUnicode(false)
                .HasColumnName("Patient Address");
            entity.Property(e => e.SrNo).HasColumnName("Sr No");
        });

        modelBuilder.Entity<Sheet4>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Sheet4$");

            entity.Property(e => e.F2).HasMaxLength(50);
            entity.Property(e => e.F3).HasMaxLength(50);
        });

        modelBuilder.Entity<Smslog>(entity =>
        {
            entity.ToTable("SMSLog");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ErrorId).HasMaxLength(50);
            entity.Property(e => e.Event).HasMaxLength(50);
            entity.Property(e => e.Message).HasMaxLength(300);
            entity.Property(e => e.MessageId).HasMaxLength(50);
            entity.Property(e => e.MessageTo).HasMaxLength(30);
            entity.Property(e => e.SessionId).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
        });

        modelBuilder.Entity<StockDetail>(entity =>
        {
            entity.ToTable("StockDetail");

            entity.Property(e => e.Action).HasMaxLength(50);
            entity.Property(e => e.AvailableQuantity).HasColumnName("Available_Quantity");
            entity.Property(e => e.Batch).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.ExpiryDate)
                .HasColumnType("datetime")
                .HasColumnName("Expiry_Date");
            entity.Property(e => e.FacilityCode).HasMaxLength(50);
            entity.Property(e => e.Geolvl)
                .HasMaxLength(50)
                .HasColumnName("GEOLVL");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.ManufacturingDate).HasColumnType("datetime");
            entity.Property(e => e.MedicineId).HasColumnName("Medicine_Id");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.StockMasterId).HasColumnName("Stock_Master_Id");
            entity.Property(e => e.Uomid).HasColumnName("UOMId");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");

            entity.HasOne(d => d.Medicine).WithMany(p => p.StockDetails)
                .HasForeignKey(d => d.MedicineId)
                .HasConstraintName("FK_StockDetail_StockMaster");

            entity.HasOne(d => d.Uom).WithMany(p => p.StockDetails)
                .HasForeignKey(d => d.Uomid)
                .HasConstraintName("FK_StockDetail_UnitofMeasurement");

            entity.HasOne(d => d.Voucher).WithMany(p => p.StockDetails)
                .HasForeignKey(d => d.VoucherId)
                .HasConstraintName("FK_StockDetail_Voucher");
        });

        modelBuilder.Entity<StockMaster>(entity =>
        {
            entity.ToTable("StockMaster");

            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.DriverName)
                .HasMaxLength(50)
                .HasColumnName("Driver_Name");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.IssuedTo)
                .HasMaxLength(50)
                .HasColumnName("Issued_To");
            entity.Property(e => e.RecievingLocation)
                .HasMaxLength(50)
                .HasColumnName("Recieving_Location");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.ReferenceNumber).HasColumnName("Reference_Number");
            entity.Property(e => e.Remarks).HasMaxLength(50);
            entity.Property(e => e.ResponsiblePerson)
                .HasMaxLength(50)
                .HasColumnName("Responsible_Person");
            entity.Property(e => e.RpCnic)
                .HasMaxLength(50)
                .HasColumnName("RP_CNIC");
            entity.Property(e => e.RpDesignation)
                .HasMaxLength(50)
                .HasColumnName("RP_Designation");
            entity.Property(e => e.RpPhone)
                .HasMaxLength(50)
                .HasColumnName("RP_Phone");
            entity.Property(e => e.SourceId).HasColumnName("Source_Id");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");
            entity.Property(e => e.TransactionDate)
                .HasColumnType("datetime")
                .HasColumnName("Transaction_Date");
            entity.Property(e => e.TransactionType)
                .HasMaxLength(50)
                .HasColumnName("Transaction_Type");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
            entity.Property(e => e.VehicleNumber)
                .HasMaxLength(50)
                .HasColumnName("Vehicle_Number");
            entity.Property(e => e.VoucherStatus).HasMaxLength(50);

            entity.HasOne(d => d.Source).WithMany(p => p.StockMasters)
                .HasForeignKey(d => d.SourceId)
                .HasConstraintName("FK_StockMaster_Supplier");

            entity.HasOne(d => d.Voucher).WithMany(p => p.StockMasters)
                .HasForeignKey(d => d.VoucherId)
                .HasConstraintName("FK_StockMaster_Voucher");
        });

        modelBuilder.Entity<StockSource>(entity =>
        {
            entity.ToTable("StockSource");

            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletionDate)
                .HasColumnType("datetime")
                .HasColumnName("Deletion_Date");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("Supplier");

            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.AlternateEmail)
                .HasMaxLength(100)
                .HasColumnName("Alternate_Email");
            entity.Property(e => e.AlternateMobNumber)
                .HasMaxLength(100)
                .HasColumnName("Alternate_Mob_Number");
            entity.Property(e => e.AlternatePhno)
                .HasMaxLength(100)
                .HasColumnName("Alternate_PHNO");
            entity.Property(e => e.ContactPerson)
                .HasMaxLength(100)
                .HasColumnName("Contact_Person");
            entity.Property(e => e.CpAddress)
                .HasMaxLength(500)
                .HasColumnName("CP_Address");
            entity.Property(e => e.CpAlternateEmail)
                .HasMaxLength(100)
                .HasColumnName("CP_Alternate_Email");
            entity.Property(e => e.CpAlternateMobNumber)
                .HasMaxLength(100)
                .HasColumnName("CP_Alternate_Mob_Number");
            entity.Property(e => e.CpAlternatePhno)
                .HasMaxLength(100)
                .HasColumnName("CP_Alternate_PHNO");
            entity.Property(e => e.CpEmail)
                .HasMaxLength(100)
                .HasColumnName("CP_Email");
            entity.Property(e => e.CpFax)
                .HasMaxLength(100)
                .HasColumnName("CP_Fax");
            entity.Property(e => e.CpGender)
                .HasMaxLength(100)
                .HasColumnName("CP_Gender");
            entity.Property(e => e.CpMobileNo)
                .HasMaxLength(100)
                .HasColumnName("CP_Mobile_No");
            entity.Property(e => e.CpPersonDesignation)
                .HasMaxLength(100)
                .HasColumnName("CP_Person_Designation");
            entity.Property(e => e.CpPersonDob)
                .HasColumnType("date")
                .HasColumnName("CP_Person_DOB");
            entity.Property(e => e.CpPhoneNumber)
                .HasMaxLength(100)
                .HasColumnName("CP_Phone_Number");
            entity.Property(e => e.CpUrl)
                .HasMaxLength(100)
                .HasColumnName("CP_URL");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EnableFlag)
                .HasMaxLength(100)
                .HasColumnName("Enable_Flag");
            entity.Property(e => e.Fax).HasMaxLength(100);
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.HealthFacilityId).HasColumnName("Health_Facility_Id");
            entity.Property(e => e.HfmisCode)
                .HasMaxLength(100)
                .HasColumnName("HFMIS_Code");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(100)
                .HasColumnName("Mobile_No");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(100)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.SortOrder).HasColumnName("Sort_Order");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
            entity.Property(e => e.WebsiteUrl)
                .HasMaxLength(100)
                .HasColumnName("Website_URL");
        });

        modelBuilder.Entity<Symptom>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<TbClinicsFeature>(entity =>
        {
            entity.Property(e => e.HfmisCode).HasMaxLength(50);
            entity.Property(e => e.SimpleTb).HasColumnName("SimpleTB");
        });

        modelBuilder.Entity<TblComorbiditiesList>(entity =>
        {
            entity.ToTable("TblComorbiditiesList");

            entity.Property(e => e.CreationBy).HasMaxLength(50);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<TblComorbidity>(entity =>
        {
            entity.ToTable("TblComorbidity");

            entity.Property(e => e.CreationBy).HasMaxLength(50);
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.OtherComorbidity)
                .HasMaxLength(50)
                .HasColumnName("otherComorbidity");
            entity.Property(e => e.RecordStatus).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Comorbidity).WithMany(p => p.TblComorbidities)
                .HasForeignKey(d => d.ComorbidityId)
                .HasConstraintName("FK_TblComorbidity_TblComorbiditiesList");

            entity.HasOne(d => d.Treatment).WithMany(p => p.TblComorbidities)
                .HasForeignKey(d => d.TreatmentId)
                .HasConstraintName("FK_TblComorbidity_PatientTreatmentInfoTB");
        });

        modelBuilder.Entity<TbmappedHealthFacility>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TBMappedHealthFacilities");

            entity.Property(e => e.CenterId).HasMaxLength(50);
        });

        modelBuilder.Entity<TransferHistory>(entity =>
        {
            entity.ToTable("TransferHistory");

            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.DestinationFacilityId).HasColumnName("Destination_Facility_Id");
            entity.Property(e => e.DestinationProgramId).HasColumnName("Destination_Program_Id");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.PatientId).HasColumnName("Patient_Id");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.SourceFacilityId).HasColumnName("Source_Facility_Id");
            entity.Property(e => e.SourceProgramId).HasColumnName("Source_Program_Id");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");

            entity.HasOne(d => d.DestinationProgram).WithMany(p => p.TransferHistories)
                .HasForeignKey(d => d.DestinationProgramId)
                .HasConstraintName("FK_TransferHistory_Program");

            entity.HasOne(d => d.Patient).WithMany(p => p.TransferHistories)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_TransferHistory_Patient");
        });

        modelBuilder.Entity<UnitofMeasurement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_UOM");

            entity.ToTable("UnitofMeasurement");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DeletionDate).HasColumnType("datetime");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.Uom)
                .HasMaxLength(50)
                .HasColumnName("UOM");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<VPatientDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_PatientDetail");

            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Cnic).HasMaxLength(30);
            entity.Property(e => e.CnicGuardianRelation)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Guardian_Relation");
            entity.Property(e => e.CnicType)
                .HasMaxLength(50)
                .HasColumnName("Cnic_Type");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.DateOfBirth)
                .HasColumnType("date")
                .HasColumnName("Date_Of_Birth");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(50)
                .HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.DepartmentRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Department_Registration_No");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(50)
                .HasColumnName("District_Code");
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(50)
                .HasColumnName("Division_Code");
            entity.Property(e => e.EmrRegistrationNo)
                .HasMaxLength(50)
                .HasColumnName("Emr_Registration_No");
            entity.Property(e => e.FatherName)
                .HasMaxLength(50)
                .HasColumnName("Father_Name");
            entity.Property(e => e.Gender).HasMaxLength(50);
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.HealthFacilityCode)
                .HasMaxLength(50)
                .HasColumnName("Health_Facility_Code");
            entity.Property(e => e.MaritalStatus)
                .HasMaxLength(50)
                .HasColumnName("Marital_Status");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.NoOfChildren).HasColumnName("No_Of_Children");
            entity.Property(e => e.Occupation).HasMaxLength(50);
            entity.Property(e => e.PatientReportingDate)
                .HasColumnType("datetime")
                .HasColumnName("Patient_Reporting_Date");
            entity.Property(e => e.PatientSource)
                .HasMaxLength(50)
                .HasColumnName("Patient_Source");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.ProgramId).HasColumnName("Program_Id");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.Street).HasMaxLength(50);
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .HasColumnName("Tehsil_Code");
            entity.Property(e => e.Town).HasMaxLength(50);
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
        });

        modelBuilder.Entity<VUserlist>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_Userlist");

            entity.Property(e => e.Cnic).HasColumnName("CNIC");
            entity.Property(e => e.DistrictName).HasColumnName("District Name");
            entity.Property(e => e.DivisionName).HasColumnName("Division Name");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Geolvl)
                .HasMaxLength(50)
                .HasColumnName("GEOLVL");
            entity.Property(e => e.HealthFacilityName).HasColumnName("Health Facility Name");
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.TehsilName).HasColumnName("Tehsil Name");
            entity.Property(e => e.UserName).HasMaxLength(256);
        });

        modelBuilder.Entity<VVisitDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_VisitDetail");

            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.CurrentStage)
                .HasMaxLength(50)
                .HasColumnName("Current_Stage");
            entity.Property(e => e.CurrentStatus)
                .HasMaxLength(50)
                .HasColumnName("Current_Status");
            entity.Property(e => e.DeletedBy)
                .HasMaxLength(50)
                .HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.FacilityCode)
                .HasMaxLength(50)
                .HasColumnName("Facility_Code");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.IsTransferred).HasColumnName("Is_Transferred");
            entity.Property(e => e.PatientId).HasColumnName("Patient_Id");
            entity.Property(e => e.ProgramId).HasColumnName("Program_Id");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.TransferredLogId).HasColumnName("Transferred_Log_Id");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
            entity.Property(e => e.VisitCount).HasColumnName("Visit_Count");
            entity.Property(e => e.VisitPurpose)
                .HasMaxLength(50)
                .HasColumnName("Visit_Purpose");
        });

        modelBuilder.Entity<ViewPatientListDashboard>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewPatientListDashboard");

            entity.Property(e => e.DistrictCode).HasMaxLength(50);
            entity.Property(e => e.DivisionCode).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityCode)
                .HasMaxLength(50)
                .HasColumnName("Health_FacilityCode");
            entity.Property(e => e.HealthFacilityDistrict).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityDivision).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityTehsil).HasMaxLength(50);
            entity.Property(e => e.MrNo).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PatientCreationDate).HasColumnType("datetime");
            entity.Property(e => e.PatientId).ValueGeneratedOnAdd();
            entity.Property(e => e.TehsilCode).HasMaxLength(50);
        });

        modelBuilder.Entity<ViewPatientSampleListDashboard>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewPatientSampleListDashboard");

            entity.Property(e => e.DistrictCode).HasMaxLength(50);
            entity.Property(e => e.DivisionCode).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityCode)
                .HasMaxLength(50)
                .HasColumnName("Health_FacilityCode");
            entity.Property(e => e.HealthFacilityDistrict).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityDivision).HasMaxLength(50);
            entity.Property(e => e.HealthFacilityTehsil).HasMaxLength(50);
            entity.Property(e => e.Hivresult)
                .HasMaxLength(50)
                .HasColumnName("HIVResult");
            entity.Property(e => e.MrNo).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PatientCreationDate).HasColumnType("datetime");
            entity.Property(e => e.Report).HasMaxLength(50);
            entity.Property(e => e.Result).HasMaxLength(50);
            entity.Property(e => e.SampleCreationDate).HasColumnType("datetime");
            entity.Property(e => e.TehsilCode).HasMaxLength(50);
            entity.Property(e => e.VitalCreationDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Visit>(entity =>
        {
            entity.ToTable("Visit");

            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("Creation_Date");
            entity.Property(e => e.CurrentStage)
                .HasMaxLength(50)
                .HasColumnName("Current_Stage");
            entity.Property(e => e.CurrentStatus)
                .HasMaxLength(50)
                .HasColumnName("Current_Status");
            entity.Property(e => e.DeletedBy).HasColumnName("Deleted_By");
            entity.Property(e => e.DeletedDate)
                .HasColumnType("datetime")
                .HasColumnName("Deleted_Date");
            entity.Property(e => e.FacilityCode)
                .HasMaxLength(50)
                .HasColumnName("Facility_Code");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.IsTransferred).HasColumnName("Is_Transferred");
            entity.Property(e => e.PatientId).HasColumnName("Patient_Id");
            entity.Property(e => e.ProgramId).HasColumnName("Program_Id");
            entity.Property(e => e.RecordStatus).HasColumnName("Record_Status");
            entity.Property(e => e.TransferredLogId).HasColumnName("Transferred_Log_Id");
            entity.Property(e => e.UpdatedBy).HasColumnName("Updated_By");
            entity.Property(e => e.UpdatedDate)
                .HasColumnType("datetime")
                .HasColumnName("Updated_Date");
            entity.Property(e => e.VisitCount).HasColumnName("Visit_Count");
            entity.Property(e => e.VisitPurpose)
                .HasMaxLength(50)
                .HasColumnName("Visit_Purpose");
        });

        modelBuilder.Entity<Voucher>(entity =>
        {
            entity.ToTable("Voucher");

            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.DeletionDate).HasColumnType("datetime");
            entity.Property(e => e.Guid).HasColumnName("GUID");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.VoucherType).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
