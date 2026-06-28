using System;
using System.Collections.Generic;

namespace HMIS.HCP.Domain.Models.OldDbModels;

public partial class TblPatientDataCsv
{
    public long Id { get; set; }

    public int? PatientId { get; set; }

    public string? RegNo { get; set; }

    public string? OldRegNo { get; set; }

    public string? MrnNo { get; set; }

    public string? CallCenterId { get; set; }

    public string? PatientName { get; set; }

    public string? Lname { get; set; }

    public string? FatherName { get; set; }

    public string? PatientDob { get; set; }

    public string? PatientAge { get; set; }

    public string? PatientType { get; set; }

    public string? SelfCnic { get; set; }

    public string? NextOfKin { get; set; }

    public string? NextOfKinCnic { get; set; }

    public string? ContactNoSelf { get; set; }

    public string? OtherContactno { get; set; }

    public string? Gender { get; set; }

    public string? AddressAvailable { get; set; }

    public string? PostalAddress { get; set; }

    public string? DivisionName { get; set; }

    public string? DistrictName { get; set; }

    public string? TehsilName { get; set; }

    public string? HfName { get; set; }

    public string? IsRegister { get; set; }

    public string? IsVital { get; set; }

    public string? IsAssesment { get; set; }

    public string? IsTreatment { get; set; }

    public string? IsVacinate { get; set; }

    public string? IsSample { get; set; }

    public string? IsRefered { get; set; }

    public string? IsClosed { get; set; }

    public string? IsConseledNClosed { get; set; }

    public string? Labno { get; set; }

    public string? Pcrreq { get; set; }

    public string? ScreeningRecommended { get; set; }

    public string? HcvPcr { get; set; }

    public string? HbvPcr { get; set; }

    public string? BaselineDate { get; set; }

    public string? IsScreening { get; set; }

    public string? IsHbvTest { get; set; }

    public string? IsHcvTest { get; set; }

    public string? Pcr { get; set; }

    public string? PcrOption { get; set; }

    public string? CreateTime { get; set; }

    public string? CreatedBy { get; set; }

    public string? Stage { get; set; }

    public string? IsHealthWeekPatient { get; set; }

    public string? NoOfMedicineDelivered { get; set; }

    public string? NoOfHbvMedicineDelivered { get; set; }

    public string? NoOfHcvMedicineDelivered { get; set; }

    public string? VitalsCreateTime { get; set; }

    public string? AssessmentCreateTime { get; set; }

    public string? SampleNumber { get; set; }

    public string? VaccinationDoseDate1 { get; set; }

    public string? VaccinationDoseDate2 { get; set; }

    public string? VaccinationDoseDate3 { get; set; }

    public string? NumberOfDoseAdministered { get; set; }

    public string? MedicineDeliveryDate1 { get; set; }

    public string? MedicineDeliveryDate2 { get; set; }

    public string? MedicineDeliveryDate3 { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? HcvFristMedicineDate { get; set; }

    public string? HbvFirstMedicineDate { get; set; }

    public string? HcvMedicineDuration { get; set; }

    public string? HbvMedicineDuration { get; set; }

    public string? MedType { get; set; }

    public string? PatientCategory { get; set; }

    public string? HbMedName { get; set; }

    public string? HbMedDuration { get; set; }

    public string? ChangeType { get; set; }

    public string? SvrSampleType { get; set; }
}
