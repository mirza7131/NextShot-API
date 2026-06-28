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
    public interface IMLESvForm
    {
        Task<CreateOrEditMlcSvInitialInfoDto> CreateOrEdit(CreateOrEditMlcSvInitialInfoDto input);
        Task<CreateOrEditMlcSvExaminationDto> CreateOrEdit(CreateOrEditMlcSvExaminationDto input);
        Task<CreateOrEditMlcSvEvidenceCollectedDto> CreateOrEdit(CreateOrEditMlcSvEvidenceCollectedDto input);
        Task<CreateOrEditMlcSvReportDto> CreateOrEdit(CreateOrEditMlcSvReportDto input);

        Task<List<GetAllMlcSvSingleRecordDto>> GetSingleMlcSvRecordByPatientId(Guid PatientId);
        Task<ViewPagerDto<GetAllMLCSVRecord>> GetAllMlcSvRecord(SearchFilterDto? filter);

    }
}
