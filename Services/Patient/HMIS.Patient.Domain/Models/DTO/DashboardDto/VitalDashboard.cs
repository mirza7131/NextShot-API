using HMIS.Patient.Domain.Models.DTO.DashboardDto.PatientRegistrationDashboard;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.DashboardDto.VitalDashboard
{
    #region Dashboard Vital login User Counts New Counts
    public class VitalDashboardLoginUserAllCountsDTO
    {
        public int? VitalCollected { get; set; }
    }
    #endregion
    #region Dashboard Vital New Counts
    public class VitalDashboardAllCountsDTO
    {
        public int? TokenIssued { get; set; }
        public int? Registrations { get; set; }
        public int? VitalCollected { get; set; }
        public int? RefferForvitals { get; set; }
        public int? SelfPatients { get; set; }
        public int? OtherThanSelfPatients { get; set; }
        public int? SelfMalePatients { get; set; }
        public int? SelfFemalePatients { get; set; }
        public int? SelfOthersPatients { get; set; }
    }
    #endregion
    #region Patient Vital Visit Count By User
    public class PatientVisitVitalCountByUser
    {
        public Guid? extra { get; set; }
        public string? name { get; set; }
        public int? value { get; set; }
    }

    #endregion

    #region Patient Vital Visit Count By Province
    public class PatientVisitVitalCountByProvince
    {
        public string? name { get; set; }
        public int? value { get; set; }

        public PatientVisitVitalCountByProvince()
        {
            extra = new PatientDashboardExtra();
        }
        public PatientDashboardExtra extra { get; set; }
    }

    #endregion

    #region Patient Vital Visit Count By Gender
    public class PatientVisitVitalCountByGender
    {
        public string? name { get; set; }
        public int? value { get; set; }
        public string? extra { get; set; }
    }
    #endregion

    #region Patient Visit Count By Weight Range
    public class PatientVisitCountByWeightRange
    {
        public int? Range0to20 { get; set; }
        public int? Range21to40 { get; set; }
        public int? Range41to60 { get; set; }
        public int? Range61to80 { get; set; }
        public int? Range81to100 { get; set; }
        public int? Range101to120 { get; set; }
        public int? Range121to140 { get; set; }
        public int? Range141to160 { get; set; }
        public int? Range161to180 { get; set; }
        public int? Range181to200 { get; set; }
    }
    #endregion

    #region Patient Vital Visit Count By Respitary Rate Range
    public class PatientVisitVitalCountByRespiratoryRateRange
    {
        public string? name { get; set; }
        public int? value { get; set; }

    }
    #endregion

    #region Patient Vital Visit Count By Temperature Range
    public class PatientVisitVitalCountByTemperatureRange
    {
        public string? name { get; set; }
        public int? value { get; set; }

    }
    #endregion

    #region Patient Vital Visit Count By Pulse Rate Range
    public class PatientVisitVitalCountByPulseRateRange
    {
        public string? name { get; set; }
        public int? value { get; set; }

    }
    #endregion

    #region Patient Vital Visit Count By Pulse Rate Range
    public class PatientVisitVitalCountByBloodPressureRange
    {
        public string? name { get; set; }
        public int? value { get; set; }

    }
    #endregion

}
