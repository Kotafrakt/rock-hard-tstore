using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
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
        private const string _messageCurrencyRatesNotFound = "Currency Rates Not Found exception";
        private const string _messageCurrencyRatesNotValid = "Currency Rates Not Valid exception";
        private const string _messageUnknown = "Unknown error";
        private const int _unknownCode = 3000;
        private const int _currencyNotValidCode = 5000;
        private const int _currencyRatesNotFoundCode = 6000;

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
            catch (CurrencyRatesNotFoundException ex)
            {
                Log.Error($"error: {ex.Message}");
                await HandleCurrencyRatesNotFoundExceptionMessageAsync(context, ex, _messageCurrencyRatesNotFound);
            }
            catch (CurrencyNotValidException ex)
            {
                Log.Error($"error: {ex.Message}");
                await HandleCurrencyNotValidExceptionMessageAsync(context, ex, _messageCurrencyRatesNotValid);
            }
            catch (FileNotFoundException ex)
            {
                var exc = new CurrencyRatesNotFoundException("There are no current Currency Rates");
                Log.Error($"error: {exc.Message}");
                await HandleCurrencyRatesNotFoundExceptionMessageAsync(context, exc, _messageCurrencyRatesNotFound); 
            }
            catch (DirectoryNotFoundException ex)
            {
                var exc = new CurrencyRatesNotFoundException("There are no current Currency Rates");
                Log.Error($"error: {exc.Message}");
                await HandleCurrencyRatesNotFoundExceptionMessageAsync(context, exc, _messageCurrencyRatesNotFound);
            }
            catch (ValidationException ex)
            {
                Log.Error($"error: {ex.Message}");
                await HandleValidationExceptionMessageAsync(context, ex, _messageValidation);
            }
            catch (Exception ex)
            {
                Log.Error($"error: {ex.Message}");
                await HandleExceptionMessageAsync(context, ex);  
            }
        }

        private static Task HandleCurrencyRatesNotFoundExceptionMessageAsync(HttpContext context, CurrencyRatesNotFoundException exception, string message)
        {
            context.Response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(new ExceptionResponse
            {
                Code = _currencyRatesNotFoundCode,
                Message = message,
                Description = exception.Message
            });
            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            return context.Response.WriteAsync(result);
        }

        private static Task HandleCurrencyNotValidExceptionMessageAsync(HttpContext context, CurrencyNotValidException exception, string message)
        {
            context.Response.ContentType = "application/json";
            var result = JsonConvert.SerializeObject(new ExceptionResponse
            {
                Code = _currencyNotValidCode,
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