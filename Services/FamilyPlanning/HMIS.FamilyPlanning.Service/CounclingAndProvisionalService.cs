using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.FamilyPlanning.Domain.Models.DbModels;
using HMIS.FamilyPlanning.Domain.Models.DTO;
using HMIS.FamilyPlanning.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.FamilyPlanning.Service
{
	public class CounclingAndProvisionalService
	{


		#region Class Fields & Propertities
		private readonly TokenService _tokenService;
		private readonly IMapper _mapper;
		private UnitOfWork<CounslingAndProvision> _uowCounclingAndProvisional;
		#endregion


		#region Constructor
		public CounclingAndProvisionalService(TokenService tokenService, IMapper mapper, UnitOfWork<CounslingAndProvision> uowCounclingAndProvisional)
		{
			_tokenService = tokenService;
			_mapper = mapper;
			_uowCounclingAndProvisional = uowCounclingAndProvisional;
		}
		#endregion



		#region CUD
		public async Task CreateOrEditCounsellingAndProvision(CounsellingAndProvisionDTO counsellingAndProvisionDTO)
		{
			if (AppCommonMethod.IsNullObject(counsellingAndProvisionDTO))
				throw new UserFriendlyException(CommonMessageConstant.DTOIsNUll);

			if (AppCommonMethod.IsNullOrEmptyGuid(counsellingAndProvisionDTO.CounslingAndProvisionalId))
			{
				await CreateCounsellingAndProvision(counsellingAndProvisionDTO);
			}
			else
			{
				await UpdateCounsellingAndProvision(counsellingAndProvisionDTO);
			}
		}

		private async Task CreateCounsellingAndProvision(CounsellingAndProvisionDTO counsellingAndProvisionDTO)
		{
			using (var trans = _uowCounclingAndProvisional.GetDbContext().Database.BeginTransaction())
			{
				try
				{
					CounslingAndProvision counslingAndProvision = new CounslingAndProvision();
					counslingAndProvision = _mapper.Map<CounslingAndProvision>(counsellingAndProvisionDTO);
					FillEntityCounslingAndProvision(counslingAndProvision);
					await _uowCounclingAndProvisional.Repository.Insert(counslingAndProvision);
					await _uowCounclingAndProvisional.Save();


					var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowCounclingAndProvisional.GetDbContext());

					var data = await _uowPatientOpenVisit.Repository.GetById(counslingAndProvision.PatientVisitId);
					if (!AppCommonMethod.IsNullObject(data))
					{
						data.IsDischarge = true;
						_uowPatientOpenVisit.Repository.Update(data);
						await _uowPatientOpenVisit.Save();
						trans.Commit();
					}
				}
				catch (Exception ex)
				{
					trans.Rollback();
				}
			}
		}



		private async Task UpdateCounsellingAndProvision(CounsellingAndProvisionDTO counsellingAndProvisionDTO)
		{
			var counsellingAndProvision = await _uowCounclingAndProvisional.Repository.GetById(counsellingAndProvisionDTO.CounslingAndProvisionalId);
			if (!AppCommonMethod.IsNullObject(counsellingAndProvision))
			{
				_mapper.Map(counsellingAndProvisionDTO, counsellingAndProvision);
				FillEntityCounslingAndProvision(counsellingAndProvision);
				_uowCounclingAndProvisional.Repository.Update(counsellingAndProvision);
				await _uowCounclingAndProvisional.Save();
			}
		}




		public async Task<CounsellingAndProvisionDTO> GetPatientCounsellingAndProvisionById(Guid PatientId)
		{
			if (AppCommonMethod.IsNullOrEmptyGuid(PatientId))
				throw new UserFriendlyException(CommonMessageConstant.PatientIdIsNUll);

			//var _uowCounslingAndProvision = new UnitOfWork<CounslingAndProvision>(_uow.GetDbContext());
			var counslingAndProvision = await _uowCounclingAndProvisional.Repository.GetALL(x => x.PatientId == PatientId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();

			return _mapper.Map<CounsellingAndProvisionDTO>(counslingAndProvision);
		}
		#endregion



		#region Helper Methods

		private void FillEntityCounslingAndProvision(CounslingAndProvision obj)
		{
			if (obj.CounslingAndProvisionalId == Guid.Empty)
			{
				obj.CounslingAndProvisionalId = Guid.NewGuid();
				obj.CreatedBy = _tokenService.GetUserId();
				obj.CreatedOn = DateTime.Now;
				obj.ActionTypeId = (int)ActionTypeEnum.Create;
				obj.IsActive = true;
			}
			else
			{
				obj.UpdatedBy = _tokenService.GetUserId();
				obj.UpdatedOn = DateTime.Now;
				obj.ActionTypeId = (int)ActionTypeEnum.Edit;
				obj.IsActive = true;
			}
		}


		#endregion

	}
}
