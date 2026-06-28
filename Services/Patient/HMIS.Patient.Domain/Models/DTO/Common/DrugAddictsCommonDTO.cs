using System;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Domain.Models.DTO.Common
{
    #region DrugAddict Dashboard Counts
    public class DrugAddictsCommonDTO
    {
        public int TotalDrugAddicts { get; set; }
        public int KnownDrugAddicts { get; set; }
        public int UnKnownDrugAddicts { get; set; }
        public int VerifiedbyNadraDrugAddicts { get; set; }
        public int NotVerifiedbyNadraDrugAddicts { get; set; }
    }

    public class DrugAddictsDiseasesDTO
    {
        public int TBCount { get; set; }
        public int HepatitisACount { get; set; }
        public int HepatitisBCount { get; set; }
        public int HepatitisCCount { get; set; }
        public int HIVCount { get; set; }
        public int CoMorbidCount { get; set; }
        public int NoDiseaseCount { get; set; }

    }
    public class DrugAddictsSocialRefferedDTO
    {
        public int SocialWelfareCount { get; set; }
        public int AdmittedCount { get; set; }
        public int DischargedCount { get; set; }
    }
    public class PatientDetailDrugAddictDTO
    {
        public Guid PatientId { get; set; }

        public string? MRNo { get; set; }

        public string? FullName { get; set; }

        public string CNIC { get; set; } = null!;
        public int? HealthFacilityId { get; set; }

        public Guid? CreatedById { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }
        public string? MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        
    }
    public class PatientDetailDrugAddictDiseasesDTO
    {
        public Guid PatientId { get; set; }

        public string? MRNo { get; set; }

        public string? FullName { get; set; }

        public string CNIC { get; set; } = null!;
        public int? HealthFacilityId { get; set; }

        public Guid? CreatedById { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }
        public string? MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        public string? Diseases { get; set; }

    }

    public class PatientDetailDrugAddictSocialWelfareDTO
    {
        public Guid PatientId { get; set; }

        public string? MRNo { get; set; }

        public string? FullName { get; set; }

        public string CNIC { get; set; } = null!;
        public int? HealthFacilityId { get; set; }

        public Guid? CreatedById { get; set; }
        public string? Name { get; set; }
        public string? DivisionName { get; set; }
        public string? DistrictName { get; set; }
        public string? TehsilName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        
    }

    #endregion

    #region New DrugAddict disease Dashboards Pagination
    public class PatientDetailDrugAddictDiseaseDTO
    {
        public int? TotalRecord { get; set; }
        public List<PatientDetailDrugAddictDiseaseList> PatientList { get; set; }

    }
    public class PatientDetailDrugAddictDiseaseList
    {
        public Guid PatientId { get; set; }

        public string? MRNo { get; set; }

        public string? FullName { get; set; }

        public string CNIC { get; set; } = null!;
        public int? HealthFacilityId { get; set; }

        public Guid? CreatedById { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? CreatedBy { get; set; }
        public string? MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }
        public string? Diseases { get; set; }

    }
    #endregion
    #region New DrugAddict social welfare Dashboards Pagination
    public class PatientDetailDrugAddictsSocialWelfareDTO
    {
        public int? TotalRecord { get; set; }
        public List<PatientDetailDrugAddictSocialWelfareList> PatientList { get; set; }

    }
    public class PatientDetailDrugAddictSocialWelfareList
    {
        public Guid PatientId { get; set; }
        public string? SrNo { get; set; }

        public string? MRNo { get; set; }

        public string? FullName { get; set; }

        public string CNIC { get; set; } = null!;
        public int? HealthFacilityId { get; set; }

        public Guid? CreatedById { get; set; }
        public string? Name { get; set; }
        public string? DivisionName { get; set; }
        public string? DistrictName { get; set; }
        public string? TehsilName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? MobileNo { get; set; }
        public Guid PatientOpenVisitId { get; set; }

    }
    #endregion
}

