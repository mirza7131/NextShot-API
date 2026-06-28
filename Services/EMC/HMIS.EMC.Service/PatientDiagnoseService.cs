using HMIS.EMC.Domain.Models.DbModels;
using JWTAuthentication;
using HMIS.EMC.Domain.Repositories.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMIS.EMC.Domain.Models.Dto;
using AutoMapper;

namespace HMIS.EMC.Service
{
    public class PatientDiagnoseService
    {
        #region Class Fields & Propeties
        private readonly TokenService _tokenService;
        private UnitOfWork<PatientDiagnose> _uowPatientDiagnose;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public PatientDiagnoseService(TokenService tokenService, UnitOfWork<PatientDiagnose> uowPatientDiagnose, IMapper mapper)
        {
            _tokenService = tokenService;
            _uowPatientDiagnose = uowPatientDiagnose;
            _mapper = mapper;
        }
        #endregion

        #region CUD
        public async Task<PatientDiagnoseDto> InsertPatientDiagnose(PatientDiagnoseDto Input)
        {
            Input.PatientDiagnoseId = Guid.NewGuid();
            var diagnose = _mapper.Map<PatientDiagnose>(Input);
            diagnose.DoctorVisitNo = 1;
            diagnose.DiagnosedBy = _tokenService.GetUserId();
            diagnose.IsDiagnoseExternally = false;
            diagnose.IsActive = true;
            diagnose.ActionTypeId = 1;
            diagnose.CreatedOn = DateTime.Now;  
            diagnose.CreatedBy = _tokenService.GetUserId();

            var dg = await _uowPatientDiagnose.Repository.Insert(diagnose);
            await _uowPatientDiagnose.CommitAsync();

            return Input;
        }
        #endregion
    }
}
