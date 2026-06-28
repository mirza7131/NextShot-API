using System;
using System.Collections.Generic;

namespace HMIS.Patient.Domain.Models.DbModels_TB;

public partial class DistrictWiseMonthlyReport
{
    public long DistrictWiseMonthlyReportId { get; set; }

    public string DistrictCode { get; set; } = null!;

    public string HfmisCode { get; set; } = null!;

    public string ReportingMonth { get; set; } = null!;

    public int NoOfBmu { get; set; }

    public int NoOfStaffShortageBmu { get; set; }

    public int MonthlyOpdcount { get; set; }

    public int? NoOfIdentifiedPersumptiveCases { get; set; }

    public int NoOfPublicSectorIdenPcases { get; set; }

    public int? NoOfPrivateSectorIdenPcases { get; set; }

    public int TotalScreenedPersumptiveCases { get; set; }

    public int TotalMicroscopyForAfb { get; set; }

    public int TotalPositiveAfbseamer { get; set; }

    public int RegisteredPositiveBactPulmonaryCases { get; set; }

    public int TotalRegisteredClinicalDiagnosedCases { get; set; }

    public int TotalRegisteredExtraPulmonaryTbcases { get; set; }

    public int TotalRegisteredAllTypeTbcases { get; set; }

    public int TotalRegisteredGxpertTbcases { get; set; }

    public int TotalRegisteredChildhoodTbcases { get; set; }

    public int TotalDetectedRrcases { get; set; }

    public int TotalLostToFollowupPatients { get; set; }

    public int TotalHivscreenedTbpatients { get; set; }

    public int TotalHivreactiveCases { get; set; }

    public int NoOfHhcontactIdentified { get; set; }

    public int NoOfHhscreenedContacts { get; set; }

    public int NoOfHhcontactsFoundTb { get; set; }

    public int NoOfLhwspresumptiveIdentified { get; set; }

    public int NoOfLhwinterventionRegCases { get; set; }

    public string AvailableAttpediatricStock { get; set; } = null!;

    public string AvailableAttadultStock { get; set; } = null!;

    public int? PvtTotalTbcasesReg { get; set; }

    public int? PvtTotalPresumptiveCasesScreened { get; set; }

    public int? ChestCampConducted { get; set; }

    public int? AllTypeCasesIdentifiedInChestCamps { get; set; }

    public int? LhwcaseRegList { get; set; }

    public int? PvtTotalCaseRegLinelist { get; set; }

    public bool? IsEnableReport { get; set; }

    public DateTime? EnableReportDateFrom { get; set; }

    public int PreventiveTherapyStock { get; set; }

    public int NoOfCasesPutOnPreventiveTreatment { get; set; }

    public DateTime? EnableReportDateTo { get; set; }

    public Guid CreatedBy { get; set; }

    public DateTime CreationDate { get; set; }

    public Guid? UpdatedBy { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public Guid? DeletedBy { get; set; }

    public DateTime? DeletedDate { get; set; }

    public bool? RecordStatus { get; set; }

    public Guid Guid { get; set; }
}
