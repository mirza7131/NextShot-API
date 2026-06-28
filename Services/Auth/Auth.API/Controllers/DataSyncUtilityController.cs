using AuthBAL;
using AuthDAL.Models.DbModels;
using AuthDAL.Repositories.UOW;
using Azure.Core;
using CommonDTOs.ResponseDTO;
using CommonMessages;
using DTOs.UserDTO;
using FileHandler;
using JWTAuthentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SharpCompress.Archives;
using SharpCompress.Common;
using System.Data;
using System.Linq;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;


namespace Auth.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DataSyncUtilityController : ControllerBase
    {
        #region Class Fields & Propertities
        private readonly IHostingEnvironment _env;
        private readonly UploadFiles _fileUploader;
        private readonly UnitOfWork<Patient> _uowPatient;
        #endregion

        #region Constructor

        public DataSyncUtilityController(
            IHostingEnvironment env,
            UploadFiles fileUploader,
            UnitOfWork<Patient> uowPatient,
            IConfiguration config
        )
        {
            _env = env;
            _fileUploader = fileUploader;
            _uowPatient = uowPatient;
        }

        #endregion

        #region CUD Operations


        #endregion

        #region Read Operations


        #endregion

        #region Helper Methods


        #endregion
    }
}
