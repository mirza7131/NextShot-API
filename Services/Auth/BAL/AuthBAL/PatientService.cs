using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AppCommonMethods;
using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Repositories.UOW;
using AutoMapper;
using Azure;
using CommonDTOs;
using CommonDTOs.Enums;
using CommonDTOs.ResponseDTO;
using CommonExceptionHandler;
using CommonMessages;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AuthBAL
{
    public class PatientService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<Patient> _uowPatient;

        #endregion

        #region Constructor

        public PatientService(
            TokenService tokenService,
            UnitOfWork<Patient> uowPatient,
            IMapper mapper
        )
        {
            _tokenService = tokenService;
            _uowPatient = uowPatient;
             _mapper = mapper;

        }

        #endregion

        #region CUD Operations

        //public async Task<CreateOrEditPatientDto> CreateOrEdit(CreateOrEditPatientDto input)
        //{
        //    if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientId))
        //        return await Create(input);
        //    else
        //        return await Update(input);
        //}

        //private async Task<CreateOrEditPatientDto> Create(CreateOrEditPatientDto input)
        //{
           

        //    var obj = _mapper.Map<DbModel.Patient>(input);
        //    FillEntity(obj);
        //    DbModel.Patient responseObj = await _uowPatient.Repository.Insert(obj);
        //    await _uowPatient.Save();
        //    return _mapper.Map<CreateOrEditPatientDto>(responseObj);
        //}

       


        #endregion

        #region Read Operations

        


        #endregion

        #region Helper Methods



        #endregion

       
    }
}
