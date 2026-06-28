using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.DrugAddict.Domain.Models.DbModels;
using HMIS.DrugAddict.Domain.Models.Dto.SocialWelfareFormDto;
using HMIS.DrugAddict.Domain.Models.DTO.SocialWelfareFormDto;
using HMIS.DrugAddict.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profile = HMIS.DrugAddict.Domain.Models.DbModels.Profile;

namespace HMIS.DrugAddict.Service
{
    public class SocialWelfareCommunityDevelopmentService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<SocialWelfareTaskPerformedByCd> _uowWelfareForm;
        private UnitOfWork<SocialWelfareForm> _uowProfile;

        #endregion

        #region Constructor
        public SocialWelfareCommunityDevelopmentService(TokenService tokenService, UnitOfWork<SocialWelfareTaskPerformedByCd> uowWelfareFormDto, UnitOfWork<SocialWelfareForm> uowProfile, IMapper mapper)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowWelfareForm = uowWelfareFormDto;
            _uowProfile = uowProfile;
        }

        #endregion

        #region CUD
        public async Task<CreateOrEditSocialWelfareTaskPerformedByCDDto> CreateOrEdit(CreateOrEditSocialWelfareTaskPerformedByCDDto input)
        {

            if (AppCommonMethod.IsNullOrEmptyGuid(input.Id))
            {
                input.SessionNo=GetSessionCount(input.PatientVisitId);
                input.VisitDate=DateTime.Now;
                return await Create(input);
            }
            else
            {
                return await Update(input);
            }
        }


        private async Task<CreateOrEditSocialWelfareTaskPerformedByCDDto> Create(CreateOrEditSocialWelfareTaskPerformedByCDDto input)
        {
            var obj = _mapper.Map<SocialWelfareTaskPerformedByCd>(input);



            FillEntity(obj);
            var profile = _uowProfile.Repository.GetALL(x => x.Id==obj.SocialWelfareFormId).FirstOrDefault();


            if (profile != null)
            {
                obj.SocialWelfareFormId = profile.Id;
            }

            SocialWelfareTaskPerformedByCd responseObj = await _uowWelfareForm.Repository.Insert(obj);

            await _uowWelfareForm.CommitAsync();
            return _mapper.Map<CreateOrEditSocialWelfareTaskPerformedByCDDto>(responseObj);
        }

        private async Task<CreateOrEditSocialWelfareTaskPerformedByCDDto> Update(CreateOrEditSocialWelfareTaskPerformedByCDDto input)
        {
            var dbObj = await _uowWelfareForm.Repository.GetById(input.Id!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);

            _uowWelfareForm.Repository.Update(obj!);
            await _uowWelfareForm.CommitAsync();

            return _mapper.Map<CreateOrEditSocialWelfareTaskPerformedByCDDto>(obj);
        }

        private int GetSessionCount(Guid? patientVisitID)
        {
            int AddSession = 0;
            var checksession = _uowProfile.GetDbContext().SocialWelfareTaskPerformedByCds
                .Where(x => x.PatientVisitId==patientVisitID).Count();
            
            AddSession = checksession + 1;

            return AddSession;
        }
        #endregion

        #region Read
        public async Task<List<ViewDrugAddictCommunityDevelopment>> GetAll()
        {
            // GetAll Patients of Community Dev form
            try
            {
                var list = await _uowWelfareForm.GetDbContext()
                .ViewDrugAddictCommunityDevelopments
                .ToListAsync();
            
            if (AppCommonMethod.IsNullOrEmptyList(list))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                if (AppCommonMethod.IsNullOrEmptyList(list))
                    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        public async Task<ViewDrugAddictCommunityDevelopment> GetSinglePatientCD(Guid id)
        {
            try
            {
                var obj = await _uowWelfareForm.GetDbContext()
                .ViewDrugAddictCommunityDevelopments.Where(x => x.PatientVisitId == id)
                .FirstOrDefaultAsync();


                if (AppCommonMethod.IsNullObject(obj))
                    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);


                return obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ViewDrugAddictCommunityDevelopment>> GetPatientByPatientVisitId(Guid id)
        {
          
           try
            {
                var list = await _uowWelfareForm.GetDbContext()
               .ViewDrugAddictCommunityDevelopments.Where(x => x.PatientVisitId == id)
               .ToListAsync();

                if (AppCommonMethod.IsNullOrEmptyList(list))
                    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
      
      public async Task<List<ViewDrugAddictFieldOfficer>> GetSingleFieldOfficerPatient(Guid id) 
        {
            try
            {
                var data = await _uowWelfareForm.GetDbContext().ViewDrugAddictFieldOfficers
                .Where(x => x.DoctorId == id).ToListAsync();
                
              if (AppCommonMethod.IsNullOrEmptyList(data))
                    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
 

        public async Task<bool> PatientsVisitClosed(Guid id,string? patientStatus)
        {
            try
            {
                var PatientSessionList = await _uowWelfareForm.GetDbContext().SocialWelfareTaskPerformedByCds
                .Where(x => x.PatientVisitId == id).ToListAsync();

                if (PatientSessionList != null && PatientSessionList.Count > 0)
                {
                    SocialWelfareTaskPerformedByCd sessionToClose = null;
                    SocialWelfareTaskPerformedByCd largerSession = PatientSessionList[0];

                    foreach (var curSession in PatientSessionList)
                    {
                        if (curSession.SessionNo > largerSession.SessionNo)
                        {
                            largerSession = curSession;
                        }
                    }

                    sessionToClose = largerSession;
                    sessionToClose.IsVisitClosed = true;
                    sessionToClose.PtStatus = patientStatus!;
                    _uowWelfareForm.Repository.Update(sessionToClose);
                    await _uowWelfareForm.CommitAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Helper Methods

        private void FillEntity(SocialWelfareTaskPerformedByCd obj)
        {
            if (obj.Id == Guid.Empty)
            {
                obj.Id = Guid.NewGuid();
                obj.CreatedBy = _tokenService.GetUserId();
                obj.CreatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Create;
            }
            else
            {
                obj.UpdatedBy = _tokenService.GetUserId();
                obj.UpdatedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Edit;
            }
        }
        private void FillEntityDelete(SocialWelfareTaskPerformedByCd obj)
        {
            obj.DeletedBy = _tokenService.GetUserId();
            obj.DeletedOn = DateTime.Now;
            obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
        }

        #endregion
    }
}
