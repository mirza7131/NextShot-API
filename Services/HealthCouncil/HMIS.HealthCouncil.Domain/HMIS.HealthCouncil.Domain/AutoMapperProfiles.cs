using HMIS.HealthCouncil.Domain.Models.DbModels;
using HMIS.HealthCouncil.Domain.Models.Dto.BankDetails;
using HMIS.HealthCouncil.Domain.Models.Dto.Budget;
using HMIS.HealthCouncil.Domain.Models.Dto.HealthCouncil;
using HMIS.HealthCouncil.Domain.Models.DTO.Budget;
using autoMapper = AutoMapper;

namespace HMIS.Patient.Domain
{
    public class AutoMapperProfiles
    {
        #region Budget

        public class BudgetProfile : autoMapper.Profile
        {
            public BudgetProfile()
            {
                CreateMap<Budget, CreateOrEditBudgetDto>().ReverseMap();
                CreateMap<Budget, ReleasebudgetDto>().ReverseMap();
                CreateMap<BankStatement, BankStatementDto>().ReverseMap();
                CreateMap<ContignetStaff, ContigmentStaffDto>().ReverseMap();
                CreateMap<CommitteeFormulation, CommitteeFormulationDto>().ReverseMap();
                CreateMap<MeetingCall, MeetingCallDto>().ReverseMap();
                CreateMap<MeetingDetail, MeetingDetailDto>().ReverseMap();
                CreateMap<MeetingExpendeture, MeetingExpendetureDto>().ReverseMap();
                CreateMap<Expenditures, MeetingDisscussedCategory>().ReverseMap();
                CreateMap<Vendor, VendorDto>().ReverseMap();
                CreateMap<Expense, ExpenseDto>().ReverseMap();
                CreateMap<HealthFacilityBankDetail, BankDetailsDto>().ReverseMap();
            }
        }

        #endregion

    }
}
