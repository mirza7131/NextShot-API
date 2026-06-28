using Azure.Core;
using CommonDTOs.ResponseDTO;
using CommonExceptionHandler;
using CommonMessages;
using FileHandler.DTO;
using HMIS.Aggregator.API;
using HMIS.Aggregator.API.Models.Dto.DataSyncUtilityLog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Reflection.Metadata;
using System.Text;

namespace FileHandler
{
    public class UploadFiles
    {
        private readonly IHostingEnvironment _env;
        private readonly string _baseURL;
        private readonly string _baseURLForEmcFileUpload;
        private readonly string _baseURLCrystalReport;
        private readonly string _fileUploadPath;
        private readonly AuthCommonService _authCommonService;


        public UploadFiles(IHostingEnvironment env, IConfiguration config, AuthCommonService authCommonService)
        {
            _authCommonService = authCommonService;
            _env = env;
            _baseURL = config.GetSection("BackendEndpoint").GetSection("CDN").Value ?? string.Empty;
            _baseURLCrystalReport = config.GetSection("BackendEndpoint").GetSection("CDN-CrystalReport").Value ?? string.Empty;
            _fileUploadPath =   config.GetSection("BackendEndpoint").GetSection("UploadEndPoint").Value ?? string.Empty;
        }

        public string UploadFileFromBase64(string folderName,string base64String)
        {

            string outputImgFilename = Guid.NewGuid().ToString() + "." + GetFileExtension(base64String);
            var folderPath = System.IO.Path.Combine(_env.ContentRootPath, "wwwroot", folderName);
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }
            System.IO.File.WriteAllBytes(Path.Combine(folderPath, outputImgFilename), Convert.FromBase64String(GetCommaSeparatedBase64(base64String)));
            return Path.Combine(folderName, outputImgFilename);
        }
         public async Task<string> UploadFileFromBase64Async(string folderName, string base64String)
         {

            string outputImgFilename = Guid.NewGuid().ToString() + "." + GetFileExtension(base64String);
            //var folderPath = System.IO.Path.Combine("\\192.168.0.42\\app-storage","wwwroot", folderName);
            var currentDirectory = Directory.GetCurrentDirectory();
            var folderPath = System.IO.Path.Combine(currentDirectory, "wwwroot", folderName);
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }
            System.IO.File.WriteAllBytes(Path.Combine(folderPath, outputImgFilename), Convert.FromBase64String(GetCommaSeparatedBase64(base64String)));
            return await Task.FromResult(Path.Combine(folderName, outputImgFilename));
         }


        #region DrugAddict


        public async Task<string> UploadFileFromBase64AsyncForDrugAddict(string folderName, string base64String)
        {

            string outputImgFilename = Guid.NewGuid().ToString() + ".jpg";
            //var folderPath = System.IO.Path.Combine("\\192.168.0.42\\app-storage","wwwroot", folderName);
            var currentDirectory = Directory.GetCurrentDirectory();
            var folderPath = System.IO.Path.Combine(currentDirectory, "wwwroot", folderName);
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }
            System.IO.File.WriteAllBytes(Path.Combine(folderPath, outputImgFilename), Convert.FromBase64String(GetCommaSeparatedBase64(base64String)));
            return await Task.FromResult(Path.Combine(folderName, outputImgFilename));
        }

        public async Task<string> ImageToBase64(string imagePath)
        {
            byte[] imageBytes = await File.ReadAllBytesAsync(imagePath);
            string base64String = Convert.ToBase64String(imageBytes);
            return base64String;
        }

        public async Task<string> GetImageAsBase64Async(string url) // return Task<string>
        {
            using (var client = new HttpClient())
            {
                var bytes = await client.GetByteArrayAsync(url); // there are other methods if you want to get involved with stream processing etc
                var base64String = Convert.ToBase64String(bytes);
                return "data:image/jpg;base64," +  base64String;
            }
        }




        #endregion



        public string UploadFileFromByteArray(string folderName, byte[] byteArray)
        {
            string outputImgFilename = Guid.NewGuid().ToString() + ".png";
            var folderPath = System.IO.Path.Combine(_env.ContentRootPath, "wwwroot", folderName);
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }
            System.IO.File.WriteAllBytes(Path.Combine(folderPath, outputImgFilename), byteArray);
            return Path.Combine(folderName, outputImgFilename);
        }

        public async Task<string?> UploadFileToCDN(string folderName, string base64String , string accessToken)
        {
            var body = new UploadDocumentDto
                {
                   ProjectName = CommonStringConstant.Hmis,
                   FolderName= folderName,
                   Base64String = base64String
                };

            var jsonBody = JsonConvert.SerializeObject(body);
            var stringContent = new StringContent(jsonBody, Encoding.UTF8, CommonStringConstant.contentTypeApplicationJson);


            HttpClient client = new HttpClient() { BaseAddress = new Uri(_baseURL)};

            
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var httpResponse = await client.PostAsync(_fileUploadPath, stringContent);
            var content = await httpResponse.Content.ReadAsStringAsync();
            
            ResponseDTO? response = JsonConvert.DeserializeObject<ResponseDTO?>(content);
            if (response == null)
                throw new UserFriendlyException(CommonMessageConstant.FileUploadError);

            if (string.IsNullOrEmpty(Convert.ToString(response!.data)))
                throw new UserFriendlyException(CommonMessageConstant.FileUploadError);

            if (response!.status)
                return await Task.FromResult(Convert.ToString(response!.data));
            else
                return null;
            
        }

        public async Task<string?> UploadDocumentsForCrystalReport(string folderName, string base64String, string accessToken)
        {
            var body = new UploadDocumentDto
            {
                ProjectName = CommonStringConstant.Hmis,
                FolderName = folderName,
                Base64String = base64String
            };

            var jsonBody = JsonConvert.SerializeObject(body);
            var stringContent = new StringContent(jsonBody, Encoding.UTF8, CommonStringConstant.contentTypeApplicationJson);


            HttpClient client = new HttpClient() { BaseAddress = new Uri(_baseURLCrystalReport) };


            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var httpResponse = await client.PostAsync(_fileUploadPath, stringContent);
            var content = await httpResponse.Content.ReadAsStringAsync();

            ResponseDTO? response = JsonConvert.DeserializeObject<ResponseDTO?>(content);
            if (response == null)
                throw new UserFriendlyException(CommonMessageConstant.FileUploadError);

            

            if (string.IsNullOrEmpty(Convert.ToString(response!.data)))
                throw new UserFriendlyException(CommonMessageConstant.FileUploadError);

            string url = "";

            if (!string.IsNullOrEmpty(Convert.ToString(response!.data)))
            {
                url = response!.data.ToString().Replace("http", "https");
                url = url.Replace("cdn", "cdn-crystalreport");
            }
            
            if (response!.status)
                return await Task.FromResult(Convert.ToString(url));
            else
                return null;

        }

        public async Task<string> UploadFileToDataSyncPendingViaFormFile(dynamic file)
        {

            var folderPath = System.IO.Path.Combine(_env.ContentRootPath, "wwwroot", @"DataSync\Pending");
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }
            string fileName = file.FileName;
            using (var fileStream = new FileStream(Path.Combine(folderPath, fileName), FileMode.Create))
            {
                await file.CopyToAsync(fileStream);

                if (!string.IsNullOrEmpty(fileName))
                {
                    var filePath = Path.Combine(folderPath, fileName);
                    FileInfo fi = new FileInfo(filePath);
                    decimal size = 0;
                    if (fi.Exists)
                        size = Math.Round((decimal)((int)fi.Length / 1024), 2); // File Size in Kb

                    CreateDataSyncUtilityLog createDataSyncUtilityLog = new CreateDataSyncUtilityLog();
                    createDataSyncUtilityLog.FileName = fileName;
                    createDataSyncUtilityLog.FileSize = size.ToString();
                    createDataSyncUtilityLog.Status = 1; // 1 = Pending
                    createDataSyncUtilityLog.StatusUpdatedOn = DateTime.Now;
                    createDataSyncUtilityLog.UploadedOn = DateTime.Now;
                    if (!string.IsNullOrEmpty(fileName))
                    {
                        string[] parts = fileName.Split('_');
                        createDataSyncUtilityLog.HealthFacilityId = (parts.Length > 2) ? int.Parse(parts[1]) : int.Parse(parts[0]);
                    }

                    await _authCommonService.CreateDataSyncUtilityLog(createDataSyncUtilityLog);
                }
                    
            }
           
            return Path.Combine(folderPath, fileName);
        }

        public bool IsBase64String(string base64String)
        {
            var base64 = base64String.Split(",");
            if (base64.Length > 1)
            {
                Span<byte> buffer = new Span<byte>(new byte[base64[1].Length]);
                return Convert.TryFromBase64String(base64[1], buffer, out int bytesParsed);
            }
            else return false;
        }

        public string GetCommaSeparatedBase64(string base64String)
        {
            var base64 = base64String.Split(",");
            return base64[1];
        }



        public async Task SyncUtilityLog()
        {

        }

        #region Helper Methods 

        private static string GetFileExtension(string base64String)
        {
            if (base64String != null)
            {
                var base64 = base64String.Split(",");
                var data = base64[1].Substring(0, 5);

                switch (data.ToUpper())
                {
                    case "IVBOR":
                        return "png";
                    case "/9J/4":
                        return "jpg";
                    case "AAAAF":
                        return "mp4";
                    case "JVBER":
                        return "pdf";
                    case "AAABA":
                        return "ico";
                    case "UMFYI":
                        return "rar";
                    case "E1XYD":
                        return "rtf";
                    case "U1PKC":
                        return "txt";
                    case "DGHPC":
                        return "txt";
                    case "UESDB":
                        return "zip";
                    case "PCFET":
                        return "html";
                    case "MQOWM":
                    case "77U/M":
                        return "srt";
                    case "PHN2Z":
                        return "svg";
                    default:
                        return string.Empty;
                }
            }

            return null;
        }

        private static string GetFileName(object moduleId, string base64Content)
        {
            var fileExtension = GetFileExtension(base64Content);
            var newFileName = $"{moduleId}_{DateTime.Now.Ticks}.{fileExtension}";
            return newFileName;
        }

        private static string GetFileName(string base64Content)
        {
            var fileExtension = GetFileExtension(base64Content);
            var newFileName = $"{DateTime.Now.Ticks}.{fileExtension}";
            return newFileName;
        }



        #endregion
    }
}