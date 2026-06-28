using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.HealthFacilityDto;
using HMIS.Patient.Domain.Models.DTO.MedicineDispatchDto;
using HMIS.Patient.Domain.Models.DTO.MedicineLookupDto;
using HMIS.Patient.Domain.Models.DTO.Patient;
using HMIS.Patient.Domain.Models.DTO.DashboardDto;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseDiseaseDto;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseDto;
using HMIS.Patient.Domain.Models.DTO.PatientDto;
using HMIS.Patient.Domain.Models.DTO.PatientLabTestDto;
using HMIS.Patient.Domain.Models.DTO.PatientOpenVisitDto;
using HMIS.Patient.Domain.Models.DTO.PatientPrescriptionDto;
using HMIS.Patient.Domain.Models.DTO.PatientVitalDto;
using HMIS.Patient.Domain.Models.DTO.PatientWorkFlowLogDto;
using HMIS.Patient.Domain.Models.DTO.ProfileDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using autoMapper = AutoMapper;
using DbModel = HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.ProvinceDto;
using HMIS.Patient.Domain.Models.DTO.DivisionDto;
using HMIS.Patient.Domain.Models.DTO.DistrictDto;
using HMIS.Patient.Domain.Models.DTO.TehsilDto;
using HMIS.Patient.Domain.Models.DTO.PhysiotherapyModalityDto;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseRecordDto;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseTemplateDto;
using HMIS.Patient.Domain.Models.DTO.PatientContactDetailsDTO;
using HMIS.Patient.Domain.Models.DTO.PatientVisitFlowDto;
using HMIS.Patient.Domain.Models.DTO.SectionProcedureDto;
using HMIS.Patient.Domain.Models.DTO.PatientDiagnoseProcedureDto;
using HMIS.Patient.Domain.Models.DTO.DashboardDto.AdminReferedDashboard;
using HMIS.Patient.Domain.Models.DTO.PatientAdmissionDetailDto;
using HMIS.Patient.Domain.Models.DTO.PatientDischargeDetailDto;
using HMIS.Patient.Domain.Models.DTO.PatientScreeningDto;
using HMIS.Patient.Domain.Models.DTO.PatientAssessmentDto;
using HMIS.Patient.Domain.Models.DTO.PatientVaccinationDto;
using HMIS.Aggregator.API.Models.NADRA;
using HMIS.Aggregator.API.Models;
using HMIS.Patient.Domain.Models.DTO.DentalDto;
using HMIS.Patient.Domain.Models.DTO.PatientDocument;
using HMIS.Patient.Domain.Models.DTO.MimsMedicineData;
using HMIS.Patient.Domain.Models.DTO.MimsGetMedicineResponse;
using HMIS.Patient.Domain.Models.DTO.MimsMedicineIndentLog;
using HMIS.Patient.Domain.Models.DTO.MimsMedicineIndentDetail;
using HMIS.Patient.Domain.Models.DTO.MedicineAdvisedDto;
using HMIS.Patient.Domain.Models.DTO.MedicineAdvisedRequisitionDto;
using HMIS.Patient.Domain.Models.DTO.NursingEventDto;


namespace HMIS.Patient.Domain
{
    public class AutoMapperProfiles
    {
        #region Patient
        public class UserProfile : autoMapper.Profile
        {
            public UserProfile()
            {
                CreateMap<DbModel.Patient, CreateOrEditPatientCentrallyDto>().ReverseMap();
                CreateMap<CreateOrEditPatientDto, CreateOrEditPatientCentrallyDto>().ReverseMap();
                CreateMap<EmrPatientRegistrationResponseDto, CreateOrEditPatientWithVisitDto>().ReverseMap();
                CreateMap<DbModel.Patient, EmrPatientRegistrationResponseDto>().ReverseMap();
                CreateMap<CreateOrEditPatientWithVisitDto, ViewPatientVisitsListWithDetailDto>().ReverseMap();


                CreateMap<DbModel.Patient, CreateOrEditPatientDto>().ReverseMap();


                CreateMap<DbModel.PatientAdditionalInfo, CreateOrEditAdditionalPatientDTO>().ReverseMap();
                CreateMap<DbModel.Patient, ViewPatientDto>().ReverseMap();
                //.ForMember(dest => dest.RelationProfile, opt => opt.MapFrom(src => src.Name));

                CreateMap<CreateOrEditPatientDto, CreateOrEditPatientWithVisitDto>().ReverseMap();
                CreateMap<DbModel.Patient, CreateOrEditPatientWithVisitDto>().ReverseMap();
                CreateMap<DbModel.Patient, ViewPatientWithVisitDto>().ReverseMap();
                CreateMap<DbModel.Patient, ViewPatientDetailsDto>().ReverseMap();
                CreateMap<Person, ViewPatientDto>().ReverseMap();
            }
        }

        public class PatientProfile : autoMapper.Profile
        {
            public PatientProfile()
            {
                CreateMap<DbModel.Patient, CreateOrEditPatientDto>().ReverseMap();
                CreateMap<DbModel.Patient, ViewPatientDto>().ReverseMap();
                CreateMap<GetPatientByFilterDto, ViewPatientDto>().ReverseMap();
                CreateMap<PatientEyeBlindness, CreateOrEditPatientWithVisitDto>().ReverseMap();
                CreateMap<DoctorNote, DoctorNotesDTO>().ReverseMap();
            }
        }

        public class PatientNadraVerification : autoMapper.Profile
        {
            public PatientNadraVerification()
            {
                CreateMap<DbModel.PatientNadraRequest, NadraVerificationDTO>().ReverseMap();
                CreateMap<DbModel.PatientNadraResponse, NadraResponseDTO>().ReverseMap();
                CreateMap<DbModel.PatientNadraInfoResponse, VerifiedPatientDataFromNADRADTO>().ReverseMap();
                CreateMap<DbModel.Patient, UnknownPatientDto>().ReverseMap();
                CreateMap<DbModel.Patient, UnknownPatient>().ReverseMap();
                CreateMap<DbModel.Patient, CreateOrEditPatientWithVisitDto>().ReverseMap();
                CreateMap<DbModel.UnknownPatient, UnknownPatientDto>().ReverseMap();
            }
        }


        public class UnknownPatientProfile : autoMapper.Profile
        {
            public UnknownPatientProfile()
            {
                CreateMap<DbModel.UnknownPatient, CreateOrEditPatientWithVisitDto>().ReverseMap();
            }
        }

        public class ProvinceProfile : autoMapper.Profile
        {
            public ProvinceProfile()
            {
                CreateMap<Province, ViewProvinceDto>().ReverseMap();
            }
        }

        public class DivisionProfile : autoMapper.Profile
        {
            public DivisionProfile()
            {
                CreateMap<Division, ViewDivisionDto>().ReverseMap();
            }
        }

        public class DistrictProfile : autoMapper.Profile
        {
            public DistrictProfile()
            {
                CreateMap<District, ViewDistrictDto>().ReverseMap();
            }
        }

        public class TehsilProfile : autoMapper.Profile
        {
            public TehsilProfile()
            {
                CreateMap<Tehsil, ViewTehsilDto>().ReverseMap();
            }
        }

        public class PatientVitalsProfile : autoMapper.Profile
        {
            public PatientVitalsProfile()
            {
                CreateMap<PatientVital, CreateOrEditPatientVitalDto>().ReverseMap();
                CreateMap<PatientVital, ViewPatientVitalDto>().ReverseMap();
                CreateMap<PatientVital, PatientVitalDto>().ReverseMap();
            }
        }

        public class PatientOpenVisitProfile : autoMapper.Profile
        {
            public PatientOpenVisitProfile()
            {
                CreateMap<PatientOpenVisit, CreateOrEditOpenVisitDto>().ReverseMap();
                CreateMap<PatientOpenVisit, ViewPatientOpenVisitDto>().ReverseMap();
                CreateMap<PatientOpenVisit, ViewPatientDetailsDto>().ReverseMap();
                CreateMap<PatientOpenVisit, CreateorEditPatientEligibleForSSCDto>().ReverseMap();
                CreateMap<PatientOpenVisit, UpdateSscStatusAndDocumentDto>().ReverseMap();

            }
        }

        public class PatientAdmissionDetailProfile : autoMapper.Profile
        {
            public PatientAdmissionDetailProfile()
            {
                CreateMap<PatientAdmissionDetail, CreateOrEditPatientAdmissionDetailDto>().ReverseMap();
                CreateMap<PatientAdmissionDetail, ViewPatientAdmissionDetailDto>().ReverseMap();
            }
        }

        public class PatientDischargeDetailProfile : autoMapper.Profile
        {
            public PatientDischargeDetailProfile()
            {
                CreateMap<PatientDischargeDetail, CreateOrEditPatientDischargeDetailDto>().ReverseMap();
                CreateMap<PatientDischargeDetail, ViewPatientDischargeDetailDto>().ReverseMap();
            }
        }

        public class PatientDiagnoseProfile : autoMapper.Profile
        {
            public PatientDiagnoseProfile()
            {
                CreateMap<PatientDiagnose, CreateOrEditPatientDiagnoseDto>().ReverseMap();
                CreateMap<PatientDiagnose, ViewPatientDiagnoseDto>().ReverseMap();
                CreateMap<PatientDiagnose, CreateOrEditPatientDiagnoseWithPrescriptionDto>().ReverseMap();
                CreateMap<CreateOrEditPatientDiagnoseWithPrescriptionDto, CreateOrEditPatientDiagnoseWithPrescriptionDto>().ReverseMap();
                CreateMap<TbPatientDetail, PatientDiagnose>().ReverseMap();
                CreateMap<PhysiotherapyHomeExercisePlan, HomeExercisePlanDTO>().ReverseMap();
                CreateMap<PatientDiagnoseProcedure, DentalProcedureListDTO>().ReverseMap();


                #region SpeechFormMapper
                CreateMap<DevelopmentMilestone, CreateOrEditPatientDiagnoseWithSpeechtherapyFormDto>().ReverseMap();
                CreateMap<SpeechMilestone, CreateOrEditPatientDiagnoseWithSpeechtherapyFormDto>().ReverseMap();
                CreateMap<SpeechAndLanguageHistory, CreateOrEditPatientDiagnoseWithSpeechtherapyFormDto>().ReverseMap();
                CreateMap<EducationalHistory, CreateOrEditPatientDiagnoseWithSpeechtherapyFormDto>().ReverseMap();
                CreateMap<FamiliyHistory, CreateOrEditPatientDiagnoseWithSpeechtherapyFormDto>().ReverseMap();
                CreateMap<HearingProblemHistory, CreateOrEditPatientDiagnoseWithSpeechtherapyFormDto>().ReverseMap();
                CreateMap<SpeechTherapyPatientAssessment, CreateOrEditPatientDiagnoseWithSpeechtherapyFormDto>().ReverseMap();
                CreateMap<SpeechDisorder, CreateOrEditPatientDiagnoseWithSpeechtherapyFormDto>().ReverseMap();
                CreateMap<AssociatedDisorder, CreateOrEditPatientDiagnoseWithSpeechtherapyFormDto>().ReverseMap();
                CreateMap<SpeechModality, CreateOrEditPatientDiagnoseWithSpeechtherapyFormDto>().ReverseMap();
                #endregion

                #region NutritionFormMapper
                CreateMap<PatientBmi, CreateOrEditPatientDiagnoseWithNutritionisttherapyFormDto>().ReverseMap();
                CreateMap<PatientNutritionalRisk, CreateOrEditPatientDiagnoseWithNutritionisttherapyFormDto>().ReverseMap();
                CreateMap<NutritionalAsessmentFinding, CreateOrEditPatientDiagnoseWithNutritionisttherapyFormDto>().ReverseMap();
                CreateMap<PatientComorbidity, CreateOrEditPatientDiagnoseWithNutritionisttherapyFormDto>().ReverseMap();
                CreateMap<PatientMalnutrition, CreateOrEditPatientDiagnoseWithNutritionisttherapyFormDto>().ReverseMap();
                CreateMap<PatientDietPlan, CreateOrEditPatientDiagnoseWithNutritionisttherapyFormDto>().ReverseMap();
                #endregion

                #region PsychologicalFormMapper
                CreateMap<PastPsychiatricHistory, CreateOrEditPatientDiagnoseWithPsychologytherapyFormDto>().ReverseMap();
                CreateMap<PsychologicalAssessment, CreateOrEditPatientDiagnoseWithPsychologytherapyFormDto>().ReverseMap();
                CreateMap<PsychologicalTestApplied, CreateOrEditPatientDiagnoseWithPsychologytherapyFormDto>().ReverseMap();
                CreateMap<PsychologyPatientModality, CreateOrEditPatientDiagnoseWithPsychologytherapyFormDto>().ReverseMap();
                CreateMap<PsychologyDisorder, CreateOrEditPatientDiagnoseWithPsychologytherapyFormDto>().ReverseMap();
                CreateMap<PsychologyAssociatedDisorder, CreateOrEditPatientDiagnoseWithPsychologytherapyFormDto>().ReverseMap();
                #endregion
            }
        }

        public class PatientDiagnoseTemplateProfile : autoMapper.Profile
        {
            public PatientDiagnoseTemplateProfile()
            {
                CreateMap<PatientDiagnoseTemplate, CreateOrEditPatientDiagnoseTemplateDto>().ReverseMap();
                CreateMap<PatientDiagnoseTemplate, ViewPatientDiagnoseTemplateDto>().ReverseMap();
            }
        }

        public class PhysiotherapyFormProfile : autoMapper.Profile
        {
            public PhysiotherapyFormProfile()
            {
                CreateMap<PhysiotherapyForm, CreateOrEditPatientDiagnoseWithPhysiotherapyFormDto>().ReverseMap();
            }
        }

        public class PhysiotherapyModalityProfile : autoMapper.Profile
        {
            public PhysiotherapyModalityProfile()
            {
                CreateMap<PhysiotherapyModality, CreateOrEditPhysiotherapyModalityDto>().ReverseMap();
            }
        }

        public class PatientDiagnoseDiseaseProfile : autoMapper.Profile
        {
            public PatientDiagnoseDiseaseProfile()
            {
                CreateMap<PatientDiagnoseDisease, CreateOrEditPatientDiagnoseDiseaseDto>().ReverseMap();
                CreateMap<PatientDiagnoseDisease, ViewPatientDiagnoseDiseaseDto>().ReverseMap();
            }
        }
        public class PatientDiagnosisRecordProfile : autoMapper.Profile
        {
            public PatientDiagnosisRecordProfile()
            {
                CreateMap<PatientDiagnosisRecord, CreateOrEditPatientDiagnoseRecordDto>().ReverseMap();
                CreateMap<PatientDiagnosisRecord, ViewPatientDiagnoseRecordDto>().ReverseMap();
            }
        }

        public class PatientPrescriptionProfile : autoMapper.Profile
        {
            public PatientPrescriptionProfile()
            {
                CreateMap<PatientPrescription, CreateOrEditPatientPrescriptionDto>().ReverseMap();
                CreateMap<PatientPrescription, ViewPatientPrescriptionDto>().ReverseMap();
            }
        }


        public class PatientContactDetailsProfile : autoMapper.Profile
        {
            public PatientContactDetailsProfile()
            {
                CreateMap<PatientContactDetail, CreatePatientContactDetailsDTO>().ReverseMap();
            }
        }


        public class PatientLabTestProfile : autoMapper.Profile
        {
            public PatientLabTestProfile()
            {
                CreateMap<PatientLabTest, CreateOrEditPatientLabTestDto>().ReverseMap();
                CreateMap<PatientLabTest, ViewPatientLabTestDto>().ReverseMap();
            }
        }

        public class PatientLabTestDetailProfile : autoMapper.Profile
        {
            public PatientLabTestDetailProfile()
            {
                CreateMap<PatientLabTestDetail, CreateOrEditPatientLabTestDto>().ReverseMap();
                CreateMap<PatientLabTestDetail, ViewPatientLabTestDto>().ReverseMap();
                CreateMap<PatientLabTestDetail, LabTestDetail>().ReverseMap();
            }
        }

        public class MedicineLookupProfile : autoMapper.Profile
        {
            public MedicineLookupProfile()
            {
                CreateMap<MedicineLookup, CreateOrEditMedicineLookupDto>().ReverseMap();
                CreateMap<MedicineLookup, ViewMedicineLookupDto>().ReverseMap();
            }
        }

        public class MedicineDispatchProfile : autoMapper.Profile
        {
            public MedicineDispatchProfile()
            {
                CreateMap<MedicineDispatch, CreateOrEditMedicineDispatchDto>().ReverseMap();
                CreateMap<Models.DTO.MedicineDispatchDto.MedicineDispatchDto, CreateOrEditPatientPrescriptionDto>().ReverseMap();
                CreateMap<MedicineDispatch, ViewMedicineDispatchDto>().ReverseMap();
                CreateMap<MedicineDispatch, Models.DTO.MedicineDispatchDto.MedicineDispatchDto>().ReverseMap();
                CreateMap<RiskFactor, Models.DTO.MedicineDispatchDto.RiskFactorsDTO>().ReverseMap();
            }
        }

        public class ProfileProfile : autoMapper.Profile
        {
            public ProfileProfile()
            {
                CreateMap<Profile, ViewProfileDto>().ReverseMap();
            }
        }

        public class HealthFacilityProfile : autoMapper.Profile
        {
            public HealthFacilityProfile()
            {
                CreateMap<HealthFacility, ViewHealthFacilityDto>().ReverseMap();
            }
        }

        public class PatientWorkFlowLogProfile : autoMapper.Profile
        {
            public PatientWorkFlowLogProfile()
            {
                CreateMap<PatientWorkFlowLog, CreateOrEditPatientWorkFlowLogDto>().ReverseMap();
                CreateMap<PatientWorkFlowLog, ViewPatientWorkFlowLogDto>().ReverseMap();
            }
        }

        public class PatientVisitFlowProfile : autoMapper.Profile
        {
            public PatientVisitFlowProfile()
            {
                CreateMap<PatientVisitFlow, CreateOrEditPatientVisitFlowDto>().ReverseMap();
                CreateMap<PatientVisitFlow, ViewPatientVisitFlowDto>().ReverseMap();
            }
        }

        public class SectionProcedureProfile : autoMapper.Profile
        {
            public SectionProcedureProfile()
            {
                CreateMap<SectionProcedure, CreateOrEditSectionProcedureDto>().ReverseMap();
                CreateMap<SectionProcedure, ViewSectionProcedureDto>().ReverseMap();
            }
        }

        public class PatientDiagnoseProcedureProfile : autoMapper.Profile
        {
            public PatientDiagnoseProcedureProfile()
            {
                CreateMap<PatientDiagnoseProcedure, CreateOrEditPatientDiagnoseProcedureDto>().ReverseMap();
                CreateMap<PatientDiagnoseProcedure, ViewPatientDiagnoseProcedureDto>().ReverseMap();
            }
        }

        public class PatientDocumentProfile : autoMapper.Profile
        {
            public PatientDocumentProfile()
            {
                CreateMap<PatientDocument, CreateOrEditPatientDocumentDto>().ReverseMap();
                CreateMap<PatientDocument, ViewPatientDocumentDto>().ReverseMap();
            }
        }



        #region HCP
        public class PatientScreeningProfile : autoMapper.Profile
        {
            public PatientScreeningProfile()
            {
                CreateMap<PatientScreening, CreateOrEditPatientScreeningDto>().ReverseMap();

            }

        }

        public class PatientAssessmentProfile : autoMapper.Profile
        {
            public PatientAssessmentProfile()
            {
                CreateMap<PatientAssessment, CreateOrEditPatientAssessmentDto>().ReverseMap();

            }

        }

        public class PatientVaccinationProfile : autoMapper.Profile
        {
            public PatientVaccinationProfile()
            {
                CreateMap<PatientVaccination, CreateOrEditPatientVaccinationDto>().ReverseMap();

            }

        }
        #endregion

        #endregion

        #region Audit Log



        #endregion

        #region DB View
        public class ViewPatientOpenVisitCountByGenderByDateProfile : autoMapper.Profile
        {
            public ViewPatientOpenVisitCountByGenderByDateProfile()
            {
                CreateMap<ViewPatientOpenVisitCountByGenderByDate, ViewPatientOpenVisitCountByGenderByMonthDto>().ReverseMap();
            }
        }

        public class ViewTodayPatientVisitCountByDeptBySecProfile : autoMapper.Profile
        {
            public ViewTodayPatientVisitCountByDeptBySecProfile()
            {
                CreateMap<ViewTodayPatientOpenVisitCountByDeptBySec, ViewTodayPatientVisitCountByDeptBySecDto>().ReverseMap();
            }
        }

        public class ViewPatientOpenVisitCountByDeptBySecByMonthProfile : autoMapper.Profile
        {
            public ViewPatientOpenVisitCountByDeptBySecByMonthProfile()
            {
                CreateMap<ViewPatientOpenVisitCountByDeptBySecByMonth, ViewPatientVisitCountByDeptBySecByMonthDto>().ReverseMap();
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

        public class MimsGetMedicineResponseDtoProfile : autoMapper.Profile
        {
            public MimsGetMedicineResponseDtoProfile()
            {
                CreateMap<MimsGetMedicineResponse, CreateOrEditMimsGetMedicineResponseDto>().ReverseMap();
                CreateMap<MimsGetMedicineResponse, ViewMimsGetMedicineResponseDto>().ReverseMap();
            }
        }
        public class MimsMedicineIndentLogDtoProfile : autoMapper.Profile
        {
            public MimsMedicineIndentLogDtoProfile()
            {
                CreateMap<MimsMedicineIndentLog, CreateOrEditMimsMedicineIndentLogDto>().ReverseMap();
                CreateMap<MimsMedicineIndentLog, ViewMimsMedicineIndentLogDto>().ReverseMap();
            }
        }

        public class MimsMedicineIndentDetailProfile : autoMapper.Profile
        {
            public MimsMedicineIndentDetailProfile()
            {
                CreateMap<MimsMedicineIndentDetail, CreateOrEditMimsMedicineIndentDetailDto>().ReverseMap();
            }
        }
        #endregion

        #region DentalSterilization
        public class DentalSterilizationRecordProfile : autoMapper.Profile
        {
            public DentalSterilizationRecordProfile()
            {
                CreateMap<DentalSterilizationRecord, CreateOrEditDentalSterilizationRecordDto>().ReverseMap();
                CreateMap<DentalSterilizationRecord, ViewDentalSterilizationRecordDto>().ReverseMap();
            }
        }
        #endregion

        #region emergency
        public class MedicineAdvisedProfile : autoMapper.Profile
        {
            public MedicineAdvisedProfile()
            {
                CreateMap<MedicineAdvised, CreateOrEditMedicineAdvisedDto>().ReverseMap();
                CreateMap<MedicineAdvised, ViewMedicineAdvisedDto>().ReverseMap();
            }
        }

        public class NursingEventProfile : autoMapper.Profile
        {
            public NursingEventProfile()
            {
                CreateMap<NursingEvent, CreateOrEditNursingEventDto>().ReverseMap();
                //CreateMap<NursingEvent, ViewMedicineAdvisedDto>().ReverseMap();
            }
        }

        

        public class MedicineAdvisedRequisitionProfile : autoMapper.Profile
        {
            public MedicineAdvisedRequisitionProfile()
            {
                CreateMap<MedicineAdvisedRequisition, CreateOrEditMedicineAdvisedRequisitionDto>().ReverseMap();
                CreateMap<MedicineAdvisedRequisition, ViewMedicineAdvisedRequisitionDto>().ReverseMap();
            }
        }
        #endregion
    }
}
