using HMIS.Aggregator.API.Services;
using HMIS.Data.SyncUtility;
using HMIS.Data.SyncUtility.Models.DbModels;
using HMIS.Data.SyncUtility.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddHttpClient();
        services.AddTransient<MIMSService>();
        services.AddTransient<DataFileCreationService>();
        services.AddTransient<UnitOfWork<Patient>>();
        services.AddTransient<UnitOfWork<OfflineServerDataSyncLog>>();
    })
    .Build();

//#region Register Project Services 
////*********** UOW Registered ************** //

////builder.Services.TryAddScoped(typeof(PagedListDto<>));
////Services
//host.Services.<DataFileCreationService>();


await host.RunAsync();
