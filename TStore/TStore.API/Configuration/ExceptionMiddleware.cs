using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using TStore.Business.Exceptions;

namespace TransactionStore.API.Configuration
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private const string _messageValidation = "Validation exception";
        private const string _messageFileNotFound = "File Not Found exception";
        private const string _messageUnknown = "Unknown error";
        private const int _unknownCode = 3000;
        private const int _fileNotFoundCode = 4004;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (FileNotFoundException ex)
            {
                await HandleFileNotFoundExceptionMessageAsync(context, ex, _messageFileNotFound);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionMessageAsync(context, ex, _messageValidation);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex);
            }
        }

        private static Task HandleFileNotFoundExceptionMessageAsync(HttpContext context, FileNotFoundException exception, string message)
        {
            context.Response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(new ExceptionResponse
            {
                Code = _fileNotFoundCode,
                Message = message,
                Description = exception.Message
            });
            context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
            return context.Response.WriteAsync(result);
        }

        private static Task HandleValidationExceptionMessageAsync(HttpContext context, ValidationException exception, string message)
        {
            context.Response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(new ValidationExceptionResponse(exception)
            {
                Message = message
            });
            context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
            return context.Response.WriteAsync(result);
        }

        private static Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(new
            {
                code = _unknownCode,
                message = _messageUnknown,
                description = exception.Message
            });
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }
    }
}