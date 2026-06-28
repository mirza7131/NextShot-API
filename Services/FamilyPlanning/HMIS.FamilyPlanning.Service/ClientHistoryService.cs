using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.FamilyPlanning.Domain.Models.DbModels;
using HMIS.FamilyPlanning.Domain.Models.DTO;
using HMIS.FamilyPlanning.Domain.Repositories.UOW;
using JWTAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profile = HMIS.FamilyPlanning.Domain.Models.DbModels.Profile;

namespace HMIS.FamilyPlanning.Service
{
	public class ClientHistoryService
	{

		#region Class Fields & Propertities
		private readonly TokenService _tokenService;
		private readonly IMapper _mapper;
		private UnitOfWork<RegistrationDetail> _uowRegistrationDetail;

		#endregion


		#region Constructor
		public ClientHistoryService(TokenService tokenService, IMapper mapper, UnitOfWork<RegistrationDetail> uowRegistrationDetail)
		{
			_tokenService = tokenService;
			_mapper = mapper;
			_uowRegistrationDetail = uowRegistrationDetail;
		}
		#endregion


		#region CUD


		public async Task CreateOrEditRegistrationDetail(RegistrationDetailDTO registrationDetailDTO)
		{
			if (AppCommonMethod.IsNullObject(registrationDetailDTO))
				throw new UserFriendlyException(CommonMessageConstant.DTOIsNUll);

			//if (AppCommonMethod.IsNullOrEmptyGuid(registrationDetailDTO.RegistrationDetailId))
			//{
			await CreateRegistrationDetail(registrationDetailDTO);
			//}
			//else
			//{
			//	await UpdateRegistrationDetail(registrationDetailDTO);
			//}
		}


		private async Task CreateRegistrationDetail(RegistrationDetailDTO registrationDetailDTO)
		{
			RegistrationDetail registrationDetail = new RegistrationDetail();
			registrationDetail = _mapper.Map<RegistrationDetail>(registrationDetailDTO);
			FillEntityRegistrationDetail(registrationDetail);
			await _uowRegistrationDetail.Repository.Insert(registrationDetail);
			await _uowRegistrationDetail.Save();
		}



		private async Task UpdateRegistrationDetail(RegistrationDetailDTO registrationDetailDTO)
		{
			var registrationDetail = await _uowRegistrationDetail.Repository.GetById(registrationDetailDTO.RegistrationDetailId);
			if (!AppCommonMethod.IsNullObject(registrationDetail))
			{
				// Map the DTO properties to the existing entity
				_mapper.Map(registrationDetailDTO, registrationDetail);
				FillEntityRegistrationDetail(registrationDetail);
				_uowRegistrationDetail.Repository.Update(registrationDetail);
				await _uowRegistrationDetail.Save();
			}
		}



		public async Task CreateOrEditPastHistory(PastHistoryDTO pastHistory)
		{
			if (AppCommonMethod.IsNullObject(pastHistory))
				throw new UserFriendlyException(CommonMessageConstant.DTOIsNUll);

			//if (AppCommonMethod.IsNullOrEmptyGuid(pastHistory.PastHistoryId))
			//{
			await CreatePastHistory(pastHistory);
			//}
			//else
			//{
			//	await UpdatePastHistory(pastHistory);
			//}
		}


		private async Task CreatePastHistory(PastHistoryDTO pastHistoryDTO)
		{
			var RegistrationDetailId = await _uowRegistrationDetail.Repository.GetALL(x => x.PatientId == pastHistoryDTO.PatientId).Select(x => x.RegistrationDetailId).FirstOrDefaultAsync();

			if (!AppCommonMethod.IsNullOrEmptyGuid(RegistrationDetailId))
			{
				var _uowPastHistory = new UnitOfWork<PastHistory>(_uowRegistrationDetail.GetDbContext());
				PastHistory pastHistory = new PastHistory();
				pastHistory = _mapper.Map<PastHistory>(pastHistoryDTO);
				pastHistory.RegistrationDetailId = RegistrationDetailId;
				if (pastHistory.IsPreviousUser == false)
				{
					pastHistory.MethodInUseProfileId = null;
				}
				FillEntityPastHistory(pastHistory);
				await _uowPastHistory.Repository.Insert(pastHistory);
				await _uowPastHistory.Save();
			}
		}


		private async Task UpdatePastHistory(PastHistoryDTO pastHistoryDTO)
		{
			var _uowPastHistory = new UnitOfWork<PastHistory>(_uowRegistrationDetail.GetDbContext());
			var pastHistory = await _uowPastHistory.Repository.GetById(pastHistoryDTO.PastHistoryId);
			if (!AppCommonMethod.IsNullObject(pastHistory))
			{
				_mapper.Map(pastHistoryDTO, pastHistory);
				if (pastHistory.IsPreviousUser == false)
				{
					pastHistory.MethodInUseProfileId = null;
				}
				FillEntityPastHistory(pastHistory);
				_uowPastHistory.Repository.Update(pastHistory);
				await _uowPastHistory.Save();
			}
		}



		public async Task CreateOrEditMedicalHistory(MedicalHistoryDTO medicalHistoryDTO)
		{
			if (AppCommonMethod.IsNullObject(medicalHistoryDTO))
				throw new UserFriendlyException(CommonMessageConstant.DTOIsNUll);

			//if (AppCommonMethod.IsNullOrEmptyGuid(medicalHistoryDTO.MedicalHistoryId))
			//{
			await CreateMedicalHistory(medicalHistoryDTO);
			//}
			//else
			//{
			//	await UpdateMedicalHistory(medicalHistoryDTO);
			//}
		}


		private async Task CreateMedicalHistory(MedicalHistoryDTO medicalHistoryDTO)
		{
			var RegistrationDetailId = await _uowRegistrationDetail.Repository.GetALL(x => x.PatientId == medicalHistoryDTO.PatientId).Select(x => x.RegistrationDetailId).FirstOrDefaultAsync();

			if (!AppCommonMethod.IsNullOrEmptyGuid(RegistrationDetailId))
			{
				var _uowMedicalHistory = new UnitOfWork<MedicalHistory>(_uowRegistrationDetail.GetDbContext());
				MedicalHistory medicalHistory = new MedicalHistory();
				medicalHistory = _mapper.Map<MedicalHistory>(medicalHistoryDTO);
				medicalHistory.RegistrationDetailId = RegistrationDetailId;
				FillEntityMedicalHistory(medicalHistory);
				await _uowMedicalHistory.Repository.Insert(medicalHistory);
				await _uowMedicalHistory.Save();
			}
		}


		private async Task UpdateMedicalHistory(MedicalHistoryDTO medicalHistoryDTO)
		{
			var _uowMedicalHistory = new UnitOfWork<MedicalHistory>(_uowRegistrationDetail.GetDbContext());
			var medicalHistory = await _uowMedicalHistory.Repository.GetById(medicalHistoryDTO.MedicalHistoryId);
			if (!AppCommonMethod.IsNullObject(medicalHistory))
			{
				_mapper.Map(medicalHistoryDTO, medicalHistory);

				FillEntityMedicalHistory(medicalHistory);
				_uowMedicalHistory.Repository.Update(medicalHistory);
				await _uowMedicalHistory.Save();
			}
		}



		public async Task CreateOrEditSurgicalHistory(SurgicalHistoryDTO surgicalHistoryDTO)
		{
			if (AppCommonMethod.IsNullObject(surgicalHistoryDTO))
				throw new UserFriendlyException(CommonMessageConstant.DTOIsNUll);

			if (AppCommonMethod.IsNullOrEmptyGuid(surgicalHistoryDTO.PreviousBirthProfileId))
				throw new UserFriendlyException(CommonMessageConstant.NullProfileId);

			var _uowProfile = new UnitOfWork<Profile>(_uowRegistrationDetail.GetDbContext());
			var previousBirth = await _uowProfile.Repository.GetALL(x => x.ProfileId == surgicalHistoryDTO.PreviousBirthProfileId)
												.Select(x => x.Name)
												.FirstOrDefaultAsync();

			if (string.IsNullOrEmpty(previousBirth))
				throw new UserFriendlyException(CommonMessageConstant.ProfileIdNotEntered);

			if (previousBirth == CommonStringConstant.Cesarean)
				if (AppCommonMethod.IsNullorZeroInt(surgicalHistoryDTO.NumberOfCsections))
					throw new UserFriendlyException(CommonMessageConstant.NoOfCesareanNotGiven);


			//if (AppCommonMethod.IsNullOrEmptyGuid(surgicalHistoryDTO.SurgicalHistoryId))
			//{
			await CreateSurgicalHistory(surgicalHistoryDTO);
			//}
			//else
			//{
			//	await UpdateSurgicalHistory(surgicalHistoryDTO);
			//}
		}


		private async Task CreateSurgicalHistory(SurgicalHistoryDTO surgicalHistoryDTO)
		{
			var RegistrationDetailId = await _uowRegistrationDetail.Repository.GetALL(x => x.PatientId == surgicalHistoryDTO.PatientId).Select(x => x.RegistrationDetailId).FirstOrDefaultAsync();

			if (!AppCommonMethod.IsNullOrEmptyGuid(RegistrationDetailId))
			{
				var _uowSurgicalHistory = new UnitOfWork<SurgicalHistory>(_uowRegistrationDetail.GetDbContext());
				SurgicalHistory surgicalHistory = new SurgicalHistory();
				surgicalHistory = _mapper.Map<SurgicalHistory>(surgicalHistoryDTO);
				surgicalHistory.RegistrationDetailId = RegistrationDetailId;
				FillEntitySurgicalHistory(surgicalHistory);
				await _uowSurgicalHistory.Repository.Insert(surgicalHistory);
				await _uowSurgicalHistory.Save();
			}
		}



		private async Task UpdateSurgicalHistory(SurgicalHistoryDTO surgicalHistoryDTO)
		{
			var _uowSurgicalHistory = new UnitOfWork<SurgicalHistory>(_uowRegistrationDetail.GetDbContext());

			var surgicalHistory = await _uowSurgicalHistory.Repository.GetById(surgicalHistoryDTO.SurgicalHistoryId);
			_mapper.Map(surgicalHistoryDTO, surgicalHistory);
			FillEntitySurgicalHistory(surgicalHistory);
			_uowSurgicalHistory.Repository.Update(surgicalHistory);
			await _uowSurgicalHistory.Save();
		}





		public async Task CreateOrEditExamination(ExaminationDTO examinationDTO)
		{
			if (AppCommonMethod.IsNullObject(examinationDTO))
				throw new UserFriendlyException(CommonMessageConstant.DTOIsNUll);

			//if (AppCommonMethod.IsNullOrEmptyGuid(examinationDTO.ExaminationId))
			//{
			await CreateExamination(examinationDTO);
			//}
			//else
			//{
			//	await updateExamination(examinationDTO);
			//}
		}


		private async Task CreateExamination(ExaminationDTO examinationDTO)
		{

			var _uowExaminationDetail = new UnitOfWork<ExaminationDetail>(_uowRegistrationDetail.GetDbContext());

			var RegistrationDetailId = await _uowRegistrationDetail.Repository.GetALL(x => x.PatientId == examinationDTO.PatientId).Select(x => x.RegistrationDetailId).FirstOrDefaultAsync();

			if (!AppCommonMethod.IsNullOrEmptyGuid(RegistrationDetailId))
			{
				var _uowExamination = new UnitOfWork<Examination>(_uowRegistrationDetail.GetDbContext());
				Examination examination = new Examination();

				examination.RegistrationDetailId = RegistrationDetailId;
				FillEntityExamination(examination);


				foreach (var profileId in examinationDTO.ExaminationProfileId!)
				{
					ExaminationDetail examinationDetail = new ExaminationDetail();
					examinationDetail.ExaminationId = examination.ExaminationId;
					examinationDetail.ExaminationProfileId = profileId;
					FillEntityExaminationDetail(examinationDetail);

					await _uowExaminationDetail.Repository.Insert(examinationDetail);
					await _uowExaminationDetail.Save();
				}

				await _uowExamination.Repository.Insert(examination);
				await _uowExamination.Save();
			}
			else
			{
				throw new UserFriendlyException(CommonMessageConstant.UserRegistrationNotDone);
			}
		}




		private async Task updateExamination(ExaminationDTO examinationDTO)
		{

			var _uowExaminationDetail = new UnitOfWork<ExaminationDetail>(_uowRegistrationDetail.GetDbContext());

			if (AppCommonMethod.IsNullOrEmptyGuid(examinationDTO.ExaminationId))
				throw new UserFriendlyException(CommonMessageConstant.NullProfileIdExamination);


			var examinationList = _uowExaminationDetail.Repository.GetALL(x => x.ExaminationId == examinationDTO.ExaminationId).ToList();
			foreach (var item in examinationList)
			{
				_uowExaminationDetail.Repository.Delete(item.ExaminationDetailId);
			}

			foreach (var profileId in examinationDTO.ExaminationProfileId!)
			{
				ExaminationDetail examinationDetail = new ExaminationDetail();
				examinationDetail.ExaminationId = (Guid)examinationDTO.ExaminationId;
				examinationDetail.ExaminationProfileId = profileId;
				FillEntityExaminationDetail(examinationDetail);

				await _uowExaminationDetail.Repository.Insert(examinationDetail);
				await _uowExaminationDetail.Save();
			}
		}

		#endregion


		#region Read Operations


		public async Task<RegistrationDetailDTO> GetRegistrationDetailById(Guid PatientId)
		{
			if (AppCommonMethod.IsNullOrEmptyGuid(PatientId))
				throw new UserFriendlyException(CommonMessageConstant.PatientIdIsNUll);

			var registrationDetailData = await _uowRegistrationDetail.Repository.GetALL(x => x.PatientId == PatientId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();
			return _mapper.Map<RegistrationDetailDTO>(registrationDetailData);
		}

		public async Task<PatientVital> GetVitalDetailById(Guid PatientId)
		{
			if (AppCommonMethod.IsNullOrEmptyGuid(PatientId))
				throw new UserFriendlyException(CommonMessageConstant.PatientIdIsNUll);

			var _uowPatientVitals = new UnitOfWork<PatientVital>(_uowRegistrationDetail.GetDbContext());
			var vitalsData = await _uowPatientVitals.Repository.GetALL(x => x.PatientId == PatientId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();
			return vitalsData;
		}

		public async Task<PastHistoryDTO> GetPatientPastHistoryById(Guid PatientId)
		{
			if (AppCommonMethod.IsNullOrEmptyGuid(PatientId))
				throw new UserFriendlyException(CommonMessageConstant.PatientIdIsNUll);


			var RegistrationDetailId = await _uowRegistrationDetail.Repository.GetALL(x => x.PatientId == PatientId).Select(x => x.RegistrationDetailId).FirstOrDefaultAsync();
			if (AppCommonMethod.IsNullOrEmptyGuid(RegistrationDetailId))
				throw new UserFriendlyException(CommonMessageConstant.UserRegistrationNotDone);

			var _uowPastHistory = new UnitOfWork<PastHistory>(_uowRegistrationDetail.GetDbContext());
			var pastHistoryData = await _uowPastHistory.Repository.GetALL(x => x.RegistrationDetailId == RegistrationDetailId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();
			return _mapper.Map<PastHistoryDTO>(pastHistoryData);
		}

		public async Task<MedicalHistoryDTO> GetPatientMedicalHistoryById(Guid PatientId)
		{
			if (AppCommonMethod.IsNullOrEmptyGuid(PatientId))
				throw new UserFriendlyException(CommonMessageConstant.PatientIdIsNUll);


			var RegistrationDetailId = await _uowRegistrationDetail.Repository.GetALL(x => x.PatientId == PatientId).Select(x => x.RegistrationDetailId).FirstOrDefaultAsync();
			if (AppCommonMethod.IsNullOrEmptyGuid(RegistrationDetailId))
				throw new UserFriendlyException(CommonMessageConstant.UserRegistrationNotDone);

			var _uowMedicalHistory = new UnitOfWork<MedicalHistory>(_uowRegistrationDetail.GetDbContext());
			var medicalHistory = await _uowMedicalHistory.Repository.GetALL(x => x.RegistrationDetailId == RegistrationDetailId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();
			return _mapper.Map<MedicalHistoryDTO>(medicalHistory);
		}



		public async Task<SurgicalHistoryDTO> GetPatientSurgicalHistoryById(Guid PatientId)
		{
			if (AppCommonMethod.IsNullOrEmptyGuid(PatientId))
				throw new UserFriendlyException(CommonMessageConstant.PatientIdIsNUll);


			var RegistrationDetailId = await _uowRegistrationDetail.Repository.GetALL(x => x.PatientId == PatientId).Select(x => x.RegistrationDetailId).FirstOrDefaultAsync();
			if (AppCommonMethod.IsNullOrEmptyGuid(RegistrationDetailId))
				throw new UserFriendlyException(CommonMessageConstant.UserRegistrationNotDone);

			var _uowSurgicalHistory = new UnitOfWork<SurgicalHistory>(_uowRegistrationDetail.GetDbContext());
			var surgicalHistory = await _uowSurgicalHistory.Repository.GetALL(x => x.RegistrationDetailId == RegistrationDetailId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();
			return _mapper.Map<SurgicalHistoryDTO>(surgicalHistory);
		}




		public async Task<List<Guid>> GetPatientExaminationById(Guid PatientId)
		{
			if (AppCommonMethod.IsNullOrEmptyGuid(PatientId))
				throw new UserFriendlyException(CommonMessageConstant.PatientIdIsNUll);


			var RegistrationDetailId = await _uowRegistrationDetail.Repository.GetALL(x => x.PatientId == PatientId).Select(x => x.RegistrationDetailId).FirstOrDefaultAsync();
			if (AppCommonMethod.IsNullOrEmptyGuid(RegistrationDetailId))
				throw new UserFriendlyException(CommonMessageConstant.UserRegistrationNotDone);

			var _uowExamination = new UnitOfWork<Examination>(_uowRegistrationDetail.GetDbContext());
			var examination = await _uowExamination.Repository.GetALL(x => x.RegistrationDetailId == RegistrationDetailId).OrderByDescending(x => x.CreatedOn).FirstOrDefaultAsync();
			if (AppCommonMethod.IsNullObject(examination))
				throw new UserFriendlyException(CommonMessageConstant.PatientExaminationNotDone);

			var _uowExaminationDetail = new UnitOfWork<ExaminationDetail>(_uowRegistrationDetail.GetDbContext());
			var examinationDetail = await _uowExaminationDetail.Repository.GetALL(x => x.ExaminationId == examination.ExaminationId).Select(x => x.ExaminationProfileId).ToListAsync();

			return examinationDetail;
		}


		public async Task<bool> IsPatientRegisteredForFPAsync(Guid patientId)
		{
			bool isRegistered = false;

			using (var conn = _uowRegistrationDetail.GetDbContext().Database.GetDbConnection())
			{
				await conn.OpenAsync(); // Open the connection

				string sqlQuery = "SELECT fp.IsPatientRegisteredForFP(@PatientId)";

				using (SqlCommand sqlComm = new SqlCommand(sqlQuery, (SqlConnection)conn))
				{
					sqlComm.CommandType = CommandType.Text;
					sqlComm.Parameters.AddWithValue("@PatientId", patientId);

					var result = await sqlComm.ExecuteScalarAsync();

					if (result != null && result != DBNull.Value)
					{
						isRegistered = (bool)result;
					}
				}

				conn.Close(); // Close the connection
			}

			return isRegistered;
		}


		#endregion



		#region Helper Methods

		private void FillEntityRegistrationDetail(RegistrationDetail obj)
		{
			if (obj.RegistrationDetailId == Guid.Empty)
			{
				obj.RegistrationDetailId = Guid.NewGuid();
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



		private void FillEntityPastHistory(PastHistory obj)
		{
			if (obj.PastHistoryId == Guid.Empty)
			{
				obj.PastHistoryId = Guid.NewGuid();
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

		private void FillEntityMedicalHistory(MedicalHistory obj)
		{
			if (obj.MedicalHistoryId == Guid.Empty)
			{
				obj.MedicalHistoryId = Guid.NewGuid();
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

		private void FillEntitySurgicalHistory(SurgicalHistory obj)
		{
			if (obj.SurgicalHistoryId == Guid.Empty)
			{
				obj.SurgicalHistoryId = Guid.NewGuid();
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
		private void FillEntityExamination(Examination obj)
		{
			if (obj.ExaminationId == Guid.Empty)
			{
				obj.ExaminationId = Guid.NewGuid();
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


		private void FillEntityExaminationDetail(ExaminationDetail obj)
		{
			if (obj.ExaminationDetailId == Guid.Empty)
			{
				obj.ExaminationDetailId = Guid.NewGuid();
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
		#endregion
	}
}
