using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class PatientContactsStatistic
{
    public int Id { get; set; }

    public int? TotalHouseHoldContacts { get; set; }

    public int? TotalContactsUnderFive { get; set; }

    public int? TotalWorkPlaceContacts { get; set; }

    public int? PatientId { get; set; }

    public virtual Patient? Patient { get; set; }
}
