using System;
using System.Collections.Generic;

namespace AuthDAL.Models.DbModelsReporting;

public partial class SectionProcedure
{
    public int SectionProcedureId { get; set; }

    public int? SectionLookupId { get; set; }

    public string? ProcedureTitle { get; set; }

    public string? Description { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public DateTime? DeletedOn { get; set; }

    public Guid? DeletedBy { get; set; }

    public byte ActionTypeId { get; set; }

    public byte? IsActive { get; set; }
}
