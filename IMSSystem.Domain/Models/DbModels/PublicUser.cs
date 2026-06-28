using System;
using System.Collections.Generic;

namespace IMSSystem.Domain.Models.DbModels;

public partial class PublicUser
{
    public Guid PublicUserId { get; set; }

    public string? Name { get; set; }

    public string? Cnic { get; set; }

    public string? MobileNo { get; set; }

    public DateTime? Dob { get; set; }

    public string? GenderProfileId { get; set; }

    public string? ParmanentAddress { get; set; }

    public string? TemporaryAddress { get; set; }

    public bool IsActive { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }

    public byte ActionTypeId { get; set; }
}
