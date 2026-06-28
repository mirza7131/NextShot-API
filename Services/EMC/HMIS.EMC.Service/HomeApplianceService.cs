using AutoMapper;
using JWTAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMIS.EMC.Domain.Repositories.UOW;
using HMIS.EMC.Domain.Models.Dto;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.EntityFrameworkCore;
using HMIS.EMC.Domain.Models.DbModels;
using AppCommonMethods;

namespace HMIS.EMC.Service
{
    public class HomeApplianceService<TEntity> where TEntity : class
    {
        #region Class Fields & Properties
        private readonly TokenService _tokenService;
        private readonly IMapper _mapper;
        private UnitOfWork<FitnessCertificate> _uowFitnessCertificate;
        #endregion

        #region Constructor
        public HomeApplianceService(TokenService tokenService, IMapper mapper,
            UnitOfWork<FitnessCertificate> uowFitnessCertificate
            , PatientDiagnoseService patientDiagnoseService
            )
        {
            _tokenService = tokenService;
            _mapper = mapper;
            _uowFitnessCertificate = uowFitnessCertificate;
        }
        #endregion

        #region Get
        public async Task<GetSingleFitnessCertificateDtoWithQuestion> Get(string Cnic)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowFitnessCertificate.GetDbContext().Database.GetDbConnection();
                try
                {
                    GetSingleFitnessCertificateDtoWithQuestion fitnessDto = new GetSingleFitnessCertificateDtoWithQuestion();

                    // Create DataSet to hold stored procedure result
                    DataSet ds = new DataSet();

                    SqlCommand sqlComm = new SqlCommand("[emc].[SpGetFitnessCertificateRecordForHomeAppliance]", (SqlConnection)conn);
                    sqlComm.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlComm.Parameters.AddWithValue("@CNIC", Cnic);

                    conn.Open();

                    // Create DataAdapter to fill DataSet
                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));

                    // Convert FitnessCertificateDto data
                    var singleRecord = ds.Tables[0].ToList<FitnessCertificateHomeAppliance>();
                    fitnessDto.fitnessRecord = singleRecord[0];
                    if (!AppCommonMethod.IsNullObject(fitnessDto.fitnessRecord))
                    {
                        // Convert PsychologicalQuestionsAndAnswer to DataTable
                        DataTable psychologyDataTable = ds.Tables[1];

                        // Assign DataTable to List<PsychologyDto>
                        List<PsychologyDto> psychologyList = new List<PsychologyDto>();
                        foreach (DataRow row in psychologyDataTable.Rows)
                        {
                            if (row != null)
                            {
                                psychologyList.Add(new PsychologyDto
                                {
                                    // Map properties from DataTable columns
                                    Question = row["Question"].ToString(),
                                    Answer = row["Answer"].ToString()
                                    // Add more properties if needed
                                });
                            }
                        }

                        // Assign List<PsychologyDto> to fitness.PsychologicalQuestionsAndAnswer
                        fitnessDto.PsychologicalQuestionsAndAnswer = psychologyList;

                        return fitnessDto;
                    }
                    else
                        throw new Exception("Record Not Found..!");

                }
                catch (Exception)
                {
                    throw new Exception("Record Not Found..!");
                }
                finally
                {
                    conn.Close();
                }
            }
            #endregion
        }
    }
}
