using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class Course
{
    public int Id { get; set; }

    public int? GrantApplicationId { get; set; }

    public string? CourseName { get; set; }

    public string? DegreeOffered { get; set; }

    public int? RequiredQualificationId { get; set; }

    public string? RequiredQualification { get; set; }

    public int? AnnuallyInductedStudents { get; set; }

    public DateTime? AnnualCalendarDate { get; set; }

    public bool? Ibccaffliated { get; set; }

    public bool? Hecrecognized { get; set; }

    public string? Ibcccertificate { get; set; }

    public string? Heccertificate { get; set; }

    public DateTime? CourseStartDate { get; set; }

    public bool? CourseDisContinued { get; set; }

    public string? DisContinuedReason { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? UserId { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public bool? IsOld { get; set; }

    public int? CurrentlyInductedStudents { get; set; }
}
