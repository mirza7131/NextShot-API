using CommonDTOs.ResponseDTO;
using ExceptionHandling.CustomMiddlewares;
using JWTAuthentication;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;
using HMIS.Aggregator.API;
using SMSSender;
using FileHandler;
using HMIS.EMC.Service;
using HMIS.EMC.Domain.Models.DbModels;
using HMIS.EMC.Domain.Repositories.UOW;
using HMIS.EMC.Service.Interfaces;
using CommonMessages;
using HMIS.EMC.Service.Interfaces.QrCodeHelper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// ************ This is for Auth Service ************* //
#region Register Authentication Services 

builder.Services.AddTokenAuthentication(builder.Configuration);
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.TryAddScoped<TokenService>();
builder.Services.TryAddScoped<PatientDiagnoseService>();
builder.Services.TryAddScoped<UploadFiles>();
builder.Services.TryAddScoped<AuthCommonService>();

//builder.Services.TryAddScoped<BarcodeHandler>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//CORS
var corsapp = "corsapp";
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


builder.Services.AddDbContext<HmisAuthContext>(options =>
options.UseSqlServer(
          builder.Configuration.GetConnectionString("DefaultConnection")
         ));


#endregion


// Default Settings
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Custom Services 
#region Register Project Services 
//*********** UOW Registered ************** //

//builder.Services.TryAddScoped(typeof(PagedListDto<>));
//Services
builder.Services.TryAddScoped<SMS>();
builder.Services.TryAddScoped<HttpClient>();
builder.Services.TryAddScoped<AuthCommonService>();


//UnitOfWork
//builder.Services.TryAddScoped<UnitOfWork<SampleConsignmentDetail>>();
builder.Services.TryAddScoped<UnitOfWork<IcvCertificate>>();
builder.Services.TryAddScoped<UnitOfWork<BirthCertificate>>();
builder.Services.TryAddScoped<UnitOfWork<Mlcpostmortem>>();
builder.Services.TryAddScoped<UnitOfWork<MlcbodyIdentifierInfo>>();
builder.Services.TryAddScoped<UnitOfWork<PostmortemExternalExamination>>();
builder.Services.TryAddScoped<UnitOfWork<PostMortemInternalExamination>>();
builder.Services.TryAddScoped<UnitOfWork<PostmortemReport>>();
builder.Services.TryAddScoped<UnitOfWork<MlebasicInfo>>();
builder.Services.TryAddScoped<UnitOfWork<PatientImage>>();
builder.Services.TryAddScoped<UnitOfWork<MlcsvinitialInfo>>();
builder.Services.TryAddScoped<UnitOfWork<Mlc>>();
builder.Services.TryAddScoped<UnitOfWork<DeathCertificate>>();
builder.Services.TryAddScoped<UnitOfWork<FitnessCertificate>>();
builder.Services.TryAddScoped<UnitOfWork<PatientDiagnose>>();
#endregion

//builder.Services.TryAddScoped<IcvCertificateService<IcvCertificate>>();

builder.Services.AddScoped<IicvCertificate, IcvCertificateService<IcvCertificate>>();
builder.Services.AddScoped<IBirthCertificate, BirthCertificateService<BirthCertificate>>();
builder.Services.AddScoped<IPostMortemForm, PostMortemFormService<Mlcpostmortem>>();
builder.Services.AddScoped<IPostMortemForm, PostMortemFormService<MlcbodyIdentifierInfo>>();
builder.Services.TryAddScoped<HomeApplianceService<FitnessCertificate>>();
builder.Services.AddScoped<IPostMortemForm, PostMortemFormService<PostmortemExternalExamination>>();
builder.Services.AddScoped<IPostMortemForm, PostMortemFormService<PostMortemInternalExamination>>();
builder.Services.AddScoped<IPostMortemForm, PostMortemFormService<PostmortemReport>>();
builder.Services.AddScoped<IMLE, MLEFormService<MlebasicInfo>>();
builder.Services.AddScoped<IMLESvForm, MLESVFormService<MlcsvinitialInfo>>();
builder.Services.AddScoped<IDeathCertificate, DeathCertificateFormService<DeathCertificate>>();
builder.Services.AddScoped<IFitnessCertificate, FitnessCertificateService<FitnessCertificate>>();

// Add Qr Code Service

builder.Services.AddScoped<IQrCodeGeneratorHelper, QrCodeGeneratorHelper>();

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