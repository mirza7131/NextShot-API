using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Models.Dto.PaginationDto;
using AuthDAL.Repositories;
using AuthDAL.Repositories.UOW;
using CommonDTOs.ResponseDTO;
using CommonMessages;
using ExceptionHandling.CustomMiddlewares;
using FileHandler;
using HealthFacilityBAL;
using HMIS.Aggregator.API;
using HMIS.Aggregator.API.Services;
using JWTAuthentication;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Newtonsoft.Json.Linq;
using RedisCache;
using SMSSender;
using System.Text.Json.Serialization;
using UserBAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// ************ This is for Auth Service ************* //
#region Register Authentication Services 

builder.Services.AddTokenAuthentication(builder.Configuration);
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.TryAddScoped<TokenService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.TryAddScoped<UploadFiles>();

//CORS
var corsapp = "corsapp";
builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Configuration.SetBasePath(builder.Environment.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{builder.Environment}.json", optional: true)
        .AddEnvironmentVariables();

builder.Services.AddDbContext<GamaContext>(options =>
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
//builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Register Custom Services 
//builder.Services.TryAddScoped<IRedisCacheService, RedisCacheService>();
//builder.Services.TryAddTransient<IRedisCacheService, RedisCacheService>();
builder.Services.TryAddSingleton<IRedisCacheService, RedisCacheService>();


#region Register Project Services 
//builder.Services.TryAddScoped(typeof(PagedListDto<>));
builder.Services.TryAddScoped<AuthRespository>();
builder.Services.TryAddScoped<AttachmentService<Attachment>>();
builder.Services.TryAddScoped<SMS>();
builder.Services.TryAddScoped<AuthService>();
//builder.Services.TryAddScoped<IntegratedDashboardService>();
builder.Services.TryAddScoped<FeatureService<Feature>>();
builder.Services.TryAddScoped<UserRepository<User>>();
builder.Services.TryAddScoped<UserService<User>>();
builder.Services.TryAddScoped<UserRoleRepository<UserRole>>();
builder.Services.TryAddScoped<UserRoleService<UserRole>>();
builder.Services.TryAddScoped<ProfileTypeRepository<ProfileType>>();
builder.Services.TryAddScoped<ProfileTypeService<ProfileType>>();
builder.Services.TryAddScoped<ProfileRepository<Profile>>();
builder.Services.TryAddScoped<ProfileService<Profile>>();
builder.Services.TryAddScoped<MenuRepository<Menu>>();
builder.Services.TryAddScoped<MenuService<Menu>>();
builder.Services.TryAddScoped<RoleRepository<Role>>();
builder.Services.TryAddScoped<RoleService<Role>>();
builder.Services.TryAddScoped<RoleMenuRepository<RoleMenu>>();
builder.Services.TryAddScoped<RoleMenuService<RoleMenu>>();
builder.Services.TryAddScoped<AuditLogRepository<AuditLog>>();
builder.Services.TryAddScoped<AuditLogService<AuditLog>>();
builder.Services.TryAddScoped<ErrorLogRepository<ErrorLog>>();
builder.Services.TryAddScoped<ErrorLogService<ErrorLog>>();
builder.Services.TryAddScoped<UserLogRepository<UserLog>>();
builder.Services.TryAddScoped<UserLogService<UserLog>>();
builder.Services.TryAddScoped<PatientService<Patient>>();
builder.Services.TryAddScoped<DataSyncToOfflineService<DataSyncToOffline>>();
builder.Services.TryAddScoped<EventService<Event>>();



builder.Services.TryAddScoped<ProvinceService<Province>>();
builder.Services.TryAddScoped<DivisionRepository<Division>>();
builder.Services.TryAddScoped<DivisionService<Division>>();
builder.Services.TryAddScoped<DistrictRepository<District>>();
builder.Services.TryAddScoped<DistrictService<District>>();
builder.Services.TryAddScoped<TehsilRepository<Tehsil>>();
builder.Services.TryAddScoped<TehsilService<Tehsil>>();
builder.Services.TryAddScoped<UnionCouncilService<UnionCouncil>>();
builder.Services.TryAddScoped<HealthFacilityRepository<HealthFacility>>();
builder.Services.TryAddScoped<HealthFacilityService<HealthFacility>>();
builder.Services.TryAddScoped<HealthFacilityCategoryService<HealthFacilityCategory>>();
builder.Services.TryAddScoped<HealthFacilityTypeService<HealthFacilityType>>();
builder.Services.TryAddScoped<HfLabTestConfigService<HfLabTestConfig>>();
builder.Services.TryAddScoped<LabTestService<LabTest>>();
builder.Services.TryAddScoped<LabTestDetailService<LabTestDetail>>();
builder.Services.TryAddScoped<HfDepartmentService<HfDepartment>>();
builder.Services.TryAddScoped<HfDepartmentSectionService<HfDepartmentSection>>();
builder.Services.TryAddScoped<DepartmentLookupService<DepartmentLookup>>();
builder.Services.TryAddScoped<SectionLookupService<SectionLookup>>();
builder.Services.TryAddScoped<HealthFacilityStationService<HealthFacilityStation>>();
builder.Services.TryAddScoped<HRService>();
builder.Services.TryAddScoped<HttpClient>();
builder.Services.TryAddScoped<SyncDataLogService<SyncDataLog>>();
builder.Services.TryAddScoped<OfflineVersionLogService<OfflineVersionLog>>();
builder.Services.TryAddScoped<DataSyncUtilityLogService<DataSyncUtilityLog>>();
builder.Services.TryAddScoped<MIMSService>();
builder.Services.TryAddScoped<MimsMedicineDataService>();
builder.Services.TryAddScoped<AuthCommonService>();
builder.Services.TryAddScoped<InvoiceService<InvoiceMaster>>();

//*********** UOW Registered ************** //

builder.Services.TryAddScoped<UnitOfWork<AuditLog>>();
builder.Services.TryAddScoped<UnitOfWork<ErrorLog>>();
builder.Services.TryAddScoped<UnitOfWork<UserLog>>();
builder.Services.TryAddScoped<UnitOfWork<Profile>>();
builder.Services.TryAddScoped<UnitOfWork<ProfileType>>();
builder.Services.TryAddScoped<UnitOfWork<Role>>();
builder.Services.TryAddScoped<UnitOfWork<Menu>>();
builder.Services.TryAddScoped<UnitOfWork<RoleMenu>>();
builder.Services.TryAddScoped<UnitOfWork<User>>();
builder.Services.TryAddScoped<UnitOfWork<UserRole>>();
builder.Services.TryAddScoped<UnitOfWork<Feature>>();
builder.Services.TryAddScoped<UnitOfWork<Attachment>>();
builder.Services.TryAddScoped<UnitOfWork<OfflineVersionLog>>();


builder.Services.TryAddScoped<UnitOfWork<Province>>();
builder.Services.TryAddScoped<UnitOfWork<Division>>();
builder.Services.TryAddScoped<UnitOfWork<District>>();
builder.Services.TryAddScoped<UnitOfWork<Tehsil>>();
builder.Services.TryAddScoped<UnitOfWork<UnionCouncil>>();
builder.Services.TryAddScoped<UnitOfWork<HealthFacility>>();
builder.Services.TryAddScoped<UnitOfWork<HealthFacilityCategory>>();
builder.Services.TryAddScoped<UnitOfWork<HealthFacilityType>>();
builder.Services.TryAddScoped<UnitOfWork<HealthFacilityStation>>();
builder.Services.TryAddScoped<UnitOfWork<HfDepartment>>();
builder.Services.TryAddScoped<UnitOfWork<HfDepartmentSection>>();

builder.Services.TryAddScoped<UnitOfWork<HfLabTestConfig>>();
builder.Services.TryAddScoped<UnitOfWork<LabTest>>();
builder.Services.TryAddScoped<UnitOfWork<LabTestDetail>>();
builder.Services.TryAddScoped<UnitOfWork<DepartmentLookup>>();
builder.Services.TryAddScoped<UnitOfWork<SectionLookup>>();
builder.Services.TryAddScoped<UnitOfWork<SyncDataLog>>();
builder.Services.TryAddScoped<UnitOfWork<DataSyncUtilityLog>>();
builder.Services.TryAddScoped<UnitOfWork<DataSyncToOffline>>();
builder.Services.TryAddScoped<UnitOfWork<MimsMedicineDatum>>();
builder.Services.TryAddScoped<UnitOfWork<Patient>>();
builder.Services.TryAddScoped<UnitOfWork<MimsGetMedicineResponse>>();
builder.Services.TryAddScoped<UnitOfWork<MimsMedicineIndentLog>>();
builder.Services.TryAddScoped<UnitOfWork<MimsMedicineIndentDetail>>();
builder.Services.TryAddScoped<UnitOfWork<Event>>();
builder.Services.TryAddScoped<UnitOfWork<InvoiceMaster>>();
builder.Services.TryAddScoped<UnitOfWork<InvoiceItemList>>();
builder.Services.TryAddScoped<UnitOfWork<InvoiceDocumentList>>();


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

#endregion

// ************ Custom Middleware ************* //
#region Custom Middleware

app.UseMiddleware<ExceptionHandlingMiddleware>();

#endregion

app.UseCors("corsapp");
app.UseStaticFiles();
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
