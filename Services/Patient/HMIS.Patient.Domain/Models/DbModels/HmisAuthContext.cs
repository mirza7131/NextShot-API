using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HMIS.Patient.Domain.Models.DbModels;

public partial class HmisAuthContext : DbContext
{
    public HmisAuthContext()
    {
    }

    public HmisAuthContext(DbContextOptions<HmisAuthContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<AuditLog> AuditLogs { get; set; }

    public virtual DbSet<DeadBodiesListView> DeadBodiesListViews { get; set; }

    public virtual DbSet<DentalSterilizationRecord> DentalSterilizationRecords { get; set; }

    public virtual DbSet<DepartmentLookup> DepartmentLookups { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Division> Divisions { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<EventVisit> EventVisits { get; set; }

    public virtual DbSet<Feature> Features { get; set; }

    public virtual DbSet<FeePayment> FeePayments { get; set; }

    public virtual DbSet<ViewFeePayment> ViewFeePayments { get; set; }
    public virtual DbSet<GetAdditionalInfo> GetAdditionalInfos { get; set; }

    public virtual DbSet<GetAllMlcQueDatum> GetAllMlcQueData { get; set; }
    public virtual DbSet<GetAllmenuForSuperAdmin> GetAllmenuForSuperAdmins { get; set; }

    public virtual DbSet<HealthFacility> HealthFacilities { get; set; }

    public virtual DbSet<HealthFacilityCategory> HealthFacilityCategories { get; set; }

    public virtual DbSet<HealthFacilityStation> HealthFacilityStations { get; set; }

    public virtual DbSet<HealthFacilityType> HealthFacilityTypes { get; set; }

    public virtual DbSet<HfDepartment> HfDepartments { get; set; }

    public virtual DbSet<HfDepartmentSection> HfDepartmentSections { get; set; }

    public virtual DbSet<HfLabTestConfig> HfLabTestConfigs { get; set; }
    public virtual DbSet<PatientEyeBlindness> PatientEyeBlindnesses { get; set; }

    public virtual DbSet<ImageBaseSixtyFour> ImageBaseSixtyFours { get; set; }

    public virtual DbSet<LabTest> LabTests { get; set; }

    public virtual DbSet<LabTestDetail> LabTestDetails { get; set; }

    public virtual DbSet<MedicineAdvised> MedicineAdviseds { get; set; }

    public virtual DbSet<MedicineAdvisedRequisition> MedicineAdvisedRequisitions { get; set; }

    public virtual DbSet<MedicineDispatch> MedicineDispatches { get; set; }

    public virtual DbSet<MedicineLookup> MedicineLookups { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }
    public virtual DbSet<MimsMedicineDatum> MimsMedicineData { get; set; }
    public virtual DbSet<MimsMedicineIndentLog> MimsMedicineIndentLog { get; set; }
    public virtual DbSet<MimsMedicineIndentDetail> MimsMedicineIndentDetail { get; set; }
    public virtual DbSet<NadraPatientImagesDetailView> NadraPatientImagesDetailViews { get; set; }

    public virtual DbSet<OutSourceSystemLog> OutSourceSystemLogs { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }
    public virtual DbSet<PatientAdditionalInfo> PatientAdditionalInfos { get; set; }

    public virtual DbSet<PatientAdmissionDetail> PatientAdmissionDetails { get; set; }

    public virtual DbSet<PatientAdmissionDetailLog> PatientAdmissionDetailLogs { get; set; }

    public virtual DbSet<PatientAdmissionFlowLog> PatientAdmissionFlowLogs { get; set; }

    public virtual DbSet<ViewPatientLabTestListsForTb> ViewPatientLabTestListsForTbs { get; set; }

    public virtual DbSet<PatientAssessment> PatientAssessments { get; set; }

    public virtual DbSet<PatientCoMorbid> PatientCoMorbids { get; set; }

    public virtual DbSet<PatientConfirmedDisease> PatientConfirmedDiseases { get; set; }

    public virtual DbSet<PatientContactDetail> PatientContactDetails { get; set; }

    public virtual DbSet<PatientDiagnose> PatientDiagnoses { get; set; }

    public virtual DbSet<PatientDiagnoseDisease> PatientDiagnoseDiseases { get; set; }

    public virtual DbSet<PatientDiagnoseProcedure> PatientDiagnoseProcedures { get; set; }

    public virtual DbSet<PatientDiagnoseTemplate> PatientDiagnoseTemplates { get; set; }

    public virtual DbSet<PatientDiagnosisRecord> PatientDiagnosisRecords { get; set; }

    public virtual DbSet<PatientDischargeDetail> PatientDischargeDetails { get; set; }

    public virtual DbSet<PatientDocument> PatientDocuments { get; set; }

    public virtual DbSet<PatientDocumentLog> PatientDocumentLogs { get; set; }

    public virtual DbSet<PatientDrugAddiction> PatientDrugAddictions { get; set; }

    public virtual DbSet<PatientImage> PatientImages { get; set; }

    public virtual DbSet<PatientLabTest> PatientLabTests { get; set; }

    public virtual DbSet<PatientLabTestDetail> PatientLabTestDetails { get; set; }

    public virtual DbSet<PatientLocationPrefix> PatientLocationPrefixes { get; set; }

    public virtual DbSet<PatientLog> PatientLogs { get; set; }

    public virtual DbSet<PatientNadraInfoResponse> PatientNadraInfoResponses { get; set; }

    public virtual DbSet<PatientNadraRequest> PatientNadraRequests { get; set; }

    public virtual DbSet<PatientNadraResponse> PatientNadraResponses { get; set; }
    public virtual DbSet<SpeechAndLanguageHistory> SpeechAndLanguageHistories { get; set; }
    public virtual DbSet<PatientOpenVisit> PatientOpenVisits { get; set; }
    public virtual DbSet<PastPsychiatricHistory> PastPsychiatricHistories { get; set; }

    public virtual DbSet<PsychologicalAssessment> PsychologicalAssessments { get; set; }

    public virtual DbSet<PsychologicalTestApplied> PsychologicalTestApplieds { get; set; }
    public virtual DbSet<PatientOpenVisitLog> PatientOpenVisitLogs { get; set; }

    public virtual DbSet<PatientPrescription> PatientPrescriptions { get; set; }

    public virtual DbSet<PatientScreening> PatientScreenings { get; set; }

    public virtual DbSet<PatientSourceInfo> PatientSourceInfos { get; set; }

    public virtual DbSet<SpeechDisorder> SpeechDisorders { get; set; }

    public virtual DbSet<PatientStatusBySpeciality> PatientStatusBySpecialities { get; set; }

    public virtual DbSet<PatientStatusBySpeciality1> PatientStatusBySpecialities1 { get; set; }

    public virtual DbSet<PatientVaccination> PatientVaccinations { get; set; }

    public virtual DbSet<PatientVisitFlow> PatientVisitFlows { get; set; }

    public virtual DbSet<PatientVisitToken> PatientVisitTokens { get; set; }

    public virtual DbSet<PatientVital> PatientVitals { get; set; }

    public virtual DbSet<PatientWorkFlowLog> PatientWorkFlowLogs { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<PhysiotherapyForm> PhysiotherapyForms { get; set; }

    public virtual DbSet<PhysiotherapyModality> PhysiotherapyModalities { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<ProfileType> ProfileTypes { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<RiderStatusLog> RiderStatusLogs { get; set; }

    public virtual DbSet<RiskFactor> RiskFactors { get; set; }

    public virtual DbSet<PsychologyAssociatedDisorder> PsychologyAssociatedDisorders { get; set; }

    public virtual DbSet<PsychologyDisorder> PsychologyDisorders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleMenu> RoleMenus { get; set; }

    public virtual DbSet<SampleConsignment> SampleConsignments { get; set; }

    public virtual DbSet<SampleConsignmentDetail> SampleConsignmentDetails { get; set; }

    public virtual DbSet<SectionLookup> SectionLookups { get; set; }

    public virtual DbSet<SectionProcedure> SectionProcedures { get; set; }

    public virtual DbSet<SocialWelfareAssignDoctor> SocialWelfareAssignDoctors { get; set; }

    public virtual DbSet<PsychologyPatientModality> PsychologyPatientModalities { get; set; }

    public virtual DbSet<SocialWelfareForm> SocialWelfareForms { get; set; }

    public virtual DbSet<SocialWelfareTaskPerformedByCd> SocialWelfareTaskPerformedByCds { get; set; }

    public virtual DbSet<SourceSystem> SourceSystems { get; set; }
    public virtual DbSet<DoctorNote> DoctorNotes { get; set; }
    public virtual DbSet<SscVisitStatusLog> SscVisitStatusLogs { get; set; }

    public virtual DbSet<PhysiotherapyHomeExercisePlan> PhysiotherapyHomeExercisePlans { get; set; }

    public virtual DbSet<SyncDataLog> SyncDataLogs { get; set; }

    public virtual DbSet<TbPatientDetail> TbPatientDetails { get; set; }

    public virtual DbSet<Tehsil> Tehsils { get; set; }

    public virtual DbSet<UnionCouncil> UnionCouncils { get; set; }

    public virtual DbSet<DevelopmentMilestone> DevelopmentMilestones { get; set; }
    public virtual DbSet<SpeechMilestone> SpeechMilestones { get; set; }

    public virtual DbSet<EducationalHistory> EducationalHistories { get; set; }


    public virtual DbSet<FamiliyHistory> FamiliyHistories { get; set; }

    public virtual DbSet<HearingProblemHistory> HearingProblemHistories { get; set; }
    public virtual DbSet<SpeechTherapyPatientAssessment> SpeechTherapyPatientAssessments { get; set; }

    public virtual DbSet<PatientBmi> PatientBmis { get; set; }

    public virtual DbSet<PatientNutritionalRisk> PatientNutritionalRisks { get; set; }

    public virtual DbSet<NursingEvent> NursingEvents { get; set; }

    public virtual DbSet<NutritionalAsessmentFinding> NutritionalAsessmentFindings { get; set; }


    public virtual DbSet<AssociatedDisorder> AssociatedDisorders { get; set; }

    public virtual DbSet<PatientComorbidity> PatientComorbidities { get; set; }

    public virtual DbSet<PatientMalnutrition> PatientMalnutritions { get; set; }

    public virtual DbSet<PatientDietPlan> PatientDietPlans { get; set; }

    public virtual DbSet<UnknownPatient> UnknownPatients { get; set; }

    public virtual DbSet<UnknownPatientsListView> UnknownPatientsListViews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAssignableRole> UserAssignableRoles { get; set; }

    public virtual DbSet<UserLog> UserLogs { get; set; }

    public virtual DbSet<UserRegistrationLog> UserRegistrationLogs { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<SpeechModality> SpeechModalities { get; set; }

    public virtual DbSet<VHfdepartmentSection> VHfdepartmentSections { get; set; }
    public virtual DbSet<ViewDoctorNote> ViewDoctorNotes { get; set; }
    public virtual DbSet<ViewAdviseLabTest> ViewAdviseLabTests { get; set; }

    public virtual DbSet<ViewDrugAddictCommunityDevelopment> ViewDrugAddictCommunityDevelopments { get; set; }

    public virtual DbSet<ViewSpecialityRoomNo> ViewSpecialityRoomNos { get; set; }

    public virtual DbSet<ViewDrugAddictFieldOfficer> ViewDrugAddictFieldOfficers { get; set; }

    public virtual DbSet<ViewDrugAddictPatientDisease> ViewDrugAddictPatientDiseases { get; set; }

    public virtual DbSet<ViewDrugAddictPatientDiseasesCommaSeparated> ViewDrugAddictPatientDiseasesCommaSeparateds { get; set; }

    public virtual DbSet<ViewDrugAddictsPatientVisit> ViewDrugAddictsPatientVisits { get; set; }

	public virtual DbSet<ViewGetAllIpdQueue> ViewGetAllIpdQueues { get; set; }

    public virtual DbSet<ViewGetAllMedicineAdvisedRequisition> ViewGetAllMedicineAdvisedRequisitions { get; set; }
    public virtual DbSet<ViewGetAllPatientsCountByPtStatusCd> ViewGetAllPatientsCountByPtStatusCds { get; set; }

    public virtual DbSet<ViewGetAllPatientsCountCd> ViewGetAllPatientsCountCds { get; set; }

    public virtual DbSet<ViewGetAllPatientsCountSw> ViewGetAllPatientsCountSws { get; set; }

    public virtual DbSet<ViewGetAllRoleMenuAccess> ViewGetAllRoleMenuAccesses { get; set; }

    public virtual DbSet<ViewGetCreateRoleMenuAccess> ViewGetCreateRoleMenuAccesses { get; set; }

    public virtual DbSet<ViewGetEditRoleMenuAccess> ViewGetEditRoleMenuAccesses { get; set; }

    public virtual DbSet<ViewGetPatientBaseSixtyFour> ViewGetPatientBaseSixtyFours { get; set; }

    public virtual DbSet<ViewGetPatientLabTestList> ViewGetPatientLabTestLists { get; set; }

    public virtual DbSet<ViewGetPatientPrescriptionList> ViewGetPatientPrescriptionLists { get; set; }

    public virtual DbSet<ViewHealthFacilityDepartmentList> ViewHealthFacilityDepartmentLists { get; set; }

    public virtual DbSet<ViewHfLocation> ViewHfLocations { get; set; }

    public virtual DbSet<ViewLabTestResult> ViewLabTestResults { get; set; }

    public virtual DbSet<ViewLocation> ViewLocations { get; set; }

    public virtual DbSet<ViewMedicineDispatchList> ViewMedicineDispatchLists { get; set; }

    public virtual DbSet<ViewPatientDiagnoseDetail> ViewPatientDiagnoseDetails { get; set; }
    public virtual DbSet<ViewPatientDischargeList> ViewPatientDischargeLists { get; set; }

    public virtual DbSet<ViewPatientLabTestDetail> ViewPatientLabTestDetails { get; set; }

    public virtual DbSet<ViewPatientLabTestList> ViewPatientLabTestLists { get; set; }

    public virtual DbSet<ViewPatientOpenVisiDashbaordList> ViewPatientOpenVisiDashbaordLists { get; set; }

    public virtual DbSet<ViewPatientOpenVisit> ViewPatientOpenVisits { get; set; }

    public virtual DbSet<ViewPatientOpenVisitCountByDeptBySecByMonth> ViewPatientOpenVisitCountByDeptBySecByMonths { get; set; }

    public virtual DbSet<ViewPatientOpenVisitCountByGenderByMonth> ViewPatientOpenVisitCountByGenderByMonths { get; set; }

    public virtual DbSet<ViewPatientOpenVisitDetail> ViewPatientOpenVisitDetails { get; set; }

    public virtual DbSet<ViewPatientOpenVisitList> ViewPatientOpenVisitLists { get; set; }

    public virtual DbSet<ViewPatientPharmacyDetail> ViewPatientPharmacyDetails { get; set; }

    public virtual DbSet<ViewPatientRegistrationDetail> ViewPatientRegistrationDetails { get; set; }

    public virtual DbSet<ViewPatientVisitFlow> ViewPatientVisitFlows { get; set; }

    public virtual DbSet<ViewPatientVitalDetail> ViewPatientVitalDetails { get; set; }

    public virtual DbSet<ViewPatientVitalList> ViewPatientVitalLists { get; set; }

    public virtual DbSet<ViewRiderLabTestList> ViewRiderLabTestLists { get; set; }

    public virtual DbSet<ViewSampleCollectedConsignmentList> ViewSampleCollectedConsignmentLists { get; set; }

    public virtual DbSet<ViewSampleConsignmentList> ViewSampleConsignmentLists { get; set; }

    public virtual DbSet<ViewSampleConsignmentWithDetailList> ViewSampleConsignmentWithDetailLists { get; set; }

    public virtual DbSet<ViewSocialWellfareDeputyDirector> ViewSocialWellfareDeputyDirectors { get; set; }

    public virtual DbSet<ViewSocialWellfareDoctor> ViewSocialWellfareDoctors { get; set; }

    public virtual DbSet<ViewSocialWellfarePatientDetail> ViewSocialWellfarePatientDetails { get; set; }

    public virtual DbSet<ViewTbPatientCount> ViewTbPatientCounts { get; set; }

    public virtual DbSet<ViewTbPatientsList> ViewTbPatientsLists { get; set; }

    public virtual DbSet<ViewTbRegisteredPatient> ViewTbRegisteredPatients { get; set; }

    public virtual DbSet<ViewTblabTest> ViewTblabTests { get; set; }

    public virtual DbSet<ViewTodayPatientOpenVisitCountByDeptBySec> ViewTodayPatientOpenVisitCountByDeptBySecs { get; set; }
    public virtual DbSet<ViewTbissuedMedicine> ViewTbissuedMedicines { get; set; }
    public virtual DbSet<ViewEyeBlindnesspatient> ViewEyeBlindnesspatients { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=172.16.15.5;Database=HMIS-Auth;Persist Security Info=False;User Id=usman;Password=asd@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=400;");

    /// <summary>
    //**********Restored HMIS_Auth****************//
    /// </summary>
    /// <param name="optionsBuilder"></param>
    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=172.16.0.18;Database=HMIS-Auth;Persist Security Info=False;User Id=shoaib;Password=asdf@1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=400;");

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=localhost;Database=HMIS_Prod;Persist Security Info=False;User Id=hmis;Password=hisdu@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    //    //*********** Live ************** //
    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=172.16.0.208;Database=HMIS_Prod;Persist Security Info=False;User Id=hmis;Password=Hisdu@#$%^;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=400;");

   /// <summary>
   //**********Restored HMIS_Auth****************//
   /// </summary>
   /// <param name="optionsBuilder"></param>
   //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
   //        => optionsBuilder.UseSqlServer("Server=172.16.0.18;Database=HMIS-Auth;Persist Security Info=False;User Id=shoaib;Password=asdf@1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=400;");

   //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
   //        => optionsBuilder.UseSqlServer("Server=localhost;Database=HMIS_Prod;Persist Security Info=False;User Id=hmis;Password=hisdu@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

   //*********** Live ************** //
   //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
   //        => optionsBuilder.UseSqlServer("Server=localhost;Database=HMIS_Prod;Persist Security Info=False;User Id=hmis;Password=hisdu@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

   //*********** Live public************** //
   //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
   //        => optionsBuilder.UseSqlServer("Server=119.159.226.181;Database=HMIS_Prod;Persist Security Info=False;User Id=hmis;Password=Hisdu@#$%^;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=400;");

   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.AttachmentId).HasName("PK__Attachme__442C64BE58F8E934");

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



        modelBuilder.Entity<ViewFeePayment>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewFeePayment");

            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.PatientName).HasMaxLength(150);
            entity.Property(e => e.RefundReason)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.SectionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TokenNo)
                .HasMaxLength(10)
                .IsUnicode(false);
        });


        modelBuilder.Entity<FeePayment>(entity =>
        {
            entity.ToTable("FeePayment", "pt");

            entity.Property(e => e.FeePaymentId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.RefundReason)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });


        modelBuilder.Entity<ViewSpecialityRoomNo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewSpecialityRoomNo");

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


        modelBuilder.Entity<DeadBodiesListView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("DeadBodiesListView");

            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.Message).IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.TransactionId).IsUnicode(false);
            entity.Property(e => e.TransctionSaveTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<DentalSterilizationRecord>(entity =>
        {
            entity.HasKey(e => e.DentalSterilizationRecordId).HasName("PK__DentalSt__A6B50EDFBD37B693");

            entity.ToTable("DentalSterilizationRecords", "den");

            entity.Property(e => e.DentalSterilizationRecordId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.DentalSterilizationRecords)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_DentalSterilizationRecords_Users");

            entity.HasOne(d => d.EquipmentProfile).WithMany(p => p.DentalSterilizationRecords)
                .HasForeignKey(d => d.EquipmentProfileId)
                .HasConstraintName("FK_DentalSterilizationRecords_EquipmentProfile");
        });

        modelBuilder.Entity<DepartmentLookup>(entity =>
        {
            entity.ToTable("DepartmentLookup", "lkup");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.DisplayName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
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

        modelBuilder.Entity<EventVisit>(entity =>
        {
            entity.ToTable("EventVisit");

            entity.Property(e => e.EventVisitId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<SpeechModality>(entity =>
        {
            entity.HasKey(e => e.SpeechModalitiesId);

            entity.ToTable("SpeechModalities", "sph");

            entity.Property(e => e.SpeechModalitiesId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Feature>(entity =>
        {
            entity.HasKey(e => e.FeatureId).HasName("PK__Features__82230BC94F9E3F0C");

            entity.Property(e => e.FeatureId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<SpeechDisorder>(entity =>
        {
            entity.ToTable("SpeechDisorder", "sph");

            entity.Property(e => e.SpeechDisorderId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });


        modelBuilder.Entity<ViewPatientLabTestListsForTb>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewPatientLabTestListsForTb");

            entity.Property(e => e.LabTestName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });


        modelBuilder.Entity<GetAdditionalInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GetAdditionalInfo", "pt");

            entity.Property(e => e.CaseAgainst).HasMaxLength(250);
            entity.Property(e => e.Caste).HasMaxLength(150);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.GuardianAddress).HasMaxLength(250);
            entity.Property(e => e.GuardianCnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("GuardianCNIC");
            entity.Property(e => e.GuardianMobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GuardianName).HasMaxLength(150);
            entity.Property(e => e.MlcSvIncidentPlace).HasMaxLength(250);
            entity.Property(e => e.MlcType).HasMaxLength(150);
            entity.Property(e => e.Mlcno).HasColumnName("MLCNo");
            entity.Property(e => e.MlctypeProfileId).HasColumnName("MLCTypeProfileId");
            entity.Property(e => e.MleIncidentPlace).HasMaxLength(250);
            entity.Property(e => e.Occupation).HasMaxLength(150);
            entity.Property(e => e.PmeIncidentPlace).HasMaxLength(250);
        });

        modelBuilder.Entity<GetAllMlcQueDatum>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("GetAllMlcQueData");

            entity.Property(e => e.CNIC)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.PatientCondition).HasMaxLength(150);
            entity.Property(e => e.Rn).HasColumnName("rn");
            entity.Property(e => e.SectionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TokenNo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.VisitDate).HasColumnType("date");
            entity.Property(e => e.VisitFor)
                .HasMaxLength(20)
                .IsUnicode(false);
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


        modelBuilder.Entity<AssociatedDisorder>(entity =>
        {
            entity.ToTable("AssociatedDisorder", "sph");

            entity.Property(e => e.AssociatedDisorderId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<HealthFacility>(entity =>
        {
            entity.ToTable("HealthFacility");

            entity.HasIndex(e => e.Code, "AIH_HealthFacility_Code");

            entity.HasIndex(e => e.CreatedOn, "XIX_HealthFacility_CreatedOn");

            entity.HasIndex(e => e.HealthFacilityTypeCode, "XIX_HealthFacility_HealthFacilityTypeCode");

            entity.HasIndex(e => new { e.HealthFacilityTypeCode, e.ActionTypeId }, "XIX_HealthFacility_HealthFacilityTypeCode_ActionTypeId");

            entity.HasIndex(e => new { e.ProvinceId, e.DivisionId }, "XIX_HealthFacility_ProvinceId_DivisionId");

            entity.HasIndex(e => e.TehsilId, "XI_HealthFacility");

            entity.HasIndex(e => e.CreatedOn, "XXIII_HealthFacility_CreatedOn");

            entity.HasIndex(e => e.CreatedOn, "XXII_HealthFacility_CreatedOn");

            entity.HasIndex(e => e.CreatedOn, "XXI_HealthFacility_CreatedOn");

            entity.HasIndex(e => new { e.HealthFacilityTypeCode, e.ActionTypeId }, "XXI_HealthFacility_HealthFacilityTypeCode_ActionTypeId");

            entity.HasIndex(e => e.CreatedOn, "XX_HealthFacility_CreatedOn");

            entity.HasIndex(e => new { e.HealthFacilityTypeCode, e.ActionTypeId }, "XX_HealthFacility_HealthFacilityTypeCode_ActionTypeId");

            entity.HasIndex(e => new { e.ProvinceId, e.DivisionId }, "XX_HealthFacility_ProvinceId_DivisionId");

            entity.HasIndex(e => e.TehsilId, "X_HealthFacility");

            entity.HasIndex(e => e.HrId, "X_HealthFacility_HrId");

            entity.HasIndex(e => e.TehsilCode, "X_HealthFacility_TehsilCode");

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

            entity.HasOne(d => d.DepartmentLookup).WithMany(p => p.HfDepartments)
                .HasForeignKey(d => d.DepartmentLookupId)
                .HasConstraintName("FK_HFDepartments_DepartmentLookup");

            entity.HasOne(d => d.HealthFacility).WithMany(p => p.HfDepartments)
                .HasForeignKey(d => d.HealthFacilityId)
                .HasConstraintName("FK_HFDepartments_HealthFacility");
        });

        modelBuilder.Entity<HfDepartmentSection>(entity =>
        {
            entity.HasKey(e => e.HfDepartmentSectionId).HasName("PK_HfDepartmentServices");

            entity.ToTable("HfDepartmentSection");

            entity.HasIndex(e => e.SectionLookupId, "AIH_HfDepartmentSection_SectionLookupId");

            entity.HasIndex(e => e.SectionLookupId, "AI_HfDepartmentSection_SectionLookupId");

            entity.HasIndex(e => new { e.HfDepartmentId, e.ActionTypeId }, "X_HfDepartmentSection_HfDepartmentId_ActionTypeId");

            entity.HasIndex(e => new { e.IsActive, e.ActionTypeId }, "X_HfDepartmentSection_IsActive_ActionTypeId");

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

            entity.HasOne(d => d.SectionLookup).WithMany(p => p.HfDepartmentSections)
                .HasForeignKey(d => d.SectionLookupId)
                .HasConstraintName("FK_HfDepartmentSection_SectionLookup");
        });

        modelBuilder.Entity<HfLabTestConfig>(entity =>
        {
            entity.ToTable("HfLabTestConfig");

            entity.Property(e => e.HfLabTestConfigId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

        });



        modelBuilder.Entity<ImageBaseSixtyFour>(entity =>
        {
            entity.ToTable("ImageBaseSixtyFour", "pt");

            entity.Property(e => e.ImageBaseSixtyFourId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<LabTest>(entity =>
        {
            entity.ToTable("LabTest", "pat");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(5000)
                .IsUnicode(false);
            entity.Property(e => e.DoctorShare).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.GovtShare).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.IsSampleRequired)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SampleType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StaffShare).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TestPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.DepartmentProfile).WithMany(p => p.LabTestDepartmentProfiles)
                .HasForeignKey(d => d.DepartmentProfileId)
                .HasConstraintName("FK_LabTest_Profile");

            entity.HasOne(d => d.LabTestCategoryProfile).WithMany(p => p.LabTestLabTestCategoryProfiles)
                .HasForeignKey(d => d.LabTestCategoryProfileId)
                .HasConstraintName("FK_LabTest_Profile1");

            entity.HasOne(d => d.LabTestTypeProfile).WithMany(p => p.LabTestLabTestTypeProfiles)
                .HasForeignKey(d => d.LabTestTypeProfileId)
                .HasConstraintName("FK_LabTest_Profile_LabTestType");
        });

        modelBuilder.Entity<LabTestDetail>(entity =>
        {
            entity.ToTable("LabTestDetail", "pat");

            entity.Property(e => e.ActionTypeId).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.MaxValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MinValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TestName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TestNormalValue)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.TestUnit)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.LabTest).WithMany(p => p.LabTestDetails)
                .HasForeignKey(d => d.LabTestId)
                .HasConstraintName("FK_LabTestDetail_LabTest");
            entity.HasOne(d => d.TestResultDropDownTypeProfile).WithMany(p => p.LabTestDetails)
          .HasForeignKey(d => d.TestResultDropDownTypeProfileId)
          .HasConstraintName("FK_LabTestDetail_Profile");
        });

        modelBuilder.Entity<MedicineAdvised>(entity =>
        {
            entity.ToTable("MedicineAdvised", "emr");

            entity.Property(e => e.MedicineAdvisedId).ValueGeneratedNever();
            entity.Property(e => e.BatchNo).HasMaxLength(200);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.MedicineName).HasMaxLength(200);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<MedicineAdvisedRequisition>(entity =>
        {
            entity.ToTable("MedicineAdvisedRequisition", "pt");

            entity.Property(e => e.MedicineAdvisedRequisitionId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<MedicineDispatch>(entity =>
        {
            entity.ToTable("MedicineDispatch", "pt");

            entity.HasIndex(e => e.PatientDiagnoseId, "AIM_MedicineDispatch_PatientDiagnoseId");

            entity.HasIndex(e => e.MedicineId, "C_MedicineDispatch_MedicineId");

            entity.HasIndex(e => new { e.CreatedOn, e.ActionTypeId }, "IX_MedicineDispatch_CreatedOn_ActionTypeId");

            entity.HasIndex(e => new { e.PatientId, e.PatientVisitId }, "IX_MedicineDispatch_PatientId_PatientVisitId");

            entity.HasIndex(e => e.CreatedOn, "XII_MedicineDispatch_CreatedOn");

            entity.HasIndex(e => e.MedicineId, "XIX_MedicineDispatch_MedicineId");

            entity.HasIndex(e => e.PatientVisitId, "XIX_MedicineDispatch_PatientVisitId");

            entity.HasIndex(e => e.PatientVisitId, "XX_MedicineDispatch_PatientVisitId");

            entity.HasIndex(e => new { e.ActionTypeId, e.MedicineName }, "X_MedicineDispatch_ActionTypeId_MedicineName");

            entity.HasIndex(e => new { e.CreatedBy, e.CreatedOn, e.ActionTypeId }, "X_MedicineDispatch_CreatedBy_CreatedOn_ActionTypeId");

            entity.HasIndex(e => e.CreatedOn, "X_MedicineDispatch_CreatedOn");

            entity.HasIndex(e => e.PatientDiagnoseId, "X_MedicineDispatch_PatientDiagnoseId");

            entity.HasIndex(e => e.PatientPrescriptionId, "X_MedicineDispatch_PatientPrescriptionId");

            entity.HasIndex(e => e.PatientVisitId, "X_MedicineDispatch_PatientVisitId");

            entity.HasIndex(e => new { e.Pharmacist, e.PatientDiagnoseId }, "X_MedicineDispatch_Pharmacist_PatientDiagnoseId");

            entity.Property(e => e.MedicineDispatchId).ValueGeneratedNever();
            entity.Property(e => e.BatchNo).HasMaxLength(200);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.MedicineName).HasMaxLength(200);
            entity.Property(e => e.Mimsdispatched).HasColumnName("MIMSDispatched");
            entity.Property(e => e.Reason).HasMaxLength(200);
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicineDispatches)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicineDispatch_Patient");

            entity.HasOne(d => d.PatientVisit).WithMany(p => p.MedicineDispatches)
                .HasForeignKey(d => d.PatientVisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicineDispatch_PatientOpenVisit");

            entity.HasOne(d => d.PharmacistNavigation).WithMany(p => p.MedicineDispatches)
                .HasForeignKey(d => d.Pharmacist)
                .HasConstraintName("FK_MedicineDispatch_Users");
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

        modelBuilder.Entity<MimsMedicineDatum>(entity =>
        {
            entity.HasKey(e => e.MimsMedicineDataId);

            entity.Property(e => e.MimsMedicineDataId).ValueGeneratedNever();
            entity.Property(e => e.AvailableQuantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalQuantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalDispatchQuantity).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.MedicineName).HasMaxLength(200);
            entity.Property(e => e.MedicineTypeName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.WardName).HasMaxLength(100);
        });

        modelBuilder.Entity<NadraPatientImagesDetailView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("NadraPatientImagesDetailView");

            entity.Property(e => e.Name).HasMaxLength(150);
        });

        modelBuilder.Entity<OutSourceSystemLog>(entity =>
        {
            entity.ToTable("OutSourceSystemLog");

            entity.Property(e => e.OutSourceSystemLogId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.TargetedApiUrl).HasMaxLength(200);
            entity.Property(e => e.UserSystemIp).HasMaxLength(200);
        });


        modelBuilder.Entity<DevelopmentMilestone>(entity =>
        {
            entity.ToTable("DevelopmentMilestones", "sph");

            entity.Property(e => e.DevelopmentMilestoneId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });




        modelBuilder.Entity<EducationalHistory>(entity =>
        {
            entity.ToTable("EducationalHistory", "sph");

            entity.Property(e => e.EducationalHistoryId).ValueGeneratedNever();
            entity.Property(e => e.ChildSchoolGrade)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.SchoolType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });



        modelBuilder.Entity<FamiliyHistory>(entity =>
        {
            entity.ToTable("FamiliyHistory", "sph");

            entity.Property(e => e.FamiliyHistoryId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });



        modelBuilder.Entity<HearingProblemHistory>(entity =>
        {
            entity.ToTable("HearingProblemHistory", "sph");

            entity.Property(e => e.HearingProblemHistoryId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.LevelOfHearingLoss)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NatureOfHearingLoss)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TypeOfHearingLoss)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.UseOfHearingAid)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PatientBmi>(entity =>
        {
            entity.ToTable("PatientBMI", "ntr");

            entity.Property(e => e.PatientBmiid)
                .ValueGeneratedNever()
                .HasColumnName("PatientBMIId");
            entity.Property(e => e.BMIName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BMIName");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });


        modelBuilder.Entity<PatientNutritionalRisk>(entity =>
        {
            entity.ToTable("PatientNutritionalRisk", "ntr");

            entity.Property(e => e.PatientNutritionalRiskId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.NutritionalRisk)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<NursingEvent>(entity =>
        {
            entity.HasKey(e => e.NursingEventsId);

            entity.ToTable("NursingEvents", "pt");

            entity.Property(e => e.NursingEventsId).ValueGeneratedNever();
            entity.Property(e => e.AcknowledgedOn).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Events)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<NutritionalAsessmentFinding>(entity =>
        {
            entity.ToTable("NutritionalAsessmentFindings", "ntr");

            entity.Property(e => e.NutritionalAsessmentFindingId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.PresentingComplaints)
                .HasMaxLength(100)
                .IsUnicode(false); 
            entity.Property(e => e.DietIntakeHistory)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsSignificantExaminationFindings).HasDefaultValueSql("((0))");
            entity.Property(e => e.SignificantExaminationFindings)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });



        modelBuilder.Entity<PatientComorbidity>(entity =>
        {
            entity.ToTable("PatientComorbidity", "ntr");

            entity.Property(e => e.PatientComorbidityId).ValueGeneratedNever();
            entity.Property(e => e.Comorbidity)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientMalnutrition>(entity =>
        {
            entity.ToTable("PatientMalnutrition", "ntr");

            entity.Property(e => e.PatientMalnutritionId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.MalnutritionStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.InvestigationAdvise)
              .HasMaxLength(150)
              .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientDietPlan>(entity =>
        {
            entity.ToTable("PatientDietPlan", "ntr");

            entity.Property(e => e.PatientDietPlanId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });



        modelBuilder.Entity<SpeechTherapyPatientAssessment>(entity =>
        {
            entity.ToTable("SpeechTherapyPatientAssessment", "sph");

            entity.Property(e => e.SpeechTherapyPatientAssessmentId).ValueGeneratedNever();
            entity.Property(e => e.AssociatedMotorBehavior)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.AvoidanceOfSounds)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ChildVoicePitch)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ChildVoiceQuality)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ChildVoiceResonance)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ConsistencySoundError)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ConversationSpeech)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.ExpressiveLanguage)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OralFacialExaminationFindings)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PlanOfCare)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PreviousInterventionTherapy)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ReceptiveLanguage)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RelevantMedicalhistorySwallowing)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.ScheduleOfTherapyCarePlan)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SignificantOralMotorFinding)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StimulabilityOfFluentSpeech)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StimulabilityOfImprovedVoice)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StimulabilitySoundError)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TypeOfdysfluency)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Voiceimprovement)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.ToTable("Patient", "pt", tb => tb.HasTrigger("trg_PatientLog"));

            entity.HasIndex(e => e.ProvinceId, "XIX_Patient_ProvinceId");

            entity.HasIndex(e => new { e.HealthFacilityId, e.CreatedOn }, "XI_Patient_HealthFacilityId_CreatedOn");

            entity.HasIndex(e => new { e.HealthFacilityId, e.CreatedOn, e.ActionTypeId }, "XI_Patient_HealthFacilityId_CreatedOn_ActionTypeId");

            entity.HasIndex(e => e.Cnic, "X_Patient_CNIC");

            entity.HasIndex(e => new { e.FirstName, e.RelationProfileId, e.Cnic }, "X_Patient_FirstName_RelationProfileId_CNIC");

            entity.HasIndex(e => e.HealthFacilityId, "X_Patient_HealthFacilityId");

            entity.HasIndex(e => new { e.HealthFacilityId, e.CreatedOn }, "X_Patient_HealthFacilityId_CreatedOn");

            entity.HasIndex(e => new { e.HealthFacilityId, e.CreatedOn, e.ActionTypeId }, "X_Patient_HealthFacilityId_CreatedOn_ActionTypeId");

            entity.HasIndex(e => new { e.IsSelf, e.Cnic }, "X_Patient_IsSelf_CNIC");

            entity.HasIndex(e => e.Mrno, "X_Patient_MRNo");

            entity.HasIndex(e => e.MobileNo, "X_Patient_MobileNo");

            entity.HasIndex(e => e.RelationProfileId, "X_Patient_RelationProfileId");

            entity.HasIndex(e => e.TehsilId, "X_Patient_TehsilId");

            entity.Property(e => e.PatientId).ValueGeneratedNever();
            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Domicile)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FollowupDate).HasColumnType("date");
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.GuardianName).HasMaxLength(150);
            entity.Property(e => e.Hfmiscode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HFMISCode");
            entity.Property(e => e.IsRegisteredExternally).HasDefaultValueSql("((0))");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.NameOfCnicHolder).HasMaxLength(150);
            entity.Property(e => e.Ntn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NTN");
            entity.Property(e => e.ParmanentAddress).HasMaxLength(250);
            entity.Property(e => e.PassportNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PregnancyDate).HasColumnType("date");
            entity.Property(e => e.SourceMrno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SourcePatientId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TemporaryAddress).HasMaxLength(250);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.BloodGroupProfile).WithMany(p => p.PatientBloodGroupProfiles)
                 .HasForeignKey(d => d.BloodGroupProfileId)
                 .HasConstraintName("FK_Patient_Profile_BloodGroup");

            entity.HasOne(d => d.CasteProfile).WithMany(p => p.PatientCasteProfiles)
                .HasForeignKey(d => d.CasteProfileId)
                .HasConstraintName("FK_Patient_Profile_Caste");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PatientCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Patient_Users");

            entity.HasOne(d => d.District).WithMany(p => p.Patients)
                .HasForeignKey(d => d.DistrictId)
                .HasConstraintName("FK_Patient_District");

            entity.HasOne(d => d.Division).WithMany(p => p.Patients)
                .HasForeignKey(d => d.DivisionId)
                .HasConstraintName("FK_Patient_Division");

            entity.HasOne(d => d.GenderProfile).WithMany(p => p.PatientGenderProfiles)
                .HasForeignKey(d => d.GenderProfileId)
                .HasConstraintName("FK_Patient_Profile_Gender");

            entity.HasOne(d => d.HealthFacility).WithMany(p => p.Patients)
                .HasForeignKey(d => d.HealthFacilityId)
                .HasConstraintName("FK_Patient_HealthFacility");

            entity.HasOne(d => d.MaritialStatusProfile).WithMany(p => p.PatientMaritialStatusProfiles)
                .HasForeignKey(d => d.MaritialStatusProfileId)
                .HasConstraintName("FK_Patient_Profile_MaritialStatus");

            entity.HasOne(d => d.MotherLandProfile).WithMany(p => p.PatientMotherLandProfiles)
                .HasForeignKey(d => d.MotherLandProfileId)
                .HasConstraintName("FK_Patient_Profile_MotherLand");

            entity.HasOne(d => d.NationalityProfile).WithMany(p => p.PatientNationalityProfiles)
                .HasForeignKey(d => d.NationalityProfileId)
                .HasConstraintName("FK_Patient_Profile_Nationality");

            entity.HasOne(d => d.Province).WithMany(p => p.Patients)
                .HasForeignKey(d => d.ProvinceId)
                .HasConstraintName("FK_Patient_Province");

            entity.HasOne(d => d.RelationProfile).WithMany(p => p.PatientRelationProfiles)
                .HasForeignKey(d => d.RelationProfileId)
                .HasConstraintName("FK_Patient_Profile_Relation");

            entity.HasOne(d => d.ReligionProfile).WithMany(p => p.PatientReligionProfiles)
                .HasForeignKey(d => d.ReligionProfileId)
                .HasConstraintName("FK_Patient_Profile_Religion");

            entity.HasOne(d => d.Tehsil).WithMany(p => p.Patients)
                .HasForeignKey(d => d.TehsilId)
                .HasConstraintName("FK_Patient_TehsilId");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.PatientUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_Patient_Users1");
        });


        modelBuilder.Entity<PatientAdditionalInfo>(entity =>
        {
            entity.HasKey(e => e.PatientAdditionalInfoId).HasName("PK_PatientAddtionalInfo");

            entity.ToTable("PatientAdditionalInfo", "pt");

            entity.Property(e => e.PatientAdditionalInfoId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.GuardianAddress).HasMaxLength(250);
            entity.Property(e => e.GuardianCnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("GuardianCNIC");
            entity.Property(e => e.GuardianMobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.GuardianName).HasMaxLength(150);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientAdditionalInfos)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_PatientAdditionalInfo_Patient");
        });


        modelBuilder.Entity<PatientAdmissionDetail>(entity =>
        {
            entity.ToTable("PatientAdmissionDetail", "pt", tb => tb.HasTrigger("trg_PatientAdmissionDetailLog"));

            entity.Property(e => e.PatientAdmissionDetailId).ValueGeneratedNever();
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

            entity.HasOne(d => d.DepartmentLookup).WithMany(p => p.PatientAdmissionDetailDepartmentLookups)
                .HasForeignKey(d => d.DepartmentLookupId)
                .HasConstraintName("FK_PatientAdmissionDetail_DepartmentLookup");

            entity.HasOne(d => d.PatientDiagnose).WithMany(p => p.PatientAdmissionDetails)
                .HasForeignKey(d => d.PatientDiagnoseId)
                .HasConstraintName("FK_PatientAdmissionDetail_PatientDiagnose");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientAdmissionDetails)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientAdmissionDetail_Patient");

            entity.HasOne(d => d.PatientLevelProfile).WithMany(p => p.PatientAdmissionDetailPatientLevelProfiles)
                .HasForeignKey(d => d.PatientLevelProfileId)
                .HasConstraintName("FK_PatientAdmissionDetail_PatientLevel");

            entity.HasOne(d => d.PatientStatusProfile).WithMany(p => p.PatientAdmissionDetailPatientStatusProfiles)
                .HasForeignKey(d => d.PatientStatusProfileId)
                .HasConstraintName("FK_PatientAdmissionDetail_Profile");

            entity.HasOne(d => d.PatientVisit).WithMany(p => p.PatientAdmissionDetails)
                .HasForeignKey(d => d.PatientVisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientAdmissionDetail_PatientOpenVisit");

            entity.HasOne(d => d.SectionLookup).WithMany(p => p.PatientAdmissionDetailSectionLookups)
                .HasForeignKey(d => d.SectionLookupId)
                .HasConstraintName("FK_PatientAdmissionDetail_SectionLookup");

            entity.HasOne(d => d.ShiftedFromDepartmentLookup).WithMany(p => p.PatientAdmissionDetailShiftedFromDepartmentLookups)
                .HasForeignKey(d => d.ShiftedFromDepartmentLookupId)
                .HasConstraintName("FK_PatientAdmissionDetail_DepartmentLookup_Referred");

            entity.HasOne(d => d.ShiftedFromSectionLookup).WithMany(p => p.PatientAdmissionDetailShiftedFromSectionLookups)
                .HasForeignKey(d => d.ShiftedFromSectionLookupId)
                .HasConstraintName("FK_PatientAdmissionDetail_SectionLookup_Referred");
        });

        modelBuilder.Entity<PatientAdmissionDetailLog>(entity =>
        {
            entity.HasKey(e => e.PatientAdmissionDetailId);

            entity.ToTable("PatientAdmissionDetailLog", "pt");

            entity.Property(e => e.PatientAdmissionDetailId).ValueGeneratedNever();
            entity.Property(e => e.ActionDate).HasColumnType("datetime");
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

        modelBuilder.Entity<PatientAssessment>(entity =>
        {
            entity.ToTable("PatientAssessment", "hcp");

            entity.HasIndex(e => e.PatientId, "IN_PatientAssessment_PatientId");

            entity.Property(e => e.PatientAssessmentId).ValueGeneratedNever();
            entity.Property(e => e.AdvisedSampleCollection).HasMaxLength(255);
            entity.Property(e => e.BloodTransfusionBloodBank)
                .HasMaxLength(255)
                .IsFixedLength();
            entity.Property(e => e.BloodTransfusionYear).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.DentalClinic)
                .HasMaxLength(255)
                .IsFixedLength();
            entity.Property(e => e.IsConfirmedCaseOfStds).HasColumnName("IsConfirmedCaseOfSTDs");
            entity.Property(e => e.IsConfirmedHivpositivePersons).HasColumnName("IsConfirmedHIVPositivePersons");
            entity.Property(e => e.Monotes)
                .HasMaxLength(255)
                .HasColumnName("MONotes");
            entity.Property(e => e.SurgeryDate).HasColumnType("datetime");
            entity.Property(e => e.SurgeryType)
                .HasMaxLength(255)
                .IsFixedLength();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.PatientDiagnose).WithMany(p => p.PatientAssessments)
                .HasForeignKey(d => d.PatientDiagnoseId)
                .HasConstraintName("FK_PatientAssessment_PatientDiagnose");

            entity.HasOne(d => d.PatientVisit).WithMany(p => p.PatientAssessments)
                .HasForeignKey(d => d.PatientVisitId)
                .HasConstraintName("FK_PatientAssessment_PatientOpenVisit");
        });

        modelBuilder.Entity<PatientCoMorbid>(entity =>
        {
            entity.ToTable("PatientCoMorbid", "da");

            entity.Property(e => e.PatientCoMorbidId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
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




        modelBuilder.Entity<PsychologyAssociatedDisorder>(entity =>
        {
            entity.ToTable("PsychologyAssociatedDisorder", "psy");

            entity.Property(e => e.PsychologyAssociatedDisorderId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PsychologyDisorder>(entity =>
        {
            entity.ToTable("PsychologyDisorder", "psy");

            entity.Property(e => e.PsychologyDisorderId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PsychologyPatientModality>(entity =>
        {
            entity.ToTable("PsychologyPatientModality", "psy");

            entity.Property(e => e.PsychologyPatientModalityId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });



        modelBuilder.Entity<PatientContactDetail>(entity =>
        {
            entity.HasKey(e => e.ContactId);

            entity.ToTable("PatientContactDetails", "pt");

            entity.Property(e => e.ContactId).ValueGeneratedNever();
            entity.Property(e => e.ContactName).HasMaxLength(50);
            entity.Property(e => e.ContactNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsSputumCollected).HasDefaultValueSql("((0))");
            entity.Property(e => e.Relation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientDiagnose>(entity =>
        {
            entity.ToTable("PatientDiagnose", "pt");

            entity.HasIndex(e => new { e.DiagnosedBy, e.CreatedOn, e.ActionTypeId }, "ICCCX_PatientDiagnose_DiagnosedBy_CreatedOn_ActionTypeId");

            entity.HasIndex(e => new { e.PatientVisitId, e.DiagnosedBy, e.CreatedOn }, "ICCX_PatientDiagnose_PatientVisitId_DiagnosedBy_CreatedOn");

            entity.HasIndex(e => new { e.DiagnosedBy, e.CreatedOn, e.ActionTypeId }, "ICIIX_PatientDiagnose_DiagnosedBy_CreatedOn_ActionTypeId");

            entity.HasIndex(e => e.ActionTypeId, "ICX_PatientDiagnose_ActionTypeId");

            entity.HasIndex(e => new { e.CreatedOn, e.ActionTypeId }, "ICX_PatientDiagnose_CreatedOn_ActionTypeId");

            entity.HasIndex(e => new { e.DiagnosedBy, e.CreatedOn, e.ActionTypeId }, "ICX_PatientDiagnose_DiagnosedBy_CreatedOn_ActionTypeId");

            entity.HasIndex(e => e.PatientId, "ICX_PatientDiagnose_PatientId");

            entity.HasIndex(e => e.PatientVisitId, "ICX_PatientDiagnose_PatientVisitId");

            entity.HasIndex(e => new { e.PatientVisitId, e.CreatedOn }, "ICX_PatientDiagnose_PatientVisitId_CreatedOn");

            entity.HasIndex(e => new { e.PatientVisitId, e.DiagnosedBy, e.CreatedOn }, "ICX_PatientDiagnose_PatientVisitId_DiagnosedBy_CreatedOn");

            entity.HasIndex(e => new { e.DiagnosedBy, e.CreatedOn, e.ActionTypeId }, "IICCX_PatientDiagnose_DiagnosedBy_CreatedOn_ActionTypeId");

            entity.HasIndex(e => e.PatientVisitId, "IICX_PatientDiagnose_PatientVisitId");

            entity.HasIndex(e => e.PatientVisitId, "IIICX_PatientDiagnose_PatientVisitId");

            entity.HasIndex(e => new { e.CreatedOn, e.ActionTypeId }, "IMCX_PatientDiagnose_CreatedOn_ActionTypeId");

            entity.HasIndex(e => new { e.DiagnosedBy, e.ActionTypeId }, "IN_PatientDiagnose_DiagnosedBy_ActionTypeId");

            entity.HasIndex(e => new { e.CreatedOn, e.ActionTypeId }, "IXCX_PatientDiagnose_CreatedOn_ActionTypeId");

            entity.HasIndex(e => new { e.FormType, e.IsConfirmed }, "IX_PatientDiagnose_FormType_IsConfirmed");

            entity.HasIndex(e => new { e.DiagnosedBy, e.ActionTypeId }, "X_IN_PatientDiagnose_DiagnosedBy_ActionTypeId");

            entity.HasIndex(e => new { e.FormType, e.IsConfirmed }, "X_PatientDiagnose_FormType_IsConfirmed");

            entity.Property(e => e.PatientDiagnoseId).ValueGeneratedNever();
            entity.Property(e => e.AdviseGiven).HasColumnType("text");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.DoctorVisitNo).HasDefaultValueSql("((1))");
            entity.Property(e => e.Examination).HasColumnType("text");
            entity.Property(e => e.FollowupDate).HasColumnType("date");
            entity.Property(e => e.FormType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.IsAutoGenerated).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsDiagnoseExternally).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsGynaePatient).HasDefaultValueSql("((0))");
            entity.Property(e => e.PatientMedicalHistory).HasColumnType("text");
            entity.Property(e => e.PresentComplaints).HasColumnType("text");
            entity.Property(e => e.ReactionNote).IsUnicode(false);
            entity.Property(e => e.SourceDoctorName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PatientDiagnoseCreatedByNavigations)
             .HasForeignKey(d => d.CreatedBy)
             .HasConstraintName("FK_PatientDiagnose_Users1");

            entity.HasOne(d => d.DiagnosedByNavigation).WithMany(p => p.PatientDiagnoseDiagnosedByNavigations)
           .HasForeignKey(d => d.DiagnosedBy)
           .HasConstraintName("FK_PatientDiagnose_Users"); 
            
            entity.HasOne(d => d.DocDepartmentLookup).WithMany(p => p.PatientDiagnoses)
                .HasForeignKey(d => d.DocDepartmentLookupId)
                .HasConstraintName("FK_PatientDiagnose_DepartmentLookup");

            entity.HasOne(d => d.DocDepartmentLookup).WithMany(p => p.PatientDiagnoses)
              .HasForeignKey(d => d.DocDepartmentLookupId)
              .HasConstraintName("FK_PatientDiagnose_DepartmentLookup");

            entity.HasOne(d => d.DocSectionLookup).WithMany(p => p.PatientDiagnoses)
                .HasForeignKey(d => d.DocSectionLookupId)
                .HasConstraintName("FK_PatientDiagnose_SectionLookup");

            entity.HasOne(d => d.ReferToHealthFacility).WithMany(p => p.PatientDiagnoses)
                .HasForeignKey(d => d.ReferToHealthFacilityId)
                .HasConstraintName("FK_PatientDiagnose_ReferToHealthFacility");

            entity.HasOne(d => d.ReferToDepartmentLookup).WithMany(p => p.PatientDiagnosesReferToDepartmentLookup)
                .HasForeignKey(d => d.ReferToDepartmentLookupId)
                .HasConstraintName("FK_PatientDiagnose_DepartmentLookup_ReferTo");

            entity.HasOne(d => d.ReferToSectionLookup).WithMany(p => p.PatientDiagnosesReferToSectionLookup)
               .HasForeignKey(d => d.ReferToSectionLookupId)
               .HasConstraintName("FK_PatientDiagnose_SectionLookup_ReferTo");

            

            entity.HasOne(d => d.MedicineRegimeProfile).WithMany(p => p.PatientDiagnoseMedicineRegimeProfiles)
                .HasForeignKey(d => d.MedicineRegimeProfileId)
                .HasConstraintName("FK_PatientDiagnose_Profile_MedicineRegime");

            entity.HasOne(d => d.OutcomeStatusProfile).WithMany(p => p.PatientDiagnoseOutcomeStatusProfiles)
                .HasForeignKey(d => d.OutcomeStatusProfileId)
                .HasConstraintName("FK_PatientDiagnose_Profile");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientDiagnoses)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_PatientDiagnose_Patient");

            entity.HasOne(d => d.PatientVisit).WithMany(p => p.PatientDiagnoses)
                .HasForeignKey(d => d.PatientVisitId)
                .HasConstraintName("FK_PatientDiagnose_PatientOpenVisit");


            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.PatientDiagnoseUpdatedByNavigations)
              .HasForeignKey(d => d.UpdatedBy)
              .HasConstraintName("FK_PatientDiagnose_Users2");
        });

        modelBuilder.Entity<PatientDiagnoseDisease>(entity =>
        {
            entity.HasKey(e => e.PatientDiagnoseDiseaseId).HasName("PK_PatientDiagnoseDisease_1");

            entity.ToTable("PatientDiagnoseDisease", "pt");

            entity.HasIndex(e => e.CreatedBy, "ICX_PatientDiagnoseDisease_CreatedBy");

            entity.HasIndex(e => e.CreatedBy, "IOCX_PatientDiagnoseDisease_CreatedBy");

            entity.HasIndex(e => e.PatientDiagnoseId, "XIX_PatientDiagnoseDisease_PatientDiagnoseId");

            entity.HasIndex(e => e.PatientId, "XIX_PatientDiagnoseDisease_PatientId");

            entity.Property(e => e.PatientDiagnoseDiseaseId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.DiseaseProfile).WithMany(p => p.PatientDiagnoseDiseases)
                .HasForeignKey(d => d.DiseaseProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientDiagnoseDisease_Profile");

            entity.HasOne(d => d.PatientDiagnose).WithMany(p => p.PatientDiagnoseDiseases)
                .HasForeignKey(d => d.PatientDiagnoseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientDiagnoseDisease_PatientDiagnose");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientDiagnoseDiseases)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_PatientDiagnoseDisease_Patient");
        });

        modelBuilder.Entity<PatientDiagnoseProcedure>(entity =>
        {
            entity.HasKey(e => e.PatientDiagnoseProcedureId).HasName("PK_PatientDiagnoseProcedure_1");

            entity.ToTable("PatientDiagnoseProcedure", "pt");

            entity.HasIndex(e => new { e.PatientDiagnoseId, e.ActionTypeId }, "X_PatientDiagnoseProcedure_PatientDiagnoseId_ActionTypeId");

            entity.Property(e => e.PatientDiagnoseProcedureId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Feedback).HasMaxLength(50);
            entity.Property(e => e.ToothNumber).HasMaxLength(50);
            entity.Property(e => e.ToothPosition).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.AssistedByNavigation).WithMany(p => p.PatientDiagnoseProcedures)
              .HasForeignKey(d => d.AssistedBy)
              .HasConstraintName("FK_PatientDiagnoseProcedure_AssistedUsers");

            entity.HasOne(d => d.PatientDiagnose).WithMany(p => p.PatientDiagnoseProcedures)
                .HasForeignKey(d => d.PatientDiagnoseId)
                .HasConstraintName("FK_PatientDiagnoseProcedure_PatientDiagnose");

            entity.HasOne(d => d.SectionProcedure).WithMany(p => p.PatientDiagnoseProcedures)
                .HasForeignKey(d => d.SectionProcedureId)
                .HasConstraintName("FK_PatientDiagnoseProcedure_SectionProcedure");
        });

        modelBuilder.Entity<PatientDiagnoseTemplate>(entity =>
        {
            entity.ToTable("PatientDiagnoseTemplate", "pt");

            entity.HasIndex(e => e.CreatedBy, "X_PatientDiagnoseTemplate_CreatedBy");

            entity.HasIndex(e => new { e.UserId, e.ActionTypeId }, "X_PatientDiagnoseTemplate_UserId_ActionTypeId");

            entity.Property(e => e.PatientDiagnoseTemplateId).ValueGeneratedNever();
            entity.Property(e => e.ActionTypeId).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientDiagnosisRecord>(entity =>
        {
            entity.ToTable("PatientDiagnosisRecord", "pt");

            entity.HasIndex(e => e.PatientVisitId, "IX_PatientDiagnosisRecord_PatientVisitId");

            entity.HasIndex(e => e.PatientDiagnoseId, "XIX_PatientDiagnosisRecord_PatientDiagnoseId");

            entity.Property(e => e.PatientDiagnosisRecordId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.FormType).HasMaxLength(50);
            entity.Property(e => e.IsPrinted).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.PatientDiagnose).WithMany(p => p.PatientDiagnosisRecords)
                .HasForeignKey(d => d.PatientDiagnoseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientDiagnosisRecord_PatientDiagnose");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientDiagnosisRecords)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientDiagnosisRecord_Patient");

            entity.HasOne(d => d.PatientVisit).WithMany(p => p.PatientDiagnosisRecords)
                .HasForeignKey(d => d.PatientVisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientDiagnosisRecord_PatientOpenVisit");
        });

        modelBuilder.Entity<PatientDischargeDetail>(entity =>
        {
            entity.ToTable("PatientDischargeDetail", "pt");

            entity.Property(e => e.PatientDischargeDetailId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DateOfDischarge).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Reason)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PatientDischargeDetailCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_PatientDischargeDetail_Users");

            entity.HasOne(d => d.DeletedByNavigation).WithMany(p => p.PatientDischargeDetailDeletedByNavigations).HasForeignKey(d => d.DeletedBy);

            entity.HasOne(d => d.DischargeStatusProfile).WithMany(p => p.PatientDischargeDetails)
                .HasForeignKey(d => d.DischargeStatusProfileId)
                .HasConstraintName("FK_PatientDischargeDetail_Profile");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientDischargeDetails)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientDischargeDetail_Patient");

            entity.HasOne(d => d.PatientVisit).WithMany(p => p.PatientDischargeDetails)
                .HasForeignKey(d => d.PatientVisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientDischargeDetail_PatientOpenVisit");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.PatientDischargeDetailUpdatedByNavigations).HasForeignKey(d => d.UpdatedBy);
        });





        modelBuilder.Entity<PsychologicalTestApplied>(entity =>
        {
            entity.ToTable("PsychologicalTestApplied", "psy");

            entity.Property(e => e.PsychologicalTestAppliedId).ValueGeneratedNever();
            entity.Property(e => e.BackAnxietyInventory)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.BackDepressionInventory)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.HouseTreePersonTest)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientDocument>(entity =>
        {
            entity.ToTable("PatientDocument", "pt", tb => tb.HasTrigger("trg_PatientDocumentLog"));

            entity.Property(e => e.PatientDocumentId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.DocumentName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.StatusReason)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.StatusUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Url)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PatientDocumentLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PatientDocumentLog", "pt");

            entity.Property(e => e.ActionDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.DocumentName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.StatusReason)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.StatusUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Url)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PatientDrugAddiction>(entity =>
        {
            entity.ToTable("PatientDrugAddiction", "da");

            entity.Property(e => e.PatientDrugAddictionId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientImage>(entity =>
        {
            entity.HasKey(e => e.PatientImageId).HasName("PK_PatientImages_1");

            entity.ToTable("PatientImages", "pt");

            entity.Property(e => e.PatientImageId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.ImageUrl).HasColumnName("ImageURL");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientLabTest>(entity =>
        {
            entity.ToTable("PatientLabTest", "pt");

            entity.HasIndex(e => e.ActionTypeId, "AIII_PatientLabTest_ActionTypeId");

            entity.HasIndex(e => e.ActionTypeId, "AII_PatientLabTest_ActionTypeId");

            entity.HasIndex(e => e.ActionTypeId, "AI_PatientLabTest_ActionTypeId");

            entity.HasIndex(e => new { e.CreatedOn, e.ActionTypeId }, "ICXIII_PatientLabTest_CreatedOn_ActionTypeId");

            entity.HasIndex(e => new { e.CreatedOn, e.ActionTypeId }, "ICXII_PatientLabTest_CreatedOn_ActionTypeId");

            entity.HasIndex(e => new { e.CreatedBy, e.CreatedOn }, "ICX_PatientLabTest_CreatedBy_CreatedOn");

            entity.HasIndex(e => new { e.CreatedBy, e.CreatedOn, e.ActionTypeId }, "ICX_PatientLabTest_CreatedBy_CreatedOn_ActionTypeId");

            entity.HasIndex(e => new { e.CreatedOn, e.ActionTypeId }, "ICX_PatientLabTest_CreatedOn_ActionTypeId");

            entity.HasIndex(e => new { e.IsReportGenerated, e.CreatedOn, e.ActionTypeId }, "ICX_PatientLabTest_IsReportGenerated_CreatedOn_ActionTypeId");

            entity.HasIndex(e => e.PatientId, "ICX_PatientLabTest_PatientId");

            entity.HasIndex(e => e.LabTestId, "IN_PatientLabTest_LabTestId");

            entity.HasIndex(e => new { e.CreatedOn, e.ActionTypeId }, "IX_PatientLabTest_CreatedOn_ActionTypeId");

            entity.HasIndex(e => new { e.IsArchived, e.IsAdvisedExternally, e.ActionTypeId }, "XIII_PatientLabTest_IsArchived_IsAdvisedExternally_ActionTypeId");

            entity.HasIndex(e => e.ActionTypeId, "XII_PatientLabTest_ActionTypeId");

            entity.HasIndex(e => new { e.LabDepartmentProfileId, e.IsArchived, e.IsAdvisedExternally }, "XII_PatientLabTest_LabDepartmentProfileId_IsArchived_IsAdvisedExternally");

            entity.HasIndex(e => new { e.IsArchived, e.IsAdvisedExternally }, "XIX_PatientLabTest_IsArchived_IsAdvisedExternally");

            entity.HasIndex(e => new { e.IsArchived, e.IsAdvisedExternally, e.ActionTypeId }, "XIX_PatientLabTest_IsArchived_IsAdvisedExternally_ActionTypeId");

            entity.HasIndex(e => new { e.IsSampleCollected, e.IsArchived, e.IsAdvisedExternally, e.ActionTypeId }, "XIX_PatientLabTest_IsSampleCollected_IsArchived_IsAdvisedExternally_ActionTypeId");

            entity.HasIndex(e => new { e.LabDepartmentProfileId, e.IsArchived, e.IsAdvisedExternally }, "XIX_PatientLabTest_LabDepartmentProfileId_IsArchived_IsAdvisedExternally");

            entity.HasIndex(e => e.PatientDiagnoseId, "XIX_PatientLabTest_PatientDiagnoseId");

            entity.HasIndex(e => new { e.PatientVisitId, e.ActionTypeId }, "XIX_PatientLabTest_PatientVisitId_ActionTypeId");

            entity.HasIndex(e => new { e.IsArchived, e.IsAdvisedExternally, e.ActionTypeId }, "XI_PatientLabTest_IsArchived_IsAdvisedExternally_ActionTypeId");

            entity.HasIndex(e => new { e.IsSampleCollected, e.IsArchived, e.IsAdvisedExternally, e.ActionTypeId }, "XI_PatientLabTest_IsSampleCollected_IsArchived_IsAdvisedExternally_ActionTypeId");

            entity.HasIndex(e => new { e.IsSampleCollected, e.IsArchived, e.IsAdvisedExternally }, "XVI_PatientLabTest_IsSampleCollected_IsArchived_IsAdvisedExternally");

            entity.HasIndex(e => new { e.IsArchived, e.IsAdvisedExternally, e.CreatedOn, e.ActionTypeId }, "XV_PatientLabTest_IsArchived_IsAdvisedExternally_CreatedOn_ActionTypeId");

            entity.HasIndex(e => new { e.LabDepartmentProfileId, e.IsArchived, e.IsAdvisedExternally }, "XXII_PatientLabTest_LabDepartmentProfileId_IsArchived_IsAdvisedExternally");

            entity.HasIndex(e => e.ActionTypeId, "XX_PatientLabTest_ActionTypeId");

            entity.HasIndex(e => new { e.IsSampleCollected, e.IsArchived, e.IsAdvisedExternally }, "XX_PatientLabTest_IsSampleCollected_IsArchived_IsAdvisedExternally");

            entity.HasIndex(e => e.ActionTypeId, "X_PatientLabTest_ActionTypeId");

            entity.HasIndex(e => new { e.CreatedBy, e.ActionTypeId }, "X_PatientLabTest_CreatedBy_ActionTypeId");

            entity.HasIndex(e => new { e.CreatedOn, e.ActionTypeId }, "X_PatientLabTest_CreatedOn_ActionTypeId");

            entity.HasIndex(e => new { e.IsArchived, e.IsAdvisedExternally }, "X_PatientLabTest_IsArchived_IsAdvisedExternally");

            entity.HasIndex(e => new { e.IsArchived, e.IsAdvisedExternally, e.ActionTypeId }, "X_PatientLabTest_IsArchived_IsAdvisedExternally_ActionTypeId");

            entity.HasIndex(e => new { e.IsArchived, e.IsAdvisedExternally, e.ActionTypeId, e.RiderUserId }, "X_PatientLabTest_IsArchived_IsAdvisedExternally_ActionTypeId_RiderUserId");

            entity.HasIndex(e => new { e.IsArchived, e.IsAdvisedExternally, e.CreatedOn, e.ActionTypeId }, "X_PatientLabTest_IsArchived_IsAdvisedExternally_CreatedOn_ActionTypeId");

            entity.HasIndex(e => new { e.IsArchived, e.IsAdvisedExternally, e.RiderUserId }, "X_PatientLabTest_IsArchived_IsAdvisedExternally_RiderUserId");

            entity.HasIndex(e => new { e.IsArchived, e.IsAdvisedExternally, e.RiderUserId, e.ActionTypeId }, "X_PatientLabTest_IsArchived_IsAdvisedExternally_RiderUserId_ActionTypeId");

            entity.HasIndex(e => new { e.IsSampleCollected, e.ActionTypeId }, "X_PatientLabTest_IsSampleCollected_ActionTypeId");

            entity.HasIndex(e => new { e.IsSampleCollected, e.CreatedOn, e.ActionTypeId }, "X_PatientLabTest_IsSampleCollected_CreatedOn_ActionTypeId");

            entity.HasIndex(e => new { e.IsSampleCollected, e.IsArchived, e.IsAdvisedExternally }, "X_PatientLabTest_IsSampleCollected_IsArchived_IsAdvisedExternally");

            entity.HasIndex(e => new { e.IsSampleCollected, e.IsArchived, e.IsAdvisedExternally, e.ActionTypeId }, "X_PatientLabTest_IsSampleCollected_IsArchived_IsAdvisedExternally_ActionTypeId");

            entity.HasIndex(e => new { e.IsSampleRequired, e.RiderUserId }, "X_PatientLabTest_IsSampleRequired_RiderUserId");

            entity.HasIndex(e => new { e.IsSampleRequired, e.RiderUserId, e.IsAdvisedExternally, e.ActionTypeId }, "X_PatientLabTest_IsSampleRequired_RiderUserId_IsAdvisedExternally_ActionTypeId");

            entity.HasIndex(e => e.LabDepartmentProfileId, "X_PatientLabTest_LabDepartmentProfileId");

            entity.HasIndex(e => new { e.LabDepartmentProfileId, e.IsArchived, e.IsAdvisedExternally }, "X_PatientLabTest_LabDepartmentProfileId_IsArchived_IsAdvisedExternally");

            entity.HasIndex(e => new { e.LabDepartmentProfileId, e.IsArchived, e.IsAdvisedExternally, e.RiderUserId }, "X_PatientLabTest_LabDepartmentProfileId_IsArchived_IsAdvisedExternally_RiderUserId");

            entity.HasIndex(e => new { e.Status, e.IsSampleRequired, e.RiderUserId }, "X_PatientLabTest_Status_IsSampleRequired_RiderUserId");

            entity.HasIndex(e => new { e.Status, e.IsSampleRequired, e.RiderUserId, e.IsAdvisedExternally, e.ActionTypeId }, "X_PatientLabTest_Status_IsSampleRequired_RiderUserId_IsAdvisedExternally_ActionTypeId");

            entity.Property(e => e.PatientLabTestId).ValueGeneratedNever();
            entity.Property(e => e.ArchivedOn).HasColumnType("datetime");
            entity.Property(e => e.BarcodeNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.IsAdvisedExternally).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsSampleRequired)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.PreGeneratedBarcodeNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ReportGeneratedOn).HasColumnType("datetime");
            entity.Property(e => e.ReportLink)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ResultImageLink)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SampleCollectedOn).HasColumnType("datetime");
            entity.Property(e => e.SampleRejectedOn).HasColumnType("datetime");
            entity.Property(e => e.SampleRejectedReason)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.SampleTransportMode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SourceBarcode).IsUnicode(false);
            entity.Property(e => e.SourceDoctorName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SourceLabTestId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SourcePkId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.StatusUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.DiscountInPercentage).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DiscountedPrice).HasColumnType("decimal(18, 2)");


            entity.HasOne(d => d.LabDepartmentProfile).WithMany(p => p.PatientLabTests)
                .HasForeignKey(d => d.LabDepartmentProfileId)
                .HasConstraintName("FK_PatientLabTest_Profile");

            entity.HasOne(d => d.LabTest).WithMany(p => p.PatientLabTests)
                .HasForeignKey(d => d.LabTestId)
                .HasConstraintName("FK_PatientLabTest_LabTest");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientLabTests)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_PatientLabTest_Patient");

            entity.HasOne(d => d.ReportGeneratedByNavigation).WithMany(p => p.PatientLabTestReportGeneratedByNavigations)
                .HasForeignKey(d => d.ReportGeneratedBy)
                .HasConstraintName("FK_PatientLabTest_Users1");

            entity.HasOne(d => d.SampleCollectedByNavigation).WithMany(p => p.PatientLabTestSampleCollectedByNavigations).HasForeignKey(d => d.SampleCollectedBy);

            entity.HasOne(d => d.SampleRejectedByNavigation).WithMany(p => p.PatientLabTestSampleRejectedByNavigations)
                .HasForeignKey(d => d.SampleRejectedBy)
                .HasConstraintName("FK_PatientLabTest_Users2");

            entity.HasOne(d => d.TestAdvisedByNavigation).WithMany(p => p.PatientLabTestTestAdvisedByNavigations)
                .HasForeignKey(d => d.TestAdvisedBy)
                .HasConstraintName("FK_PatientLabTest_Users");
        });

        modelBuilder.Entity<PatientLabTestDetail>(entity =>
        {
            entity.ToTable("PatientLabTestDetail", "pt");

            entity.HasIndex(e => e.PatientLabTestId, "XIX_PatientLabTestDetail_PatientLabTestId");

            entity.HasIndex(e => e.PatientLabTestId, "XX_PatientLabTestDetail_PatientLabTestId");

            entity.Property(e => e.PatientLabTestDetailId).ValueGeneratedNever();
            entity.Property(e => e.ActionTypeId).HasDefaultValueSql("((1))");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.MaxValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MinValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Result)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.TestName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TestNormalValue)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.TestUnit)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.PatientLabTest).WithMany(p => p.PatientLabTestDetails)
                .HasForeignKey(d => d.PatientLabTestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientLabTestDetail_PatientLabTestDetail");
        });

        modelBuilder.Entity<PatientLocationPrefix>(entity =>
        {
            entity.ToTable("PatientLocationPrefix", "pt");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.LocationCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PatientLog", "pt");

            entity.Property(e => e.ActionDate).HasColumnType("datetime");
            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Domicile)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FollowupDate).HasColumnType("date");
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.GuardianName).HasMaxLength(150);
            entity.Property(e => e.Hfmiscode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HFMISCode");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.NameOfCnicHolder).HasMaxLength(150);
            entity.Property(e => e.Ntn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NTN");
            entity.Property(e => e.ParmanentAddress).HasMaxLength(250);
            entity.Property(e => e.PassportNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TemporaryAddress).HasMaxLength(250);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientNadraInfoResponse>(entity =>
        {
            entity.HasKey(e => e.PatientNadraInfoResponseId).HasName("PK_PatientNadraInfoResponse_1");

            entity.ToTable("PatientNadraInfoResponse", "da");

            entity.Property(e => e.PatientNadraInfoResponseId).ValueGeneratedNever();
            entity.Property(e => e.CitizenNumber).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.ErrorResponse).IsUnicode(false);
            entity.Property(e => e.Message).IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.RequestCount).ValueGeneratedOnAdd();
            entity.Property(e => e.TransactionId).IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientNadraRequest>(entity =>
        {
            entity.HasKey(e => e.PatientNadraRequestId).HasName("PK_PatientNadraRequest_1");

            entity.ToTable("PatientNadraRequest", "da");

            entity.Property(e => e.PatientNadraRequestId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.FingerPrintFormate)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LocationId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RequestCount).ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientNadraResponse>(entity =>
        {
            entity.ToTable("PatientNadraResponse", "da");

            entity.Property(e => e.PatientNadraResponseId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.ErrorResponse).IsUnicode(false);
            entity.Property(e => e.Message).IsUnicode(false);
            entity.Property(e => e.RequestCount).ValueGeneratedOnAdd();
            entity.Property(e => e.TransactionId).IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });
        modelBuilder.Entity<PatientOpenVisit>(entity =>
        {
            entity.ToTable("PatientOpenVisit", "pt", tb =>
            {
                tb.HasTrigger("trg_PatientOpenVisitLog");
                tb.HasTrigger("trg_SscVisitStatusLog");
            });

            entity.HasIndex(e => e.HealthFacilityId, "Test_X_PatientOpenVisit_HealthFacilityId");

            entity.HasIndex(e => new { e.HealthFacilityId, e.DepartementLookupId, e.SectionLookupId, e.CurrentStationProfileId, e.VisitDate }, "Test_X_PatientOpenVisit_HealthFacilityId_DepartementLookupId_SectionLookupId_CurrentStationProfileId_VisitDate");

            entity.HasIndex(e => new { e.PatientId, e.HealthFacilityId }, "Test_X_PatientOpenVisit_PatientId_HealthFacilityId");

            entity.Property(e => e.PatientOpenVisitId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsAdmittedInIpd).HasColumnName("IsAdmittedInIPD");
            entity.Property(e => e.IsAutoGenerated).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsFromPmis).HasColumnName("IsFromPMIS");
            entity.Property(e => e.IsOccupied).HasDefaultValueSql("('FALSE')");
            entity.Property(e => e.IsReferred).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsVisitExternally).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsVitalSkip).HasDefaultValueSql("((0))");
            entity.Property(e => e.SlipNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SourceHealthFacilityId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SourceReferredHealthFacilityId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SourceVisitId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SscClaimedDate).HasColumnType("datetime");
            entity.Property(e => e.SscNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SscStatusReason)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SscStatusUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.TokenNo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.VisitDate).HasColumnType("date");
            entity.Property(e => e.VisitFor)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PatientOpenVisitCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_PatientOpenVisit_Users");

            entity.HasOne(d => d.CurrentStationProfile).WithMany(p => p.PatientOpenVisitCurrentStationProfiles)
                .HasForeignKey(d => d.CurrentStationProfileId)
                .HasConstraintName("FK_PatientOpenVisit_Profile");

            entity.HasOne(d => d.DepartementLookup).WithMany(p => p.PatientOpenVisitDepartementLookups)
                .HasForeignKey(d => d.DepartementLookupId)
                .HasConstraintName("FK_PatientOpenVisit_DepartmentLookup");

            entity.HasOne(d => d.HealthFacility).WithMany(p => p.PatientOpenVisits)
                .HasForeignKey(d => d.HealthFacilityId)
                .HasConstraintName("FK_PatientOpenVisit_HealthFacility");

            entity.HasOne(d => d.IpdDepartmentLookup).WithMany(p => p.PatientOpenVisitIpdDepartmentLookups)
                .HasForeignKey(d => d.IpdDepartmentLookupId)
                .HasConstraintName("FK_PatientOpenVisit_DepartmentLookup_IPD");

            entity.HasOne(d => d.IpdReferredByNavigation).WithMany(p => p.PatientOpenVisitIpdReferredByNavigations).HasForeignKey(d => d.IpdReferredBy);

            entity.HasOne(d => d.IpdReferredByDepartmentLookup).WithMany(p => p.PatientOpenVisitIpdReferredByDepartmentLookups)
                .HasForeignKey(d => d.IpdReferredByDepartmentLookupId)
                .HasConstraintName("FK_PatientOpenVisit_DepartmentLookup_IpdReferredByDep");

            entity.HasOne(d => d.IpdReferredBySectionLookup).WithMany(p => p.PatientOpenVisitIpdReferredBySectionLookups)
                .HasForeignKey(d => d.IpdReferredBySectionLookupId)
                .HasConstraintName("FK_PatientOpenVisit_PatientOpenVisit_IpdReferredBySec");

            entity.HasOne(d => d.IpdSectionLookup).WithMany(p => p.PatientOpenVisitIpdSectionLookups)
                .HasForeignKey(d => d.IpdSectionLookupId)
                .HasConstraintName("FK_PatientOpenVisit_SectionLookup_IPD");

            entity.HasOne(d => d.OccupiedByNavigation).WithMany(p => p.PatientOpenVisitOccupiedByNavigations).HasForeignKey(d => d.OccupiedBy);

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientOpenVisits)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_PatientOpenVisit_Patient");

            entity.HasOne(d => d.PharmacyAttendedByNavigation).WithMany(p => p.PatientOpenVisitPharmacyAttendedByNavigations)
                .HasForeignKey(d => d.PharmacyAttendedBy)
                .HasConstraintName("FK_PatientOpenVisit_Users-PharmacyUser");

            entity.HasOne(d => d.ReasonIfNotEligibleForSscNavigation).WithMany(p => p.PatientOpenVisitReasonIfNotEligibleForSscNavigations)
                .HasForeignKey(d => d.ReasonIfNotEligibleForSsc)
                .HasConstraintName("FK_PatientOpenVisit_EligibleReason");

            entity.HasOne(d => d.ReferredByNavigation).WithMany(p => p.PatientOpenVisitReferredByNavigations).HasForeignKey(d => d.ReferredBy);

            entity.HasOne(d => d.ReferredDepartmentLookup).WithMany(p => p.PatientOpenVisitReferredDepartmentLookups)
                .HasForeignKey(d => d.ReferredDepartmentLookupId)
                .HasConstraintName("FK_PatientOpenVisit_DepartmentLookup_Referred");

            entity.HasOne(d => d.ReferredSectionLookup).WithMany(p => p.PatientOpenVisitReferredSectionLookups)
                .HasForeignKey(d => d.ReferredSectionLookupId)
                .HasConstraintName("FK_PatientOpenVisit_SectionLookup_Referred");

            entity.HasOne(d => d.SectionLookup).WithMany(p => p.PatientOpenVisitSectionLookups)
                .HasForeignKey(d => d.SectionLookupId)
                .HasConstraintName("FK_PatientOpenVisit_SectionLookup");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.PatientOpenVisitUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_PatientOpenVisit_Users1");

            entity.HasOne(d => d.VisitTypeProfile).WithMany(p => p.PatientOpenVisitVisitTypeProfiles)
                .HasForeignKey(d => d.VisitTypeProfileId)
                .HasConstraintName("FK_PatientOpenVisit_Profile_VisitType");

            entity.HasOne(d => d.VitalCollectedByNavigation).WithMany(p => p.PatientOpenVisitVitalCollectedByNavigations)
                .HasForeignKey(d => d.VitalCollectedBy)
                .HasConstraintName("FK_PatientOpenVisit_Users2");
        });

        modelBuilder.Entity<PatientOpenVisitLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PatientOpenVisitLog", "pt");

            entity.Property(e => e.ActionDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsFromPmis).HasColumnName("IsFromPMIS");
            entity.Property(e => e.IsReferred).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsVisitExternally).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsVitalSkip).HasDefaultValueSql("((0))");
            entity.Property(e => e.SlipNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SourceHealthFacilityId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SourceReferredHealthFacilityId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SourceVisitId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TokenNo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.VisitDate).HasColumnType("date");
        });


        modelBuilder.Entity<PatientPrescription>(entity =>
        {
            entity.ToTable("PatientPrescription", "pt");

            entity.HasIndex(e => new { e.CreatedBy, e.CreatedOn }, "ICX_PatientPrescription_CreatedBy_CreatedOn");

            entity.HasIndex(e => new { e.CreatedBy, e.CreatedOn, e.ActionTypeId }, "ICX_PatientPrescription_CreatedBy_CreatedOn_ActionTypeId");

            entity.HasIndex(e => new { e.PatientDiagnoseId, e.ActionTypeId }, "ICX_PatientPrescription_PatientDiagnoseId_ActionTypeId");

            entity.HasIndex(e => e.PatientVisitId, "IX_PatientPrescription_PatientVisitId");

            entity.HasIndex(e => e.PrescribedBy, "IX_PatientPrescription_PrescribedBy");

            entity.HasIndex(e => e.MedicineId, "XII_PatientPrescription_MedicineId");

            entity.HasIndex(e => e.PrescribedBy, "XII_PatientPrescription_PrescribedBy");

            entity.HasIndex(e => e.CreatedOn, "XIX_PatientPrescription_CreatedOn");

            entity.HasIndex(e => new { e.CreatedOn, e.ActionTypeId }, "XIX_PatientPrescription_CreatedOn_ActionTypeId");

            entity.HasIndex(e => e.MedicineId, "XIX_PatientPrescription_MedicineId");

            entity.HasIndex(e => e.MedicineId, "XI_PatientPrescription_MedicineId");

            entity.HasIndex(e => e.MedicineId, "XVI_PatientPrescription_MedicineId");

            entity.HasIndex(e => e.MedicineId, "XV_PatientPrescription_MedicineId");

            entity.HasIndex(e => e.CreatedBy, "X_PatientPrescription_CreatedBy");

            entity.HasIndex(e => e.MedicineId, "X_PatientPrescription_MedicineId");

            entity.HasIndex(e => new { e.MedicineId, e.CreatedOn }, "X_PatientPrescription_MedicineId_CreatedOn");

            entity.HasIndex(e => e.PatientId, "X_PatientPrescription_PatientId");

            entity.HasIndex(e => e.PrescribedBy, "X_PatientPrescription_PrescribedBy");

            entity.HasIndex(e => new { e.PrescribedBy, e.CreatedOn }, "X_PatientPrescription_PrescribedBy_CreatedOn");

            entity.HasIndex(e => new { e.PrescribedBy, e.MedicineId }, "X_PatientPrescription_PrescribedBy_MedicineId");

            entity.Property(e => e.PatientPrescriptionId).ValueGeneratedNever();
            entity.Property(e => e.AfterFood).HasDefaultValueSql("((0))");
            entity.Property(e => e.AfterNoonDose).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BatchNo).HasMaxLength(200);
            entity.Property(e => e.BeforeFood).HasDefaultValueSql("((0))");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.EveningDose).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.MedicineDose).HasMaxLength(200);
            entity.Property(e => e.MedicineDuration).HasMaxLength(200);
            entity.Property(e => e.MedicineFrequency).HasMaxLength(200);
            entity.Property(e => e.MedicineInstruction).HasMaxLength(200);
            entity.Property(e => e.MedicineName).HasMaxLength(200);
            entity.Property(e => e.MedicineRoute).HasMaxLength(200);
            entity.Property(e => e.MorningDose).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NightDose).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.DoseProfile).WithMany(p => p.PatientPrescriptionDoseProfiles)
                .HasForeignKey(d => d.DoseProfileId)
                .HasConstraintName("FK_PatientPrescription_Profile");

            entity.HasOne(d => d.DoseTimeProfile).WithMany(p => p.PatientPrescriptionDoseTimeProfiles)
                .HasForeignKey(d => d.DoseTimeProfileId)
                .HasConstraintName("FK_PatientPrescriptionDoseTime_Profile");

            entity.HasOne(d => d.PatientDiagnose).WithMany(p => p.PatientPrescriptions)
                .HasForeignKey(d => d.PatientDiagnoseId)
                .HasConstraintName("FK_PatientPrescription_PatientDiagnose");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientPrescriptions)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_PatientPrescription_Patient");

            entity.HasOne(d => d.PatientVisit).WithMany(p => p.PatientPrescriptions)
                .HasForeignKey(d => d.PatientVisitId)
                .HasConstraintName("FK_PatientPrescription_PatientOpenVisit");

            entity.HasOne(d => d.PrescribedByNavigation).WithMany(p => p.PatientPrescriptions)
                .HasForeignKey(d => d.PrescribedBy)
                .HasConstraintName("FK_PatientPrescription_Users");
        });

        modelBuilder.Entity<PatientScreening>(entity =>
        {
            entity.ToTable("PatientScreening", "hcp");

            entity.HasIndex(e => e.PatientId, "X_PatientScreening_PatientId");

            entity.Property(e => e.PatientScreeningId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.HasHbvpcrconfirmation).HasColumnName("HasHBVPCRConfirmation");
            entity.Property(e => e.HasHcvpcrconfirmation).HasColumnName("HasHCVPCRConfirmation");
            entity.Property(e => e.IsDiagnosedHbvrepidKit).HasColumnName("IsDiagnosedHBVRepidKit");
            entity.Property(e => e.IsDiagnosedHcvrepidKit).HasColumnName("IsDiagnosedHCVRepidKit");
            entity.Property(e => e.IsPreviouslyDiagnosedHbv).HasColumnName("IsPreviouslyDiagnosedHBV");
            entity.Property(e => e.IsPreviouslyDiagnosedHcv).HasColumnName("IsPreviouslyDiagnosedHCV");
            entity.Property(e => e.PatientType)
                .HasMaxLength(255)
                .IsFixedLength();
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.PatientDiagnose).WithMany(p => p.PatientScreenings)
                .HasForeignKey(d => d.PatientDiagnoseId)
                .HasConstraintName("FK_PatientScreening_PatientDiagnose");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientScreenings)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_PatientScreening_Patient");

            entity.HasOne(d => d.PatientVisit).WithMany(p => p.PatientScreenings)
                .HasForeignKey(d => d.PatientVisitId)
                .HasConstraintName("FK_PatientScreening_PatientOpenVisit");
        });

        modelBuilder.Entity<SpeechAndLanguageHistory>(entity =>
        {
            entity.ToTable("SpeechAndLanguageHistory", "sph");

            entity.Property(e => e.SpeechAndLanguageHistoryId).ValueGeneratedNever();
            entity.Property(e => e.ChildCommunicates)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ChildInteraction)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ChildVisitProfessional)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.LanguageProblem)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LanguageSpokenAtHome)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LanguageSpokenAtSchool)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MotherLanguage)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OtherBehavioralProblem)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });


        modelBuilder.Entity<PatientSourceInfo>(entity =>
        {
            entity.ToTable("PatientSourceInfo", "pt");

            entity.Property(e => e.PatientSourceInfoId).ValueGeneratedNever();
            entity.Property(e => e.ContactNo).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Designation).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.VehicleNo)
                .HasMaxLength(50)
                .IsUnicode(false);
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
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientStatusBySpeciality1>(entity =>
        {
            entity.HasKey(e => e.PatientStatusBySpecialityId);

            entity.ToTable("PatientStatusBySpeciality", "pt");

            entity.Property(e => e.PatientStatusBySpecialityId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.DeseaseSubType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FormType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientVaccination>(entity =>
        {
            entity.ToTable("PatientVaccination", "hcp");

            entity.Property(e => e.PatientVaccinationId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.VaccinationDose1Date).HasColumnType("datetime");
            entity.Property(e => e.VaccinationDose2Date).HasColumnType("datetime");
            entity.Property(e => e.VaccinationDose3Date).HasColumnType("datetime");

            entity.HasOne(d => d.PatientDiagnose).WithMany(p => p.PatientVaccinations)
                .HasForeignKey(d => d.PatientDiagnoseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientVaccination_PatientDiagnose");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientVaccinations)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientVaccination_Patient");

            entity.HasOne(d => d.PatientVisit).WithMany(p => p.PatientVaccinations)
                .HasForeignKey(d => d.PatientVisitId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientVaccination_PatientOpenVisit");

            entity.HasOne(d => d.VaccinationProfile).WithMany(p => p.PatientVaccinationVaccinationProfiles)
                .HasForeignKey(d => d.VaccinationProfileId)
                .HasConstraintName("FK_PatientVaccination_Profile1");

            entity.HasOne(d => d.VaccinationTypeProfile).WithMany(p => p.PatientVaccinationVaccinationTypeProfiles)
                .HasForeignKey(d => d.VaccinationTypeProfileId)
                .HasConstraintName("FK_PatientVaccination_Profile");
        });

        modelBuilder.Entity<PatientVisitFlow>(entity =>
        {
            entity.ToTable("PatientVisitFlow", "pt");

            entity.HasIndex(e => new { e.PatientVisitId, e.CreatedOn, e.IsConsultant }, "IX_PatientVisitFlow_PatientVisitId_CreatedOn_IsConsultant");

            entity.HasIndex(e => e.IsConsultant, "XII_PatientVisitFlow_IsConsultant");

            entity.HasIndex(e => new { e.CreatedOn, e.IsConsultant }, "XI_PatientVisitFlow_CreatedOn_IsConsultant");

            entity.HasIndex(e => new { e.PatientDiagnoseId, e.IsConsultant }, "XI_PatientVisitFlow_PatientDiagnoseId_IsConsultant");

            entity.HasIndex(e => new { e.CreatedOn, e.IsConsultant }, "X_PatientVisitFlow_CreatedOn_IsConsultant");

            entity.HasIndex(e => new { e.CurrentDepartmentId, e.CurrentSectionId, e.PatientVisitId }, "X_PatientVisitFlow_CurrentDepartmentId_CurrentSectionId_PatientVisitId");

            entity.HasIndex(e => new { e.HealthFacilityId, e.CreatedOn, e.ActionTypeId, e.IsConsultant }, "X_PatientVisitFlow_HealthFacilityId_CreatedOn_ActionTypeId_IsConsultant");

            entity.HasIndex(e => e.IsConsultant, "X_PatientVisitFlow_IsConsultant");

            entity.HasIndex(e => new { e.PatientDiagnoseId, e.IsConsultant }, "X_PatientVisitFlow_PatientDiagnoseId_IsConsultant");

            entity.HasIndex(e => new { e.PatientDiagnoseId, e.IsConsultant, e.CreatedOn }, "X_PatientVisitFlow_PatientDiagnoseId_IsConsultant_CreatedOn");

            entity.HasIndex(e => e.PatientTreatmentCycleNo, "X_PatientVisitFlow_PatientTreatmentCycleNo");

            entity.HasIndex(e => new { e.PatientVisitId, e.CreatedOn, e.IsConsultant }, "X_PatientVisitFlow_PatientVisitId_CreatedOn_IsConsultant");

            entity.Property(e => e.PatientVisitFlowId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsAutoGenerated).HasDefaultValueSql("((0))");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PatientVisitToken>(entity =>
        {
            entity.ToTable("PatientVisitToken", "pt");

            entity.Property(e => e.PatientVisitTokenId).ValueGeneratedNever();
            entity.Property(e => e.Date).HasColumnType("date");
        });

        modelBuilder.Entity<PatientVital>(entity =>
        {
            entity.ToTable("PatientVitals", "pt");

            entity.HasIndex(e => new { e.PatientId, e.CreatedOn }, "AII_PatientVitals_PatientId_CreatedOn");

            entity.HasIndex(e => new { e.PatientId, e.CreatedOn }, "AI_PatientVitals_PatientId_CreatedOn");

            entity.HasIndex(e => e.ActionTypeId, "IA_PatientVitals_ActionTypeId");

            entity.HasIndex(e => new { e.CreatedOn, e.ActionTypeId }, "IIIX_PatientVitals_CreatedOn_ActionTypeId");

            entity.HasIndex(e => new { e.CreatedOn, e.ActionTypeId }, "IX_PatientVitals_CreatedOn_ActionTypeId");

            entity.HasIndex(e => new { e.CreatedOn, e.ActionTypeId }, "XIX_PatientVitals_CreatedOn_ActionTypeId");

            entity.HasIndex(e => e.PatientVisitId, "XIX_PatientVitals_PatientVisitId");

            entity.HasIndex(e => e.CreatedOn, "XI_PatientVitals_CreatedOn");

            entity.HasIndex(e => e.PatientVisitId, "XXII_PatientVitals_PatientVisitId");

            entity.HasIndex(e => new { e.CreatedOn, e.ActionTypeId }, "XX_PatientVitals_CreatedOn_ActionTypeId");

            entity.HasIndex(e => e.PatientVisitId, "XX_PatientVitals_PatientVisitId");

            entity.HasIndex(e => e.CreatedOn, "X_PatientVitals_CreatedOn");

            entity.HasIndex(e => new { e.CreatedOn, e.ActionTypeId }, "X_PatientVitals_CreatedOn_ActionTypeId");

            entity.Property(e => e.PatientVitalId).ValueGeneratedNever();
            entity.Property(e => e.Bmi)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BMI");
            entity.Property(e => e.BpdiaSystolic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("BPDiaSystolic");
            entity.Property(e => e.Bpsystolic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("BPSystolic");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.BloodSugar).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Height)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Pulse)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ResperatoryRate)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Temprature)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Weight)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.PatientVitalCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_PatientVitals_Users1");

            entity.HasOne(d => d.Patient).WithMany(p => p.PatientVitals)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_PatientVitals_Patient");

            entity.HasOne(d => d.PatientVisit).WithMany(p => p.PatientVitals)
                .HasForeignKey(d => d.PatientVisitId)
                .HasConstraintName("FK_PatientVitals_PatientOpenVisit");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.PatientVitalUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_PatientVitals_Users2");

            entity.HasOne(d => d.VitalsCollectedByNavigation).WithMany(p => p.PatientVitalVitalsCollectedByNavigations)
                .HasForeignKey(d => d.VitalsCollectedBy)
                .HasConstraintName("FK_PatientVitals_Users");
        });




        modelBuilder.Entity<PatientWorkFlowLog>(entity =>
        {
            entity.ToTable("PatientWorkFlowLog", "pt");

            entity.HasIndex(e => e.PatientVisitId, "IX_PatientWorkFlowLog_PatientVisitId");

            entity.Property(e => e.PatientWorkFlowLogId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.TimeDifference)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });


        modelBuilder.Entity<SpeechMilestone>(entity =>
        {
            entity.ToTable("SpeechMilestone", "sph");

            entity.Property(e => e.SpeechMilestoneId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
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

        modelBuilder.Entity<PhysiotherapyForm>(entity =>
        {
            entity.ToTable("PhysiotherapyForm", "pt");

            entity.Property(e => e.PhysiotherapyFormId).ValueGeneratedNever();
            entity.Property(e => e.AnyComorbidity)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.ClinicalDiagnosis)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.DrugHistory)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.FrequencyOfExercise)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IntensityOfExercise)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.KeyTreatment).IsUnicode(false);
            entity.Property(e => e.PhysiotherapyDiagnosis)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.PresentingComplaint)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.ProblemSince).HasColumnType("date");
            entity.Property(e => e.Prognosis)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SignificantExaminationFindings)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.TotalDurationOfTreatmentSession)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TypeOfExercise)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.DischargePlanOfCareProfile).WithMany(p => p.PhysiotherapyForms)
                .HasForeignKey(d => d.DischargePlanOfCareProfileId)
                .HasConstraintName("FK_PhysiotherapyForm_Profile");

            entity.HasOne(d => d.PatientDiagnose).WithMany(p => p.PhysiotherapyForms)
                .HasForeignKey(d => d.PatientDiagnoseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhysiotherapyForm_PatientDiagnose");
        });


        modelBuilder.Entity<PhysiotherapyHomeExercisePlan>(entity =>
        {
            entity.ToTable("PhysiotherapyHomeExercisePlan", "pt");

            entity.Property(e => e.PhysiotherapyHomeExercisePlanId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<PhysiotherapyModality>(entity =>
        {
            entity.HasKey(e => e.PhysiotherapyModalitiesId);

            entity.ToTable("PhysiotherapyModalities", "pt");

            entity.Property(e => e.PhysiotherapyModalitiesId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Value)
                .HasMaxLength(2000)
                .IsUnicode(false);

            entity.HasOne(d => d.DepartmentLookup).WithMany(p => p.PhysiotherapyModalities)
                .HasForeignKey(d => d.DepartmentLookupId)
                .HasConstraintName("FK_PhysiotherapyModalities_DepartmentLookup");

            entity.HasOne(d => d.ModalitiesProfile).WithMany(p => p.PhysiotherapyModalities)
                .HasForeignKey(d => d.ModalitiesProfileId)
                .HasConstraintName("FK_PhysiotherapyModalities_Profile");

            entity.HasOne(d => d.PhysiotherapyForm).WithMany(p => p.PhysiotherapyModalities)
                .HasForeignKey(d => d.PhysiotherapyFormId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PhysiotherapyModalities_PhysiotherapyForm");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.ToTable("Profile");

            entity.HasIndex(e => new { e.ProfileTypeId, e.IsActive }, "XIII_Profile");

            entity.HasIndex(e => new { e.ProfileTypeId, e.IsActive }, "XII_Profile");

            entity.HasIndex(e => new { e.ProfileTypeId, e.IsActive }, "XIV_Profile");

            entity.HasIndex(e => new { e.IsActive, e.ActionTypeId }, "XIX_Profile_IsActive_ActionTypeId");

            entity.HasIndex(e => new { e.ShortName, e.IsActive, e.ActionTypeId }, "XIX_Profile_ShortName_IsActive_ActionTypeId");

            entity.HasIndex(e => e.ProfileTypeId, "XI_Profile");

            entity.HasIndex(e => new { e.ActionTypeId, e.ShortName, e.IsActive }, "XV_Profile");

            entity.HasIndex(e => new { e.ProfileTypeId, e.IsActive }, "X_Profile");

            entity.HasIndex(e => e.Name, "X_Profile_Name");

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

        modelBuilder.Entity<RiderStatusLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("RiderStatusLog", "pt");

            entity.Property(e => e.ActionDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsActive)
                .IsRequired()
                .HasDefaultValueSql("((1))");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.StatusUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<RiskFactor>(entity =>
        {
            entity.ToTable("RiskFactors", "pt");

            entity.Property(e => e.RiskFactorId).ValueGeneratedNever();
            entity.Property(e => e.Answer)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
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

        modelBuilder.Entity<SampleConsignment>(entity =>
        {
            entity.ToTable("SampleConsignment", "pat");

            entity.Property(e => e.SampleConsignmentId).ValueGeneratedNever();
            entity.Property(e => e.BatchNo).HasMaxLength(50);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.StatusReason)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<SampleConsignmentDetail>(entity =>
        {
            entity.ToTable("SampleConsignmentDetail", "pat");

            entity.Property(e => e.SampleConsignmentDetailId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.LabTestName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StatusReason)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.SampleConsignment).WithMany(p => p.SampleConsignmentDetails)
                .HasForeignKey(d => d.SampleConsignmentId)
                .HasConstraintName("FK_SampleConsignmentDetail_SampleConsignment");
        });

        modelBuilder.Entity<SectionLookup>(entity =>
        {
            entity.HasKey(e => e.SectionLookupId).HasName("PK_SectionLoopup");

            entity.ToTable("SectionLookup", "lkup");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.DisplayName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FormType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('GeneralForm')");
            entity.Property(e => e.MimsWardId).HasDefaultValueSql("((0))");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.DepartmentLookup).WithMany(p => p.SectionLookups)
                .HasForeignKey(d => d.DepartmentLookupId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SectionLookup_DepartmentLookup");
        });

        modelBuilder.Entity<SectionProcedure>(entity =>
        {
            entity.ToTable("SectionProcedure", "pt");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(200);
            entity.Property(e => e.ProcedureTitle)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<SocialWelfareAssignDoctor>(entity =>
        {
            entity.HasKey(e => e.SwDoctorAssignId).HasName("PK__SocialWe__EA0C8DC1F8F71CF1");

            entity.ToTable("SocialWelfareAssignDoctor", "da");

            entity.Property(e => e.SwDoctorAssignId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsAssignDoctor).HasColumnName("isAssignDoctor");
            entity.Property(e => e.UpdatedBy).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<SocialWelfareForm>(entity =>
        {
            entity.ToTable("SocialWelfareForms", "da");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsVisitClosed).HasColumnName("isVisitClosed");
            entity.Property(e => e.MdrcanyOtherMso)
                .HasMaxLength(1000)
                .HasColumnName("MDRCAnyOtherMSO");
            entity.Property(e => e.Mdrccfodabuse)
                .HasMaxLength(1000)
                .HasColumnName("MDRCCFODAbuse");
            entity.Property(e => e.MdrcdetailOfCounsellingSessionsSesssionI).HasColumnName("MDRCDetailOfCounsellingSessionsSesssionI");
            entity.Property(e => e.MdrcdetailOfCounsellingSessionsSesssionIi).HasColumnName("MDRCDetailOfCounsellingSessionsSesssionII");
            entity.Property(e => e.MdrcdetailOfCounsellingSessionsSesssionIii).HasColumnName("MDRCDetailOfCounsellingSessionsSesssionIII");
            entity.Property(e => e.MdrcfamilyAttitude)
                .HasMaxLength(1000)
                .HasColumnName("MDRCFamilyAttitude");
            entity.Property(e => e.MdrcfhrdrugAddiction)
                .HasMaxLength(1000)
                .HasColumnName("MDRCFHRDrugAddiction");
            entity.Property(e => e.MdrcifYesRelation)
                .HasMaxLength(1000)
                .HasColumnName("MDRCIfYesRelation");
            entity.Property(e => e.MdrcindoorActivities)
                .HasMaxLength(1000)
                .HasColumnName("MDRCIndoorActivities");
            entity.Property(e => e.MdrcmonthlyIncome)
                .HasMaxLength(1000)
                .HasColumnName("MDRCMonthlyIncome");
            entity.Property(e => e.MdrcmsoprovisionReadingMaterial)
                .HasMaxLength(1000)
                .HasColumnName("MDRCMSOProvisionReadingMaterial");
            entity.Property(e => e.MdrcpatientAttitude)
                .HasMaxLength(1000)
                .HasColumnName("MDRCPatientAttitude");
            entity.Property(e => e.Mdrcprofession)
                .HasMaxLength(1000)
                .HasColumnName("MDRCProfession");
            entity.Property(e => e.MdrcrecreationalActivities)
                .HasMaxLength(1000)
                .HasColumnName("MDRCRecreationalActivities");
            entity.Property(e => e.ReferDivisionId).HasColumnName("ReferDivisionID");
            entity.Property(e => e.ReferProvinceId).HasColumnName("ReferProvinceID");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<SocialWelfareTaskPerformedByCd>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SocialWe__3214EC075C0D0B3D");

            entity.ToTable("SocialWelfareTaskPerformedByCD", "da");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CounsellingOfPatientOrFamily).IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.GuidanceProvided).IsUnicode(false);
            entity.Property(e => e.PhoneNo).IsUnicode(false);
            entity.Property(e => e.PtDailyRoutine).IsUnicode(false);
            entity.Property(e => e.PtDoctorCheckup).IsUnicode(false);
            entity.Property(e => e.PtFamilyAttitude).IsUnicode(false);
            entity.Property(e => e.PtMedicineRoutine).IsUnicode(false);
            entity.Property(e => e.PtRelativeOrOtherDetail).IsUnicode(false);
            entity.Property(e => e.PtStatus).IsUnicode(false);
            entity.Property(e => e.RelationWithPt).IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.VisitDate).HasColumnType("datetime");
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

        modelBuilder.Entity<SscVisitStatusLog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("SscVisitStatusLog", "pt");

            entity.Property(e => e.ActionDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsAdmittedInIpd).HasColumnName("IsAdmittedInIPD");
            entity.Property(e => e.IsAutoGenerated).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsFromPmis).HasColumnName("IsFromPMIS");
            entity.Property(e => e.IsVisitExternally).HasDefaultValueSql("((0))");
            entity.Property(e => e.SlipNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SscNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SscStatusReason)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SscStatusUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.TokenNo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.VisitDate).HasColumnType("date");
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

        modelBuilder.Entity<TbPatientDetail>(entity =>
        {
            entity.HasKey(e => e.TbPatientDetailsId).HasName("PK_TbPatientDetails_1");

            entity.ToTable("TbPatientDetails", "pt");

            entity.Property(e => e.TbPatientDetailsId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Drtbcenter)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DRTBCenter");
            entity.Property(e => e.IsReferToDrtb).HasColumnName("IsReferToDRTB");
            entity.Property(e => e.PatientStatus)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.PatientDiagnose).WithMany(p => p.TbPatientDetails)
                .HasForeignKey(d => d.PatientDiagnoseId)
                .HasConstraintName("FK_TbPatientDetails_PatientDiagnose");

            entity.HasOne(d => d.Patient).WithMany(p => p.TbPatientDetails)
                .HasForeignKey(d => d.PatientId)
                .HasConstraintName("FK_TbPatientDetails_TbPatientDetails");

            entity.HasOne(d => d.PatientVisit).WithMany(p => p.TbPatientDetails)
                .HasForeignKey(d => d.PatientVisitId)
                .HasConstraintName("FK_TbPatientDetails_PatientOpenVisit");
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

        modelBuilder.Entity<UnknownPatient>(entity =>
        {
            entity.ToTable("UnknownPatient", "pt");

            entity.Property(e => e.UnknownPatientId).ValueGeneratedNever();
            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Domicile)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FollowupDate).HasColumnType("date");
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.GuardianName).HasMaxLength(150);
            entity.Property(e => e.Hfmiscode)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("HFMISCode");
            entity.Property(e => e.IsRegisteredExternally).HasDefaultValueSql("((0))");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.NameOfCnicHolder).HasMaxLength(150);
            entity.Property(e => e.Ntn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NTN");
            entity.Property(e => e.ParmanentAddress).HasMaxLength(250);
            entity.Property(e => e.PassportNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SourceMrno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SourcePatientId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TemporaryAddress).HasMaxLength(250);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<UnknownPatientsListView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("UnknownPatientsListView");

            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.TransactionId).IsUnicode(false);
            entity.Property(e => e.TransctionSaveTime).HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable(tb => tb.HasTrigger("trg_UserRegistrationLog"));

            entity.HasIndex(e => e.SectionId, "AIII_Users_SectionId");

            entity.HasIndex(e => e.ActionTypeId, "AIU_Users_ActionTypeId");

            entity.HasIndex(e => e.SectionId, "AI_Users_SectionId");

            entity.HasIndex(e => e.TehsilId, "IAIV_Users_TehsilId");

            entity.HasIndex(e => e.TehsilId, "IA_Users_TehsilId");

            entity.HasIndex(e => new { e.ProvinceId, e.DivisionId, e.HealthFacilityId, e.ActionTypeId }, "ICX_Users_ProvinceId_DivisionId_HealthFacilityId_ActionTypeId");

            entity.HasIndex(e => e.Cnic, "IX_Users_CNIC");

            entity.HasIndex(e => e.Email, "Unique_Email").IsUnique();

            entity.HasIndex(e => e.Username, "Unique_Username").IsUnique();

            entity.HasIndex(e => new { e.ProvinceId, e.DivisionId, e.DistrictId, e.TehsilId, e.ActionTypeId }, "XIX_Users_ProvinceId_DivisionId_DistrictId_TehsilId_ActionTypeId");

            entity.HasIndex(e => new { e.ProvinceId, e.DivisionId, e.DistrictId, e.TehsilId, e.HealthFacilityId, e.ActionTypeId }, "XIX_Users_ProvinceId_DivisionId_DistrictId_TehsilId_HealthFacilityId_ActionTypeId");

            entity.HasIndex(e => new { e.ProvinceId, e.DivisionId, e.UserId, e.ActionTypeId }, "XIX_Users_ProvinceId_DivisionId_UserId_ActionTypeId");

            entity.HasIndex(e => new { e.HealthFacilityId, e.DeletedOn }, "XI_Users_HealthFacilityId_DeletedOn");

            entity.HasIndex(e => new { e.ProvinceId, e.CreatedBy, e.UserId, e.ActionTypeId }, "XI_Users_ProvinceId_CreatedBy_UserId_ActionTypeId");

            entity.HasIndex(e => new { e.ProvinceId, e.UserId, e.ActionTypeId }, "XI_Users_ProvinceId_UserId_ActionTypeId");

            entity.HasIndex(e => new { e.UserId, e.ActionTypeId, e.ProvinceId, e.DivisionId }, "X_Users");

            entity.HasIndex(e => e.ContactNo, "X_Users_ContactNo");

            entity.HasIndex(e => e.HealthFacilityId, "X_Users_HealthFacilityId");

            entity.HasIndex(e => new { e.HealthFacilityId, e.DeletedOn }, "X_Users_HealthFacilityId_DeletedOn");

            entity.HasIndex(e => e.HrId, "X_Users_HrId");

            entity.HasIndex(e => e.PmisUser, "X_Users_PmisUSER");

            entity.HasIndex(e => new { e.ProvinceId, e.CreatedBy, e.UserId, e.ActionTypeId }, "X_Users_ProvinceId_CreatedBy_UserId_ActionTypeId");

            entity.HasIndex(e => new { e.ProvinceId, e.DivisionId, e.ActionTypeId }, "X_Users_ProvinceId_DivisionId_ActionTypeId");

            entity.HasIndex(e => new { e.ProvinceId, e.DivisionId, e.DistrictId, e.TehsilId, e.HealthFacilityId, e.UserId, e.ActionTypeId }, "X_Users_ProvinceId_DivisionId_DistrictId_TehsilId_HealthFacilityId_UserId_ActionTypeId");

            entity.HasIndex(e => new { e.ProvinceId, e.UserId, e.ActionTypeId }, "X_Users_ProvinceId_UserId_ActionTypeId");

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
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.PmisUser).HasColumnName("PmisUSER");
            entity.Property(e => e.ProfilePic)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(150);

            entity.HasOne(d => d.Department).WithMany(p => p.Users)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Users_DepartmentLookup");

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

            entity.HasOne(d => d.Section).WithMany(p => p.Users)
                .HasForeignKey(d => d.SectionId)
                .HasConstraintName("FK_Users_SectionLookup");

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


        modelBuilder.Entity<PastPsychiatricHistory>(entity =>
        {
            entity.ToTable("PastPsychiatricHistory", "psy");

            entity.Property(e => e.PastPsychiatricHistoryId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });


        modelBuilder.Entity<PsychologicalAssessment>(entity =>
        {
            entity.ToTable("PsychologicalAssessment", "psy");

            entity.Property(e => e.PsychologicalAssessmentId).ValueGeneratedNever();
            entity.Property(e => e.Affect)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Appearance)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CognitiveDeficits)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.EstimatedIntellectualFunctioning)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Insight)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Judgment)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Memory)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MentalStatusExamination)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mood)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Orientation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PerceptualProcess)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Speech)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ThroughContent)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ThroughProcess)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });


        modelBuilder.Entity<PsychologicalTestApplied>(entity =>
        {
            entity.ToTable("PsychologicalTestApplied", "psy");

            entity.Property(e => e.PsychologicalTestAppliedId).ValueGeneratedNever();
            entity.Property(e => e.BackAnxietyInventory)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.BackDepressionInventory)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.HouseTreePersonTest)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
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
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.ProfilePic)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(150);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_UserRoles_UserId");

            entity.HasIndex(e => e.RoleId, "XIX_UserRoles_RoleId");

            entity.HasIndex(e => e.RoleId, "X_UserRoles_RoleId");

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

        modelBuilder.Entity<VHfdepartmentSection>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("V_HFDepartmentSection");

            entity.Property(e => e.DepartmentName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacilityName).IsUnicode(false);
            entity.Property(e => e.SectionName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewAdviseLabTest>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewAdviseLabTest");

            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
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
            entity.Property(e => e.FormType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacilityName).IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TehsilName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TestName)
                .HasMaxLength(200)
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

        modelBuilder.Entity<ViewDrugAddictCommunityDevelopment>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewDrugAddictCommunityDevelopment", "da");

            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CounsellingOfPatientOrFamily).IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.DoctorName).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.Gender).HasMaxLength(150);
            entity.Property(e => e.GuidanceProvided).IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.PatientDistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNo).IsUnicode(false);
            entity.Property(e => e.PtDailyRoutine).IsUnicode(false);
            entity.Property(e => e.PtDoctorCheckup).IsUnicode(false);
            entity.Property(e => e.PtFamilyAttitude).IsUnicode(false);
            entity.Property(e => e.PtMedicineRoutine).IsUnicode(false);
            entity.Property(e => e.PtStatus).IsUnicode(false);
            entity.Property(e => e.RelationWithPt).IsUnicode(false);
            entity.Property(e => e.VisitDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ViewDrugAddictFieldOfficer>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewDrugAddictFieldOfficer", "da");

            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.DoctorName).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.Gender).HasMaxLength(150);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.PatientDistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PtAttitudeWithFamily).IsUnicode(false);
            entity.Property(e => e.PtDailyRoutine).IsUnicode(false);
            entity.Property(e => e.PtDoctorCheckup).IsUnicode(false);
            entity.Property(e => e.PtMedicineRoutine).IsUnicode(false);
            entity.Property(e => e.PtRelativeOrOtherDetail).IsUnicode(false);
            entity.Property(e => e.PtStatus).IsUnicode(false);
            entity.Property(e => e.RelationWithPt).IsUnicode(false);
            entity.Property(e => e.VisitDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<ViewDrugAddictPatientDisease>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewDrugAddictPatientDiseases", "da");

            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedBy).HasMaxLength(150);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.Name).IsUnicode(false);
            entity.Property(e => e.ProfileName).HasMaxLength(150);
            entity.Property(e => e.ShortName).HasMaxLength(150);
        });

        modelBuilder.Entity<ViewDrugAddictPatientDiseasesCommaSeparated>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewDrugAddictPatientDiseasesCommaSeparated", "da");

            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedBy).HasMaxLength(150);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.Name).IsUnicode(false);
        });


        modelBuilder.Entity<ViewDrugAddictsPatientVisit>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewDrugAddictsPatientVisits");

            entity.Property(e => e.Addicted)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.AdmissionDate).HasColumnType("datetime");
            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.DischargeDate).HasColumnType("datetime");
            entity.Property(e => e.District)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.Gender).HasMaxLength(150);
            entity.Property(e => e.GuardianName).HasMaxLength(150);
            entity.Property(e => e.HealthFacility).IsUnicode(false);
            entity.Property(e => e.ParmanentAddress).HasMaxLength(250);
            entity.Property(e => e.Rehabilitation)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("REHABILITATION");
            entity.Property(e => e.TreatmentStatus)
                .HasMaxLength(9)
                .IsUnicode(false);
        });



        modelBuilder.Entity<ViewEyeBlindnesspatient>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewEyeBlindnesspatient");

            entity.Property(e => e.ComorbidityBy).IsUnicode(false);
            entity.Property(e => e.ConsultantName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DateOfInocvlation).HasColumnType("datetime");
            entity.Property(e => e.EyeInvolved)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.HealthFacility).IsUnicode(false);
            entity.Property(e => e.InjectedHospital).IsUnicode(false);
            entity.Property(e => e.Recovery)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatusOfVision)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewGetAllIpdQueue>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewGetAllIpdQueue");

            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.TokenNo)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewGetAllMedicineAdvisedRequisition>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewGetAllMedicineAdvisedRequisition");

            entity.Property(e => e.RequsitionBy).HasMaxLength(150);
            entity.Property(e => e.RequsitionFor).HasMaxLength(150);
            entity.Property(e => e.RequsitionOn).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(10)
                .IsUnicode(false);
        });


        modelBuilder.Entity<ViewGetAllPatientsCountByPtStatusCd>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewGetAllPatientsCountByPtStatusCD", "da");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.PatientDistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientDivisionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PtStatus).IsUnicode(false);
        });

        modelBuilder.Entity<ViewGetAllPatientsCountCd>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewGetAllPatientsCountCD", "da");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.PatientDistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientDivisionName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewGetAllPatientsCountSw>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewGetAllPatientsCountSW", "da");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.PatientDistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientDivisionName)
                .HasMaxLength(200)
                .IsUnicode(false);
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

        modelBuilder.Entity<ViewGetPatientBaseSixtyFour>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewGetPatientBaseSixtyFour");
        });

        modelBuilder.Entity<ViewGetPatientLabTestList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewGetPatientLabTestList");

            entity.Property(e => e.DepartmentName).HasMaxLength(150);
            entity.Property(e => e.DepartmentShortName).HasMaxLength(150);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PrescribedByDesignation).HasMaxLength(150);
            entity.Property(e => e.PrescribedByName).HasMaxLength(150);
            entity.Property(e => e.PrescribedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ViewGetPatientPrescriptionList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewGetPatientPrescriptionList");

            entity.Property(e => e.MedicineDose).HasMaxLength(200);
            entity.Property(e => e.MedicineFrequency).HasMaxLength(200);
            entity.Property(e => e.MedicineName).HasMaxLength(200);
            entity.Property(e => e.PrescribedByDesignation).HasMaxLength(150);
            entity.Property(e => e.PrescribedByName).HasMaxLength(150);
        });

        modelBuilder.Entity<ViewHealthFacilityDepartmentList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewHealthFacilityDepartmentList");

            entity.Property(e => e.CreatedByName).HasMaxLength(150);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacilityName).IsUnicode(false);
            entity.Property(e => e.UpdatedByName).HasMaxLength(150);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
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

        modelBuilder.Entity<ViewLabTestResult>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewLabTestResults");

            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
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
            entity.Property(e => e.FormType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacilityName).IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Result)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TehsilName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TestName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TestResultName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });


        modelBuilder.Entity<PatientEyeBlindness>(entity =>
        {
            entity.ToTable("PatientEyeBlindness", "pt");

            entity.Property(e => e.PatientEyeBlindnessId).ValueGeneratedNever();
            entity.Property(e => e.ComorbidityBy).IsUnicode(false);
            entity.Property(e => e.ConsultantName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DateOfInocvlation).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.EyeInvolved)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.HospitalInjectedId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Recovery)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StatusOfVision)
                .HasMaxLength(50)
                .IsUnicode(false);
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

        modelBuilder.Entity<ViewMedicineDispatchList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewMedicineDispatchList");

            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedByName).HasMaxLength(150);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DivisionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacilityName).IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.PatientMobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.ProvinceName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TehsilName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedByName).HasMaxLength(150);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.VisitDate).HasColumnType("date");
        });

        modelBuilder.Entity<ViewPatientDiagnoseDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewPatientDiagnoseDetail", "dashboard");

            entity.Property(e => e.Age).HasColumnType("numeric(17, 6)");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.Gender).HasMaxLength(150);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PatientCreatedOn).HasColumnType("datetime");
            entity.Property(e => e.PatientDiagnoseCreatedOn).HasColumnType("datetime");
            entity.Property(e => e.PatientDistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientDivisionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientHf)
                .IsUnicode(false)
                .HasColumnName("PatientHF");
            entity.Property(e => e.PatientProvinceName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientTehsilName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientVisitCreatedByDesignation).HasMaxLength(150);
            entity.Property(e => e.PatientVisitCreatedByName).HasMaxLength(150);
            entity.Property(e => e.PatientVisitCreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Relation).HasMaxLength(150);
            entity.Property(e => e.VisitDate).HasColumnType("date");
            entity.Property(e => e.VisitHf)
                .IsUnicode(false)
                .HasColumnName("VisitHF");
        });
        modelBuilder.Entity<ViewPatientDischargeList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewPatientDischargeList");

            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy).HasMaxLength(150);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Department)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DivisionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FormType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender).HasMaxLength(150);
            entity.Property(e => e.HealthFacilityName).IsUnicode(false);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MrNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientCreatedOn).HasColumnType("datetime");
            entity.Property(e => e.PatientMobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientName).HasMaxLength(150);
            entity.Property(e => e.PatientProvinceName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ProvinceName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Relation).HasMaxLength(150);
            entity.Property(e => e.Section)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SscClaimedDate).HasColumnType("datetime");
            entity.Property(e => e.SscNotConfirmReason).HasMaxLength(150);
            entity.Property(e => e.SscNotEligibleReason).HasMaxLength(150);
            entity.Property(e => e.SscNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SscStatusName)
                .HasMaxLength(18)
                .IsUnicode(false);
            entity.Property(e => e.SscStatusReason)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SscStatusUpdatedBy).HasMaxLength(150);
            entity.Property(e => e.SscStatusUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.TehsilName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TokenNo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy).HasMaxLength(150);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.VisitDate).HasColumnType("date");
        });

        modelBuilder.Entity<ViewPatientLabTestDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewPatientLabTestDetail", "dashboard");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Department)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Gender).HasMaxLength(150);
            entity.Property(e => e.LabTestName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.LabType).HasMaxLength(150);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PatientCreatedOn).HasColumnType("datetime");
            entity.Property(e => e.PatientDistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientDivisionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientHf)
                .IsUnicode(false)
                .HasColumnName("PatientHF");
            entity.Property(e => e.PatientProvinceName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientTehsilName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientVisitCreatedByName).HasMaxLength(150);
            entity.Property(e => e.PatientVisitCreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Relation).HasMaxLength(150);
            entity.Property(e => e.ReportGeneratedByName).HasMaxLength(150);
            entity.Property(e => e.SampleCollectedByName).HasMaxLength(150);
            entity.Property(e => e.VisitDate).HasColumnType("date");
            entity.Property(e => e.VisitHf)
                .IsUnicode(false)
                .HasColumnName("VisitHF");
        });

        modelBuilder.Entity<ViewPatientLabTestList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewPatientLabTestList");

            entity.Property(e => e.AdvisedBy).HasMaxLength(150);
            entity.Property(e => e.AdvisedOn).HasColumnType("datetime");
            entity.Property(e => e.ArchivedOn).HasColumnType("datetime");
            entity.Property(e => e.BarcodeNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.DocDepartmentName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DocSectionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacilityName).IsUnicode(false);
            entity.Property(e => e.LabDepartmentName).HasMaxLength(150);
            entity.Property(e => e.LabDepartmentShortName).HasMaxLength(150);
            entity.Property(e => e.LabTestName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.LabType).HasMaxLength(150);
            entity.Property(e => e.LabTypeShortName).HasMaxLength(150);
            entity.Property(e => e.MrNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientMobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientName).HasMaxLength(150);
            entity.Property(e => e.PreGeneratedBarcodeNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ReportGeneratedBy).HasMaxLength(150);
            entity.Property(e => e.ReportGeneratedOn).HasColumnType("datetime");
            entity.Property(e => e.ReportLink)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ResultImageLink)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SampleCollectedBy).HasMaxLength(150);
            entity.Property(e => e.SampleCollectedOn).HasColumnType("datetime");
            entity.Property(e => e.SampleRejectedBy).HasMaxLength(150);
            entity.Property(e => e.SampleRejectedOn).HasColumnType("datetime");
            entity.Property(e => e.SampleRejectedReason)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.SampleType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SourceDoctorName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StageName)
                .HasMaxLength(29)
                .IsUnicode(false);
            entity.Property(e => e.StatusName)
                .HasMaxLength(29)
                .IsUnicode(false);
            entity.Property(e => e.TestPrice).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<ViewPatientOpenVisiDashbaordList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewPatientOpenVisiDashbaordList");

            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy).HasMaxLength(150);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Department)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DivisionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(150);
            entity.Property(e => e.HealthFacilityName).IsUnicode(false);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MrNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientCreatedOn).HasColumnType("datetime");
            entity.Property(e => e.PatientMobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientName).HasMaxLength(150);
            entity.Property(e => e.PatientProvinceName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ProvinceName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Relation).HasMaxLength(150);
            entity.Property(e => e.Section)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SscClaimedDate).HasColumnType("datetime");
            entity.Property(e => e.SscNotConfirmReason).HasMaxLength(150);
            entity.Property(e => e.SscNotEligibleReason).HasMaxLength(150);
            entity.Property(e => e.SscNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SscStatusName)
                .HasMaxLength(18)
                .IsUnicode(false);
            entity.Property(e => e.TehsilName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TokenNo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy).HasMaxLength(150);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.VisitDate).HasColumnType("date");
        });

        modelBuilder.Entity<ViewPatientOpenVisit>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewPatientOpenVisit");

            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Domicile)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.GuardianName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.VisitDate).HasColumnType("date");
        });

        modelBuilder.Entity<ViewPatientOpenVisitCountByDeptBySecByMonth>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewPatientOpenVisitCountByDeptBySecByMonth");

            entity.Property(e => e.DepartmentName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SectionName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewPatientOpenVisitCountByGenderByMonth>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewPatientOpenVisitCountByGenderByMonth");

            entity.Property(e => e.Gender)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewPatientOpenVisitDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewPatientOpenVisitDetail", "dashboard");

            entity.Property(e => e.Age).HasColumnType("numeric(17, 6)");
            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.CurrentStation).HasMaxLength(150);
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.Gender).HasMaxLength(150);
            entity.Property(e => e.IsFromPmis).HasColumnName("IsFromPMIS");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.PatientCreatedOn).HasColumnType("datetime");
            entity.Property(e => e.PatientDistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientDivisionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientHf)
                .IsUnicode(false)
                .HasColumnName("PatientHF");
            entity.Property(e => e.PatientProvinceName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientTehsilName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientVisitCreatedByCnic).HasMaxLength(16);
            entity.Property(e => e.PatientVisitCreatedByDesignation).HasMaxLength(150);
            entity.Property(e => e.PatientVisitCreatedByName).HasMaxLength(150);
            entity.Property(e => e.Relation).HasMaxLength(150);
            entity.Property(e => e.SectionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.VisitDate).HasColumnType("date");
            entity.Property(e => e.VisitHf)
                .IsUnicode(false)
                .HasColumnName("VisitHF");
        });

        modelBuilder.Entity<ViewPatientOpenVisitList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewPatientOpenVisitList");

            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy).HasMaxLength(150);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Department)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DivisionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FormType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender).HasMaxLength(150);
            entity.Property(e => e.HealthFacilityName).IsUnicode(false);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MrNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientCreatedOn).HasColumnType("datetime");
            entity.Property(e => e.PatientMobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientName).HasMaxLength(150);
            entity.Property(e => e.PatientProvinceName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ProvinceName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Relation).HasMaxLength(150);
            entity.Property(e => e.Section)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SscClaimedDate).HasColumnType("datetime");
            entity.Property(e => e.SscNotConfirmReason).HasMaxLength(150);
            entity.Property(e => e.SscNotEligibleReason).HasMaxLength(150);
            entity.Property(e => e.SscNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SscStatusName)
                .HasMaxLength(18)
                .IsUnicode(false);
            entity.Property(e => e.SscStatusReason)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SscStatusUpdatedBy).HasMaxLength(150);
            entity.Property(e => e.SscStatusUpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.TehsilName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.TokenNo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedBy).HasMaxLength(150);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.VisitDate).HasColumnType("date");
        });

        modelBuilder.Entity<ViewPatientPharmacyDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewPatientPharmacyDetail", "dashboard");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Gender).HasMaxLength(150);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MedicineName).HasMaxLength(200);
            entity.Property(e => e.PatientCreatedOn).HasColumnType("datetime");
            entity.Property(e => e.PatientDistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientDivisionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientHf)
                .IsUnicode(false)
                .HasColumnName("PatientHF");
            entity.Property(e => e.PatientProvinceName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientTehsilName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientVisitCreatedByName).HasMaxLength(150);
            entity.Property(e => e.PatientVisitCreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Relation).HasMaxLength(150);
            entity.Property(e => e.VisitDate).HasColumnType("date");
            entity.Property(e => e.VisitHf)
                .IsUnicode(false)
                .HasColumnName("VisitHF");
        });

        modelBuilder.Entity<ViewPatientRegistrationDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewPatientRegistrationDetail", "dashboard");

            entity.Property(e => e.Age).HasColumnType("numeric(17, 6)");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Gender).HasMaxLength(150);
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PatientDistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientDivisionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientProvinceName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientTehsilName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Relation).HasMaxLength(150);
        });

        modelBuilder.Entity<ViewPatientVisitFlow>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewPatientVisitFlow", "dashboard");

            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ViewPatientVitalDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewPatientVitalDetail", "dashboard");

            entity.Property(e => e.Age).HasColumnType("numeric(17, 6)");
            entity.Property(e => e.BpdiaSystolic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("BPDiaSystolic");
            entity.Property(e => e.Bpsystolic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("BPSystolic");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.Gender).HasMaxLength(150);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.PatientCreatedOn).HasColumnType("datetime");
            entity.Property(e => e.PatientDistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientDivisionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientHf)
                .IsUnicode(false)
                .HasColumnName("PatientHF");
            entity.Property(e => e.PatientProvinceName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientTehsilName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientVisitCreatedByDesignation).HasMaxLength(150);
            entity.Property(e => e.PatientVisitCreatedByName).HasMaxLength(150);
            entity.Property(e => e.PatientVisitCreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Pulse)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Relation).HasMaxLength(150);
            entity.Property(e => e.ResperatoryRate)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Temprature)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.VisitDate).HasColumnType("date");
            entity.Property(e => e.VisitHf)
                .IsUnicode(false)
                .HasColumnName("VisitHF");
            entity.Property(e => e.Weight)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewPatientVitalList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewPatientVitalList");

            entity.Property(e => e.BpdiaSystolic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("BPDiaSystolic");
            entity.Property(e => e.Bpsystolic)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("BPSystolic");
            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedByName).HasMaxLength(150);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.CurrentStation).HasMaxLength(150);
            entity.Property(e => e.DistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DivisionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacilityName).IsUnicode(false);
            entity.Property(e => e.Height)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.PatientMobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.ProvinceName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Pulse)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.ResperatoryRate)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TehsilName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Temprature)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedByName).HasMaxLength(150);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            entity.Property(e => e.VisitDate).HasColumnType("date");
            entity.Property(e => e.Weight)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewRiderLabTestList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewRiderLabTestList");

            entity.Property(e => e.AdvisedBy).HasMaxLength(150);
            entity.Property(e => e.AdvisedOn).HasColumnType("datetime");
            entity.Property(e => e.BarcodeNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.HealthFacilityName).IsUnicode(false);
            entity.Property(e => e.LabDepartmentName).HasMaxLength(150);
            entity.Property(e => e.LabDepartmentShortName).HasMaxLength(150);
            entity.Property(e => e.LabTestName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.LabType).HasMaxLength(150);
            entity.Property(e => e.LabTypeShortName).HasMaxLength(150);
            entity.Property(e => e.MrNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientMobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.PreGeneratedBarcodeNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ReportGeneratedBy).HasMaxLength(150);
            entity.Property(e => e.ReportGeneratedOn).HasColumnType("datetime");
            entity.Property(e => e.ReportLink)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ResultImageLink)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SampleCollectedBy).HasMaxLength(150);
            entity.Property(e => e.SampleCollectedOn).HasColumnType("datetime");
            entity.Property(e => e.SampleRejectedBy).HasMaxLength(150);
            entity.Property(e => e.SampleRejectedOn).HasColumnType("datetime");
            entity.Property(e => e.SampleRejectedReason)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.SampleType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SourceDoctorName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StatusName)
                .HasMaxLength(29)
                .IsUnicode(false);
            entity.Property(e => e.TestPrice).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<ViewSampleCollectedConsignmentList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewSampleCollectedConsignmentList");

            entity.Property(e => e.AdvisedBy).HasMaxLength(150);
            entity.Property(e => e.AdvisedOn).HasColumnType("datetime");
            entity.Property(e => e.ArchivedOn).HasColumnType("datetime");
            entity.Property(e => e.BarcodeNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.DocDepartmentName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DocSectionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacilityName).IsUnicode(false);
            entity.Property(e => e.LabDepartmentName).HasMaxLength(150);
            entity.Property(e => e.LabDepartmentShortName).HasMaxLength(150);
            entity.Property(e => e.LabTestName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.LabType).HasMaxLength(150);
            entity.Property(e => e.LabTypeShortName).HasMaxLength(150);
            entity.Property(e => e.MrNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientMobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PatientName).HasMaxLength(150);
            entity.Property(e => e.PreGeneratedBarcodeNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ReportGeneratedBy).HasMaxLength(150);
            entity.Property(e => e.ReportGeneratedOn).HasColumnType("datetime");
            entity.Property(e => e.ReportLink)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ResultImageLink)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SampleCollectedBy).HasMaxLength(150);
            entity.Property(e => e.SampleCollectedOn).HasColumnType("datetime");
            entity.Property(e => e.SampleRejectedBy).HasMaxLength(150);
            entity.Property(e => e.SampleRejectedOn).HasColumnType("datetime");
            entity.Property(e => e.SampleRejectedReason)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.SampleType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SourceDoctorName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StageName)
                .HasMaxLength(29)
                .IsUnicode(false);
            entity.Property(e => e.StatusName)
                .HasMaxLength(29)
                .IsUnicode(false);
            entity.Property(e => e.TestPrice).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<ViewSampleConsignmentList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewSampleConsignmentList");

            entity.Property(e => e.BatchNo).HasMaxLength(50);
            entity.Property(e => e.ConsignmentStatusReason)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FromHealthFacility).IsUnicode(false);
            entity.Property(e => e.SampleConsignmentCreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.ToHealthFacility).IsUnicode(false);
        });

        modelBuilder.Entity<ViewSampleConsignmentWithDetailList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewSampleConsignmentWithDetailList");

            entity.Property(e => e.BarcodeNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.BatchNo).HasMaxLength(50);
            entity.Property(e => e.ConsignmentDetailStatusReason)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ConsignmentStatusReason)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FromHealthFacility).IsUnicode(false);
            entity.Property(e => e.LabTestName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PatientName).HasMaxLength(150);
            entity.Property(e => e.SampleConsignemntCreated).HasColumnType("datetime");
            entity.Property(e => e.SampleConsignemntDetailCreated).HasColumnType("datetime");
            entity.Property(e => e.SampleConsignmentCreatedBy).HasMaxLength(150);
            entity.Property(e => e.SampleConsignmentCreatedOn).HasColumnType("datetime");
            entity.Property(e => e.SampleConsignmentDetailCreatedBy).HasMaxLength(150);
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.ToHealthFacility).IsUnicode(false);
        });

        modelBuilder.Entity<ViewSocialWellfareDeputyDirector>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewSocialWellfareDeputyDirector", "da");

            entity.Property(e => e.Age).HasColumnType("numeric(17, 6)");
            entity.Property(e => e.AssignedDoctor).HasMaxLength(150);
            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.Gender).HasMaxLength(150);
            entity.Property(e => e.IsAssignDoctor).HasColumnName("isAssignDoctor");
            entity.Property(e => e.IsVisitClosed).HasColumnName("isVisitClosed");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.PatientDistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientDivisionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.VisitDate).HasColumnType("date");
            entity.Property(e => e.VisitHf)
                .IsUnicode(false)
                .HasColumnName("VisitHF");
        });

        modelBuilder.Entity<ViewSocialWellfareDoctor>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewSocialWellfareDoctors", "da");

            entity.Property(e => e.FatherName).HasMaxLength(150);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewSocialWellfarePatientDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewSocialWellfarePatientDetail", "da");

            entity.Property(e => e.Age).HasColumnType("numeric(17, 6)");
            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.Gender).HasMaxLength(150);
            entity.Property(e => e.IsVisitClosed).HasColumnName("isVisitClosed");
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.MdrcanyOtherMso)
                .HasMaxLength(1000)
                .HasColumnName("MDRCAnyOtherMSO");
            entity.Property(e => e.MdrcdetailOfCounsellingSessionsSesssionI).HasColumnName("MDRCDetailOfCounsellingSessionsSesssionI");
            entity.Property(e => e.MdrcfamilyAttitude)
                .HasMaxLength(1000)
                .HasColumnName("MDRCFamilyAttitude");
            entity.Property(e => e.MdrcfhrdrugAddiction)
                .HasMaxLength(1000)
                .HasColumnName("MDRCFHRDrugAddiction");
            entity.Property(e => e.MdrcifYesRelation)
                .HasMaxLength(1000)
                .HasColumnName("MDRCIfYesRelation");
            entity.Property(e => e.MdrcindoorActivities)
                .HasMaxLength(1000)
                .HasColumnName("MDRCIndoorActivities");
            entity.Property(e => e.MdrcmonthlyIncome)
                .HasMaxLength(1000)
                .HasColumnName("MDRCMonthlyIncome");
            entity.Property(e => e.MdrcmsoprovisionReadingMaterial)
                .HasMaxLength(1000)
                .HasColumnName("MDRCMSOProvisionReadingMaterial");
            entity.Property(e => e.MdrcpatientAttitude)
                .HasMaxLength(1000)
                .HasColumnName("MDRCPatientAttitude");
            entity.Property(e => e.Mdrcprofession)
                .HasMaxLength(1000)
                .HasColumnName("MDRCProfession");
            entity.Property(e => e.MdrcrecreationalActivities)
                .HasMaxLength(1000)
                .HasColumnName("MDRCRecreationalActivities");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.PatientCreatedOn).HasColumnType("datetime");
            entity.Property(e => e.PatientDistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientDivisionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientHf)
                .IsUnicode(false)
                .HasColumnName("PatientHF");
            entity.Property(e => e.PatientProvinceName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientTehsilName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PatientVisitCreatedByName).HasMaxLength(150);
            entity.Property(e => e.PatientVisitCreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ReferDivisionId).HasColumnName("ReferDivisionID");
            entity.Property(e => e.Relation).HasMaxLength(150);
            entity.Property(e => e.VisitDate).HasColumnType("date");
            entity.Property(e => e.VisitHf)
                .IsUnicode(false)
                .HasColumnName("VisitHF");
        });

        modelBuilder.Entity<ViewTbPatientCount>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewTbPatientCounts");

            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
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
            entity.Property(e => e.FormType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TehsilName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewTbPatientsList>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewTbPatientsList");

            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedBy).HasMaxLength(150);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.FormType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
        });

        modelBuilder.Entity<ViewTbRegisteredPatient>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewTbRegisteredPatient");

            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
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
            entity.Property(e => e.FormType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.HealthFacilityName).IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.TehsilCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.TehsilName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });


        modelBuilder.Entity<ViewTbissuedMedicine>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("viewTBIssuedMedicine");

            entity.Property(e => e.Cnic)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DistrictName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DivisionName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(150);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mrno)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MRNo");
            entity.Property(e => e.TehsilName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewTblabTest>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewTBLabTest");

            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Result)
                .HasMaxLength(2000)
                .IsUnicode(false);
            entity.Property(e => e.TestName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ViewTodayPatientOpenVisitCountByDeptBySec>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewTodayPatientOpenVisitCountByDeptBySec");

            entity.Property(e => e.DepartmentName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.SectionName)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
