using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class MeasallContext : DbContext
{
    public MeasallContext()
    {
    }

    public MeasallContext(DbContextOptions<MeasallContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AppVersion> AppVersions { get; set; }

    public virtual DbSet<ApplicationHftype> ApplicationHftypes { get; set; }

    public virtual DbSet<ApplicationModule> ApplicationModules { get; set; }

    public virtual DbSet<ApplicationType> ApplicationTypes { get; set; }

    public virtual DbSet<AssignHf> AssignHfs { get; set; }

    public virtual DbSet<AssignMea> AssignMeas { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<CategoriesFlood> CategoriesFloods { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategoryHfType> CategoryHfTypes { get; set; }

    public virtual DbSet<CategoryShift> CategoryShifts { get; set; }

    public virtual DbSet<Designation> Designations { get; set; }

    public virtual DbSet<DesignationHfT> DesignationHfTs { get; set; }

    public virtual DbSet<DistrictView> DistrictViews { get; set; }

    public virtual DbSet<DivisionView> DivisionViews { get; set; }

    public virtual DbSet<Employess> Employesses { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<HealthFacilityType> HealthFacilityTypes { get; set; }

    public virtual DbSet<HfAmbulance> HfAmbulances { get; set; }

    public virtual DbSet<HfZone> HfZones { get; set; }

    public virtual DbSet<Hfactive> Hfactives { get; set; }

    public virtual DbSet<HfcategoryView> HfcategoryViews { get; set; }

    public virtual DbSet<HflistMode> HflistModes { get; set; }

    public virtual DbSet<Hfpackage> Hfpackages { get; set; }

    public virtual DbSet<Hfshift> Hfshifts { get; set; }

    public virtual DbSet<HftypeView> HftypeViews { get; set; }

    public virtual DbSet<Indicator> Indicators { get; set; }

    public virtual DbSet<IndicatorHealthFacility> IndicatorHealthFacilities { get; set; }

    public virtual DbSet<IndicatorHf> IndicatorHfs { get; set; }

    public virtual DbSet<IndicatorOption> IndicatorOptions { get; set; }

    public virtual DbSet<IndicatorOptionsFlood> IndicatorOptionsFloods { get; set; }

    public virtual DbSet<IndicatorShift> IndicatorShifts { get; set; }

    public virtual DbSet<Indicators1> Indicators1s { get; set; }

    public virtual DbSet<Indicators29August23> Indicators29August23s { get; set; }

    public virtual DbSet<IndicatorsFlood> IndicatorsFloods { get; set; }

    public virtual DbSet<IndicatorsRecov> IndicatorsRecovs { get; set; }

    public virtual DbSet<IndicatorsRecov1> IndicatorsRecov1s { get; set; }

    public virtual DbSet<InputType> InputTypes { get; set; }

    public virtual DbSet<IntegratedRhc> IntegratedRhcs { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Location3sep2022> Location3sep2022s { get; set; }

    public virtual DbSet<LocationViewInActiveRemark> LocationViewInActiveRemarks { get; set; }

    public virtual DbSet<MapHf> MapHfs { get; set; }

    public virtual DbSet<MapReportSeq> MapReportSeqs { get; set; }

    public virtual DbSet<MapReportingSequence> MapReportingSequences { get; set; }

    public virtual DbSet<MeasBundal> MeasBundals { get; set; }

    public virtual DbSet<MeasPakage> MeasPakages { get; set; }

    public virtual DbSet<MedicalCamp> MedicalCamps { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<ModuleFlood> ModuleFloods { get; set; }

    public virtual DbSet<MonitoringAttachment> MonitoringAttachments { get; set; }

    public virtual DbSet<MonitoringAttendance> MonitoringAttendances { get; set; }

    public virtual DbSet<MonitoringChild> MonitoringChildren { get; set; }

    public virtual DbSet<MonitoringChildFlood> MonitoringChildFloods { get; set; }

    public virtual DbSet<MonitoringFeedback> MonitoringFeedbacks { get; set; }

    public virtual DbSet<MonitoringMaster> MonitoringMasters { get; set; }

    public virtual DbSet<MonitoringMasterDuplicate> MonitoringMasterDuplicates { get; set; }

    public virtual DbSet<MonitoringMasterFlood> MonitoringMasterFloods { get; set; }

    public virtual DbSet<OptionType> OptionTypes { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<ProfileType> ProfileTypes { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<RepeatVisitPercentage> RepeatVisitPercentages { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Seq> Seqs { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<SubCategoriesFlood> SubCategoriesFloods { get; set; }

    public virtual DbSet<SubCategory> SubCategories { get; set; }

    public virtual DbSet<TblAmbulance> TblAmbulances { get; set; }

    public virtual DbSet<TblDistrictUsermap> TblDistrictUsermaps { get; set; }

    public virtual DbSet<TblHfAmbulance> TblHfAmbulances { get; set; }

    public virtual DbSet<TblHfAmbulance12sep23> TblHfAmbulance12sep23s { get; set; }

    public virtual DbSet<TblIndicatorMap> TblIndicatorMaps { get; set; }

    public virtual DbSet<TblLocationNew> TblLocationNews { get; set; }

    public virtual DbSet<TblLocationNew1> TblLocationNew1s { get; set; }

    public virtual DbSet<TblLocationNew2> TblLocationNew2s { get; set; }

    public virtual DbSet<TblShcuserMap> TblShcuserMaps { get; set; }

    public virtual DbSet<TehsilView> TehsilViews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserLocation> UserLocations { get; set; }

    public virtual DbSet<UserLog> UserLogs { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<UserRoles1> UserRoles1s { get; set; }

    public virtual DbSet<UserRoles2> UserRoles2s { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    public virtual DbSet<UserVisit> UserVisits { get; set; }

    public virtual DbSet<UserVisits10822> UserVisits10822s { get; set; }

    public virtual DbSet<Vacancy> Vacancies { get; set; }

    public virtual DbSet<VaccanciesMaster> VaccanciesMasters { get; set; }

    public virtual DbSet<VaccancyMaster> VaccancyMasters { get; set; }

    public virtual DbSet<ViewHealthFacility> ViewHealthFacilities { get; set; }

    public virtual DbSet<ViewLocation> ViewLocations { get; set; }

    public virtual DbSet<ViewLocationMea> ViewLocationMeas { get; set; }

    public virtual DbSet<ViewMeasVisit> ViewMeasVisits { get; set; }

    public virtual DbSet<ViewMeasVisitDetail> ViewMeasVisitDetails { get; set; }

    public virtual DbSet<ViewTodayVisit> ViewTodayVisits { get; set; }

    public virtual DbSet<ViewTodaysVisit> ViewTodaysVisits { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

    public virtual DbSet<Zone> Zones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    { optionsBuilder.UseSqlServer("Server=172.16.10.31;Database=MEAsall;Persist Security Info=False;User Id=tayyba;Password=asd@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=400;").EnableSensitiveDataLogging(); ; 
    }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AppVersion>(entity =>
        {
            entity.ToTable("AppVersion");

            entity.Property(e => e.AppVersionId).HasColumnName("AppVersionID");
            entity.Property(e => e.ChecklistMacs).HasColumnName("ChecklistMACS");
            entity.Property(e => e.Version).HasMaxLength(50);
        });

        modelBuilder.Entity<ApplicationHftype>(entity =>
        {
            entity.ToTable("ApplicationHFType");

            entity.HasOne(d => d.Application).WithMany(p => p.ApplicationHftypes)
                .HasForeignKey(d => d.ApplicationId)
                .HasConstraintName("FK_ApplicationHFType_ApplicationType");

            entity.HasOne(d => d.HfType).WithMany(p => p.ApplicationHftypes)
                .HasForeignKey(d => d.HfTypeId)
                .HasConstraintName("FK_ApplicationHFType_HealthFacilityType");
        });

        modelBuilder.Entity<ApplicationModule>(entity =>
        {
            entity.HasOne(d => d.ApplicationType).WithMany(p => p.ApplicationModules)
                .HasForeignKey(d => d.ApplicationTypeId)
                .HasConstraintName("FK_ApplicationModules_ApplicationType");

            entity.HasOne(d => d.Module).WithMany(p => p.ApplicationModules)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK_ApplicationModules_Modules");
        });

        modelBuilder.Entity<ApplicationType>(entity =>
        {
            entity.ToTable("ApplicationType");

            entity.Property(e => e.ApplicationTypeName).HasMaxLength(500);
        });

        modelBuilder.Entity<AssignHf>(entity =>
        {
            entity.ToTable("AssignHF");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Hfid).HasColumnName("HFId");
            entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<AssignMea>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.ToTable("AuditLog");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Host)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.HostNameType)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.IdnHost)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.IsModelStateValid)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.JsonBody).HasColumnType("text");
            entity.Property(e => e.LocalPath)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Method)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ModelStateError).IsUnicode(false);
            entity.Property(e => e.PathAndQuery).IsUnicode(false);
            entity.Property(e => e.RequestTime).HasColumnType("datetime");
            entity.Property(e => e.RequestUrl)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("RequestURL");
            entity.Property(e => e.ResponseData).IsUnicode(false);
            entity.Property(e => e.ResponseTime).HasColumnType("datetime");
            entity.Property(e => e.Token).IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<CategoriesFlood>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Categories_Flood");

            entity.Property(e => e.CategoryId).ValueGeneratedOnAdd();
            entity.Property(e => e.CategoryName).HasMaxLength(500);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.CategoryName).HasMaxLength(500);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.ApplicationType).WithMany(p => p.Categories)
                .HasForeignKey(d => d.ApplicationTypeId)
                .HasConstraintName("FK_Categories_ApplicationType");

            entity.HasOne(d => d.Module).WithMany(p => p.Categories)
                .HasForeignKey(d => d.ModuleId)
                .HasConstraintName("FK_Categories_Modules");
        });

        modelBuilder.Entity<CategoryHfType>(entity =>
        {
            entity.ToTable("CategoryHfType");

            entity.Property(e => e.CategoryHftypeId).HasColumnName("CategoryHFtypeId");
            entity.Property(e => e.HfTypeId).HasColumnName("hfTypeId");

            entity.HasOne(d => d.Category).WithMany(p => p.CategoryHfTypes)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_CategoryHfType_Categories");

            entity.HasOne(d => d.HfType).WithMany(p => p.CategoryHfTypes)
                .HasForeignKey(d => d.HfTypeId)
                .HasConstraintName("FK_CategoryHfType_HealthFacilityType");
        });

        modelBuilder.Entity<CategoryShift>(entity =>
        {
            entity.ToTable("CategoryShift");

            entity.HasOne(d => d.Category).WithMany(p => p.CategoryShifts)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_CategoryShift_Categories");

            entity.HasOne(d => d.Shift).WithMany(p => p.CategoryShifts)
                .HasForeignKey(d => d.ShiftId)
                .HasConstraintName("FK_CategoryShift_Shifts");
        });

        modelBuilder.Entity<Designation>(entity =>
        {
            entity.Property(e => e.DesignationName).HasMaxLength(250);
        });

        modelBuilder.Entity<DesignationHfT>(entity =>
        {
            entity.HasKey(e => e.DesignationHfTypeId);

            entity.ToTable("DesignationHfT");

            entity.HasOne(d => d.Designation).WithMany(p => p.DesignationHfTs)
                .HasForeignKey(d => d.DesignationId)
                .HasConstraintName("FK_DesignationHfT_Designations");

            entity.HasOne(d => d.HfType).WithMany(p => p.DesignationHfTs)
                .HasForeignKey(d => d.HfTypeId)
                .HasConstraintName("FK_DesignationHfT_HealthFacilityType");
        });

        modelBuilder.Entity<DistrictView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("DistrictView");

            entity.Property(e => e.CapitalTehsilCode).HasMaxLength(20);
        });

        modelBuilder.Entity<DivisionView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("DivisionView");
        });

        modelBuilder.Entity<Employess>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);

            entity.ToTable("Employess");

            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
            entity.Property(e => e.Cnic)
                .HasMaxLength(50)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.HfmisCode).HasMaxLength(500);
            entity.Property(e => e.MobileNo).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(500);
            entity.Property(e => e.Option).HasMaxLength(500);
            entity.Property(e => e.OriginalPosting).HasMaxLength(1500);
            entity.Property(e => e.OriginalPostingFacility).HasMaxLength(1500);
            entity.Property(e => e.Shift).HasMaxLength(50);
            entity.Property(e => e.SyncOn).HasColumnType("datetime");
            entity.Property(e => e.VacancyId).HasColumnName("VacancyID");
            entity.Property(e => e.VacancyTitle).HasMaxLength(500);

            entity.HasOne(d => d.Master).WithMany(p => p.Employesses)
                .HasForeignKey(d => d.MasterId)
                .HasConstraintName("FK_Employess_VaccancyMaster");
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.ToTable("ErrorLog");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.InnerException)
                .HasMaxLength(3000)
                .IsUnicode(false);
            entity.Property(e => e.Message)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Method)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Route)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.RouteBase)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StackTrace)
                .HasMaxLength(3000)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<HealthFacilityType>(entity =>
        {
            entity.HasKey(e => e.FacilityTypeId).HasName("PK_FacilityType");

            entity.ToTable("HealthFacilityType");

            entity.Property(e => e.FaciltyTypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<HfAmbulance>(entity =>
        {
            entity.ToTable("hf_ambulance");

            entity.Property(e => e.AmbulanceNo).HasMaxLength(100);
            entity.Property(e => e.Dhiscode)
                .HasMaxLength(100)
                .HasColumnName("DHISCode");
            entity.Property(e => e.HealthFacilityName).HasMaxLength(1000);
            entity.Property(e => e.Hfmiscode)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("hfmiscode");
            entity.Property(e => e.Lvl)
                .HasMaxLength(50)
                .HasColumnName("lvl");
            entity.Property(e => e.ModeName).HasMaxLength(100);
        });

        modelBuilder.Entity<HfZone>(entity =>
        {
            entity.HasIndex(e => new { e.HfId, e.IsActive }, "_dta_index_HfZones_27_599673184__K2_K4_3");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Zone).WithMany(p => p.HfZones)
                .HasForeignKey(d => d.ZoneId)
                .HasConstraintName("FK_HfZones_Zones");
        });

        modelBuilder.Entity<Hfactive>(entity =>
        {
            entity.ToTable("HFActive");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.InActiveTill).HasColumnType("datetime");
            entity.Property(e => e.Remarks).HasMaxLength(500);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<HfcategoryView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("HFCategoryView");
        });

        modelBuilder.Entity<HflistMode>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("HFListMode");

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
            entity.Property(e => e.ModeName).HasMaxLength(50);
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
            entity.Property(e => e.UrlImagePath).HasColumnName("urlImagePath");
            entity.Property(e => e.UsersId)
                .HasMaxLength(128)
                .HasColumnName("Users_Id");
        });

        modelBuilder.Entity<Hfpackage>(entity =>
        {
            entity.ToTable("HFPackage");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Hfid).HasColumnName("HFid");
            entity.Property(e => e.Hfname)
                .HasMaxLength(250)
                .HasColumnName("HFName");
            entity.Property(e => e.HftypeCode)
                .HasMaxLength(250)
                .HasColumnName("HFTypeCode");
            entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.PackName).HasMaxLength(250);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Hfshift>(entity =>
        {
            entity.ToTable("HFShifts");

            entity.Property(e => e.HfshiftId).HasColumnName("HFShiftId");
            entity.Property(e => e.HftypeId).HasColumnName("HFTypeId");

            entity.HasOne(d => d.Hftype).WithMany(p => p.Hfshifts)
                .HasForeignKey(d => d.HftypeId)
                .HasConstraintName("FK_HFShifts_HealthFacilityType");

            entity.HasOne(d => d.Shift).WithMany(p => p.Hfshifts)
                .HasForeignKey(d => d.ShiftId)
                .HasConstraintName("FK_HFShifts_Shifts");
        });

        modelBuilder.Entity<HftypeView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("HFTypeView");

            entity.Property(e => e.HfcatId).HasColumnName("HFCat_Id");
        });

        modelBuilder.Entity<Indicator>(entity =>
        {
            entity.HasIndex(e => new { e.IndicatorTypeId, e.IsActive, e.ModuleId, e.InputTypeId }, "IA_Indicators_IndicatorTypeId_IsActive_ModuleId_InputTypeId");

            entity.HasIndex(e => new { e.ParentIndicatorId, e.CategoryId, e.IsActive, e.ModuleId }, "IA_Indicators_ParentIndicatorId_CategoryId_IsActive_ModuleId");

            entity.HasIndex(e => new { e.ParentIndicatorId, e.CategoryId, e.SubCategoryId }, "IA_Indicators_ParentIndicatorId_CategoryId_SubCategoryId");

            entity.HasIndex(e => new { e.ParentIndicatorId, e.CategoryId, e.SubCategoryId }, "XIII_Indicators");

            entity.HasIndex(e => new { e.ParentIndicatorId, e.CategoryId, e.SubCategoryId, e.FormId, e.IsActive }, "XII_Indicators");

            entity.HasIndex(e => new { e.ParentIndicatorId, e.SubCategoryId, e.IsActive }, "XIV_Indicators");

            entity.HasIndex(e => new { e.ParentIndicatorId, e.SubCategoryId, e.FormId, e.IsActive }, "XI_Indicators");

            entity.HasIndex(e => new { e.ParentIndicatorId, e.CategoryId, e.SubCategoryId, e.IsActive }, "XVI_Indicators");

            entity.HasIndex(e => new { e.ParentIndicatorId, e.IsActive }, "XV_Indicators");

            entity.HasIndex(e => e.CategoryId, "X_Indicators");

            entity.HasIndex(e => new { e.IsActive, e.ModuleId }, "X_Indicators_IsActive_ModuleId");

            entity.HasIndex(e => new { e.IsActive, e.ModuleId, e.IndicatorTypeId }, "X_Indicators_IsActive_ModuleId_IndicatorTypeId");

            entity.HasIndex(e => e.ModuleId, "X_Indicators_ModuleId");

            entity.HasIndex(e => new { e.ParentIndicatorId, e.CategoryId, e.ApplicationTypeId, e.ModuleId }, "X_Indicators_ParentIndicatorId_CategoryId_ApplicationTypeId_ModuleId");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DefaultValue).HasMaxLength(50);
            entity.Property(e => e.FormId).HasMaxLength(10);
            entity.Property(e => e.Question).HasMaxLength(1000);
            entity.Property(e => e.ReportActive).HasColumnName("Report_Active");
            entity.Property(e => e.ShortQuestion).HasMaxLength(1000);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Indicators)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Indicators_Categories");

            entity.HasOne(d => d.IndicatorType).WithMany(p => p.Indicators)
                .HasForeignKey(d => d.IndicatorTypeId)
                .HasConstraintName("FK_Indicators_OptionTypes");

            entity.HasOne(d => d.InputType).WithMany(p => p.Indicators)
                .HasForeignKey(d => d.InputTypeId)
                .HasConstraintName("FK_Indicators_InputType");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.Indicators)
                .HasForeignKey(d => d.SubCategoryId)
                .HasConstraintName("FK_Indicators_SubCategories");
        });

        modelBuilder.Entity<IndicatorHealthFacility>(entity =>
        {
            entity.HasKey(e => e.IndicatorHfid);

            entity.ToTable("IndicatorHealthFacility");

            entity.Property(e => e.IndicatorHfid).HasColumnName("IndicatorHFId");
            entity.Property(e => e.Hfid).HasColumnName("HFId");

            entity.HasOne(d => d.Indicator).WithMany(p => p.IndicatorHealthFacilities)
                .HasForeignKey(d => d.IndicatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IndicatorHealthFacility_Indicators");
        });

        modelBuilder.Entity<IndicatorHf>(entity =>
        {
            entity.HasKey(e => e.InidcatorHfTypeId);

            entity.ToTable("IndicatorHFs");

            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.HfType).WithMany(p => p.IndicatorHfs)
                .HasForeignKey(d => d.HfTypeId)
                .HasConstraintName("FK_IndicatorHFs_HealthFacilityType");

            entity.HasOne(d => d.Indicator).WithMany(p => p.IndicatorHfs)
                .HasForeignKey(d => d.IndicatorId)
                .HasConstraintName("FK_IndicatorHFs_Indicators");
        });

        modelBuilder.Entity<IndicatorOption>(entity =>
        {
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DefaultValue).HasMaxLength(50);
            entity.Property(e => e.IsDeleted).HasDefaultValueSql("((0))");
            entity.Property(e => e.Label).HasMaxLength(1000);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Indicator).WithMany(p => p.IndicatorOptions)
                .HasForeignKey(d => d.IndicatorId)
                .HasConstraintName("FK_IndicatorOptions_Indicators");

            entity.HasOne(d => d.InputType).WithMany(p => p.IndicatorOptions)
                .HasForeignKey(d => d.InputTypeId)
                .HasConstraintName("FK_IndicatorOptions_InputType");

            entity.HasOne(d => d.Type).WithMany(p => p.IndicatorOptions)
                .HasForeignKey(d => d.TypeId)
                .HasConstraintName("FK_IndicatorOptions_OptionTypes");
        });

        modelBuilder.Entity<IndicatorOptionsFlood>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("IndicatorOptions_Flood");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DefaultValue).HasMaxLength(50);
            entity.Property(e => e.IndicatorOptionId).ValueGeneratedOnAdd();
            entity.Property(e => e.Label).HasMaxLength(1000);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<IndicatorShift>(entity =>
        {
            entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Indicator).WithMany(p => p.IndicatorShifts)
                .HasForeignKey(d => d.IndicatorId)
                .HasConstraintName("FK_IndicatorShifts_Indicators");

            entity.HasOne(d => d.Shift).WithMany(p => p.IndicatorShifts)
                .HasForeignKey(d => d.ShiftId)
                .HasConstraintName("FK_IndicatorShifts_Shifts");
        });

        modelBuilder.Entity<Indicators1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Indicators1");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DefaultValue).HasMaxLength(50);
            entity.Property(e => e.FormId).HasMaxLength(10);
            entity.Property(e => e.IndicatorId).ValueGeneratedOnAdd();
            entity.Property(e => e.Question).HasMaxLength(1000);
            entity.Property(e => e.ReportActive).HasColumnName("Report_Active");
            entity.Property(e => e.ShortQuestion).HasMaxLength(1000);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Indicators29August23>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Indicators29August23");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DefaultValue).HasMaxLength(50);
            entity.Property(e => e.FormId).HasMaxLength(10);
            entity.Property(e => e.IndicatorId).ValueGeneratedOnAdd();
            entity.Property(e => e.Question).HasMaxLength(1000);
            entity.Property(e => e.ReportActive).HasColumnName("Report_Active");
            entity.Property(e => e.ShortQuestion).HasMaxLength(1000);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<IndicatorsFlood>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Indicators_Flood");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DefaultValue).HasMaxLength(50);
            entity.Property(e => e.FormId).HasMaxLength(10);
            entity.Property(e => e.IndicatorId).ValueGeneratedOnAdd();
            entity.Property(e => e.Question).HasMaxLength(1000);
            entity.Property(e => e.ReportActive).HasColumnName("Report_Active");
            entity.Property(e => e.ShortQuestion).HasMaxLength(1000);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<IndicatorsRecov>(entity =>
        {
            entity.HasKey(e => e.IndicatorId);

            entity.ToTable("Indicators_recov");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DefaultValue).HasMaxLength(50);
            entity.Property(e => e.FormId).HasMaxLength(10);
            entity.Property(e => e.Question).HasMaxLength(1000);
            entity.Property(e => e.ReportActive).HasColumnName("Report_Active");
            entity.Property(e => e.ShortQuestion).HasMaxLength(1000);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.IndicatorsRecovs)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Indicators_recov_Categories");

            entity.HasOne(d => d.IndicatorType).WithMany(p => p.IndicatorsRecovs)
                .HasForeignKey(d => d.IndicatorTypeId)
                .HasConstraintName("FK_Indicators_recov_OptionTypes");

            entity.HasOne(d => d.InputType).WithMany(p => p.IndicatorsRecovs)
                .HasForeignKey(d => d.InputTypeId)
                .HasConstraintName("FK_Indicators_recov_InputType");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.IndicatorsRecovs)
                .HasForeignKey(d => d.SubCategoryId)
                .HasConstraintName("FK_Indicators_recov_SubCategories");
        });

        modelBuilder.Entity<IndicatorsRecov1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Indicators_recov1");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DefaultValue).HasMaxLength(50);
            entity.Property(e => e.FormId).HasMaxLength(10);
            entity.Property(e => e.IndicatorId).ValueGeneratedOnAdd();
            entity.Property(e => e.Question).HasMaxLength(1000);
            entity.Property(e => e.ReportActive).HasColumnName("Report_Active");
            entity.Property(e => e.ShortQuestion).HasMaxLength(1000);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<InputType>(entity =>
        {
            entity.ToTable("InputType");

            entity.Property(e => e.InputTypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<IntegratedRhc>(entity =>
        {
            entity.ToTable("integrated-rhc");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Dhis)
                .HasMaxLength(150)
                .HasColumnName("DHIS");
            entity.Property(e => e.Hfname).HasColumnName("hfname");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Location");

            entity.HasIndex(e => new { e.IsActive, e.DivisionCode }, "AI_Location_IsActive_DivisionCode");

            entity.HasIndex(e => new { e.Lvl, e.IsActive, e.DivisionCode }, "AI_Location_lvl_IsActive_DivisionCode");

            entity.HasIndex(e => new { e.Hfmiscode, e.IsActive, e.DivisionCode }, "IA_Location_HFMISCode_IsActive_DivisionCode");

            entity.HasIndex(e => new { e.Lvl, e.IsActive, e.DivisionCode }, "IA_Location_lvl_IsActive_DivisionCode");

            entity.HasIndex(e => new { e.DivisionCode, e.Hfmiscode, e.IsActive }, "XII_Location");

            entity.HasIndex(e => e.DivisionCode, "XIV_Location");

            entity.HasIndex(e => new { e.HfId, e.IsActive }, "XV_Location");

            entity.HasIndex(e => new { e.DivisionCode, e.Hfmiscode, e.IsActive }, "XXII_Location");

            entity.HasIndex(e => new { e.DivisionCode, e.Hfmiscode, e.IsActive }, "XXI_Location");

            entity.HasIndex(e => new { e.DivisionCode, e.Lvl, e.IsActive }, "XX_Location");

            entity.HasIndex(e => new { e.DivisionCode, e.HfId, e.IsActive }, "X_Location");

            entity.HasIndex(e => new { e.ModeName, e.IsActive, e.DivisionCode }, "X_Location_ModeName_IsActive_DivisionCode");

            entity.Property(e => e.DhisFacilityCode)
                .HasMaxLength(50)
                .HasColumnName("DHIS_FacilityCode");
            entity.Property(e => e.DistrictCode).HasMaxLength(50);
            entity.Property(e => e.DistrictName).HasMaxLength(150);
            entity.Property(e => e.DivisionCode).HasMaxLength(50);
            entity.Property(e => e.DivisionName).HasMaxLength(150);
            entity.Property(e => e.HealthFacilityName).HasMaxLength(500);
            entity.Property(e => e.Hfmiscode)
                .HasMaxLength(50)
                .HasColumnName("HFMISCode");
            entity.Property(e => e.HfmiscodeOld)
                .HasMaxLength(50)
                .HasColumnName("HFMISCode_OLD");
            entity.Property(e => e.IntegratedRhc).HasColumnName("Integrated_RHC");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.Lvl)
                .HasMaxLength(50)
                .HasColumnName("lvl");
            entity.Property(e => e.ModeName).HasMaxLength(50);
            entity.Property(e => e.Phase)
                .HasMaxLength(500)
                .HasColumnName("PHASE");
            entity.Property(e => e.TehsilCode).HasMaxLength(50);
            entity.Property(e => e.TehsilName).HasMaxLength(150);
        });

        modelBuilder.Entity<Location3sep2022>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Location_3Sep2022");

            entity.Property(e => e.DhisFacilityCode)
                .HasMaxLength(50)
                .HasColumnName("DHIS_FacilityCode");
            entity.Property(e => e.DistrictCode).HasMaxLength(50);
            entity.Property(e => e.DistrictName).HasMaxLength(150);
            entity.Property(e => e.DivisionCode).HasMaxLength(50);
            entity.Property(e => e.DivisionName).HasMaxLength(150);
            entity.Property(e => e.HealthFacilityName).HasMaxLength(500);
            entity.Property(e => e.Hfmiscode)
                .HasMaxLength(50)
                .HasColumnName("HFMISCode");
            entity.Property(e => e.LocationId).ValueGeneratedOnAdd();
            entity.Property(e => e.Lvl)
                .HasMaxLength(50)
                .HasColumnName("lvl");
            entity.Property(e => e.ModeName).HasMaxLength(50);
            entity.Property(e => e.TehsilCode).HasMaxLength(50);
            entity.Property(e => e.TehsilName).HasMaxLength(150);
        });

        modelBuilder.Entity<LocationViewInActiveRemark>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("LocationViewInActiveRemarks");

            entity.Property(e => e.Hfmiscode).HasColumnName("HFMISCode");
            entity.Property(e => e.InActiveTill)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Lvl)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("lvl");
            entity.Property(e => e.ModeName).HasMaxLength(4000);
            entity.Property(e => e.Remarks).HasMaxLength(500);
        });

        modelBuilder.Entity<MapHf>(entity =>
        {
            entity.ToTable("Map_HF");

            entity.Property(e => e.CurrentZone).HasMaxLength(50);
            entity.Property(e => e.DistrictName).HasMaxLength(50);
            entity.Property(e => e.DivisionName).HasMaxLength(50);
            entity.Property(e => e.FinalZone).HasMaxLength(50);
            entity.Property(e => e.Hfid)
                .HasMaxLength(50)
                .HasColumnName("HFId");
            entity.Property(e => e.ModeName).HasMaxLength(50);
            entity.Property(e => e.TehilName).HasMaxLength(50);
            entity.Property(e => e.UpdatedModeName).HasMaxLength(50);
        });

        modelBuilder.Entity<MapReportSeq>(entity =>
        {
            entity.ToTable("MapReportSeq");
        });

        modelBuilder.Entity<MapReportingSequence>(entity =>
        {
            entity.ToTable("MapReportingSequence");

            entity.Property(e => e.Question).HasMaxLength(500);
            entity.Property(e => e.ShortQuestion).HasMaxLength(500);
        });

        modelBuilder.Entity<MeasBundal>(entity =>
        {
            entity.ToTable("MeasBundal");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DistrictCode).HasMaxLength(250);
            entity.Property(e => e.DistrictName).HasMaxLength(250);
            entity.Property(e => e.DivisionCode).HasMaxLength(250);
            entity.Property(e => e.DivisionName).HasMaxLength(250);
            entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            entity.Property(e => e.Month).HasMaxLength(250);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<MeasPakage>(entity =>
        {
            entity.ToTable("MeasPakage");

            entity.Property(e => e.AssignName).HasMaxLength(250);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            entity.Property(e => e.PackName).HasMaxLength(250);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<MedicalCamp>(entity =>
        {
            entity.HasKey(e => e.CampId);

            entity.ToTable("Medical_Camps");

            entity.Property(e => e.CampId).HasColumnName("Camp_Id");
            entity.Property(e => e.CampName)
                .HasMaxLength(500)
                .HasColumnName("Camp_Name");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DistrictName)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.ShiftId).HasColumnName("Shift_Id");
            entity.Property(e => e.Site)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TehsilName)
                .HasMaxLength(500)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.Property(e => e.ModuleName).HasMaxLength(500);
        });

        modelBuilder.Entity<ModuleFlood>(entity =>
        {
            entity.HasKey(e => e.ModuleId);

            entity.ToTable("Module_Flood");

            entity.Property(e => e.ModuleId).ValueGeneratedNever();
            entity.Property(e => e.ModuleName).HasMaxLength(500);
        });

        modelBuilder.Entity<MonitoringAttachment>(entity =>
        {
            entity.HasIndex(e => e.FeedbackId, "X_MonitoringAttachments");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ImageName).HasMaxLength(250);
            entity.Property(e => e.ImagePath).HasMaxLength(250);
            entity.Property(e => e.SyncOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Feedback).WithMany(p => p.MonitoringAttachments)
                .HasForeignKey(d => d.FeedbackId)
                .HasConstraintName("FK_MonitoringAttachments_MonitoringFeedback");
        });

        modelBuilder.Entity<MonitoringAttendance>(entity =>
        {
            entity.ToTable("MonitoringAttendance");

            entity.HasIndex(e => e.MonitoringMasterId, "XII_MonitoringAttendance");

            entity.HasIndex(e => e.MonitoringMasterId, "X_MonitoringAttendance");

            entity.Property(e => e.Cnic)
                .HasMaxLength(50)
                .HasColumnName("CNIC");
            entity.Property(e => e.ContactNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.SyncOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.MonitoringMaster).WithMany(p => p.MonitoringAttendances)
                .HasForeignKey(d => d.MonitoringMasterId)
                .HasConstraintName("FK_MonitoringAttendance_MonitoringMaster");
        });

        modelBuilder.Entity<MonitoringChild>(entity =>
        {
            entity.ToTable("MonitoringChild");

            entity.HasIndex(e => new { e.MonitoringMasterId, e.ModuleId }, "XI_MonitoringChild");

            entity.HasIndex(e => e.MonitoringMasterId, "XXII_MonitoringChild");

            entity.HasIndex(e => e.MonitoringMasterId, "X_MonitoringChild");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.SyncOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Indicator).WithMany(p => p.MonitoringChildren)
                .HasForeignKey(d => d.IndicatorId)
                .HasConstraintName("FK_MonitoringChild_Indicators");

            entity.HasOne(d => d.MonitoringMaster).WithMany(p => p.MonitoringChildren)
                .HasForeignKey(d => d.MonitoringMasterId)
                .HasConstraintName("FK_MonitoringChild_MonitoringMaster");
        });

        modelBuilder.Entity<MonitoringChildFlood>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MonitoringChild_Flood");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.SyncOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<MonitoringFeedback>(entity =>
        {
            entity.ToTable("MonitoringFeedback");

            entity.HasIndex(e => e.MonitoringMasterId, "XVII_MonitoringFeedback");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Meacomment).HasColumnName("MEAComment");
            entity.Property(e => e.SyncOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.MonitoringMaster).WithMany(p => p.MonitoringFeedbacks)
                .HasForeignKey(d => d.MonitoringMasterId)
                .HasConstraintName("FK_MonitoringFeedback_MonitoringMaster");
        });

        modelBuilder.Entity<MonitoringMaster>(entity =>
        {
            entity.ToTable("MonitoringMaster");

            entity.HasIndex(e => new { e.ModuleId, e.HealthFacilityTypeId, e.ShiftTypeId, e.IsActive, e.CreatedOn }, "AII_MonitoringMaster_ModuleId_HealthFacilityTypeId_ShiftTypeId_IsActive_CreatedOn");

            entity.HasIndex(e => new { e.ApplicationTypeId, e.FacilityStatus, e.IsActive, e.HealthFacilityTypeId }, "AI_MonitoringMaster_ApplicationTypeId_FacilityStatus_IsActive_HealthFacilityTypeId");

            entity.HasIndex(e => new { e.ApplicationTypeId, e.FacilityStatus, e.IsActive, e.HealthFacilityTypeId, e.CreatedOn }, "AI_MonitoringMaster_ApplicationTypeId_FacilityStatus_IsActive_HealthFacilityTypeId_CreatedOn");

            entity.HasIndex(e => new { e.ApplicationTypeId, e.IsActive, e.CreatedBy, e.HealthFacilityTypeId }, "AI_MonitoringMaster_ApplicationTypeId_IsActive_CreatedBy_HealthFacilityTypeId");

            entity.HasIndex(e => new { e.ApplicationTypeId, e.IsActive, e.CreatedBy, e.HealthFacilityTypeId, e.CreatedOn }, "AI_MonitoringMaster_ApplicationTypeId_IsActive_CreatedBy_HealthFacilityTypeId_CreatedOn");

            entity.HasIndex(e => new { e.ApplicationTypeId, e.IsActive, e.HealthFacilityTypeId }, "AI_MonitoringMaster_ApplicationTypeId_IsActive_HealthFacilityTypeId");

            entity.HasIndex(e => new { e.ApplicationTypeId, e.IsActive, e.HealthFacilityTypeId, e.CreatedOn }, "AI_MonitoringMaster_ApplicationTypeId_IsActive_HealthFacilityTypeId_CreatedOn");

            entity.HasIndex(e => new { e.HealthFacilityTypeId, e.ShiftTypeId, e.CreatedBy, e.CreatedOn }, "AI_MonitoringMaster_HealthFacilityTypeId_ShiftTypeId_CreatedBy_CreatedOn");

            entity.HasIndex(e => new { e.ModuleId, e.HealthFacilityTypeId, e.ShiftTypeId, e.IsActive, e.CreatedOn }, "AI_MonitoringMaster_ModuleId_HealthFacilityTypeId_ShiftTypeId_IsActive_CreatedOn");

            entity.HasIndex(e => new { e.ModuleId, e.IsActive, e.CreatedOn }, "AI_MonitoringMaster_ModuleId_IsActive_CreatedOn");

            entity.HasIndex(e => new { e.ModuleId, e.ShiftTypeId, e.IsActive, e.CreatedOn }, "AI_MonitoringMaster_ModuleId_ShiftTypeId_IsActive_CreatedOn");

            entity.HasIndex(e => e.HfmisCode, "IA_MonitoringMaster_HfmisCode");

            entity.HasIndex(e => new { e.ApplicationTypeId, e.HealthFacilityTypeId, e.FacilityStatus, e.IsActive, e.CreatedBy }, "XIII_MonitoringMaster");

            entity.HasIndex(e => new { e.ModuleId, e.IsActive, e.CreatedOn }, "XIII_MonitoringMaster_ModuleId_IsActive_CreatedOn");

            entity.HasIndex(e => new { e.ApplicationTypeId, e.HealthFacilityTypeId, e.IsActive, e.CreatedBy }, "XII_MonitoringMaster");

            entity.HasIndex(e => new { e.ApplicationTypeId, e.HealthFacilityTypeId, e.FacilityStatus, e.IsActive, e.CreatedBy }, "XI_MonitoringMaster");

            entity.HasIndex(e => new { e.FacilityStatus, e.ApplicationTypeId, e.HealthFacilityTypeId, e.IsActive, e.CreatedBy }, "XVIII_MonitoringMaster");

            entity.HasIndex(e => new { e.ApplicationTypeId, e.HealthFacilityTypeId, e.IsActive, e.CreatedBy }, "XVII_MonitoringMaster");

            entity.HasIndex(e => new { e.IsActive, e.CreatedBy }, "XVI_MonitoringMaster");

            entity.HasIndex(e => new { e.IsActive, e.CreatedBy }, "XV_MonitoringMaster");

            entity.HasIndex(e => new { e.CreatedOn, e.ModuleId, e.IsActive }, "XXII_MonitoringMaster");

            entity.HasIndex(e => new { e.CreatedOn, e.HealthFacilityTypeId, e.ShiftTypeId, e.IsActive }, "XXI_MonitoringMaster");

            entity.HasIndex(e => new { e.CreatedOn, e.HealthFacilityTypeId, e.ShiftTypeId }, "XX_MonitoringMaster");

            entity.HasIndex(e => new { e.ModuleId, e.ShiftTypeId }, "X_MonitoringMaster");

            entity.HasIndex(e => new { e.HealthFacilityTypeId, e.FacilityStatus, e.IsActive, e.CreatedOn }, "X_MonitoringMaster_HealthFacilityTypeId_FacilityStatus_IsActive_CreatedOn");

            entity.HasIndex(e => new { e.ModuleId, e.IsActive, e.CreatedOn }, "X_MonitoringMaster_ModuleId_IsActive_CreatedOn");

            entity.Property(e => e.CloseReason).HasMaxLength(250);
            entity.Property(e => e.Comments).HasMaxLength(500);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DhisFacilityCode)
                .HasMaxLength(50)
                .HasColumnName("DHIS_FacilityCode");
            entity.Property(e => e.HfmisCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HfmisCodeOld)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HfmisCode_OLD");
            entity.Property(e => e.IllegalOccupationSince).HasColumnType("datetime");
            entity.Property(e => e.InchargeCnic)
                .HasMaxLength(20)
                .HasColumnName("InchargeCNIC");
            entity.Property(e => e.InchargeMobileNo).HasMaxLength(20);
            entity.Property(e => e.InchargeName).HasMaxLength(500);
            entity.Property(e => e.SyncOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.WholeOrPart).HasMaxLength(50);
        });

        modelBuilder.Entity<MonitoringMasterDuplicate>(entity =>
        {
            entity.ToTable("MonitoringMaster_Duplicates");

            entity.Property(e => e.CloseReason).HasMaxLength(250);
            entity.Property(e => e.Comments).HasMaxLength(500);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.HfmisCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IllegalOccupationSince).HasColumnType("datetime");
            entity.Property(e => e.InchargeCnic)
                .HasMaxLength(20)
                .HasColumnName("InchargeCNIC");
            entity.Property(e => e.InchargeMobileNo).HasMaxLength(20);
            entity.Property(e => e.InchargeName).HasMaxLength(500);
            entity.Property(e => e.SyncOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.WholeOrPart).HasMaxLength(50);
        });

        modelBuilder.Entity<MonitoringMasterFlood>(entity =>
        {
            entity.ToTable("MonitoringMaster_Flood");

            entity.Property(e => e.CampId).HasColumnName("Camp_Id");
            entity.Property(e => e.CloseReason).HasMaxLength(250);
            entity.Property(e => e.Comments).HasMaxLength(500);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InchargeCnic)
                .HasMaxLength(20)
                .HasColumnName("InchargeCNIC");
            entity.Property(e => e.InchargeMobileNo).HasMaxLength(20);
            entity.Property(e => e.InchargeName).HasMaxLength(500);
            entity.Property(e => e.SyncOn).HasColumnType("datetime");
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<OptionType>(entity =>
        {
            entity.Property(e => e.OptionTypeName).HasMaxLength(50);
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.ToTable("Profile");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ProfileName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.ProfileShortName)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.ProfileType).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.ProfileTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profile_ProfileType");
        });

        modelBuilder.Entity<ProfileType>(entity =>
        {
            entity.ToTable("ProfileType");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ProfileTypeName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProfileTypeShortName)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.ToTable("Region");

            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.RegionName).HasMaxLength(500);
        });

        modelBuilder.Entity<RepeatVisitPercentage>(entity =>
        {
            entity.HasKey(e => e.RepeatVisitId);

            entity.ToTable("RepeatVisitPercentage");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Month).HasMaxLength(50);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Year).HasMaxLength(50);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Seq>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("seq");

            entity.Property(e => e.Indicatorid).HasColumnName("indicatorid");
            entity.Property(e => e.Seq1)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("seq");
            entity.Property(e => e.Text)
                .HasMaxLength(1500)
                .HasColumnName("text");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.Property(e => e.ShiftName).HasMaxLength(50);
        });

        modelBuilder.Entity<SubCategoriesFlood>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SubCategories_Flood");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.SubCategoryId).ValueGeneratedOnAdd();
            entity.Property(e => e.SubCategoryName).HasMaxLength(200);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.SubCategoryName).HasMaxLength(200);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.SubCategories)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubCategories_Categories");
        });

        modelBuilder.Entity<TblAmbulance>(entity =>
        {
            entity.ToTable("tbl_ambulance");

            entity.Property(e => e.AmbulanceNo).HasMaxLength(500);
            entity.Property(e => e.Dhisname)
                .HasMaxLength(2000)
                .HasColumnName("DHISName");
            entity.Property(e => e.District).HasMaxLength(500);
            entity.Property(e => e.HfmisName)
                .HasMaxLength(2000)
                .HasColumnName("HFMIS_Name");
            entity.Property(e => e.Parking).HasMaxLength(2000);
        });

        modelBuilder.Entity<TblDistrictUsermap>(entity =>
        {
            entity.ToTable("tbl_districtUsermap");

            entity.Property(e => e.DistrictCode).HasMaxLength(50);
            entity.Property(e => e.DistrictName).HasMaxLength(500);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(500);
        });

        modelBuilder.Entity<TblHfAmbulance>(entity =>
        {
            entity.ToTable("tbl_hf_ambulance");

            entity.HasIndex(e => e.HfId, "X_tbl_hf_ambulance");

            entity.Property(e => e.AmbulanceNo).HasMaxLength(100);
            entity.Property(e => e.Dhiscode)
                .HasMaxLength(100)
                .HasColumnName("DHISCode");
            entity.Property(e => e.District)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacilityName).HasMaxLength(1000);
            entity.Property(e => e.Hfmiscode)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("hfmiscode");
            entity.Property(e => e.Lvl)
                .HasMaxLength(50)
                .HasColumnName("lvl");
            entity.Property(e => e.ModeName).HasMaxLength(100);
        });

        modelBuilder.Entity<TblHfAmbulance12sep23>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_hf_ambulance12sep23");

            entity.Property(e => e.AmbulanceNo).HasMaxLength(100);
            entity.Property(e => e.Dhiscode)
                .HasMaxLength(100)
                .HasColumnName("DHISCode");
            entity.Property(e => e.District)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacilityName).HasMaxLength(1000);
            entity.Property(e => e.Hfmiscode)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("hfmiscode");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Lvl)
                .HasMaxLength(50)
                .HasColumnName("lvl");
            entity.Property(e => e.ModeName).HasMaxLength(100);
        });

        modelBuilder.Entity<TblIndicatorMap>(entity =>
        {
            entity.HasKey(e => e.IndicatorId);

            entity.ToTable("tblIndicatorMap");

            entity.Property(e => e.IndicatorId).ValueGeneratedNever();
            entity.Property(e => e.Question).HasMaxLength(500);
            entity.Property(e => e.ShortQuestion).HasMaxLength(500);
        });

        modelBuilder.Entity<TblLocationNew>(entity =>
        {
            entity.ToTable("tbl_Location_new");

            entity.Property(e => e.Ambulances).HasMaxLength(500);
            entity.Property(e => e.Comments).HasMaxLength(500);
            entity.Property(e => e.HfmisDistrictCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("HFMIS_District_Code");
            entity.Property(e => e.HfmisDistrictName)
                .HasMaxLength(500)
                .HasColumnName("HFMIS_District_Name");
            entity.Property(e => e.HfmisDivisionCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("HFMIS_Division_Code");
            entity.Property(e => e.HfmisDivisionName)
                .HasMaxLength(500)
                .HasColumnName("HFMIS_Division_Name");
            entity.Property(e => e.HfmisFacilityType)
                .HasMaxLength(500)
                .HasColumnName("HFMIS_Facility_Type");
            entity.Property(e => e.HfmisId).HasColumnName("HFMIS_ID");
            entity.Property(e => e.HfmisName)
                .HasMaxLength(500)
                .HasColumnName("HFMIS_Name");
            entity.Property(e => e.HfmisTehsilCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("HFMIS_Tehsil_Code");
            entity.Property(e => e.HfmisTehsilName)
                .HasMaxLength(500)
                .HasColumnName("HFMIS_Tehsil_Name");
            entity.Property(e => e.NewHfmisCode)
                .HasMaxLength(500)
                .HasColumnName("New_HFMIS_Code");
            entity.Property(e => e.OldHfmisCode)
                .HasMaxLength(500)
                .HasColumnName("Old_HFMIS_Code");
            entity.Property(e => e.Phase).HasMaxLength(500);
            entity.Property(e => e.PspuMeaZoneId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PSPU_MEA_ZONE_ID");
            entity.Property(e => e.PspuReportingFacilityType)
                .HasMaxLength(500)
                .HasColumnName("PSPU_Reporting_Facility_Type");
            entity.Property(e => e.ReportingFacilityType)
                .HasMaxLength(500)
                .HasColumnName("Reporting_Facility_Type");
        });

        modelBuilder.Entity<TblLocationNew1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_Location_new1");

            entity.Property(e => e.Ambulances).HasMaxLength(500);
            entity.Property(e => e.Comments).HasMaxLength(500);
            entity.Property(e => e.HfmisDistrictCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("HFMIS_District_Code");
            entity.Property(e => e.HfmisDistrictName)
                .HasMaxLength(500)
                .HasColumnName("HFMIS_District_Name");
            entity.Property(e => e.HfmisDivisionCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("HFMIS_Division_Code");
            entity.Property(e => e.HfmisDivisionName)
                .HasMaxLength(500)
                .HasColumnName("HFMIS_Division_Name");
            entity.Property(e => e.HfmisFacilityType)
                .HasMaxLength(500)
                .HasColumnName("HFMIS_Facility_Type");
            entity.Property(e => e.HfmisId).HasColumnName("HFMIS_ID");
            entity.Property(e => e.HfmisName)
                .HasMaxLength(500)
                .HasColumnName("HFMIS_Name");
            entity.Property(e => e.HfmisTehsilCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("HFMIS_Tehsil_Code");
            entity.Property(e => e.HfmisTehsilName)
                .HasMaxLength(500)
                .HasColumnName("HFMIS_Tehsil_Name");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.NewHfmisCode)
                .HasMaxLength(500)
                .HasColumnName("New_HFMIS_Code");
            entity.Property(e => e.OldHfmisCode)
                .HasMaxLength(500)
                .HasColumnName("Old_HFMIS_Code");
            entity.Property(e => e.Phase).HasMaxLength(500);
            entity.Property(e => e.PspuMeaZoneId)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PSPU_MEA_ZONE_ID");
            entity.Property(e => e.PspuReportingFacilityType)
                .HasMaxLength(500)
                .HasColumnName("PSPU_Reporting_Facility_Type");
            entity.Property(e => e.ReportingFacilityType)
                .HasMaxLength(500)
                .HasColumnName("Reporting_Facility_Type");
        });

        modelBuilder.Entity<TblLocationNew2>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_Location_new2");

            entity.Property(e => e.Ambulances).HasMaxLength(500);
            entity.Property(e => e.Comments).HasMaxLength(500);
            entity.Property(e => e.DhisFacilityCode)
                .HasMaxLength(100)
                .HasColumnName("DHIS_Facility_Code");
            entity.Property(e => e.HfmisDistrictCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("HFMIS_District_Code");
            entity.Property(e => e.HfmisDistrictName)
                .HasMaxLength(500)
                .HasColumnName("HFMIS_District_Name");
            entity.Property(e => e.HfmisDivisionCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("HFMIS_Division_Code");
            entity.Property(e => e.HfmisDivisionName)
                .HasMaxLength(500)
                .HasColumnName("HFMIS_Division_Name");
            entity.Property(e => e.HfmisFacilityType)
                .HasMaxLength(500)
                .HasColumnName("HFMIS_Facility_Type");
            entity.Property(e => e.HfmisId).HasColumnName("HFMIS_ID");
            entity.Property(e => e.HfmisName)
                .HasMaxLength(500)
                .HasColumnName("HFMIS_Name");
            entity.Property(e => e.HfmisTehsilCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("HFMIS_Tehsil_Code");
            entity.Property(e => e.HfmisTehsilName)
                .HasMaxLength(500)
                .HasColumnName("HFMIS_Tehsil_Name");
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.NewHfmisCode)
                .HasMaxLength(500)
                .HasColumnName("New_HFMIS_Code");
            entity.Property(e => e.Otp)
                .HasMaxLength(500)
                .HasColumnName("OTP");
            entity.Property(e => e.Phase).HasMaxLength(500);
            entity.Property(e => e.PspuMeaZoneName)
                .HasMaxLength(500)
                .HasColumnName("PSPU_MEA_ZoneName");
            entity.Property(e => e.PspuReportingFacilityType)
                .HasMaxLength(500)
                .HasColumnName("PSPU_Reporting_Facility_Type");
            entity.Property(e => e.ReportingFacilityType)
                .HasMaxLength(500)
                .HasColumnName("Reporting_Facility_Type");
        });

        modelBuilder.Entity<TblShcuserMap>(entity =>
        {
            entity.ToTable("tblSHCUserMap");

            entity.Property(e => e.Division).HasMaxLength(100);
            entity.Property(e => e.MeaType).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Region).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(250);
        });

        modelBuilder.Entity<TehsilView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TehsilView");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .HasColumnName("CNIC");
            entity.Property(e => e.ContactNo).HasMaxLength(15);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Designation)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            entity.Property(e => e.LocationCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Zone).WithMany(p => p.Users)
                .HasForeignKey(d => d.ZoneId)
                .HasConstraintName("FK_Users_Zones");
        });

        modelBuilder.Entity<UserLocation>(entity =>
        {
            entity.Property(e => e.LocationCode).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.UserLocations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserLocations_Users");
        });

        modelBuilder.Entity<UserLog>(entity =>
        {
            entity.ToTable("UserLog");

            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .HasColumnName("CNIC");
            entity.Property(e => e.ContactNo).HasMaxLength(15);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Designation)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            entity.Property(e => e.LocationCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_UserRoles_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_UserRoles_Users");
        });

        modelBuilder.Entity<UserRoles1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserRoles1");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.UserRoleId)
                .ValueGeneratedOnAdd()
                .HasColumnName("UserRoleID");
        });

        modelBuilder.Entity<UserRoles2>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserRoles2");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.UserRoleId)
                .ValueGeneratedOnAdd()
                .HasColumnName("UserRoleID");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.ToTable("UserType");

            entity.Property(e => e.UserTypeId).HasColumnName("UserTypeID");
            entity.Property(e => e.UserTypeName).HasMaxLength(500);
        });

        modelBuilder.Entity<UserVisit>(entity =>
        {
            entity.HasKey(e => e.VisitId);

            entity.HasIndex(e => new { e.Month, e.IsVisited, e.IsActive }, "XIII_UserVisits");

            entity.HasIndex(e => new { e.Month, e.Year }, "XII_UserVisits");

            entity.HasIndex(e => new { e.Month, e.Year }, "XII_UserVisits_Month_Year");

            entity.HasIndex(e => new { e.UserId, e.HfId, e.ShiftId, e.Month, e.Year }, "XIV_UserVisits");

            entity.HasIndex(e => new { e.HfId, e.ShiftId, e.Month, e.IsVisited, e.IsActive }, "XI_UserVisits");

            entity.HasIndex(e => new { e.Month, e.Year }, "XI_UserVisits_Month_Year");

            entity.HasIndex(e => new { e.UserId, e.Month, e.Year }, "XVII_UserVisits");

            entity.HasIndex(e => e.Month, "XXI_UserVisits");

            entity.HasIndex(e => new { e.Month, e.IsRepeat, e.IsSpecial }, "XX_UserVisits");

            entity.HasIndex(e => new { e.Month, e.Year }, "X_UserVisits_Month_Year");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Month).HasMaxLength(25);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.VisitedDate).HasColumnType("datetime");
            entity.Property(e => e.Year).HasMaxLength(25);
        });

        modelBuilder.Entity<UserVisits10822>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserVisits10822");

            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Month).HasMaxLength(25);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.VisitId).ValueGeneratedOnAdd();
            entity.Property(e => e.VisitedDate).HasColumnType("datetime");
            entity.Property(e => e.Year).HasMaxLength(25);
        });

        modelBuilder.Entity<Vacancy>(entity =>
        {
            entity.Property(e => e.VacancyId).HasColumnName("VacancyID");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.VacancyTitle).HasMaxLength(500);

            entity.HasOne(d => d.Master).WithMany(p => p.Vacancies)
                .HasForeignKey(d => d.MasterId)
                .HasConstraintName("FK_Vacancies_VaccancyMaster");
        });

        modelBuilder.Entity<VaccanciesMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("VaccanciesMaster");

            entity.Property(e => e.CloseReason).HasMaxLength(250);
            entity.Property(e => e.Comments).HasMaxLength(500);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DhisFacilityCode)
                .HasMaxLength(50)
                .HasColumnName("DHIS_FacilityCode");
            entity.Property(e => e.HfmisCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.IllegalOccupationSince).HasColumnType("datetime");
            entity.Property(e => e.InchargeCnic)
                .HasMaxLength(20)
                .HasColumnName("InchargeCNIC");
            entity.Property(e => e.InchargeMobileNo).HasMaxLength(20);
            entity.Property(e => e.InchargeName).HasMaxLength(500);
            entity.Property(e => e.SyncOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.WholeOrPart).HasMaxLength(50);
        });

        modelBuilder.Entity<VaccancyMaster>(entity =>
        {
            entity.HasKey(e => e.MasterId);

            entity.ToTable("VaccancyMaster");

            entity.Property(e => e.CloseReason).HasMaxLength(250);
            entity.Property(e => e.Comment).HasMaxLength(1500);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.HfmisCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IllegalOccupationSince).HasColumnType("datetime");
            entity.Property(e => e.InchargeCnic)
                .HasMaxLength(50)
                .HasColumnName("InchargeCNIC");
            entity.Property(e => e.InchargeMobileNo).HasMaxLength(50);
            entity.Property(e => e.InchargeName).HasMaxLength(500);
            entity.Property(e => e.SyncOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.WholeOrPart).HasMaxLength(50);
        });

        modelBuilder.Entity<ViewHealthFacility>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewHealthFacilities");

            entity.Property(e => e.Hfmiscode).HasColumnName("HFMISCode");
            entity.Property(e => e.Lvl)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("lvl");
            entity.Property(e => e.ModeName).HasMaxLength(4000);
        });

        modelBuilder.Entity<ViewLocation>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewLocation");

            entity.Property(e => e.Hfmiscode).HasColumnName("HFMISCode");
            entity.Property(e => e.Lvl)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("lvl");
            entity.Property(e => e.ModeName).HasMaxLength(4000);
        });

        modelBuilder.Entity<ViewLocationMea>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewLocationMEAs");

            entity.Property(e => e.AmbulanceNo).HasMaxLength(100);
            entity.Property(e => e.DhisFacilityCode)
                .HasMaxLength(50)
                .HasColumnName("DHIS_FacilityCode");
            entity.Property(e => e.HealthFacilityName).HasMaxLength(500);
            entity.Property(e => e.Hfmiscode)
                .HasMaxLength(50)
                .HasColumnName("HFMISCode");
            entity.Property(e => e.IntegratedRhc).HasColumnName("Integrated_RHC");
            entity.Property(e => e.Lvl)
                .HasMaxLength(50)
                .HasColumnName("lvl");
            entity.Property(e => e.ModeName).HasMaxLength(50);
            entity.Property(e => e.Phase)
                .HasMaxLength(500)
                .HasColumnName("PHASE");
        });

        modelBuilder.Entity<ViewMeasVisit>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_MEAS_visits");

            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.HealthFacilityName).HasMaxLength(500);
            entity.Property(e => e.Hfmiscode)
                .HasMaxLength(50)
                .HasColumnName("HFMISCode");
            entity.Property(e => e.ShiftName).HasMaxLength(50);
            entity.Property(e => e.SyncOn).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<ViewMeasVisitDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_MEAS_VisitDetails");

            entity.Property(e => e.CategoryName).HasMaxLength(500);
            entity.Property(e => e.Indicator).HasMaxLength(2005);
            entity.Property(e => e.ModuleName).HasMaxLength(500);
            entity.Property(e => e.Subcategory)
                .HasMaxLength(200)
                .HasColumnName("subcategory");
        });

        modelBuilder.Entity<ViewTodayVisit>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_TodayVisits");

            entity.Property(e => e.ApplicationTypeName).HasMaxLength(500);
            entity.Property(e => e.CloseReason).HasMaxLength(250);
            entity.Property(e => e.Comments).HasMaxLength(500);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DhisFacilityCode)
                .HasMaxLength(50)
                .HasColumnName("DHIS_FacilityCode");
            entity.Property(e => e.FaciltyTypeName).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.HealthFacilityName).HasMaxLength(500);
            entity.Property(e => e.HfStatus)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.HfmisCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IllegalOccupationSince).HasColumnType("datetime");
            entity.Property(e => e.InchargeCnic)
                .HasMaxLength(20)
                .HasColumnName("InchargeCNIC");
            entity.Property(e => e.InchargeMobileNo).HasMaxLength(20);
            entity.Property(e => e.InchargeName).HasMaxLength(500);
            entity.Property(e => e.ShiftName).HasMaxLength(50);
            entity.Property(e => e.SyncOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.WholeOrPart).HasMaxLength(50);
        });

        modelBuilder.Entity<ViewTodaysVisit>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("View_TodaysVisits");

            entity.Property(e => e.TodayBhu247visits).HasColumnName("TodayBHU247Visits");
            entity.Property(e => e.TodayBhuvisits).HasColumnName("TodayBHUVisits");
            entity.Property(e => e.TodayRhcvisits).HasColumnName("TodayRHCVisits");
        });

        modelBuilder.Entity<Ward>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.WardName).HasMaxLength(500);
        });

        modelBuilder.Entity<Zone>(entity =>
        {
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DistrictCode).HasMaxLength(50);
            entity.Property(e => e.DivisonCode).HasMaxLength(50);
            entity.Property(e => e.TehsilCode).HasMaxLength(50);
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.ZoneName).HasMaxLength(500);

            entity.HasOne(d => d.ApplicationType).WithMany(p => p.Zones)
                .HasForeignKey(d => d.ApplicationTypeId)
                .HasConstraintName("FK_Zones_ApplicationType");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
