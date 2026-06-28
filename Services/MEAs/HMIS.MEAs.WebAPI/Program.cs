using CommonDTOs.ResponseDTO;
using CommonMessages;
using ExceptionHandling.CustomMiddlewares;
using HMIS.Aggregator.API.Services;
using HMIS.MEAs.Domain.Models.DbModels;
using HMIS.MEAs.Service;
using HMIS.MEAs.Domain.Repositories._UOW;
using JWTAuthentication;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

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


builder.Services.AddDbContext<MeasallContext>(options =>
options.UseSqlServer(
          builder.Configuration.GetConnectionString("DefaultConnection")
         ));

//builder.Services.AddStackExchangeRedisCache(options =>
//{
//    options.InstanceName = builder.Configuration.GetSection("RedisCache:InstanceName").Value; // For Production "Prod-HMIS-Portal" , For Staging and Dev "Prod-HMIS-Portal"
//    options.Configuration = $"{builder.Configuration.GetSection("RedisCache:Host").Value}:{builder.Configuration.GetSection("RedisCache:Port").Value},password={builder.Configuration.GetSection("RedisCache:Password").Value}";
//});

#endregion




// Default Settings
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



#region Register Project Services 
//Service
builder.Services.TryAddScoped<DashboardService>();
builder.Services.TryAddScoped<evaluationService>();
builder.Services.TryAddScoped<ProfileServices>();
builder.Services.TryAddScoped<UsersService>();

//UnitOfWork
builder.Services.TryAddScoped<UnitOfWork<User>>();
builder.Services.TryAddScoped<UnitOfWork<UserLocation>>();
builder.Services.TryAddScoped<UnitOfWork<UserLog>>();
builder.Services.TryAddScoped<UnitOfWork<UserRole>>();
builder.Services.TryAddScoped<UnitOfWork<UserType>>();
builder.Services.TryAddScoped<UnitOfWork<Region>>();
builder.Services.TryAddScoped<UnitOfWork<DivisionView>>();
builder.Services.TryAddScoped<UnitOfWork<DistrictView>>();
builder.Services.TryAddScoped<UnitOfWork<TehsilView>>();
builder.Services.TryAddScoped<UnitOfWork<Zone>>();
builder.Services.TryAddScoped<UnitOfWork<Role>>();
builder.Services.TryAddScoped<UnitOfWork<ViewTodayVisit>>();
builder.Services.TryAddScoped<UnitOfWork<RepeatVisitPercentage>>();
builder.Services.TryAddScoped<UnitOfWork<MonitoringChild>>();
builder.Services.TryAddScoped<UnitOfWork<Module>>();
builder.Services.TryAddScoped<UnitOfWork<Indicator>>();
builder.Services.TryAddScoped<UnitOfWork<Zone>>();
#endregion



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

// Configure the HTTP request pipeline.
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
