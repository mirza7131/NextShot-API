using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AppCommonMethods;
using HMIS.Data.SyncUtility.Models.DbModels;
using HMIS.Data.SyncUtility.Models.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.IO.Compression;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing;
using Microsoft.VisualBasic.FileIO;
using SearchOption = System.IO.SearchOption;
using System.Xml.Linq;
using HMIS.Aggregator.API.Services;
using HMIS.Aggregator.API.Models.MIMS;
using JWTAuthentication;
using CommonDTOs.Enums;
using HMIS.Data.SyncUtility.Models.DTO.DataSyncUtilityLogDto;

namespace HMIS.Data.SyncUtility.Services
{
    public class DataFileCreationService
    {
        #region Class Fields & Propertities
        private readonly UnitOfWork<Patient> _uowPatient;
        private readonly UnitOfWork<OfflineServerDataSyncLog> _uowOfflineServerDataSyncLog;
        private readonly UnitOfWork<MedicineDispatch> _uowMedicineDispatch;
        private readonly int _healthFacilityId;
        private string[] _dbTablestoSync; // Tables to Sync
        private string _pendingSyncDirectoryPath; // Pending Sync Directory Path
        private string _errorSyncDirectoryPath;
        private string _doneSyncDirectoryPath;
        private string _sourceFileToZip; // Pending Sync Directory Path
        private DateTime? _defaultDateTimeToSyncData; // date time setting via appsettings to sync data accordingly
        private string _masterServerUrlForUploadFile;
        private SPForDbTablesToSync[] _sPForDbTablesToSync; // Tables to Sync
        private string _masterServerUrlOfUploadedDir;
        private readonly ILogger<Worker> _logger;
        private bool _utitliyRunOnlineServer;
        private readonly MIMSService _mimsService;
        private readonly bool _isDevelopment;
        private readonly string _mimsBaseUrl;
        private string[] _bulkMimsUploadHf; // array of health facility ids for which medicine is going to be sync
        #endregion

        #region Constructor

        public DataFileCreationService(
             UnitOfWork<Patient> uowPatient,
             UnitOfWork<OfflineServerDataSyncLog> uowOfflineServerDataSyncLog,
             IConfiguration config,
             ILogger<Worker> logger,
             MIMSService mimsService
        )
        {
            _uowPatient = uowPatient;
            _uowOfflineServerDataSyncLog = uowOfflineServerDataSyncLog;
            _healthFacilityId = config.GetSection("SystemMode").GetSection("Offline").GetValue<bool>("IsActive") ?
                                  config.GetSection("SystemMode").GetSection("Offline").GetValue<int>("HealthFacilityId") : 0;
            _dbTablestoSync = config.GetSection("SystemMode").GetSection("Offline").GetSection("DbTablesToSync").Get<string[]>() ?? new string[0];
            _pendingSyncDirectoryPath = config.GetSection("SystemMode").GetSection("Offline").GetSection("PendingSyncDirectoryPath").Get<string>() ?? "";
            _errorSyncDirectoryPath = config.GetSection("SystemMode").GetSection("Offline").GetSection("ErrorSyncDirectoryPath").Get<string>() ?? "";
            _doneSyncDirectoryPath = config.GetSection("SystemMode").GetSection("Offline").GetSection("DoneSyncDirectoryPath").Get<string>() ?? "";
            _sourceFileToZip = config.GetSection("SystemMode").GetSection("Offline").GetSection("SFileToZip").Get<string>() ?? "";

            // manual sync date time setting in case if want to sync data for a specific datetime & It will be Start Datetime of Software in Health Facility 
            _defaultDateTimeToSyncData = config.GetSection("DefaultSyncDateTime").GetValue<bool>("IsActive") ? 
                new DateTime(
                    config.GetSection("DefaultSyncDateTime").GetValue<int>("Year"),
                    config.GetSection("DefaultSyncDateTime").GetValue<int>("Month"),
                     config.GetSection("DefaultSyncDateTime").GetValue<int>("Day"),
                      config.GetSection("DefaultSyncDateTime").GetValue<int>("Hour"),
                      config.GetSection("DefaultSyncDateTime").GetValue<int>("Minutes"),
                      config.GetSection("DefaultSyncDateTime").GetValue<int>("Seconds"),
                      config.GetSection("DefaultSyncDateTime").GetValue<int>("MiliiSeconds")
                ):null;


            _masterServerUrlForUploadFile = config.GetSection("SystemMode").GetSection("Offline").GetSection("MasterServerUrlForUploadFile").Get<string>() ?? "";
            _sPForDbTablesToSync = config.GetSection("SPForDbTablesToSync").Get<SPForDbTablesToSync[]>() ?? new SPForDbTablesToSync[0];

            _masterServerUrlOfUploadedDir = config.GetSection("SystemMode").GetSection("Offline").GetSection("MasterServerUrlOfUploadedDir").Get<string>() ?? "";

            _utitliyRunOnlineServer = config.GetValue<bool>("UtitliyRunOnlineServer");
            _logger = logger;
            _mimsService = mimsService;
            _isDevelopment = config.GetValue<bool>("IsDevelopment") ? config.GetValue<bool>("IsDevelopment") : false;
            if (_isDevelopment)
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Dev").GetSection("MIMS").Value ?? string.Empty;
            else
                _mimsBaseUrl = config.GetSection("EndPoints").GetSection("Prod").GetSection("MIMS").Value ?? string.Empty;
            _bulkMimsUploadHf = config.GetSection("BulkUploadMimsHealthFacility").Get<string[]>() ?? new string[0];
        }

        #endregion

        public async Task ExecuteDataSyncUtility()
        {
            //if falg is true then execute utility for master server
            if (_utitliyRunOnlineServer)
                    await DataSyncUtilityForMasterServer();
                else
                    await DataSyncUtilityForOfflineServer();
            //await BulkUploadMimsData();
        }

        #region CUD Operations

        public async Task DataSyncUtilityForOfflineServer()
        {
            try
            {
                //Time Stamp File Name
                string dateTimeStamp = DateTime.Now.ToString("ddMMyyyyHHmmssffff");

                string dirName = _healthFacilityId + "_" + dateTimeStamp;

                string dirFullPath = _pendingSyncDirectoryPath + @"\" + dirName;

                ////Create Directory
                string subDir = CreateDirectoryIfNotExist();

                string zipFileName = @dirName;
                DateTime currentDateTime = DateTime.Now;

                try
                {

                    
                    //Last Sync Record Datetime
                    var connection = _uowOfflineServerDataSyncLog.GetDbContext().Database.GetConnectionString();
                    var lastSyncRecord = await _uowOfflineServerDataSyncLog.GetDbContext().OfflineServerDataSyncLogs.Where(x => x.HealthFacilityId == _healthFacilityId).OrderByDescending(x => x.LastSync).FirstOrDefaultAsync();

                    if (lastSyncRecord != null)
                        _defaultDateTimeToSyncData = lastSyncRecord.LastSync;

                    _logger.LogInformation("---------> Last Sync Record : {_defaultDateTimeToSyncData}", _defaultDateTimeToSyncData);
                    //_dbTablestoSync is coming from appsetting
                    _logger.LogInformation("-------------------- Proessing Tables -----------------");
                    foreach (var item in _dbTablestoSync.Select((value, index) => new { value, index }))
                    {
                        _logger.LogInformation("Working on table : {table}", item.value);
                        string filePath = CreateFileIfNotExist(subDir, item.value, dateTimeStamp, item.index + 10);
                        _logger.LogInformation("---------> File Path : {filePath}", filePath);
                        var currentList = await GetDataFromDbTablesAfterLastSyncDate(item.value, _defaultDateTimeToSyncData, currentDateTime);

                        File.WriteAllText(filePath, currentList);
                    }

                    _logger.LogInformation("-------------------- Creating Zip File -----------------");
                    string zipFilePath = CreateZipOnOfflineServer(subDir, zipFileName);

                    zipFileName = Path.GetFileName(zipFilePath);

                    FileInfo fi = new FileInfo(zipFilePath);
                    decimal size = 0;
                    if (fi.Exists)
                        size = Math.Round((decimal)((int)fi.Length/1024),2); // File Size in Kb

                    DeleteAllFilesAfterZip(subDir);

                    await SaveLastSyncDateTime(currentDateTime);

                    _logger.LogInformation("-------------------- Uploading On Server -----------------");
                    var IsUploaded = await UploadFilesToMasterServer(null, currentDateTime);

                    ////Thread.Sleep(600 * 1000);
                    Thread.Sleep(9000);
                }
                catch (Exception ex)
                {
                    //string zipFilePath = CreateZipOnOfflineServer(subDir, zipFileName);

                    //zipFileName = Path.GetFileName(zipFilePath);

                    DeleteAllFilesAfterZip(subDir);
                    var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    // Sync Log
                    await DataSyncLogInfo(zipFileName, null, currentDateTime, 0, (int)DataSyncLogStatus.Error, msg);
                    throw;
                }

                //_logger.LogInformation("-------------------- All Uploading On Server -----------------");
                //await UploadFilesToMasterServer();

                //_logger.LogInformation("-------------------- Moving All Done Uploaded Zip File From Pending To Done -----------------");
                //MoveZipFileFromPendingToDone();


            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task SaveLastSyncDateTime(DateTime? input)
        {
            OfflineServerDataSyncLog offlineServerDataSyncLog = new OfflineServerDataSyncLog();

            offlineServerDataSyncLog.LastSync = input;
            offlineServerDataSyncLog.OfflineServerDataSyncLogId = Guid.NewGuid();
            offlineServerDataSyncLog.HealthFacilityId = _healthFacilityId;

            var responseObjj = await _uowOfflineServerDataSyncLog.Repository.Insert(offlineServerDataSyncLog);
            await _uowOfflineServerDataSyncLog.CommitAsync();
        }

        #endregion

        #region Read Operations
        public async Task<String> GetDataFromDbTablesAfterLastSyncDate(string tableName,DateTime? lastSyncDateTime,DateTime? currentSyncDateTime)
        {
            using (var db = new HmisAuthContext())
            {
                var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                try
                {
                    var dt = DateTime.Now.ToString();
                    DataSet ds = new DataSet();
                    SqlCommand sqlComm = new SqlCommand("SPGetAllDataByTableNameByDateTime", (SqlConnection)conn);
                    sqlComm.CommandType = CommandType.StoredProcedure;

                    sqlComm.Parameters.AddWithValue("@StartDateTime", lastSyncDateTime == null ? DateTime.Now.ToString() : lastSyncDateTime);
                    sqlComm.Parameters.AddWithValue("@EndDateTime", currentSyncDateTime == null ? DateTime.Now.ToString() : currentSyncDateTime);

                    if (!string.IsNullOrEmpty(tableName))
                        sqlComm.Parameters.AddWithValue("@TableName", tableName);

                    SqlDataAdapter da = new SqlDataAdapter();
                    da.SelectCommand = sqlComm;
                    await Task.Run(() => da.Fill(ds));
                    List<TableDataInJsonDTO> lst = ds.Tables[0].ToList<TableDataInJsonDTO>();
                    return lst[0].JsonData;

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
        #endregion


        #region MasterServer
        public async Task DataSyncUtilityForMasterServer()
        {
            _logger.LogInformation("-------------------- Insert Uploaded Files Into Database -----------------");
            await InsertUploadedFiles();

            _logger.LogInformation("-------------------- Dispense Medicine Stock in MIMS -----------------");
            if (!_isDevelopment)
                await BulkUploadMimsData();
            //_logger.LogInformation("-------------------- Move Processed Files From Pending To Done  -----------------");
            //MoveProcessedFilesFromPendingToDoneOnMasterServer();

            //_logger.LogInformation("-------------------- Create Zip File of Files In Done Folder  -----------------");
            //CreateZipFileOfProcessDoneFiles();

            //if (!_isDevelopment)
            //    await BulkUploadMimsData();

        }
        public async Task InsertUploadedFiles()
        {
            try
            {
                string pendingFolderPath = _masterServerUrlOfUploadedDir + @"\DataSync\Pending";

                string[] filePaths = Directory.GetFiles(pendingFolderPath, "*.zip");
                
                _logger.LogInformation("-------------------- Start Proessing/Executin Text Files in Store Procedures -----------------");

                foreach (var file in filePaths.Where(x => !x.Contains("_done")))
                {
                    var processedOn = DateTime.Now;
                    var zipFileName = Path.GetFileName(file);
                    try
                    {
                        System.IO.Compression.ZipFile.ExtractToDirectory(file, pendingFolderPath);

                        string[] rawFilepath = Directory.GetFiles(pendingFolderPath, @"*.txt", SearchOption.TopDirectoryOnly).Select(Path.GetFileName)
                        .ToArray();
                        // read a file
                        foreach (string fileName in rawFilepath)
                        {
                            if (fileName.Contains("_done"))
                                continue;

                            string filePath = pendingFolderPath + @"\" + fileName;

                            _logger.LogInformation("------->  Start Processing on File : {filePath} at {datetime}", filePath , DateTime.Now);

                            // Split the string by underscore
                            string[] parts = fileName.Split('_');
                            string leftPart = parts[1];
                            _logger.LogInformation("------->  Left Part File : {leftPart} at {datetime}", leftPart, DateTime.Now);

                                string[] tableNames = leftPart.Split('.');

                            _logger.LogInformation("------->  Table Name : {tableNames[1]} at {datetime}", tableNames[1], DateTime.Now);

                            //"SPMedicineDispatchInsertOrUpdateData"
                            string spName = "SP" + tableNames[1] + "InsertOrUpdateData";


                            FileStream fileStream = new FileStream(filePath, FileMode.Open);
                            using (StreamReader reader = new StreamReader(fileStream))
                            {
                                string line = reader.ReadLine();

                                if (line != null)
                                {
                                    using (var db = new HmisAuthContext())
                                    {
                                        //_logger.LogInformation("------->   Connectiong Db ", DateTime.Now);
                                        var conn = _uowPatient.GetDbContext().Database.GetDbConnection();
                                        _logger.LogInformation("Working on Store Procedure : {spName}", spName);

                                        try
                                        {
                                            DataSet ds = new DataSet();
                                            SqlCommand sqlComm = new SqlCommand(spName, (SqlConnection)conn);
                                            sqlComm.CommandTimeout = 60000; // seconds
                                            sqlComm.CommandType = CommandType.StoredProcedure;
                                            sqlComm.Parameters.AddWithValue("@json", line);

                                            SqlDataAdapter da = new SqlDataAdapter();
                                            da.SelectCommand = sqlComm;
                                            await Task.Run(() => da.Fill(ds));
                                            await sqlComm.DisposeAsync();
                                        }
                                        catch (Exception ex)
                                        {
                                            throw ex;
                                        }
                                        finally
                                        {
                                            conn.Close();
                                        }
                                    }
                                }
                                else
                                    _logger.LogInformation("-------> File is Empty ");
                            }

                            // to rename files which is process
                            RenameProcessedFileOnMasterServer(filePath, fileName);
                            _logger.LogInformation("<------- End Processing on File : {filePath} at {datetime}", filePath, DateTime.Now);
                     
                        }
                        // Sync Log
                        await DataSyncLogInfo(zipFileName, null, processedOn, 0, (int)DataSyncLogStatus.Completed);
                        _logger.LogInformation("Delete Processed Zip File : {item}", file);
                        RenameProcessedFileOnMasterServer(file, zipFileName);

                        //File.Delete(file);

                        _logger.LogInformation("-------------------- Move Processed Files From Pending To Done  -----------------");
                        MoveProcessedFilesFromPendingToDoneOnMasterServer();

                        DeleteAllFilesWhenDone(pendingFolderPath);

                        //_logger.LogInformation("-------------------- Create Zip File of Files In Done Folder  -----------------");
                        //CreateZipFileOfProcessDoneFiles();
                    }
                    catch (Exception ex)
                    {
                        
                        MoveProcessedFilesFromPendingToErrorOnMasterServer(file, zipFileName);

                        DeleteAllFilesWhenDone(pendingFolderPath);

                        var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                        // Sync Log
                        await DataSyncLogInfo(zipFileName, null, processedOn, 0, (int)DataSyncLogStatus.Error, msg);
                    }
                }

                _logger.LogInformation("-------------------- End Proessing/Executin Text Files in Store Procedures -----------------");
            }
            catch (Exception ex)
            {
                _logger.LogInformation("***** Exception Occur ****** : {exception}", ex.InnerException != null ? ex.InnerException.Message : ex.Message);
            }
        }

        public async Task<string> RenameProcessedFileOnClientServer(string filePath, string fileName)
        {
            string pendingFilePath = _pendingSyncDirectoryPath;

            if (File.Exists(filePath))
            {
                // Get the file name without extension
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

                string fileExtension = Path.GetExtension(fileName);
                // Remove the dot (.) from the file extension
                string cleanFileExtension = fileExtension.TrimStart('.');
                string newFilePath = Path.Combine(pendingFilePath, fileNameWithoutExtension + @"_done" + fileExtension);
                _logger.LogInformation("Renaming File {filePath} to Processed Done {newFilePath}:", filePath, newFilePath);
                // Rename the file
                File.Move(filePath, newFilePath);

                Console.WriteLine("File renamed successfully.");
                return newFilePath;
            }
            else
            {
                Console.WriteLine("Old file does not exist.");
                return filePath;
            }

        }
        public void RenameProcessedFileOnMasterServer(string filePath, string fileName)
        {
            string pendingFilePath =_masterServerUrlOfUploadedDir + @"\DataSync\Pending\";

            if (File.Exists(filePath))
            {
                // Get the file name without extension
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

                string fileExtension = Path.GetExtension(fileName);
                // Remove the dot (.) from the file extension
                string cleanFileExtension = fileExtension.TrimStart('.');
                string newFilePath = Path.Combine(pendingFilePath, fileNameWithoutExtension + @"_done" + fileExtension);
                _logger.LogInformation("Renaming File {filePath} to Processed Done {newFilePath}:", filePath, newFilePath);
                // Rename the file
                File.Move(filePath, newFilePath);

                Console.WriteLine("File renamed successfully.");
            }
            else
            {
                Console.WriteLine("Old file does not exist.");
            }
        }
        public void MoveProcessedFilesFromPendingToDoneOnMasterServer()
        {
            if (!Directory.Exists(_masterServerUrlOfUploadedDir + @"\DataSync\Done"))
            {
                Directory.CreateDirectory(_masterServerUrlOfUploadedDir + @"\DataSync\Done");
            }
            string dirpath = _masterServerUrlOfUploadedDir + @"\DataSync\Pending";
            try
            {
                //foreach (string filePath in Directory.GetFiles(dirpath, @"*_done.txt", SearchOption.TopDirectoryOnly))
                foreach (string filePath in Directory.GetFiles(dirpath, @"*_done.zip", SearchOption.TopDirectoryOnly))
                {
                    string destinationPath = filePath.Replace("Pending", "Done");
                    // Check if the source file exists before attempting to move it.
                    if (File.Exists(filePath))
                    {
                        // Move the ZIP file to the destination folder.
                        File.Move(filePath, destinationPath);

                        Console.WriteLine("file moved successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Source file does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void MoveProcessedFilesFromPendingToErrorOnMasterServer(string FilePath, string FileName)
        {
            if (!Directory.Exists(_masterServerUrlOfUploadedDir + @"\DataSync\Error"))
            {
                Directory.CreateDirectory(_masterServerUrlOfUploadedDir + @"\DataSync\Error");
            }
            string dirpath = _masterServerUrlOfUploadedDir + @"\DataSync\Pending";
            try
            {
                foreach (string filePath in Directory.GetFiles(dirpath, @FileName, SearchOption.TopDirectoryOnly))
                {
                    string destinationPath = filePath.Replace("Pending", "Error");
                    // Check if the source file exists before attempting to move it.
                    if (File.Exists(filePath))
                    {
                        // Move the ZIP file to the destination folder.
                        File.Move(filePath, destinationPath);

                        Console.WriteLine("file moved successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Source file does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public void CreateZipFileOfProcessDoneFiles()
        {
            _logger.LogInformation("-------------------- Creating Zip File of Processed Files  -----------------");
            try
            {
                string dirPath = _masterServerUrlOfUploadedDir + @"\DataSync\Done\";

                string[] fileNames = Directory.GetFiles(dirPath, @"*_done.txt").Select(Path.GetFileName).ToArray();

                string[] splitResutls = fileNames[0].Split("_");

                string zipFilePath = dirPath + @"10_" + @splitResutls[2] + @"_" + @splitResutls[3] + "_done.zip";

                using (ZipArchive archive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                {

                    foreach (string fileName in Directory.GetFiles(dirPath, @"*.txt", SearchOption.TopDirectoryOnly))
                    {
                        if (fileName.Contains(@splitResutls[3]))

                        {
                            string fileNamee = fileName.Substring(fileName.LastIndexOf('\\') + 1);

                            archive.CreateEntryFromFile(fileName, fileNamee);
                            File.Delete(fileName);
                        }
                    }
                    archive.Dispose();

                    if (Directory.GetFiles(dirPath, @"*_done.txt").Length > 0)
                        CreateZipFileOfProcessDoneFiles();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        public async Task BulkUploadMimsData()
        {
            var medicineDispatchConnection = _uowPatient.GetDbContext();
            

            // var groupedMedicineDispatches = await medicineDispatchConnection.ViewGetAccumulateMedicineDispatches.Where(x=>x.MIMSDispatched == false)
            //.GroupBy(p => new { p.MedicineId,p.MedicineName,p.WardId,p.HealthFacilityId, p.HealthFacilityCode })
            //.Select(group =>
            // new {
            //         MedicineId = group.Key.MedicineId,
            //         MedicineName = group.Key.MedicineName,
            //         WardId = group.Key.WardId,
            //         HealthFacilityId = group.Key.HealthFacilityId,
            //         HealthFacilityCode = group.Key.HealthFacilityCode,
            //         QuantityDispatched = group.Sum(x=>x.QuantityDispatch??0)
            //    }).ToListAsync();


            var groupedMedicineDispatches = await medicineDispatchConnection.ViewGetAccumulateMedicineDispatches.ToListAsync();
            groupedMedicineDispatches = groupedMedicineDispatches.Where(x => _bulkMimsUploadHf.Contains(Convert.ToString(x.HealthFacilityId))).ToList(); // for samnabad hospital
            var healhtFacilityIds = groupedMedicineDispatches.Select(l => l.HealthFacilityId).Distinct().ToList();

            foreach (var hfId in healhtFacilityIds)
            {

                List<MedicineDispenseDto> mimsMedicineList = new List<MedicineDispenseDto>();

                mimsMedicineList = groupedMedicineDispatches.Where(x=>x.HealthFacilityId == hfId).Select(x => new MedicineDispenseDto
                {
                    hfmisCode = Convert.ToString(x.HealthFacilityHrId),
                    Quantity = x.QuantityDispatched ?? 0,
                    MedId = x.MedicineId,
                    WardId = x.WardId,
                    HealthFacilityId = hfId??0,
                    MIMSDispatched = true
                }).ToList();

                //}

                //foreach (var item in groupedMedicineDispatches)
                //{

                //    mimsMedicineList.Add(new MedicineDispenseDto
                //    {
                //        hfmisCode = item.HealthFacilityCode,
                //        Quantity = item.QuantityDispatched ?? 0,
                //        MedId = item.MedicineId,
                //        WardId = item.WardId,
                //    });

                var mimsDispatchResponse = await _mimsService.MedicineDespenseByHealthFacility(_mimsBaseUrl, mimsMedicineList);

                string reason = null;
                bool mIMSDispatched = false;

                if (mimsDispatchResponse != null && mimsDispatchResponse.Status) // if medicine is dispatched on MIMS db then set true in our internal db otherwise false
                {
                    mimsMedicineList.ForEach(
                          s => {
                              s.Reason = mimsDispatchResponse.Message;
                          });
                }
                else if (mimsDispatchResponse.Data == null)
                    reason = mimsDispatchResponse.Message;
                else if (mimsDispatchResponse.Data.Count > 0)
                {
                    foreach (var item in mimsDispatchResponse.Data)
                    {
                        mimsMedicineList.Where(x => x.MedId == item.MedId).ToList().ForEach(
                            s =>
                            {
                                s.Reason = item.Reason;
                                s.MIMSDispatched = false;

                            });
                    }
                }


                    var line = JsonConvert.SerializeObject(mimsMedicineList);
                
                    
                    var conn = _uowPatient.GetDbContext().Database.GetDbConnection();

                    try
                    {
                        //        DataSet ds = new DataSet();
                        //        SqlCommand sqlComm = new SqlCommand("SPUpdateMimsMedicineDispatchResponse", (SqlConnection)conn);
                        //        sqlComm.CommandTimeout = 600; // seconds
                        //        sqlComm.CommandType = CommandType.StoredProcedure;
                        //        sqlComm.Parameters.AddWithValue("@HealthFacilityId", hfId);
                        //        sqlComm.Parameters.AddWithValue("@MedicineId", item.MedicineId);
                        //        sqlComm.Parameters.AddWithValue("@WardId", item.WardId);
                        //        sqlComm.Parameters.AddWithValue("@Message", reason);
                        //        sqlComm.Parameters.AddWithValue("@MIMSDispatched", mIMSDispatched);

                        //        SqlDataAdapter da = new SqlDataAdapter();
                        //        da.SelectCommand = sqlComm;
                        //        await Task.Run(() => da.Fill(ds));
                        //        await sqlComm.DisposeAsync();

                        DataSet ds = new DataSet();
                        SqlCommand sqlComm = new SqlCommand("SPUpdateMimsMedicineDispatchResponse", (SqlConnection)conn);
                        sqlComm.CommandTimeout = 60000; // seconds
                        sqlComm.CommandType = CommandType.StoredProcedure;
                        sqlComm.Parameters.AddWithValue("@json", line);

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = sqlComm;
                        await Task.Run(() => da.Fill(ds));
                        await sqlComm.DisposeAsync();

                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                
            }

        }

        private async Task DataSyncLogInfo(string fileName, DateTime? uploadedOn, DateTime? processedOn, decimal fileSize, int status, string msg = "")
        {
            DataSyncUtilityLog obj = new DataSyncUtilityLog();
            var _uowDataSyncUtilityLog = new UnitOfWork<DataSyncUtilityLog>(_uowPatient.GetDbContext());
            var env = (_utitliyRunOnlineServer == true) ? "Online" : "Offline";

            var dbObj = await _uowDataSyncUtilityLog.Repository.GetALL(x => x.FileName == fileName && x.ServerType == env).SingleOrDefaultAsync();

            if (!AppCommonMethod.IsNullObject(dbObj))
            {
                obj = dbObj;

                obj.ServerType = (_utitliyRunOnlineServer == true) ? "Online" : "Offline";
                obj.Status = status;
                obj.StatusUpdatedOn = DateTime.Now;

                obj.UploadedOn = (obj.UploadedOn != null) ? obj.UploadedOn : processedOn;
                obj.ProcessedOn = processedOn;
                if (obj.Status == (int)DataSyncLogStatus.Completed)
                {
                    obj.CompletedOn = DateTime.Now;
                    obj.Message = null;
                }
                else if (obj.Status == (int)DataSyncLogStatus.Error)
                    obj.Message = msg;
                _uowDataSyncUtilityLog.Repository.Update(obj);

            }
            else
            {
                obj.DataSyncUtilityLogId = Guid.NewGuid();

                if (!string.IsNullOrEmpty(fileName))
                {
                    string[] parts = fileName.Split('_');

                    obj.HealthFacilityId = (parts.Length > 2) ? int.Parse(parts[1]) : int.Parse(parts[0]);
                }

                obj.ServerType = (_utitliyRunOnlineServer == true) ? "Online" : "Offline";
                obj.FileName = fileName;
                obj.FileSize = fileSize.ToString();
                obj.Status = status;
                obj.StatusUpdatedOn = DateTime.Now;

                obj.UploadedOn = (uploadedOn == null) ? processedOn : uploadedOn;
                obj.ProcessedOn = (uploadedOn == null) ? DateTime.Now : processedOn;
                if (obj.Status == (int)DataSyncLogStatus.Completed)
                {
                    obj.CompletedOn = DateTime.Now;
                    obj.Message = null;
                }
                else if (obj.Status == (int)DataSyncLogStatus.Error)
                    obj.Message = msg;

                await _uowDataSyncUtilityLog.Repository.Insert(obj);
            }

            await _uowDataSyncUtilityLog.CommitAsync();

        }

        #endregion

        #region Helper Methods

        public string CreateDirectoryIfNotExist()
        {
            if (!Directory.Exists(@_pendingSyncDirectoryPath))
            {
                Directory.CreateDirectory(@_pendingSyncDirectoryPath);
            }

            //if (!Directory.Exists(dirFullPath))
            //{
            //    Directory.CreateDirectory(dirFullPath);
            //}

            return @_pendingSyncDirectoryPath;
        }

        public string CreateFileIfNotExist(string subDir,string tableName,string datetimeStamp , int index)
        {
            string fileName =  _pendingSyncDirectoryPath + @"\" + index +"_"+tableName + "_" +_healthFacilityId + "_" + datetimeStamp + ".txt"; ;
            _logger.LogInformation("----------------------> Creating File :{fileName}", fileName);
            if (File.Exists(fileName))
            {
                //File.WriteAllText(fileName, "");
                // If the file exists, delete it to replace it with the new content.
                File.Delete(fileName);
            }

            return fileName;
        }

        public string CreateZipOnOfflineServer(string dirPath, string zipName)
        {
            int zipFileCount = 0;
            string zipFilePath = "";
            try
            {
                zipFileCount = Directory.GetFiles(_pendingSyncDirectoryPath, @"*.zip").Length;


                if (zipFileCount == 0)
                    zipName = @"10_" + zipName;
                else
                {

                    // Get a list of file paths in the folder
                    string[] fileNames = Directory.GetFiles(_pendingSyncDirectoryPath, @"*.zip").Select(Path.GetFileName).ToArray();

                    var sortedFiles = fileNames.OrderByDescending(f => f).ToList();

                    // Split the string by underscore
                    string[] parts = sortedFiles[0].Split('_');
                    string leftPart = parts[0];

                    zipName = Convert.ToString(Convert.ToInt32(leftPart) + 1) + @"_" + zipName;

                }

                zipFilePath = _pendingSyncDirectoryPath + @"\" + zipName + @".zip";

                if (File.Exists(zipFilePath))
                {
                    // If the file exists, delete it to replace it with the new content.
                    File.Delete(zipFilePath);
                }

                using (ZipArchive archive = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                {

                    foreach (string fileName in Directory.GetFiles(dirPath, @"*.txt", SearchOption.TopDirectoryOnly))
                    {
                        string fileNamee = fileName.Substring(fileName.LastIndexOf('\\') + 1);

                        archive.CreateEntryFromFile(fileName, fileNamee);

                    }
                    archive.Dispose();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return zipFilePath;
        }

        public void DeleteAllFilesAfterZip(string dirPath)
        {
            string[] filePaths = Directory.GetFiles(dirPath, "*.txt");

            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }
        }

        public void DeleteAllFilesWhenDone(string dirPath)
        {
            string[] filePaths = Directory.GetFiles(dirPath, "*.txt");

            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }
        }

        public async Task<bool> UploadFilesToMasterServer(string? filePath = null, DateTime? processedDate = null)
        {

            string fileType = @"*.zip";
            //string fileType = @"*.rar"; // @"*.zip";

            // Create an HttpClient instance
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var files = new List<string>();

                    if (string.IsNullOrEmpty(filePath))
                        files = Directory.GetFiles(_pendingSyncDirectoryPath, fileType, SearchOption.TopDirectoryOnly).ToList();
                    else
                        files.Add(filePath);

                    foreach (string fileName in files.Where(x => !x.Contains("_done")))
                    {
                        if (processedDate == null)
                            processedDate = DateTime.Now;

                        FileInfo fi = new FileInfo(fileName);
                        decimal size = 0;
                        if (fi.Exists)
                            size = Math.Round((decimal)((int)fi.Length / 1024), 2); // File Size in Kb

                        // Create a FileStream to read the file
                        using (FileStream fileStream = File.OpenRead(fileName))
                        {
                            // Create a MultipartFormDataContent
                            using (MultipartFormDataContent content = new MultipartFormDataContent())
                            {
                                // Add the file to the content
                                content.Add(new StreamContent(fileStream), "file", Path.GetFileName(fileName));

                                //client.Timeout = TimeSpan.FromMilliseconds(100000000);

                                // Send the HTTP POST request
                                _logger.LogInformation("Request URL : {URL}" , _masterServerUrlForUploadFile);
                                HttpResponseMessage response = await client.PostAsync(_masterServerUrlForUploadFile, content);

                                fileStream.Close();

                                // Check if the request was successful
                                if (response.IsSuccessStatusCode)
                                {
                                    var uploadedOn = DateTime.Now;
                                    Console.WriteLine("File uploaded successfully.");

                                    // to rename files which is process
                                    _logger.LogInformation("-------------------- Renaming File -----------------");
                                    var newFileNamePath = await RenameProcessedFileOnClientServer(fileName, Path.GetFileName(fileName));

                                    _logger.LogInformation("-------------------- Moving Uploaded Zip File From Pending To Done -----------------");
                                    //MoveZipFileFromPendingToDone(newFileNamePath);
                                    MoveZipFileFromPendingToDone();

                                    // Sync Log
                                    await DataSyncLogInfo(Path.GetFileName(fileName), uploadedOn, processedDate, size, (int)DataSyncLogStatus.Completed);

                                    if (!string.IsNullOrEmpty(filePath))
                                        return true;
                                    
                                }
                                else
                                {
                                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");

                                    //_logger.LogInformation("-------------------- Moving Uploaded Zip File From Pending To Error -----------------");
                                    //MoveZipFileFromPendingToError();

                                    // Sync Log
                                    await DataSyncLogInfo(Path.GetFileName(fileName), null, processedDate, size, (int)DataSyncLogStatus.NotUploaded);

                                    if (!string.IsNullOrEmpty(filePath))
                                        return false;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    return false;
                }
            }
            return false;
        }

        public void MoveZipFileFromPendingToDone(string? FileName = null)
        {
            
            if (!Directory.Exists(@_doneSyncDirectoryPath))
            {
                Directory.CreateDirectory(@_doneSyncDirectoryPath);
            }

            try
            {
                var files = new List<string>();

                if (string.IsNullOrEmpty(FileName))
                    files = Directory.GetFiles(_pendingSyncDirectoryPath, @"*_done.zip", SearchOption.TopDirectoryOnly).ToList();
                else
                    files.Add(FileName);
                
                foreach (string fileName in files)
                {
                    string destinationPath = fileName.Replace("Pending", "Done");
                    // Check if the source file exists before attempting to move it.
                    if (File.Exists(fileName))
                    {
                        // Move the ZIP file to the destination folder.
                        File.Move(fileName, destinationPath);

                        Console.WriteLine("ZIP file moved successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Source ZIP file does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void MoveZipFileFromPendingToError()
        {

            if (!Directory.Exists(@_errorSyncDirectoryPath))
            {
                Directory.CreateDirectory(@_errorSyncDirectoryPath);
            }

            try
            {
                foreach (string fileName in Directory.GetFiles(_pendingSyncDirectoryPath, @"*.zip", SearchOption.TopDirectoryOnly))
                {
                    string destinationPath = fileName.Replace("Pending", "Error");
                    // Check if the source file exists before attempting to move it.
                    if (File.Exists(fileName))
                    {
                        // Move the ZIP file to the destination folder.
                        File.Move(fileName, destinationPath);

                        Console.WriteLine("ZIP file moved successfully.");
                    }
                    else
                    {
                        Console.WriteLine("Source ZIP file does not exist.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }



        #endregion
    }
}
public class SPForDbTablesToSync
{
    public string SPName { get; set; }
    public string TableName { get; set; }

}
