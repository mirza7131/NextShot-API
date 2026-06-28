using HMIS.EMC.Domain.Models.Dto;
using HMIS.EMC.Domain.Models.Dto.FilterDto;
using HMIS.EMC.Domain.Models.Dto.PaginationDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Service.Interfaces
{
    public interface IDeathCertificate
    {
        Task<CreateOrEditDeathCertificateDto> CreateOrEdit(CreateOrEditDeathCertificateDto input);
        Task<ViewPagerDto<GetDeathCertificateRecordDto>> GetDeathCertificatePatientsList(SearchFilterDto? filter);
        Task<List<GetDeathCertificateRecordDto>> GetSingleDeathCertificatePatient(Guid PatientVisitId);
    }
}
