using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HMIS.Patient.Domain.Models.HMIS_HUBModels;

public partial class HmisHubContext : DbContext
{
    public HmisHubContext()
    {
    }

    public HmisHubContext(DbContextOptions<HmisHubContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Person> People { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=172.16.15.5;Database=HMIS-Auth;Persist Security Info=False;User Id=usman;Password=asd@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("_Person");

            entity.HasIndex(e => e.MobileNumber, "Mobile_dbo.person");

            entity.HasIndex(e => e.Cnic, "NCI_CNIC");

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
