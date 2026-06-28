using HMIS.Data.SyncUtility.Services;
using System;

namespace HMIS.Data.SyncUtility
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly DataFileCreationService _DataFileCreationService;
  
        private DateTime _scheduleTime;
        private static int FixHours = 12;//12 P.M.
        private static bool IsInProcessing = true;
        private readonly int _consolePrintTimeInSeconds;
        public Worker(
            ILogger<Worker> logger,
             DataFileCreationService DataFileCreationService,
             IConfiguration config
        )
        {
            _logger = logger;
            _DataFileCreationService = DataFileCreationService;
            _consolePrintTimeInSeconds = config.GetValue<int>("ConsolePrintTimeInSec");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //var timeInMilliSeconds = 0.00;

                //if (DateTime.Now.TimeOfDay.Hours < FixHours)
                //{
                //    var staticDateTime = DateTime.Now.Date;
                //    staticDateTime = staticDateTime.AddHours(FixHours).AddMinutes(0).AddSeconds(0);
                //    timeInMilliSeconds = staticDateTime.Subtract(DateTime.Now).TotalMilliseconds;
                //}
                //else
                //{
                //    // Schedule to run once a day at 12 P.M.
                //    _scheduleTime = DateTime.Today.AddDays(1).AddHours(FixHours);
                //    timeInMilliSeconds = _scheduleTime.Subtract(DateTime.Now).TotalMilliseconds;
                //}
                //if(IsInProcessing)
                //{
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    await _DataFileCreationService.ExecuteDataSyncUtility();
                    _logger.LogInformation("Worker Completed at: {time}", DateTimeOffset.Now);
                    await Task.Delay(_consolePrintTimeInSeconds, stoppingToken);    
                    Environment.Exit(0);

                //await Task.Delay(_consolePrintTimeInSeconds, stoppingToken);
                //IsInProcessing = false;
                //await Task.Delay(Convert.ToInt32(timeInMilliSeconds), stoppingToken);
                //}

            }
            
        }

        public void ConsolePrint()
        {
            _logger.LogInformation("Console Print at: {time}", DateTimeOffset.Now);
        }
    }
}