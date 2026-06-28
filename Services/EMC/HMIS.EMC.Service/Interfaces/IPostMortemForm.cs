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
    public interface IPostMortemForm
    {
        Task<CreateOrEditPostMortemGeneralFormDto> CreateOrEdit(CreateOrEditPostMortemGeneralFormDto input);
        Task<CreateOrEditPostMortemExternalFormDto> CreateOrEdit(CreateOrEditPostMortemExternalFormDto input);
        Task<CreateOrEditPostMortemInternalFormDto> CreateOrEdit(CreateOrEditPostMortemInternalFormDto input);
        Task<CreateOrEditPostMortemReportDto> CreateOrEdit(CreateOrEditPostMortemReportDto input);
        Task<ViewPagerDto<GetAllPostMortemPatientsListDto>> GetAllPostMortemPatients(SearchFilterDto? filter);
        Task<List<PostMortemRecordDto>> GetSinglePostMortemFormByPatientId(Guid PatientId);
    }
}
