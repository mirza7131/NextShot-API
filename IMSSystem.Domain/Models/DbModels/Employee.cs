using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class Employee
{
    public int Id { get; set; }

    public int? GrantApplicationId { get; set; }

    public int? EmployeeCategoryId { get; set; }

    public string? EmployeeName { get; set; }

    public int? DesignationId { get; set; }

    public int? QualificationId { get; set; }

    public string? ImagePath { get; set; }

    public int? GovtService { get; set; }

    public int? JobType { get; set; }

    public string? Category { get; set; }

    public string? Designation { get; set; }

    public string? Qualification { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? JoiningDate { get; set; }

    public decimal? MonthlySalary { get; set; }

    public string? Courses { get; set; }

    public string? UserId { get; set; }

    public DateTime? ModifiedAt { get; set; }

    public DateTime? CreatedAt { get; set; }
}
