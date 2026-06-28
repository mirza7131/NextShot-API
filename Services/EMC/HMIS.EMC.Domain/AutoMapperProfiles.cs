using HMIS.EMC.Domain.Models.DbModels;
using HMIS.EMC.Domain.Models.Dto;
using autoMapper = AutoMapper;

namespace HMIS.EMC.Domain
{
    public class AutoMapperProfiles : autoMapper.Profile
    {
        #region IcvCertificate
        public class IcvCertificateFormProfile : autoMapper.Profile
        {
            public IcvCertificateFormProfile()
            {
                CreateMap<IcvCertificate, CreateOrEditIcvCertificateDto>().ReverseMap();
            }
        }
        #endregion

        #region BirthCertificate
        public class BirthCertificateFormProfile : autoMapper.Profile
        {
            public BirthCertificateFormProfile()
            {
                CreateMap<BirthCertificate, CreateOrEditBirthCertificateDto>().ReverseMap();
                CreateMap<Emc,CreateOrEditEmcDto>().ReverseMap();
            }
        }
        #endregion

        #region Post-Mortem
        public class PostMortemFormProfile : autoMapper.Profile
        {
            public PostMortemFormProfile()
            {
                CreateMap<Mlcpostmortem,CreateOrEditPostMortemGeneralFormDto>().ReverseMap();
                CreateMap<MlcbodyIdentifierInfo, MLEBodyIdentifierInfoDto>().ReverseMap();
                CreateMap<PostmortemExternalExamination,CreateOrEditPostMortemExternalFormDto>().ReverseMap();
                CreateMap<PostMortemInternalExamination,CreateOrEditPostMortemInternalFormDto>().ReverseMap();
                CreateMap<PostmortemReport,CreateOrEditPostMortemReportDto>().ReverseMap();
                CreateMap <Mlcpostmortem,PostMortemRecordDto>().ReverseMap();
                CreateMap <MlcpoliceInfo,CreateOrEditMlcPoliceInfoDto>().ReverseMap();
                CreateMap <Mlc,CreateOrEditMLCDto>().ReverseMap();
            }
        }
        #endregion

        #region MLE
        public class MLEFormProfile : autoMapper.Profile { 
            public MLEFormProfile()
            {
                CreateMap<MlebasicInfo,CreateOrEditMLEBasicInfoDto>().ReverseMap(); 
                CreateMap<Mleexamination,CreateOrEditMLEExaminationDto>().ReverseMap(); 
                CreateMap<Mlereport,CreateOrEditMleReportDto>().ReverseMap(); 
                CreateMap<PatientImage, CreateOrEditPatientImageDto>().ReverseMap(); 
                CreateMap<Mlc, CreateOrEditMLCDto>().ReverseMap();
                CreateMap<ImageBaseSixtyFour, CreateOrEditBase64Dto>().ReverseMap();
                CreateMap<Mlc, MlcrecordDto>().ReverseMap();
                CreateMap<PatientDiagnose, PatientDiagnoseDto>().ReverseMap();
                CreateMap<Mlc, UpdateAssignDoctorDto>().ReverseMap();
            
            } 
        }
        #endregion

        #region MLCSV
        public class MLCSVProfile : autoMapper.Profile
        {
            public MLCSVProfile()
            {
                CreateMap<MlcsvinitialInfo, CreateOrEditMlcSvInitialInfoDto>().ReverseMap();
                CreateMap<Mlcsvexamination, CreateOrEditMlcSvExaminationDto>().ReverseMap();
                CreateMap<MlcsvevidenceCollected, CreateOrEditMlcSvEvidenceCollectedDto>().ReverseMap();
                CreateMap<Mlcsvreport, CreateOrEditMlcSvReportDto>().ReverseMap();
            }
        }
        #endregion

        #region DeathCertificate
        public class DeathCertificateProfile : autoMapper.Profile
        {
            public DeathCertificateProfile()
            {
                CreateMap<DeathCertificate, CreateOrEditDeathCertificateDto>().ReverseMap();
            }
        }
        #endregion

        #region FitnessCertificate
        public class FitnessCertificateProfile : autoMapper.Profile
        {
            public FitnessCertificateProfile()
            {
                CreateMap<FitnessCertificate, CreateOrEditFitnessCertificateDto>().ReverseMap();
                CreateMap<FitnessSerology, CreateOrEditFitnessSerologyDto>().ReverseMap();
                CreateMap<FitnessCbc, CreateOrEditFitnessCBCDto>().ReverseMap();
                CreateMap<FitnessUrineCe, CreateOrEditFitnessUrineCEDto>().ReverseMap();
                CreateMap<FitnessGeneralParameter, CreateOrEditFitnessGeneralOrWidalParameterDto>().ReverseMap();
                CreateMap<FitnessStoolExamination, CreateOrEditFitnessStoolExaminationDto>().ReverseMap();
                CreateMap<MentalAssessment, MentalAssessmentDto>().ReverseMap();
            }
        }
        #endregion
    }
}