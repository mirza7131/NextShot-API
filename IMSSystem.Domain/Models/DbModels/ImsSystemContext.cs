using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IMSSystem.Domain.Models.DbModels;

public partial class ImsSystemContext : DbContext
{
    public ImsSystemContext()
    {
    }

    public ImsSystemContext(DbContextOptions<ImsSystemContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdditionalInfo> AdditionalInfos { get; set; }

    public virtual DbSet<ApplicationStatus> ApplicationStatuses { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<AssessmentOption> AssessmentOptions { get; set; }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<Building> Buildings { get; set; }

    public virtual DbSet<BuildingDimension> BuildingDimensions { get; set; }

    public virtual DbSet<BuildingsPhoto> BuildingsPhotos { get; set; }

    public virtual DbSet<Configuration> Configurations { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<DataSyncPatientRecord> DataSyncPatientRecords { get; set; }

    public virtual DbSet<DataSyncPatientRecordBackup> DataSyncPatientRecordBackups { get; set; }

    public virtual DbSet<DataSyncToOffline> DataSyncToOfflines { get; set; }

    public virtual DbSet<DataSyncUtilityLog> DataSyncUtilityLogs { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Division> Divisions { get; set; }

    public virtual DbSet<DoctorNote> DoctorNotes { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmploymentType> EmploymentTypes { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<EventHealthFacility> EventHealthFacilities { get; set; }

    public virtual DbSet<EventUser> EventUsers { get; set; }

    public virtual DbSet<EventVisit> EventVisits { get; set; }

    public virtual DbSet<Feature> Features { get; set; }

    public virtual DbSet<FeeStructure> FeeStructures { get; set; }

    public virtual DbSet<FileUploadedToMasterServerLog> FileUploadedToMasterServerLogs { get; set; }

    public virtual DbSet<Finance> Finances { get; set; }

    public virtual DbSet<GetAllmenuForSuperAdmin> GetAllmenuForSuperAdmins { get; set; }

    public virtual DbSet<Governance> Governances { get; set; }

    public virtual DbSet<GrantApplication> GrantApplications { get; set; }

    public virtual DbSet<HcpMedicineDeliveryDatum> HcpMedicineDeliveryData { get; set; }

    public virtual DbSet<HealthFacility> HealthFacilities { get; set; }

    public virtual DbSet<HealthFacilityCategory> HealthFacilityCategories { get; set; }

    public virtual DbSet<HealthFacilityStation> HealthFacilityStations { get; set; }

    public virtual DbSet<HealthFacilityType> HealthFacilityTypes { get; set; }

    public virtual DbSet<HfDepartment> HfDepartments { get; set; }

    public virtual DbSet<HfDepartmentSection> HfDepartmentSections { get; set; }

    public virtual DbSet<HfLabTestConfig> HfLabTestConfigs { get; set; }

    public virtual DbSet<HfSpEmployee> HfSpEmployees { get; set; }

    public virtual DbSet<ImsDesignation> ImsDesignations { get; set; }

    public virtual DbSet<InstituteDetail> InstituteDetails { get; set; }

    public virtual DbSet<InstituteIncome> InstituteIncomes { get; set; }

    public virtual DbSet<Link> Links { get; set; }

    public virtual DbSet<MedicineLookup> MedicineLookups { get; set; }

    public virtual DbSet<MentalAssessment> MentalAssessments { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<MimsGetMedicineResponse> MimsGetMedicineResponses { get; set; }

    public virtual DbSet<MimsMedicineDatum> MimsMedicineData { get; set; }

    public virtual DbSet<MimsMedicineIndentDetail> MimsMedicineIndentDetails { get; set; }

    public virtual DbSet<MimsMedicineIndentLog> MimsMedicineIndentLogs { get; set; }

    public virtual DbSet<Mlclog> Mlclogs { get; set; }

    public virtual DbSet<OfflineServerDataSyncLog> OfflineServerDataSyncLogs { get; set; }

    public virtual DbSet<OfflineVersionLog> OfflineVersionLogs { get; set; }

    public virtual DbSet<Otpcode> Otpcodes { get; set; }

    public virtual DbSet<OutSourceSystemLog> OutSourceSystemLogs { get; set; }

    public virtual DbSet<PastAffliation> PastAffliations { get; set; }

    public virtual DbSet<PatientAdmissionFlowLog> PatientAdmissionFlowLogs { get; set; }

    public virtual DbSet<PatientConfirmedDisease> PatientConfirmedDiseases { get; set; }

    public virtual DbSet<PatientStatusBySpeciality> PatientStatusBySpecialities { get; set; }

    public virtual DbSet<PatientVisitSpecilityUpdatedDatum> PatientVisitSpecilityUpdatedData { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PrincipalExperience> PrincipalExperiences { get; set; }

    public virtual DbSet<PrincipalQualification> PrincipalQualifications { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<ProfileType> ProfileTypes { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectRole> ProjectRoles { get; set; }

    public virtual DbSet<ProjectUser> ProjectUsers { get; set; }

    public virtual DbSet<Proposal> Proposals { get; set; }

    public virtual DbSet<ProposalProbe> ProposalProbes { get; set; }

    public virtual DbSet<ProposalView> ProposalViews { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<PublicUser> PublicUsers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleMenu> RoleMenus { get; set; }

    public virtual DbSet<Scholarship> Scholarships { get; set; }

    public virtual DbSet<ServiceProvider> ServiceProviders { get; set; }

    public virtual DbSet<ServiceProviderHealthFacility> ServiceProviderHealthFacilities { get; set; }

    public virtual DbSet<ServiceType> ServiceTypes { get; set; }

    public virtual DbSet<Shift> Shifts { get; set; }

    public virtual DbSet<SmsLog> SmsLogs { get; set; }

    public virtual DbSet<SmsSession> SmsSessions { get; set; }

    public virtual DbSet<SourceSystem> SourceSystems { get; set; }

    public virtual DbSet<SpEmployee> SpEmployees { get; set; }

    public virtual DbSet<SpInvoice> SpInvoices { get; set; }

    public virtual DbSet<SpService> SpServices { get; set; }

    public virtual DbSet<StakeHolderType> StakeHolderTypes { get; set; }

    public virtual DbSet<SyncDataLog> SyncDataLogs { get; set; }

    public virtual DbSet<Table1> Table1s { get; set; }

    public virtual DbSet<TbMedicineDeliveryDatum> TbMedicineDeliveryData { get; set; }

    public virtual DbSet<Tehsil> Tehsils { get; set; }

    public virtual DbSet<TempMedicinePrice> TempMedicinePrices { get; set; }

    public virtual DbSet<TempMimsmedicineListPrice> TempMimsmedicineListPrices { get; set; }

    public virtual DbSet<Uc> Ucs { get; set; }

    public virtual DbSet<UnionCouncil> UnionCouncils { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAssignableRole> UserAssignableRoles { get; set; }

    public virtual DbSet<UserLog> UserLogs { get; set; }

    public virtual DbSet<UserPermission> UserPermissions { get; set; }

    public virtual DbSet<UserRegistrationLog> UserRegistrationLogs { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<ViewAllInvoiceStatus> ViewAllInvoiceStatuses { get; set; }

    public virtual DbSet<ViewDoctorNote> ViewDoctorNotes { get; set; }

    public virtual DbSet<ViewGetAllIpsychologicalAssessmentQuestion> ViewGetAllIpsychologicalAssessmentQuestions { get; set; }

    public virtual DbSet<ViewGetAllRoleMenuAccess> ViewGetAllRoleMenuAccesses { get; set; }

    public virtual DbSet<ViewGetCreateRoleMenuAccess> ViewGetCreateRoleMenuAccesses { get; set; }

    public virtual DbSet<ViewGetEditRoleMenuAccess> ViewGetEditRoleMenuAccesses { get; set; }

    public virtual DbSet<ViewHfLocation> ViewHfLocations { get; set; }

    public virtual DbSet<ViewInvoiceStatusCount> ViewInvoiceStatusCounts { get; set; }

    public virtual DbSet<ViewLocation> ViewLocations { get; set; }

    public virtual DbSet<ViewSpEmployeesListForInvoice> ViewSpEmployeesListForInvoices { get; set; }

    public virtual DbSet<ViewSpecialityRoomNo> ViewSpecialityRoomNos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=172.16.0.19;Database=IMS_System;Persist Security Info=False;User Id=khurram;Password=abcd@1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=400;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdditionalInfo>(entity =>
        {
            entity.ToTable("AdditionalInfo");

            entity.Property(e => e.GrantApplicationId).HasColumnName("GrantApplication_Id");
            entity.Property(e => e.Hear).HasColumnName("hear");
            entity.Property(e => e.UserId).HasMaxLength(2050);
        });

        modelBuilder.Entity<ApplicationStatus>(entity =>
        {
            entity.ToTable("ApplicationStatus");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.Property(e => e.RoleId).HasMaxLength(450);

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.Property(e => e.Cnic).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DesigCode).HasMaxLength(2050);
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.DistrictId)
                .HasMaxLength(2050)
                .HasColumnName("DistrictID");
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.DivisionId)
                .HasMaxLength(2050)
                .HasColumnName("DivisionID");
            entity.Property(e => e.Domiclie).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Hashynoty)
                .HasMaxLength(2050)
                .HasColumnName("hashynoty");
            entity.Property(e => e.HfTypeCode).HasMaxLength(2050);
            entity.Property(e => e.HfmisCodeNew).HasMaxLength(2050);
            entity.Property(e => e.Hfmiscode)
                .HasMaxLength(2050)
                .HasColumnName("hfmiscode");
            entity.Property(e => e.Hfname).HasColumnName("HFName");
            entity.Property(e => e.IsUpdated).HasColumnName("isUpdated");
            entity.Property(e => e.LevelId)
                .HasMaxLength(2050)
                .HasColumnName("LevelID");
            entity.Property(e => e.ModifiedBy).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.Responsibleuser).HasColumnName("responsibleuser");
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.TehsilId)
                .HasMaxLength(2050)
                .HasColumnName("TehsilID");
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AspNetUserRoles");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.Property(e => e.UserId).HasMaxLength(450);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AssessmentOption>(entity =>
        {
            entity.ToTable("AssessmentOption");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Option1).HasMaxLength(200);
            entity.Property(e => e.Option2).HasMaxLength(200);
            entity.Property(e => e.Option3).HasMaxLength(200);
            entity.Property(e => e.Option4).HasMaxLength(200);
            entity.Property(e => e.Option5).HasMaxLength(200);
            entity.Property(e => e.Option6).HasMaxLength(200);
            entity.Property(e => e.Option7).HasMaxLength(200);
            entity.Property(e => e.Option8).HasMaxLength(200);
            entity.Property(e => e.Option9).HasMaxLength(200);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.AttachmentId).HasName("PK__Attachme__442C64BE1BB7D96E");

            entity.Property(e => e.AttachmentId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.ParentType)
                .HasMaxLength(50)
                .IsUnicode(false);
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
            entity.Property(e => e.JsonBody).IsUnicode(false);
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

        modelBuilder.Entity<Building>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ClassRoom");

            entity.Property(e => e.BuilidingType).HasMaxLength(2050);
            entity.Property(e => e.GrantApplicationId).HasColumnName("GrantApplication_Id");
            entity.Property(e => e.ImagePath).HasMaxLength(2050);
            entity.Property(e => e.LabPurpose).HasMaxLength(2050);
            entity.Property(e => e.Length).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Name).HasMaxLength(2050);
            entity.Property(e => e.UserId).HasMaxLength(2050);
            entity.Property(e => e.Width).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<BuildingDimension>(entity =>
        {
            entity.Property(e => e.BuildingStatus).HasMaxLength(2050);
            entity.Property(e => e.BuildingStatusId).HasColumnName("BuildingStatus_Id");
            entity.Property(e => e.Floors).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GrantApplicationId).HasColumnName("GrantApplication_Id");
            entity.Property(e => e.Map).HasMaxLength(2050);
            entity.Property(e => e.TotalArea).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalCoveredArea).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserId).HasMaxLength(2050);
        });

        modelBuilder.Entity<BuildingsPhoto>(entity =>
        {
            entity.Property(e => e.BuildingId).HasColumnName("Building_Id");
            entity.Property(e => e.GrantApplicationId).HasColumnName("GrantApplication_Id");
            entity.Property(e => e.ImagePath).HasMaxLength(2050);
        });

        modelBuilder.Entity<Configuration>(entity =>
        {
            entity.ToTable("Configuration");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.Property(e => e.CourseName).HasMaxLength(2050);
            entity.Property(e => e.DegreeOffered).HasMaxLength(2050);
            entity.Property(e => e.DisContinuedReason).HasMaxLength(2050);
            entity.Property(e => e.GrantApplicationId).HasColumnName("GrantApplication_Id");
            entity.Property(e => e.Heccertificate)
                .HasMaxLength(2050)
                .HasColumnName("HECCertificate");
            entity.Property(e => e.Hecrecognized).HasColumnName("HECRecognized");
            entity.Property(e => e.Ibccaffliated).HasColumnName("IBCCAffliated");
            entity.Property(e => e.Ibcccertificate)
                .HasMaxLength(2050)
                .HasColumnName("IBCCCertificate");
            entity.Property(e => e.RequiredQualification).HasMaxLength(2050);
            entity.Property(e => e.RequiredQualificationId).HasColumnName("RequiredQualification_Id");
            entity.Property(e => e.UserId).HasMaxLength(2050);
        });

        modelBuilder.Entity<DataSyncPatientRecord>(entity =>
        {
            entity.ToTable("DataSyncPatientRecord");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<DataSyncPatientRecordBackup>(entity =>
        {
            entity.HasKey(e => e.DataSyncPatientRecordId);

            entity.ToTable("DataSyncPatientRecord_Backup");

            entity.Property(e => e.DataSyncPatientRecordId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<DataSyncToOffline>(entity =>
        {
            entity.ToTable("DataSyncToOffline");

            entity.Property(e => e.ActionOn).HasColumnType("datetime");
            entity.Property(e => e.PrevSyncOn).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SyncOn).HasColumnType("datetime");
            entity.Property(e => e.TableName)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DataSyncUtilityLog>(entity =>
        {
            entity.ToTable("DataSyncUtilityLog");

            entity.Property(e => e.DataSyncUtilityLogId).ValueGeneratedNever();
            entity.Property(e => e.CompletedOn).HasColumnType("datetime");
            entity.Property(e => e.FileName)
                .HasMaxLength(120)
                .IsUnicode(false);
            entity.Property(e => e.FileSize)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.ProcessedOn).HasColumnType("datetime");
            entity.Property(e => e.ServerType)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.StatusUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.UploadedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.ToTable("District");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Division).WithMany(p => p.Districts)
                .HasForeignKey(d => d.DivisionId)
                .HasConstraintName("FK_District_Division");
        });

        modelBuilder.Entity<Division>(entity =>
        {
            entity.ToTable("Division");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Province).WithMany(p => p.Divisions)
                .HasForeignKey(d => d.ProvinceId)
                .HasConstraintName("FK_Division_Province");
        });

        modelBuilder.Entity<DoctorNote>(entity =>
        {
            entity.HasKey(e => e.DoctorNotesId);

            entity.Property(e => e.DoctorNotesId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Category).HasMaxLength(250);
            entity.Property(e => e.Courses).HasMaxLength(2050);
            entity.Property(e => e.Designation).HasMaxLength(250);
            entity.Property(e => e.DesignationId).HasColumnName("Designation_Id");
            entity.Property(e => e.EmployeeCategoryId).HasColumnName("EmployeeCategory_Id");
            entity.Property(e => e.EmployeeName).HasMaxLength(2050);
            entity.Property(e => e.GrantApplicationId).HasColumnName("GrantApplication_Id");
            entity.Property(e => e.ImagePath).HasMaxLength(2050);
            entity.Property(e => e.MonthlySalary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Qualification).HasMaxLength(250);
            entity.Property(e => e.QualificationId).HasColumnName("Qualification_Id");
            entity.Property(e => e.UserId).HasMaxLength(2050);
        });

        modelBuilder.Entity<EmploymentType>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
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

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Event");

            entity.Property(e => e.EventId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.EndDateTime).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<EventHealthFacility>(entity =>
        {
            entity.ToTable("EventHealthFacility");

            entity.Property(e => e.EventHealthFacilityId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.EndDateTime).HasColumnType("datetime");
            entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<EventUser>(entity =>
        {
            entity.HasKey(e => e.EventUsersId);

            entity.Property(e => e.EventUsersId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.EndDateTime).HasColumnType("datetime");
            entity.Property(e => e.StartDateTime).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<EventVisit>(entity =>
        {
            entity.ToTable("EventVisit");

            entity.Property(e => e.EventVisitId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Feature>(entity =>
        {
            entity.HasKey(e => e.FeatureId).HasName("PK__Features__82230BC9171B0A06");

            entity.Property(e => e.FeatureId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<FeeStructure>(entity =>
        {
            entity.ToTable("FeeStructure");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.FeeType).HasMaxLength(2050);
            entity.Property(e => e.GrantApplicationId).HasColumnName("GrantApplication_Id");
            entity.Property(e => e.UserId).HasMaxLength(2050);
        });

        modelBuilder.Entity<FileUploadedToMasterServerLog>(entity =>
        {
            entity.ToTable("FileUploadedToMasterServerLog");

            entity.Property(e => e.FileUploadedToMasterServerLogId).ValueGeneratedNever();
            entity.Property(e => e.CdnUrl).HasMaxLength(200);
            entity.Property(e => e.FileName).HasMaxLength(200);
            entity.Property(e => e.ProcessOn).HasColumnType("datetime");
            entity.Property(e => e.UploadedDateTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<Finance>(entity =>
        {
            entity.ToTable("Finance");

            entity.Property(e => e.AnnualExpenditures).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AuditReport).HasMaxLength(2050);
            entity.Property(e => e.FinancialStatement).HasMaxLength(2050);
            entity.Property(e => e.GrantApplicationId).HasColumnName("GrantApplication_Id");
            entity.Property(e => e.UserId).HasMaxLength(2050);
        });

        modelBuilder.Entity<GetAllmenuForSuperAdmin>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GetALLMenuForSuperAdmin");

            entity.Property(e => e.DisplayName).HasMaxLength(100);
            entity.Property(e => e.Icon).HasMaxLength(100);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.IsApi).HasColumnName("IsAPI");
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.Url)
                .HasMaxLength(100)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<Governance>(entity =>
        {
            entity.ToTable("Governance");

            entity.Property(e => e.EvaluationReport).HasMaxLength(2050);
            entity.Property(e => e.GrantApplicationId).HasColumnName("GrantApplication_Id");
        });

        modelBuilder.Entity<GrantApplication>(entity =>
        {
            entity.ToTable("GrantApplication");

            entity.Property(e => e.BuildingCoveredArea).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BuildingStatus).HasMaxLength(250);
            entity.Property(e => e.BuildingTotalArea).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Cnic)
                .HasMaxLength(2050)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(2050);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.InstituteName).HasMaxLength(2050);
            entity.Property(e => e.Mobile).HasMaxLength(50);
            entity.Property(e => e.ModifiedAt).HasMaxLength(2050);
            entity.Property(e => e.OwnerName).HasMaxLength(2050);
            entity.Property(e => e.OwnershipProof).HasMaxLength(2050);
            entity.Property(e => e.PrincipalName).HasMaxLength(2050);
            entity.Property(e => e.Qualification).HasMaxLength(250);
            entity.Property(e => e.ScrutinyByUserId).HasMaxLength(2050);
            entity.Property(e => e.StatusId).HasColumnName("Status_Id");
        });

        modelBuilder.Entity<HcpMedicineDeliveryDatum>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Age).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Cnic)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.DeliveryOn).HasColumnType("datetime");
            entity.Property(e => e.FatherName).HasMaxLength(150);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacility)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacilityDistrict)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacilityTehsil)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.LastMedicineReceivedDate).HasColumnType("datetime");
            entity.Property(e => e.Medicine)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.MrNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PackingOn).HasColumnType("datetime");
            entity.Property(e => e.PatientDistrict)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PatientDivision)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PatientName).HasMaxLength(150);
            entity.Property(e => e.PatientTehsil)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNo1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNo2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
            entity.Property(e => e.SentForPackingOn).HasColumnType("datetime");
            entity.Property(e => e.TypeOfPatient)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.VerifiedAddress)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HealthFacility>(entity =>
        {
            entity.ToTable("HealthFacility");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacilityTypeCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsRunningHmis).HasColumnName("IsRunningHMIS");
            entity.Property(e => e.Latitude).HasColumnType("decimal(18, 10)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(18, 10)");
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.ProvinceCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UnionCouncilCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.District).WithMany(p => p.HealthFacilities)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK_HealthFacility_District");

            entity.HasOne(d => d.Division).WithMany(p => p.HealthFacilities)
                .HasForeignKey(d => d.DivisionId)
                .HasConstraintName("FK_HealthFacility_Division");

            entity.HasOne(d => d.HealthFacilityType).WithMany(p => p.HealthFacilities)
                .HasForeignKey(d => d.HealthFacilityTypeId)
                .HasConstraintName("FK_HealthFacility_HealthFacilityType");

            entity.HasOne(d => d.Province).WithMany(p => p.HealthFacilities)
                .HasForeignKey(d => d.ProvinceId)
                .HasConstraintName("FK_HealthFacility_Province");

            entity.HasOne(d => d.Tehsil).WithMany(p => p.HealthFacilities)
                .HasForeignKey(d => d.TehsilId)
                .HasConstraintName("FK_HealthFacility_Tehsil");

            entity.HasOne(d => d.UnionCouncil).WithMany(p => p.HealthFacilities)
                .HasForeignKey(d => d.UnionCouncilId)
                .HasConstraintName("FK_HealthFacility_UnionCouncil");
        });

        modelBuilder.Entity<HealthFacilityCategory>(entity =>
        {
            entity.ToTable("HealthFacilityCategory");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<HealthFacilityStation>(entity =>
        {
            entity.ToTable("HealthFacilityStation");

            entity.Property(e => e.HealthFacilityStationId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.HealthFacility).WithMany(p => p.HealthFacilityStations)
                .HasForeignKey(d => d.HealthFacilityId)
                .HasConstraintName("FK_HealthFacilityStation_HealthFacility");

            entity.HasOne(d => d.StationProfile).WithMany(p => p.HealthFacilityStations)
                .HasForeignKey(d => d.StationProfileId)
                .HasConstraintName("FK_HealthFacilityStation_Profile");
        });

        modelBuilder.Entity<HealthFacilityType>(entity =>
        {
            entity.ToTable("HealthFacilityType");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.HealthFacilityCategory).WithMany(p => p.HealthFacilityTypes)
                .HasForeignKey(d => d.HealthFacilityCategoryId)
                .HasConstraintName("FK_HealthFacilityType_HealthFacilityCategory");
        });

        modelBuilder.Entity<HfDepartment>(entity =>
        {
            entity.HasKey(e => e.HfDepartmentId).HasName("PK_HFDepartments");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.HealthFacility).WithMany(p => p.HfDepartments)
                .HasForeignKey(d => d.HealthFacilityId)
                .HasConstraintName("FK_HFDepartments_HealthFacility");
        });

        modelBuilder.Entity<HfDepartmentSection>(entity =>
        {
            entity.HasKey(e => e.HfDepartmentSectionId).HasName("PK_HfDepartmentServices");

            entity.ToTable("HfDepartmentSection");

            entity.Property(e => e.AlmonerFloorNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AlmonerRoomNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.DoctorFloorNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DoctorRoomNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PathalogyFloorNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PathalogyRoomNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PharmacyFloorNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PharmacyRoomNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.VitalsFloorNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VitalsRoomNo)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.HfDepartment).WithMany(p => p.HfDepartmentSections)
                .HasForeignKey(d => d.HfDepartmentId)
                .HasConstraintName("FK_HfDepartmentSection_HfDepartments");
        });

        modelBuilder.Entity<HfLabTestConfig>(entity =>
        {
            entity.ToTable("HfLabTestConfig");

            entity.Property(e => e.HfLabTestConfigId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.HfLabTestConfigCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.HfLabTestConfigDeletedByNavigations).HasForeignKey(d => d.DeletedBy);

            entity.HasOne(d => d.HealthFacility).WithMany(p => p.HfLabTestConfigs)
                .HasForeignKey(d => d.HealthFacilityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HfLabTestConfig_HealthFacility");

            entity.HasOne(d => d.LabDepartmentProfile).WithMany(p => p.HfLabTestConfigs)
                .HasForeignKey(d => d.LabDepartmentProfileId)
                .HasConstraintName("FK_HfLabTestConfig_Profile");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.HfLabTestConfigUpdatedByNavigations).HasForeignKey(d => d.UpdatedBy);
        });

        modelBuilder.Entity<HfSpEmployee>(entity =>
        {
            entity.Property(e => e.HfSpEmployeeId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ImsDesignation>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("imsDesignations");

            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.ShortName).HasMaxLength(50);
        });

        modelBuilder.Entity<InstituteDetail>(entity =>
        {
            entity.ToTable("InstituteDetail");

            entity.Property(e => e.AffliatedNumber).HasMaxLength(2050);
            entity.Property(e => e.AffliationCertificate).HasMaxLength(2050);
            entity.Property(e => e.AffliationType).HasMaxLength(2050);
            entity.Property(e => e.AffliationTypeId).HasColumnName("AffliationType_Id");
            entity.Property(e => e.CurrentAffliationUniversity)
                .HasMaxLength(2050)
                .HasColumnName("CurrentAffliation_University");
            entity.Property(e => e.GrantApplicationId).HasColumnName("GrantApplication_Id");
            entity.Property(e => e.Hospital).HasMaxLength(2050);
            entity.Property(e => e.HospitalMou)
                .HasMaxLength(2050)
                .HasColumnName("HospitalMOU");
            entity.Property(e => e.HospitalSector).HasMaxLength(2050);
            entity.Property(e => e.HospitalSectorId).HasColumnName("HospitalSector_Id");
            entity.Property(e => e.InstituteType).HasMaxLength(2050);
            entity.Property(e => e.InstituteTypeId).HasColumnName("InstituteType_Id");
            entity.Property(e => e.LegalRegistrationCertificate).HasMaxLength(2050);
            entity.Property(e => e.LegalStatus).HasMaxLength(2050);
            entity.Property(e => e.LegalStatusId).HasColumnName("LegalStatus_Id");
            entity.Property(e => e.UniversityName).HasMaxLength(2050);
            entity.Property(e => e.UserId).HasMaxLength(2050);
        });

        modelBuilder.Entity<InstituteIncome>(entity =>
        {
            entity.ToTable("InstituteIncome");

            entity.Property(e => e.GrantApplicationId).HasColumnName("GrantApplication_Id");
            entity.Property(e => e.IncomeSource).HasMaxLength(2050);
            entity.Property(e => e.IncomeStatement).HasMaxLength(2050);
            entity.Property(e => e.IncomeTotal).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserId).HasMaxLength(2050);
        });

        modelBuilder.Entity<Link>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Links");

            entity.ToTable("Link");

            entity.Property(e => e.Badge).HasMaxLength(2050);
            entity.Property(e => e.Icon).HasMaxLength(2050);
            entity.Property(e => e.Name).HasMaxLength(2050);
            entity.Property(e => e.SerialNo).HasMaxLength(2050);
            entity.Property(e => e.Url).HasMaxLength(2050);
        });

        modelBuilder.Entity<MedicineLookup>(entity =>
        {
            entity.ToTable("MedicineLookup");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<MentalAssessment>(entity =>
        {
            entity.ToTable("MentalAssessment");

            entity.Property(e => e.MentalAssessmentId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.ToTable("Menu");

            entity.Property(e => e.MenuId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.DisplayName).HasMaxLength(100);
            entity.Property(e => e.Icon).HasMaxLength(100);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.IsApi).HasColumnName("IsAPI");
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Url)
                .HasMaxLength(100)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<MimsGetMedicineResponse>(entity =>
        {
            entity.ToTable("MimsGetMedicineResponse");

            entity.Property(e => e.MimsGetMedicineResponseId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.WardName).HasMaxLength(100);
        });

        modelBuilder.Entity<MimsMedicineDatum>(entity =>
        {
            entity.HasKey(e => e.MimsMedicineDataId);

            entity.Property(e => e.MimsMedicineDataId).ValueGeneratedNever();
            entity.Property(e => e.AvailableQuantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsSmlmedicine).HasColumnName("IsSMLMedicine");
            entity.Property(e => e.MedicineName).HasMaxLength(200);
            entity.Property(e => e.MedicineTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TotalDispatchQuantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalQuantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.WardName).HasMaxLength(100);
        });

        modelBuilder.Entity<MimsMedicineIndentDetail>(entity =>
        {
            entity.ToTable("MimsMedicineIndentDetail");

            entity.Property(e => e.MimsMedicineIndentDetailId).ValueGeneratedNever();
            entity.Property(e => e.AvailableQuantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.MedicineName).HasMaxLength(200);
            entity.Property(e => e.MedicineTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PricePerItem).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.WardName).HasMaxLength(100);
        });

        modelBuilder.Entity<MimsMedicineIndentLog>(entity =>
        {
            entity.ToTable("MimsMedicineIndentLog");

            entity.Property(e => e.MimsMedicineIndentLogId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.WardName).HasMaxLength(100);
        });

        modelBuilder.Entity<Mlclog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("MLCLog");

            entity.Property(e => e.BookNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CaseAgainst).HasMaxLength(250);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.McdtypeProfileId).HasColumnName("MCDTypeProfileId");
            entity.Property(e => e.Mlcid).HasColumnName("MLCId");
            entity.Property(e => e.MlclogId).HasColumnName("MLCLogId");
            entity.Property(e => e.Mlcno).HasColumnName("MLCNo");
            entity.Property(e => e.MlctypeProfileId).HasColumnName("MLCTypeProfileId");
            entity.Property(e => e.PcdtypeProfileId).HasColumnName("PCDTypeProfileId");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<OfflineServerDataSyncLog>(entity =>
        {
            entity.ToTable("OfflineServerDataSyncLog");

            entity.Property(e => e.OfflineServerDataSyncLogId).ValueGeneratedNever();
            entity.Property(e => e.LastSync).HasColumnType("datetime");
        });

        modelBuilder.Entity<OfflineVersionLog>(entity =>
        {
            entity.ToTable("OfflineVersionLog");

            entity.Property(e => e.OfflineVersionLogId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("IP_Address");
            entity.Property(e => e.ReleaseDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.VersionNumber).HasMaxLength(20);

            entity.HasOne(d => d.HealthFacility).WithMany(p => p.OfflineVersionLogs)
                .HasForeignKey(d => d.HealthFacilityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OfflineVersionLog_HealthFacilityId");

            entity.HasOne(d => d.ProjectProfile).WithMany(p => p.OfflineVersionLogs)
                .HasForeignKey(d => d.ProjectProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OfflineVersionLog_Profile");
        });

        modelBuilder.Entity<Otpcode>(entity =>
        {
            entity.ToTable("OTPCode");

            entity.Property(e => e.OtpcodeId)
                .ValueGeneratedNever()
                .HasColumnName("OTPCodeId");
            entity.Property(e => e.Datetime).HasColumnType("datetime");
            entity.Property(e => e.Otp).HasColumnName("OTP");
        });

        modelBuilder.Entity<OutSourceSystemLog>(entity =>
        {
            entity.ToTable("OutSourceSystemLog");

            entity.Property(e => e.OutSourceSystemLogId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.TargetedApiUrl).HasMaxLength(200);
            entity.Property(e => e.UserSystemIp).HasMaxLength(200);
        });

        modelBuilder.Entity<PastAffliation>(entity =>
        {
            entity.Property(e => e.Certificate).HasMaxLength(2050);
            entity.Property(e => e.GrantApplicationId).HasColumnName("GrantApplication_Id");
            entity.Property(e => e.OrganizationName).HasMaxLength(2050);
            entity.Property(e => e.UserId).HasMaxLength(2050);
        });

        modelBuilder.Entity<PatientAdmissionFlowLog>(entity =>
        {
            entity.ToTable("PatientAdmissionFlowLog");

            entity.Property(e => e.PatientAdmissionFlowLogId).ValueGeneratedNever();
            entity.Property(e => e.AdmittedBy)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.AdmittedInSpeciality).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.PatientAdmittedOn).HasColumnType("datetime");
            entity.Property(e => e.ReasonOfShifting)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ShiftedBy)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ShiftedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientConfirmedDisease>(entity =>
        {
            entity.Property(e => e.PatientConfirmedDiseaseId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FormType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientStatusBySpeciality>(entity =>
        {
            entity.ToTable("PatientStatusBySpeciality");

            entity.Property(e => e.PatientStatusBySpecialityId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.DeseaseSubType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FormType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TbPatientConfirmationType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientVisitSpecilityUpdatedDatum>(entity =>
        {
            entity.HasKey(e => e.PatientVisitSpecilityUpdatedDataId);

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permission");

            entity.ToTable("Permission");

            entity.Property(e => e.ActionMethodName).HasMaxLength(2050);
            entity.Property(e => e.CreatedBy).HasMaxLength(550);
            entity.Property(e => e.CreatedByUserId).HasMaxLength(150);
            entity.Property(e => e.Description).HasMaxLength(2050);
            entity.Property(e => e.ImagePath).HasMaxLength(2050);
            entity.Property(e => e.LinkId).HasColumnName("Link_Id");
            entity.Property(e => e.LinkName).HasMaxLength(550);
            entity.Property(e => e.Name).HasMaxLength(550);
            entity.Property(e => e.Url).HasMaxLength(2050);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Person");

            entity.ToTable("Person");

            entity.Property(e => e.BloodGroup).HasMaxLength(5);
            entity.Property(e => e.Caste).HasMaxLength(2050);
            entity.Property(e => e.Cnic)
                .HasMaxLength(2050)
                .HasColumnName("CNIC");
            entity.Property(e => e.CnicrelationId).HasColumnName("CNICRelation_Id");
            entity.Property(e => e.CnicrelationName)
                .HasMaxLength(550)
                .HasColumnName("CNICRelationName");
            entity.Property(e => e.CorrespondenceAddress).HasMaxLength(2050);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.DomicileId).HasColumnName("Domicile_Id");
            entity.Property(e => e.Email).HasMaxLength(55);
            entity.Property(e => e.EmailSecondary).HasMaxLength(55);
            entity.Property(e => e.EntityId).HasColumnName("Entity_Id");
            entity.Property(e => e.FatherName).HasMaxLength(250);
            entity.Property(e => e.FirstName).HasMaxLength(2050);
            entity.Property(e => e.Gender).HasMaxLength(2050);
            entity.Property(e => e.HealthFacilityName).HasMaxLength(500);
            entity.Property(e => e.Hfmiscode)
                .HasMaxLength(250)
                .HasColumnName("HFMISCode");
            entity.Property(e => e.IsRegisteredHmis).HasColumnName("IsRegisteredHMIS");
            entity.Property(e => e.LandlineNumber).HasMaxLength(15);
            entity.Property(e => e.LastName).HasMaxLength(2050);
            entity.Property(e => e.MaritalStatusId).HasColumnName("MaritalStatus_Id");
            entity.Property(e => e.MiddleName).HasMaxLength(2050);
            entity.Property(e => e.MobileNumber).HasMaxLength(15);
            entity.Property(e => e.MobileNumberSecondary).HasMaxLength(15);
            entity.Property(e => e.NameTitle).HasMaxLength(2050);
            entity.Property(e => e.Nationality).HasMaxLength(2050);
            entity.Property(e => e.PassportNo).HasMaxLength(2050);
            entity.Property(e => e.PermanentAddress).HasMaxLength(2050);
            entity.Property(e => e.PersonTypeId).HasColumnName("PersonType_Id");
            entity.Property(e => e.PlaceOfBirth).HasMaxLength(2050);
            entity.Property(e => e.PostalCode).HasMaxLength(2050);
            entity.Property(e => e.ReligionId).HasColumnName("Religion_Id");
            entity.Property(e => e.Source).HasMaxLength(2050);
        });

        modelBuilder.Entity<PrincipalExperience>(entity =>
        {
            entity.ToTable("PrincipalExperience");

            entity.Property(e => e.GrantApplicationId).HasColumnName("GrantApplication_Id");
            entity.Property(e => e.JobTitle).HasMaxLength(2050);
            entity.Property(e => e.Organization).HasMaxLength(2050);
            entity.Property(e => e.UploadPath).HasMaxLength(2050);
            entity.Property(e => e.UserId).HasMaxLength(2050);
        });

        modelBuilder.Entity<PrincipalQualification>(entity =>
        {
            entity.ToTable("PrincipalQualification");

            entity.Property(e => e.GrantApplicationId).HasColumnName("GrantApplication_Id");
            entity.Property(e => e.QualificationId).HasColumnName("Qualification_Id");
            entity.Property(e => e.QualificationName)
                .HasMaxLength(2050)
                .HasColumnName("Qualification_Name");
            entity.Property(e => e.UploadPath).HasMaxLength(2050);
            entity.Property(e => e.UserId).HasMaxLength(2050);
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.ToTable("Profile");

            entity.Property(e => e.ProfileId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.ShortName).HasMaxLength(150);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.ProfileType).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.ProfileTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Profile_ProfileType");
        });

        modelBuilder.Entity<ProfileType>(entity =>
        {
            entity.ToTable("ProfileType");

            entity.Property(e => e.ProfileTypeId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.ShortName).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("Project");

            entity.Property(e => e.AuthKey).HasMaxLength(2050);
            entity.Property(e => e.Name).HasMaxLength(2050);
        });

        modelBuilder.Entity<ProjectRole>(entity =>
        {
            entity.ToTable("ProjectRole");

            entity.Property(e => e.ProjectId).HasColumnName("Project_Id");
            entity.Property(e => e.RoleId)
                .HasMaxLength(250)
                .HasColumnName("Role_Id");
        });

        modelBuilder.Entity<ProjectUser>(entity =>
        {
            entity.ToTable("ProjectUser");

            entity.Property(e => e.ProjectId).HasColumnName("Project_Id");
            entity.Property(e => e.UserId)
                .HasMaxLength(250)
                .HasColumnName("User_Id");
        });

        modelBuilder.Entity<Proposal>(entity =>
        {
            entity.ToTable("Proposal");

            entity.Property(e => e.GrantApplicationId).HasColumnName("GrantApplication_Id");
            entity.Property(e => e.UserId).HasMaxLength(2050);
        });

        modelBuilder.Entity<ProposalProbe>(entity =>
        {
            entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GrantApplicationId).HasColumnName("GrantApplication_Id");
            entity.Property(e => e.ProbeId).HasColumnName("Probe_Id");
            entity.Property(e => e.ProbeName).HasMaxLength(2050);
            entity.Property(e => e.ProposalId).HasColumnName("Proposal_Id");
            entity.Property(e => e.ProposalReport).HasMaxLength(2050);
            entity.Property(e => e.RiskStatement).HasMaxLength(2050);
            entity.Property(e => e.UserId).HasMaxLength(2050);
        });

        modelBuilder.Entity<ProposalView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ProposalView");

            entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GrantApplicationId).HasColumnName("GrantApplication_Id");
            entity.Property(e => e.ProbeId).HasColumnName("Probe_Id");
            entity.Property(e => e.ProbeName).HasMaxLength(2050);
            entity.Property(e => e.ProposalId1).HasColumnName("Proposal_Id");
            entity.Property(e => e.ProposalProbeUserId).HasMaxLength(2050);
            entity.Property(e => e.ProposalReport).HasMaxLength(2050);
            entity.Property(e => e.ProposalUserId).HasMaxLength(2050);
            entity.Property(e => e.RiskStatement).HasMaxLength(2050);
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.ToTable("Province");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PublicUser>(entity =>
        {
            entity.ToTable("PublicUser");

            entity.Property(e => e.PublicUserId).ValueGeneratedNever();
            entity.Property(e => e.Cnic)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.GenderProfileId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsShowHfadmin).HasColumnName("IsShowHFAdmin");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.RoleType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RoutingUrl)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ShortName)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SyncDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<RoleMenu>(entity =>
        {
            entity.ToTable("RoleMenu");

            entity.Property(e => e.RoleMenuId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.HasAccess)
                .IsRequired()
                .HasDefaultValueSql("('0')");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Menu).WithMany(p => p.RoleMenus)
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleMenu_Menu");

            entity.HasOne(d => d.Role).WithMany(p => p.RoleMenus)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RoleMenu_Roles");
        });

        modelBuilder.Entity<Scholarship>(entity =>
        {
            entity.ToTable("Scholarship");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GrantApplicationId).HasColumnName("GrantApplication_Id");
            entity.Property(e => e.GrantName).HasMaxLength(2050);
            entity.Property(e => e.ScholarshipType).HasMaxLength(2050);
        });

        modelBuilder.Entity<ServiceProvider>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Banner).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DistrictCode).HasMaxLength(50);
            entity.Property(e => e.DivisionCde).HasMaxLength(50);
            entity.Property(e => e.Logo).HasMaxLength(50);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OwneCnic)
                .HasMaxLength(50)
                .HasColumnName("OwneCNIC");
            entity.Property(e => e.OwnerMob).HasMaxLength(50);
            entity.Property(e => e.OwnerName).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ServiceProviderHealthFacility>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("ServiceProviderHealthFacility");
        });

        modelBuilder.Entity<ServiceType>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.DisplayName).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<SmsLog>(entity =>
        {
            entity.ToTable("SMS_Log");

            entity.Property(e => e.Fkid).HasColumnName("FKId");
            entity.Property(e => e.Mask).HasMaxLength(50);
            entity.Property(e => e.Message).HasMaxLength(4000);
            entity.Property(e => e.MessageId).HasMaxLength(50);
            entity.Property(e => e.Number).HasMaxLength(50);
            entity.Property(e => e.Otp)
                .HasMaxLength(50)
                .HasColumnName("OTP");
            entity.Property(e => e.SmsSessionId).HasColumnName("SMS_Session_Id");
            entity.Property(e => e.SmsrecieverUserId)
                .HasMaxLength(2050)
                .HasColumnName("SMSReciever_UserId");
            entity.Property(e => e.Status).HasMaxLength(550);
            entity.Property(e => e.UserId).HasMaxLength(550);
        });

        modelBuilder.Entity<SmsSession>(entity =>
        {
            entity.ToTable("SMS_Session");

            entity.Property(e => e.RequestedBy).HasMaxLength(250);
            entity.Property(e => e.SessionId).HasMaxLength(550);
        });

        modelBuilder.Entity<SourceSystem>(entity =>
        {
            entity.ToTable("SourceSystem");

            entity.Property(e => e.SourceSystemId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ShortName)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<SpEmployee>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SpEmployee");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<SpInvoice>(entity =>
        {
            entity.HasKey(e => e.InvoiceId);

            entity.ToTable("SpInvoice");

            entity.Property(e => e.ApprovedOn).HasColumnType("datetime");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.InvoiceNumber).HasMaxLength(50);
            entity.Property(e => e.IsRejectedOn).HasColumnType("datetime");
            entity.Property(e => e.IssuedOn).HasColumnType("datetime");
            entity.Property(e => e.Month)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReIssuedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<SpService>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Sp).WithMany(p => p.SpServices)
                .HasForeignKey(d => d.SpId)
                .HasConstraintName("FK_SpServices_ServiceProviders");
        });

        modelBuilder.Entity<StakeHolderType>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.DisplayName).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<SyncDataLog>(entity =>
        {
            entity.ToTable("SyncDataLog");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.LastSync).HasColumnType("datetime");
            entity.Property(e => e.ResourceSystem)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.TableName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Table1>(entity =>
        {
            entity.ToTable("Table_1");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<TbMedicineDeliveryDatum>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AdditionalMedicineAdvised)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Age).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Cnic)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.DateOfExamination).HasColumnType("datetime");
            entity.Property(e => e.DeliveryOn).HasColumnType("datetime");
            entity.Property(e => e.DiseaseSite)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Dose)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DurationInWeeks)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FatherName).HasMaxLength(150);
            entity.Property(e => e.Frequency)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacility)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacilityDistrict)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacilityTehsil)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.MrNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NameOfMedicineAdvised)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.PackingOn).HasColumnType("datetime");
            entity.Property(e => e.PatientDistrict)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PatientDivision)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PatientName).HasMaxLength(150);
            entity.Property(e => e.PatientTehsil)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNo1)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNo2)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegimenType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");
            entity.Property(e => e.SentForPackingOn).HasColumnType("datetime");
            entity.Property(e => e.TreatmentPhase)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.TypeOfPatient)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Weight)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tehsil>(entity =>
        {
            entity.ToTable("Tehsil");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.District).WithMany(p => p.Tehsils)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK_Tehsil_District");
        });

        modelBuilder.Entity<TempMedicinePrice>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.MedicineName).HasMaxLength(500);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");
        });

        modelBuilder.Entity<TempMimsmedicineListPrice>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TempMIMSMedicineListPrice");

            entity.Property(e => e.MedicineName).HasMaxLength(100);
            entity.Property(e => e.MedicineType).HasMaxLength(500);
        });

        modelBuilder.Entity<Uc>(entity =>
        {
            entity.ToTable("Uc");

            entity.Property(e => e.UcId).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UnionCouncil>(entity =>
        {
            entity.ToTable("UnionCouncil");

            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Tehsil).WithMany(p => p.UnionCouncils)
                .HasForeignKey(d => d.TehsilId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UnionCouncil_Tehsil");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.Email, "Unique_Email").IsUnique();

            entity.HasIndex(e => e.Username, "Unique_Username").IsUnique();

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .HasColumnName("CNIC");
            entity.Property(e => e.ContactNo).HasMaxLength(15);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.CurrentGradeBps)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CurrentGradeBPS");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FatherName).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.IsShowRoleOnly).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsUserLoginFirstTime).HasDefaultValueSql("((1))");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PasswordChangedOn).HasColumnType("datetime");
            entity.Property(e => e.PmisUser).HasColumnName("PmisUSER");
            entity.Property(e => e.ProfilePic)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(150);

            entity.HasOne(d => d.DesignationProfile).WithMany(p => p.Users)
                .HasForeignKey(d => d.DesignationProfileId)
                .HasConstraintName("FK_Users_Profile");

            entity.HasOne(d => d.District).WithMany(p => p.Users)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK_Users_District");

            entity.HasOne(d => d.Division).WithMany(p => p.Users)
                .HasForeignKey(d => d.DivisionId)
                .HasConstraintName("FK_Users_Division");

            entity.HasOne(d => d.HealthFacility).WithMany(p => p.Users)
                .HasForeignKey(d => d.HealthFacilityId)
                .HasConstraintName("FK_Users_HealthFacility");

            entity.HasOne(d => d.Province).WithMany(p => p.Users)
                .HasForeignKey(d => d.ProvinceId)
                .HasConstraintName("FK_Users_Province");

            entity.HasOne(d => d.Tehsil).WithMany(p => p.Users)
                .HasForeignKey(d => d.TehsilId)
                .HasConstraintName("FK_Users_Tehsil");
        });

        modelBuilder.Entity<UserAssignableRole>(entity =>
        {
            entity.HasKey(e => e.AssignableUserRoleId);

            entity.Property(e => e.AssignableUserRoleId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<UserLog>(entity =>
        {
            entity.ToTable("UserLog");

            entity.Property(e => e.AccessToken).IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.ExpireOn).HasColumnType("datetime");
            entity.Property(e => e.HealthFacilityCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RefreshToken).IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<UserPermission>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserPermission");

            entity.ToTable("UserPermission");

            entity.Property(e => e.CreatedBy).HasMaxLength(550);
            entity.Property(e => e.CreatedByUserId).HasMaxLength(150);
            entity.Property(e => e.Description).HasMaxLength(2050);
            entity.Property(e => e.Remarks).HasMaxLength(2050);
            entity.Property(e => e.RoleId).HasMaxLength(150);
            entity.Property(e => e.UserId).HasMaxLength(150);
        });

        modelBuilder.Entity<UserRegistrationLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserRegistrationLog");

            entity.Property(e => e.ActionDate).HasColumnType("datetime");
            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .HasColumnName("CNIC");
            entity.Property(e => e.ContactNo).HasMaxLength(15);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.CurrentGradeBps)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CurrentGradeBPS");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FatherName).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.IsShowRoleOnly).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsUserLoginFirstTime).HasDefaultValueSql("((1))");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PasswordChangedOn).HasColumnType("datetime");
            entity.Property(e => e.PmisUser).HasColumnName("PmisUSER");
            entity.Property(e => e.ProfilePic)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(150);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.Property(e => e.UserRoleId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Roles");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRoles_Users");
        });

        modelBuilder.Entity<ViewAllInvoiceStatus>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewAllInvoiceStatus");

            entity.Property(e => e.Banner).HasMaxLength(50);
            entity.Property(e => e.District)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DistrictCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Division)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.HealthFacilityName).IsUnicode(false);
            entity.Property(e => e.HealthFacilityTypeCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Hftype)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("HFType");
            entity.Property(e => e.InvoiceNumber).HasMaxLength(50);
            entity.Property(e => e.InvoiceStatus)
                .HasMaxLength(8)
                .IsUnicode(false);
            entity.Property(e => e.IssuedOn).HasColumnType("datetime");
            entity.Property(e => e.Logo).HasMaxLength(50);
            entity.Property(e => e.Month)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Service).HasMaxLength(50);
            entity.Property(e => e.SpName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewDoctorNote>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewDoctorNotes");

            entity.Property(e => e.AdvisedBy).HasMaxLength(150);
            entity.Property(e => e.AdvisedOn).HasColumnType("datetime");
            entity.Property(e => e.Notes).IsUnicode(false);
            entity.Property(e => e.PatientvisitId).HasColumnName("patientvisitId");
        });

        modelBuilder.Entity<ViewGetAllIpsychologicalAssessmentQuestion>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewGetAllIPsychologicalAssessmentQuestions");

            entity.Property(e => e.Option1).HasMaxLength(200);
            entity.Property(e => e.Option2).HasMaxLength(200);
            entity.Property(e => e.Option3).HasMaxLength(200);
            entity.Property(e => e.Option4).HasMaxLength(200);
            entity.Property(e => e.Option5).HasMaxLength(200);
            entity.Property(e => e.Option6).HasMaxLength(200);
            entity.Property(e => e.Option7).HasMaxLength(200);
            entity.Property(e => e.Option8).HasMaxLength(200);
            entity.Property(e => e.Option9).HasMaxLength(200);
            entity.Property(e => e.Question).HasMaxLength(150);
            entity.Property(e => e.ShortName).HasMaxLength(50);
        });

        modelBuilder.Entity<ViewGetAllRoleMenuAccess>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewGetAllRoleMenuAccess");

            entity.Property(e => e.DisplayName).HasMaxLength(100);
            entity.Property(e => e.Icon).HasMaxLength(100);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.IsApi).HasColumnName("IsAPI");
            entity.Property(e => e.Name).HasMaxLength(250);
            entity.Property(e => e.Url)
                .HasMaxLength(100)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<ViewGetCreateRoleMenuAccess>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewGetCreateRoleMenuAccess");

            entity.Property(e => e.DisplayName).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(250);
        });

        modelBuilder.Entity<ViewGetEditRoleMenuAccess>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewGetEditRoleMenuAccess");

            entity.Property(e => e.DisplayName).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(250);
        });

        modelBuilder.Entity<ViewHfLocation>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewHfLocation");

            entity.Property(e => e.DistrictCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DivisionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacilityCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacilityName).IsUnicode(false);
            entity.Property(e => e.ProvinceCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProvinceName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TehsilName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewInvoiceStatusCount>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewInvoiceStatusCounts");
        });

        modelBuilder.Entity<ViewLocation>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewLocation");

            entity.Property(e => e.DistrictCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DivisionCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DivisionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacilityCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacilityName).IsUnicode(false);
            entity.Property(e => e.ProvinceCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProvinceName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TehsilName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewSpEmployeesListForInvoice>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewSpEmployeesListForInvoice");

            entity.Property(e => e.Designation).HasMaxLength(50);
            entity.Property(e => e.EmploymentTypes).HasMaxLength(50);
            entity.Property(e => e.HealthFacility).IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Service).HasMaxLength(50);
            entity.Property(e => e.ServiceProvider)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Shifts).HasMaxLength(50);
        });

        modelBuilder.Entity<ViewSpecialityRoomNo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewSpecialityRoomNo");

            entity.Property(e => e.AlmonerFloorNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AlmonerRoomNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DoctorFloorNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.DoctorRoomNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PathalogyFloorNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PathalogyRoomNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PharmacyFloorNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PharmacyRoomNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VitalsFloorNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VitalsRoomNo)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
