using System;
using System.Collections.Generic;

namespace HMIS.DrugAddict.Domain.Models.DbModels;

public partial class Otpcode
{
    public Guid OtpcodeId { get; set; }

    public int? Otp { get; set; }

    public Guid? UserId { get; set; }

    public DateTime? Datetime { get; set; }
}
