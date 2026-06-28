using CommonDTOs.ResponseDTO;
using CommonExceptionHandler;
using CommonMessages;
using FileHandler;
using HMIS.Aggregator.API;
using HMIS.CDN.WebAPI.Common.DTO;
using JWTAuthentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HMIS.CDN.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        #region Class Fields & Propertities

        private readonly UploadFiles _fileUploader;
        private readonly string _baseURL;
        private readonly AuthCommonService _authCommonService;

        #endregion

        #region Constructor

        public UploadFileController(UploadFiles fileUploader,IConfiguration config, AuthCommonService authCommonService)
        {
            _fileUploader = fileUploader;
            _authCommonService = authCommonService;
            _baseURL = config.GetSection("BackendEndpoint").GetSection("BaseURL").Value ?? string.Empty;
        }

        #endregion

        #region CUD Operations

        [HttpPost]
        [Route("Upload")]
        public async Task<ActionResult> UploadDocument(UploadDocumentDto input)
        {
            if (!string.IsNullOrEmpty(input.Base64String))
            {
                if (_fileUploader.IsBase64String(input.Base64String))
                {
                    var uploadedURL = "";
                    input.FolderName = System.IO.Path.Combine(input.ProjectName, input.FolderName);
                    if(input.FolderName == "HMIS\\Patient/Fingerprint") //Drug Addict
                    {
                        uploadedURL = await _fileUploader.UploadFileFromBase64AsyncForDrugAddict(input.FolderName, input.Base64String);
                    }
                    else
                    {
                        uploadedURL = await _fileUploader.UploadFileFromBase64Async(input.FolderName, input.Base64String);
                    }
                    if (string.IsNullOrEmpty(uploadedURL))
                        throw new UserFriendlyException(CommonMessageConstant.FileUploadError);
                    
                    return Ok(new ResponseSuccess { data = Path.Combine(_baseURL, uploadedURL)});
                }
                else
                    return Ok(new ResponseSuccess { data = input.Base64String }); // if string is not base64 then it means it is URL
            }
            else
            {
                throw new Exception(CommonMessageConstant.FileUploadError);
            }
        }

        [HttpPost]
        [Route("UploadFile")]
        public async Task<IActionResult> UploadFile()
        {
            var filelist = HttpContext.Request.Form.Files;
            var fileUrl = "";
            if (filelist.Count > 0)
            {
                foreach (var file in filelist)
                {
                    fileUrl = await _fileUploader.UploadFileToDataSyncPendingViaFormFile(file);
                }
            }

            return Ok(new ResponseSuccess { data = fileUrl });
        }

        #endregion

            #region Read Operations


            #endregion

            #region Helper Methods


            #endregion
        }
}
