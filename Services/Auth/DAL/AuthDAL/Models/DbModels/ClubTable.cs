using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthDAL.Models.DbModels;

public partial class ClubTable
{
    public int ClubTableId { get; set; }

    public int TableNo { get; set; }

    public string TableName { get; set; } = null!;

    public string TableType { get; set; } = null!;

    public decimal HourlyRate { get; set; }

    public decimal GameRate { get; set; }

    public decimal DoubleHourlyRate { get; set; }
    public decimal DoubleGameRate { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedOn { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }

    public string? DeletedBy { get; set; }

    public DateTime? DeletedOn { get; set; }
}