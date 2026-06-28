using HMIS.EMC.Domain.Models.Dto;
using HMIS.EMC.Domain.Models.Dto.FilterDto;
using HMIS.EMC.Domain.Models.Dto.PaginationDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.EMC.Service.Interfaces
{
    public interface IBirthCertificate
    {
        Task<CreateOrEditBirthCertificateDto> CreateOrEdit(CreateOrEditBirthCertificateDto input);

        Task<ViewPagerDto<GetAllBirthCertificateDto>> GetAllBirthCertificateDto(SearchFilterDto? filter);

        Task<List<SingleBirthCertificateRecordDto>> GetSingleBirthCertificateRecord(Guid PatientVisitId);
    }
}
