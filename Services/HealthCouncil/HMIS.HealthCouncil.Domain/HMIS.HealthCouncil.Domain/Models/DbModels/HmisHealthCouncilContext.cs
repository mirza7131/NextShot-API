using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HMIS.HealthCouncil.Domain.Models.DbModels;

public partial class HmisHealthCouncilContext : DbContext
{
    public HmisHealthCouncilContext()
    {
    }

    public HmisHealthCouncilContext(DbContextOptions<HmisHealthCouncilContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AccountHead> AccountHeads { get; set; }

    public virtual DbSet<BankDetail> BankDetails { get; set; }

    public virtual DbSet<BankStatement> BankStatements { get; set; }

    public virtual DbSet<Budget> Budgets { get; set; }

    public virtual DbSet<CommitteeFormulation> CommitteeFormulations { get; set; }

    public virtual DbSet<ContignetStaff> ContignetStaffs { get; set; }

    public virtual DbSet<Expense> Expenses { get; set; }

    public virtual DbSet<HealthFacilityBankDetail> HealthFacilityBankDetails { get; set; }

    public virtual DbSet<MeetingCall> MeetingCalls { get; set; }

    public virtual DbSet<MeetingDetail> MeetingDetails { get; set; }

    public virtual DbSet<MeetingDisscussedCategory> MeetingDisscussedCategories { get; set; }

    public virtual DbSet<MeetingExpendeture> MeetingExpendetures { get; set; }

    public virtual DbSet<PhraseLog> PhraseLogs { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    public virtual DbSet<ViewAllocateBudget> ViewAllocateBudgets { get; set; }

    public virtual DbSet<ViewChequeIssue> ViewChequeIssues { get; set; }

    public virtual DbSet<ViewReleaseBudget> ViewReleaseBudgets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=172.16.15.5;Database=HMIS-HealthCouncil;Persist Security Info=False;User Id=khurshid;Password=asd@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AccountHead>(entity =>
        {
            entity.Property(e => e.AccountHeadId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<BankDetail>(entity =>
        {
            entity.Property(e => e.BankDetailId).ValueGeneratedNever();
            entity.Property(e => e.BankAccountNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankAccountTitle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankBranchCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankBranchName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankContactNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BankName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<BankStatement>(entity =>
        {
            entity.ToTable("BankStatement");

            entity.Property(e => e.BankStatementId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Discription)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.File).IsUnicode(false);
            entity.Property(e => e.Month)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Budget>(entity =>
        {
            entity.ToTable("Budget");

            entity.Property(e => e.BudgetId).ValueGeneratedNever();
            entity.Property(e => e.AllocatedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChequeDate).HasColumnType("datetime");
            entity.Property(e => e.ChequeImage).IsUnicode(false);
            entity.Property(e => e.ChequeIssueDate).HasColumnType("datetime");
            entity.Property(e => e.ChequeReceivedDate).HasColumnType("datetime");
            entity.Property(e => e.CourierCompany)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CourierDispatchDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.DiaryNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReleaseAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<CommitteeFormulation>(entity =>
        {
            entity.ToTable("CommitteeFormulation");

            entity.Property(e => e.CommitteeFormulationId).ValueGeneratedNever();
            entity.Property(e => e.Cnic)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Designation)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(200);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ContignetStaff>(entity =>
        {
            entity.ToTable("ContignetStaff");

            entity.Property(e => e.ContignetStaffId).ValueGeneratedNever();
            entity.Property(e => e.BankAccountNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankAccountTitle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankBranchCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankBranchName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.BankName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Cnic)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CNIC");
            entity.Property(e => e.ContractExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.ContractStartDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.FatherName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Expense>(entity =>
        {
            entity.ToTable("Expense");

            entity.Property(e => e.ExpenseId).ValueGeneratedNever();
            entity.Property(e => e.ChequeDate).HasColumnType("datetime");
            entity.Property(e => e.ChequeNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.ExpenseFormJson).IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.MeetingDetail).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.MeetingDetailId)
                .HasConstraintName("FK_Expense_Expense");

            entity.HasOne(d => d.MeetingDisscussedCategory).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.MeetingDisscussedCategoryId)
                .HasConstraintName("FK_Expense_MeetingDisscussedCategories");

            entity.HasOne(d => d.Vendor).WithMany(p => p.Expenses)
                .HasForeignKey(d => d.VendorId)
                .HasConstraintName("FK_Expense_Vendor");
        });

        modelBuilder.Entity<HealthFacilityBankDetail>(entity =>
        {
            entity.ToTable("HealthFacilityBankDetail");

            entity.Property(e => e.HealthFacilityBankDetailId).ValueGeneratedNever();
            entity.Property(e => e.AccountNo).HasMaxLength(50);
            entity.Property(e => e.AccountTitle).HasMaxLength(200);
            entity.Property(e => e.Bank)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.BankContact).HasMaxLength(50);
            entity.Property(e => e.BranchCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BranchName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.CurrentBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.OpeningBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<MeetingCall>(entity =>
        {
            entity.ToTable("MeetingCall");

            entity.Property(e => e.MeetingCallId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.IsMeetingDone).HasDefaultValueSql("((0))");
            entity.Property(e => e.MeetingAgenda)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.MeetingDate).HasColumnType("datetime");
            entity.Property(e => e.MeetingMembers).IsUnicode(false);
            entity.Property(e => e.NotificationDate).HasColumnType("datetime");
            entity.Property(e => e.NotificationNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<MeetingDetail>(entity =>
        {
            entity.Property(e => e.MeetingDetailId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.MeetingAgenda)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.MeetingDate).HasColumnType("datetime");
            entity.Property(e => e.MeetingDecision)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.MeetingDetails).IsUnicode(false);
            entity.Property(e => e.MeetingMembers).IsUnicode(false);
            entity.Property(e => e.MeetingNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PreviousMeetingRemarks)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<MeetingDisscussedCategory>(entity =>
        {
            entity.Property(e => e.MeetingDisscussedCategoryId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.AccountHead).WithMany(p => p.MeetingDisscussedCategories)
                .HasForeignKey(d => d.AccountHeadId)
                .HasConstraintName("FK_MeetingDisscussedCategories_AccountHeads");
        });

        modelBuilder.Entity<MeetingExpendeture>(entity =>
        {
            entity.Property(e => e.MeetingExpendetureId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.EstimatedCost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ItemName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PricePerUnit).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

            entity.HasOne(d => d.AccountHead).WithMany(p => p.MeetingExpendetures)
                .HasForeignKey(d => d.AccountHeadId)
                .HasConstraintName("FK_MeetingExpendetures_AccountHeads");
        });

        modelBuilder.Entity<PhraseLog>(entity =>
        {
            entity.ToTable("PhraseLog");

            entity.Property(e => e.PhraseLogId).ValueGeneratedNever();
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.TableName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.ToTable("Vendor");

            entity.Property(e => e.VendorId).ValueGeneratedNever();
            entity.Property(e => e.Address1)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Address2)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.BankAccountNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankAccountTitle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankBranchCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankBranchName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BankName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ContactNo)
                .HasMaxLength(13)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DeletedOn).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirmName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Ntn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("NTN");
            entity.Property(e => e.SalesTaxNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
        });

        modelBuilder.Entity<ViewAllocateBudget>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewAllocateBudget");

            entity.Property(e => e.AccountNo).HasMaxLength(50);
            entity.Property(e => e.AccountTitle).HasMaxLength(200);
            entity.Property(e => e.AllocatedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Bank)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.BankContact).HasMaxLength(50);
            entity.Property(e => e.BranchCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BranchName)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.CurrentBalance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.OpeningBalance).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<ViewChequeIssue>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewChequeIssue");

            entity.Property(e => e.ChequeIssueDate).HasColumnType("datetime");
            entity.Property(e => e.ChequeReceivedDate).HasColumnType("datetime");
            entity.Property(e => e.CourierCompany)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CourierDispatchDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.DiaryNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReleaseAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<ViewReleaseBudget>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ViewReleaseBudget");

            entity.Property(e => e.AllocatedAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ChequeImage).IsUnicode(false);
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.ReleaseAmount).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
