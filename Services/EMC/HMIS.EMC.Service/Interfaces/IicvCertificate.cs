using HMIS.EMC.Domain.Models.DbModels;
using HMIS.EMC.Domain.Models.Dto;
using HMIS.EMC.Domain.Models.Dto.FilterDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMIS.EMC.Domain.Models.Dto.PaginationDto;

namespace HMIS.EMC.Service.Interfaces
{
    public interface IicvCertificate
    {
        Task<CreateOrEditIcvCertificateDto> CreateOrEdit(CreateOrEditIcvCertificateDto input);
        Task<ViewPagerDto<IcvPatientDto>> GetIcvPatientsList(SearchFilterDto? filter);
        Task<IcvCertificate> GetSingleIcvPatientByPatientId(Guid PatientId);
    }
}
