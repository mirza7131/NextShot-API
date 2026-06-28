using CommonDTOs.ResponseDTO;
using CommonMessages;
using ExceptionHandling.CustomMiddlewares;
using JWTAuthentication;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using HMIS.Dashboard.Domain.Repositories.UOW;
using RedisCache;
using HMIS.Dashboard.Domain.Models.DbModels;
using HMIS.Dashboard.Services;
using HMIS.Dashboard.Service;
using HMIS.Aggregator.API.Services;
using TransDbModels = HMIS.Dashboard.Domain.Models.TransDbModels;
using SMSSender;

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


builder.Services.AddDbContext<HmisRepContext>(options =>
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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Custom Services 

builder.Services.TryAddSingleton<IRedisCacheService, RedisCacheService>();

#region Register Project Services 
//*********** UOW Registered ************** //
builder.Services.TryAddScoped<DashboardService>();
builder.Services.TryAddScoped<MobileAppService>();
builder.Services.TryAddScoped<DashboardSyncService>();
builder.Services.TryAddScoped<DashboardTransService>();

builder.Services.TryAddScoped<UnitOfWork<Patient>>();
builder.Services.TryAddScoped<UnitOfWork<PatientAdditionalInfo>>();
builder.Services.TryAddScoped<UnitOfWork<PatientOpenVisit>>();
builder.Services.TryAddScoped<UnitOfWork<PatientVital>>();
builder.Services.TryAddScoped<UnitOfWork<PatientDiagnose>>();
builder.Services.TryAddScoped<IPDDashboardService>();
builder.Services.TryAddScoped<MLCDashboardService>();
builder.Services.TryAddScoped<ParaplegicDashboardService>();


builder.Services.TryAddScoped<MIMSService>();
builder.Services.TryAddScoped<BASService>();
builder.Services.TryAddScoped<HttpClient>();
builder.Services.TryAddScoped<HealthWatchService>();
builder.Services.TryAddScoped<HealthCertificateService>();
builder.Services.TryAddScoped<MedicoLegalService>();
builder.Services.TryAddScoped<HealthCouncilService>();
builder.Services.TryAddScoped<TBScreeningService>();
builder.Services.TryAddScoped<AidsService>();
builder.Services.TryAddScoped<SMS>();
//UnitOfWork
builder.Services.TryAddScoped<UnitOfWork<PatientAdditionalInfo>>();
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

builder.Services.TryAddScoped<TransUnitOfWork<TransDbModels.Patient>>();
builder.Services.TryAddScoped<TransUnitOfWork<TransDbModels.PatientDiagnose>>();
builder.Services.TryAddScoped<TransUnitOfWork<TransDbModels.MimsMedicineDatum>>();
builder.Services.TryAddScoped<TransUnitOfWork<TransDbModels.PatientOpenVisit>>();
//builder.Services.TryAddScoped(typeof(PagedListDto<>));
//Services
//builder.Services.TryAddScoped<PatientService<DbModel.Patient>>();

builder.Services.TryAddScoped<HttpClient>();

//UnitOfWork
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