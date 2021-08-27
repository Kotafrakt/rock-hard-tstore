﻿using DevEdu.API.Configuration;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using TStore.Business.Exceptions;

namespace TransactionStore.API.Configuration
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private const string _messageAuthorization = "Authorization exception";
        private const string _messageValidation = "Validation exception";
        private const string _messageEntity = "Entity not found exception";
        private const string _messageUnknown = "Unknown error";
        private const int _authorizationCode = 2000;
        private const int _entityCode = 400;
        private const int _unknownCode = 3000;

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
            catch (AuthorizationException ex)
            {
                await HandlerExceptionMessageAsync(context, ex, HttpStatusCode.Forbidden);
            }
            catch (EntityNotFoundException ex)
            {
                await HandlerExceptionMessageAsync(context, ex, HttpStatusCode.NotFound);
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

        private static Task HandlerExceptionMessageAsync(HttpContext context, Exception exception, HttpStatusCode statusCode)
        {

            var code = statusCode == HttpStatusCode.Forbidden ? _authorizationCode : _entityCode;
            var message = statusCode == HttpStatusCode.Forbidden ? _messageAuthorization : _messageEntity;

            context.Response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(
                new ExceptionResponse
                {
                    Code = code,
                    Message = message,
                    Description = exception.Message
                }
            );
            context.Response.StatusCode = (int)statusCode;
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

