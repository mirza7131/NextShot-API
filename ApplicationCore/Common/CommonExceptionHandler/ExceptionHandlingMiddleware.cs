using System.Net;
using System.Text.Json;
using CommonDTOs.ResponseDTO;
using CommonExceptionHandler;
using CommonMessages;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;

namespace ExceptionHandling.CustomMiddlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = CommonStringConstant.contentTypeApplicationJson;
        var response = context.Response;

        var errorResponse = new ResponseError();

        switch (exception)
        {
            case UserFriendlyException ex:

                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.statusCode = HttpStatusCode.BadRequest;
                //errorResponse.isShowFromInterceptor = ex.iss;
                errorResponse.message = await GetExceptionMessage(ex);

                break;
            case UserFriendlyExceptionForUI ex:

                response.StatusCode = (int)(HttpStatusCode)1000;
                errorResponse.statusCode = (HttpStatusCode)1000;
                errorResponse.message = await GetExceptionMessage(ex);

                break;
            case SqlException ex:

                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.statusCode = HttpStatusCode.BadRequest;
                errorResponse.message = await GetExceptionMessage(ex);

                break;
            case ApplicationException ex:
                if (ex.Message.Contains(CommonStringConstant.InvalidToken))
                {
                    response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    errorResponse.message = await GetExceptionMessage(ex);
                    break;
                }
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse.statusCode = HttpStatusCode.BadRequest;
                errorResponse.message = await GetExceptionMessage(ex);
                break;
            case Exception ex:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.message = await GetExceptionMessage(ex); // CommonMessageConstant.InternalServerError;
                break;
            default:
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                errorResponse.message = CommonMessageConstant.InternalServerError;
                break;
        }

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        //_logger.LogError(exception.Message);
        var result = JsonSerializer.Serialize(errorResponse);
        await context.Response.WriteAsync(result);
    }

    private async Task<string> GetExceptionMessage(Exception ex)
    {
        return await Task.FromResult(ex.InnerException != null ? ex.InnerException.Message : ex.Message);
    }
}