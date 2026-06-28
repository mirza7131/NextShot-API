using System;
using System.Collections.Generic;

namespace HMIS.HealthCouncil.Domain.Models.DbModels;

public partial class PhraseLog
{
    public Guid PhraseLogId { get; set; }

    public Guid? UniqueId { get; set; }

    public string? TableName { get; set; }

    public string? Phrase { get; set; }

    public DateTime? CreatedOn { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public Guid? UpdatedBy { get; set; }
}
