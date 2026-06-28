using AutoMapper;
using HMIS.FamilyPlanning.Domain.Models.DbModels;
using JWTAuthentication;
using HMIS.FamilyPlanning.Domain.Repositories.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMIS.FamilyPlanning.Domain.Models.DTO;
using AppCommonMethods;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HMIS.FamilyPlanning.Service
{
	public class ClientFollowupService
	{

		#region Class Fields & Propertities
		private readonly TokenService _tokenService;
		private readonly IMapper _mapper;
		private UnitOfWork<ClientFollowup> _uowClientFollowup;

		#endregion


		#region Constructor
		public ClientFollowupService(TokenService tokenService, IMapper mapper, UnitOfWork<ClientFollowup> uowClientFollowup)
		{
			_tokenService = tokenService;
			_mapper = mapper;
			_uowClientFollowup = uowClientFollowup;
		}
		#endregion



		#region CUD
		public async Task CreateOrEditClientFollowUp(ClientFollowupDTO clientFollowupDTO)
		{
			if (AppCommonMethod.IsNullObject(clientFollowupDTO))
				throw new UserFriendlyException(CommonMessageConstant.DTOIsNUll);

			//if (AppCommonMethod.IsNullOrEmptyGuid(clientFollowupDTO.FollowupClientId))
			//{
				await CreateClientFollowUp(clientFollowupDTO);
			//}
			//else
			//{
			//	await UpdateClientFollowUp(clientFollowupDTO);
			//}
		}

		private async Task CreateClientFollowUp(ClientFollowupDTO clientFollowupDTO)
		{
			using (var trans = _uowClientFollowup.GetDbContext().Database.BeginTransaction())
			{
				try
				{
					ClientFollowup clientFollowup = new ClientFollowup();
					clientFollowup = _mapper.Map<ClientFollowup>(clientFollowupDTO);
					FillEntityClientFollowup(clientFollowup);
					await _uowClientFollowup.Repository.Insert(clientFollowup);
					await _uowClientFollowup.Save();

					var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowClientFollowup.GetDbContext());

					var data = await _uowPatientOpenVisit.Repository.GetById(clientFollowupDTO.PatientVisitId);
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


		private async Task UpdateClientFollowUp(ClientFollowupDTO clientFollowupDTO)
		{
			var clientFollowup = await _uowClientFollowup.Repository.GetById(clientFollowupDTO.FollowupClientId);
			if (!AppCommonMethod.IsNullObject(clientFollowup))
			{
				_mapper.Map(clientFollowupDTO, clientFollowup);
				FillEntityClientFollowup(clientFollowup);
				_uowClientFollowup.Repository.Update(clientFollowup);
				await _uowClientFollowup.Save();
			}
		}
		#endregion



		#region Read operations
		public async Task<Guid> GetMethodInUse(Guid PatientId)
		{
			var _uowCounslingAndProvision = new UnitOfWork<CounslingAndProvision>(_uowClientFollowup.GetDbContext());
			var SwitchedMethodProfileId = await _uowClientFollowup.Repository.GetALL(x => x.PatientId == PatientId).OrderByDescending(x => x.CreatedOn).Select(x => x.SwitchedMethodProfileId).FirstOrDefaultAsync();
			if (AppCommonMethod.IsNullOrEmptyGuid(SwitchedMethodProfileId))
			{
				var MethodAdoptedProfileId = await _uowCounslingAndProvision.Repository.GetALL(x => x.PatientId == PatientId).OrderByDescending(x => x.CreatedOn).Select(x => x.MethodAdoptedProfileId).FirstOrDefaultAsync();
				return MethodAdoptedProfileId;
			}
			return (Guid)SwitchedMethodProfileId;
		}


		public async Task<bool> IspatientFollowUp(Guid PatientId, Guid PatientVisitId)
		{
			var _uowCounslingAndProvision = new UnitOfWork<CounslingAndProvision>(_uowClientFollowup.GetDbContext());
			var _PatientVisitId = await _uowCounslingAndProvision.Repository.GetALL(x => x.PatientId == PatientId).OrderByDescending(x => x.CreatedOn).Select(x => x.PatientVisitId).FirstOrDefaultAsync();
			if (AppCommonMethod.IsNullOrEmptyGuid(_PatientVisitId))
				return false;

			if (PatientVisitId == _PatientVisitId)
				return false;

			return true;
		}
		#endregion



		#region Helper Method
		private void FillEntityClientFollowup(ClientFollowup obj)
		{
			if (obj.FollowupClientId == Guid.Empty)
			{
				obj.FollowupClientId = Guid.NewGuid();
				obj.CreatedBy = _tokenService.GetUserId();
				obj.CreatedOn = DateTime.Now;
				obj.IsActive = true;
				obj.ActionTypeId = (int)ActionTypeEnum.Create;
			}
			else
			{
				obj.UpdatedBy = _tokenService.GetUserId();
				obj.UpdatedOn = DateTime.Now;
				obj.IsActive = true;
				obj.ActionTypeId = (int)ActionTypeEnum.Edit;
			}
		}
		#endregion

	}
}
