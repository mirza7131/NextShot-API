using AppCommonMethods;
using AutoMapper;
using CommonDTOs.Enums;
using CommonExceptionHandler;
using CommonMessages;
using HMIS.NCD.Domain.Models.DbModels;
using HMIS.NCD.Domain.Models.DTO;
using HMIS.NCD.Domain.Repositories._UOW;
using HMIS.NCD.Service.Interfaces;
using JWTAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.NCD.Service
{
    public class NcdAssessmentAnswerService<TEntity> : INcdAssessmentAnswers where TEntity : class
    {
        #region Class Fields & Properties
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<NcdAssessmentAnswer> _uowNcdAssessmentAnswer;
        #endregion

        #region Constructor
        public NcdAssessmentAnswerService(TokenService tokenService, IMapper mapper, UnitOfWork<NcdAssessmentAnswer> uowNcdAssessmentAnswer)
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowNcdAssessmentAnswer = uowNcdAssessmentAnswer;
        }
        #endregion

        #region Create Or Update Api
        public async Task<NcdAssessmentDto> CreateNcdAssessment(NcdAssessmentDto input)
        {
            try
            {
                if (!AppCommonMethod.IsNullOrEmptyList(input.PersonalHistory))
                {
                    foreach (var item in input.PersonalHistory)
                    {
                        var mappedObj = _mapper.Map<NcdAssessmentAnswer>(item);
                        mappedObj.NcdAssessmentAnswersId = Guid.Empty;
                        mappedObj.PatientId = input.PatientId;
                        mappedObj.PatientVisitId = input.PatientVisitId;
                        mappedObj.HealthFacilityId = input.HealthFacilityId;
                        FillEntity(mappedObj);
                        if (mappedObj.Answer == "YES")
                        {
                            mappedObj.Score = 1;
                        }
                        else
                        {
                            mappedObj.Score = 0;
                        }

                        // Save Personal History Answers
                        await _uowNcdAssessmentAnswer.Repository.Insert(mappedObj);
                        await _uowNcdAssessmentAnswer.CommitAsync();
                        // End Personal History

                        // Save Diabates Case
                        if (!AppCommonMethod.IsNullOrEmptyList(item.DiabatesCase))
                        {
                            foreach (var diabatesCase in item.DiabatesCase)
                            {
                                var mappedDiabObj = _mapper.Map<NcdAssessmentAnswer>(diabatesCase);
                                mappedDiabObj.NcdAssessmentAnswersId = Guid.Empty;
                                mappedDiabObj.PatientId = input.PatientId;
                                mappedDiabObj.PatientVisitId = input.PatientVisitId;
                                mappedDiabObj.HealthFacilityId = input.HealthFacilityId;
                                FillEntity(mappedDiabObj);
                                if (mappedDiabObj.Answer == "YES")
                                {
                                    mappedDiabObj.Score = 1;
                                }
                                else
                                {
                                    mappedDiabObj.Score = 0;
                                }

                                await _uowNcdAssessmentAnswer.Repository.Insert(mappedDiabObj);
                                await _uowNcdAssessmentAnswer.CommitAsync();
                            }
                        }
                        // End Diabates Case

                        // Save Copd Case
                        if (!AppCommonMethod.IsNullOrEmptyList(item.CopdCase))
                        {
                            foreach (var copdCase in item.CopdCase)
                            {
                                var mappedDiabObj = _mapper.Map<NcdAssessmentAnswer>(copdCase);
                                mappedDiabObj.NcdAssessmentAnswersId = Guid.Empty;
                                mappedDiabObj.PatientId = input.PatientId;
                                mappedDiabObj.PatientVisitId = input.PatientVisitId;
                                mappedDiabObj.HealthFacilityId = input.HealthFacilityId;
                                FillEntity(mappedDiabObj);
                                if (mappedDiabObj.Answer == "YES")
                                {
                                    mappedDiabObj.Score = 1;
                                }
                                else
                                {
                                    mappedDiabObj.Score = 0;
                                }



                                await _uowNcdAssessmentAnswer.Repository.Insert(mappedDiabObj);
                                await _uowNcdAssessmentAnswer.CommitAsync();
                            }
                        }
                        // End copd Case

                        // Save Asthma Case
                        if (!AppCommonMethod.IsNullOrEmptyList(item.AsthmaCase))
                        {
                            foreach (var asthmaCase in item.AsthmaCase)
                            {
                                var mappedDiabObj = _mapper.Map<NcdAssessmentAnswer>(asthmaCase);
                                mappedDiabObj.NcdAssessmentAnswersId = Guid.Empty;
                                mappedDiabObj.PatientId = input.PatientId;
                                mappedDiabObj.PatientVisitId = input.PatientVisitId;
                                mappedDiabObj.HealthFacilityId = input.HealthFacilityId;
                                FillEntity(mappedDiabObj);
                                if (mappedDiabObj.Answer == "YES")
                                {
                                    mappedDiabObj.Score = 1;
                                }
                                else
                                {
                                    mappedDiabObj.Score = 0;
                                }

                                await _uowNcdAssessmentAnswer.Repository.Insert(mappedDiabObj);
                                await _uowNcdAssessmentAnswer.CommitAsync();
                            }
                        }

                        // End Asthma Case
                    }
                }

                if (!AppCommonMethod.IsNullOrEmptyList(input.RiskAssessmentOfMentalHealth))
                {
                    foreach (var item in input.RiskAssessmentOfMentalHealth)
                    {
                        var mappedDiabObj = _mapper.Map<NcdAssessmentAnswer>(item);
                        mappedDiabObj.NcdAssessmentAnswersId = Guid.Empty;
                        mappedDiabObj.PatientId = input.PatientId;
                        mappedDiabObj.PatientVisitId = input.PatientVisitId;
                        mappedDiabObj.HealthFacilityId = input.HealthFacilityId;
                        FillEntity(mappedDiabObj);

                        await _uowNcdAssessmentAnswer.Repository.Insert(mappedDiabObj);
                        await _uowNcdAssessmentAnswer.CommitAsync();
                    }
                }



                //var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowNcdAssessmentAnswer.GetDbContext());
                //var patientVisitObj = await _uowPatientOpenVisit.Repository.GetById(input.PatientVisitId);
                //if (!AppCommonMethod.IsNullObject(patientVisitObj))
                //{
                //    var _uowSectionLookUp = new UnitOfWork<SectionLookup>(_uowNcdAssessmentAnswer.GetDbContext());
                //    //var patientSection =  await _uowSectionLookUp.Repository.GetALL(x => x.SectionLookupId == patientVisitObj.SectionLookupId).FirstOrDefaultAsync();


                //    if (input.IsNcdPositive == true && input.IsMuawinPositive == true)
                //    {
                //        int sectionId = await _uowSectionLookUp.Repository.GetALL(x => x.FormType == CommonStringConstant.NcdAndMuawinClinicForm).Select(x => x.SectionLookupId).FirstOrDefaultAsync();
                //        if (!AppCommonMethod.IsNullorZeroInt(sectionId))
                //        {
                //            patientVisitObj.ReferredDepartmentLookupId = patientVisitObj.DepartementLookupId;
                //            patientVisitObj.ReferredSectionLookupId = patientVisitObj.SectionLookupId;
                //            patientVisitObj.SectionLookupId = sectionId;
                //        }
                //    }

                //    else if (input.IsNcdPositive == true)
                //    {
                //        int sectionId = await _uowSectionLookUp.Repository.GetALL(x => x.FormType == CommonStringConstant.NCDClinicForm).Select(x => x.SectionLookupId).FirstOrDefaultAsync();
                //        if (!AppCommonMethod.IsNullorZeroInt(sectionId))
                //        {
                //            patientVisitObj.ReferredDepartmentLookupId = patientVisitObj.DepartementLookupId;
                //            patientVisitObj.ReferredSectionLookupId = patientVisitObj.SectionLookupId;
                //            patientVisitObj.SectionLookupId = sectionId;
                //        }
                //    }
                //    else if (input.IsMuawinPositive == true)
                //    {
                //        int sectionId = await _uowSectionLookUp.Repository.GetALL(x => x.FormType == CommonStringConstant.MuawinClinicsForm).Select(x => x.SectionLookupId).FirstOrDefaultAsync();
                //        if (!AppCommonMethod.IsNullorZeroInt(sectionId))
                //        {
                //            patientVisitObj.ReferredDepartmentLookupId = patientVisitObj.DepartementLookupId;
                //            patientVisitObj.ReferredSectionLookupId = patientVisitObj.SectionLookupId;
                //            patientVisitObj.SectionLookupId = sectionId;
                //        }
                //    }


                //    _uowPatientOpenVisit.Repository.Update(patientVisitObj);
                //    await _uowPatientOpenVisit.Save();

                //}


                return input;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<NcdAssessmentDto> UpdateNcdAssessment(NcdAssessmentDto input)
        {
            try
            {
                if (!AppCommonMethod.IsNullOrEmptyList(input.PersonalHistory))
                {
                    foreach (var item in input.PersonalHistory)
                    {
                        var obj = await _uowNcdAssessmentAnswer.GetDbContext().NcdAssessmentAnswers
                            .Where(x => x.NcdAssessmentAnswersId == item.NcdAssessmentAnswersId)
                            .FirstOrDefaultAsync();
                        var mappedObj = _mapper.Map(item, obj);
                        mappedObj.PatientId = input.PatientId;
                        mappedObj.PatientVisitId = input.PatientVisitId;
                        mappedObj.HealthFacilityId = input.HealthFacilityId;
                        FillEntity(mappedObj);
                        if (mappedObj.Answer == "YES")
                        {
                            mappedObj.Score = 1;

                            // Delete Diabates if Answer Yes
                            if (item.ShortName == "KCODBTES" && item.Answer == "YES")
                            {
                                DeleteAssessmentQuestionsIfYesForEdit(input, "KDBTSECSE");
                            }

                            // Delete Copd if Answer Yes
                            if (item.ShortName == "KCOCOPD" && item.Answer == "YES")
                            {
                                DeleteAssessmentQuestionsIfYesForEdit(input, "KWNCOPDCSE");
                            }

                            // Delete Asthma if Answer Yes
                            if (item.ShortName == "KCOASTMA" && item.Answer == "YES")
                            {
                                DeleteAssessmentQuestionsIfYesForEdit(input, "KWNASTCSE");
                            }
                        }
                        else
                        {
                            mappedObj.Score = 0;
                        }

                        // Save Personal History Answers
                        _uowNcdAssessmentAnswer.Repository.Update(mappedObj);
                        await _uowNcdAssessmentAnswer.CommitAsync();
                        // End Personal History

                        // Save Diabates Case
                        if (!AppCommonMethod.IsNullOrEmptyList(item.DiabatesCase))
                        {
                            foreach (var diabatesCase in item.DiabatesCase)
                            {
                                if (!AppCommonMethod.IsNullOrEmptyGuid(diabatesCase.NcdAssessmentAnswersId))
                                {
                                    var obj1 = await _uowNcdAssessmentAnswer.GetDbContext().NcdAssessmentAnswers
                                       .Where(x => x.NcdAssessmentAnswersId == diabatesCase.NcdAssessmentAnswersId)
                                       .FirstOrDefaultAsync();
                                    var mappedDiabObj = _mapper.Map(diabatesCase, obj1);
                                    mappedDiabObj.PatientId = input.PatientId;
                                    mappedDiabObj.PatientVisitId = input.PatientVisitId;
                                    mappedDiabObj.HealthFacilityId = input.HealthFacilityId;
                                    FillEntity(mappedDiabObj);

                                    if (mappedDiabObj.Answer == "YES")
                                    {
                                        mappedDiabObj.Score = 1;

                                    }
                                    else
                                    {
                                        mappedDiabObj.Score = 0;
                                    }

                                    _uowNcdAssessmentAnswer.Repository.Update(mappedDiabObj);
                                    await _uowNcdAssessmentAnswer.CommitAsync();
                                }
                                else
                                {
                                    var mappedDiabObj = _mapper.Map<NcdAssessmentAnswer>(diabatesCase);
                                    mappedDiabObj.PatientId = input.PatientId;
                                    mappedDiabObj.PatientVisitId = input.PatientVisitId;
                                    mappedDiabObj.HealthFacilityId = input.HealthFacilityId;
                                    FillEntity(mappedDiabObj);
                                    if (mappedDiabObj.Answer == "YES")
                                    {
                                        mappedDiabObj.Score = 1;
                                    }
                                    else
                                    {
                                        mappedDiabObj.Score = 0;
                                    }

                                    await _uowNcdAssessmentAnswer.Repository.Insert(mappedDiabObj);
                                    await _uowNcdAssessmentAnswer.CommitAsync();
                                }
                            }
                        }
                        // End Diabates Case

                        // Save Copd Case
                        if (!AppCommonMethod.IsNullOrEmptyList(item.CopdCase))
                        {
                            foreach (var copdCase in item.CopdCase)
                            {
                                if (!AppCommonMethod.IsNullOrEmptyGuid(copdCase.NcdAssessmentAnswersId))
                                {
                                    var obj1 = await _uowNcdAssessmentAnswer.GetDbContext().NcdAssessmentAnswers
                                       .Where(x => x.NcdAssessmentAnswersId == copdCase.NcdAssessmentAnswersId)
                                       .FirstOrDefaultAsync();
                                    var mappedDiabObj = _mapper.Map(copdCase, obj1);

                                    mappedDiabObj.PatientId = input.PatientId;
                                    mappedDiabObj.PatientVisitId = input.PatientVisitId;
                                    mappedDiabObj.HealthFacilityId = input.HealthFacilityId;
                                    FillEntity(mappedDiabObj);
                                    if (mappedDiabObj.Answer == "YES")
                                    {
                                        mappedDiabObj.Score = 1;

                                    }
                                    else
                                    {
                                        mappedDiabObj.Score = 0;
                                    }



                                    _uowNcdAssessmentAnswer.Repository.Update(mappedDiabObj);
                                    await _uowNcdAssessmentAnswer.CommitAsync();
                                }
                                else
                                {
                                    var mappedDiabObj = _mapper.Map<NcdAssessmentAnswer>(copdCase);
                                    mappedDiabObj.PatientId = input.PatientId;
                                    mappedDiabObj.PatientVisitId = input.PatientVisitId;
                                    mappedDiabObj.HealthFacilityId = input.HealthFacilityId;
                                    FillEntity(mappedDiabObj);
                                    if (mappedDiabObj.Answer == "YES")
                                    {
                                        mappedDiabObj.Score = 1;
                                    }
                                    else
                                    {
                                        mappedDiabObj.Score = 0;
                                    }



                                    await _uowNcdAssessmentAnswer.Repository.Insert(mappedDiabObj);
                                    await _uowNcdAssessmentAnswer.CommitAsync();
                                }
                            }
                        }
                        // End copd Case

                        // Save Asthma Case
                        if (!AppCommonMethod.IsNullOrEmptyList(item.AsthmaCase))
                        {
                            foreach (var asthmaCase in item.AsthmaCase)
                            {
                                if (!AppCommonMethod.IsNullOrEmptyGuid(asthmaCase.NcdAssessmentAnswersId))
                                {
                                    var obj1 = await _uowNcdAssessmentAnswer.GetDbContext().NcdAssessmentAnswers
                                       .Where(x => x.NcdAssessmentAnswersId == asthmaCase.NcdAssessmentAnswersId)
                                       .FirstOrDefaultAsync();
                                    var mappedDiabObj = _mapper.Map(asthmaCase, obj1);

                                    mappedDiabObj.PatientId = input.PatientId;
                                    mappedDiabObj.PatientVisitId = input.PatientVisitId;
                                    mappedDiabObj.HealthFacilityId = input.HealthFacilityId;
                                    FillEntity(mappedDiabObj);
                                    if (mappedDiabObj.Answer == "YES")
                                    {
                                        mappedDiabObj.Score = 1;
                                    }
                                    else
                                    {
                                        mappedDiabObj.Score = 0;
                                    }

                                    _uowNcdAssessmentAnswer.Repository.Update(mappedDiabObj);
                                    await _uowNcdAssessmentAnswer.CommitAsync();
                                }
                                else
                                {
                                    var mappedDiabObj = _mapper.Map<NcdAssessmentAnswer>(asthmaCase);
                                    mappedDiabObj.PatientId = input.PatientId;
                                    mappedDiabObj.PatientVisitId = input.PatientVisitId;
                                    mappedDiabObj.HealthFacilityId = input.HealthFacilityId;
                                    FillEntity(mappedDiabObj);
                                    if (mappedDiabObj.Answer == "YES")
                                    {
                                        mappedDiabObj.Score = 1;
                                    }
                                    else
                                    {
                                        mappedDiabObj.Score = 0;
                                    }

                                    await _uowNcdAssessmentAnswer.Repository.Insert(mappedDiabObj);
                                    await _uowNcdAssessmentAnswer.CommitAsync();
                                }
                            }
                        }

                        // End Asthma Case
                    }
                }

                if (!AppCommonMethod.IsNullOrEmptyList(input.RiskAssessmentOfMentalHealth))
                {
                    foreach (var item in input.RiskAssessmentOfMentalHealth)
                    {
                        var obj1 = await _uowNcdAssessmentAnswer.GetDbContext().NcdAssessmentAnswers
                                        .Where(x => x.NcdAssessmentAnswersId == item.NcdAssessmentAnswersId)
                                        .FirstOrDefaultAsync();
                        var mappedDiabObj = _mapper.Map(item, obj1);
                        mappedDiabObj.PatientId = input.PatientId;
                        mappedDiabObj.PatientVisitId = input.PatientVisitId;
                        mappedDiabObj.HealthFacilityId = input.HealthFacilityId;
                        FillEntity(mappedDiabObj);

                        _uowNcdAssessmentAnswer.Repository.Update(mappedDiabObj);
                        await _uowNcdAssessmentAnswer.CommitAsync();
                    }
                }




                //var _uowPatientOpenVisit = new UnitOfWork<PatientOpenVisit>(_uowNcdAssessmentAnswer.GetDbContext());
                //var patientVisitObj = await _uowPatientOpenVisit.Repository.GetById(input.PatientVisitId);
                //if (!AppCommonMethod.IsNullObject(patientVisitObj))
                //{
                //    var _uowSectionLookUp = new UnitOfWork<SectionLookup>(_uowNcdAssessmentAnswer.GetDbContext());
                //    //var patientSection =  await _uowSectionLookUp.Repository.GetALL(x => x.SectionLookupId == patientVisitObj.SectionLookupId).FirstOrDefaultAsync();


                //    if (input.IsNcdPositive == true && input.IsMuawinPositive == true)
                //    {
                //        int sectionId = await _uowSectionLookUp.Repository.GetALL(x => x.FormType == CommonStringConstant.NcdAndMuawinClinicForm).Select(x => x.SectionLookupId).FirstOrDefaultAsync();
                //        if (!AppCommonMethod.IsNullorZeroInt(sectionId))
                //        {
                //            patientVisitObj.ReferredDepartmentLookupId = patientVisitObj.DepartementLookupId;
                //            patientVisitObj.ReferredSectionLookupId = patientVisitObj.SectionLookupId;
                //            patientVisitObj.SectionLookupId = sectionId;
                //        }
                //    }

                //    else if (input.IsNcdPositive == true)
                //    {
                //        int sectionId = await _uowSectionLookUp.Repository.GetALL(x => x.FormType == CommonStringConstant.NCDClinicForm).Select(x => x.SectionLookupId).FirstOrDefaultAsync();
                //        if (!AppCommonMethod.IsNullorZeroInt(sectionId))
                //        {
                //            patientVisitObj.ReferredDepartmentLookupId = patientVisitObj.DepartementLookupId;
                //            patientVisitObj.ReferredSectionLookupId = patientVisitObj.SectionLookupId;
                //            patientVisitObj.SectionLookupId = sectionId;
                //        }
                //    }
                //    else if (input.IsMuawinPositive == true)
                //    {
                //        int sectionId = await _uowSectionLookUp.Repository.GetALL(x => x.FormType == CommonStringConstant.MuawinClinicsForm).Select(x => x.SectionLookupId).FirstOrDefaultAsync();
                //        if (!AppCommonMethod.IsNullorZeroInt(sectionId))
                //        {
                //            patientVisitObj.ReferredDepartmentLookupId = patientVisitObj.DepartementLookupId;
                //            patientVisitObj.ReferredSectionLookupId = patientVisitObj.SectionLookupId;
                //            patientVisitObj.SectionLookupId = sectionId;
                //        }
                //    }


                //    _uowPatientOpenVisit.Repository.Update(patientVisitObj);
                //    await _uowPatientOpenVisit.Save();

                //}

                return input;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }

        public async Task CreateNCDAssessmentQuestionAndAnswer(PatientAssessmentDTO assessmentDTO)
        {
            var _uowPersonalHistory = new UnitOfWork<PersonalHistory>(_uowNcdAssessmentAnswer.GetDbContext());
            var _uowPatientScore = new UnitOfWork<PatientScore>(_uowNcdAssessmentAnswer.GetDbContext());

            var _uowMentalHealthAssessment = new UnitOfWork<MentalHealthAssessment>(_uowNcdAssessmentAnswer.GetDbContext());
            var _uowMentalHealthPatientDetail = new UnitOfWork<MentalHealthPatientDetail>(_uowNcdAssessmentAnswer.GetDbContext());
            using (var trans = _uowNcdAssessmentAnswer.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    PersonalHistory personalHistory = new PersonalHistory();
                    FillEntityPersonalHistory(personalHistory);
                    foreach (var question in assessmentDTO.ncdAssessmentQuestionsAndAnswers)
                    {
                        if (AppCommonMethod.IsNullOrEmptyGuid(personalHistory.PatientId))
                        {
                            personalHistory.PatientId = assessmentDTO.PatientId;
                        }
                        if (AppCommonMethod.IsNullOrEmptyGuid(personalHistory.PatientVisitId))
                        {
                            personalHistory.PatientVisitId = assessmentDTO.PatientVisitId;
                        }
                        if (question.ShortName == CommonStringConstant.KnownCaseOfHypertension)
                        {
                            personalHistory.KnownCaseOfHypertension = question.Answer;
                        }
                        if (question.ShortName == CommonStringConstant.KnownCaseOfDiabetes)
                        {
                            personalHistory.KnownCaseOfDiabetes = question.Answer;
                            personalHistory.ScoreDiabetes = question.diabetesScore;
                            foreach (var diabetes in question.DiabatesCase)
                            {
                                PatientScore patientScore = new PatientScore();
                                patientScore.PatientId = assessmentDTO.PatientId;
                                patientScore.PatientVisitId = assessmentDTO.PatientVisitId;
                                patientScore.PatientHistoryId = personalHistory.PersonalHistoryId;
                                patientScore.PatientScoreQuestionId = diabetes.QuestionTypeProfileId;
                                patientScore.Value = diabetes.Answer;
                                patientScore.Score = diabetes.Score;
                                FillEntityPatientScore(patientScore);
                                await _uowPatientScore.Repository.Insert(patientScore);
                                await _uowPatientScore.Save();
                            }
                        }
                        if (question.ShortName == CommonStringConstant.KnownCaseOfAsthama)
                        {
                            personalHistory.KnownCaseOfAsthama = question.Answer;
                            personalHistory.ScoreAsthama = question.astamaScore;
                            foreach (var AsthmaCase in question.AsthmaCase)
                            {
                                PatientScore patientScore = new PatientScore();
                                patientScore.PatientId = assessmentDTO.PatientId;
                                patientScore.PatientVisitId = assessmentDTO.PatientVisitId;
                                patientScore.PatientHistoryId = personalHistory.PersonalHistoryId;
                                patientScore.PatientScoreQuestionId = AsthmaCase.QuestionTypeProfileId;
                                patientScore.Value = AsthmaCase.Answer;
                                patientScore.Score = AsthmaCase.Score;
                                FillEntityPatientScore(patientScore);
                                await _uowPatientScore.Repository.Insert(patientScore);
                                await _uowPatientScore.Save();
                            }
                        }
                        if (question.ShortName == CommonStringConstant.KnownCaseOfCOPD)
                        {
                            personalHistory.KnownCaseOfCopd = question.Answer;
                            personalHistory.ScoreCopd = question.copdScore;
                            foreach (var CopdCase in question.CopdCase)
                            {
                                PatientScore patientScore = new PatientScore();
                                patientScore.PatientId = assessmentDTO.PatientId;
                                patientScore.PatientVisitId = assessmentDTO.PatientVisitId;
                                patientScore.PatientHistoryId = personalHistory.PersonalHistoryId;
                                patientScore.PatientScoreQuestionId = CopdCase.QuestionTypeProfileId;
                                patientScore.Value = CopdCase.Answer;
                                patientScore.Score = CopdCase.Score;
                                FillEntityPatientScore(patientScore);
                                await _uowPatientScore.Repository.Insert(patientScore);
                                await _uowPatientScore.Save();
                            }
                        }
                        if (question.ShortName == CommonStringConstant.CSMTTENYEARS)
                        {
                            PatientScore patientScore = new PatientScore();
                            patientScore.PatientId = assessmentDTO.PatientId;
                            patientScore.PatientVisitId = assessmentDTO.PatientVisitId;
                            patientScore.PatientHistoryId = personalHistory.PersonalHistoryId;
                            patientScore.PatientScoreQuestionId = question.QuestionTypeProfileId;
                            patientScore.Value = question.Answer;
                            patientScore.Score = 0;
                            FillEntityPatientScore(patientScore);
                            await _uowPatientScore.Repository.Insert(patientScore);
                            await _uowPatientScore.Save();
                        }
                        if (question.ShortName == CommonStringConstant.STPNKIMTAD)
                        {
                            PatientScore patientScore = new PatientScore();
                            patientScore.PatientId = assessmentDTO.PatientId;
                            patientScore.PatientVisitId = assessmentDTO.PatientVisitId;
                            patientScore.PatientHistoryId = personalHistory.PersonalHistoryId;
                            patientScore.PatientScoreQuestionId = question.QuestionTypeProfileId;
                            patientScore.Value = question.Answer;
                            patientScore.Score = 0;
                            FillEntityPatientScore(patientScore);
                            await _uowPatientScore.Repository.Insert(patientScore);
                            await _uowPatientScore.Save();
                        }

                    }

                    await _uowPersonalHistory.Repository.Insert(personalHistory);
                    await _uowPersonalHistory.Save();




                    int DepressionScore = 0;
                    int AnxietyScore = 0;
                    foreach (var question in assessmentDTO.ncdRiskAssessmentQuestionsAndAnswers)
                    {

                        MentalHealthAssessment mentalHealthAssessment = new MentalHealthAssessment();
                        if (question.ShortName == CommonStringConstant.MentalHealthQuestion1)
                        {
                            DepressionScore += (int)question.DepressoinQuestion1Score;
                            mentalHealthAssessment.MentalType = "Depression";
                        }
                        if (question.ShortName == CommonStringConstant.MentalHealthQuestion2)
                        {
                            DepressionScore += (int)question.DepressoinQuestion2Score;
                            mentalHealthAssessment.MentalType = "Depression";
                        }
                        if (question.ShortName == CommonStringConstant.MentalHealthQuestion3)
                        {
                            AnxietyScore += (int)question.AnxityQuestion1Score;
                            mentalHealthAssessment.MentalType = "Anxeity";
                        }
                        if (question.ShortName == CommonStringConstant.MentalHealthQuestion4)
                        {
                            AnxietyScore += (int)question.AnxityQuestion2Score;
                            mentalHealthAssessment.MentalType = "Anxeity";
                        }


                        mentalHealthAssessment.PatinetId = assessmentDTO.PatientId;
                        mentalHealthAssessment.PatientVisitId = assessmentDTO.PatientVisitId;
                        mentalHealthAssessment.Question = question.MentalHealthQuestion;
                        mentalHealthAssessment.Answer = question.Answer;
                        mentalHealthAssessment.AssessmentType = "Health Risk Assessment";
                        mentalHealthAssessment.Status = true;


                        FillEntityMentalHealthAssessment(mentalHealthAssessment);
                        await _uowMentalHealthAssessment.Repository.Insert(mentalHealthAssessment);
                        await _uowMentalHealthAssessment.Save();

                    }


                    MentalHealthPatientDetail mentalHealthPatientDetail = new MentalHealthPatientDetail();
                    mentalHealthPatientDetail.PatientId = assessmentDTO.PatientId;
                    mentalHealthPatientDetail.PatientVisitId = assessmentDTO.PatientVisitId;
                    mentalHealthPatientDetail.HraAnxietyTotalScore = AnxietyScore;
                    mentalHealthPatientDetail.HraAnxietyRiskStatus = AnxietyScore >= 3 ? "Positive" : "Negative";
                    mentalHealthPatientDetail.HraDepressionTotalScore = DepressionScore;
                    mentalHealthPatientDetail.HraDepressionRiskStatus = DepressionScore >= 3 ? "Positive" : "Negative";
                    mentalHealthPatientDetail.HraDate = DateTime.Now;
                    FillEntityMentalHealthPatientDetail(mentalHealthPatientDetail);
                    await _uowMentalHealthPatientDetail.Repository.Insert(mentalHealthPatientDetail);
                    await _uowMentalHealthPatientDetail.Save();


                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
            }
        }

        public async Task UpdateNCDAssessmentQuestionAndAnswer(PersonalHistory data, PatientAssessmentDTO assessmentDTO)
        {
            var _uowPersonalHistory = new UnitOfWork<PersonalHistory>(_uowNcdAssessmentAnswer.GetDbContext());
            var _uowPatientScore = new UnitOfWork<PatientScore>(_uowNcdAssessmentAnswer.GetDbContext());
            //var data = await _uowPersonalHistory.Repository.GetALL(x => x.PatientVisitId == assessmentDTO.PatientVisitId).FirstOrDefaultAsync();
            var patientScore = await _uowPatientScore.Repository.GetALL(x => x.PatientVisitId == assessmentDTO.PatientVisitId).ToListAsync();


            using (var trans = _uowNcdAssessmentAnswer.GetDbContext().Database.BeginTransaction())
            {
                try
                {

                    if (!AppCommonMethod.IsNullOrEmptyList(patientScore))
                        foreach (var question in assessmentDTO.ncdAssessmentQuestionsAndAnswers)
                        {

                            if (question.ShortName == CommonStringConstant.KnownCaseOfHypertension)
                            {
                                data.KnownCaseOfHypertension = question.Answer;
                            }
                            if (question.ShortName == CommonStringConstant.KnownCaseOfDiabetes)
                            {
                                data.KnownCaseOfDiabetes = question.Answer;
                                data.ScoreDiabetes = question.diabetesScore;
                                foreach (var (diabetes, i) in question.DiabatesCase.Select((value, i) => (value, i)))
                                {
                                    if (patientScore[i].PatientScoreQuestionId == diabetes.QuestionTypeProfileId)
                                    {
                                        patientScore[i].Value = diabetes.Answer;
                                        patientScore[i].Score = diabetes.Score;
                                        FillEntityPatientScore(patientScore[i]);


                                        _uowPatientScore.Repository.Update(patientScore[i]);
                                        await _uowPatientScore.Save();
                                    }
                                }
                            }
                            if (question.ShortName == CommonStringConstant.KnownCaseOfAsthama)
                            {
                                data.KnownCaseOfAsthama = question.Answer;
                                data.ScoreAsthama = question.astamaScore;
                                foreach (var (AsthmaCase, i) in question.AsthmaCase.Select((value, i) => (value, i)))
                                {
                                    if (patientScore[i].PatientScoreQuestionId == AsthmaCase.QuestionTypeProfileId)
                                    {
                                        patientScore[i].Value = AsthmaCase.Answer;
                                        patientScore[i].Score = AsthmaCase.Score;
                                        FillEntityPatientScore(patientScore[i]);
                                        _uowPatientScore.Repository.Update(patientScore[i]);
                                        await _uowPatientScore.Save();
                                    }
                                }
                            }
                            if (question.ShortName == CommonStringConstant.KnownCaseOfCOPD)
                            {
                                data.KnownCaseOfCopd = question.Answer;
                                data.ScoreCopd = question.copdScore;

                                foreach (var (CopdCase, i) in question.CopdCase.Select((value, i) => (value, i)))
                                {

                                    if (patientScore[i].PatientScoreQuestionId == CopdCase.QuestionTypeProfileId)
                                    {
                                        patientScore[i].Value = CopdCase.Answer;
                                        patientScore[i].Score = CopdCase.Score;
                                        FillEntityPatientScore(patientScore[i]);
                                        _uowPatientScore.Repository.Update(patientScore[i]);
                                        await _uowPatientScore.Save();
                                    }
                                }
                            }
                            if (question.ShortName == CommonStringConstant.CSMTTENYEARS)
                            {
                                var index = patientScore.FindIndex(x => x.PatientScoreQuestionId == question.QuestionTypeProfileId);
                                patientScore[index].Value = question.Answer;
                                patientScore[index].Score = 0;
                                FillEntityPatientScore(patientScore[index]);
                                _uowPatientScore.Repository.Update(patientScore[index]);
                                await _uowPatientScore.Save();
                            }
                            if (question.ShortName == CommonStringConstant.STPNKIMTAD)
                            {

                                var index = patientScore.FindIndex(x => x.PatientScoreQuestionId == question.QuestionTypeProfileId);
                                patientScore[index].Value = question.Answer;
                                patientScore[index].Score = 0;
                                FillEntityPatientScore(patientScore[index]);
                                _uowPatientScore.Repository.Update(patientScore[index]);
                                await _uowPatientScore.Save();
                            }


                        }
                    _uowPersonalHistory.Repository.Update(data);
                    await _uowPersonalHistory.Save();

                    trans.Commit();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                }
            }


        }


        public async Task CreateOrEditNCDAssessmentQuestionAndAnswer(PatientAssessmentDTO assessmentDTO)
        {
            var _uowPersonalHistory = new UnitOfWork<PersonalHistory>(_uowNcdAssessmentAnswer.GetDbContext());
            var _uowPatientScore = new UnitOfWork<PatientScore>(_uowNcdAssessmentAnswer.GetDbContext());

            var data = await _uowPersonalHistory.Repository.GetALL(x => x.PatientVisitId == assessmentDTO.PatientVisitId).FirstOrDefaultAsync();
            if (AppCommonMethod.IsNullObject(data))
                await CreateNCDAssessmentQuestionAndAnswer(assessmentDTO);
            else
                await UpdateNCDAssessmentQuestionAndAnswer(data, assessmentDTO);


        }



        public async Task<MentalHealthPatientDetail> SaveNCDPsychologicalAssessment(PsychologicalAssessmentDTO assessmentDTO)
        {

            using (var trans = _uowNcdAssessmentAnswer.GetDbContext().Database.BeginTransaction())
            {
                try
                {
                    var _uowMentalHealthAssessment = new UnitOfWork<MentalHealthAssessment>(_uowNcdAssessmentAnswer.GetDbContext());
                    var _uowMentalHealthPatientDetail = new UnitOfWork<MentalHealthPatientDetail>(_uowNcdAssessmentAnswer.GetDbContext());
                    if (!AppCommonMethod.IsNullOrEmptyList(assessmentDTO?.AssessmentOfDepression))
                    {

                        //var assessmentOfDepression = await _uowMentalHealthAssessment.Repository.GetALL(x => x.PatientVisitId == assessmentDTO.PatientVisitId && x.AssessmentType == "Doctor Assessment").ToListAsync();
                        //if (AppCommonMethod.IsNullOrEmptyList(assessmentOfDepression))
                        //{
                        foreach (var item in assessmentDTO?.AssessmentOfDepression)
                        {
                            MentalHealthAssessment mentalHealthAssessment = new MentalHealthAssessment();
                            mentalHealthAssessment.Question = item.Name;
                            mentalHealthAssessment.Answer = item.Answer;
                            mentalHealthAssessment.PatinetId = assessmentDTO.PatientId;
                            mentalHealthAssessment.PatientVisitId = assessmentDTO.PatientVisitId;
                            mentalHealthAssessment.MentalType = "Depression";
                            mentalHealthAssessment.AssessmentType = "Doctor Assessment";
                            mentalHealthAssessment.Status = true;
                            FillEntityMentalHealthAssessment(mentalHealthAssessment);
                            await _uowMentalHealthAssessment.Repository.Insert(mentalHealthAssessment);
                            await _uowMentalHealthAssessment.Save();
                        }
                        //}
                        //else
                        //{
                        //    foreach (var value in assessmentOfDepression)
                        //    {
                        //        foreach (var item in assessmentDTO?.AssessmentOfDepression)
                        //        {
                        //            if (item.Name == value.Question)
                        //            {
                        //                value.Question = item.Name;
                        //                value.Answer = item.Answer;
                        //                FillEntityMentalHealthAssessment(value);
                        //                _uowMentalHealthAssessment.Repository.Update(value);
                        //                await _uowMentalHealthAssessment.Save();
                        //            }
                        //        }
                        //    }
                        //}
                    }

                    if (!AppCommonMethod.IsNullOrEmptyList(assessmentDTO?.AssessmentOfAxniety))
                    {
                        //var assessmentOfAnxeity = await _uowMentalHealthAssessment.Repository.GetALL(x => x.PatientVisitId == assessmentDTO.PatientVisitId && x.MentalType == "Anxeity").ToListAsync();
                        //if (AppCommonMethod.IsNullOrEmptyList(assessmentOfAnxeity))
                        //{
                        foreach (var item in assessmentDTO?.AssessmentOfAxniety)
                        {
                            MentalHealthAssessment mentalHealthAssessment = new MentalHealthAssessment();
                            mentalHealthAssessment.Question = item.Name;
                            mentalHealthAssessment.Answer = item.Answer;
                            mentalHealthAssessment.PatinetId = assessmentDTO.PatientId;
                            mentalHealthAssessment.PatientVisitId = assessmentDTO.PatientVisitId;
                            mentalHealthAssessment.MentalType = "Anxeity";
                            mentalHealthAssessment.AssessmentType = "Doctor Assessment";
                            mentalHealthAssessment.Status = true;
                            FillEntityMentalHealthAssessment(mentalHealthAssessment);
                            await _uowMentalHealthAssessment.Repository.Insert(mentalHealthAssessment);
                            await _uowMentalHealthAssessment.Save();
                        }
                        //}
                        //else
                        //{
                        //    foreach (var value in assessmentOfAnxeity)
                        //    {
                        //        foreach (var item in assessmentDTO?.AssessmentOfAxniety)
                        //        {
                        //            if (item.Name == value.Question)
                        //            {
                        //                value.Question = item.Name;
                        //                value.Answer = item.Answer;
                        //                FillEntityMentalHealthAssessment(value);
                        //                _uowMentalHealthAssessment.Repository.Update(value);
                        //                await _uowMentalHealthAssessment.Save();
                        //            }
                        //        }
                        //    }
                        //}
                    }


                    //MentalHealthPatientDetail mentalHealthPatientDetail = new MentalHealthPatientDetail();
                    var mentalHealthPatientDetail = await _uowMentalHealthPatientDetail.Repository.GetALL(x => x.PatientVisitId == assessmentDTO!.PatientVisitId).FirstOrDefaultAsync();
                    if (!AppCommonMethod.IsNullObject(mentalHealthPatientDetail))
                    {
                        mentalHealthPatientDetail.DaAnxietyTotalScore = assessmentDTO.AnxietyScore; ;
                        mentalHealthPatientDetail.DaDepressionTotalScore = assessmentDTO.DepressionScore; ;

                        if (mentalHealthPatientDetail.DaAnxietyTotalScore >= 0 && mentalHealthPatientDetail.DaAnxietyTotalScore <= 4)
                            mentalHealthPatientDetail.DaAnxietyRiskStatus = "No Anxiety";
                        else if (mentalHealthPatientDetail.DaAnxietyTotalScore >= 5 && mentalHealthPatientDetail.DaAnxietyTotalScore <= 9)
                            mentalHealthPatientDetail.DaAnxietyRiskStatus = "Mild Anxiety";
                        else if (mentalHealthPatientDetail.DaAnxietyTotalScore >= 10 && mentalHealthPatientDetail.DaAnxietyTotalScore <= 19)
                            mentalHealthPatientDetail.DaAnxietyRiskStatus = "Moderate Anxiety";
                        else if (mentalHealthPatientDetail.DaAnxietyTotalScore >= 20)
                            mentalHealthPatientDetail.DaAnxietyRiskStatus = "Severe Anxiety";
                        mentalHealthPatientDetail.DaAnxietyDate = DateTime.Now;
                        mentalHealthPatientDetail.DaAnxietyMessage = "Your Anxiety Assessment Status: - " + mentalHealthPatientDetail.DaAnxietyRiskStatus;
                        mentalHealthPatientDetail.DaAnxietyCreatedBy = _tokenService.GetUserId();



                        if (mentalHealthPatientDetail.DaDepressionTotalScore >= 0 && mentalHealthPatientDetail.DaDepressionTotalScore <= 4)
                            mentalHealthPatientDetail.DaDepressionRiskStatus = "No Depression";
                        else if (mentalHealthPatientDetail.DaDepressionTotalScore >= 5 && mentalHealthPatientDetail.DaDepressionTotalScore <= 9)
                            mentalHealthPatientDetail.DaDepressionRiskStatus = "Mild Depression";
                        else if (mentalHealthPatientDetail.DaDepressionTotalScore >= 10 && mentalHealthPatientDetail.DaDepressionTotalScore <= 19)
                            mentalHealthPatientDetail.DaDepressionRiskStatus = "Moderate Depression";
                        else if (mentalHealthPatientDetail.DaDepressionTotalScore >= 20)
                            mentalHealthPatientDetail.DaDepressionRiskStatus = "Severe Depression";
                        mentalHealthPatientDetail.DaDepressionDate = DateTime.Now;
                        mentalHealthPatientDetail.DaDepressionMessage = "Your Depression Assessment Status: - " + mentalHealthPatientDetail.DaDepressionRiskStatus;
                        mentalHealthPatientDetail.DaDepressionCreatedBy = _tokenService.GetUserId();


                        FillEntityMentalHealthPatientDetail(mentalHealthPatientDetail);
                        _uowMentalHealthPatientDetail.Repository.Update(mentalHealthPatientDetail);
                        await _uowMentalHealthPatientDetail.Save();


                        trans.Commit();

                    }
                    return mentalHealthPatientDetail;
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw ex;
                }
            }

        }
        #endregion

        #region GET API
        public async Task<List<SingleNcdAsssessmentRecord>> GetSingleNcdAsssessmentRecord(Guid? PatientVisitId)
        {

            using (var db = new HmisAuthContext())
            {
                var conn = _uowNcdAssessmentAnswer.GetDbContext().Database.GetDbConnection();
                try
                {
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("[ncd].[SpGetNcdAssessmentQuestionRecord]", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@Activity", "ById");
                    sqlComm.Parameters.AddWithValue("@PatientVisitId", PatientVisitId);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    var lst = ds.Tables[0].ToList<SingleNcdAsssessmentRecord>();


                    return lst;
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public async Task<NCDPatientAssessmentAnswersDTO> GetNcdAsssessmentAnswers(Guid? PatientVisitId)
        {
            NCDPatientAssessmentAnswersDTO nCDPatientAssessmentAnswers = new NCDPatientAssessmentAnswersDTO();
            var _uowPersonalHostory = new UnitOfWork<PersonalHistory>(_uowNcdAssessmentAnswer.GetDbContext());
            var personalData = await _uowPersonalHostory.Repository.GetALL(x => x.PatientVisitId == PatientVisitId).FirstOrDefaultAsync();

            //if (AppCommonMethod.IsNullObject(personalData))
            //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            nCDPatientAssessmentAnswers.personalHistory = personalData;
            nCDPatientAssessmentAnswers.patientFamiliyHistory = await _uowNcdAssessmentAnswer.GetDbContext().ViewNcdPatientFamiliyHistories
                .Where(x => x.PatientVisitId == PatientVisitId)
                .ToListAsync();

            return nCDPatientAssessmentAnswers;
        }


        public async Task<MentalHealthPatientDetail> GetPatinetLastVisitScore(Guid? PatientVisitId)
        {
            var _uowMentalHealthPatientDetail = new UnitOfWork<MentalHealthPatientDetail>(_uowNcdAssessmentAnswer.GetDbContext());
            var dataObj = await _uowMentalHealthPatientDetail.Repository.GetALL(x => x.PatientVisitId == PatientVisitId).FirstOrDefaultAsync();

            //if (AppCommonMethod.IsNullObject(personalData))
            //    throw new UserFriendlyException(CommonMessageConstant.RecordNotFound);

            return dataObj;
        }



        public async Task<dynamic> GetPatientLastVisitIdForNcdAssesmentQuestions(Guid PatientId, string FormType)
        {
            if (FormType == CommonStringConstant.NCDClinicForm)
            {
                var _uowPersonalHistory = new UnitOfWork<PersonalHistory>(_uowNcdAssessmentAnswer.GetDbContext());

                var lastVisitID = await _uowPersonalHistory.Repository.GetALL(x => x.PatientId == PatientId).OrderByDescending(x => x.CreatedOn)
                    .Select(x => x.PatientVisitId).FirstOrDefaultAsync();
                if (AppCommonMethod.IsNullOrEmptyGuid(lastVisitID))
                    return Guid.Empty;
                return (Guid)lastVisitID;
            }
            else
            {
                var userObj = TokenService.GetUserLoggedInfo();
                var _uowMentalHealthPatientDetail = new UnitOfWork<MentalHealthPatientDetail>(_uowNcdAssessmentAnswer.GetDbContext());
                var dataObj = await _uowNcdAssessmentAnswer.GetDbContext().ViewPatientLastAssessments.Where(x => x.PatinetId == PatientId)
                    .WhereIf(userObj!.IsDoctor, x => x.AssessmentType == "Doctor Assessment")
                    .OrderByDescending(x => x.Createdon).FirstOrDefaultAsync();
                if (AppCommonMethod.IsNullObject(dataObj)) return null;
                if (AppCommonMethod.IsNullOrEmptyGuid(dataObj!.PatientVisitId))
                    return null;
                return dataObj;
            }

        }
        #endregion

        #region Helper Method

        private void DeleteAssessmentQuestionsIfYesForEdit(NcdAssessmentDto input, string ShortName)
        {
            //then delete all diabates case questions answers
            var existingProfileType = _uowNcdAssessmentAnswer.GetDbContext().ProfileTypes.Where(x => x.ShortName == ShortName && x.ActionTypeId != 3).FirstOrDefault();

            if (!AppCommonMethod.IsNullObject(existingProfileType))
            {

                var existingDiabatesCaseQuestion = _uowNcdAssessmentAnswer.GetDbContext().Profiles.Where(x => x.ProfileTypeId == existingProfileType!.ProfileTypeId && x.ActionTypeId != 3).ToList();
                if (!AppCommonMethod.IsNullOrEmptyList(existingDiabatesCaseQuestion))
                {
                    foreach (var existing in existingDiabatesCaseQuestion)
                    {
                        var rowToDelete = _uowNcdAssessmentAnswer.GetDbContext().NcdAssessmentAnswers.Where(x => x.QuestionTypeProfileId == existing.ProfileId && x.PatientVisitId == input.PatientVisitId && x.ActionTypeId != 3).FirstOrDefault();

                        if (!AppCommonMethod.IsNullObject(rowToDelete))
                        {
                            rowToDelete.ActionTypeId = (int)ActionTypeEnum.Deleted;
                            rowToDelete.IsActive = false;
                            rowToDelete.DeletedOn = DateTime.Now;
                            rowToDelete.DeletedBy = _tokenService.GetUserId();

                            _uowNcdAssessmentAnswer.Repository.Update(rowToDelete);
                            _uowNcdAssessmentAnswer.GetDbContext().SaveChanges();
                        }
                    }
                }
            }
        }
        private void FillEntity(NcdAssessmentAnswer obj)
        {
            if (obj.NcdAssessmentAnswersId == Guid.Empty)
            {
                obj.NcdAssessmentAnswersId = Guid.NewGuid();
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



        private void FillEntityPersonalHistory(PersonalHistory obj)
        {
            if (obj.PersonalHistoryId == Guid.Empty)
            {
                obj.PersonalHistoryId = Guid.NewGuid();
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



        private void FillEntityPatientScore(PatientScore obj)
        {
            if (obj.PatientScoreId == Guid.Empty)
            {
                obj.PatientScoreId = Guid.NewGuid();
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

        private void FillEntityMentalHealthAssessment(MentalHealthAssessment obj)
        {
            if (obj.MentalHealthAssessmentId == Guid.Empty)
            {
                obj.MentalHealthAssessmentId = Guid.NewGuid();
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
        private void FillEntityMentalHealthPatientDetail(MentalHealthPatientDetail obj)
        {
            if (obj.MentalHealthPatientDetailId == Guid.Empty)
            {
                obj.MentalHealthPatientDetailId = Guid.NewGuid();
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
