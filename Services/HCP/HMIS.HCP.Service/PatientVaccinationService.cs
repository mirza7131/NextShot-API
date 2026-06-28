using AppCommonMethods;
using AutoMapper;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.HCP.Domain.Models.DbModels;
using HMIS.HCP.Domain.Models.DTO;
using HMIS.HCP.Domain.Repositories._UOW;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.HCP.Service
{
    public class PatientVaccinationService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<PatientVaccination> _uowPatientVaccination;

        #endregion

        #region Constructor

        public PatientVaccinationService(TokenService tokenService, UnitOfWork<PatientVaccination> uowPatientVaccination, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowPatientVaccination= uowPatientVaccination;
        }

        #endregion
        public async Task<List<PatientVaccination>> GetPatientPreviousVaccinations(Guid input)
        {
            //PatientScreening? responseObj = await _uowPatientScreening.Repository.GetById(input);
            var responseObj = await _uowPatientVaccination.Repository.GetALL(x => x.PatientId == input).ToListAsync();

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<List<PatientVaccination>>(responseObj);
        }
    }
}
