using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PatientPrescriptionDto;
using HMIS.Patient.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.Patient.Service
{
    public class PatientPrescriptionService<TEntity> where TEntity : class
    {
        #region Class Fields & Propertities

        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UnitOfWork<PatientPrescription> _uowPatientPrescription;

        #endregion

        #region Constructor

        public PatientPrescriptionService(TokenService tokenService, UnitOfWork<PatientPrescription> uowPatientPrescription, IMapper mapper)
        {
            _tokenService = tokenService;
            _uowPatientPrescription = uowPatientPrescription;
            _mapper = mapper;
        }

        #endregion

        #region CUD Operations

        public async Task<CreateOrEditPatientPrescriptionDto> CreateOrEdit(CreateOrEditPatientPrescriptionDto input)
        {
            if (AppCommonMethod.IsNullOrEmptyGuid(input.PatientPrescriptionId))
                return await Create(input);
            else
                return await Update(input);
        }

        private async Task<CreateOrEditPatientPrescriptionDto> Create(CreateOrEditPatientPrescriptionDto input)
        {
            var obj = _mapper.Map<PatientPrescription>(input);
            FillEntity(obj);
            PatientPrescription responseObj = await _uowPatientPrescription.Repository.Insert(obj);
            await _uowPatientPrescription.Save();
            return _mapper.Map<CreateOrEditPatientPrescriptionDto>(responseObj);

        }

        private async Task<CreateOrEditPatientPrescriptionDto> Update(CreateOrEditPatientPrescriptionDto input)
        {
            var dbObj = await _uowPatientPrescription.Repository.GetById(input.PatientPrescriptionId!);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            var obj = _mapper.Map(input, dbObj);
            FillEntity(obj!);
            _uowPatientPrescription.Repository.Update(obj!);
            await _uowPatientPrescription.CommitAsync();
            return _mapper.Map<CreateOrEditPatientPrescriptionDto>(obj);
        }

        public async Task<bool> Delete(object Id)
        {
            var _uowMedicineDispense = new UnitOfWork<MedicineDispatch>(_uowPatientPrescription.GetDbContext());
            var dbObj = await _uowPatientPrescription.Repository.GetById(Id);

            var IsDispensed = await _uowMedicineDispense.Repository.GetALL(x => x.PatientPrescriptionId == Guid.Parse((string)Id)).FirstOrDefaultAsync();
            if(!AppCommonMethod.IsNullObject(IsDispensed))
                throw new UserFriendlyExceptionForUI(CommonMessageConstant.DispenseAlert);

            if (AppCommonMethod.IsNullObject(dbObj))
                throw new UserFriendlyExceptionForUI(CommonMessageConstant.RecordNotFound);

            FillEntityDelete(dbObj!);

            _uowPatientPrescription.Repository.Update(dbObj!);
            await _uowPatientPrescription.CommitAsync();
            return true;
        }

        public async Task<bool> CheckIfDispensed(Guid Id)
        {
            var _uowMedicineDispense = new UnitOfWork<MedicineDispatch>(_uowPatientPrescription.GetDbContext());

            var IsDispensed = await _uowMedicineDispense.Repository.GetALL(x => x.PatientPrescriptionId == Id).FirstOrDefaultAsync();
            if (!AppCommonMethod.IsNullObject(IsDispensed))
                return true;
            else
                return false;
            //throw new UserFriendlyExceptionForUI(CommonMessageConstant.DispenseAlert);

        }

        public async Task<bool> CheckIfCanDeleteLab(Guid Id)
        {
            var _uowPatientLabTest = new UnitOfWork<PatientLabTest>(_uowPatientPrescription.GetDbContext());

            var canDelete = await _uowPatientLabTest.Repository.GetALL(x => x.PatientLabTestId == Id && (x.IsPaid == true)).FirstOrDefaultAsync();
            if (AppCommonMethod.IsNullObject(canDelete))
                return true;
            else
                return false;
            //throw new UserFriendlyExceptionForUI(CommonMessageConstant.DispenseAlert);

        }


        #endregion

        #region Read Operations

        public async Task<List<ViewPatientPrescriptionDto>> GetAll(Expression<Func<PatientPrescription, bool>>? filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            string includeProperties = "")
        {
            List<PatientPrescription> responseObj = await _uowPatientPrescription.Repository.GetALL(filter).ToListAsync();
            return _mapper.Map<List<ViewPatientPrescriptionDto>>(responseObj);
        }


        public async Task<ViewPatientPrescriptionDto> GetById(Guid input)
        {
            PatientPrescription? responseObj = await _uowPatientPrescription.Repository.GetById(input);

            if (AppCommonMethod.IsNullObject(responseObj))
                throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return _mapper.Map<ViewPatientPrescriptionDto>(responseObj);
        }


        #endregion

        #region Helper Methods

        private void FillEntity(PatientPrescription obj)
        {
            if (obj.PatientPrescriptionId == Guid.Empty)
            {
                obj.PatientPrescriptionId = Guid.NewGuid();
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
        private void FillEntityDelete(PatientPrescription obj)
        {
            if (obj != null)
            {
                obj.DeletedBy = _tokenService.GetUserId();
                obj.DeletedOn = DateTime.Now;
                obj.ActionTypeId = (int)ActionTypeEnum.Deleted;
            }
        }

        #endregion
    }
}
