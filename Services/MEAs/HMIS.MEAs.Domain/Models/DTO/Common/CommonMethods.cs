using HMIS.MEAs.Domain.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMIS.MEAs.Domain.Models.DTO.Common
{
    public static class CommonMethods
    {

        #region Helper Methods
        public static async Task<long> LogError(Exception ex)
        {
            ErrorLog obj = new ErrorLog();
            obj.Message = ex.Message;
            obj.StackTrace = ex.StackTrace;
            obj.InnerException = ex.InnerException != null ? ex.InnerException.ToString() : null;
            obj.CreatedOn = DateTime.Now;
            return await CreateAsync(obj);
        }

        private static async Task<long> CreateAsync(ErrorLog entityToCreate)
        {
            try
            {
                using (var db = new MeasallContext())
                {
                    db.ErrorLogs.Add(entityToCreate);
                    db.SaveChanges();
                    return entityToCreate.ErrorLogId;
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static string SaveDocument(string Base64, string Path, string FolderName)
        {
            try
            {
                var webRoot = Path;

                var Folder = System.IO.Path.Combine(webRoot, FolderName);


                if (!System.IO.Directory.Exists(Folder))
                    System.IO.Directory.CreateDirectory(Folder);

                var fileName = @"\" + Guid.NewGuid() + "." + GetFileExtensionFromBase64(Base64);

                var filePathName = Folder + fileName;

                //set the image path
                if (Base64.Contains(","))
                    Base64 = Base64.Substring(Base64.IndexOf(",") + 1);

                byte[] imageBytes = Convert.FromBase64String(Base64);

                File.WriteAllBytes(filePathName, imageBytes);

                return fileName;
            }
            catch (Exception)
            {
                throw;
            }
        }


        private static string GetFileExtensionFromBase64(string base64)
        {
            String[] strings = base64.Split(',');
            String extension;
            switch (strings[0])
            {
                case "data:image/jpeg;base64":
                    return extension = "jpeg";
                    break;
                case "data:image/png;base64":
                    return extension = "png";
                    break;
                case "data:application/octet-stream;base64":
                    return extension = "xlsx";
                case "data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64":
                    return extension = "xlsx";

                default://should write cases for more images types
                    return extension = "jpg";
                    break;
            }
        }


        #region CRUD Operations Messages

        public static string saveSuccess = "Save Successfully.";
        public static string updateSuccess = "Updated Successfully.";
        public static string deleteSuccess = "Deleted Successfully.";

        #endregion

        #region Authorizationa and Authentication Messages
        public static string LoggedIn = "User LoggedIn Successfully.";
        public static string UnAuthenticated = "Email or password incorrect.";
        public static string UnAuthorized = "UnAuthorized User.";
        public static string tokenExpire = "Token expired.";
        public static string invalidToken = "Invalid token.";
        public static string serverSideError = "Server Side Error Occur. Code : ";
        public static string validationError = "Validation Error :  ";

        #endregion

        #region Record Messages

        public static string dataFetched = "Data Fetched!.";
        public static string visitGenerated = "Visits Generated!.";
        public static string notFound = "Record Not Found!.";
        public static string recordExists = "Record Already Exisited!.";
        public static string userEmailExists = "Username or Email Already Exists.";
        public static string recordActivated = "Activated Successfully.";
        public static string recordDeactivated = "Record deactivated.";
        public static string EPICenterNameReq = "Please Enter EPI Center Name!";
        public static string SequenceNoExist = "Sequence # Already Exist:  ";

        #endregion

        #endregion
    }


    public class ResponseDTO
    {
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; }
        public Boolean Error { get; set; } = false;
        public Object Data { get; set; }
    }

    public class ModulesSaveResponseDTO
    {
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; }
        public Boolean Error { get; set; } = false;
        public String ModuleName { get; set; }
    }

    public class ResponseListDTO
    {
        public int StatusCode { get; set; } = 200;
        public string Message { get; set; }
        public Boolean Error { get; set; } = false;
        public Object List { get; set; }
    }

    public class ResponsePaginatedDTO : ResponseDTO
    {
        public int PageNumber { get; set; } = 1;
        public long TotalRecords { get; set; }
        public long Size { get; set; }
        public long PageCount { get; set; }
        public long TotalVaccinated { get; set; }
    }


    public static class MessageEnum
    {

        #region CRUD Operations Messages

        public static string saveSuccess = "Save Successfully.";
        public static string updateSuccess = "Updated Successfully.";
        public static string deleteSuccess = "Deleted Successfully.";

        #endregion

        #region Authorizationa and Authentication Messages
        public static string LoggedIn = "User LoggedIn Successfully.";
        public static string UnAuthenticated = "Email or password incorrect.";
        public static string UnAuthorized = "UnAuthorized User.";
        public static string tokenExpire = "Token expired.";
        public static string invalidToken = "Invalid token.";
        public static string serverSideError = "Server Side Error Occur. Code : ";
        public static string validationError = "Validation Error :  ";

        #endregion

        #region Record Messages

        public static string dataFetched = "Data Fetched!.";
        public static string visitGenerated = "Visits Generated!.";
        public static string notFound = "Record Not Found!.";
        public static string recordExists = "Record Already Exisited!.";
        public static string userEmailExists = "Username or Email Already Exists.";
        public static string recordActivated = "Activated Successfully.";
        public static string recordDeactivated = "Record deactivated.";
        public static string EPICenterNameReq = "Please Enter EPI Center Name!";
        public static string SequenceNoExist = "Sequence # Already Exist:  ";

        #endregion
    }
    public static class RolesEnum
    {
        public static string superAdmin = "SuperAdmin";
        public static string fedralUser = "FedralUser";
        public static string provisionalUser = "ProvisionalUser";
        public static string divisionalUser = "DivisionalUser";
        public static string districtUser = "DistrictUser";
        public static string tehsilUser = "TehsilUser";
        public static string uCUser = "UnionCouncilUser";
        public static string sIAUCUser = "SIAUCUser";
    }




}
