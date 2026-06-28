using System;
using System.Collections.Generic;

namespace HMIS.NCD.Domain.Models.OldDbModels;

public partial class TblLabortary
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? EnableFlag { get; set; }

    public string? CreatedBy { get; set; }

    public string? CreationDate { get; set; }

    public string? UpdtedBy { get; set; }

    public string? UpdationDate { get; set; }
}
