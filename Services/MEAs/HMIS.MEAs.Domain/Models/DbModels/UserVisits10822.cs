using System;
using System.Collections.Generic;

namespace HMIS.MEAs.Domain.Models.DbModels;

public partial class UserVisits10822
{
    public int VisitId { get; set; }

    public int? ApplicationTypeId { get; set; }

    public int? UserId { get; set; }

    public int? ZoneId { get; set; }

    public int? HfId { get; set; }

    public int? ShiftId { get; set; }

    public string? Month { get; set; }

    public string? Year { get; set; }

    public bool? IsVisited { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdateBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsRepeat { get; set; }

    public bool? IsSpecial { get; set; }

    public DateTime? VisitedDate { get; set; }
}
