using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.AuditDto;
using AuthDAL.Models.Dto.DistrictDto;
using AuthDAL.Models.Dto.DivisionDto;
using AuthDAL.Models.Dto.ErrorLogDto;
using AuthDAL.Models.Dto.HealthFacilityCategoryDto;
using AuthDAL.Models.Dto.HealthFacilityDto;
using AuthDAL.Models.Dto.HealthFacilityStationDto;
using AuthDAL.Models.Dto.HealthFacilityTypeDto;
using AuthDAL.Models.Dto.LabTestDto;
using AuthDAL.Models.Dto.HfDepartmentDto;
using AuthDAL.Models.Dto.HfDepartmentSectionDto;
using AuthDAL.Models.Dto.MenuDto;
using AuthDAL.Models.Dto.ProfileDto;
using AuthDAL.Models.Dto.ProfileTypeDto;
using AuthDAL.Models.Dto.ProvinceDto;
using AuthDAL.Models.Dto.RoleDto;
using AuthDAL.Models.Dto.TehsilDto;
using AuthDAL.Models.Dto.UnionCouncilDto;
using AuthDAL.Models.Dto.UserDto;
using AuthDAL.Models.Dto.UserLogDto;
using AuthDAL.Models.Dto.UserRole;
using autoMapper = AutoMapper;
using AuthDAL.Models.Dto.LabTestDetailDto;
using AuthDAL.Models.Dto.DepartmentLookupDto;
using AuthDAL.Models.Dto.SectionLookupDto;
using CommonDTOs.LocationDTO;
using CommonDTOs.DropdownDTO;
using AuthDAL.Models.Dto.FeatureDto;
using AuthDAL.Models.Dto.AttachmentDto;
using AuthDAL.Models.Dto.MimsMedicineData;
using AuthDAL.Models.Dto.DataSyncUtilityLog;
using AuthDAL.Models.Dto.DataSyncToOfflineDto;
using AuthDAL.Models.Dto.OfflineVersionLogDto;
using AuthDAL.Models.Dto.MimsMedicineIndentLog;
using AuthDAL.Models.Dto.MimsMedicineIndentDetail;
using AuthDAL.Models.Dto.HfLabTestConfigDto;
using AuthDAL.Models.Dto.EventDto;
using AuthDAL.Models.Dto.EventHealthFacilityDto;
using AuthDAL.Models.Dto.InvoiceDto;

namespace AuthDAL
{
    public class AutoMapperProfiles
    {
        #region UMS
        public class UserProfile : autoMapper.Profile
        {
            public UserProfile()
            {
                CreateMap<User, CreateOrEditUserDto>().ReverseMap();
                CreateMap<User, ViewUserDto>().ReverseMap();
                CreateMap<CreateOrEditHrUserDto, CreateOrEditUserDto>().ReverseMap();
            }
        }

        public class UserRoleProfile : autoMapper.Profile
        {
            public UserRoleProfile()
            {
                CreateMap<UserRole, CreateOrEditUserRoleDto>().ReverseMap();
                CreateMap<UserRole, ViewUserRoleDto>().ReverseMap();
            }
        }

        public class ProfileTypeProfile : autoMapper.Profile
        {
            public ProfileTypeProfile()
            {
                CreateMap<ProfileType, CreateOrEditProfileTypeDto>().ReverseMap();
                CreateMap<ProfileType, ViewProfileTypeDto>().ReverseMap();
                CreateMap<ProfileType, CacheProfileTypeDto>().ReverseMap();
            }
        }

        public class ProfileProfile : autoMapper.Profile
        {
            public ProfileProfile()
            {
                CreateMap<Profile, CreateOrEditProfileDto>().ReverseMap();
                CreateMap<Profile, ViewProfileDto>().ReverseMap();
                CreateMap<Profile, CacheProfileDto>().ReverseMap();
            }
        }

        public class MenuProfile : autoMapper.Profile
        {
            public MenuProfile()
            {
                CreateMap<Menu, CreateOrEditMenuDto>().ReverseMap();
                CreateMap<Menu, ViewMenuDto>().ReverseMap();
                CreateMap<Menu, ViewModulesDto>().ReverseMap();
                CreateMap<Menu, ViewModuleAccessDto>().ReverseMap();
            }
        }

        public class RoleProfile : autoMapper.Profile
        {
            public RoleProfile()
            {
                CreateMap<Role, CreateOrEditRoleDto>().ReverseMap();
                CreateMap<Role, ViewRoleDto>().ReverseMap();
            }
        }

        public class RoleMenuProfile : autoMapper.Profile
        {
            public RoleMenuProfile()
            {
                CreateMap<RoleMenu, CreateOrEditRoleMenuDto>().ReverseMap();
                CreateMap<RoleMenu, ViewRoleMenuDto>().ReverseMap();
            }
        }

        public class DepartmentLookupProfile : autoMapper.Profile
        {
            public DepartmentLookupProfile()
            {
                CreateMap<DepartmentLookup, CreateOrEditDepartmentLookupDto>().ReverseMap();
                CreateMap<DepartmentLookup, ViewDepartmentLookupDto>().ReverseMap();
                CreateMap<DepartmentLookup, CacheDepartmentLookupDto>().ReverseMap();
            }
        }

        public class SectionLookupProfile : autoMapper.Profile
        {
            public SectionLookupProfile()
            {
                CreateMap<SectionLookup, CreateOrEditSectionLookupDto>().ReverseMap();
                CreateMap<SectionLookup, ViewSectionLookupDto>().ReverseMap();
                CreateMap<SectionLookup, CacheSectionLookupDto>().ReverseMap();
            }
        }

        public class FeatureProfile : autoMapper.Profile
        {
            public FeatureProfile()
            {
                CreateMap<Feature, CreateOrEditFeatureDto>().ReverseMap();
                CreateMap<Feature, ViewFeatureDto>().ReverseMap();
                CreateMap<List<CreateOrEditAttachmentDto>, Attachment>();
            }
        }
        
        public class AttachmentProfile : autoMapper.Profile
        {
            public AttachmentProfile()
            {
                CreateMap<Attachment, CreateOrEditAttachmentDto>().ReverseMap();
                CreateMap<Attachment, ViewAttachmentDto>().ReverseMap();

            }
        }
        
        public class OfflineVersionLogProfile: autoMapper.Profile
        {
            public OfflineVersionLogProfile()
            {
                CreateMap<OfflineVersionLog, CreateOrEditOfflineVersionLogDto>().ReverseMap();
                CreateMap<OfflineVersionLog, ViewOfflineVersionLogDto>().ReverseMap();
            }
        }

        public class MimsMedicineDatumProfile : autoMapper.Profile
        {
            public MimsMedicineDatumProfile()
            {
                CreateMap<MimsMedicineDatum, CreateOrEditMimsMedicineDataDto>().ReverseMap();
                CreateMap<MimsMedicineDatum, ViewMimsMedicineDataDto>().ReverseMap();
            }
        }

        public class MimsGetMedicineResponseProfile : autoMapper.Profile
        {
            public MimsGetMedicineResponseProfile()
            {
                CreateMap<MimsGetMedicineResponse, CreateOrEditMimsGetMedicineResponseDto>().ReverseMap();
            }
        }

        public class MimsMedicineIndentLogProfile : autoMapper.Profile
        {
            public MimsMedicineIndentLogProfile()
            {
                CreateMap<MimsMedicineIndentLog, CreateOrEditMimsMedicineIndentLogDto>().ReverseMap();
            }
        }
        public class MimsMedicineIndentDetailProfile : autoMapper.Profile
        {
            public MimsMedicineIndentDetailProfile()
            {
                CreateMap<MimsMedicineIndentDetail, CreateOrEditMimsMedicineIndentDetailDto>().ReverseMap();
            }
        }

        public class HfLabTestConfigProfile : autoMapper.Profile
        {
            public HfLabTestConfigProfile()
            {
                CreateMap<HfLabTestConfig, CreateOrEditHfLabTestConfigDto>().ReverseMap();
                CreateMap<HfLabTestConfig, ViewHfLabTestConfigDto>().ReverseMap();
                CreateMap<SPHfLabTestConfigList, ViewHfLabTestConfigDto>().ReverseMap();
            }
        }


        #endregion

        #region Sync utility

        public class DataSyncUtilityLogProfile : autoMapper.Profile
        {
            public DataSyncUtilityLogProfile()
            {
                CreateMap<DataSyncUtilityLog, CreateOrEditDataSyncUtilityLogDto>().ReverseMap();
                CreateMap<DataSyncUtilityLog, ViewDataSyncUtilityLogDto>().ReverseMap();
            }
        }

        public class DataSyncToOfflineProfile : autoMapper.Profile
        {
            public DataSyncToOfflineProfile()
            {
                CreateMap<DataSyncToOffline, CreateOrEditDataSyncToOfflineDto>().ReverseMap();
                CreateMap<DataSyncToOffline, ViewDataSyncToOfflineDto>().ReverseMap();
            }
        }


        #endregion

        #region HealthFacility Location 

        public class ProvinceProfile : autoMapper.Profile
        {
            public ProvinceProfile()
            {
                CreateMap<Province, CreateOrEditProvinceDto>().ReverseMap();
                CreateMap<Province, ViewProvinceDto>().ReverseMap();
                CreateMap<Province, CacheProvinceDto>().ReverseMap();
            }
        }

        public class DivisionProfile : autoMapper.Profile
        {
            public DivisionProfile()
            {
                CreateMap<Division, CreateOrEditDivisionDto>().ReverseMap();
                CreateMap<Division, ViewDivisionDto>().ReverseMap();
                CreateMap<Division, CacheDivisionDto>().ReverseMap();
            }
        }
        public class DistrictProfile : autoMapper.Profile
        {
            public DistrictProfile()
            {
                CreateMap<District, CreateOrEditDistrictDto>().ReverseMap();
                CreateMap<District, ViewDistrictDto>().ReverseMap();
                CreateMap<District, CacheDistrictDto>().ReverseMap();
            }
        }
        public class TehsilProfile : autoMapper.Profile
        {
            public TehsilProfile()
            {
                CreateMap<Tehsil, CreateOrEditTehsilDto>().ReverseMap();
                CreateMap<Tehsil, ViewTehsilDto>().ReverseMap();
                CreateMap<Tehsil, CacheTehsilDto>().ReverseMap();
            }
        }

        public class UnionCouncilProfile : autoMapper.Profile
        {
            public UnionCouncilProfile()
            {
                CreateMap<UnionCouncil, CreateOrEditUnionCouncilDto>().ReverseMap();
                CreateMap<UnionCouncil, ViewUnionCouncilDto>().ReverseMap();
                CreateMap<UnionCouncil, CacheUnionCouncilDto>().ReverseMap();
            }
        }

        public class HealthFacilityCategoryProfile : autoMapper.Profile
        {
            public HealthFacilityCategoryProfile()
            {
                CreateMap<HealthFacilityCategory, CreateOrEditHealthFacilityCategoryDto>().ReverseMap();
                CreateMap<HealthFacilityCategory, ViewHealthFacilityCategoryDto>().ReverseMap();
            }
        }

        public class HealthFacilityTypeProfile : autoMapper.Profile
        {
            public HealthFacilityTypeProfile()
            {
                CreateMap<HealthFacilityType, CreateOrEditHealthFacilityTypeDto>().ReverseMap();
                CreateMap<HealthFacilityType, ViewHealthFacilityTypeDto>().ReverseMap();
            }
        }
        public class HealthFacilityProfile : autoMapper.Profile
        {
            public HealthFacilityProfile()
            {
                CreateMap<HealthFacility, CreateOrEditHealthFacilityDto>().ReverseMap();
                CreateMap<HealthFacility, ViewHealthFacilityDto>().ReverseMap();
                CreateMap<HealthFacility, CacheHealthFacilityDto>().ReverseMap();
            }
        }

        public class HfDepartmentProfile : autoMapper.Profile
        {
            public HfDepartmentProfile()
            {
                CreateMap<HfDepartment, CreateOrEditHfDepartmentDto>().ReverseMap();
                CreateMap<HfDepartment, ViewHfDepartmentDto>().ReverseMap();
                CreateMap<HfDepartment, CacheHfDepartmentDto>().ReverseMap();
            }
        }

        public class HfDepartmentSectionProfile : autoMapper.Profile
        {
            public HfDepartmentSectionProfile()
            {
                CreateMap<HfDepartmentSection, CreateOrEditHfDepartmentSectionDto>().ReverseMap();
                CreateMap<HfDepartmentSection, ViewHfDepartmentSectionDto>().ReverseMap();
                CreateMap<HfDepartmentSection, CacheHfDepartmentSectionDto>().ReverseMap();
            }
        }

        public class HealthFacilityStationProfile : autoMapper.Profile
        {
            public HealthFacilityStationProfile()
            {
                CreateMap<HealthFacilityStation, CreateOrEditHealthFacilityStationDto>().ReverseMap();
                CreateMap<HealthFacilityStation, ViewHealthFacilityStationDto>().ReverseMap();
                CreateMap<HealthFacilityStation, ViewHealthFacilityStationWithDetailsDto>().ReverseMap();
            }
        }






        #endregion

        #region Audit Log

        public class AuditLogProfile : autoMapper.Profile
        {
            public AuditLogProfile()
            {
                CreateMap<AuditLog, CreateOrEditAuditLogDto>().ReverseMap();
                CreateMap<AuditLog, ViewAuditLogDto>().ReverseMap();
            }
        }

        public class ErrorLogProfile : autoMapper.Profile
        {
            public ErrorLogProfile()
            {
                CreateMap<ErrorLog, CreateOrEditErrorLogDto>().ReverseMap();
                CreateMap<ErrorLog, ViewErrorLogDto>().ReverseMap();
            }
        }

        public class UserLogProfile : autoMapper.Profile
        {
            public UserLogProfile()
            {
                CreateMap<UserLog, CreateOrEditUserLogDto>().ReverseMap();
                CreateMap<UserLog, ViewUserLogDto>().ReverseMap();
            }
        }

        #endregion

        #region DB View

        public class ViewRoleMenuAccessProfile : autoMapper.Profile
        {
            public ViewRoleMenuAccessProfile()
            {
                CreateMap<ViewGetCreateRoleMenuAccess, ViewGetEditRoleMenuAccess>().ReverseMap();
                CreateMap<ViewModulesDto, ViewGetAllRoleMenuAccess>().ReverseMap();
            }
        }

        #endregion

        #region Pathology
        public class LabTestProfile : autoMapper.Profile
        {
            public LabTestProfile()
            {
                CreateMap<LabTest, CreateOrEditLabTestDto>().ReverseMap();
                CreateMap<LabTest, ViewLabTestDto>().ReverseMap();
                CreateMap<LabTest, CacheLabTestDto>().ReverseMap();
            }
        }

        public class UcProfile : autoMapper.Profile
        {
            public UcProfile()
            {
                CreateMap<Uc, ViewUcDto>().ReverseMap();
            }
        }
        public class LabTestDetailProfile : autoMapper.Profile
        {
            public LabTestDetailProfile()
            {
                CreateMap<LabTestDetail, CreateOrEditLabTestDetailDto>().ReverseMap();
                CreateMap<LabTestDetail, ViewLabTestDetailDto>().ReverseMap();
            }
        }

        #endregion

        #region Event
        public class EventProfile : autoMapper.Profile
        {
            public EventProfile()
            {
                CreateMap<Event, CreateOrEditEventDto>().ReverseMap();
                CreateMap<Event, ViewEventDto>().ReverseMap();
            }
        }

        public class EventHealthFaciltyProfile : autoMapper.Profile
        {
            public EventHealthFaciltyProfile()
            {
                CreateMap<EventHealthFacility, CreateOrEditEventHealthFacilityDto>().ReverseMap();
                //CreateMap<EventHealthFacility, ViewEventEventHealthFacilityDto>().ReverseMap();
            }
        }

        #endregion

        #region Invoice
        public class InvoiceMasterProfile : autoMapper.Profile
        {
            public InvoiceMasterProfile()
            {
                CreateMap<InvoiceMaster, InvoiceMasterDto>().ReverseMap();
            }
        }

        public class InvoiceItemProfile : autoMapper.Profile
        {
            public InvoiceItemProfile()
            {
                CreateMap<InvoiceItemList, InvoiceItemListDto>().ReverseMap();
            }
        }

        public class InvoiceDocumentProfile : autoMapper.Profile
        {
            public InvoiceDocumentProfile()
            {
                CreateMap<InvoiceDocumentList, InvoiceDocumentListDto>().ReverseMap();
            }
        }
        #endregion
    }
}
