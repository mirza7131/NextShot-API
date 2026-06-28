using CommonMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CommonDTOs.ResponseDTO
{
    public class ResponseDTO
    {
        public HttpStatusCode statusCode { get; set; }
        public bool status { get; set; } = true;
        public string message { get; set; } = CommonMessageConstant.Read;
        public Object? data { get; set; }

    }

    public class ResponseSuccess : ResponseDTO
    {
        public ResponseSuccess()
        {
            this.statusCode = HttpStatusCode.OK;
            this.status = true;
            this.message = String.Empty;
        }
    }

    public class ResponseSave : ResponseDTO
    {
        public ResponseSave()
        {
            this.statusCode = HttpStatusCode.OK;
            this.status = true;
            this.message = CommonMessageConstant.Save;
        }
    }

    public class ResponseUpdate : ResponseDTO
    {
        public ResponseUpdate()
        {
            this.statusCode = HttpStatusCode.OK;
            this.status = true;
            this.message = CommonMessageConstant.Update;
        }
    }

    public class ResponseDelete : ResponseDTO
    {
        public ResponseDelete()
        {
            this.statusCode = HttpStatusCode.OK;
            this.status = true;
            this.message = CommonMessageConstant.Delete;
        }
    }

    public class ResponseError : ResponseDTO
    {
        public bool isShowFromInterceptor { get; set; }
        public ResponseError()
        {
            this.isShowFromInterceptor = true;
            this.statusCode = HttpStatusCode.InternalServerError;
            this.status = false;
        }
    }

    public class ResponseValidationError : ResponseDTO
    {
        public ResponseValidationError()
        {
            this.statusCode = HttpStatusCode.BadRequest;
            this.status = false;
            this.message = "Validation Error";
        }
    }

    public class ResponsePaginatedDTO : ResponseDTO
    {
        public int currentPageNumber { get; set; } = 0;
        public int totalRecords { get; set; }
        public int size { get; set; } = 10;
        public int pageCount { get; set; }

    }

    public class ResponseForHrAPIsDTO
    {
        public HttpStatusCode statusCode { get; set; }
        public bool status { get; set; } = true;
        public string message { get; set; } = CommonMessageConstant.Read;
        public moduleList? data { get; set; }

    }
    public class moduleList
    {
        public List<moduleName> modules { get; set; }

    }
    public class moduleName
    {
        public string name { get; set; }
        public int count { get; set; }

    }
    public class ResponseForIntegratedDashboad
    {
        public string message { get; set; } = CommonMessageConstant.Read;
        public bool status { get; set; } = true;
        public int? totalRecords { get; set; } = 0;
        public object? list { get; set; }
        public object? data { get; set; }
        

    }

}
