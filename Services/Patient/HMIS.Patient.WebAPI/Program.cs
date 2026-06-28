using CommonDTOs.ResponseDTO;
using ExceptionHandling.CustomMiddlewares;
using JWTAuthentication;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using DbModel = HMIS.Patient.Domain.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using HMIS.Patient.Domain.Repositories.UOW;
using HMIS.Patient.Service;
using HMIS.Patient.Domain.Models.DbModels;
using HMIS.Patient.Domain.Models.DTO.PaginationDto;
using HMIS.Aggregator.API.Services;
using SMSSender;
using RedisCache;
using System.Diagnostics;
using FileHandler;
using HMIS.Aggregator.API;
using Microsoft.Extensions.Options;
using CommonMessages;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// ************ This is for Auth Service ************* //
#region Register Authentication Services 

builder.Services.AddTokenAuthentication(builder.Configuration);
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.TryAddScoped<TokenService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//CORS
var corsapp = "corsapp";
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


builder.Services.AddDbContext<DbModel.HmisAuthContext>(options =>
options.UseSqlServer(
          builder.Configuration.GetConnectionString("DefaultConnection")
         ));

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.InstanceName = builder.Configuration.GetSection("RedisCache:InstanceName").Value; // For Production "Prod-HMIS-Portal" , For Staging and Dev "Prod-HMIS-Portal"
    options.Configuration = $"{builder.Configuration.GetSection("RedisCache:Host").Value}:{builder.Configuration.GetSection("RedisCache:Port").Value},password={builder.Configuration.GetSection("RedisCache:Password").Value}";
});

#endregion

// Default Settings
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Register Custom Services 

builder.Services.TryAddSingleton<IRedisCacheService, RedisCacheService>();

#region Register Project Services 
//*********** UOW Registered ************** //

//builder.Services.TryAddScoped(typeof(PagedListDto<>));
//Services
builder.Services.TryAddScoped<PatientService<DbModel.Patient>>();
//builder.Services.TryAddScoped<PatientService<DbModel.PatientContactDetail>>();
builder.Services.TryAddScoped<PatientVitalService<PatientVital>>();
builder.Services.TryAddScoped<DashboardService>();
builder.Services.TryAddScoped<IPDDashboardService>();
builder.Services.TryAddScoped<MLCDashboardService>();
builder.Services.TryAddScoped<ParaplegicDashboardService>();
builder.Services.TryAddScoped<GetAllMlcQueDatum>();

builder.Services.TryAddScoped<PatientDiagnoseService<PatientDiagnose>>();

builder.Services.TryAddScoped<PatientDiagnoseTemplateService<PatientDiagnoseTemplate>>();
builder.Services.TryAddScoped<PatientOpenVisitService<PatientOpenVisit>>();
builder.Services.TryAddScoped<MedicineLookupService<MedicineLookup>>();
builder.Services.TryAddScoped<MedicineDispatchService<MedicineDispatch>>();
builder.Services.TryAddScoped<PatientWorkFlowLogService<PatientWorkFlowLog>>();
builder.Services.TryAddScoped<PatientDischargeDetailService<PatientDischargeDetail>>();
builder.Services.TryAddScoped<PatientPrescriptionService<PatientPrescription>>();
builder.Services.TryAddScoped<PatientDocumentService<PatientDocument>>();


builder.Services.TryAddScoped<EMRService>();
builder.Services.TryAddScoped<MIMSService>();
builder.Services.TryAddScoped<BASService>();
builder.Services.TryAddScoped<HttpClient>();
builder.Services.TryAddScoped<SMS>();
builder.Services.TryAddScoped<PatientVisitFlowService<PatientVisitFlow>>();
builder.Services.TryAddScoped<SectionProcedureService<SectionProcedure>>();
builder.Services.TryAddScoped<HealthWatchService>();
builder.Services.TryAddScoped<HealthCertificateService>();
builder.Services.TryAddScoped<MedicoLegalService>();
builder.Services.TryAddScoped<HealthCouncilService>();
builder.Services.TryAddScoped<TBScreeningService>();
builder.Services.TryAddScoped<AidsService>();
builder.Services.TryAddScoped<PatientAdmissionDetailService<PatientAdmissionDetail>>();
builder.Services.TryAddScoped<UploadFiles>();
builder.Services.TryAddScoped<ProfileTypeService>();
builder.Services.TryAddScoped<RequestForNadraVerfication>();
builder.Services.TryAddScoped<VerifiedPatientDataFromNADRAService>();
builder.Services.TryAddScoped<DentalService>();
builder.Services.TryAddScoped<CdcService>();
builder.Services.TryAddScoped<MimsMedicineDataService>();
builder.Services.TryAddScoped<AuthCommonService>();
builder.Services.TryAddScoped<MedicineAdvisedService>();
builder.Services.TryAddScoped<MedicineAdvisedRequisitionService<MedicineAdvisedRequisition>>();
builder.Services.TryAddScoped<EmergencyDoctorService>();
builder.Services.TryAddScoped<PatientDiagnoseProcedureService>();
builder.Services.TryAddScoped<NursingEventsService<NursingEvent>>();
builder.Services.TryAddScoped<NCDService>();

//UnitOfWork
builder.Services.TryAddScoped<UnitOfWork<Patient>>();
builder.Services.TryAddScoped<UnitOfWork<PatientAdditionalInfo>>();
builder.Services.TryAddScoped<UnitOfWork<DbModel.Profile>>();
builder.Services.TryAddScoped<UnitOfWork<PatientOpenVisit>>();
builder.Services.TryAddScoped<UnitOfWork<PatientVital>>();
builder.Services.TryAddScoped<UnitOfWork<PatientDiagnose>>();
builder.Services.TryAddScoped<UnitOfWork<PatientDiagnoseTemplate>>();
builder.Services.TryAddScoped<UnitOfWork<PatientPrescription>>();
builder.Services.TryAddScoped<UnitOfWork<PatientLabTest>>();
builder.Services.TryAddScoped<UnitOfWork<Person>>();
builder.Services.TryAddScoped<UnitOfWork<Tehsil>>();
builder.Services.TryAddScoped<UnitOfWork<Province>>();
builder.Services.TryAddScoped<UnitOfWork<User>>();
builder.Services.TryAddScoped<UnitOfWork<HealthFacilityStation>>();
builder.Services.TryAddScoped<UnitOfWork<MedicineLookup>>();
builder.Services.TryAddScoped<UnitOfWork<MedicineDispatch>>();
builder.Services.TryAddScoped<UnitOfWork<PatientLocationPrefix>>();
builder.Services.TryAddScoped<UnitOfWork<PatientWorkFlowLog>>();
builder.Services.TryAddScoped<UnitOfWork<DataBankService>>();
builder.Services.TryAddScoped<UnitOfWork<PatientVisitFlow>>();
builder.Services.TryAddScoped<UnitOfWork<SectionProcedure>>();
builder.Services.TryAddScoped<UnitOfWork<PatientAdmissionDetail>>();
builder.Services.TryAddScoped<UnitOfWork<PatientDischargeDetail>>();
builder.Services.TryAddScoped<UnitOfWork<DentalSterilizationRecord>>();
builder.Services.TryAddScoped<UnitOfWork<PatientDocument>>();
builder.Services.TryAddScoped<UnitOfWork<MimsMedicineDatum>>();
builder.Services.TryAddScoped<UnitOfWork<MimsGetMedicineResponse>>();
builder.Services.TryAddScoped<UnitOfWork<MimsMedicineIndentLog>>();
builder.Services.TryAddScoped<UnitOfWork<MimsMedicineIndentDetail>>();
builder.Services.TryAddScoped<UnitOfWork<MedicineAdvised>>();
builder.Services.TryAddScoped<UnitOfWork<MedicineAdvisedRequisition>>();
builder.Services.TryAddScoped<UnitOfWork<PatientDiagnoseProcedure>>();
builder.Services.TryAddScoped<UnitOfWork<GetAllMlcQueDatum>>();
builder.Services.TryAddScoped<UnitOfWork<NursingEvent>>();
#endregion

// ************ Common Configuration ************* //
#region  Common Configuration

#pragma warning disable ASP5001 // Type or member is obsolete
builder.Services.AddMvc()
.SetCompatibilityVersion(CompatibilityVersion.Latest)
  // ConfigureApiBehaviorOptions is an extention method of IMvcbuilder  
  // interface and is used to configure ApiBehaviorOptions.                                    
  //In a nutshell, ApiBehaviorOptions is shipped with .Net Core 2.1 and facilitates  
  // automatic model state validation, automatic parameter binding    
  //and much more usefull features.    
  .ConfigureApiBehaviorOptions(options => {
//InvalidModelStateResponseFactory is a Func delegate  
// and used to customize the error response.    
//It is exposed as property of ApiBehaviorOptions class  
// that is used to configure api behaviour.    
options.InvalidModelStateResponseFactory = actionContext => {
//CustomErrorResponse is method that gets model validation errors     
//using ActionContext creates customized response,  
// and converts invalid model state dictionary    

return CustomErrorResponse(actionContext);
};
})

.AddJsonOptions(o =>
{
o.JsonSerializerOptions.PropertyNamingPolicy = null;
o.JsonSerializerOptions.DictionaryKeyPolicy = null;
});
#pragma warning restore ASP5001 // Type or member is obsolete

builder.Services.Configure<FormOptions>(o => {
o.ValueLengthLimit = int.MaxValue;
o.MultipartBodyLengthLimit = int.MaxValue;
o.MemoryBufferThreshold = int.MaxValue;
});

#endregion


var app = builder.Build();

var environment = (!string.IsNullOrEmpty(builder.Configuration.GetSection("Environment").Value)) ? builder.Configuration.GetSection("Environment").Value : CommonStringConstant.ProductionEnvironment;

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
if (environment != CommonStringConstant.ProductionEnvironment) // use when Environment is not Production
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseSwagger();
//app.UseSwaggerUI();



// ************ This is for Auth Service ************* //
#region Register Authentication Services 

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.UseDeveloperExceptionPage();

#endregion

// ************ Custom Middleware ************* //
#region Custom Middleware

app.UseMiddleware<ExceptionHandlingMiddleware>();

#endregion

app.UseCors("corsapp");
app.MapControllers();
app.Run();


// Below method extracts model state errors and assigns to the properties of Custom class.    
BadRequestObjectResult CustomErrorResponse(ActionContext actionContext)
{
//BadRequestObjectResult is class found Microsoft.AspNetCore.Mvc and is inherited from ObjectResult.    
//Rest code is linq.    
return new BadRequestObjectResult(new ResponseValidationError()
{
data = actionContext.ModelState
 .Where(modelError => modelError.Value?.Errors.Count > 0)
 .Select(modelError => new Error
{
ErrorField = modelError.Key,
ErrorDescription = modelError.Value.Errors.FirstOrDefault().ErrorMessage ?? string.Empty
}).ToList()
});
}

public class Error
{
    public string ErrorField { get; set; }
    public string ErrorDescription { get; set; }
}